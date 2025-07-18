﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading; //스레드 클래스 사용
using System.Diagnostics;
using System.Drawing;


namespace LiveCell_Gui
{
    partial class LiveCell
    {
        /* Receive  */

        /* Send */
        const string MCU_LIVE_TEST = "mculive";
        const string MOTOR_LIVE_TEST = "motorlive";
        const string TRY_CONNECT = "TryConnection";
        const string MOTOR_STATUS_REQ = "motorstatusreq";
        const string MOTOR_POS = "motorpos";
        const string MOTOR_STATUS = "motorstatus";
        const string MOTOR_STATUS_IDLE = "motorstatusidle";
        const string MOTOR_STATUS_BUSY = "motorstatusbusy";
        private static SerialPort? opto_serial;// = new SerialPort();

        private List<byte> recvBuffer = new List<byte>();  // 데이터 버퍼

        static StringBuilder _buffer = new StringBuilder();
        string recv_str = string.Empty;

        private const int DLEAY_10 = 10;
        private const int DLEAY_20 = 20;

        private const int X_MAX_DIST = 30000;//real 1350000;
        private const int Y_MAX_DIST = 35000;//real 200000;
        private const int Z_MAX_DIST = 14000;//real 125000;

        private const int MAX_SPEED_X = 200000;
        private const int DEFAULT_SPEED_X = 100000;

        private const int MAX_SPEED_Y = 150000;
        private const int DEFAULT_SPEED_Y = 66000;

        private const int MAX_SPEED_Z = 10000;
        private const int DEFAULT_SPEED_Z = 6000;

        private const int LED_BR_MAX = 10000;

        private void Connection()
        {
            if (this.InvokeRequired)
            {
                // UI Thread에서 실행
                this.Invoke(new Action(() => Connection()));
                return;
            }

            //CheckForIllegalCrossThreadCalls = false;
            string comport_str = string.Empty;// comboBox_available_port.Text;

            if (comboBox_available_port.InvokeRequired == true)
                comboBox_available_port.Invoke(new MethodInvoker(delegate () { comport_str = comboBox_available_port.Text; }));
            else
                comport_str = comboBox_available_port.Text;
#if false
            opto_serial = new SerialPort
            {
                PortName = comboBox_available_port.Text;
                BaudRate = 115200,  // 보드레이트 설정
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 100,
                WriteTimeout = 100
            };
//#else
            //opto_serial = new SerialPort();
            opto_serial.PortName = comboBox_available_port.Text;
            opto_serial.BaudRate = 115200;
            opto_serial.DataBits = 8;
            opto_serial.Parity = Parity.None;
            opto_serial.StopBits = StopBits.One;
            opto_serial.ReadTimeout = 100;
            opto_serial.WriteTimeout = 100;
            opto_serial.Handshake = Handshake.None;
#endif
            opto_serial = new SerialPort(comport_str, 115200, Parity.None, 8, StopBits.One);
            opto_serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
            opto_serial.ErrorReceived += OptoSerial_ErrorReceived;

            if (opto_serial != null && !opto_serial.IsOpen)
            {
                try
                {
                    opto_serial.Open();
                    Thread.Sleep(10);

                    display_data_RX_textbox(string.Empty);

                    if (opto_serial_write(TRY_CONNECT, false))
                    {
                        comport_str += " - Open Success !";

                        display_data_RX_textbox(comport_str);

                        Connection_display(true);
                    }
                    else
                    {
                        opto_serial.Dispose();
                        comport_str += " - Open Something wrong !";
                        display_data_RX_textbox(comport_str);

                        Connection_display(false);
                    }
                }
                catch (Exception e)
                {
                    opto_serial.Dispose();
                    if (this.IsHandleCreated)  display_data_RX_textbox("Error : " + e.Message);
                }
            }
            else
            {
                if (opto_serial_write(TRY_CONNECT, true) == true)
                {
                    display_data_RX_textbox(string.Empty);
                    comport_str += " - Already Opened !";
                    display_data_RX_textbox(comport_str);
                    Connection_display(true);
                }
                else
                {
                    display_data_RX_textbox(comport_str + " Connection Something Wrong");
                    Connection_display(false);
                }
            }
        }
        private void Disconnection()
        {
            byte[] serial_send_data = new byte[8];
            string comport_str = comboBox_available_port.Text;

            if (opto_serial != null && opto_serial.IsOpen)
            {
                Thread closeThread = new Thread(() =>
                {
                    try
                    {
                        opto_serial.DataReceived -= serial_DataReceived;
                        opto_serial.ErrorReceived -= OptoSerial_ErrorReceived;
                        opto_serial.Dispose();
                        // UI 갱신은 메인 스레드에서
                        this.BeginInvoke(new Action(() =>
                        {
                            comport_str += " - Closed !";
                            display_data_RX_textbox(comport_str);
                        }));
                    }
                    catch
                    {
                        MessageBox.Show(Form.ActiveForm, "SEIRAL PORT Close FAIL !", " Error");
                    }

                });
                
                closeThread.IsBackground = true;
                closeThread.Start();
            }
            else
            {
                if (opto_serial != null) opto_serial.Dispose();
                comport_str += " - Closed!";
                display_data_RX_textbox(comport_str);
            }

            Connection_display(false);
        }
        private bool serial_sleep_wait(string str)
        {
            recv_str = string.Empty;
            int i;
            for (i = 0; i < DLEAY_10; i++)
            {
                Thread.Sleep(DLEAY_10);

                if ((_buffer.ToString().IndexOf(str) >= 0) || recv_str.Contains(str))
                    return true;
            }

            return false;
        }

        private void serial_write_thread(string str, bool check)
        {
            if (opto_serial.IsOpen == false) return;

            opto_serial.Write("#" + str + "*");

            if (check == false) return;

            if (serial_sleep_wait(str) == false)
            {
                display_data_RX_textbox(str + " Serial Transfer Thread Fail");
            }
        }

        private bool opto_serial_write(string str, bool check)
        {
            if (opto_serial.IsOpen == false)
            {
                display_data_RX_textbox("통신연결을 확인해주세요");
                Disconnection();
                return false;
            }

            try
            {

                opto_serial.Write('#' + str + '*');

                display_data_RX_textbox("#" + str + "*");

                if (check == false) return true;

                if (serial_sleep_wait(str) == false)
                {
                    display_data_RX_textbox(str + " Transfer Fail");
                    return false;
                }
                else
                {
                    if (str.Contains(TRY_CONNECT)) return true;

                    display_data_RX_textbox(str + " Transfer Success");
                    return true;
                }
            }
            catch (IOException ex)// COM 포트 분리된 상태
            {
                display_data_RX_textbox("포트에 쓰기 실패 - 장치가 제거되었습니다.");
                Disconnection();
            }
            catch (InvalidOperationException ex)// 포트 닫힌 상태
            {
                display_data_RX_textbox("포트에 쓰기 실패 - 포트가 닫혀 있습니다.");
            }

            return false;
        }

        private void parse_string(string recv)
        {
            /*********************************************************************************/
            /**************** Received func **************************************************/
            /*********************************************************************************/
            if (recv.Contains(TRY_CONNECT))
            {
                display_data_RX_textbox('#' + recv + '*');

                if (recv[14] == '1')
                {
                    if (gbxaxis.InvokeRequired) { gbxaxis.Invoke(new MethodInvoker(delegate () { gbxaxis.BackColor = Color.Lavender; ; })); } else gbxaxis.BackColor = Color.Lavender;
                    MotorLive[0] = 1;
                }
                if (recv[15] == '1')
                {
                    if (gbyaxis.InvokeRequired) { gbyaxis.Invoke(new MethodInvoker(delegate () { gbyaxis.BackColor = Color.Lavender; ; })); } else gbyaxis.BackColor = Color.Lavender;
                    MotorLive[1] = 1;
                }
                if (recv[16] == '1')
                {
                    if (gbzaxis.InvokeRequired) { gbzaxis.Invoke(new MethodInvoker(delegate () { gbzaxis.BackColor = Color.Lavender; ; })); } else gbzaxis.BackColor = Color.Lavender;
                    MotorLive[2] = 1;
                }
            }
            else if (recv.Contains(MOTOR_LIVE_TEST))
            {
                display_data_RX_textbox('#' + recv + '*');
            }
            else if (recv.Contains(MOTOR_POS))
            {
                string position = recv.Substring(10);
                if (cbdebug.Checked == true) display_data_RX_textbox('#' + recv + '*');
                if (recv[8] == 'x') { if (lbcurposx.InvokeRequired) { lbcurposx.Invoke(new MethodInvoker(delegate () { lbcurposx.Text = position; ; })); } else lbcurposx.Text = position; }
                else if (recv[8] == 'y') { if (lbcurposy.InvokeRequired) { lbcurposy.Invoke(new MethodInvoker(delegate () { lbcurposy.Text = position; ; })); } else lbcurposy.Text = position; }
                else if (recv[8] == 'z') { if (lbcurposz.InvokeRequired) { lbcurposz.Invoke(new MethodInvoker(delegate () { lbcurposz.Text = position; ; })); } else lbcurposz.Text = position; }

                //this.Invalidate();  // request a delayed Repaint by the normal MessageLoop system    
                //this.Update();      // forces Repaint of invalidated area 
                //this.Refresh();     // Combines Invalidate() and Update()
            }
            else if (recv.Contains(MOTOR_STATUS_REQ))
            {
                display_data_RX_textbox('#' + recv + '*');
            }
            else if (recv.Contains(MOTOR_STATUS))
            {
                if (cbdebug.Checked == true) display_data_RX_textbox('#' + recv + '*');

                Color backcolor;
                if (recv[11] == 'x')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; display_data_RX_textbox('#' + recv + '*'); }

                    if (btMoveXasix.InvokeRequired) { btMoveXasix.Invoke(new MethodInvoker(delegate () { btMoveXasix.BackColor = backcolor; ; })); } else btMoveXasix.BackColor = backcolor;
                }
                else if (recv[11] == 'y')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; display_data_RX_textbox('#' + recv + '*'); }

                    if (btMoveYasix.InvokeRequired) { btMoveYasix.Invoke(new MethodInvoker(delegate () { btMoveYasix.BackColor = backcolor; ; })); } else btMoveYasix.BackColor = backcolor;
                }
                else if (recv[11] == 'z')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; display_data_RX_textbox('#' + recv + '*'); }

                    if (btMoveZasix.InvokeRequired) { btMoveZasix.Invoke(new MethodInvoker(delegate () { btMoveZasix.BackColor = backcolor; ; })); } else btMoveZasix.BackColor = backcolor;
                }
            }
            else
            {
                display_data_RX_textbox("Motor Status Wrong Text Recv : " + recv);
            }
        }
        private void OptoSerial_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            // 예: 이벤트 종류 표시
            display_data_RX_textbox($"[ErrorReceived] EventType = {e.EventType}");

            // 장치 제거 감지 시 안전 닫기
            if (e.EventType == SerialError.Frame || e.EventType == SerialError.RXOver)
            {
                Disconnection();
            }
        }
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (opto_serial.IsOpen)
            //MySerialReceivedByte(sender, e);
            //MySerialReceivedByteBinary(sender, e);
            serial_DataReceived_list(sender, e);
        }

        /********************************************************************************************************/
        /******************************* serial_DataReceived_list ***********************************************/
        /********************************************************************************************************/
        private void serial_DataReceived_list(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytesToRead = opto_serial.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                opto_serial.Read(buffer, 0, bytesToRead);

                lock (recvBuffer) // 스레드 안전 처리
                {
                    recvBuffer.AddRange(buffer);

                    while (true)
                    {
                        int startIdx = recvBuffer.IndexOf((byte)'#');
                        int endIdx = recvBuffer.IndexOf((byte)'*');

                        if (startIdx != -1 && endIdx > startIdx + 3)  // 최소 크기 보장
                        {
                            byte[] packet = recvBuffer.Skip(startIdx).Take(endIdx - startIdx + 1).ToArray();
                            recvBuffer.RemoveRange(0, endIdx + 1); // 처리된 패킷 삭제

                            ProcessPacket(packet);
                        }
                        else
                        {
                            break; // 데이터 부족하면 루프 종료
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)   display_data_RX_textbox($"Error processing data: {ex.Message}");
            }
        }
        private void ProcessPacket(byte[] packet)
        {
            if (packet.Length < 5) return; // 최소 길이 체크 (CRC 2바이트 포함)

            int delsize = 2;
            byte[] dataWithoutCRC = packet.Skip(1).Take(packet.Length - delsize).ToArray(); // '#' 제거, CRC 제외
            
            recv_str = Encoding.UTF8.GetString(dataWithoutCRC);
            parse_string(recv_str);
        }

        /********************************************************************************************************/
        /******************************* MySerialReceivedByteBinary *********************************************/
        /********************************************************************************************************/
        private void ProcessBuffer()
        {
            string data = _buffer.ToString();
            const string startToken = "#"; // 시작 문자열
            const string endToken = "*";    // 종료 문자열

            int startIndex = data.IndexOf(startToken);
            int endIndex = data.IndexOf(endToken, startIndex + startToken.Length);

            // 시작과 종료 문자열이 모두 있는 경우 데이터 추출
            while (startIndex >= 0 && endIndex > startIndex)
            {
                int dataStart = startIndex + startToken.Length;
                int dataLength = endIndex - dataStart;
                string extractedData = data.Substring(dataStart, dataLength);

                //display_data_RX_textbox($"Extracted Data : {extractedData}");
                parse_string(extractedData);

                // 처리된 데이터와 토큰 제거
                data = data.Substring(endIndex + endToken.Length);
                startIndex = data.IndexOf(startToken);
                endIndex = data.IndexOf(endToken, startIndex + startToken.Length);

                //display_data_RX_textbox($"After data : {data}");
            }

            // 남은 데이터 버퍼에 다시 저장
            _buffer.Clear();
            _buffer.Append(data);
        }
        private void MySerialReceivedByteBinary(object s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {
            try
            {
                // 수신된 데이터 읽기
                int bytesToRead = opto_serial.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                opto_serial.Read(buffer, 0, bytesToRead);

                // 읽은 데이터를 StringBuilder에 추가
                string receivedData = Encoding.ASCII.GetString(buffer); // 바이너리 데이터를 ASCII 문자열로 변환
                _buffer.Append(receivedData);

                // 버퍼에서 데이터를 처리
                ProcessBuffer();
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)   display_data_RX_textbox($"Error processing data: {ex.Message}");
            }
        }

        private void MySerialReceivedByte(object s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {
            int loopcnt = 0, idx = 0;
            int readlen = opto_serial.BytesToRead;
            byte firstbyte;

            if (readlen == 0) return;
            firstbyte = (byte)opto_serial.ReadByte();
            if (firstbyte != '#') return;

            byte[] recvbytes = new byte[100];
            Array.Clear(recvbytes, 0x00, recvbytes.Length);

            recvbytes[idx++] = firstbyte;

            //DateTime start = DateTime.Now;            // 시간차 구하기

            do
            {
                if (opto_serial.BytesToRead > 0)
                {
                    recvbytes[idx++] = (byte)opto_serial.ReadByte();
                    if (recvbytes[idx - 1] == '*') { break; }
                    loopcnt = 0;
                }

            } while (loopcnt++ < 500);//300 ok

            //TimeSpan span = DateTime.Now - start;
            //display_data_RX_textbox("END - ");

            recv_str = Encoding.Default.GetString(recvbytes, 0, idx);
            parse_string(recv_str.Substring(1, recv_str.Length - 2));

            Thread.Sleep(1);
            //Task.Delay(TimeSpan.FromMilliseconds(0.1)).GetAwaiter().GetResult();
        }
    }
}