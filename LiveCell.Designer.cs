namespace LiveCell_Gui
{
    partial class LiveCell
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveCell));
            btconnection = new Button();
            groupBox_comport = new GroupBox();
            comboBox_available_port = new ComboBox();
            port_label = new Label();
            btdisconnection = new Button();
            textBox_RX_data = new TextBox();
            gbTransfer = new GroupBox();
            button_disp_clear = new Button();
            textBox_TX_data = new TextBox();
            button_tx_send = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            gbmotionctrl = new GroupBox();
            gbyaxis = new GroupBox();
            label3 = new Label();
            tbjogspeedy = new TextBox();
            lbjogspeedy = new Label();
            lbymaxspeed = new Label();
            lbymaxdistance = new Label();
            gbjogctrly = new GroupBox();
            btJogYinc = new Button();
            btJogYdec = new Button();
            btMoveYasix = new Button();
            lbcurposy = new Label();
            lbcurposyaxis = new Label();
            tbcmdspeedy = new TextBox();
            tbcmdposy = new TextBox();
            lbcmdspeedy = new Label();
            lbcmdposy = new Label();
            gbxaxis = new GroupBox();
            label1 = new Label();
            tbjogspeedx = new TextBox();
            lbjogspeedx = new Label();
            lbxmaxspeed = new Label();
            lbxmaxdistance = new Label();
            gbjogctrlx = new GroupBox();
            btJogXinc = new Button();
            btJogXdec = new Button();
            btMoveXasix = new Button();
            lbcurposx = new Label();
            lbcurposxaxis = new Label();
            tbcmdspeedx = new TextBox();
            tbcmdposx = new TextBox();
            lbcmdspeedx = new Label();
            lbcmdposx = new Label();
            groupBox_comport.SuspendLayout();
            gbTransfer.SuspendLayout();
            gbmotionctrl.SuspendLayout();
            gbyaxis.SuspendLayout();
            gbjogctrly.SuspendLayout();
            gbxaxis.SuspendLayout();
            gbjogctrlx.SuspendLayout();
            SuspendLayout();
            // 
            // btconnection
            // 
            btconnection.Font = new Font("맑은 고딕", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btconnection.Location = new Point(14, 110);
            btconnection.Name = "btconnection";
            btconnection.Size = new Size(125, 56);
            btconnection.TabIndex = 0;
            btconnection.Text = "연결";
            btconnection.UseVisualStyleBackColor = true;
            btconnection.Click += btconnection_Click;
            // 
            // groupBox_comport
            // 
            groupBox_comport.BackColor = SystemColors.ButtonFace;
            groupBox_comport.Controls.Add(comboBox_available_port);
            groupBox_comport.Controls.Add(port_label);
            groupBox_comport.Controls.Add(btdisconnection);
            groupBox_comport.Controls.Add(btconnection);
            groupBox_comport.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            groupBox_comport.Location = new Point(4, 4);
            groupBox_comport.Name = "groupBox_comport";
            groupBox_comport.Size = new Size(289, 179);
            groupBox_comport.TabIndex = 1;
            groupBox_comport.TabStop = false;
            groupBox_comport.Text = "통신설정";
            // 
            // comboBox_available_port
            // 
            comboBox_available_port.FormattingEnabled = true;
            comboBox_available_port.Location = new Point(95, 55);
            comboBox_available_port.Name = "comboBox_available_port";
            comboBox_available_port.Size = new Size(175, 40);
            comboBox_available_port.TabIndex = 3;
            comboBox_available_port.SelectedIndexChanged += comboBox_available_port_SelectedIndexChanged;
            comboBox_available_port.Click += comboBox_available_port_Click;
            // 
            // port_label
            // 
            port_label.AutoSize = true;
            port_label.Font = new Font("맑은 고딕", 18F, FontStyle.Regular, GraphicsUnit.Point, 129);
            port_label.Location = new Point(5, 52);
            port_label.Name = "port_label";
            port_label.Size = new Size(92, 48);
            port_label.TabIndex = 2;
            port_label.Text = "포트";
            // 
            // btdisconnection
            // 
            btdisconnection.Font = new Font("맑은 고딕", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btdisconnection.Location = new Point(145, 110);
            btdisconnection.Name = "btdisconnection";
            btdisconnection.Size = new Size(130, 56);
            btdisconnection.TabIndex = 1;
            btdisconnection.Text = "해제";
            btdisconnection.UseVisualStyleBackColor = true;
            btdisconnection.Click += btdisconnection_Click;
            // 
            // textBox_RX_data
            // 
            textBox_RX_data.Location = new Point(6, 68);
            textBox_RX_data.Multiline = true;
            textBox_RX_data.Name = "textBox_RX_data";
            textBox_RX_data.ScrollBars = ScrollBars.Vertical;
            textBox_RX_data.Size = new Size(758, 111);
            textBox_RX_data.TabIndex = 2;
            // 
            // gbTransfer
            // 
            gbTransfer.Controls.Add(button_disp_clear);
            gbTransfer.Controls.Add(textBox_RX_data);
            gbTransfer.Controls.Add(textBox_TX_data);
            gbTransfer.Controls.Add(button_tx_send);
            gbTransfer.Location = new Point(299, 4);
            gbTransfer.Name = "gbTransfer";
            gbTransfer.Size = new Size(770, 179);
            gbTransfer.TabIndex = 3;
            gbTransfer.TabStop = false;
            gbTransfer.Text = "송신";
            // 
            // button_disp_clear
            // 
            button_disp_clear.Location = new Point(661, 27);
            button_disp_clear.Name = "button_disp_clear";
            button_disp_clear.Size = new Size(103, 34);
            button_disp_clear.TabIndex = 4;
            button_disp_clear.Text = "Clear";
            button_disp_clear.UseVisualStyleBackColor = true;
            button_disp_clear.Click += button_disp_clear_Click;
            // 
            // textBox_TX_data
            // 
            textBox_TX_data.Location = new Point(123, 30);
            textBox_TX_data.Name = "textBox_TX_data";
            textBox_TX_data.Size = new Size(531, 31);
            textBox_TX_data.TabIndex = 4;
            // 
            // button_tx_send
            // 
            button_tx_send.Location = new Point(6, 29);
            button_tx_send.Name = "button_tx_send";
            button_tx_send.Size = new Size(112, 34);
            button_tx_send.TabIndex = 4;
            button_tx_send.Text = "Send";
            button_tx_send.UseVisualStyleBackColor = true;
            button_tx_send.Click += button_tx_send_Click;
            // 
            // gbmotionctrl
            // 
            gbmotionctrl.BackColor = Color.GhostWhite;
            gbmotionctrl.Controls.Add(gbyaxis);
            gbmotionctrl.Controls.Add(gbxaxis);
            gbmotionctrl.Font = new Font("맑은 고딕", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbmotionctrl.Location = new Point(8, 194);
            gbmotionctrl.Name = "gbmotionctrl";
            gbmotionctrl.Size = new Size(1055, 756);
            gbmotionctrl.TabIndex = 4;
            gbmotionctrl.TabStop = false;
            gbmotionctrl.Text = "Motion Control";
            // 
            // gbyaxis
            // 
            gbyaxis.BackColor = Color.Lavender;
            gbyaxis.Controls.Add(label3);
            gbyaxis.Controls.Add(tbjogspeedy);
            gbyaxis.Controls.Add(lbjogspeedy);
            gbyaxis.Controls.Add(lbymaxspeed);
            gbyaxis.Controls.Add(lbymaxdistance);
            gbyaxis.Controls.Add(gbjogctrly);
            gbyaxis.Controls.Add(btMoveYasix);
            gbyaxis.Controls.Add(lbcurposy);
            gbyaxis.Controls.Add(lbcurposyaxis);
            gbyaxis.Controls.Add(tbcmdspeedy);
            gbyaxis.Controls.Add(tbcmdposy);
            gbyaxis.Controls.Add(lbcmdspeedy);
            gbyaxis.Controls.Add(lbcmdposy);
            gbyaxis.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbyaxis.Location = new Point(391, 53);
            gbyaxis.Name = "gbyaxis";
            gbyaxis.Size = new Size(289, 478);
            gbyaxis.TabIndex = 13;
            gbyaxis.TabStop = false;
            gbyaxis.Text = "Y-axis";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label3.Location = new Point(16, 396);
            label3.Name = "label3";
            label3.Size = new Size(105, 25);
            label3.TabIndex = 12;
            label3.Text = "(0~300000)";
            // 
            // tbjogspeedy
            // 
            tbjogspeedy.Location = new Point(123, 373);
            tbjogspeedy.MaxLength = 6;
            tbjogspeedy.Name = "tbjogspeedy";
            tbjogspeedy.Size = new Size(147, 45);
            tbjogspeedy.TabIndex = 11;
            tbjogspeedy.Text = "30000";
            tbjogspeedy.TextAlign = HorizontalAlignment.Right;
            // 
            // lbjogspeedy
            // 
            lbjogspeedy.AutoSize = true;
            lbjogspeedy.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedy.Location = new Point(55, 366);
            lbjogspeedy.Name = "lbjogspeedy";
            lbjogspeedy.Size = new Size(62, 32);
            lbjogspeedy.TabIndex = 10;
            lbjogspeedy.Text = "속도";
            // 
            // lbymaxspeed
            // 
            lbymaxspeed.AutoSize = true;
            lbymaxspeed.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbymaxspeed.Location = new Point(25, 195);
            lbymaxspeed.Name = "lbymaxspeed";
            lbymaxspeed.Size = new Size(105, 25);
            lbymaxspeed.TabIndex = 9;
            lbymaxspeed.Text = "(0~100000)";
            // 
            // lbymaxdistance
            // 
            lbymaxdistance.AutoSize = true;
            lbymaxdistance.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbymaxdistance.Location = new Point(15, 128);
            lbymaxdistance.Name = "lbymaxdistance";
            lbymaxdistance.Size = new Size(105, 25);
            lbymaxdistance.TabIndex = 8;
            lbymaxdistance.Text = "(0~200000)";
            // 
            // gbjogctrly
            // 
            gbjogctrly.Controls.Add(btJogYinc);
            gbjogctrly.Controls.Add(btJogYdec);
            gbjogctrly.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbjogctrly.Location = new Point(13, 328);
            gbjogctrly.Name = "gbjogctrly";
            gbjogctrly.Size = new Size(260, 144);
            gbjogctrly.TabIndex = 7;
            gbjogctrly.TabStop = false;
            gbjogctrly.Text = "Jog Mode";
            // 
            // btJogYinc
            // 
            btJogYinc.Location = new Point(155, 100);
            btJogYinc.Name = "btJogYinc";
            btJogYinc.Size = new Size(102, 41);
            btJogYinc.TabIndex = 1;
            btJogYinc.Text = "Jog(+)";
            btJogYinc.UseVisualStyleBackColor = true;
            btJogYinc.MouseDown += btJogYinc_MouseDown;
            btJogYinc.MouseUp += btJogYinc_MouseUp;
            // 
            // btJogYdec
            // 
            btJogYdec.Location = new Point(7, 100);
            btJogYdec.Name = "btJogYdec";
            btJogYdec.Size = new Size(102, 41);
            btJogYdec.TabIndex = 0;
            btJogYdec.Text = "Jog(-)";
            btJogYdec.UseVisualStyleBackColor = true;
            btJogYdec.MouseDown += btJogYdec_MouseDown;
            btJogYdec.MouseUp += btJogYdec_MouseUp;
            // 
            // btMoveYasix
            // 
            btMoveYasix.Location = new Point(16, 243);
            btMoveYasix.Name = "btMoveYasix";
            btMoveYasix.Size = new Size(260, 54);
            btMoveYasix.TabIndex = 6;
            btMoveYasix.Text = "위치이동";
            btMoveYasix.UseVisualStyleBackColor = true;
            btMoveYasix.Click += btMoveYasix_Click;
            // 
            // lbcurposy
            // 
            lbcurposy.AutoSize = true;
            lbcurposy.BackColor = Color.Transparent;
            lbcurposy.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposy.Location = new Point(170, 53);
            lbcurposy.Name = "lbcurposy";
            lbcurposy.RightToLeft = RightToLeft.No;
            lbcurposy.Size = new Size(117, 32);
            lbcurposy.TabIndex = 5;
            lbcurposy.Text = "Unknown";
            lbcurposy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbcurposyaxis
            // 
            lbcurposyaxis.AutoSize = true;
            lbcurposyaxis.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposyaxis.Location = new Point(15, 53);
            lbcurposyaxis.Name = "lbcurposyaxis";
            lbcurposyaxis.Size = new Size(110, 32);
            lbcurposyaxis.TabIndex = 4;
            lbcurposyaxis.Text = "현재위치";
            // 
            // tbcmdspeedy
            // 
            tbcmdspeedy.Location = new Point(131, 172);
            tbcmdspeedy.MaxLength = 6;
            tbcmdspeedy.Name = "tbcmdspeedy";
            tbcmdspeedy.Size = new Size(147, 45);
            tbcmdspeedy.TabIndex = 3;
            tbcmdspeedy.Text = "80000";
            tbcmdspeedy.TextAlign = HorizontalAlignment.Right;
            // 
            // tbcmdposy
            // 
            tbcmdposy.Location = new Point(131, 103);
            tbcmdposy.MaxLength = 7;
            tbcmdposy.Name = "tbcmdposy";
            tbcmdposy.Size = new Size(147, 45);
            tbcmdposy.TabIndex = 2;
            tbcmdposy.Text = "0";
            tbcmdposy.TextAlign = HorizontalAlignment.Right;
            // 
            // lbcmdspeedy
            // 
            lbcmdspeedy.AutoSize = true;
            lbcmdspeedy.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdspeedy.Location = new Point(63, 165);
            lbcmdspeedy.Name = "lbcmdspeedy";
            lbcmdspeedy.Size = new Size(62, 32);
            lbcmdspeedy.TabIndex = 1;
            lbcmdspeedy.Text = "속도";
            // 
            // lbcmdposy
            // 
            lbcmdposy.AutoSize = true;
            lbcmdposy.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdposy.Location = new Point(15, 98);
            lbcmdposy.Name = "lbcmdposy";
            lbcmdposy.Size = new Size(110, 32);
            lbcmdposy.TabIndex = 0;
            lbcmdposy.Text = "설정위치";
            // 
            // gbxaxis
            // 
            gbxaxis.BackColor = Color.Lavender;
            gbxaxis.Controls.Add(label1);
            gbxaxis.Controls.Add(tbjogspeedx);
            gbxaxis.Controls.Add(lbjogspeedx);
            gbxaxis.Controls.Add(lbxmaxspeed);
            gbxaxis.Controls.Add(lbxmaxdistance);
            gbxaxis.Controls.Add(gbjogctrlx);
            gbxaxis.Controls.Add(btMoveXasix);
            gbxaxis.Controls.Add(lbcurposx);
            gbxaxis.Controls.Add(lbcurposxaxis);
            gbxaxis.Controls.Add(tbcmdspeedx);
            gbxaxis.Controls.Add(tbcmdposx);
            gbxaxis.Controls.Add(lbcmdspeedx);
            gbxaxis.Controls.Add(lbcmdposx);
            gbxaxis.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbxaxis.Location = new Point(10, 53);
            gbxaxis.Name = "gbxaxis";
            gbxaxis.Size = new Size(289, 624);
            gbxaxis.TabIndex = 0;
            gbxaxis.TabStop = false;
            gbxaxis.Text = "X-axis";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(16, 396);
            label1.Name = "label1";
            label1.Size = new Size(105, 25);
            label1.TabIndex = 12;
            label1.Text = "(0~300000)";
            // 
            // tbjogspeedx
            // 
            tbjogspeedx.Location = new Point(123, 373);
            tbjogspeedx.MaxLength = 6;
            tbjogspeedx.Name = "tbjogspeedx";
            tbjogspeedx.Size = new Size(147, 45);
            tbjogspeedx.TabIndex = 11;
            tbjogspeedx.Text = "50000";
            tbjogspeedx.TextAlign = HorizontalAlignment.Right;
            tbjogspeedx.KeyPress += tbjogspeed_KeyPress;
            // 
            // lbjogspeedx
            // 
            lbjogspeedx.AutoSize = true;
            lbjogspeedx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedx.Location = new Point(55, 366);
            lbjogspeedx.Name = "lbjogspeedx";
            lbjogspeedx.Size = new Size(62, 32);
            lbjogspeedx.TabIndex = 10;
            lbjogspeedx.Text = "속도";
            // 
            // lbxmaxspeed
            // 
            lbxmaxspeed.AutoSize = true;
            lbxmaxspeed.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbxmaxspeed.Location = new Point(25, 195);
            lbxmaxspeed.Name = "lbxmaxspeed";
            lbxmaxspeed.Size = new Size(105, 25);
            lbxmaxspeed.TabIndex = 9;
            lbxmaxspeed.Text = "(0~300000)";
            // 
            // lbxmaxdistance
            // 
            lbxmaxdistance.AutoSize = true;
            lbxmaxdistance.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbxmaxdistance.Location = new Point(10, 128);
            lbxmaxdistance.Name = "lbxmaxdistance";
            lbxmaxdistance.Size = new Size(115, 25);
            lbxmaxdistance.TabIndex = 8;
            lbxmaxdistance.Text = "(0~1350000)";
            // 
            // gbjogctrlx
            // 
            gbjogctrlx.Controls.Add(btJogXinc);
            gbjogctrlx.Controls.Add(btJogXdec);
            gbjogctrlx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbjogctrlx.Location = new Point(13, 328);
            gbjogctrlx.Name = "gbjogctrlx";
            gbjogctrlx.Size = new Size(260, 144);
            gbjogctrlx.TabIndex = 7;
            gbjogctrlx.TabStop = false;
            gbjogctrlx.Text = "Jog Mode";
            // 
            // btJogXinc
            // 
            btJogXinc.Location = new Point(155, 100);
            btJogXinc.Name = "btJogXinc";
            btJogXinc.Size = new Size(102, 41);
            btJogXinc.TabIndex = 1;
            btJogXinc.Text = "Jog(+)";
            btJogXinc.UseVisualStyleBackColor = true;
            btJogXinc.MouseDown += btJogXinc_MouseDown;
            btJogXinc.MouseUp += btJogXinc_MouseUp;
            // 
            // btJogXdec
            // 
            btJogXdec.Location = new Point(7, 100);
            btJogXdec.Name = "btJogXdec";
            btJogXdec.Size = new Size(102, 41);
            btJogXdec.TabIndex = 0;
            btJogXdec.Text = "Jog(-)";
            btJogXdec.UseVisualStyleBackColor = true;
            btJogXdec.MouseDown += btJogXdec_MouseDown;
            btJogXdec.MouseUp += btJogXdec_MouseUp;
            // 
            // btMoveXasix
            // 
            btMoveXasix.Location = new Point(16, 243);
            btMoveXasix.Name = "btMoveXasix";
            btMoveXasix.Size = new Size(260, 54);
            btMoveXasix.TabIndex = 6;
            btMoveXasix.Text = "위치이동";
            btMoveXasix.UseVisualStyleBackColor = true;
            btMoveXasix.Click += btMoveXasix_Click;
            // 
            // lbcurposx
            // 
            lbcurposx.AutoSize = true;
            lbcurposx.BackColor = Color.Transparent;
            lbcurposx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposx.Location = new Point(170, 53);
            lbcurposx.Name = "lbcurposx";
            lbcurposx.RightToLeft = RightToLeft.No;
            lbcurposx.Size = new Size(117, 32);
            lbcurposx.TabIndex = 5;
            lbcurposx.Text = "Unknown";
            lbcurposx.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbcurposxaxis
            // 
            lbcurposxaxis.AutoSize = true;
            lbcurposxaxis.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposxaxis.Location = new Point(15, 53);
            lbcurposxaxis.Name = "lbcurposxaxis";
            lbcurposxaxis.Size = new Size(110, 32);
            lbcurposxaxis.TabIndex = 4;
            lbcurposxaxis.Text = "현재위치";
            // 
            // tbcmdspeedx
            // 
            tbcmdspeedx.Location = new Point(131, 172);
            tbcmdspeedx.MaxLength = 6;
            tbcmdspeedx.Name = "tbcmdspeedx";
            tbcmdspeedx.Size = new Size(147, 45);
            tbcmdspeedx.TabIndex = 3;
            tbcmdspeedx.Text = "100000";
            tbcmdspeedx.TextAlign = HorizontalAlignment.Right;
            tbcmdspeedx.KeyPress += tbcmdspeed_KeyPress;
            // 
            // tbcmdposx
            // 
            tbcmdposx.Location = new Point(131, 103);
            tbcmdposx.MaxLength = 7;
            tbcmdposx.Name = "tbcmdposx";
            tbcmdposx.Size = new Size(147, 45);
            tbcmdposx.TabIndex = 2;
            tbcmdposx.Text = "0";
            tbcmdposx.TextAlign = HorizontalAlignment.Right;
            tbcmdposx.KeyPress += tbcmdpos_KeyPress;
            // 
            // lbcmdspeedx
            // 
            lbcmdspeedx.AutoSize = true;
            lbcmdspeedx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdspeedx.Location = new Point(63, 165);
            lbcmdspeedx.Name = "lbcmdspeedx";
            lbcmdspeedx.Size = new Size(62, 32);
            lbcmdspeedx.TabIndex = 1;
            lbcmdspeedx.Text = "속도";
            // 
            // lbcmdposx
            // 
            lbcmdposx.AutoSize = true;
            lbcmdposx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdposx.Location = new Point(15, 98);
            lbcmdposx.Name = "lbcmdposx";
            lbcmdposx.Size = new Size(110, 32);
            lbcmdposx.TabIndex = 0;
            lbcmdposx.Text = "설정위치";
            // 
            // LiveCell
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1065, 949);
            Controls.Add(gbmotionctrl);
            Controls.Add(gbTransfer);
            Controls.Add(groupBox_comport);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LiveCell";
            Text = "LiveCell";
            FormClosing += LiveCell_FormClosing;
            Load += LiveCell_Load;
            groupBox_comport.ResumeLayout(false);
            groupBox_comport.PerformLayout();
            gbTransfer.ResumeLayout(false);
            gbTransfer.PerformLayout();
            gbmotionctrl.ResumeLayout(false);
            gbyaxis.ResumeLayout(false);
            gbyaxis.PerformLayout();
            gbjogctrly.ResumeLayout(false);
            gbxaxis.ResumeLayout(false);
            gbxaxis.PerformLayout();
            gbjogctrlx.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btconnection;
        private GroupBox groupBox_comport;
        private ComboBox comboBox_available_port;
        private Label port_label;
        private Button btdisconnection;
        private TextBox textBox_RX_data;
        private GroupBox gbTransfer;
        private Button button_tx_send;
        private Button button_disp_clear;
        private TextBox textBox_TX_data;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox gbmotionctrl;
        private GroupBox gbxaxis;
        private TextBox tbcmdposx;
        private Label lbcmdspeedx;
        private Label lbcmdposx;
        private TextBox tbcmdspeedx;
        private Label lbcurposx;
        private Label lbcurposxaxis;
        private Button btMoveXasix;
        private GroupBox gbjogctrlx;
        private Button btJogXinc;
        private Button btJogXdec;
        private Label lbxmaxdistance;
        private Label lbxmaxspeed;
        private Label label1;
        private TextBox tbjogspeedx;
        private Label lbjogspeedx;
        private GroupBox gbyaxis;
        private Label label3;
        private TextBox tbjogspeedy;
        private Label lbjogspeedy;
        private Label lbymaxspeed;
        private Label lbymaxdistance;
        private GroupBox gbjogctrly;
        private Button btJogYinc;
        private Button btJogYdec;
        private Button btMoveYasix;
        private Label lbcurposy;
        private Label lbcurposyaxis;
        private TextBox tbcmdspeedy;
        private TextBox tbcmdposy;
        private Label lbcmdspeedy;
        private Label lbcmdposy;
    }
}
