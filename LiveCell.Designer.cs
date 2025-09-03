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
            cbdebug = new CheckBox();
            button_disp_clear = new Button();
            textBox_TX_data = new TextBox();
            button_tx_send = new Button();
            gbmotionctrl = new GroupBox();
            gbxyzaxis = new GroupBox();
            btHomeXYZ = new Button();
            btMoveXYZasix = new Button();
            gbzaxis = new GroupBox();
            btOffsetZfor = new Button();
            gbHomePosZ = new GroupBox();
            btHomeZ = new Button();
            btOffsetZback = new Button();
            tbOffsetZ = new TextBox();
            lbjogspeedlimitz = new Label();
            tbjogspeedz = new TextBox();
            lbjogspeedz = new Label();
            lbzmaxspeed = new Label();
            lbzmaxdistance = new Label();
            gbjogctrlz = new GroupBox();
            btJogZinc = new Button();
            btJogZdec = new Button();
            btMoveZasix = new Button();
            lbcurposz = new Label();
            lbcurposzaxis = new Label();
            tbcmdspeedz = new TextBox();
            tbcmdposz = new TextBox();
            lbcmdspeedz = new Label();
            lbcmdposz = new Label();
            gbyaxis = new GroupBox();
            btOffsetYfor = new Button();
            gbHomePosY = new GroupBox();
            btHomeY = new Button();
            btOffsetYback = new Button();
            tbOffsetY = new TextBox();
            lbjogspeedlimity = new Label();
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
            btOffsetXfor = new Button();
            btOffsetXback = new Button();
            tbOffsetX = new TextBox();
            gbHomePosX = new GroupBox();
            btHomeX = new Button();
            lbjogspeedlimitx = new Label();
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
            gbxyzaxis.SuspendLayout();
            gbzaxis.SuspendLayout();
            gbHomePosZ.SuspendLayout();
            gbjogctrlz.SuspendLayout();
            gbyaxis.SuspendLayout();
            gbHomePosY.SuspendLayout();
            gbjogctrly.SuspendLayout();
            gbxaxis.SuspendLayout();
            gbHomePosX.SuspendLayout();
            gbjogctrlx.SuspendLayout();
            SuspendLayout();
            // 
            // btconnection
            // 
            btconnection.BackColor = Color.LightBlue;
            btconnection.Font = new Font("맑은 고딕", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btconnection.Location = new Point(14, 110);
            btconnection.Name = "btconnection";
            btconnection.Size = new Size(125, 56);
            btconnection.TabIndex = 0;
            btconnection.Text = "연결";
            btconnection.UseVisualStyleBackColor = false;
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
            gbTransfer.Controls.Add(cbdebug);
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
            // cbdebug
            // 
            cbdebug.AutoSize = true;
            cbdebug.Location = new Point(718, 32);
            cbdebug.Name = "cbdebug";
            cbdebug.Size = new Size(51, 29);
            cbdebug.TabIndex = 5;
            cbdebug.Text = "D";
            cbdebug.UseVisualStyleBackColor = true;
            // 
            // button_disp_clear
            // 
            button_disp_clear.Location = new Point(612, 29);
            button_disp_clear.Name = "button_disp_clear";
            button_disp_clear.Size = new Size(103, 34);
            button_disp_clear.TabIndex = 4;
            button_disp_clear.Text = "Clear";
            button_disp_clear.UseVisualStyleBackColor = true;
            button_disp_clear.Click += button_disp_clear_Click;
            // 
            // textBox_TX_data
            // 
            textBox_TX_data.Location = new Point(120, 31);
            textBox_TX_data.Name = "textBox_TX_data";
            textBox_TX_data.Size = new Size(490, 31);
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
            gbmotionctrl.BackColor = Color.FromArgb(240, 250, 255);
            gbmotionctrl.Controls.Add(gbxyzaxis);
            gbmotionctrl.Controls.Add(gbzaxis);
            gbmotionctrl.Controls.Add(gbyaxis);
            gbmotionctrl.Controls.Add(gbxaxis);
            gbmotionctrl.Font = new Font("맑은 고딕", 16F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbmotionctrl.Location = new Point(4, 194);
            gbmotionctrl.Name = "gbmotionctrl";
            gbmotionctrl.Size = new Size(1065, 881);
            gbmotionctrl.TabIndex = 4;
            gbmotionctrl.TabStop = false;
            gbmotionctrl.Text = "Motion Control";
            // 
            // gbxyzaxis
            // 
            gbxyzaxis.Controls.Add(btHomeXYZ);
            gbxyzaxis.Controls.Add(btMoveXYZasix);
            gbxyzaxis.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbxyzaxis.Location = new Point(46, 734);
            gbxyzaxis.Name = "gbxyzaxis";
            gbxyzaxis.Size = new Size(978, 134);
            gbxyzaxis.TabIndex = 16;
            gbxyzaxis.TabStop = false;
            gbxyzaxis.Text = "XYZ-axis";
            // 
            // btHomeXYZ
            // 
            btHomeXYZ.BackColor = SystemColors.ButtonFace;
            btHomeXYZ.Location = new Point(515, 49);
            btHomeXYZ.Name = "btHomeXYZ";
            btHomeXYZ.Size = new Size(260, 54);
            btHomeXYZ.TabIndex = 18;
            btHomeXYZ.Text = "Home XYZ-axis";
            btHomeXYZ.UseVisualStyleBackColor = false;
            btHomeXYZ.Click += btHomeXYZ_Click;
            // 
            // btMoveXYZasix
            // 
            btMoveXYZasix.BackColor = SystemColors.ButtonFace;
            btMoveXYZasix.Location = new Point(181, 49);
            btMoveXYZasix.Name = "btMoveXYZasix";
            btMoveXYZasix.Size = new Size(260, 54);
            btMoveXYZasix.TabIndex = 17;
            btMoveXYZasix.Text = "Move XYZ-axis";
            btMoveXYZasix.UseVisualStyleBackColor = false;
            btMoveXYZasix.Click += btMoveXYZasix_Click;
            // 
            // gbzaxis
            // 
            gbzaxis.BackColor = SystemColors.ButtonFace;
            gbzaxis.Controls.Add(btOffsetZfor);
            gbzaxis.Controls.Add(gbHomePosZ);
            gbzaxis.Controls.Add(btOffsetZback);
            gbzaxis.Controls.Add(tbOffsetZ);
            gbzaxis.Controls.Add(lbjogspeedlimitz);
            gbzaxis.Controls.Add(tbjogspeedz);
            gbzaxis.Controls.Add(lbjogspeedz);
            gbzaxis.Controls.Add(lbzmaxspeed);
            gbzaxis.Controls.Add(lbzmaxdistance);
            gbzaxis.Controls.Add(gbjogctrlz);
            gbzaxis.Controls.Add(btMoveZasix);
            gbzaxis.Controls.Add(lbcurposz);
            gbzaxis.Controls.Add(lbcurposzaxis);
            gbzaxis.Controls.Add(tbcmdspeedz);
            gbzaxis.Controls.Add(tbcmdposz);
            gbzaxis.Controls.Add(lbcmdspeedz);
            gbzaxis.Controls.Add(lbcmdposz);
            gbzaxis.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbzaxis.Location = new Point(713, 62);
            gbzaxis.Name = "gbzaxis";
            gbzaxis.Size = new Size(308, 664);
            gbzaxis.TabIndex = 15;
            gbzaxis.TabStop = false;
            gbzaxis.Text = "Z-axis";
            // 
            // btOffsetZfor
            // 
            btOffsetZfor.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetZfor.Location = new Point(219, 318);
            btOffsetZfor.Name = "btOffsetZfor";
            btOffsetZfor.Size = new Size(72, 45);
            btOffsetZfor.TabIndex = 22;
            btOffsetZfor.Text = "Up";
            btOffsetZfor.UseVisualStyleBackColor = true;
            btOffsetZfor.Click += btOffsetZfor_Click;
            // 
            // gbHomePosZ
            // 
            gbHomePosZ.Controls.Add(btHomeZ);
            gbHomePosZ.Location = new Point(18, 547);
            gbHomePosZ.Name = "gbHomePosZ";
            gbHomePosZ.Size = new Size(260, 112);
            gbHomePosZ.TabIndex = 14;
            gbHomePosZ.TabStop = false;
            gbHomePosZ.Text = "Home Position";
            // 
            // btHomeZ
            // 
            btHomeZ.BackColor = SystemColors.ButtonFace;
            btHomeZ.Location = new Point(0, 50);
            btHomeZ.Name = "btHomeZ";
            btHomeZ.Size = new Size(260, 54);
            btHomeZ.TabIndex = 0;
            btHomeZ.Text = "Home Z-axis";
            btHomeZ.UseVisualStyleBackColor = false;
            btHomeZ.Click += btHomeZ_Click;
            // 
            // btOffsetZback
            // 
            btOffsetZback.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetZback.Location = new Point(19, 318);
            btOffsetZback.Name = "btOffsetZback";
            btOffsetZback.Size = new Size(72, 45);
            btOffsetZback.TabIndex = 21;
            btOffsetZback.Text = "Dn";
            btOffsetZback.UseVisualStyleBackColor = true;
            btOffsetZback.Click += btOffsetZback_Click;
            // 
            // tbOffsetZ
            // 
            tbOffsetZ.Location = new Point(97, 318);
            tbOffsetZ.MaxLength = 6;
            tbOffsetZ.Name = "tbOffsetZ";
            tbOffsetZ.Size = new Size(116, 45);
            tbOffsetZ.TabIndex = 20;
            tbOffsetZ.Text = "1000";
            tbOffsetZ.TextAlign = HorizontalAlignment.Center;
            // 
            // lbjogspeedlimitz
            // 
            lbjogspeedlimitz.AutoSize = true;
            lbjogspeedlimitz.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedlimitz.Location = new Point(19, 448);
            lbjogspeedlimitz.Name = "lbjogspeedlimitz";
            lbjogspeedlimitz.Size = new Size(95, 25);
            lbjogspeedlimitz.TabIndex = 12;
            lbjogspeedlimitz.Text = "(1~50000)";
            // 
            // tbjogspeedz
            // 
            tbjogspeedz.Location = new Point(126, 425);
            tbjogspeedz.MaxLength = 6;
            tbjogspeedz.Name = "tbjogspeedz";
            tbjogspeedz.Size = new Size(147, 45);
            tbjogspeedz.TabIndex = 11;
            tbjogspeedz.Text = "10000";
            tbjogspeedz.TextAlign = HorizontalAlignment.Right;
            tbjogspeedz.KeyPress += tbjogspeedz_KeyPress;
            // 
            // lbjogspeedz
            // 
            lbjogspeedz.AutoSize = true;
            lbjogspeedz.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedz.Location = new Point(58, 418);
            lbjogspeedz.Name = "lbjogspeedz";
            lbjogspeedz.Size = new Size(62, 32);
            lbjogspeedz.TabIndex = 10;
            lbjogspeedz.Text = "속도";
            // 
            // lbzmaxspeed
            // 
            lbzmaxspeed.AutoSize = true;
            lbzmaxspeed.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbzmaxspeed.Location = new Point(25, 195);
            lbzmaxspeed.Name = "lbzmaxspeed";
            lbzmaxspeed.Size = new Size(95, 25);
            lbzmaxspeed.TabIndex = 9;
            lbzmaxspeed.Text = "(0~50000)";
            // 
            // lbzmaxdistance
            // 
            lbzmaxdistance.AutoSize = true;
            lbzmaxdistance.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbzmaxdistance.Location = new Point(15, 128);
            lbzmaxdistance.Name = "lbzmaxdistance";
            lbzmaxdistance.Size = new Size(95, 25);
            lbzmaxdistance.TabIndex = 8;
            lbzmaxdistance.Text = "(0~20000)";
            // 
            // gbjogctrlz
            // 
            gbjogctrlz.Controls.Add(btJogZinc);
            gbjogctrlz.Controls.Add(btJogZdec);
            gbjogctrlz.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbjogctrlz.Location = new Point(16, 380);
            gbjogctrlz.Name = "gbjogctrlz";
            gbjogctrlz.Size = new Size(260, 144);
            gbjogctrlz.TabIndex = 7;
            gbjogctrlz.TabStop = false;
            gbjogctrlz.Text = "Jog Mode";
            // 
            // btJogZinc
            // 
            btJogZinc.BackColor = SystemColors.ButtonFace;
            btJogZinc.Location = new Point(155, 100);
            btJogZinc.Name = "btJogZinc";
            btJogZinc.Size = new Size(102, 41);
            btJogZinc.TabIndex = 1;
            btJogZinc.Text = "Jog(+)";
            btJogZinc.UseVisualStyleBackColor = false;
            btJogZinc.MouseDown += btJogZinc_MouseDown;
            btJogZinc.MouseUp += btJogZinc_MouseUp;
            // 
            // btJogZdec
            // 
            btJogZdec.BackColor = SystemColors.ButtonFace;
            btJogZdec.Location = new Point(3, 100);
            btJogZdec.Name = "btJogZdec";
            btJogZdec.Size = new Size(102, 41);
            btJogZdec.TabIndex = 0;
            btJogZdec.Text = "Jog(-)";
            btJogZdec.UseVisualStyleBackColor = false;
            btJogZdec.MouseDown += btJogZdec_MouseDown;
            btJogZdec.MouseUp += btJogZdec_MouseUp;
            // 
            // btMoveZasix
            // 
            btMoveZasix.BackColor = SystemColors.ButtonFace;
            btMoveZasix.Location = new Point(16, 243);
            btMoveZasix.Name = "btMoveZasix";
            btMoveZasix.Size = new Size(260, 54);
            btMoveZasix.TabIndex = 6;
            btMoveZasix.Text = "Move Z-axis";
            btMoveZasix.UseVisualStyleBackColor = false;
            btMoveZasix.Click += btMoveZasix_Click;
            // 
            // lbcurposz
            // 
            lbcurposz.AutoSize = true;
            lbcurposz.BackColor = Color.Transparent;
            lbcurposz.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposz.Location = new Point(170, 53);
            lbcurposz.Name = "lbcurposz";
            lbcurposz.RightToLeft = RightToLeft.No;
            lbcurposz.Size = new Size(117, 32);
            lbcurposz.TabIndex = 5;
            lbcurposz.Text = "Unknown";
            lbcurposz.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbcurposzaxis
            // 
            lbcurposzaxis.AutoSize = true;
            lbcurposzaxis.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcurposzaxis.Location = new Point(15, 53);
            lbcurposzaxis.Name = "lbcurposzaxis";
            lbcurposzaxis.Size = new Size(110, 32);
            lbcurposzaxis.TabIndex = 4;
            lbcurposzaxis.Text = "현재위치";
            // 
            // tbcmdspeedz
            // 
            tbcmdspeedz.Location = new Point(131, 172);
            tbcmdspeedz.MaxLength = 6;
            tbcmdspeedz.Name = "tbcmdspeedz";
            tbcmdspeedz.Size = new Size(147, 45);
            tbcmdspeedz.TabIndex = 3;
            tbcmdspeedz.TextAlign = HorizontalAlignment.Right;
            tbcmdspeedz.KeyPress += tbcmdspeedz_KeyPress;
            // 
            // tbcmdposz
            // 
            tbcmdposz.Location = new Point(131, 103);
            tbcmdposz.MaxLength = 5;
            tbcmdposz.Name = "tbcmdposz";
            tbcmdposz.Size = new Size(147, 45);
            tbcmdposz.TabIndex = 2;
            tbcmdposz.Text = "7000";
            tbcmdposz.TextAlign = HorizontalAlignment.Right;
            tbcmdposz.KeyPress += tbcmdposz_KeyPress;
            // 
            // lbcmdspeedz
            // 
            lbcmdspeedz.AutoSize = true;
            lbcmdspeedz.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdspeedz.Location = new Point(63, 165);
            lbcmdspeedz.Name = "lbcmdspeedz";
            lbcmdspeedz.Size = new Size(62, 32);
            lbcmdspeedz.TabIndex = 1;
            lbcmdspeedz.Text = "속도";
            // 
            // lbcmdposz
            // 
            lbcmdposz.AutoSize = true;
            lbcmdposz.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbcmdposz.Location = new Point(15, 98);
            lbcmdposz.Name = "lbcmdposz";
            lbcmdposz.Size = new Size(110, 32);
            lbcmdposz.TabIndex = 0;
            lbcmdposz.Text = "설정위치";
            // 
            // gbyaxis
            // 
            gbyaxis.BackColor = SystemColors.ButtonFace;
            gbyaxis.Controls.Add(btOffsetYfor);
            gbyaxis.Controls.Add(gbHomePosY);
            gbyaxis.Controls.Add(btOffsetYback);
            gbyaxis.Controls.Add(tbOffsetY);
            gbyaxis.Controls.Add(lbjogspeedlimity);
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
            gbyaxis.Location = new Point(380, 62);
            gbyaxis.Name = "gbyaxis";
            gbyaxis.Size = new Size(292, 664);
            gbyaxis.TabIndex = 13;
            gbyaxis.TabStop = false;
            gbyaxis.Text = "Y-axis";
            // 
            // btOffsetYfor
            // 
            btOffsetYfor.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetYfor.Location = new Point(211, 319);
            btOffsetYfor.Name = "btOffsetYfor";
            btOffsetYfor.Size = new Size(72, 45);
            btOffsetYfor.TabIndex = 19;
            btOffsetYfor.Text = "전진";
            btOffsetYfor.UseVisualStyleBackColor = true;
            btOffsetYfor.Click += btOffsetYfor_Click;
            // 
            // gbHomePosY
            // 
            gbHomePosY.Controls.Add(btHomeY);
            gbHomePosY.Location = new Point(15, 549);
            gbHomePosY.Name = "gbHomePosY";
            gbHomePosY.Size = new Size(260, 112);
            gbHomePosY.TabIndex = 14;
            gbHomePosY.TabStop = false;
            gbHomePosY.Text = "Home Position";
            // 
            // btHomeY
            // 
            btHomeY.BackColor = SystemColors.ButtonFace;
            btHomeY.Location = new Point(0, 50);
            btHomeY.Name = "btHomeY";
            btHomeY.Size = new Size(260, 54);
            btHomeY.TabIndex = 0;
            btHomeY.Text = "Home Y-axis";
            btHomeY.UseVisualStyleBackColor = false;
            btHomeY.Click += btHomeY_Click;
            // 
            // btOffsetYback
            // 
            btOffsetYback.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetYback.Location = new Point(11, 319);
            btOffsetYback.Name = "btOffsetYback";
            btOffsetYback.Size = new Size(72, 45);
            btOffsetYback.TabIndex = 18;
            btOffsetYback.Text = "후진";
            btOffsetYback.UseVisualStyleBackColor = true;
            btOffsetYback.Click += btOffsetYback_Click;
            // 
            // tbOffsetY
            // 
            tbOffsetY.Location = new Point(89, 319);
            tbOffsetY.MaxLength = 6;
            tbOffsetY.Name = "tbOffsetY";
            tbOffsetY.Size = new Size(116, 45);
            tbOffsetY.TabIndex = 17;
            tbOffsetY.Text = "1000";
            tbOffsetY.TextAlign = HorizontalAlignment.Center;
            // 
            // lbjogspeedlimity
            // 
            lbjogspeedlimity.AutoSize = true;
            lbjogspeedlimity.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedlimity.Location = new Point(16, 450);
            lbjogspeedlimity.Name = "lbjogspeedlimity";
            lbjogspeedlimity.Size = new Size(105, 25);
            lbjogspeedlimity.TabIndex = 12;
            lbjogspeedlimity.Text = "(0~100000)";
            // 
            // tbjogspeedy
            // 
            tbjogspeedy.Location = new Point(123, 427);
            tbjogspeedy.MaxLength = 6;
            tbjogspeedy.Name = "tbjogspeedy";
            tbjogspeedy.Size = new Size(147, 45);
            tbjogspeedy.TabIndex = 11;
            tbjogspeedy.Text = "30000";
            tbjogspeedy.TextAlign = HorizontalAlignment.Right;
            tbjogspeedy.KeyPress += tbjogspeedy_KeyPress;
            // 
            // lbjogspeedy
            // 
            lbjogspeedy.AutoSize = true;
            lbjogspeedy.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedy.Location = new Point(55, 420);
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
            lbymaxdistance.Size = new Size(95, 25);
            lbymaxdistance.TabIndex = 8;
            lbymaxdistance.Text = "(0~20000)";
            // 
            // gbjogctrly
            // 
            gbjogctrly.Controls.Add(btJogYinc);
            gbjogctrly.Controls.Add(btJogYdec);
            gbjogctrly.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbjogctrly.Location = new Point(13, 382);
            gbjogctrly.Name = "gbjogctrly";
            gbjogctrly.Size = new Size(260, 144);
            gbjogctrly.TabIndex = 7;
            gbjogctrly.TabStop = false;
            gbjogctrly.Text = "Jog Mode";
            // 
            // btJogYinc
            // 
            btJogYinc.BackColor = SystemColors.ButtonFace;
            btJogYinc.Location = new Point(155, 100);
            btJogYinc.Name = "btJogYinc";
            btJogYinc.Size = new Size(102, 41);
            btJogYinc.TabIndex = 1;
            btJogYinc.Text = "Jog(+)";
            btJogYinc.UseVisualStyleBackColor = false;
            btJogYinc.MouseDown += btJogYinc_MouseDown;
            btJogYinc.MouseUp += btJogYinc_MouseUp;
            // 
            // btJogYdec
            // 
            btJogYdec.BackColor = SystemColors.ButtonFace;
            btJogYdec.Location = new Point(3, 100);
            btJogYdec.Name = "btJogYdec";
            btJogYdec.Size = new Size(102, 41);
            btJogYdec.TabIndex = 0;
            btJogYdec.Text = "Jog(-)";
            btJogYdec.UseVisualStyleBackColor = false;
            btJogYdec.MouseDown += btJogYdec_MouseDown;
            btJogYdec.MouseUp += btJogYdec_MouseUp;
            // 
            // btMoveYasix
            // 
            btMoveYasix.BackColor = SystemColors.ButtonFace;
            btMoveYasix.Location = new Point(16, 243);
            btMoveYasix.Name = "btMoveYasix";
            btMoveYasix.Size = new Size(260, 54);
            btMoveYasix.TabIndex = 6;
            btMoveYasix.Text = "Move Y-axis";
            btMoveYasix.UseVisualStyleBackColor = false;
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
            tbcmdspeedy.TextAlign = HorizontalAlignment.Right;
            tbcmdspeedy.KeyPress += tbcmdspeedy_KeyPress;
            // 
            // tbcmdposy
            // 
            tbcmdposy.Location = new Point(131, 103);
            tbcmdposy.MaxLength = 5;
            tbcmdposy.Name = "tbcmdposy";
            tbcmdposy.Size = new Size(147, 45);
            tbcmdposy.TabIndex = 2;
            tbcmdposy.Text = "20000";
            tbcmdposy.TextAlign = HorizontalAlignment.Right;
            tbcmdposy.KeyPress += tbcmdposy_KeyPress;
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
            gbxaxis.BackColor = SystemColors.ButtonFace;
            gbxaxis.Controls.Add(btOffsetXfor);
            gbxaxis.Controls.Add(btOffsetXback);
            gbxaxis.Controls.Add(tbOffsetX);
            gbxaxis.Controls.Add(gbHomePosX);
            gbxaxis.Controls.Add(lbjogspeedlimitx);
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
            gbxaxis.Location = new Point(46, 62);
            gbxaxis.Name = "gbxaxis";
            gbxaxis.Size = new Size(287, 664);
            gbxaxis.TabIndex = 0;
            gbxaxis.TabStop = false;
            gbxaxis.Text = "X-axis";
            // 
            // btOffsetXfor
            // 
            btOffsetXfor.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetXfor.Location = new Point(209, 318);
            btOffsetXfor.Name = "btOffsetXfor";
            btOffsetXfor.Size = new Size(72, 45);
            btOffsetXfor.TabIndex = 16;
            btOffsetXfor.Text = "전진";
            btOffsetXfor.UseVisualStyleBackColor = true;
            btOffsetXfor.Click += btOffsetXfor_Click;
            // 
            // btOffsetXback
            // 
            btOffsetXback.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            btOffsetXback.Location = new Point(9, 318);
            btOffsetXback.Name = "btOffsetXback";
            btOffsetXback.Size = new Size(72, 45);
            btOffsetXback.TabIndex = 15;
            btOffsetXback.Text = "후진";
            btOffsetXback.UseVisualStyleBackColor = true;
            btOffsetXback.Click += btOffsetXback_Click;
            // 
            // tbOffsetX
            // 
            tbOffsetX.Location = new Point(87, 318);
            tbOffsetX.MaxLength = 6;
            tbOffsetX.Name = "tbOffsetX";
            tbOffsetX.Size = new Size(116, 45);
            tbOffsetX.TabIndex = 14;
            tbOffsetX.Text = "1000";
            tbOffsetX.TextAlign = HorizontalAlignment.Center;
            // 
            // gbHomePosX
            // 
            gbHomePosX.Controls.Add(btHomeX);
            gbHomePosX.Location = new Point(15, 549);
            gbHomePosX.Name = "gbHomePosX";
            gbHomePosX.Size = new Size(260, 112);
            gbHomePosX.TabIndex = 13;
            gbHomePosX.TabStop = false;
            gbHomePosX.Text = "Home Position";
            // 
            // btHomeX
            // 
            btHomeX.BackColor = SystemColors.ButtonFace;
            btHomeX.Location = new Point(0, 50);
            btHomeX.Name = "btHomeX";
            btHomeX.Size = new Size(260, 54);
            btHomeX.TabIndex = 0;
            btHomeX.Text = "Home X-axis";
            btHomeX.UseVisualStyleBackColor = false;
            btHomeX.Click += btHomeX_Click;
            // 
            // lbjogspeedlimitx
            // 
            lbjogspeedlimitx.AutoSize = true;
            lbjogspeedlimitx.Font = new Font("맑은 고딕", 9F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedlimitx.Location = new Point(16, 450);
            lbjogspeedlimitx.Name = "lbjogspeedlimitx";
            lbjogspeedlimitx.Size = new Size(105, 25);
            lbjogspeedlimitx.TabIndex = 12;
            lbjogspeedlimitx.Text = "(0~300000)";
            // 
            // tbjogspeedx
            // 
            tbjogspeedx.Location = new Point(125, 427);
            tbjogspeedx.MaxLength = 6;
            tbjogspeedx.Name = "tbjogspeedx";
            tbjogspeedx.Size = new Size(147, 45);
            tbjogspeedx.TabIndex = 11;
            tbjogspeedx.Text = "50000";
            tbjogspeedx.TextAlign = HorizontalAlignment.Right;
            tbjogspeedx.KeyPress += tbjogspeedx_KeyPress;
            // 
            // lbjogspeedx
            // 
            lbjogspeedx.AutoSize = true;
            lbjogspeedx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            lbjogspeedx.Location = new Point(55, 420);
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
            lbxmaxdistance.Size = new Size(105, 25);
            lbxmaxdistance.TabIndex = 8;
            lbxmaxdistance.Text = "(0~120000)";
            // 
            // gbjogctrlx
            // 
            gbjogctrlx.Controls.Add(btJogXinc);
            gbjogctrlx.Controls.Add(btJogXdec);
            gbjogctrlx.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gbjogctrlx.Location = new Point(13, 382);
            gbjogctrlx.Name = "gbjogctrlx";
            gbjogctrlx.Size = new Size(260, 144);
            gbjogctrlx.TabIndex = 7;
            gbjogctrlx.TabStop = false;
            gbjogctrlx.Text = "Jog Mode";
            // 
            // btJogXinc
            // 
            btJogXinc.BackColor = SystemColors.ButtonFace;
            btJogXinc.Location = new Point(157, 100);
            btJogXinc.Name = "btJogXinc";
            btJogXinc.Size = new Size(100, 40);
            btJogXinc.TabIndex = 1;
            btJogXinc.Text = "Jog(+)";
            btJogXinc.UseVisualStyleBackColor = false;
            btJogXinc.MouseDown += btJogXinc_MouseDown;
            btJogXinc.MouseUp += btJogXinc_MouseUp;
            // 
            // btJogXdec
            // 
            btJogXdec.BackColor = SystemColors.ButtonFace;
            btJogXdec.Location = new Point(3, 100);
            btJogXdec.Name = "btJogXdec";
            btJogXdec.Size = new Size(100, 40);
            btJogXdec.TabIndex = 0;
            btJogXdec.Text = "Jog(-)";
            btJogXdec.UseVisualStyleBackColor = false;
            btJogXdec.MouseDown += btJogXdec_MouseDown;
            btJogXdec.MouseUp += btJogXdec_MouseUp;
            // 
            // btMoveXasix
            // 
            btMoveXasix.BackColor = SystemColors.ButtonFace;
            btMoveXasix.Location = new Point(16, 243);
            btMoveXasix.Name = "btMoveXasix";
            btMoveXasix.Size = new Size(260, 54);
            btMoveXasix.TabIndex = 6;
            btMoveXasix.Text = "Move X-axis";
            btMoveXasix.UseVisualStyleBackColor = false;
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
            tbcmdspeedx.TextAlign = HorizontalAlignment.Right;
            tbcmdspeedx.KeyPress += tbcmdspeedx_KeyPress;
            // 
            // tbcmdposx
            // 
            tbcmdposx.Location = new Point(131, 103);
            tbcmdposx.MaxLength = 6;
            tbcmdposx.Name = "tbcmdposx";
            tbcmdposx.Size = new Size(147, 45);
            tbcmdposx.TabIndex = 2;
            tbcmdposx.Text = "26000";
            tbcmdposx.TextAlign = HorizontalAlignment.Right;
            tbcmdposx.KeyPress += tbcmdposx_KeyPress;
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
            ClientSize = new Size(1067, 1072);
            Controls.Add(gbmotionctrl);
            Controls.Add(gbTransfer);
            Controls.Add(groupBox_comport);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "LiveCell";
            Text = "LiveCell Motion Test";
            FormClosing += LiveCell_FormClosing;
            Load += LiveCell_Load;
            groupBox_comport.ResumeLayout(false);
            groupBox_comport.PerformLayout();
            gbTransfer.ResumeLayout(false);
            gbTransfer.PerformLayout();
            gbmotionctrl.ResumeLayout(false);
            gbxyzaxis.ResumeLayout(false);
            gbzaxis.ResumeLayout(false);
            gbzaxis.PerformLayout();
            gbHomePosZ.ResumeLayout(false);
            gbjogctrlz.ResumeLayout(false);
            gbyaxis.ResumeLayout(false);
            gbyaxis.PerformLayout();
            gbHomePosY.ResumeLayout(false);
            gbjogctrly.ResumeLayout(false);
            gbxaxis.ResumeLayout(false);
            gbxaxis.PerformLayout();
            gbHomePosX.ResumeLayout(false);
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
        private Label lbjogspeedlimitx;
        private TextBox tbjogspeedx;
        private Label lbjogspeedx;
        private GroupBox gbyaxis;
        private Label lbjogspeedlimity;
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
        private GroupBox gbHomePosX;
        private Button btHomeX;
        private GroupBox gbHomePosY;
        private Button btHomeY;
        private GroupBox gbzaxis;
        private GroupBox gbHomePosZ;
        private Button btHomeZ;
        private Label lbjogspeedlimitz;
        private TextBox tbjogspeedz;
        private Label lbjogspeedz;
        private Label lbzmaxspeed;
        private Label lbzmaxdistance;
        private GroupBox gbjogctrlz;
        private Button btJogZinc;
        private Button btJogZdec;
        private Button btMoveZasix;
        private Label lbcurposz;
        private Label lbcurposzaxis;
        private TextBox tbcmdspeedz;
        private TextBox tbcmdposz;
        private Label lbcmdspeedz;
        private Label lbcmdposz;
        private GroupBox gbxyzaxis;
        private Button btHomeXYZ;
        private Button btMoveXYZasix;
        private CheckBox cbdebug;
        private TextBox tbOffsetX;
        private Button btOffsetXfor;
        private Button btOffsetXback;
        private Button btOffsetZfor;
        private Button btOffsetZback;
        private TextBox tbOffsetZ;
        private Button btOffsetYfor;
        private Button btOffsetYback;
        private TextBox tbOffsetY;
    }
}
