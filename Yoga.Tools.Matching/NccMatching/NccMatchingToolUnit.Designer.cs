namespace Yoga.Tools.NccMatching
{
    partial class NccMatchingToolUnit
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxCreateROI = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAccuracy = new System.Windows.Forms.ComboBox();
            this.NumMatchesUpDown = new System.Windows.Forms.NumericUpDown();
            this.AngleExtentUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.ckbPyramidLevelAuto = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.PyramidLevelUpDown = new System.Windows.Forms.NumericUpDown();
            this.MinScoreUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.btnShowRefImage = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPageTestModel = new System.Windows.Forms.TabPage();
            this.FindAlwaysCheckBox = new System.Windows.Forms.CheckBox();
            this.testImgListBox = new System.Windows.Forms.ListBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.deleteAllTestImgButton = new System.Windows.Forms.Button();
            this.deleteTestImgButton = new System.Windows.Forms.Button();
            this.loadTestImgButton = new System.Windows.Forms.Button();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.reduceRect1Button = new System.Windows.Forms.Button();
            this.circleButton = new System.Windows.Forms.Button();
            this.delROIButton = new System.Windows.Forms.Button();
            this.delAllROIButton = new System.Windows.Forms.Button();
            this.rect2Button = new System.Windows.Forms.Button();
            this.subFromROIButton = new System.Windows.Forms.RadioButton();
            this.addToROIButton = new System.Windows.Forms.RadioButton();
            this.rect1Button = new System.Windows.Forms.Button();
            this.chkUseCalib = new System.Windows.Forms.CheckBox();
            this.groupBoxCreateROI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMatchesUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleExtentUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PyramidLevelUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinScoreUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageTestModel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCreateROI
            // 
            this.groupBoxCreateROI.Controls.Add(this.chkUseCalib);
            this.groupBoxCreateROI.Controls.Add(this.label3);
            this.groupBoxCreateROI.Controls.Add(this.comboBoxAccuracy);
            this.groupBoxCreateROI.Controls.Add(this.NumMatchesUpDown);
            this.groupBoxCreateROI.Controls.Add(this.AngleExtentUpDown);
            this.groupBoxCreateROI.Controls.Add(this.label6);
            this.groupBoxCreateROI.Controls.Add(this.ckbPyramidLevelAuto);
            this.groupBoxCreateROI.Controls.Add(this.label14);
            this.groupBoxCreateROI.Controls.Add(this.PyramidLevelUpDown);
            this.groupBoxCreateROI.Controls.Add(this.MinScoreUpDown);
            this.groupBoxCreateROI.Controls.Add(this.label8);
            this.groupBoxCreateROI.Controls.Add(this.label13);
            this.groupBoxCreateROI.Location = new System.Drawing.Point(19, 6);
            this.groupBoxCreateROI.Name = "groupBoxCreateROI";
            this.groupBoxCreateROI.Size = new System.Drawing.Size(330, 380);
            this.groupBoxCreateROI.TabIndex = 78;
            this.groupBoxCreateROI.TabStop = false;
            this.groupBoxCreateROI.Text = "参数设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 92;
            this.label3.Text = "匹配精度";
            // 
            // comboBoxAccuracy
            // 
            this.comboBoxAccuracy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccuracy.FormattingEnabled = true;
            this.comboBoxAccuracy.Location = new System.Drawing.Point(85, 164);
            this.comboBoxAccuracy.Name = "comboBoxAccuracy";
            this.comboBoxAccuracy.Size = new System.Drawing.Size(112, 20);
            this.comboBoxAccuracy.TabIndex = 91;
            this.comboBoxAccuracy.SelectedIndexChanged += new System.EventHandler(this.comboBoxAccuracy_SelectedIndexChanged);
            // 
            // NumMatchesUpDown
            // 
            this.NumMatchesUpDown.Location = new System.Drawing.Point(85, 137);
            this.NumMatchesUpDown.Name = "NumMatchesUpDown";
            this.NumMatchesUpDown.Size = new System.Drawing.Size(57, 21);
            this.NumMatchesUpDown.TabIndex = 88;
            this.NumMatchesUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumMatchesUpDown.ValueChanged += new System.EventHandler(this.NumMatchesUpDown_ValueChanged);
            // 
            // AngleExtentUpDown
            // 
            this.AngleExtentUpDown.Location = new System.Drawing.Point(85, 74);
            this.AngleExtentUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.AngleExtentUpDown.Name = "AngleExtentUpDown";
            this.AngleExtentUpDown.Size = new System.Drawing.Size(58, 21);
            this.AngleExtentUpDown.TabIndex = 90;
            this.AngleExtentUpDown.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.AngleExtentUpDown.ValueChanged += new System.EventHandler(this.AngleExtentUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 44);
            this.label6.TabIndex = 89;
            this.label6.Text = "角度范围";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbPyramidLevelAuto
            // 
            this.ckbPyramidLevelAuto.AutoSize = true;
            this.ckbPyramidLevelAuto.Location = new System.Drawing.Point(164, 41);
            this.ckbPyramidLevelAuto.Name = "ckbPyramidLevelAuto";
            this.ckbPyramidLevelAuto.Size = new System.Drawing.Size(48, 16);
            this.ckbPyramidLevelAuto.TabIndex = 88;
            this.ckbPyramidLevelAuto.Text = "自动";
            this.ckbPyramidLevelAuto.UseVisualStyleBackColor = true;
            this.ckbPyramidLevelAuto.CheckedChanged += new System.EventHandler(this.ckbPyramidLevelAuto_CheckedChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(6, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 26);
            this.label14.TabIndex = 87;
            this.label14.Text = "匹配个数";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PyramidLevelUpDown
            // 
            this.PyramidLevelUpDown.Location = new System.Drawing.Point(85, 36);
            this.PyramidLevelUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.PyramidLevelUpDown.Name = "PyramidLevelUpDown";
            this.PyramidLevelUpDown.Size = new System.Drawing.Size(58, 21);
            this.PyramidLevelUpDown.TabIndex = 86;
            this.PyramidLevelUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PyramidLevelUpDown.ValueChanged += new System.EventHandler(this.PyramidLevelUpDown_ValueChanged);
            // 
            // MinScoreUpDown
            // 
            this.MinScoreUpDown.DecimalPlaces = 2;
            this.MinScoreUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.MinScoreUpDown.Location = new System.Drawing.Point(85, 105);
            this.MinScoreUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinScoreUpDown.Name = "MinScoreUpDown";
            this.MinScoreUpDown.Size = new System.Drawing.Size(57, 21);
            this.MinScoreUpDown.TabIndex = 86;
            this.MinScoreUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.MinScoreUpDown.ValueChanged += new System.EventHandler(this.MinScoreUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 43);
            this.label8.TabIndex = 85;
            this.label8.Text = "金字塔级别";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(6, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 43);
            this.label13.TabIndex = 85;
            this.label13.Text = "最小分数";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TestModelButton);
            this.groupBox3.Location = new System.Drawing.Point(19, 406);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 48);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(173, 13);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(105, 30);
            this.TestModelButton.TabIndex = 73;
            this.TestModelButton.Text = "训练图像";
            this.TestModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
            // 
            // btnShowRefImage
            // 
            this.btnShowRefImage.Location = new System.Drawing.Point(285, 331);
            this.btnShowRefImage.Name = "btnShowRefImage";
            this.btnShowRefImage.Size = new System.Drawing.Size(108, 29);
            this.btnShowRefImage.TabIndex = 74;
            this.btnShowRefImage.Text = "显示标准图像";
            this.btnShowRefImage.Click += new System.EventHandler(this.btnShowRefImage_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(17, 336);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(173, 143);
            this.txtResult.TabIndex = 81;
            this.txtResult.Text = "结果";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageTestModel);
            this.tabControl1.Location = new System.Drawing.Point(416, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 503);
            this.tabControl1.TabIndex = 82;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBoxCreateROI);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 477);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数";
            // 
            // tabPageTestModel
            // 
            this.tabPageTestModel.Controls.Add(this.FindAlwaysCheckBox);
            this.tabPageTestModel.Controls.Add(this.testImgListBox);
            this.tabPageTestModel.Controls.Add(this.btnTest);
            this.tabPageTestModel.Controls.Add(this.deleteAllTestImgButton);
            this.tabPageTestModel.Controls.Add(this.deleteTestImgButton);
            this.tabPageTestModel.Controls.Add(this.loadTestImgButton);
            this.tabPageTestModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageTestModel.Name = "tabPageTestModel";
            this.tabPageTestModel.Size = new System.Drawing.Size(362, 477);
            this.tabPageTestModel.TabIndex = 1;
            this.tabPageTestModel.Text = "模板测试";
            // 
            // FindAlwaysCheckBox
            // 
            this.FindAlwaysCheckBox.Location = new System.Drawing.Point(233, 219);
            this.FindAlwaysCheckBox.Name = "FindAlwaysCheckBox";
            this.FindAlwaysCheckBox.Size = new System.Drawing.Size(105, 35);
            this.FindAlwaysCheckBox.TabIndex = 68;
            this.FindAlwaysCheckBox.Text = "一直测试";
            // 
            // testImgListBox
            // 
            this.testImgListBox.HorizontalScrollbar = true;
            this.testImgListBox.ItemHeight = 12;
            this.testImgListBox.Location = new System.Drawing.Point(38, 26);
            this.testImgListBox.Name = "testImgListBox";
            this.testImgListBox.Size = new System.Drawing.Size(173, 220);
            this.testImgListBox.TabIndex = 67;
            this.testImgListBox.SelectedIndexChanged += new System.EventHandler(this.testImgListBox_SelectedIndexChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(233, 183);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(105, 30);
            this.btnTest.TabIndex = 66;
            this.btnTest.Text = "测试图像";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // deleteAllTestImgButton
            // 
            this.deleteAllTestImgButton.Location = new System.Drawing.Point(233, 86);
            this.deleteAllTestImgButton.Name = "deleteAllTestImgButton";
            this.deleteAllTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.deleteAllTestImgButton.TabIndex = 64;
            this.deleteAllTestImgButton.Text = "删除所有";
            this.deleteAllTestImgButton.Click += new System.EventHandler(this.deleteAllTestImgButton_Click);
            // 
            // deleteTestImgButton
            // 
            this.deleteTestImgButton.Location = new System.Drawing.Point(233, 51);
            this.deleteTestImgButton.Name = "deleteTestImgButton";
            this.deleteTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.deleteTestImgButton.TabIndex = 63;
            this.deleteTestImgButton.Text = "删除图像";
            this.deleteTestImgButton.Click += new System.EventHandler(this.deleteTestImgButton_Click);
            // 
            // loadTestImgButton
            // 
            this.loadTestImgButton.Location = new System.Drawing.Point(233, 17);
            this.loadTestImgButton.Name = "loadTestImgButton";
            this.loadTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.loadTestImgButton.TabIndex = 0;
            this.loadTestImgButton.Text = "加载图像";
            this.loadTestImgButton.Click += new System.EventHandler(this.loadTestImgButton_Click);
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(4, 64);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(406, 275);
            this.hWndUnit1.TabIndex = 83;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reduceRect1Button);
            this.groupBox2.Controls.Add(this.circleButton);
            this.groupBox2.Controls.Add(this.delROIButton);
            this.groupBox2.Controls.Add(this.delAllROIButton);
            this.groupBox2.Controls.Add(this.rect2Button);
            this.groupBox2.Controls.Add(this.subFromROIButton);
            this.groupBox2.Controls.Add(this.addToROIButton);
            this.groupBox2.Controls.Add(this.rect1Button);
            this.groupBox2.Location = new System.Drawing.Point(212, 379);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(202, 155);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "创建ROI";
            // 
            // reduceRect1Button
            // 
            this.reduceRect1Button.Location = new System.Drawing.Point(101, 58);
            this.reduceRect1Button.Name = "reduceRect1Button";
            this.reduceRect1Button.Size = new System.Drawing.Size(80, 26);
            this.reduceRect1Button.TabIndex = 13;
            this.reduceRect1Button.Text = "搜索矩形";
            this.reduceRect1Button.Click += new System.EventHandler(this.reduceRect1Button_Click);
            // 
            // circleButton
            // 
            this.circleButton.Location = new System.Drawing.Point(6, 117);
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(80, 26);
            this.circleButton.TabIndex = 12;
            this.circleButton.Text = "圆形";
            this.circleButton.Click += new System.EventHandler(this.circleButton_Click);
            // 
            // delROIButton
            // 
            this.delROIButton.Location = new System.Drawing.Point(96, 90);
            this.delROIButton.Name = "delROIButton";
            this.delROIButton.Size = new System.Drawing.Size(86, 26);
            this.delROIButton.TabIndex = 11;
            this.delROIButton.Text = "删除激活ROI";
            this.delROIButton.Click += new System.EventHandler(this.delROIButton_Click);
            // 
            // delAllROIButton
            // 
            this.delAllROIButton.Location = new System.Drawing.Point(96, 117);
            this.delAllROIButton.Name = "delAllROIButton";
            this.delAllROIButton.Size = new System.Drawing.Size(86, 26);
            this.delAllROIButton.TabIndex = 10;
            this.delAllROIButton.Text = "删除所有ROI";
            this.delAllROIButton.Click += new System.EventHandler(this.delAllROIButton_Click);
            // 
            // rect2Button
            // 
            this.rect2Button.Location = new System.Drawing.Point(6, 88);
            this.rect2Button.Name = "rect2Button";
            this.rect2Button.Size = new System.Drawing.Size(80, 26);
            this.rect2Button.TabIndex = 9;
            this.rect2Button.Text = "带角度矩形";
            this.rect2Button.Click += new System.EventHandler(this.rect2Button_Click);
            // 
            // subFromROIButton
            // 
            this.subFromROIButton.Location = new System.Drawing.Point(125, 17);
            this.subFromROIButton.Name = "subFromROIButton";
            this.subFromROIButton.Size = new System.Drawing.Size(57, 43);
            this.subFromROIButton.TabIndex = 8;
            this.subFromROIButton.Text = "(-)";
            this.subFromROIButton.CheckedChanged += new System.EventHandler(this.subFromROIButton_CheckedChanged);
            // 
            // addToROIButton
            // 
            this.addToROIButton.Checked = true;
            this.addToROIButton.Location = new System.Drawing.Point(38, 17);
            this.addToROIButton.Name = "addToROIButton";
            this.addToROIButton.Size = new System.Drawing.Size(48, 43);
            this.addToROIButton.TabIndex = 7;
            this.addToROIButton.TabStop = true;
            this.addToROIButton.Text = "(+)";
            this.addToROIButton.CheckedChanged += new System.EventHandler(this.addToROIButton_CheckedChanged);
            // 
            // rect1Button
            // 
            this.rect1Button.Location = new System.Drawing.Point(6, 59);
            this.rect1Button.Name = "rect1Button";
            this.rect1Button.Size = new System.Drawing.Size(80, 26);
            this.rect1Button.TabIndex = 5;
            this.rect1Button.Text = "矩形";
            this.rect1Button.Click += new System.EventHandler(this.rect1Button_Click);
            // 
            // chkUseCalib
            // 
            this.chkUseCalib.AutoSize = true;
            this.chkUseCalib.Location = new System.Drawing.Point(11, 217);
            this.chkUseCalib.Name = "chkUseCalib";
            this.chkUseCalib.Size = new System.Drawing.Size(72, 16);
            this.chkUseCalib.TabIndex = 93;
            this.chkUseCalib.Text = "标定输出";
            this.chkUseCalib.UseVisualStyleBackColor = true;
            this.chkUseCalib.CheckedChanged += new System.EventHandler(this.chkUseCalib_CheckedChanged);
            // 
            // NccMatchingToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnShowRefImage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtResult);
            this.Name = "NccMatchingToolUnit";
            this.Load += new System.EventHandler(this.OCRToolUnit_Load);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.btnShowRefImage, 0);
            this.groupBoxCreateROI.ResumeLayout(false);
            this.groupBoxCreateROI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMatchesUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleExtentUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PyramidLevelUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinScoreUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPageTestModel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxCreateROI;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button TestModelButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ImageControl.HWndUnit hWndUnit1;
        private System.Windows.Forms.Button btnShowRefImage;
        private System.Windows.Forms.TabPage tabPageTestModel;
        private System.Windows.Forms.CheckBox FindAlwaysCheckBox;
        private System.Windows.Forms.ListBox testImgListBox;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button deleteAllTestImgButton;
        private System.Windows.Forms.Button deleteTestImgButton;
        private System.Windows.Forms.Button loadTestImgButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button reduceRect1Button;
        private System.Windows.Forms.Button circleButton;
        private System.Windows.Forms.Button delROIButton;
        private System.Windows.Forms.Button delAllROIButton;
        private System.Windows.Forms.Button rect2Button;
        private System.Windows.Forms.RadioButton subFromROIButton;
        private System.Windows.Forms.RadioButton addToROIButton;
        private System.Windows.Forms.Button rect1Button;
        private System.Windows.Forms.CheckBox ckbPyramidLevelAuto;
        private System.Windows.Forms.NumericUpDown PyramidLevelUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown AngleExtentUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown NumMatchesUpDown;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown MinScoreUpDown;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAccuracy;
        private System.Windows.Forms.CheckBox chkUseCalib;
    }
}
