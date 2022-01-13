namespace Yoga.VisionMix.Units
{
    partial class SysConfigUnit
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.UpdateOnce = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlcData5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlcData4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPlcData3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPlcData2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPlcData1 = new System.Windows.Forms.TextBox();
            this.btnSavePLCData = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkUsePlc = new System.Windows.Forms.CheckBox();
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
            this.Btn_SaveCOM = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkUseIO = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.drpComList1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.drpBaudRate1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.drpParity1 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.drpDataBits1 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.drpStopBits1 = new System.Windows.Forms.ComboBox();
            this.Btn_SaveCOM1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpdateOnce
            // 
            this.UpdateOnce.Enabled = true;
            this.UpdateOnce.Interval = 500;
            this.UpdateOnce.Tick += new System.EventHandler(this.UpdateOnce_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlcData5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPlcData4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtPlcData3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtPlcData2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtPlcData1);
            this.groupBox1.Controls.Add(this.btnSavePLCData);
            this.groupBox1.Location = new System.Drawing.Point(334, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 219);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "plc控制参数";
            // 
            // txtPlcData5
            // 
            this.txtPlcData5.Location = new System.Drawing.Point(153, 136);
            this.txtPlcData5.Name = "txtPlcData5";
            this.txtPlcData5.Size = new System.Drawing.Size(141, 21);
            this.txtPlcData5.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "备用(D24068):";
            // 
            // txtPlcData4
            // 
            this.txtPlcData4.Location = new System.Drawing.Point(153, 106);
            this.txtPlcData4.Name = "txtPlcData4";
            this.txtPlcData4.Size = new System.Drawing.Size(141, 21);
            this.txtPlcData4.TabIndex = 38;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "剔除距离(D2406):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 35;
            this.label11.Text = "屏蔽距离(D2404):";
            // 
            // txtPlcData3
            // 
            this.txtPlcData3.Location = new System.Drawing.Point(153, 79);
            this.txtPlcData3.Name = "txtPlcData3";
            this.txtPlcData3.Size = new System.Drawing.Size(141, 21);
            this.txtPlcData3.TabIndex = 36;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 33;
            this.label10.Text = "剔除距离(D2402):";
            // 
            // txtPlcData2
            // 
            this.txtPlcData2.Location = new System.Drawing.Point(153, 52);
            this.txtPlcData2.Name = "txtPlcData2";
            this.txtPlcData2.Size = new System.Drawing.Size(141, 21);
            this.txtPlcData2.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 32;
            this.label9.Text = "触发距离(D2400):";
            // 
            // txtPlcData1
            // 
            this.txtPlcData1.Location = new System.Drawing.Point(153, 19);
            this.txtPlcData1.Name = "txtPlcData1";
            this.txtPlcData1.Size = new System.Drawing.Size(141, 21);
            this.txtPlcData1.TabIndex = 32;
            // 
            // btnSavePLCData
            // 
            this.btnSavePLCData.Location = new System.Drawing.Point(153, 189);
            this.btnSavePLCData.Name = "btnSavePLCData";
            this.btnSavePLCData.Size = new System.Drawing.Size(78, 24);
            this.btnSavePLCData.TabIndex = 20;
            this.btnSavePLCData.Text = "写入保存";
            this.btnSavePLCData.UseVisualStyleBackColor = true;
            this.btnSavePLCData.Click += new System.EventHandler(this.btnSavePLCData_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkUsePlc);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.Btn_SaveCOM);
            this.groupBox5.Location = new System.Drawing.Point(55, 26);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 219);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "plc通讯设定";
            // 
            // chkUsePlc
            // 
            this.chkUsePlc.AutoSize = true;
            this.chkUsePlc.Location = new System.Drawing.Point(29, 183);
            this.chkUsePlc.Name = "chkUsePlc";
            this.chkUsePlc.Size = new System.Drawing.Size(72, 16);
            this.chkUsePlc.TabIndex = 20;
            this.chkUsePlc.Text = "使用串口";
            this.chkUsePlc.UseVisualStyleBackColor = true;
            this.chkUsePlc.CheckedChanged += new System.EventHandler(this.chkUsePlc_CheckedChanged);
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
            // Btn_SaveCOM
            // 
            this.Btn_SaveCOM.Location = new System.Drawing.Point(118, 178);
            this.Btn_SaveCOM.Name = "Btn_SaveCOM";
            this.Btn_SaveCOM.Size = new System.Drawing.Size(78, 24);
            this.Btn_SaveCOM.TabIndex = 17;
            this.Btn_SaveCOM.Text = "保存";
            this.Btn_SaveCOM.UseVisualStyleBackColor = true;
            this.Btn_SaveCOM.Click += new System.EventHandler(this.Btn_SaveCOM_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkUseIO);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.Btn_SaveCOM1);
            this.groupBox2.Location = new System.Drawing.Point(55, 270);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 219);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IO盒子通讯设定";
            // 
            // chkUseIO
            // 
            this.chkUseIO.AutoSize = true;
            this.chkUseIO.Location = new System.Drawing.Point(29, 183);
            this.chkUseIO.Name = "chkUseIO";
            this.chkUseIO.Size = new System.Drawing.Size(72, 16);
            this.chkUseIO.TabIndex = 20;
            this.chkUseIO.Text = "使用串口";
            this.chkUseIO.UseVisualStyleBackColor = true;
            this.chkUseIO.CheckedChanged += new System.EventHandler(this.chkUseIO_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.drpComList1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.drpBaudRate1);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.drpParity1);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.drpDataBits1);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.drpStopBits1);
            this.groupBox3.Location = new System.Drawing.Point(14, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 150);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "串口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "串口";
            // 
            // drpComList1
            // 
            this.drpComList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpComList1.FormattingEnabled = true;
            this.drpComList1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.drpComList1.Location = new System.Drawing.Point(79, 17);
            this.drpComList1.Name = "drpComList1";
            this.drpComList1.Size = new System.Drawing.Size(89, 20);
            this.drpComList1.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 38;
            this.label4.Text = "波特率";
            // 
            // drpBaudRate1
            // 
            this.drpBaudRate1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpBaudRate1.FormattingEnabled = true;
            this.drpBaudRate1.Items.AddRange(new object[] {
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
            this.drpBaudRate1.Location = new System.Drawing.Point(79, 41);
            this.drpBaudRate1.Name = "drpBaudRate1";
            this.drpBaudRate1.Size = new System.Drawing.Size(89, 20);
            this.drpBaudRate1.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(32, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 40;
            this.label13.Text = "校验位";
            // 
            // drpParity1
            // 
            this.drpParity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpParity1.FormattingEnabled = true;
            this.drpParity1.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.drpParity1.Location = new System.Drawing.Point(79, 67);
            this.drpParity1.Name = "drpParity1";
            this.drpParity1.Size = new System.Drawing.Size(89, 20);
            this.drpParity1.TabIndex = 41;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(32, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 42;
            this.label14.Text = "数据位";
            // 
            // drpDataBits1
            // 
            this.drpDataBits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpDataBits1.FormattingEnabled = true;
            this.drpDataBits1.Items.AddRange(new object[] {
            "8",
            "7",
            "6"});
            this.drpDataBits1.Location = new System.Drawing.Point(79, 93);
            this.drpDataBits1.Name = "drpDataBits1";
            this.drpDataBits1.Size = new System.Drawing.Size(89, 20);
            this.drpDataBits1.TabIndex = 43;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(32, 122);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "停止位";
            // 
            // drpStopBits1
            // 
            this.drpStopBits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpStopBits1.FormattingEnabled = true;
            this.drpStopBits1.Items.AddRange(new object[] {
            "1",
            "2"});
            this.drpStopBits1.Location = new System.Drawing.Point(79, 119);
            this.drpStopBits1.Name = "drpStopBits1";
            this.drpStopBits1.Size = new System.Drawing.Size(89, 20);
            this.drpStopBits1.TabIndex = 45;
            // 
            // Btn_SaveCOM1
            // 
            this.Btn_SaveCOM1.Location = new System.Drawing.Point(118, 178);
            this.Btn_SaveCOM1.Name = "Btn_SaveCOM1";
            this.Btn_SaveCOM1.Size = new System.Drawing.Size(78, 24);
            this.Btn_SaveCOM1.TabIndex = 17;
            this.Btn_SaveCOM1.Text = "保存";
            this.Btn_SaveCOM1.UseVisualStyleBackColor = true;
            this.Btn_SaveCOM1.Click += new System.EventHandler(this.Btn_SaveCOM1_Click);
            // 
            // SysConfigUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Name = "SysConfigUnit";
            this.Size = new System.Drawing.Size(1260, 658);
            this.Load += new System.EventHandler(this.SysConfigFrame_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer UpdateOnce;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPlcData4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPlcData3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPlcData2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPlcData1;
        private System.Windows.Forms.Button btnSavePLCData;
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
        private System.Windows.Forms.Button Btn_SaveCOM;
        private System.Windows.Forms.TextBox txtPlcData5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkUsePlc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkUseIO;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox drpComList1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox drpBaudRate1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox drpParity1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox drpDataBits1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox drpStopBits1;
        private System.Windows.Forms.Button Btn_SaveCOM1;
    }
}
