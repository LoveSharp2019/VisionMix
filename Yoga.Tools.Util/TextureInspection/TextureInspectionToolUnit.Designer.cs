namespace Yoga.Tools.TextureInspection
{
    partial class TextureInspectionToolUnit
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
            this.tabPageTestModel = new System.Windows.Forms.TabPage();
            this.FindAlwaysCheckBox = new System.Windows.Forms.CheckBox();
            this.testImgListBox = new System.Windows.Forms.ListBox();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.deleteAllTestImgButton = new System.Windows.Forms.Button();
            this.deleteTestImgButton = new System.Windows.Forms.Button();
            this.loadTestImgButton = new System.Windows.Forms.Button();
            this.tabPageAddimage = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowRefImage = new System.Windows.Forms.Button();
            this.trainImgListBox = new System.Windows.Forms.ListBox();
            this.BtnDelAllTrainImage = new System.Windows.Forms.Button();
            this.loadTrainImg = new System.Windows.Forms.Button();
            this.deleteTrainImgButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.OpeningRadiusTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpeningRadiusReset = new System.Windows.Forms.Button();
            this.OpeningRadiusUpDown = new System.Windows.Forms.NumericUpDown();
            this.btnTrainModel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.leastAreaTrackBar = new System.Windows.Forms.TrackBar();
            this.btnLeastAreaInit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.leastAreaUpDown = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.lblResult = new System.Windows.Forms.Label();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.tabPageTestModel.SuspendLayout();
            this.tabPageAddimage.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusUpDown)).BeginInit();
            this.tabControl.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageTestModel
            // 
            this.tabPageTestModel.Controls.Add(this.FindAlwaysCheckBox);
            this.tabPageTestModel.Controls.Add(this.testImgListBox);
            this.tabPageTestModel.Controls.Add(this.TestModelButton);
            this.tabPageTestModel.Controls.Add(this.deleteAllTestImgButton);
            this.tabPageTestModel.Controls.Add(this.deleteTestImgButton);
            this.tabPageTestModel.Controls.Add(this.loadTestImgButton);
            this.tabPageTestModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageTestModel.Name = "tabPageTestModel";
            this.tabPageTestModel.Size = new System.Drawing.Size(377, 556);
            this.tabPageTestModel.TabIndex = 0;
            this.tabPageTestModel.Text = "模板测试";
            // 
            // FindAlwaysCheckBox
            // 
            this.FindAlwaysCheckBox.Location = new System.Drawing.Point(192, 228);
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
            this.testImgListBox.Size = new System.Drawing.Size(148, 220);
            this.testImgListBox.TabIndex = 67;
            this.testImgListBox.SelectedIndexChanged += new System.EventHandler(this.testImgListBox_SelectedIndexChanged);
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(192, 192);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(105, 30);
            this.TestModelButton.TabIndex = 66;
            this.TestModelButton.Text = "测试图像";
            this.TestModelButton.Click += new System.EventHandler(this.testModelButton_Click);
            // 
            // deleteAllTestImgButton
            // 
            this.deleteAllTestImgButton.Location = new System.Drawing.Point(192, 95);
            this.deleteAllTestImgButton.Name = "deleteAllTestImgButton";
            this.deleteAllTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.deleteAllTestImgButton.TabIndex = 64;
            this.deleteAllTestImgButton.Text = "删除所有";
            this.deleteAllTestImgButton.Click += new System.EventHandler(this.deleteAllTestImgButton_Click);
            // 
            // deleteTestImgButton
            // 
            this.deleteTestImgButton.Location = new System.Drawing.Point(192, 60);
            this.deleteTestImgButton.Name = "deleteTestImgButton";
            this.deleteTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.deleteTestImgButton.TabIndex = 63;
            this.deleteTestImgButton.Text = "删除图像";
            this.deleteTestImgButton.Click += new System.EventHandler(this.deleteTestImgButton_Click);
            // 
            // loadTestImgButton
            // 
            this.loadTestImgButton.Location = new System.Drawing.Point(192, 26);
            this.loadTestImgButton.Name = "loadTestImgButton";
            this.loadTestImgButton.Size = new System.Drawing.Size(105, 30);
            this.loadTestImgButton.TabIndex = 0;
            this.loadTestImgButton.Text = "加载图像";
            this.loadTestImgButton.Click += new System.EventHandler(this.loadTestImgButton_Click);
            // 
            // tabPageAddimage
            // 
            this.tabPageAddimage.Controls.Add(this.groupBoxCreateModel);
            this.tabPageAddimage.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddimage.Name = "tabPageAddimage";
            this.tabPageAddimage.Size = new System.Drawing.Size(377, 556);
            this.tabPageAddimage.TabIndex = 0;
            this.tabPageAddimage.Text = "  创建模型";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.groupBox4);
            this.groupBoxCreateModel.Controls.Add(this.groupBox5);
            this.groupBoxCreateModel.Controls.Add(this.groupBox1);
            this.groupBoxCreateModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(377, 556);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowRefImage);
            this.groupBox1.Controls.Add(this.trainImgListBox);
            this.groupBox1.Controls.Add(this.BtnDelAllTrainImage);
            this.groupBox1.Controls.Add(this.loadTrainImg);
            this.groupBox1.Controls.Add(this.deleteTrainImgButton);
            this.groupBox1.Location = new System.Drawing.Point(17, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 240);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加载训练图像";
            // 
            // btnShowRefImage
            // 
            this.btnShowRefImage.Location = new System.Drawing.Point(196, 141);
            this.btnShowRefImage.Name = "btnShowRefImage";
            this.btnShowRefImage.Size = new System.Drawing.Size(105, 30);
            this.btnShowRefImage.TabIndex = 73;
            this.btnShowRefImage.Text = "显示标准图像";
            this.btnShowRefImage.Click += new System.EventHandler(this.btnShowRefImage_Click);
            // 
            // trainImgListBox
            // 
            this.trainImgListBox.HorizontalScrollbar = true;
            this.trainImgListBox.ItemHeight = 12;
            this.trainImgListBox.Location = new System.Drawing.Point(6, 25);
            this.trainImgListBox.Name = "trainImgListBox";
            this.trainImgListBox.Size = new System.Drawing.Size(184, 196);
            this.trainImgListBox.TabIndex = 71;
            this.trainImgListBox.SelectedIndexChanged += new System.EventHandler(this.trainImgListBox_SelectedIndexChanged);
            // 
            // BtnDelAllTrainImage
            // 
            this.BtnDelAllTrainImage.Location = new System.Drawing.Point(196, 92);
            this.BtnDelAllTrainImage.Name = "BtnDelAllTrainImage";
            this.BtnDelAllTrainImage.Size = new System.Drawing.Size(105, 30);
            this.BtnDelAllTrainImage.TabIndex = 70;
            this.BtnDelAllTrainImage.Text = "删除所有";
            this.BtnDelAllTrainImage.Click += new System.EventHandler(this.BtnDelAllTrainImage_Click);
            // 
            // loadTrainImg
            // 
            this.loadTrainImg.Location = new System.Drawing.Point(196, 23);
            this.loadTrainImg.Name = "loadTrainImg";
            this.loadTrainImg.Size = new System.Drawing.Size(105, 30);
            this.loadTrainImg.TabIndex = 68;
            this.loadTrainImg.Text = "加载图像";
            this.loadTrainImg.Click += new System.EventHandler(this.loadTrainImg_Click);
            // 
            // deleteTrainImgButton
            // 
            this.deleteTrainImgButton.Location = new System.Drawing.Point(196, 57);
            this.deleteTrainImgButton.Name = "deleteTrainImgButton";
            this.deleteTrainImgButton.Size = new System.Drawing.Size(105, 30);
            this.deleteTrainImgButton.TabIndex = 69;
            this.deleteTrainImgButton.Text = "删除图像";
            this.deleteTrainImgButton.Click += new System.EventHandler(this.deleteTrainImgButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 43);
            this.label2.TabIndex = 37;
            this.label2.Text = "灰度误差允许值";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpeningRadiusTrackBar
            // 
            this.OpeningRadiusTrackBar.Location = new System.Drawing.Point(200, 22);
            this.OpeningRadiusTrackBar.Maximum = 500;
            this.OpeningRadiusTrackBar.Name = "OpeningRadiusTrackBar";
            this.OpeningRadiusTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OpeningRadiusTrackBar.Size = new System.Drawing.Size(91, 45);
            this.OpeningRadiusTrackBar.TabIndex = 52;
            this.OpeningRadiusTrackBar.TickFrequency = 10;
            this.OpeningRadiusTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.OpeningRadiusTrackBar.Value = 35;
            this.OpeningRadiusTrackBar.Scroll += new System.EventHandler(this.OpeningRadiusTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 30);
            this.label1.TabIndex = 51;
            this.label1.Text = "过滤像素半径";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOpeningRadiusReset
            // 
            this.btnOpeningRadiusReset.Location = new System.Drawing.Point(297, 20);
            this.btnOpeningRadiusReset.Name = "btnOpeningRadiusReset";
            this.btnOpeningRadiusReset.Size = new System.Drawing.Size(57, 26);
            this.btnOpeningRadiusReset.TabIndex = 54;
            this.btnOpeningRadiusReset.Text = "重置";
            this.btnOpeningRadiusReset.Click += new System.EventHandler(this.btnOpeningRadiusReset_Click);
            // 
            // OpeningRadiusUpDown
            // 
            this.OpeningRadiusUpDown.DecimalPlaces = 1;
            this.OpeningRadiusUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.OpeningRadiusUpDown.Location = new System.Drawing.Point(123, 29);
            this.OpeningRadiusUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OpeningRadiusUpDown.Name = "OpeningRadiusUpDown";
            this.OpeningRadiusUpDown.Size = new System.Drawing.Size(58, 21);
            this.OpeningRadiusUpDown.TabIndex = 53;
            this.OpeningRadiusUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OpeningRadiusUpDown.ValueChanged += new System.EventHandler(this.OpeningRadiusUpDown_ValueChanged);
            // 
            // btnTrainModel
            // 
            this.btnTrainModel.Location = new System.Drawing.Point(10, 20);
            this.btnTrainModel.Name = "btnTrainModel";
            this.btnTrainModel.Size = new System.Drawing.Size(105, 30);
            this.btnTrainModel.TabIndex = 73;
            this.btnTrainModel.Text = "训练模板";
            this.btnTrainModel.Click += new System.EventHandler(this.btnTrainModel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAddimage);
            this.tabControl.Controls.Add(this.tabPageTestModel);
            this.tabControl.Location = new System.Drawing.Point(411, 69);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(385, 582);
            this.tabControl.TabIndex = 23;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnTrainModel);
            this.groupBox5.Location = new System.Drawing.Point(17, 270);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(318, 56);
            this.groupBox5.TabIndex = 77;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "模板训练";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.leastAreaTrackBar);
            this.groupBox4.Controls.Add(this.btnLeastAreaInit);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.leastAreaUpDown);
            this.groupBox4.Controls.Add(this.OpeningRadiusTrackBar);
            this.groupBox4.Controls.Add(this.btnOpeningRadiusReset);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.OpeningRadiusUpDown);
            this.groupBox4.Location = new System.Drawing.Point(6, 347);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(361, 106);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "结果过滤";
            // 
            // leastAreaTrackBar
            // 
            this.leastAreaTrackBar.Location = new System.Drawing.Point(200, 60);
            this.leastAreaTrackBar.Maximum = 500;
            this.leastAreaTrackBar.Name = "leastAreaTrackBar";
            this.leastAreaTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.leastAreaTrackBar.Size = new System.Drawing.Size(91, 45);
            this.leastAreaTrackBar.TabIndex = 56;
            this.leastAreaTrackBar.TickFrequency = 10;
            this.leastAreaTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.leastAreaTrackBar.Value = 35;
            this.leastAreaTrackBar.Scroll += new System.EventHandler(this.leastAreaTrackBar_Scroll);
            // 
            // btnLeastAreaInit
            // 
            this.btnLeastAreaInit.Location = new System.Drawing.Point(297, 56);
            this.btnLeastAreaInit.Name = "btnLeastAreaInit";
            this.btnLeastAreaInit.Size = new System.Drawing.Size(57, 26);
            this.btnLeastAreaInit.TabIndex = 58;
            this.btnLeastAreaInit.Text = "重置";
            this.btnLeastAreaInit.Click += new System.EventHandler(this.btnLeastAreaInit_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 30);
            this.label3.TabIndex = 55;
            this.label3.Text = "最小面积";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leastAreaUpDown
            // 
            this.leastAreaUpDown.DecimalPlaces = 1;
            this.leastAreaUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.leastAreaUpDown.Location = new System.Drawing.Point(123, 64);
            this.leastAreaUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.leastAreaUpDown.Name = "leastAreaUpDown";
            this.leastAreaUpDown.Size = new System.Drawing.Size(58, 21);
            this.leastAreaUpDown.TabIndex = 57;
            this.leastAreaUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.leastAreaUpDown.ValueChanged += new System.EventHandler(this.leastAreaUpDown_ValueChanged);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.White;
            this.lblResult.Location = new System.Drawing.Point(3, 361);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(185, 124);
            this.lblResult.TabIndex = 74;
            this.lblResult.Text = "训练结果: ";
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(0, 69);
            this.hWndUnit1.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(406, 267);
            this.hWndUnit1.TabIndex = 75;
            // 
            // TextureInspectionToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.tabControl);
            this.Name = "TextureInspectionToolUnit";
            this.Load += new System.EventHandler(this.PrintCheckToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.tabPageTestModel.ResumeLayout(false);
            this.tabPageAddimage.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpeningRadiusUpDown)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leastAreaUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPageTestModel;
        private System.Windows.Forms.ListBox testImgListBox;
        private System.Windows.Forms.Button TestModelButton;
        private System.Windows.Forms.Button deleteAllTestImgButton;
        private System.Windows.Forms.Button deleteTestImgButton;
        private System.Windows.Forms.Button loadTestImgButton;
        private System.Windows.Forms.TabPage tabPageAddimage;
        private System.Windows.Forms.GroupBox groupBoxCreateModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox trainImgListBox;
        private System.Windows.Forms.Button BtnDelAllTrainImage;
        private System.Windows.Forms.Button loadTrainImg;
        private System.Windows.Forms.Button deleteTrainImgButton;
        private System.Windows.Forms.CheckBox FindAlwaysCheckBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnShowRefImage;
        private System.Windows.Forms.Button btnTrainModel;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.TrackBar OpeningRadiusTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpeningRadiusReset;
        private System.Windows.Forms.NumericUpDown OpeningRadiusUpDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar leastAreaTrackBar;
        private System.Windows.Forms.Button btnLeastAreaInit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown leastAreaUpDown;
        private ImageControl.HWndUnit hWndUnit1;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}
