namespace Yoga.Tools.PretreatImage
{
    partial class PretreatImageToolUnit
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
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.reduceRect1Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mat2DMangerUnit1 = new Yoga.Tools.Mat2DMangerUnit();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCreateModel = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxSobelAmp = new System.Windows.Forms.GroupBox();
            this.MetricBoxSobelAmpFilterType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudSobelAmpSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.chkSobelAmp = new System.Windows.Forms.CheckBox();
            this.groupBoxMedian = new System.Windows.Forms.GroupBox();
            this.UpDownMedianRadius = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.chkUseMedian = new System.Windows.Forms.CheckBox();
            this.groupBoxEmphasize = new System.Windows.Forms.GroupBox();
            this.UpDownFactor = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.UpDownMaskWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.UpDownMaskHeight = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkEmphasize = new System.Windows.Forms.CheckBox();
            this.groupBoxScaleImage = new System.Windows.Forms.GroupBox();
            this.ScaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.OffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkScaleImage = new System.Windows.Forms.CheckBox();
            this.btnDeleteSearchRegion = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.chkShowResultImage = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabPageCreateModel.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxSobelAmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSobelAmpSize)).BeginInit();
            this.groupBoxMedian.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMedianRadius)).BeginInit();
            this.groupBoxEmphasize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskHeight)).BeginInit();
            this.groupBoxScaleImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // reduceRect1Button
            // 
            this.reduceRect1Button.Location = new System.Drawing.Point(230, 407);
            this.reduceRect1Button.Name = "reduceRect1Button";
            this.reduceRect1Button.Size = new System.Drawing.Size(80, 26);
            this.reduceRect1Button.TabIndex = 13;
            this.reduceRect1Button.Text = "画修改框";
            this.reduceRect1Button.Click += new System.EventHandler(this.reduceRect1Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 82;
            this.label2.Text = "字符长度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 81;
            this.label1.Text = "字符起始位";
            // 
            // mat2DMangerUnit1
            // 
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(17, 359);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 83;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCreateModel);
            this.tabControl.Location = new System.Drawing.Point(390, 66);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(399, 511);
            this.tabControl.TabIndex = 76;
            // 
            // tabPageCreateModel
            // 
            this.tabPageCreateModel.Controls.Add(this.groupBoxCreateModel);
            this.tabPageCreateModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateModel.Name = "tabPageCreateModel";
            this.tabPageCreateModel.Size = new System.Drawing.Size(391, 485);
            this.tabPageCreateModel.TabIndex = 0;
            this.tabPageCreateModel.Text = "  参数设置";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.chkShowResultImage);
            this.groupBoxCreateModel.Controls.Add(this.groupBox2);
            this.groupBoxCreateModel.Controls.Add(this.btnDeleteSearchRegion);
            this.groupBoxCreateModel.Controls.Add(this.mat2DMangerUnit1);
            this.groupBoxCreateModel.Controls.Add(this.reduceRect1Button);
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(388, 485);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBoxSobelAmp);
            this.groupBox2.Controls.Add(this.chkSobelAmp);
            this.groupBox2.Controls.Add(this.groupBoxMedian);
            this.groupBox2.Controls.Add(this.chkUseMedian);
            this.groupBox2.Controls.Add(this.groupBoxEmphasize);
            this.groupBox2.Controls.Add(this.chkEmphasize);
            this.groupBox2.Controls.Add(this.groupBoxScaleImage);
            this.groupBox2.Controls.Add(this.chkScaleImage);
            this.groupBox2.Location = new System.Drawing.Point(17, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 333);
            this.groupBox2.TabIndex = 87;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图像预处理";
            // 
            // groupBoxSobelAmp
            // 
            this.groupBoxSobelAmp.Controls.Add(this.MetricBoxSobelAmpFilterType);
            this.groupBoxSobelAmp.Controls.Add(this.label8);
            this.groupBoxSobelAmp.Controls.Add(this.nudSobelAmpSize);
            this.groupBoxSobelAmp.Controls.Add(this.label9);
            this.groupBoxSobelAmp.Location = new System.Drawing.Point(183, 213);
            this.groupBoxSobelAmp.Name = "groupBoxSobelAmp";
            this.groupBoxSobelAmp.Size = new System.Drawing.Size(142, 80);
            this.groupBoxSobelAmp.TabIndex = 88;
            this.groupBoxSobelAmp.TabStop = false;
            this.groupBoxSobelAmp.Text = "边缘提取参数";
            // 
            // MetricBoxSobelAmpFilterType
            // 
            this.MetricBoxSobelAmpFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MetricBoxSobelAmpFilterType.Items.AddRange(new object[] {
            "sum_abs",
            "sum_abs_binomial",
            "sum_sqrt",
            "sum_sqrt_binomial",
            "thin_max_abs",
            "thin_max_abs_binomial",
            "thin_sum_abs",
            "thin_sum_abs_binomial",
            "x",
            "x_binomial",
            "y",
            "y_binomial"});
            this.MetricBoxSobelAmpFilterType.Location = new System.Drawing.Point(65, 17);
            this.MetricBoxSobelAmpFilterType.Name = "MetricBoxSobelAmpFilterType";
            this.MetricBoxSobelAmpFilterType.Size = new System.Drawing.Size(71, 20);
            this.MetricBoxSobelAmpFilterType.TabIndex = 74;
            this.MetricBoxSobelAmpFilterType.SelectedIndexChanged += new System.EventHandler(this.MetricBoxSobelAmpFilterType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 43;
            this.label8.Text = "提取大小";
            // 
            // nudSobelAmpSize
            // 
            this.nudSobelAmpSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudSobelAmpSize.Location = new System.Drawing.Point(68, 47);
            this.nudSobelAmpSize.Maximum = new decimal(new int[] {
            39,
            0,
            0,
            0});
            this.nudSobelAmpSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSobelAmpSize.Name = "nudSobelAmpSize";
            this.nudSobelAmpSize.Size = new System.Drawing.Size(58, 21);
            this.nudSobelAmpSize.TabIndex = 41;
            this.nudSobelAmpSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSobelAmpSize.ValueChanged += new System.EventHandler(this.nudSobelAmpSize_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 42;
            this.label9.Text = "提取类型";
            // 
            // chkSobelAmp
            // 
            this.chkSobelAmp.AutoSize = true;
            this.chkSobelAmp.Location = new System.Drawing.Point(193, 191);
            this.chkSobelAmp.Name = "chkSobelAmp";
            this.chkSobelAmp.Size = new System.Drawing.Size(72, 16);
            this.chkSobelAmp.TabIndex = 90;
            this.chkSobelAmp.Text = "边缘提取";
            this.chkSobelAmp.UseVisualStyleBackColor = true;
            this.chkSobelAmp.CheckedChanged += new System.EventHandler(this.chkSobelAmp_CheckedChanged);
            // 
            // groupBoxMedian
            // 
            this.groupBoxMedian.Controls.Add(this.UpDownMedianRadius);
            this.groupBoxMedian.Controls.Add(this.label10);
            this.groupBoxMedian.Location = new System.Drawing.Point(164, 42);
            this.groupBoxMedian.Name = "groupBoxMedian";
            this.groupBoxMedian.Size = new System.Drawing.Size(142, 65);
            this.groupBoxMedian.TabIndex = 89;
            this.groupBoxMedian.TabStop = false;
            this.groupBoxMedian.Text = "中值滤波参数";
            // 
            // UpDownMedianRadius
            // 
            this.UpDownMedianRadius.Location = new System.Drawing.Point(71, 20);
            this.UpDownMedianRadius.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.UpDownMedianRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownMedianRadius.Name = "UpDownMedianRadius";
            this.UpDownMedianRadius.Size = new System.Drawing.Size(58, 21);
            this.UpDownMedianRadius.TabIndex = 40;
            this.UpDownMedianRadius.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UpDownMedianRadius.ValueChanged += new System.EventHandler(this.UpDownMedianRadius_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 42;
            this.label10.Text = "滤波半径";
            // 
            // chkUseMedian
            // 
            this.chkUseMedian.AutoSize = true;
            this.chkUseMedian.Location = new System.Drawing.Point(183, 20);
            this.chkUseMedian.Name = "chkUseMedian";
            this.chkUseMedian.Size = new System.Drawing.Size(72, 16);
            this.chkUseMedian.TabIndex = 89;
            this.chkUseMedian.Text = "中值滤波";
            this.chkUseMedian.UseVisualStyleBackColor = true;
            this.chkUseMedian.CheckedChanged += new System.EventHandler(this.chkUseMedian_CheckedChanged);
            // 
            // groupBoxEmphasize
            // 
            this.groupBoxEmphasize.Controls.Add(this.UpDownFactor);
            this.groupBoxEmphasize.Controls.Add(this.label7);
            this.groupBoxEmphasize.Controls.Add(this.UpDownMaskWidth);
            this.groupBoxEmphasize.Controls.Add(this.label5);
            this.groupBoxEmphasize.Controls.Add(this.UpDownMaskHeight);
            this.groupBoxEmphasize.Controls.Add(this.label6);
            this.groupBoxEmphasize.Location = new System.Drawing.Point(9, 42);
            this.groupBoxEmphasize.Name = "groupBoxEmphasize";
            this.groupBoxEmphasize.Size = new System.Drawing.Size(142, 131);
            this.groupBoxEmphasize.TabIndex = 88;
            this.groupBoxEmphasize.TabStop = false;
            this.groupBoxEmphasize.Text = "图像锐化参数";
            // 
            // UpDownFactor
            // 
            this.UpDownFactor.DecimalPlaces = 1;
            this.UpDownFactor.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.UpDownFactor.Location = new System.Drawing.Point(71, 82);
            this.UpDownFactor.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.UpDownFactor.Name = "UpDownFactor";
            this.UpDownFactor.Size = new System.Drawing.Size(58, 21);
            this.UpDownFactor.TabIndex = 45;
            this.UpDownFactor.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.UpDownFactor.ValueChanged += new System.EventHandler(this.UpDownFactor_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "增强因子";
            // 
            // UpDownMaskWidth
            // 
            this.UpDownMaskWidth.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UpDownMaskWidth.Location = new System.Drawing.Point(71, 20);
            this.UpDownMaskWidth.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.UpDownMaskWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UpDownMaskWidth.Name = "UpDownMaskWidth";
            this.UpDownMaskWidth.Size = new System.Drawing.Size(58, 21);
            this.UpDownMaskWidth.TabIndex = 40;
            this.UpDownMaskWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UpDownMaskWidth.ValueChanged += new System.EventHandler(this.UpDownMaskWidth_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 43;
            this.label5.Text = "滤波高度";
            // 
            // UpDownMaskHeight
            // 
            this.UpDownMaskHeight.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UpDownMaskHeight.Location = new System.Drawing.Point(71, 51);
            this.UpDownMaskHeight.Maximum = new decimal(new int[] {
            201,
            0,
            0,
            0});
            this.UpDownMaskHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UpDownMaskHeight.Name = "UpDownMaskHeight";
            this.UpDownMaskHeight.Size = new System.Drawing.Size(58, 21);
            this.UpDownMaskHeight.TabIndex = 41;
            this.UpDownMaskHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.UpDownMaskHeight.ValueChanged += new System.EventHandler(this.UpDownMaskHeight_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "滤波宽度";
            // 
            // chkEmphasize
            // 
            this.chkEmphasize.AutoSize = true;
            this.chkEmphasize.Location = new System.Drawing.Point(9, 20);
            this.chkEmphasize.Name = "chkEmphasize";
            this.chkEmphasize.Size = new System.Drawing.Size(72, 16);
            this.chkEmphasize.TabIndex = 88;
            this.chkEmphasize.Text = "图像锐化";
            this.chkEmphasize.UseVisualStyleBackColor = true;
            this.chkEmphasize.CheckedChanged += new System.EventHandler(this.chkEmphasize_CheckedChanged);
            // 
            // groupBoxScaleImage
            // 
            this.groupBoxScaleImage.Controls.Add(this.ScaleUpDown);
            this.groupBoxScaleImage.Controls.Add(this.label3);
            this.groupBoxScaleImage.Controls.Add(this.OffsetUpDown);
            this.groupBoxScaleImage.Controls.Add(this.label4);
            this.groupBoxScaleImage.Location = new System.Drawing.Point(9, 213);
            this.groupBoxScaleImage.Name = "groupBoxScaleImage";
            this.groupBoxScaleImage.Size = new System.Drawing.Size(142, 80);
            this.groupBoxScaleImage.TabIndex = 87;
            this.groupBoxScaleImage.TabStop = false;
            this.groupBoxScaleImage.Text = "比例增强参数";
            // 
            // ScaleUpDown
            // 
            this.ScaleUpDown.DecimalPlaces = 1;
            this.ScaleUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ScaleUpDown.Location = new System.Drawing.Point(68, 20);
            this.ScaleUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.ScaleUpDown.Name = "ScaleUpDown";
            this.ScaleUpDown.Size = new System.Drawing.Size(58, 21);
            this.ScaleUpDown.TabIndex = 40;
            this.ScaleUpDown.ValueChanged += new System.EventHandler(this.ScaleUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "偏移";
            // 
            // OffsetUpDown
            // 
            this.OffsetUpDown.Location = new System.Drawing.Point(68, 47);
            this.OffsetUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.OffsetUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.OffsetUpDown.Name = "OffsetUpDown";
            this.OffsetUpDown.Size = new System.Drawing.Size(58, 21);
            this.OffsetUpDown.TabIndex = 41;
            this.OffsetUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.OffsetUpDown.ValueChanged += new System.EventHandler(this.OffsetUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "比例";
            // 
            // chkScaleImage
            // 
            this.chkScaleImage.AutoSize = true;
            this.chkScaleImage.Location = new System.Drawing.Point(20, 191);
            this.chkScaleImage.Name = "chkScaleImage";
            this.chkScaleImage.Size = new System.Drawing.Size(72, 16);
            this.chkScaleImage.TabIndex = 12;
            this.chkScaleImage.Text = "比例增强";
            this.chkScaleImage.UseVisualStyleBackColor = true;
            this.chkScaleImage.CheckedChanged += new System.EventHandler(this.chkScaleImage_CheckedChanged);
            // 
            // btnDeleteSearchRegion
            // 
            this.btnDeleteSearchRegion.Location = new System.Drawing.Point(230, 439);
            this.btnDeleteSearchRegion.Name = "btnDeleteSearchRegion";
            this.btnDeleteSearchRegion.Size = new System.Drawing.Size(80, 26);
            this.btnDeleteSearchRegion.TabIndex = 86;
            this.btnDeleteSearchRegion.Text = "删除修改框";
            this.btnDeleteSearchRegion.Click += new System.EventHandler(this.btnDeleteSearchRegion_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(15, 386);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(280, 182);
            this.txtResult.TabIndex = 80;
            this.txtResult.Text = "结果";
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(18, 66);
            this.hWndUnit1.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(366, 297);
            this.hWndUnit1.TabIndex = 81;
            // 
            // chkShowResultImage
            // 
            this.chkShowResultImage.AutoSize = true;
            this.chkShowResultImage.Location = new System.Drawing.Point(231, 376);
            this.chkShowResultImage.Name = "chkShowResultImage";
            this.chkShowResultImage.Size = new System.Drawing.Size(96, 16);
            this.chkShowResultImage.TabIndex = 91;
            this.chkShowResultImage.Text = "显示结果图像";
            this.chkShowResultImage.UseVisualStyleBackColor = true;
            this.chkShowResultImage.CheckedChanged += new System.EventHandler(this.chkShowResultImage_CheckedChanged);
            // 
            // PretreatImageToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.tabControl);
            this.Name = "PretreatImageToolUnit";
            this.Load += new System.EventHandler(this.SacleImageToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxSobelAmp.ResumeLayout(false);
            this.groupBoxSobelAmp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSobelAmpSize)).EndInit();
            this.groupBoxMedian.ResumeLayout(false);
            this.groupBoxMedian.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMedianRadius)).EndInit();
            this.groupBoxEmphasize.ResumeLayout(false);
            this.groupBoxEmphasize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownMaskHeight)).EndInit();
            this.groupBoxScaleImage.ResumeLayout(false);
            this.groupBoxScaleImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button reduceRect1Button;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCreateModel;
        private System.Windows.Forms.GroupBox groupBoxCreateModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResult;
        private ImageControl.HWndUnit hWndUnit1;
        private Mat2DMangerUnit mat2DMangerUnit1;
        private System.Windows.Forms.Button btnDeleteSearchRegion;
        private System.Windows.Forms.GroupBox groupBoxScaleImage;
        private System.Windows.Forms.NumericUpDown ScaleUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown OffsetUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkScaleImage;
        private System.Windows.Forms.GroupBox groupBoxEmphasize;
        private System.Windows.Forms.NumericUpDown UpDownFactor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown UpDownMaskWidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown UpDownMaskHeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkEmphasize;
        private System.Windows.Forms.CheckBox chkUseMedian;
        private System.Windows.Forms.GroupBox groupBoxMedian;
        private System.Windows.Forms.NumericUpDown UpDownMedianRadius;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBoxSobelAmp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudSobelAmpSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkSobelAmp;
        private System.Windows.Forms.ComboBox MetricBoxSobelAmpFilterType;
        private System.Windows.Forms.CheckBox chkShowResultImage;
    }
}
