namespace Yoga.VisionMix.Frame
{
    partial class frmCommSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommSetting));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmServerPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.drpComList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.drpBaudRate = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.drpParity = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.drpDataBits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.drpStopBits = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCommType = new System.Windows.Forms.ComboBox();
            this.chkIsExtTrigger = new System.Windows.Forms.CheckBox();
            this.ipTextBoxExt1 = new Yoga.Common.UI.IpTextBoxExt();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmServerPort)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.btnStart);
            this.groupBox5.Location = new System.Drawing.Point(12, 73);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(459, 219);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "外部通讯设定";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ipTextBoxExt1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.nmServerPort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(220, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 79);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "以太网参数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "端口";
            // 
            // nmServerPort
            // 
            this.nmServerPort.Location = new System.Drawing.Point(46, 46);
            this.nmServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nmServerPort.Name = "nmServerPort";
            this.nmServerPort.Size = new System.Drawing.Size(56, 21);
            this.nmServerPort.TabIndex = 13;
            this.nmServerPort.Value = new decimal(new int[] {
            2756,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "IP:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.drpComList);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.drpBaudRate);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.drpParity);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.drpDataBits);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.drpStopBits);
            this.groupBox6.Location = new System.Drawing.Point(14, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 150);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "串口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "串口";
            // 
            // drpComList
            // 
            this.drpComList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpComList.FormattingEnabled = true;
            this.drpComList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.drpComList.Location = new System.Drawing.Point(79, 17);
            this.drpComList.Name = "drpComList";
            this.drpComList.Size = new System.Drawing.Size(89, 20);
            this.drpComList.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "波特率";
            // 
            // drpBaudRate
            // 
            this.drpBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpBaudRate.FormattingEnabled = true;
            this.drpBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.drpBaudRate.Location = new System.Drawing.Point(79, 41);
            this.drpBaudRate.Name = "drpBaudRate";
            this.drpBaudRate.Size = new System.Drawing.Size(89, 20);
            this.drpBaudRate.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "校验位";
            // 
            // drpParity
            // 
            this.drpParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpParity.FormattingEnabled = true;
            this.drpParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.drpParity.Location = new System.Drawing.Point(79, 67);
            this.drpParity.Name = "drpParity";
            this.drpParity.Size = new System.Drawing.Size(89, 20);
            this.drpParity.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 42;
            this.label7.Text = "数据位";
            // 
            // drpDataBits
            // 
            this.drpDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpDataBits.FormattingEnabled = true;
            this.drpDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6"});
            this.drpDataBits.Location = new System.Drawing.Point(79, 93);
            this.drpDataBits.Name = "drpDataBits";
            this.drpDataBits.Size = new System.Drawing.Size(89, 20);
            this.drpDataBits.TabIndex = 43;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "停止位";
            // 
            // drpStopBits
            // 
            this.drpStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpStopBits.FormattingEnabled = true;
            this.drpStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.drpStopBits.Location = new System.Drawing.Point(79, 119);
            this.drpStopBits.Name = "drpStopBits";
            this.drpStopBits.Size = new System.Drawing.Size(89, 20);
            this.drpStopBits.TabIndex = 45;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(118, 178);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(78, 24);
            this.btnStart.TabIndex = 17;
            this.btnStart.Text = "确定";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCommType);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 44);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通信方式";
            // 
            // cmbCommType
            // 
            this.cmbCommType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommType.FormattingEnabled = true;
            this.cmbCommType.Location = new System.Drawing.Point(93, 17);
            this.cmbCommType.Name = "cmbCommType";
            this.cmbCommType.Size = new System.Drawing.Size(89, 20);
            this.cmbCommType.TabIndex = 46;
            // 
            // chkIsExtTrigger
            // 
            this.chkIsExtTrigger.AutoSize = true;
            this.chkIsExtTrigger.Location = new System.Drawing.Point(372, 31);
            this.chkIsExtTrigger.Name = "chkIsExtTrigger";
            this.chkIsExtTrigger.Size = new System.Drawing.Size(84, 16);
            this.chkIsExtTrigger.TabIndex = 35;
            this.chkIsExtTrigger.Text = "相机外触发";
            this.chkIsExtTrigger.UseVisualStyleBackColor = true;
            // 
            // ipTextBoxExt1
            // 
            this.ipTextBoxExt1.BackColor = System.Drawing.Color.Black;
            this.ipTextBoxExt1.Location = new System.Drawing.Point(42, 14);
            this.ipTextBoxExt1.Name = "ipTextBoxExt1";
            this.ipTextBoxExt1.Size = new System.Drawing.Size(175, 26);
            this.ipTextBoxExt1.TabIndex = 23;
            this.ipTextBoxExt1.Value = ((System.Net.IPAddress)(resources.GetObject("ipTextBoxExt1.Value")));
            // 
            // frmCommSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 304);
            this.Controls.Add(this.chkIsExtTrigger);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Name = "frmCommSetting";
            this.Text = "串口通信设定";
            this.Load += new System.EventHandler(this.frmCommSetting_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmServerPort)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox drpComList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox drpBaudRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox drpParity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox drpDataBits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox drpStopBits;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIsExtTrigger;
        private System.Windows.Forms.ComboBox cmbCommType;
        private Common.UI.IpTextBoxExt ipTextBoxExt1;
    }
}