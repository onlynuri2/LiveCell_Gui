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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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

        private byte[] MotorLive = { 0, 0, 0 };

        //public static LiveCell livecell;

        public LiveCell()
        {
            InitializeComponent();
            CenterToScreen();
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

                    gbxaxis.BackColor = SystemColors.ButtonFace;
                    gbyaxis.BackColor = SystemColors.ButtonFace;
                    gbzaxis.BackColor = SystemColors.ButtonFace;

                    btMoveXasix.BackColor = SystemColors.ButtonFace;
                    btMoveYasix.BackColor = SystemColors.ButtonFace;
                    btMoveZasix.BackColor = SystemColors.ButtonFace;
                }));
            }
        }
        private void LiveCell_Load(object sender, EventArgs e)
        {
            //Populate the Combobox with SerialPorts on the System
            comport_info_update();

            this.Text = "LiveCell Motion Test - " + "V1.1.0";

            lbxmaxdistance.Text = "(0" + "~" + X_MAX_DIST.ToString() + ")";
            lbxmaxspeed.Text = "(0" + "~" + MAX_SPEED_X.ToString() + ")";

            lbymaxdistance.Text = "(0" + "~" + Y_MAX_DIST.ToString() + ")";
            lbymaxspeed.Text = "(0" + "~" + MAX_SPEED_Y.ToString() + ")";

            lbzmaxdistance.Text = "(0" + "~" + Z_MAX_DIST.ToString() + ")";
            lbzmaxspeed.Text = "(0" + "~" + MAX_SPEED_Z.ToString() + ")";

            lbjogspeedlimitx.Text = "(0" + "~" + MAX_SPEED_X.ToString() + ")";
            lbjogspeedlimity.Text = "(0" + "~" + MAX_SPEED_Y.ToString() + ")";
            lbjogspeedlimitz.Text = "(0" + "~" + MAX_SPEED_Z.ToString() + ")";

            //tbcmdposx.Text = tbcmdposy.Text = tbcmdposz.Text = "0";
            tbcmdspeedx.Text = Convert.ToString(DEFAULT_SPEED_X);
            tbcmdspeedy.Text = Convert.ToString(DEFAULT_SPEED_Y);
            tbcmdspeedz.Text = Convert.ToString(DEFAULT_SPEED_Z);

            tbjogspeedx.Text = Convert.ToString(DEFAULT_SPEED_X);
            tbjogspeedy.Text = Convert.ToString(DEFAULT_SPEED_Y);
            tbjogspeedz.Text = Convert.ToString(DEFAULT_SPEED_Z);
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
        private void display_data_RX_textbox(string str)
        {
            //this.BeginInvoke(new Action(() =>
            this.BeginInvoke((MethodInvoker)(() =>
            {
                if (str.Length > 0)
                    this.textBox_RX_data.AppendText(str + Environment.NewLine);
                else
                    this.textBox_RX_data.Clear();
            }));
        }
        private void display_data_TX_textbox(string str)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(display_data_TX_textbox), str);
                return;
            }

            if (str.Length > 0)
                this.textBox_TX_data.AppendText(str + Environment.NewLine);
            else
                this.textBox_TX_data.Clear();
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
            display_data_RX_textbox(string.Empty);

            string[] port = SerialPort.GetPortNames();
            comboBox_available_port.Items.Clear();

            comport_info_update();
        }

        private void button_disp_clear_Click(object sender, EventArgs e)
        {
            display_data_RX_textbox(string.Empty);
            display_data_TX_textbox(string.Empty);
        }

        private void button_tx_send_Click(object sender, EventArgs e)
        {
            if (opto_serial == null || opto_serial.IsOpen == false) { display_data_RX_textbox("통신연결을 확인해주세요"); return; }

            display_data_RX_textbox(textBox_TX_data.Text);
            opto_serial.Write(textBox_TX_data.Text);
        }

        /************************************************************************************************
                                                                        Move Singal Abs
        *************************************************************************************************/
        private void btMoveXasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposx.Text, out pos))
            {
                if (pos > X_MAX_DIST)
                {
                    display_data_RX_textbox("Error : Max Postiton Exceed X-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Postiton Exceed X-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : X Positon Value Something Wrong !"); return; }

            if (int.TryParse(tbcmdspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed X-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed X-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : X Speed Value Something Wrong !"); return; }

            string senddata = "movesabs" + ",x," + tbcmdposx.Text + ',' + tbcmdspeedx.Text;
            opto_serial_write(senddata, false);
        }

        private void btMoveYasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposy.Text, out pos))
            {
                if (pos > Y_MAX_DIST)
                {
                    display_data_RX_textbox("Error : Max Postiton Exceed Y-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Postiton Exceed Y-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : Y Positon Value Something Wrong !"); return; }

            if (int.TryParse(tbcmdspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed Y-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed Y-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : Y Speed Value Something Wrong !"); return; }

            string senddata = "movesabs" + ",y," + tbcmdposy.Text + ',' + tbcmdspeedy.Text;
            opto_serial_write(senddata, false);
        }

        private void btMoveZasix_Click(object sender, EventArgs e)
        {
            int pos, speed;
            if (int.TryParse(tbcmdposz.Text, out pos))
            {
                if (pos > Z_MAX_DIST)
                {
                    display_data_RX_textbox("Error : Max Postiton Exceed Z-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Postiton Exceed Z-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : Z Positon Value Something Wrong !"); return; }

            if (int.TryParse(tbcmdspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed Z-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed Z-axis!", "Error");
                    return;
                }
            }
            else { display_data_RX_textbox("Error : Z Speed Value Something Wrong !"); return; }

            string senddata = "movesabs" + ",z," + tbcmdposz.Text + ',' + tbcmdspeedz.Text;
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                        Jog Control +
        *************************************************************************************************/
        private void btJogXinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog X-axis inc ***************************************/
        {
            string senddata = "movejog,x,1";

            int speed;
            if (int.TryParse(tbjogspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed X-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed X-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedx.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogXinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,x";
            opto_serial_write(senddata, false);
        }

        private void btJogYinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog Y-axis inc ***************************************/
        {
            string senddata = "movejog,y,1";

            int speed;
            if (int.TryParse(tbjogspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed  Y-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed  Y-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedy.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogYinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,y";
            opto_serial_write(senddata, false);
        }

        private void btJogZinc_MouseDown(object sender, MouseEventArgs e)           /************************************* Jog Z-axis inc ***************************************/
        {
            string senddata = "movejog,z,1";

            int speed;
            if (int.TryParse(tbjogspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed  Z-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed  Z-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedz.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogZinc_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,z";
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                        Jog Control -
        *************************************************************************************************/
        private void btJogXdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog X-axis dec ***************************************/
        {
            string senddata = "movejog,x,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedx.Text, out speed))
            {
                if (speed > MAX_SPEED_X)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed  X-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed  X-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedx.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogXdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,x";
            opto_serial_write(senddata, false);
        }
        private void btJogYdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog Y-axis dec ***************************************/
        {
            string senddata = "movejog,y,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedy.Text, out speed))
            {
                if (speed > MAX_SPEED_Y)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed  Y-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed  Y-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedy.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogYdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,y";
            opto_serial_write(senddata, false);
        }

        private void btJogZdec_MouseDown(object sender, MouseEventArgs e)       /************************************* Jog Z-axis dec ***************************************/
        {
            string senddata = "movejog,z,0";

            int speed = 0;
            if (int.TryParse(tbjogspeedz.Text, out speed))
            {
                if (speed > MAX_SPEED_Z)
                {
                    display_data_RX_textbox("Error : Max Speed Exceed  Z-axis!");
                    MessageBox.Show(Form.ActiveForm, "Max Speed Exceed  Z-axis!", "Error");
                    return;
                }
                if (speed > 0) senddata += ',' + tbjogspeedz.Text;
            }

            opto_serial_write(senddata, false);
        }

        private void btJogZdec_MouseUp(object sender, MouseEventArgs e)
        {
            string senddata = "movestop,z";
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                Home Position
        *************************************************************************************************/
        private void btHomeX_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg,x";
            opto_serial_write(senddata, false);
        }

        private void btHomeY_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg,y";
            opto_serial_write(senddata, false);
        }

        private void btHomeZ_Click(object sender, EventArgs e)
        {
            string senddata = "moveorg,z";
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
                    if (pos > X_MAX_DIST)
                    {
                        display_data_RX_textbox("Error : Max Postiton Exceed X-axis!");
                        MessageBox.Show(Form.ActiveForm, "Max Postiton Exceed X-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : X Positon Value Something Wrong !"); return; }

                if (int.TryParse(tbcmdspeedx.Text, out speed))
                {
                    if (speed > MAX_SPEED_X)
                    {
                        display_data_RX_textbox("Error : Max Speed Exceed X-axis!");
                        MessageBox.Show(Form.ActiveForm, "Max Speed Exceed X-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : X Speed Value Something Wrong !"); return; }
            }
            /******************************************** Y-axis Move Singal Abs *******************************************************/
            if (MotorLive[1] == 1)
            {
                if (int.TryParse(tbcmdposy.Text, out pos))
                {
                    if (pos > Y_MAX_DIST)
                    {
                        display_data_RX_textbox("Error : Max Postiton Exceed Y-axis!");
                        MessageBox.Show(Form.ActiveForm, "Error : Max Postiton Exceed Y-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : Y Positon Value Something Wrong !"); return; }

                if (int.TryParse(tbcmdspeedy.Text, out speed))
                {
                    if (speed > MAX_SPEED_Y)
                    {
                        display_data_RX_textbox("Error : Max Speed Exceed Y-axis!!");
                        MessageBox.Show(Form.ActiveForm, "Max Speed Exceed Y-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : Y Speed Value Something Wrong !"); return; }
            }
            /******************************************** Z-axis Move Singal Abs *******************************************************/
            if (MotorLive[2] == 1)
            {
                if (int.TryParse(tbcmdposz.Text, out pos))
                {
                    if (pos > Z_MAX_DIST)
                    {
                        display_data_RX_textbox("Error : Max Postiton Exceed Z-axis!");
                        MessageBox.Show(Form.ActiveForm, "Error : Max Postiton Exceed Z-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : Z Positon Value Something Wrong !"); return; }

                if (int.TryParse(tbcmdspeedz.Text, out speed))
                {
                    if (speed > MAX_SPEED_Z)
                    {
                        display_data_RX_textbox("Error : Max Speed Exceed Z-axis!!");
                        MessageBox.Show(Form.ActiveForm, "Max Speed Exceed Z-axis!", "Error");
                        return;
                    }
                }
                else { display_data_RX_textbox("Error : Z Speed Value Something Wrong !"); return; }
            }

            string senddata = "movetabs";

            if (MotorLive[0] == 1) senddata += ",x," + tbcmdposx.Text + ',' + tbcmdspeedx.Text;
            if (MotorLive[1] == 1) senddata += ",y," + tbcmdposy.Text + ',' + tbcmdspeedy.Text;
            if (MotorLive[2] == 1) senddata += ",z," + tbcmdposz.Text + ',' + tbcmdspeedz.Text;

            opto_serial_write(senddata, false);
        }

        private void btHomeXYZ_Click(object sender, EventArgs e)
        {
            string senddata;

            senddata = "moveallorg";
            opto_serial_write(senddata, false);
        }

        /************************************************************************************************
                                                                        Number Input Control
        *************************************************************************************************/
        private void tbcmdposx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true; return;
            }
        }

        private void tbcmdspeedx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbjogspeedx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbcmdposy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbcmdspeedy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbjogspeedy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbcmdposz_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbcmdspeedz_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void tbjogspeedz_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자와 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        /************************************************************************************************
                                                                        Offset X
        *************************************************************************************************/
        private void btOffsetXfor_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if(lbcurposx.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetX Value"); return; }

            if (int.TryParse(lbcurposx.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetXfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetX.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetXfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos + OffsetPos;
            if (TargetPos > X_MAX_DIST) TargetPos = X_MAX_DIST;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",x," + MovePos + ',' + tbcmdspeedx.Text;
            opto_serial_write(senddata, false);
        }
        private void btOffsetXback_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if (lbcurposx.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetX Value"); return; }

            if (int.TryParse(lbcurposx.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetXfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetX.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetXfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos - OffsetPos;
            if (TargetPos < 0) TargetPos = 0;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",x," + MovePos + ',' + tbcmdspeedx.Text;
            opto_serial_write(senddata, false);
        }
        /************************************************************************************************
                                                                        Offset Y
        *************************************************************************************************/
        private void btOffsetYfor_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if (lbcurposy.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetY Value"); return; }

            if (int.TryParse(lbcurposy.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetYfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetY.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetYfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos + OffsetPos;
            if (TargetPos > Y_MAX_DIST) TargetPos = Y_MAX_DIST;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",y," + MovePos + ',' + tbcmdspeedy.Text;
            opto_serial_write(senddata, false);
        }

        private void btOffsetYback_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if (lbcurposy.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetY Value"); return; }

            if (int.TryParse(lbcurposy.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetYfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetY.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetYfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos - OffsetPos;
            if (TargetPos < 0) TargetPos = 0;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",y," + MovePos + ',' + tbcmdspeedy.Text;
            opto_serial_write(senddata, false);

        }
        /************************************************************************************************
                                                                        Offset Z
        *************************************************************************************************/
        private void btOffsetZfor_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if (lbcurposz.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetZ Value"); return; }

            if (int.TryParse(lbcurposz.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetZfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetZ.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetZfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos + OffsetPos;
            if (TargetPos > Z_MAX_DIST) TargetPos = Z_MAX_DIST;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",z," + MovePos + ',' + tbcmdspeedz.Text;
            opto_serial_write(senddata, false);
        }

        private void btOffsetZback_Click(object sender, EventArgs e)
        {
            int OffsetPos, CurrentPos, TargetPos;
            string MovePos;

            if (lbcurposz.Text.Length == 0) { display_data_RX_textbox("Error : Input OffsetZ Value"); return; }

            if (int.TryParse(lbcurposz.Text, out CurrentPos) == false) { display_data_RX_textbox("Error : btOffsetZfor CurrentPos Something Wrong !"); return; }
            if (int.TryParse(tbOffsetZ.Text, out OffsetPos) == false) { display_data_RX_textbox("Error : btOffsetZfor OffsetPosSomething Wrong !"); return; }

            TargetPos = CurrentPos - OffsetPos;
            if (TargetPos < 0) TargetPos = 0;
            MovePos = TargetPos.ToString();

            string senddata = "movesabs" + ",z," + MovePos + ',' + tbcmdspeedz.Text;
            opto_serial_write(senddata, false);
        }
    }
}
