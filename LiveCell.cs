using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

using System.Threading; //Thread Class Using
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using System.Reflection;

namespace LiveCell_Gui
{
    public partial class LiveCell : Form
    {
        /*-----------------------------------------------------------------------------
        *  Serial Communication Variable
        *---------------------------------------------------------------------------*/
        delegate void SetTextCallBack(string str);      // Callback �Լ�

        private string disp_str = string.Empty;
        private byte[] b_disp_buf = new byte[100];
        private int comport_index = 0;

        private byte[] MotorLive = {  0, 0, 0 };

        //public static LiveCell livecell;

        public LiveCell()
        {
            InitializeComponent();
            //livecell = this;
        }

        private void Connection_display(bool is_connect)
        {
            if (is_connect)
            {
                this.Invoke(new Action(() =>
                {
                    btconnection.Enabled = false;
                    btdisconnection.Enabled = true;
                    btconnection.BackColor = Color.Transparent;
                    btdisconnection.BackColor = Color.LightBlue;
                    groupBox_comport.BackColor = Color.LightSteelBlue;
                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    btconnection.Enabled = true;
                    btdisconnection.Enabled = false;
                    btconnection.BackColor = Color.LightBlue;
                    btdisconnection.BackColor = Color.Transparent;
                    groupBox_comport.BackColor = Color.Transparent;

                    gbxaxis.BackColor = Color.GhostWhite;
                    gbyaxis.BackColor = Color.GhostWhite;
                    gbzaxis.BackColor = Color.GhostWhite;
                }));
            }
        }
        private void LiveCell_Load(object sender, EventArgs e)
        {
            //Populate the Combobox with SerialPorts on the System
            comport_info_update();

            this.Text = "LiveCell - " + "V1.0";

            opto_serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
        }
        private void LiveCell_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnection();
        }
        private void btconnection_Click(object sender, EventArgs e)
        {
            if (comboBox_available_port.Items.Count <= 1)//if (comboBox_available_port.SelectedValue.ToString() > 0)
            {
                MessageBox.Show(Form.ActiveForm, "No Serial Port !!", " Error");
                return;
            }

            Connection();
        }

        private void btdisconnection_Click(object sender, EventArgs e)
        {
            Disconnection();
        }

        /*-----------------------------------------------------------------------------
        *  Display Function
        *---------------------------------------------------------------------------*/
        private void display_data_textbox(string str)
        {
            this.textBox_RX_data.AppendText(str + Environment.NewLine);
        }

        private void comport_info_update()
        {
            string[] port = SerialPort.GetPortNames();
            comboBox_available_port.Items.Clear();

            comboBox_available_port.Items.Add("Select");

            foreach (string portName in port)
            {
                if (!comboBox_available_port.Items.Contains(portName))
                    comboBox_available_port.Items.Add(portName);
            }

            if (comboBox_available_port.Items.Count > comport_index)
                comboBox_available_port.SelectedIndex = comport_index;
            else
                comboBox_available_port.SelectedIndex = comport_index = 0;
        }

        private void comboBox_available_port_SelectedIndexChanged(object sender, EventArgs e)
        {
            comport_index = comboBox_available_port.SelectedIndex;
        }
        private void comboBox_available_port_Click(object sender, EventArgs e)
        {
            textBox_RX_data.Clear();

            string[] port = SerialPort.GetPortNames();
            comboBox_available_port.Items.Clear();

            comport_info_update();
        }

        private void button_disp_clear_Click(object sender, EventArgs e)
        {
            textBox_RX_data.Clear();
            textBox_TX_data.Clear();
        }

        private void button_tx_send_Click(object sender, EventArgs e)
        {
            opto_serial_write(textBox_TX_data.Text, false);
        }

        /************************************************************************************************
                                                                        Move Singal Abs
        *************************************************************************************************/
        private void btMoveXasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposx.Text, out pos))
            {
                if (pos > X_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed X-axis!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : X Positon Value Something Wrong !" }); return; }

            if (int.TryParse(tbcmdspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed X-axis!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : X Speed Value Something Wrong !" }); return; }

            string senddata = "movesabs0" + ',' + tbcmdposx.Text + ',' + tbcmdspeedx.Text;
            opto_serial_write(senddata, false);
        }

        private void btMoveYasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposy.Text, out pos))
            {
                if (pos > Y_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed Y-axis!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Y Positon Value Something Wrong !" }); return; }

            if (int.TryParse(tbcmdspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed Y-axis!!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Y Speed Value Something Wrong !" }); return; }

            string senddata = "movesabs1" + ',' + tbcmdposy.Text + ',' + tbcmdspeedy.Text;
            opto_serial_write(senddata, false);
        }

        private void btMoveZasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposz.Text, out pos))
            {
                if (pos > Z_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed Z-axis!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Z Positon Value Something Wrong !" }); return; }

            if (int.TryParse(tbcmdspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed Z-axis!!" }); return; }
            }
            else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Z Speed Value Something Wrong !" }); return; }

            string senddata = "movesabs2" + ',' + tbcmdposz.Text + ',' + tbcmdspeedz.Text;
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                        Jog Control +
        *************************************************************************************************/
        private void btJogXinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog X-axis inc ***************************************/
        {
            string senddata = "movejog0,1";

            int speed;
            if (int.TryParse(tbjogspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed X-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedx.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogXinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop0";
            opto_serial_write(senddata, false);
        }

        private void btJogYinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog Y-axis inc ***************************************/
        {
            string senddata = "movejog1,1";

            int speed;
            if (int.TryParse(tbjogspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed  Y-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedy.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogYinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop1";
            opto_serial_write(senddata, false);
        }

        private void btJogZinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog Z-axis inc ***************************************/
        {
            string senddata = "movejog2,1";

            int speed;
            if (int.TryParse(tbjogspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed  Z-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedz.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogZinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop2";
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                        Jog Control -
        *************************************************************************************************/
        private void btJogXdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog X-axis dec ***************************************/
        {
            string senddata = "movejog0,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed  X-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedx.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogXdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop0";
            opto_serial_write(senddata, false);
        }
        private void btJogYdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog Y-axis dec ***************************************/
        {
            string senddata = "movejog1,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed  Y-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedy.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogYdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop1";
            opto_serial_write(senddata, false);
        }

        private void btJogZdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog Z-axis dec ***************************************/
        {
            string senddata = "movejog2,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed  Z-axis!" }); return; }
                if (speed > 0) senddata += ',' + tbjogspeedz.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogZdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop2";
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                Home Position
        *************************************************************************************************/
        private void btHomeX_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg0";
            opto_serial_write(senddata, false);
        }

        private void btHomeY_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg1";
            opto_serial_write(senddata, false);
        }

        private void btHomeZ_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg2";
            opto_serial_write(senddata, false);
        }

        /************************************************************************************************
                                                                XYZ Control
        *************************************************************************************************/
        private void btMoveXYZasix_Click(object sender, EventArgs e)
        {
            int pos, speed;

            /******************************************** X-axis Move Singal Abs *******************************************************/
            if (MotorLive[0] == 1)
            {
                if (int.TryParse(tbcmdposx.Text, out pos))
                {
                    if (pos > X_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed X-axis!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : X Positon Value Something Wrong !" }); return; }

                if (int.TryParse(tbcmdspeedx.Text, out speed))
                {
                    if (speed > MAX_SPEED_X) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed X-axis!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : X Speed Value Something Wrong !" }); return; }
            }
            /******************************************** Y-axis Move Singal Abs *******************************************************/
            if (MotorLive[1] == 1)
            {
                if (int.TryParse(tbcmdposy.Text, out pos))
                {
                    if (pos > Y_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed Y-axis!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Y Positon Value Something Wrong !" }); return; }

                if (int.TryParse(tbcmdspeedy.Text, out speed))
                {
                    if (speed > MAX_SPEED_Y) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed Y-axis!!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Y Speed Value Something Wrong !" }); return; }
            }
            /******************************************** Z-axis Move Singal Abs *******************************************************/
            if (MotorLive[2] == 1)
            {
                if (int.TryParse(tbcmdposz.Text, out pos))
                {
                    if (pos > Z_MAX_DIST) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Postiton Exceed Z-axis!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Z Positon Value Something Wrong !" }); return; }

                if (int.TryParse(tbcmdspeedz.Text, out speed))
                {
                    if (speed > MAX_SPEED_Z) { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Max Speed Exceed Z-axis!!" }); return; }
                }
                else { this.BeginInvoke(new SetTextCallBack(display_data_textbox), new object[] { "Error : Z Speed Value Something Wrong !" }); return; }
            }

            string senddata = "movetabs";

            if (MotorLive[0] == 1) senddata += ",0," + tbcmdposx.Text + ',' + tbcmdspeedx.Text;
            if (MotorLive[1] == 1) senddata += ",1," + tbcmdposy.Text + ',' + tbcmdspeedy.Text;
            if (MotorLive[2] == 1) senddata += ",2," + tbcmdposz.Text + ',' + tbcmdspeedz.Text;

            opto_serial_write(senddata, false);
        }

        private void btHomeXYZ_Click(object sender, EventArgs e)
        {
            string senddata;

            senddata = "moveallorg";
            opto_serial_write(senddata, false);

#if false
            if (MotorLive[0] == 1)
            {
                senddata = "moveorg0";
                opto_serial_write(senddata, false);
                Thread.Sleep(DLEAY_10);
            }

            if (MotorLive[1] == 1)
            {
                senddata = "moveorg1";
                opto_serial_write(senddata, false);
                Thread.Sleep(DLEAY_10);
            }

            if (MotorLive[2] == 1)
            {
                senddata = "moveorg2";
                opto_serial_write(senddata, false);
            }
#endif
        }
    }
}
