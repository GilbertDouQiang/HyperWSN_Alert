namespace _432报警器信息修改
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cbx_new_timeSrc = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tbx_new_alert = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbx_new_carousel = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbx_carousel = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbx_new_bps = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.tbx_new_pattern = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbx_alert = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.tbx_ramH = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tbx_pattern = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbx_new_hwRevision = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_new_category = new System.Windows.Forms.TextBox();
            this.tbx_new_customer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbx_new_interval = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_new_id = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbx_new_debug = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbx_swRevisionMSP432 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbx_hwRevision = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbx_category = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_debug = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_customer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_interval = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbx_id = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbx_serverPort = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.tbx_serverDomain = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.tbx_new_serverDomain = new System.Windows.Forms.TextBox();
            this.tbx_new_serverPort = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.tbx_swRevisionCC1310 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tbx_ramL = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbx_flashH = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tbx_flashL = new System.Windows.Forms.TextBox();
            this.cbx_new_channel = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cbx_new_transPolicy = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.cbx_bps = new System.Windows.Forms.ComboBox();
            this.cbx_timeSrc = new System.Windows.Forms.ComboBox();
            this.cbx_transPolicy = new System.Windows.Forms.ComboBox();
            this.cbx_channel = new System.Windows.Forms.ComboBox();
            this.cbx_queueNo = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cbx_new_timeSrc
            // 
            this.cbx_new_timeSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_new_timeSrc.FormattingEnabled = true;
            this.cbx_new_timeSrc.Items.AddRange(new object[] {
            resources.GetString("cbx_new_timeSrc.Items"),
            resources.GetString("cbx_new_timeSrc.Items1")});
            resources.ApplyResources(this.cbx_new_timeSrc, "cbx_new_timeSrc");
            this.cbx_new_timeSrc.Name = "cbx_new_timeSrc";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbx_new_alert
            // 
            resources.ApplyResources(this.tbx_new_alert, "tbx_new_alert");
            this.tbx_new_alert.Name = "tbx_new_alert";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // tbx_new_carousel
            // 
            resources.ApplyResources(this.tbx_new_carousel, "tbx_new_carousel");
            this.tbx_new_carousel.Name = "tbx_new_carousel";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // tbx_carousel
            // 
            resources.ApplyResources(this.tbx_carousel, "tbx_carousel");
            this.tbx_carousel.Name = "tbx_carousel";
            this.tbx_carousel.ReadOnly = true;
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // cbx_new_bps
            // 
            this.cbx_new_bps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_new_bps.FormattingEnabled = true;
            this.cbx_new_bps.Items.AddRange(new object[] {
            resources.GetString("cbx_new_bps.Items"),
            resources.GetString("cbx_new_bps.Items1"),
            resources.GetString("cbx_new_bps.Items2")});
            resources.ApplyResources(this.cbx_new_bps, "cbx_new_bps");
            this.cbx_new_bps.Name = "cbx_new_bps";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // tbx_new_pattern
            // 
            resources.ApplyResources(this.tbx_new_pattern, "tbx_new_pattern");
            this.tbx_new_pattern.Name = "tbx_new_pattern";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // tbx_alert
            // 
            resources.ApplyResources(this.tbx_alert, "tbx_alert");
            this.tbx_alert.Name = "tbx_alert";
            this.tbx_alert.ReadOnly = true;
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // tbx_ramH
            // 
            resources.ApplyResources(this.tbx_ramH, "tbx_ramH");
            this.tbx_ramH.Name = "tbx_ramH";
            this.tbx_ramH.ReadOnly = true;
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // tbx_pattern
            // 
            resources.ApplyResources(this.tbx_pattern, "tbx_pattern");
            this.tbx_pattern.Name = "tbx_pattern";
            this.tbx_pattern.ReadOnly = true;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbx_new_hwRevision
            // 
            resources.ApplyResources(this.tbx_new_hwRevision, "tbx_new_hwRevision");
            this.tbx_new_hwRevision.Name = "tbx_new_hwRevision";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tbx_new_category
            // 
            resources.ApplyResources(this.tbx_new_category, "tbx_new_category");
            this.tbx_new_category.Name = "tbx_new_category";
            // 
            // tbx_new_customer
            // 
            resources.ApplyResources(this.tbx_new_customer, "tbx_new_customer");
            this.tbx_new_customer.Name = "tbx_new_customer";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // tbx_new_interval
            // 
            resources.ApplyResources(this.tbx_new_interval, "tbx_new_interval");
            this.tbx_new_interval.Name = "tbx_new_interval";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tbx_new_id
            // 
            resources.ApplyResources(this.tbx_new_id, "tbx_new_id");
            this.tbx_new_id.Name = "tbx_new_id";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // tbx_new_debug
            // 
            resources.ApplyResources(this.tbx_new_debug, "tbx_new_debug");
            this.tbx_new_debug.Name = "tbx_new_debug";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOpen
            // 
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbx_swRevisionMSP432
            // 
            resources.ApplyResources(this.tbx_swRevisionMSP432, "tbx_swRevisionMSP432");
            this.tbx_swRevisionMSP432.Name = "tbx_swRevisionMSP432";
            this.tbx_swRevisionMSP432.ReadOnly = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // tbx_hwRevision
            // 
            this.tbx_hwRevision.AcceptsTab = true;
            resources.ApplyResources(this.tbx_hwRevision, "tbx_hwRevision");
            this.tbx_hwRevision.Name = "tbx_hwRevision";
            this.tbx_hwRevision.ReadOnly = true;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // tbx_category
            // 
            resources.ApplyResources(this.tbx_category, "tbx_category");
            this.tbx_category.Name = "tbx_category";
            this.tbx_category.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tbx_debug
            // 
            resources.ApplyResources(this.tbx_debug, "tbx_debug");
            this.tbx_debug.Name = "tbx_debug";
            this.tbx_debug.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tbx_customer
            // 
            resources.ApplyResources(this.tbx_customer, "tbx_customer");
            this.tbx_customer.Name = "tbx_customer";
            this.tbx_customer.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tbx_interval
            // 
            resources.ApplyResources(this.tbx_interval, "tbx_interval");
            this.tbx_interval.Name = "tbx_interval";
            this.tbx_interval.ReadOnly = true;
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // tbx_id
            // 
            this.tbx_id.AcceptsTab = true;
            resources.ApplyResources(this.tbx_id, "tbx_id");
            this.tbx_id.Name = "tbx_id";
            this.tbx_id.ReadOnly = true;
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // tbx_serverPort
            // 
            resources.ApplyResources(this.tbx_serverPort, "tbx_serverPort");
            this.tbx_serverPort.Name = "tbx_serverPort";
            this.tbx_serverPort.ReadOnly = true;
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.Name = "label46";
            // 
            // tbx_serverDomain
            // 
            resources.ApplyResources(this.tbx_serverDomain, "tbx_serverDomain");
            this.tbx_serverDomain.Name = "tbx_serverDomain";
            this.tbx_serverDomain.ReadOnly = true;
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.Name = "label47";
            // 
            // tbx_new_serverDomain
            // 
            resources.ApplyResources(this.tbx_new_serverDomain, "tbx_new_serverDomain");
            this.tbx_new_serverDomain.Name = "tbx_new_serverDomain";
            // 
            // tbx_new_serverPort
            // 
            resources.ApplyResources(this.tbx_new_serverPort, "tbx_new_serverPort");
            this.tbx_new_serverPort.Name = "tbx_new_serverPort";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // tbx_swRevisionCC1310
            // 
            resources.ApplyResources(this.tbx_swRevisionCC1310, "tbx_swRevisionCC1310");
            this.tbx_swRevisionCC1310.Name = "tbx_swRevisionCC1310";
            this.tbx_swRevisionCC1310.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // tbx_ramL
            // 
            resources.ApplyResources(this.tbx_ramL, "tbx_ramL");
            this.tbx_ramL.Name = "tbx_ramL";
            this.tbx_ramL.ReadOnly = true;
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // tbx_flashH
            // 
            resources.ApplyResources(this.tbx_flashH, "tbx_flashH");
            this.tbx_flashH.Name = "tbx_flashH";
            this.tbx_flashH.ReadOnly = true;
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // tbx_flashL
            // 
            resources.ApplyResources(this.tbx_flashL, "tbx_flashL");
            this.tbx_flashL.Name = "tbx_flashL";
            this.tbx_flashL.ReadOnly = true;
            // 
            // cbx_new_channel
            // 
            this.cbx_new_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_new_channel.FormattingEnabled = true;
            this.cbx_new_channel.Items.AddRange(new object[] {
            resources.GetString("cbx_new_channel.Items"),
            resources.GetString("cbx_new_channel.Items1"),
            resources.GetString("cbx_new_channel.Items2"),
            resources.GetString("cbx_new_channel.Items3"),
            resources.GetString("cbx_new_channel.Items4"),
            resources.GetString("cbx_new_channel.Items5"),
            resources.GetString("cbx_new_channel.Items6"),
            resources.GetString("cbx_new_channel.Items7")});
            resources.ApplyResources(this.cbx_new_channel, "cbx_new_channel");
            this.cbx_new_channel.Name = "cbx_new_channel";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // cbx_new_transPolicy
            // 
            this.cbx_new_transPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_new_transPolicy.FormattingEnabled = true;
            this.cbx_new_transPolicy.Items.AddRange(new object[] {
            resources.GetString("cbx_new_transPolicy.Items"),
            resources.GetString("cbx_new_transPolicy.Items1")});
            resources.ApplyResources(this.cbx_new_transPolicy, "cbx_new_transPolicy");
            this.cbx_new_transPolicy.Name = "cbx_new_transPolicy";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // cbx_bps
            // 
            this.cbx_bps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_bps.FormattingEnabled = true;
            this.cbx_bps.Items.AddRange(new object[] {
            resources.GetString("cbx_bps.Items"),
            resources.GetString("cbx_bps.Items1"),
            resources.GetString("cbx_bps.Items2")});
            resources.ApplyResources(this.cbx_bps, "cbx_bps");
            this.cbx_bps.Name = "cbx_bps";
            // 
            // cbx_timeSrc
            // 
            this.cbx_timeSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_timeSrc.FormattingEnabled = true;
            this.cbx_timeSrc.Items.AddRange(new object[] {
            resources.GetString("cbx_timeSrc.Items"),
            resources.GetString("cbx_timeSrc.Items1")});
            resources.ApplyResources(this.cbx_timeSrc, "cbx_timeSrc");
            this.cbx_timeSrc.Name = "cbx_timeSrc";
            // 
            // cbx_transPolicy
            // 
            this.cbx_transPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_transPolicy.FormattingEnabled = true;
            this.cbx_transPolicy.Items.AddRange(new object[] {
            resources.GetString("cbx_transPolicy.Items"),
            resources.GetString("cbx_transPolicy.Items1")});
            resources.ApplyResources(this.cbx_transPolicy, "cbx_transPolicy");
            this.cbx_transPolicy.Name = "cbx_transPolicy";
            // 
            // cbx_channel
            // 
            this.cbx_channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_channel.FormattingEnabled = true;
            this.cbx_channel.Items.AddRange(new object[] {
            resources.GetString("cbx_channel.Items"),
            resources.GetString("cbx_channel.Items1"),
            resources.GetString("cbx_channel.Items2"),
            resources.GetString("cbx_channel.Items3"),
            resources.GetString("cbx_channel.Items4"),
            resources.GetString("cbx_channel.Items5"),
            resources.GetString("cbx_channel.Items6"),
            resources.GetString("cbx_channel.Items7")});
            resources.ApplyResources(this.cbx_channel, "cbx_channel");
            this.cbx_channel.Name = "cbx_channel";
            // 
            // cbx_queueNo
            // 
            this.cbx_queueNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_queueNo.FormattingEnabled = true;
            this.cbx_queueNo.Items.AddRange(new object[] {
            resources.GetString("cbx_queueNo.Items"),
            resources.GetString("cbx_queueNo.Items1"),
            resources.GetString("cbx_queueNo.Items2"),
            resources.GetString("cbx_queueNo.Items3"),
            resources.GetString("cbx_queueNo.Items4")});
            resources.ApplyResources(this.cbx_queueNo, "cbx_queueNo");
            this.cbx_queueNo.Name = "cbx_queueNo";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cbx_queueNo);
            this.Controls.Add(this.cbx_channel);
            this.Controls.Add(this.cbx_transPolicy);
            this.Controls.Add(this.cbx_timeSrc);
            this.Controls.Add(this.cbx_bps);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.cbx_new_transPolicy);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbx_new_channel);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.tbx_flashL);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.tbx_flashH);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.tbx_ramL);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbx_swRevisionCC1310);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_new_serverDomain);
            this.Controls.Add(this.tbx_new_serverPort);
            this.Controls.Add(this.label50);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.tbx_serverPort);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.tbx_serverDomain);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.cbx_new_timeSrc);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tbx_new_alert);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.tbx_new_carousel);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.tbx_carousel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cbx_new_bps);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.tbx_new_pattern);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.tbx_alert);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.tbx_ramH);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.tbx_pattern);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbx_new_hwRevision);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbx_new_category);
            this.Controls.Add(this.tbx_new_customer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbx_new_interval);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbx_new_id);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbx_new_debug);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbx_swRevisionMSP432);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbx_hwRevision);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbx_category);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbx_debug);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbx_customer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_interval);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.tbx_id);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cbx_new_timeSrc;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbx_new_alert;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbx_new_carousel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbx_carousel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cbx_new_bps;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox tbx_new_pattern;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbx_alert;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox tbx_ramH;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbx_pattern;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbx_new_hwRevision;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbx_new_category;
        private System.Windows.Forms.TextBox tbx_new_customer;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbx_new_interval;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbx_new_id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbx_new_debug;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox tbx_swRevisionMSP432;
		private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbx_hwRevision;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbx_category;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_debug;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_customer;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_interval;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbx_id;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbx_serverPort;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox tbx_serverDomain;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox tbx_new_serverDomain;
        private System.Windows.Forms.TextBox tbx_new_serverPort;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.TextBox tbx_swRevisionCC1310;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbx_ramL;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tbx_flashH;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbx_flashL;
        private System.Windows.Forms.ComboBox cbx_new_channel;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox cbx_new_transPolicy;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cbx_bps;
        private System.Windows.Forms.ComboBox cbx_timeSrc;
        private System.Windows.Forms.ComboBox cbx_transPolicy;
        private System.Windows.Forms.ComboBox cbx_channel;
        private System.Windows.Forms.ComboBox cbx_queueNo;
        private System.Windows.Forms.Label label30;
    }
}

