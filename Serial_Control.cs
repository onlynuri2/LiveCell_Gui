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
        const string TRY_CONNECT = "TryConnection";
        const string SERVO_POS       = "servopos";
        const string SERVO_STATUS = "servostatus";
        const string SERVO_STATUS_IDLE = "servostatusidle";
        const string SERVO_STATUS_BUSY = "servostatusbusy";
        private SerialPort opto_serial = new SerialPort();        // 시리얼 포트 변수 생성
        string recv_str = string.Empty;

        private const int DLEAY_10 = 10;
        private const int DLEAY_20 = 20;

        private const int X_MAX_DIST = 120000;//real 1350000;
        private const int Y_MAX_DIST = 20000;//real 200000;
        private const int Z_MAX_DIST = 20000;//real 125000;

        private const int MAX_SPEED_X = 300000;
        private const int DEFAULT_SPEED_X = 100000;

        private const int MAX_SPEED_Y = 100000;
        private const int DEFAULT_SPEED_Y = 80000;

        private const int MAX_SPEED_Z = 50000;
        private const int DEFAULT_SPEED_Z = 20000;

        private const int LED_BR_MAX = 10000;

        private void Connection()
        {
            //CheckForIllegalCrossThreadCalls = false;
            string comport_str = string.Empty;// comboBox_available_port.Text;

            if (comboBox_available_port.InvokeRequired == true)
                comboBox_available_port.Invoke(new MethodInvoker(delegate () { comport_str = comboBox_available_port.Text; }));
            else
                comport_str = comboBox_available_port.Text;

            if (!opto_serial.IsOpen)
            {
                if (comboBox_available_port.InvokeRequired == true)
                    comboBox_available_port.Invoke(new MethodInvoker(delegate () { opto_serial.PortName = comboBox_available_port.Text; }));
                else
                    opto_serial.PortName = comboBox_available_port.Text;

                opto_serial.BaudRate = 115200;
                opto_serial.DataBits = 8;
                opto_serial.Parity = Parity.None;
                opto_serial.StopBits = StopBits.One;
                opto_serial.ReadTimeout = 100;
                opto_serial.WriteTimeout = 100;
                opto_serial.Handshake = Handshake.None;
                //opto_serial.RtsEnable = true;

                try
                {
                    opto_serial.Open();
                    Thread.Sleep(10);

                    textBox_RX_data.Clear();

                    if (opto_serial_write(TRY_CONNECT, true))
                    {
                        comport_str += " - Open Success !";

                        this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { comport_str } );

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
                    this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : " + e.Message });
                    MessageBox.Show(Form.ActiveForm, comport_str + " Port Connection Fail !", " Error");
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

                if (recv_str.Contains(str))
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
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { str  + " Transfer Success"});
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
                //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "TRY CONNECT Received" });

                if (recv[14] == '1') { gbxaxis.BackColor = Color.Lavender; MotorLive[0] = 1; } else { gbxaxis.BackColor = Color.Lavender; }
                if (recv[15] == '1') { gbyaxis.BackColor = Color.Lavender; MotorLive[1] = 1; } else { gbyaxis.BackColor = Color.Lavender; }
                if (recv[16] == '1') { gbzaxis.BackColor = Color.Lavender; MotorLive[2] = 1; } else { gbzaxis.BackColor = Color.Lavender; }
            }
            else if (recv.Contains(MCU_LIVE_TEST))
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "MCU LIVE TEST Received" });
            }
            else if (recv.Contains(SERVO_POS))
            {
                string position = recv.Substring(10);
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });
                if (recv[8] == '0') { if (lbcurposx.InvokeRequired) { lbcurposx.Invoke(new MethodInvoker(delegate () { lbcurposx.Text = position; ; })); } else lbcurposx.Text = position; }
                else if (recv[8] == '1') { if (lbcurposy.InvokeRequired) { lbcurposy.Invoke(new MethodInvoker(delegate () { lbcurposy.Text = position; ; })); } else lbcurposy.Text = position; }
                else if (recv[8] == '2') { if (lbcurposz.InvokeRequired) { lbcurposz.Invoke(new MethodInvoker(delegate() { lbcurposz.Text = position; ; })); } else lbcurposz.Text = position; }
            }
            else if (recv.Contains(SERVO_STATUS))
            {
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { '#' + recv + '*' });

                if (recv[11] == '0')
                {
                    if (recv.Contains("idle")) btMoveXasix.BackColor = Color.LightCyan;
                    else if(recv.Contains("busy")) btMoveXasix.BackColor = Color.MistyRose;
                    else btMoveXasix.BackColor = Color.Red;
                }
                else if (recv[11] == '1')
                {
                    if (recv.Contains("idle")) btMoveYasix.BackColor = Color.LightCyan;
                    else if (recv.Contains("busy")) btMoveYasix.BackColor = Color.MistyRose;
                    else btMoveYasix.BackColor = Color.Red;
                }
                if (recv[11] == '2')
                {
                    if (recv.Contains("idle")) btMoveZasix.BackColor = Color.LightCyan;
                    else if (recv.Contains("busy")) btMoveZasix.BackColor = Color.MistyRose;
                    else btMoveZasix.BackColor = Color.Red;
                }


            }
        }
        private void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //this.Invoke(new EventHandler(MySerialReceived));
            //MySerialReceived(sender, e);

            //if (opto_serial.IsOpen)
            MySerialReceivedByte(sender, e);
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
                    if (recvbytes[idx - 1] == '*') { recvbytes[idx] = 0; break; }
                    loopcnt = 0;
                }

            } while (loopcnt++ < 100000);

            //TimeSpan span = DateTime.Now - start;
            //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "END - " });

            recv_str = Encoding.Default.GetString(recvbytes, 0, idx);
            parse_string(recv_str.Substring(1, recv_str.Length - 2));

            Thread.Sleep(DLEAY_10);
        }

        private void MySerialReceived(object s, EventArgs e)  //여기에서 수신 데이타를 사용자의 용도에 따라 처리한다.
        {
            int cnt = 0, old_recv_len = 0;
            recv_str = String.Empty;
            char start, end;
            //Thread.Sleep(1);
            string starts = String.Empty, ends = String.Empty; ;
            do
            {
                recv_str += opto_serial.ReadExisting();

                if ((recv_str.Length > 0) && (recv_str[recv_str.Length - 1] == '*')) break; ;

                if (recv_str.Length != old_recv_len) { old_recv_len = recv_str.Length; cnt = 0; }

                Thread.Sleep(1);

            } while (cnt++ < 10);

            if (recv_str.Length == 0) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "length 0" }); return; }

            start = recv_str[0];
            if (start != '#') { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Not #" + recv_str }); return; }

            end = recv_str[recv_str.Length - 1];
            if (end != '*') { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Not *" + recv_str }); return; }

            if (end == '*')
            {
                //parse_string(recv_str.Substring(1, recv_str.Length - 2));
                //this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "recv-" +recv_str.Substring(0, recv_str.Length - 1) });
            }
            else
                this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Someting Wrong : " });
        }
    }
}
