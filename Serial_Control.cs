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
        static SerialPort opto_serial;        // 시리얼 포트 변수 생성
        static StringBuilder _buffer = new StringBuilder();
        string recv_str = string.Empty;

        private const int DLEAY_10 = 10;
        private const int DLEAY_20 = 20;

        private const int X_MAX_DIST = 30000;//real 1350000;
        private const int Y_MAX_DIST = 20000;//real 200000;
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
            //CheckForIllegalCrossThreadCalls = false;
            string comport_str = string.Empty;// comboBox_available_port.Text;

            if (comboBox_available_port.InvokeRequired == true)
                comboBox_available_port.Invoke(new MethodInvoker(delegate () { comport_str = comboBox_available_port.Text; }));
            else
                comport_str = comboBox_available_port.Text;

            opto_serial = new SerialPort
            {
                BaudRate = 115200,  // 보드레이트 설정
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 500,
                WriteTimeout = 500
            };
            opto_serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);

            if (!opto_serial.IsOpen)
            {
                if (comboBox_available_port.InvokeRequired == true)
                    comboBox_available_port.Invoke(new MethodInvoker(delegate () { opto_serial.PortName = comboBox_available_port.Text; }));
                else
                    opto_serial.PortName = comboBox_available_port.Text;

                try
                {
                    opto_serial.Open();
                    Thread.Sleep(10);

                    textBox_RX_data.Clear();

                    if (opto_serial_write(TRY_CONNECT, false))
                    {
                        comport_str += " - Open Success !";

                        this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str });

                        Connection_display(true);
                    }
                    else
                    {
                        opto_serial.Close();
                        comport_str += " - Open Something wrong !";
                        this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str });

                        Connection_display(false);
                    }
                }
                catch (Exception e)
                {
                    opto_serial.Close();
                    if (this.IsHandleCreated)  this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : " + e.Message });
                }
            }
            else
            {
                if (opto_serial_write(TRY_CONNECT, true) == true)
                {
                    textBox_RX_data.Clear();
                    comport_str += " - Already Opened !";
                    this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str });
                    Connection_display(true);
                }
                else
                {
                    this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str + " Connection Something Wrong" });
                    Connection_display(false);
                }
            }
        }
        private void Disconnection()
        {
            byte[] serial_send_data = new byte[8];
            string comport_str = comboBox_available_port.Text;

            if (opto_serial.IsOpen)
            {
                try
                {
                    opto_serial.Close();
                    comport_str += " - Closed !";
                    this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str });
                }
                catch
                {
                    MessageBox.Show(Form.ActiveForm, "SEIRAL PORT Close FAIL !", " Error");
                }
            }
            else
            {
                opto_serial.Close();
                comport_str += " - Closed!";
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str });
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
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { str + " Serial Transfer Thread Fail" });
            }
        }

        private bool opto_serial_write(string str, bool check)
        {
            if (opto_serial.IsOpen == false)
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "통신연결을 확인해주세요" });
                return false;
            }

            opto_serial.Write('#' + str + '*');

            this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "#" + str + "*" });

            if (check == false) return true;

            if (serial_sleep_wait(str) == false)
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { str + " Transfer Fail" });
                return false;
            }
            else
            {
                if (str.Contains(TRY_CONNECT)) return true;

                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { str + " Transfer Success" });
                return true;
            }
        }

        private void parse_string(string recv)
        {
            /*********************************************************************************/
            /**************** Received func **************************************************/
            /*********************************************************************************/
            if (recv.Contains(TRY_CONNECT))
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });

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
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });
            }
            else if (recv.Contains(MOTOR_POS))
            {
                string position = recv.Substring(10);
                if (cbdebug.Checked == true) this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });
                if (recv[8] == 'x') { if (lbcurposx.InvokeRequired) { lbcurposx.Invoke(new MethodInvoker(delegate () { lbcurposx.Text = position; ; })); } else lbcurposx.Text = position; }
                else if (recv[8] == 'y') { if (lbcurposy.InvokeRequired) { lbcurposy.Invoke(new MethodInvoker(delegate () { lbcurposy.Text = position; ; })); } else lbcurposy.Text = position; }
                else if (recv[8] == 'z') { if (lbcurposz.InvokeRequired) { lbcurposz.Invoke(new MethodInvoker(delegate () { lbcurposz.Text = position; ; })); } else lbcurposz.Text = position; }

                //this.Invalidate();  // request a delayed Repaint by the normal MessageLoop system    
                //this.Update();      // forces Repaint of invalidated area 
                //this.Refresh();     // Combines Invalidate() and Update()
            }
            else if (recv.Contains(MOTOR_STATUS_REQ))
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });
            }
            else if (recv.Contains(MOTOR_STATUS))
            {
                if (cbdebug.Checked == true) this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });

                Color backcolor;
                if (recv[11] == 'x')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' }); }

                    if (btMoveXasix.InvokeRequired) { btMoveXasix.Invoke(new MethodInvoker(delegate () { btMoveXasix.BackColor = backcolor; ; })); } else btMoveXasix.BackColor = backcolor;
                }
                else if (recv[11] == 'y')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' }); }

                    if (btMoveYasix.InvokeRequired) { btMoveYasix.Invoke(new MethodInvoker(delegate () { btMoveYasix.BackColor = backcolor; ; })); } else btMoveYasix.BackColor = backcolor;
                }
                else if (recv[11] == 'z')
                {
                    if (recv.Contains("idle")) backcolor = Color.LightCyan;
                    else if (recv.Contains("busy")) backcolor = Color.MistyRose;
                    else { backcolor = Color.Red; this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' }); }

                    if (btMoveZasix.InvokeRequired) { btMoveZasix.Invoke(new MethodInvoker(delegate () { btMoveZasix.BackColor = backcolor; ; })); } else btMoveZasix.BackColor = backcolor;
                }
            }
            else
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Motor Status Wrong Text Recv : " + recv });
            }
        }
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //this.Invoke(new EventHandler(MySerialReceived));
            //MySerialReceived(sender, e);

            //if (opto_serial.IsOpen)
            //MySerialReceivedByte(sender, e);
            MySerialReceivedByteBinary(sender, e);
        }

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

                //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { $"Extracted Data : {extractedData}" });
                parse_string(extractedData);

                // 처리된 데이터와 토큰 제거
                data = data.Substring(endIndex + endToken.Length);
                startIndex = data.IndexOf(startToken);
                endIndex = data.IndexOf(endToken, startIndex + startToken.Length);

                //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { $"After data : {data}" });
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
                if (this.IsHandleCreated)   this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { $"Error processing data: {ex.Message}" });
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
            //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "END - " });

            recv_str = Encoding.Default.GetString(recvbytes, 0, idx);
            parse_string(recv_str.Substring(1, recv_str.Length - 2));

            Thread.Sleep(1);
            //Task.Delay(TimeSpan.FromMilliseconds(0.1)).GetAwaiter().GetResult();
        }
    }
}