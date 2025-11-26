namespace LiveCell_Gui
{
    partial class CGT_Viewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            Go = new ToolStripMenuItem();
            Stop = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            orignalToolStripMenuItem = new ToolStripMenuItem();
            zoomToolStripMenuItem = new ToolStripMenuItem();
            menuItem1 = new ToolStripMenuItem();
            bmpToolStripMenuItem = new ToolStripMenuItem();
            pngToolStripMenuItem = new ToolStripMenuItem();
            jpegToolStripMenuItem = new ToolStripMenuItem();
            autoCaptureToolStripMenuItem = new ToolStripMenuItem();
            autoCaptureStopToolStripMenuItem = new ToolStripMenuItem();
            delayToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            StatusLabel1 = new ToolStripStatusLabel();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            menuStrip1.SuspendLayout();
            statusStrip.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { Go, Stop, viewToolStripMenuItem, menuItem1, delayToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(902, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // Go
            // 
            Go.Name = "Go";
            Go.Size = new Size(44, 22);
            Go.Text = "Start";
            Go.Click += Go_Click;
            // 
            // Stop
            // 
            Stop.Name = "Stop";
            Stop.Size = new Size(44, 22);
            Stop.Text = "Stop";
            Stop.Click += Stop_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { orignalToolStripMenuItem, zoomToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(45, 22);
            viewToolStripMenuItem.Text = "View";
            // 
            // orignalToolStripMenuItem
            // 
            orignalToolStripMenuItem.Name = "orignalToolStripMenuItem";
            orignalToolStripMenuItem.Size = new Size(113, 22);
            orignalToolStripMenuItem.Text = "Orignal";
            orignalToolStripMenuItem.Click += OrignalViewStripMenuItem_Click;
            // 
            // zoomToolStripMenuItem
            // 
            zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            zoomToolStripMenuItem.Size = new Size(113, 22);
            zoomToolStripMenuItem.Text = "Zoom";
            zoomToolStripMenuItem.Click += ZoomViewStripMenuItem_Click;
            // 
            // menuItem1
            // 
            menuItem1.DropDownItems.AddRange(new ToolStripItem[] { bmpToolStripMenuItem, pngToolStripMenuItem, jpegToolStripMenuItem, autoCaptureToolStripMenuItem, autoCaptureStopToolStripMenuItem });
            menuItem1.Name = "menuItem1";
            menuItem1.Size = new Size(61, 22);
            menuItem1.Text = "Capture";
            // 
            // bmpToolStripMenuItem
            // 
            bmpToolStripMenuItem.Name = "bmpToolStripMenuItem";
            bmpToolStripMenuItem.Size = new Size(180, 22);
            bmpToolStripMenuItem.Text = ".bmp";
            bmpToolStripMenuItem.Click += Capture_Click;
            // 
            // pngToolStripMenuItem
            // 
            pngToolStripMenuItem.Name = "pngToolStripMenuItem";
            pngToolStripMenuItem.Size = new Size(180, 22);
            pngToolStripMenuItem.Text = ".png";
            pngToolStripMenuItem.Click += Capture_Click;
            // 
            // jpegToolStripMenuItem
            // 
            jpegToolStripMenuItem.Name = "jpegToolStripMenuItem";
            jpegToolStripMenuItem.Size = new Size(180, 22);
            jpegToolStripMenuItem.Text = ".jpeg";
            jpegToolStripMenuItem.Click += Capture_Click;
            // 
            // autoCaptureToolStripMenuItem
            // 
            autoCaptureToolStripMenuItem.Name = "autoCaptureToolStripMenuItem";
            autoCaptureToolStripMenuItem.Size = new Size(180, 22);
            autoCaptureToolStripMenuItem.Text = "Auto Capture Start";
            autoCaptureToolStripMenuItem.Click += AutoCapture_Start_StripMenuItem_Click;
            // 
            // autoCaptureStopToolStripMenuItem
            // 
            autoCaptureStopToolStripMenuItem.Name = "autoCaptureStopToolStripMenuItem";
            autoCaptureStopToolStripMenuItem.Size = new Size(180, 22);
            autoCaptureStopToolStripMenuItem.Text = "Auto Capture Stop";
            autoCaptureStopToolStripMenuItem.Click += AutoCapture_Stop_StripMenuItem_Click;
            // 
            // delayToolStripMenuItem
            // 
            delayToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9 });
            delayToolStripMenuItem.Name = "delayToolStripMenuItem";
            delayToolStripMenuItem.Size = new Size(73, 22);
            delayToolStripMenuItem.Text = "Delay(ms)";
            delayToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(102, 22);
            toolStripMenuItem3.Text = "100";
            toolStripMenuItem3.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(102, 22);
            toolStripMenuItem4.Text = "300";
            toolStripMenuItem4.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(102, 22);
            toolStripMenuItem5.Text = "500";
            toolStripMenuItem5.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(102, 22);
            toolStripMenuItem6.Text = "700";
            toolStripMenuItem6.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(102, 22);
            toolStripMenuItem7.Text = "1000";
            toolStripMenuItem7.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(102, 22);
            toolStripMenuItem8.Text = "1500";
            toolStripMenuItem8.Click += DelayStripMenuItem_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(102, 22);
            toolStripMenuItem9.Text = "2000";
            toolStripMenuItem9.Click += DelayStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.AutoSize = false;
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { StatusLabel1 });
            statusStrip.Location = new Point(0, 924);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 10, 0);
            statusStrip.Size = new Size(902, 22);
            statusStrip.TabIndex = 2;
            // 
            // StatusLabel1
            // 
            StatusLabel1.Name = "StatusLabel1";
            StatusLabel1.Size = new Size(121, 17);
            StatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(1, 23);
            panel1.Name = "panel1";
            panel1.Size = new Size(900, 900);
            panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(900, 900);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // CGT_Viewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 946);
            Controls.Add(panel1);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "CGT_Viewer";
            Text = "CGT Viewer";
            FormClosing += CGT_Viewer_FormClosing;
            Load += CGT_Viewer_Load;
            Paint += CGT_Viewer_Paint;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem Go;
        private ToolStripMenuItem Stop;
        private ToolStripMenuItem menuItem1;
        private ToolStripMenuItem bmpToolStripMenuItem;
        private ToolStripMenuItem pngToolStripMenuItem;
        private ToolStripMenuItem jpegToolStripMenuItem;
        private StatusStrip statusStrip;
        private Panel panel1;
        private PictureBox pictureBox1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem orignalToolStripMenuItem;
        private ToolStripMenuItem zoomToolStripMenuItem;
        private ToolStripMenuItem autoCaptureToolStripMenuItem;
        private ToolStripMenuItem autoCaptureStopToolStripMenuItem;
        private ToolStripStatusLabel StatusLabel1;
        private ToolStripMenuItem delayToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
    }
}