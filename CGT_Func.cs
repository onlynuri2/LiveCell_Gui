using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCell_Gui
{
    partial class LiveCell
    {
        CGT_Viewer? CGT_Viewer;
        /************************************************************************************************************************/
        /*                                                                  Auto Capture Start                                                                                    */
        /************************************************************************************************************************/
        private void AutoCaptureStart()
        {
            if (CGT_Viewer == null) { MessageBox.Show(Form.ActiveForm, "Viewer 를 먼저 실행하세요!", " Error!"); return; }

            int curposx, curposy, bOffsetX, bOffsetY;

            if( int.TryParse(lbcurposx.Text, out curposx) && int.TryParse(lbcurposy.Text, out curposy) &&
                int.TryParse(tbOffsetX.Text, out bOffsetX) && int.TryParse(tbOffsetY.Text, out bOffsetY))
            {
                CGT_Viewer.Send_Start_Offset_To_CGTViewer(curposx, curposy, bOffsetX, bOffsetY);
                CGT_Viewer.AutoCapture_Thread_Start();
            }
            else
                MessageBox.Show(Form.ActiveForm, "Offset Position Info Something Wrong", " Error!");
        }
        /************************************************************************************************************************/
        /*                                                                  Auto Capture Stop                                                                                    */
        /************************************************************************************************************************/
        private void AutoCaptureStop()
        {
            if(CGT_Viewer != null) CGT_Viewer.AutoCapture_Thread_Stop();
        }
        /************************************************************************************************************************/
        /*                                                                  Received Data From SubForm                                                                  */
        /************************************************************************************************************************/
        public void Received_Data_From_SubForm(string data)
        {
            if (data.Contains(CAPTURE_START))
            {
                AutoCaptureStart();
            }
            else if (data.Contains(CAPTURE_STOP))
            {
                AutoCaptureStop();
            }
            else if (data.Contains("UpdateUI"))
            {
                //this.Invalidate();  // request a delayed Repaint by the normal MessageLoop system    
                //this.Update();      // forces Repaint of invalidated area 
                this.Refresh();     // Combines Invalidate() and Update()
            }
            display_data_RX_textbox("Received Data_From_SubForm : " + data);
        }

        /************************************************************************************************************************/
        /*                                                                          Key Event                                                                                           */
        /************************************************************************************************************************/
        private void btViewer_Click(object sender, EventArgs e)
        {
            if (opto_serial == null || opto_serial.IsOpen == false) { display_data_RX_textbox("통신연결을 확인해주세요"); return; }

#if LIVECELL
            string senddata = "movetabs";

            if (MotorLive[0] == 1) senddata += ",x," + "300000" + ',' + tbcmdspeedx.Text;
            if (MotorLive[1] == 1) senddata += ",y," + "190000" + ',' + tbcmdspeedy.Text;
            if (MotorLive[2] == 1) senddata += ",z," + "5000" + ',' + tbcmdspeedz.Text;

            _ = opto_serial_write(senddata, false);
#elif CGT
            if (CGT_Viewer != null) { CGT_Viewer.Close(); }

            CGT_Viewer = new CGT_Viewer(this, opto_serial);
            CGT_Viewer.FormClosed += (s, args) => { CGT_Viewer = null; };
            CGT_Viewer.Show();
#endif
        }

        private void AutoCapture_Click(object sender, EventArgs e)
        {
            if (opto_serial == null || opto_serial.IsOpen == false) { display_data_RX_textbox("통신연결을 확인해주세요"); return; }

#if LIVECELL
            string senddata = "movetabs";

            if (MotorLive[0] == 1) senddata += ",x," + "50000" + ',' + tbcmdspeedx.Text;
            if (MotorLive[1] == 1) senddata += ",y," + "190000" + ',' + tbcmdspeedy.Text;
            if (MotorLive[2] == 1) senddata += ",z," + "5000" + ',' + tbcmdspeedz.Text;

            _ = opto_serial_write(senddata, false);
#elif CGT
            if (MotorStatus[0] != 1 || MotorStatus[1] != 1) { display_data_RX_textbox("모터 정지 후 시작해 주세요"); return; }

            //if (MotorStatus[0] != 1 || MotorStatus[1] != 1)
            {
                //Task.Run(() => { System.Windows.Forms.Application.Run(new AutoCloseForm("Info", "모터 정지 후 시작해 주세요.", 2000)); });
                //return;
                //this.BeginInvoke((MethodInvoker)(() => { MessageBox.Show(Form.ActiveForm, "모터 정지 후 시작해 주세요", " Error!"); return; }));
            }

            AutoCaptureStart();
#endif
        }

        private void AutoCapture_Stop_Click(object sender, EventArgs e)
        {
            if (opto_serial == null || opto_serial.IsOpen == false) { display_data_RX_textbox("통신연결을 확인해주세요"); return; }

            AutoCaptureStop();
        }
    }
}
