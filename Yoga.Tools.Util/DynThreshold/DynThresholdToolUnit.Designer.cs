namespace Yoga.Tools.DynThreshold
{
    partial class DynThresholdToolUnit
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
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpDownMaskHeight = new System.Windows.Forms.NumericUpDown();
            this.UpDownMaskWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.UpDownOffset = new System.Windows.Forms.NumericUpDown();
            this.cmbLightDark = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.leastAreaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ClosingRadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.OpeningRadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBoxCreateROI = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.circleButton = new System.Windows.Forms.Button();
            this.delROIButton = new System.Windows.Forms.Button();
            this.delAllROIButton = new System.Windows.Forms.Button();
            this.rect2Button = new System.Windows.Forms.Button();
            this.subFromROIButton = new System.Windows.Forms.RadioButton();
            this.addToROIButton = new System.Windows.Forms.RadioButton();
            this.rect1Button = new System.Windows.Forms.Button();
            this.mat2DMangerUnit1 = new Yoga.Tools.Mat2DMangerUnit();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownOffset)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClosingRadiusUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusUpDown)).BeginInit();
            this.groupBoxCreateROI.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(5, 68);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(394, 220);
            this.hWndUnit1.TabIndex = 81;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(566, 417);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(105, 30);
            this.btnTest.TabIndex = 83;
            this.btnTest.Text = "测试图像";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 88;
            this.label2.Text = "滤波高度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 87;
            this.label1.Text = "滤波宽度";
            // 
            // UpDownMaskHeight
            // 
            this.UpDownMaskHeight.Location = new System.Drawing.Point(86, 55);
            this.UpDownMaskHeight.Maximum = new decimal(new int[] {
            501,
            0,
            0,
            0});
            this.UpDownMaskHeight.Name = "UpDownMaskHeight";
            this.UpDownMaskHeight.Size = new System.Drawing.Size(120, 21);
            this.UpDownMaskHeight.TabIndex = 86;
            this.UpDownMaskHeight.ValueChanged += new System.EventHandler(this.UpDownMaskHeight_ValueChanged);
            // 
            // UpDownMaskWidth
            // 
            this.UpDownMaskWidth.Location = new System.Drawing.Point(86, 17);
            this.UpDownMaskWidth.Maximum = new decimal(new int[] {
            501,
            0,
            0,
            0});
            this.UpDownMaskWidth.Name = "UpDownMaskWidth";
            this.UpDownMaskWidth.Size = new System.Drawing.Size(120, 21);
            this.UpDownMaskWidth.TabIndex = 85;
            this.UpDownMaskWidth.ValueChanged += new System.EventHandler(this.UpDownMaskWidth_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 90;
            this.label3.Text = "偏倚范围";
            // 
            // UpDownOffset
            // 
            this.UpDownOffset.Location = new System.Drawing.Point(86, 92);
            this.UpDownOffset.Name = "UpDownOffset";
            this.UpDownOffset.Size = new System.Drawing.Size(120, 21);
            this.UpDownOffset.TabIndex = 89;
            this.UpDownOffset.ValueChanged += new System.EventHandler(this.UpDownOffset_ValueChanged);
            // 
            // cmbLightDark
            // 
            this.cmbLightDark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLightDark.Items.AddRange(new object[] {
            "dark",
            "light",
            "equal",
            "not_equal"});
            this.cmbLightDark.Location = new System.Drawing.Point(86, 137);
            this.cmbLightDark.Name = "cmbLightDark";
            this.cmbLightDark.Size = new System.Drawing.Size(127, 20);
            this.cmbLightDark.TabIndex = 91;
            this.cmbLightDark.SelectedIndexChanged += new System.EventHandler(this.cmbLightDark_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 92;
            this.label4.Text = "瑕疵选择";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.leastAreaUpDown);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.ClosingRadiusUpDown);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.OpeningRadiusUpDown);
            this.groupBox4.Location = new System.Drawing.Point(432, 275);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(258, 136);
            this.groupBox4.TabIndex = 93;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "结果过滤";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 30);
            this.label7.TabIndex = 58;
            this.label7.Text = "最小面积";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leastAreaUpDown
            // 
            this.leastAreaUpDown.DecimalPlaces = 1;
            this.leastAreaUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.leastAreaUpDown.Location = new System.Drawing.Point(121, 93);
            this.leastAreaUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.leastAreaUpDown.Name = "leastAreaUpDown";
            this.leastAreaUpDown.Size = new System.Drawing.Size(118, 21);
            this.leastAreaUpDown.TabIndex = 59;
            this.leastAreaUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.leastAreaUpDown.ValueChanged += new System.EventHandler(this.leastAreaUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 30);
            this.label5.TabIndex = 55;
            this.label5.Text = "闭运算半径";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ClosingRadiusUpDown
            // 
            this.ClosingRadiusUpDown.DecimalPlaces = 1;
            this.ClosingRadiusUpDown.Location = new System.Drawing.Point(121, 30);
            this.ClosingRadiusUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ClosingRadiusUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.ClosingRadiusUpDown.Name = "ClosingRadiusUpDown";
            this.ClosingRadiusUpDown.Size = new System.Drawing.Size(118, 21);
            this.ClosingRadiusUpDown.TabIndex = 57;
            this.ClosingRadiusUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ClosingRadiusUpDown.ValueChanged += new System.EventHandler(this.ClosingRadiusUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 30);
            this.label6.TabIndex = 51;
            this.label6.Text = "开运算半径";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpeningRadiusUpDown
            // 
            this.OpeningRadiusUpDown.DecimalPlaces = 1;
            this.OpeningRadiusUpDown.Location = new System.Drawing.Point(121, 62);
            this.OpeningRadiusUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OpeningRadiusUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.OpeningRadiusUpDown.Name = "OpeningRadiusUpDown";
            this.OpeningRadiusUpDown.Size = new System.Drawing.Size(118, 21);
            this.OpeningRadiusUpDown.TabIndex = 53;
            this.OpeningRadiusUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OpeningRadiusUpDown.ValueChanged += new System.EventHandler(this.OpeningRadiusUpDown_ValueChanged);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.White;
            this.lblResult.Location = new System.Drawing.Point(17, 317);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(312, 27);
            this.lblResult.TabIndex = 94;
            this.lblResult.Text = "结果: ";
            // 
            // groupBoxCreateROI
            // 
            this.groupBoxCreateROI.Controls.Add(this.button1);
            this.groupBoxCreateROI.Controls.Add(this.circleButton);
            this.groupBoxCreateROI.Controls.Add(this.delROIButton);
            this.groupBoxCreateROI.Controls.Add(this.delAllROIButton);
            this.groupBoxCreateROI.Controls.Add(this.rect2Button);
            this.groupBoxCreateROI.Controls.Add(this.subFromROIButton);
            this.groupBoxCreateROI.Controls.Add(this.addToROIButton);
            this.groupBoxCreateROI.Controls.Add(this.rect1Button);
            this.groupBoxCreateROI.Location = new System.Drawing.Point(212, 361);
            this.groupBoxCreateROI.Name = "groupBoxCreateROI";
            this.groupBoxCreateROI.Size = new System.Drawing.Size(202, 155);
            this.groupBoxCreateROI.TabIndex = 95;
            this.groupBoxCreateROI.TabStop = false;
            this.groupBoxCreateROI.Text = "创建ROI";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "搜索矩形";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // mat2DMangerUnit1
            // 
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(6, 361);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 96;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.UpDownOffset);
            this.panel1.Controls.Add(this.UpDownMaskWidth);
            this.panel1.Controls.Add(this.UpDownMaskHeight);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbLightDark);
            this.panel1.Location = new System.Drawing.Point(422, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 182);
            this.panel1.TabIndex = 97;
            // 
            // DynThresholdToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mat2DMangerUnit1);
            this.Controls.Add(this.groupBoxCreateROI);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.hWndUnit1);
            this.Name = "DynThresholdToolUnit";
            this.Load += new System.EventHandler(this.DynThresholdToolUnit_Load);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.btnTest, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.Controls.SetChildIndex(this.groupBoxCreateROI, 0);
            this.Controls.SetChildIndex(this.mat2DMangerUnit1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownOffset)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClosingRadiusUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusUpDown)).EndInit();
            this.groupBoxCreateROI.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ImageControl.HWndUnit hWndUnit1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UpDownMaskHeight;
        private System.Windows.Forms.NumericUpDown UpDownMaskWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown UpDownOffset;
        private System.Windows.Forms.ComboBox cmbLightDark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ClosingRadiusUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown OpeningRadiusUpDown;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.GroupBox groupBoxCreateROI;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button circleButton;
        private System.Windows.Forms.Button delROIButton;
        private System.Windows.Forms.Button delAllROIButton;
        private System.Windows.Forms.Button rect2Button;
        private System.Windows.Forms.RadioButton subFromROIButton;
        private System.Windows.Forms.RadioButton addToROIButton;
        private System.Windows.Forms.Button rect1Button;
        private Mat2DMangerUnit mat2DMangerUnit1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown leastAreaUpDown;
        private System.Windows.Forms.Panel panel1;
    }
}
