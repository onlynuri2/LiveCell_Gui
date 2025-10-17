//속성 -> 빌드 -> 조건부컴파일에 추가
//#define LIVECELL
//#define CGT

using System;
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

        const string CAPTURE_START = "CaptureStart";
        const string CAPTURE_STOP = "CaptureStop";

        private static SerialPort? opto_serial;// = new SerialPort();

        private List<byte> recvBuffer = new List<byte>();  // 데이터 버퍼

        static StringBuilder _buffer = new StringBuilder();
        volatile string recv_str = string.Empty;

        private const int DLEAY_10 = 10;
        private const int DLEAY_20 = 20;

        private byte[] MotorStatus;

#if LIVECELL
        private const int X_MAX_DIST = 300000;//real 1350000;
        private const int Y_MAX_DIST = 350000;//real 200000;
        private const int Z_MAX_DIST = 14000;//real 125000;

        private const int MAX_SPEED_X = 99999;
        private const int DEFAULT_SPEED_X = 80000;

        private const int MAX_SPEED_Y = 99999;
        private const int DEFAULT_SPEED_Y = 66000;

        private const int MAX_SPEED_Z = 99999;
        private const int DEFAULT_SPEED_Z = 6000;

        private const int DEFAULT_POS_X = 260000;
        private const int DEFAULT_POS_Y = 200000;
        private const int DEFAULT_POS_Z = 7000;

        private const int DEFAULT_OFFSET_X = 1000;
        private const int DEFAULT_OFFSET_Y = 1000;
        private const int DEFAULT_OFFSET_Z = 1000;
#endif
#if CGT
        private const int X_MAX_DIST = 150000;//real 1350000;
        private const int Y_MAX_DIST = 150000;//real 200000;
        private const int Z_MAX_DIST = 14000;//real 125000;

        private const int MAX_SPEED_X = 99999;
        private const int DEFAULT_SPEED_X = 50000;

        private const int MAX_SPEED_Y = 99999;
        private const int DEFAULT_SPEED_Y = 50000;

        private const int MAX_SPEED_Z = 10000;
        private const int DEFAULT_SPEED_Z = 6000;

        private const int DEFAULT_POS_X = 20000;
        private const int DEFAULT_POS_Y = 20000;
        private const int DEFAULT_POS_Z = 0;

        private const int DEFAULT_OFFSET_X = 19150;
        private const int DEFAULT_OFFSET_Y = 18600;
        private const int DEFAULT_OFFSET_Z = 7000;
#endif

        private const int LED_BR_MAX = 10000;

        private async Task Connection()
        {
            if (this.InvokeRequired)
            {
                // UI Thread에서 실행
                this.Invoke(new Action(async () => await Connection()));
                return;
            }

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
                    //Thread.Sleep(10);

                    display_data_RX_textbox(string.Empty);

                    bool ok = await opto_serial_write(TRY_CONNECT, true);
                    //if (opto_serial_write(TRY_CONNECT, true))
                    if(ok)
                    {
                        comport_str += " - Open Success !";

                        display_data_RX_textbox(comport_str);

                        Connection_display(true);
                        _= opto_serial_write(MOTOR_STATUS_REQ, false);
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
                bool ok = await opto_serial_write(TRY_CONNECT, true);
                //if (opto_serial_write(TRY_CONNECT, true) == true)
                if(ok)
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
                        this.BeginInvoke((MethodInvoker)(() =>
                        {
                            if (CGT_Viewer != null) { CGT_Viewer.Close(); }
                        }));

                        opto_serial.DataReceived -= serial_DataReceived;
                        opto_serial.ErrorReceived -= OptoSerial_ErrorReceived;
                        opto_serial.Dispose();
                        display_data_RX_textbox(comport_str);
                    }
                    catch (Exception ex)
                    {
                        display_data_RX_textbox("SEIRAL PORT Close FAIL !" + ex);
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
        private async Task<bool> serial_sleep_wait(string str)
        {
            //recv_str = string.Empty;
            int i;
            for (i = 0; i < DLEAY_10; i++)
            {
                //Thread.Sleep(DLEAY_10);
                await Task.Delay(100);

                Debug.WriteLine($"serial_sleep_wait cnt---------- ");
                if (recv_str.Contains(str))
                    return true;
					
				//Application.DoEvents(); // UI 메시지 처리 (임시방편)
            }

            return false;
        }

        private TaskCompletionSource<bool>? _recvTask;
        string expect_str = string.Empty;
        private async Task<bool> SerialWaitAsync(string expect, int timeoutMs = 100)
        {
            _recvTask = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            recv_str = string.Empty;
            expect_str = expect;

            // 타임아웃 설정
            using (var cts = new CancellationTokenSource(timeoutMs))
            {
                using (cts.Token.Register(() => _recvTask.TrySetResult(false)))
                {
                    return await _recvTask.Task.ConfigureAwait(false);
                }
            }
        }
        private async Task<bool> opto_serial_write(string str, bool check)
        {
            if (opto_serial == null || opto_serial.IsOpen == false)
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

                //if (serial_sleep_wait(str) == false)
                //bool ok = await (_ = serial_sleep_wait(str));
                bool ok = await SerialWaitAsync(TRY_CONNECT, 100);
                if (!ok)
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
                display_data_RX_textbox("포트에 쓰기 실패 - 장치가 제거되었습니다." + ex);
                Disconnection();
            }
            catch (InvalidOperationException ex)// 포트 닫힌 상태
            {
                display_data_RX_textbox("포트에 쓰기 실패 - 포트가 닫혀 있습니다." + ex);
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

                if (CGT_Viewer != null && opto_serial != null && opto_serial.IsOpen)
                {
                    if (int.TryParse(lbcurposx.Text, out int x) && int.TryParse(lbcurposy.Text, out int y) && int.TryParse(lbcurposz.Text, out int z))
                    {
                        if(CGT_Viewer != null) CGT_Viewer.Send_Current_Position_To_CGTViewer(x, y, z);
                    }
                    //else
                    //    MessageBox.Show(Form.ActiveForm, "Position Info Something Wrong", " Error!");
                }
            }
            else if (recv.Contains(MOTOR_STATUS_REQ))
            {
                display_data_RX_textbox('#' + recv + '*');

                int x = 1, y = 1, z = 1;

                try
                {
                    string valueString = recv.Split(':')[1];
                    string[] values = valueString.Split(',');

                    // TryParse를 사용하여 안전하게 변환
                    if (values.Length == 3 &&
                        int.TryParse(values[0], out x) &&
                        int.TryParse(values[1], out y) &&
                        int.TryParse(values[2], out z))
                    {
                        display_data_RX_textbox("Motor Status Text Recv : " + values[0] + values[1] + values[2]);
                        if (x == 1) MotorStatus[0] = 0;
                        if (y == 1) MotorStatus[1] = 0;
                        if (z == 1) MotorStatus[2] = 0;
                    }
                    else
                    {
                        display_data_RX_textbox("MOTOR_STATUS_REQ Wrong Text Recv : " + recv);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    display_data_RX_textbox("MOTOR_STATUS_REQ Wrong : " + recv); 
                }
            }
            else if (recv.Contains(MOTOR_STATUS))
            {
                if (cbdebug.Checked == true) display_data_RX_textbox('#' + recv + '*');

                Color backcolor;
                if (recv[11] == 'x')
                {
                    MotorStatus[0] = 1;

                    if (recv.Contains("idle")) { backcolor = Color.LightCyan; MotorStatus[0] = 0; }
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; display_data_RX_textbox('#' + recv + '*'); }

                    if (btMoveXasix.InvokeRequired) { btMoveXasix.Invoke(new MethodInvoker(delegate () { btMoveXasix.BackColor = backcolor; ; })); } else btMoveXasix.BackColor = backcolor;
                }
                else if (recv[11] == 'y')
                {
                    MotorStatus[1] = 1;

                    if (recv.Contains("idle")) { backcolor = Color.LightCyan; MotorStatus[1] = 0; }
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; display_data_RX_textbox('#' + recv + '*'); }

                    if (btMoveYasix.InvokeRequired) { btMoveYasix.Invoke(new MethodInvoker(delegate () { btMoveYasix.BackColor = backcolor; ; })); } else btMoveYasix.BackColor = backcolor;
                }
                else if (recv[11] == 'z')
                {
                    MotorStatus[2] = 1;

                    if (recv.Contains("idle")) { backcolor = Color.LightCyan; MotorStatus[2] = 0; }
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
            if (opto_serial != null && opto_serial.IsOpen)
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
                if (opto_serial != null && opto_serial.IsOpen == true)
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

            if (_recvTask != null && recv_str.Contains(expect_str)) // 예: 기대 문자열
            {
                _recvTask.TrySetResult(true);
                expect_str = string.Empty;
            }
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
                recv_str = data.Substring(dataStart, dataLength);

                //display_data_RX_textbox($"Extracted Data : {extractedData}");
                parse_string(recv_str);

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
                if (opto_serial != null && opto_serial.IsOpen == true)
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
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)   display_data_RX_textbox($"Error processing data: {ex.Message}");
            }
        }
    }
}