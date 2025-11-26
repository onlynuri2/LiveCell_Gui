using Euresys.MultiCam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace LiveCell_Gui
{
    public partial class CGT_Viewer : Form
    {
        const string CAPTURE_START = "CaptureStart";
        const string CAPTURE_STOP = "CaptureStop";

        private SerialPort opto_serial_sub;

        // Creation of an event for asynchronous call to paint function
        public delegate void PaintDelegate(Graphics g);
        public delegate void UpdateStatusBarDelegate(String text);

        // The object that will contain the acquired image
        private Bitmap? image = null;

        // The object that will contain the palette information for the bitmap
        private ColorPalette? imgpal;

        // The Mutex object that will protect image objects during processing
        private static Mutex imageMutex = new Mutex();

        // The MultiCam object that controls the acquisition
        UInt32 channel;

        // The MultiCam object that contains the acquired buffer
        private UInt32 currentSurface;

        MC.CALLBACK? multiCamCallback;

        private LiveCell mainform;

        /************************************************************************************************************************/
        /*                                                                  CGT Viewer Create                                                                                   */
        /************************************************************************************************************************/
        public CGT_Viewer(LiveCell main, SerialPort serial)
        {
            InitializeComponent();
            //CenterToScreen();

            this.Controls.SetChildIndex(statusStrip, 0); // StatusStrip을 최상단(Z-order 위로)

            mainform = main;
            opto_serial_sub = serial;
        }
        /************************************************************************************************************************/
        /*                                                                  CGT Viewer Load                                                                                      */
        /************************************************************************************************************************/
        private void CGT_Viewer_Load(object sender, EventArgs e)
        {
            try
            {
                MC.OpenDriver();

                //MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

                MC.Create("CHANNEL", out channel);
                MC.SetParam(channel, "DriverIndex", 0);

                // For all Grablink boards except Grablink DualBase
                MC.SetParam(channel, "Connector", "M");

                // Choose the CAM file
                //MC.SetParam(channel, "CamFile", "1000m_P50RG");
                MC.SetParam(channel, "CamFile", "STC-GPB250BPCL_5120x5120_FULL_8T8_25FPS_SC");
                // Choose the camera expose duration
                MC.SetParam(channel, "Expose_us", 20000);
                // Choose the pixel color format
                MC.SetParam(channel, "ColorFormat", "Y8");

                //Set the acquisition mode to Snapshot
                MC.SetParam(channel, "AcquisitionMode", "SNAPSHOT");
                // Choose the way the first acquisition is triggered
                MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                // Choose the triggering mode for subsequent acquisitions
                MC.SetParam(channel, "NextTrigMode", "REPEAT");
                // Choose the number of images to acquire
                MC.SetParam(channel, "SeqLength_Fr", MC.INDETERMINATE);

                // Register the callback function
                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(channel, multiCamCallback, channel);

                // Enable the signals corresponding to the callback functions
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

                // Prepare the channel in order to minimize the acquisition sequence startup latency
                MC.SetParam(channel, "ChannelState", "READY");

                Go_Click(sender, e);
                ZoomViewStripMenuItem_Click(sender, e);
                SetDelayMenuChecked();
            }
            catch (Euresys.MultiCamException exc)
            {
                // An exception has occurred in the try {...} block. 
                // Retrieve its description and display it in a message box.
                MessageBox.Show(exc.Message, "MultiCam Exception");
                Close();
            }

            this.Location = new Point(0, 0);
        }
        /************************************************************************************************************************/
        /*                                                                CGT Viewer FormClosing                                                                            */
        /************************************************************************************************************************/
        private void CGT_Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (channel != 0)
                {
                    // 콜백 먼저 해제 (중요)
                    MC.RegisterCallback(channel, null, 0);

                    // 채널 중지
                    try { MC.SetParam(channel, "ChannelState", "IDLE"); } catch { }

                    // 채널 삭제
                    try { MC.Delete(channel); } catch { }

                    channel = 0;

                    // 드라이버 닫기
                    try { MC.CloseDriver(); } catch { }

                    if (Is_Thread_run) AutoCapture_Thread_Stop();
                }
            }
            catch (Euresys.MultiCamException exc)
            {
                MessageBox.Show(exc.Message, "MultiCam Exception");
            }
        }
        /************************************************************************************************************************/
        /*                                                            Auto Capture Thread Start Stop                                                                     */
        /************************************************************************************************************************/

        string CaptureStartTime = DateTime.Now.ToString("yyMMdd_HHmmss");

        bool Is_Thread_run = false;
        int start_x, start_y, offset_x, offset_y;

        public Task AutoCapture_Thread_Start()
        {
            CaptureStartTime = DateTime.Now.ToString("yyMMdd_HHmmss");

            Is_Thread_run = true;
            _ = Task.Run(() => AutoCapture_Thread());
            return Task.CompletedTask;

        }
        public void AutoCapture_Thread_Stop()
        {
            Is_Thread_run = false;
        }
        //private async Task AutoCapture_Thread()
		private async Task AutoCapture_Thread()
        {
            if(uiTimer == null || uiTimer.Enabled == false)
            {
                UIHelper.ShowAutoCloseMsg("Info", "카메라를 활성화 해주세요!", 1200);
                return;
            }

            var path = GenerateSerpentinePath(start_x, start_y, offset_x, offset_y, 6, 6);

            Byte[] Sequence_Idx = { 1, 2, 3, 4, 5, 6,
                                    12, 11, 10, 9, 8, 7,
                                    13, 14, 15, 16, 17, 18,
                                    24, 23, 22, 21, 20, 19,
                                    25, 26, 27, 28, 29, 30,
                                    36, 35, 34, 33, 32, 31
                                    };

            //System.Windows.Forms.Application.Run(new AutoCloseForm("Info", "Capture Start", 1000));
            UIHelper.ShowAutoCloseMsg("Info", "Capture Start", 1000);

            await Task.Delay(1000);

            for (int idx = 0; idx < path.Count; idx++)
            {
                if (!Is_Thread_run) return;

                string senddata = "movetabs";

                int xpos = start_x;
                senddata += ",x," + path[idx].X.ToString();// + ",70000";
                senddata += ",y," + path[idx].Y.ToString();// + ",70000";

                opto_serial_sub_write(senddata);

                for (int cnt = 0; cnt <= 30; cnt++)
                {
                    if (path[idx].X == Curr_Pos_X && path[idx].Y == Curr_Pos_Y)
                    {
                        await Task.Delay(CaptureDelay);

                        AutoCaptureImageSave(CaptureStartTime, Sequence_Idx[idx]);
                        break;
                    }

                    if (cnt >= 30) { UIHelper.ShowAutoCloseMsg("Error", "Auto Capture 중 error", 1000); return; }

                    await Task.Delay(100);
                }

                if (idx == path.Count - 1) opto_serial_sub_write("moveallorg");
            }

            Is_Thread_run = false;
        }
        private void AutoCaptureImageSave(string dirname, int idx)
        {
            try
            {
                imageMutex.WaitOne();

                if (image != null)
                {
                    if (!Directory.Exists(dirname)) Directory.CreateDirectory(dirname);

                    string fileName = dirname + "_" + idx.ToString("D2") + ".bmp";
                    string filePath = Path.Combine(dirname, fileName);

                    image.Save(filePath, ImageFormat.Bmp);
                }
                else MessageBox.Show("저장할 이미지가 없습니다.");
            }
            catch (Exception ex) { MessageBox.Show("자동 저장 중 오류: " + ex.Message); }
            finally { imageMutex.ReleaseMutex(); }
        }
        /************************************************************************************************************************/
        /*                                                            Recived Start XY, Offset XY                                                                             */
        /************************************************************************************************************************/
        public void Send_Start_Offset_To_CGTViewer(int _start_x, int _start_y, int _offset_x, int _offset_y)
        {
            start_x = _start_x; start_y = _start_y; offset_x = _offset_x; offset_y = _offset_y;
            Send_Current_Position_To_CGTViewer(start_x, start_y, 0);
        }
        /************************************************************************************************************************/
        /*                                                                Recived XYZ Position                                                                                  */
        /************************************************************************************************************************/
        int Curr_Pos_X, Curr_Pos_Y, Curr_Pos_Z;
        public void Send_Current_Position_To_CGTViewer(int x, int y, int z)
        {
            Curr_Pos_X = x; Curr_Pos_Y = y; Curr_Pos_Z = z;
        }
        /************************************************************************************************************************/
        /*                                                               Camera Image Control                                                                                */
        /************************************************************************************************************************/
        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;
                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;
                default:
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            currentSurface = signalInfo.SignalInfo;

            try
            {
                // Update the image with the acquired image buffer data 
                Int32 width, height, bufferPitch;
                IntPtr bufferAddress;
                MC.GetParam(currentChannel, "ImageSizeX", out width);
                MC.GetParam(currentChannel, "ImageSizeY", out height);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                MC.GetParam(currentSurface, "SurfaceAddr", out bufferAddress);

                Bitmap? oldImage = null;

                try
                {
                    imageMutex.WaitOne();

                    oldImage = image;

                    image = new Bitmap(width, height, bufferPitch, PixelFormat.Format8bppIndexed, bufferAddress);

                    imgpal = image.Palette;

                    // Build bitmap palette Y8
                    for (uint i = 0; i < 256; i++)
                    {
                        imgpal.Entries[i] = Color.FromArgb(
                        (byte)0xFF,
                        (byte)i,
                        (byte)i,
                        (byte)i);
                    }

                    image.Palette = imgpal;

                    /* Insert image analysis and processing code here */
                }
                finally
                {
                    imageMutex.ReleaseMutex();
                    oldImage?.Dispose();
                }

                // Retrieve the frame rate
                Double frameRate_Hz;
                MC.GetParam(channel, "PerSecond_Fr", out frameRate_Hz);

                // Retrieve the channel state
                String channelState;
                MC.GetParam(channel, "ChannelState", out channelState);

                // Display frame rate and channel state
                UpdateStatusBar(String.Format("Frame Rate: {0:f2}, Channel State: {1}  ", frameRate_Hz, channelState));

                // Display the new image
                //this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
                RedrawClone();
            }
            catch (Euresys.MultiCamException exc)
            {
                MessageBox.Show(exc.Message, "MultiCam Exception");
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, "System Exception");
            }
        }

        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            try
            {
                // Display frame rate and channel state
                UpdateStatusBar(String.Format("Acquisition Failure, Channel State: IDLE"));
                this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, "System Exception");
            }
        }
        /************************************************************************************************************************/
        /*                                                      Viewer Screen Update Image Control                                                                  */
        /************************************************************************************************************************/
        private void UpdateStatusBar(String text)
        {
            this.BeginInvoke((MethodInvoker)(() =>
            {
                StatusLabel1.Text = text + $"X: {MOUSE_X}, Y: {MOUSE_Y}";
            }));
        }
        private readonly object frameLock = new object();
        private Bitmap? latestFrame = null;
        private System.Windows.Forms.Timer? uiTimer;

        private void StartUiTimer()
        {
            uiTimer = new System.Windows.Forms.Timer();
            uiTimer.Interval = 80; // 약 33fps (30ms마다 갱신)
            uiTimer.Tick += (s, e) => RedrawFrame();
            uiTimer.Start();
        }
        private void StopUiTimer()
        {
            if (uiTimer != null)
            {
                uiTimer?.Stop();
                uiTimer?.Dispose();
                uiTimer = null;
            }

            this.BeginInvoke((MethodInvoker)(() =>
            {
                image?.Dispose();
                image = null;

                latestFrame?.Dispose();
                latestFrame = null;

                pictureBox1.Image?.Dispose();
                pictureBox1.Image = null;
            }));
        }
        private void RedrawFrame()
        {
            Bitmap? frameCopy = null;

            lock (frameLock)
            {
                if (latestFrame == null) return;

                // 새 프레임 참조 복제
                frameCopy = (Bitmap)latestFrame.Clone();
            }

            if (frameCopy != null)
            {
                var old = pictureBox1.Image;
                pictureBox1.Image = frameCopy;
                old?.Dispose();
            }
        }
        private void RedrawClone()
        {
            try
            {
                imageMutex.WaitOne();

                if (image != null)
                {
                    // 프레임 복제만 수행 (UI 접근 없음)
                    var newFrame = (Bitmap)image.Clone();

                    // 최신 프레임 교체 (이전 프레임 Dispose)
                    lock (frameLock)
                    {
                        latestFrame?.Dispose();
                        latestFrame = newFrame;
                    }
                }
            }
            finally
            {
                imageMutex.ReleaseMutex();
            }
        }
        void Redraw(Graphics g)
        {
            try
            {
                imageMutex.WaitOne();
                Task.Run(() =>
                {
                    if (image != null)
                    {
                        Bitmap frameCopy = (Bitmap)image.Clone();

                        // UI 스레드에서 안전하게 적용
                        this.BeginInvoke((MethodInvoker)(() =>
                        {
                            pictureBox1.Image?.Dispose();
                            pictureBox1.Image = frameCopy;
                        }));
                    }
                });
            }
            catch (System.Exception exc) { MessageBox.Show(exc.Message, "System Exception"); }
            finally { imageMutex.ReleaseMutex(); }
        }
        private void CGT_Viewer_Paint(object sender, PaintEventArgs e)
        {
            Redraw(e.Graphics);
        }
        /************************************************************************************************************************/
        /*                                                               Menu Button Control                                                                                  */
        /************************************************************************************************************************/
        private void Go_Click(object sender, System.EventArgs e)
        {
            // Start an acquisition sequence by activating the channel
            String channelState;
            MC.GetParam(channel, "ChannelState", out channelState);
            if (channelState != "ACTIVE")
                MC.SetParam(channel, "ChannelState", "ACTIVE");
            Refresh();

            StartUiTimer();
        }

        private void Stop_Click(object sender, System.EventArgs e)
        {
            // Stop an acquisition sequence by deactivating the channel
            if (channel != 0)
                MC.SetParam(channel, "ChannelState", "IDLE");
            UpdateStatusBar(String.Format("Frame Rate: {0:f2}, Channel State: IDLE", 0));

            StopUiTimer();
        }
        private void Capture_Click(object sender, EventArgs e)
        {
            ImageFormat format = ImageFormat.Bmp;

            // 2. sender 객체를 ToolStripMenuItem으로 형변환
            ToolStripMenuItem? clickedItem = sender as ToolStripMenuItem;
            // 3. null 체크 (형변환 실패 방지)
            if (clickedItem != null)
            {
                // 4. clickedItem의 Text 속성으로 구분
                switch (clickedItem.Text)
                {
                    case ".bmp": format = ImageFormat.Bmp; break;
                    case ".png": format = ImageFormat.Png; break;
                    case ".jpeg": format = ImageFormat.Jpeg; break;
                }
            }
            try
            {
                imageMutex.WaitOne();

                if (image != null && clickedItem != null)
                {
                    string fileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss_fff")}{clickedItem.Text}";
                    image.Save(fileName, format);
                    MessageBox.Show("Saved: " + fileName);
                }
                else MessageBox.Show("저장할 이미지가 없습니다.");
            }
            catch (Exception ex) { MessageBox.Show("저장 중 오류: " + ex.Message + "item :"); }
            finally { imageMutex.ReleaseMutex(); }
        }
        private void OrignalViewStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(5120, 5120);
            pictureBox1.Dock = DockStyle.None;              // PictureBox는 작게 유지
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal; // 비율 유지 축소 표시
        }

        private void ZoomViewStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = new Size(900, 900);
            pictureBox1.Dock = DockStyle.Fill;              // PictureBox는 작게 유지
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // 비율 유지 축소 표시
        }

        private void AutoCapture_Start_StripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Is_Thread_run)
            {
                mainform.Received_Data_From_SubForm(CAPTURE_START);
            }
            else
            {
                Task.Run(() => { System.Windows.Forms.Application.Run(new AutoCloseForm("Info", "이미 실행중입니다.", 2000)); });
            }
        }
        private void AutoCapture_Stop_StripMenuItem_Click(object sender, EventArgs e)
        {
            mainform.Received_Data_From_SubForm(CAPTURE_STOP);
        }
        int CaptureDelay = 300;
        private void DelayStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem? clickedItem = sender as ToolStripMenuItem;
            // 3. null 체크 (형변환 실패 방지)
            if (clickedItem != null)
            {
                if(int.TryParse(clickedItem.Text, out CaptureDelay))
                {
                    foreach (ToolStripMenuItem item in delayToolStripMenuItem.DropDownItems) item.Checked = false;

                    clickedItem.Checked = true;

                    Task.Run(() => { System.Windows.Forms.Application.Run(new AutoCloseForm("Info", "Delay Time Change : " + clickedItem.Text, 2000)); });
                }
                else
                    Task.Run(() => { System.Windows.Forms.Application.Run(new AutoCloseForm("Info", "Delay Time Change Failed", 2000)); });
            }
        }
        private void SetDelayMenuChecked()
        {
            foreach (ToolStripMenuItem item in delayToolStripMenuItem.DropDownItems)
            {
                if (int.TryParse(item.Text, out int delay))
                {
                    if(CaptureDelay == delay) item.Checked = true;
                }
                else
                    item.Checked = false;
            }
        }
        /************************************************************************************************************************/
        /*                                                               Mouse Move XY Coord                                                                                */
        /************************************************************************************************************************/
        private int MOUSE_X, MOUSE_Y;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null) return;

            int imgWidth = pictureBox1.Image.Width;   // 원본 = 5120
            int imgHeight = pictureBox1.Image.Height;

            int pbWidth = pictureBox1.ClientSize.Width;   // PictureBox 크기 = 900
            int pbHeight = pictureBox1.ClientSize.Height;

            // Zoom 스케일 계산
            double scale = Math.Min((double)pbWidth / imgWidth, (double)pbHeight / imgHeight);

            int displayedWidth = (int)(imgWidth * scale);
            int displayedHeight = (int)(imgHeight * scale);

            // 마우스 좌표를 원본 좌표로 변환
            int realX = (int)(e.X / scale);
            int realY = (int)(e.Y / scale);

            MOUSE_X = realX;
            MOUSE_Y = realY;
        }
        /************************************************************************************************************************/
        /*                                                                  opto serial sub write                                                                                */
        /************************************************************************************************************************/
        private void opto_serial_sub_write(string str)
        {
            if (opto_serial_sub == null) { return; }

            if (opto_serial_sub.IsOpen == false) { return; }

            try { opto_serial_sub.Write('#' + str + '*'); }
            catch (IOException ex) { UIHelper.ShowAutoCloseMsg("Info", "opto_serial_sub_write error" + ex.Message, 2000); }
            catch (InvalidOperationException ex) { UIHelper.ShowAutoCloseMsg("Info", "opto_serial_sub_write error2" + ex.Message, 2000); }
        }
        /************************************************************************************************************************/
        /*                                                                  X, Y Position Search                                                                                  */
        /************************************************************************************************************************/
        public static List<Point> GenerateSerpentinePath(
            int startX, int startY,
            int xOffset, int yOffset,
            int cols, int rows)
        {
            var path = new List<Point>();

            for (int row = 0; row < rows; row++)
            {
                // 현재 y 좌표
                int y = startY + row * yOffset;

                if (row % 2 == 0) // 짝수 줄: 왼→오른쪽
                {
                    for (int col = 0; col < cols; col++)
                    {
                        int x = startX + col * xOffset;
                        path.Add(new Point(x, y));
                    }
                }
                else // 홀수 줄: 오른쪽→왼쪽
                {
                    for (int col = cols - 1; col >= 0; col--)
                    {
                        int x = startX + col * xOffset;
                        path.Add(new Point(x, y));
                    }
                }
            }

            return path;
        }
    }
}
