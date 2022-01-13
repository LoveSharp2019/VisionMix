namespace Yoga.Tools.CreateImage
{
    partial class CreateImageUnit
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
            this.label1 = new System.Windows.Forms.Label();
            this.UpDownCameraIndex = new System.Windows.Forms.NumericUpDown();
            this.btnGetImage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOffLinePath = new System.Windows.Forms.TextBox();
            this.btnGetImagePath = new System.Windows.Forms.Button();
            this.chkOffLine = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetRefImage = new System.Windows.Forms.Button();
            this.btnShowRefImage = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCameraIndex)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(3, 79);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(346, 271);
            this.hWndUnit1.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 83;
            this.label1.Text = "相机编号";
            // 
            // UpDownCameraIndex
            // 
            this.UpDownCameraIndex.Enabled = false;
            this.UpDownCameraIndex.Location = new System.Drawing.Point(91, 15);
            this.UpDownCameraIndex.Name = "UpDownCameraIndex";
            this.UpDownCameraIndex.Size = new System.Drawing.Size(120, 21);
            this.UpDownCameraIndex.TabIndex = 82;
            this.UpDownCameraIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownCameraIndex.ValueChanged += new System.EventHandler(this.UpDownCameraIndex_ValueChanged);
            // 
            // btnGetImage
            // 
            this.btnGetImage.Location = new System.Drawing.Point(15, 30);
            this.btnGetImage.Name = "btnGetImage";
            this.btnGetImage.Size = new System.Drawing.Size(75, 23);
            this.btnGetImage.TabIndex = 84;
            this.btnGetImage.Text = "采集图像";
            this.btnGetImage.UseVisualStyleBackColor = true;
            this.btnGetImage.Click += new System.EventHandler(this.btnGetImage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOffLinePath);
            this.groupBox1.Controls.Add(this.btnGetImagePath);
            this.groupBox1.Controls.Add(this.chkOffLine);
            this.groupBox1.Location = new System.Drawing.Point(19, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 100);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "离线模式";
            // 
            // txtOffLinePath
            // 
            this.txtOffLinePath.Location = new System.Drawing.Point(7, 56);
            this.txtOffLinePath.Name = "txtOffLinePath";
            this.txtOffLinePath.ReadOnly = true;
            this.txtOffLinePath.Size = new System.Drawing.Size(334, 21);
            this.txtOffLinePath.TabIndex = 85;
            // 
            // btnGetImagePath
            // 
            this.btnGetImagePath.Location = new System.Drawing.Point(307, 29);
            this.btnGetImagePath.Name = "btnGetImagePath";
            this.btnGetImagePath.Size = new System.Drawing.Size(42, 23);
            this.btnGetImagePath.TabIndex = 1;
            this.btnGetImagePath.Text = "...";
            this.btnGetImagePath.UseVisualStyleBackColor = true;
            this.btnGetImagePath.Click += new System.EventHandler(this.btnGetImagePath_Click);
            // 
            // chkOffLine
            // 
            this.chkOffLine.AutoSize = true;
            this.chkOffLine.Location = new System.Drawing.Point(7, 21);
            this.chkOffLine.Name = "chkOffLine";
            this.chkOffLine.Size = new System.Drawing.Size(72, 16);
            this.chkOffLine.TabIndex = 0;
            this.chkOffLine.Text = "离线使能";
            this.chkOffLine.UseVisualStyleBackColor = true;
            this.chkOffLine.CheckedChanged += new System.EventHandler(this.chkOffLine_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 84;
            this.label2.Text = "文件夹";
            // 
            // btnSetRefImage
            // 
            this.btnSetRefImage.Location = new System.Drawing.Point(107, 30);
            this.btnSetRefImage.Name = "btnSetRefImage";
            this.btnSetRefImage.Size = new System.Drawing.Size(75, 23);
            this.btnSetRefImage.TabIndex = 86;
            this.btnSetRefImage.Text = "设为模板";
            this.btnSetRefImage.UseVisualStyleBackColor = true;
            this.btnSetRefImage.Click += new System.EventHandler(this.btnSetRefImage_Click);
            // 
            // btnShowRefImage
            // 
            this.btnShowRefImage.Location = new System.Drawing.Point(203, 30);
            this.btnShowRefImage.Name = "btnShowRefImage";
            this.btnShowRefImage.Size = new System.Drawing.Size(75, 23);
            this.btnShowRefImage.TabIndex = 87;
            this.btnShowRefImage.Text = "显示模板";
            this.btnShowRefImage.UseVisualStyleBackColor = true;
            this.btnShowRefImage.Click += new System.EventHandler(this.btnShowRefImage_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetImage);
            this.groupBox2.Controls.Add(this.btnShowRefImage);
            this.groupBox2.Controls.Add(this.btnSetRefImage);
            this.groupBox2.Location = new System.Drawing.Point(19, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 100);
            this.groupBox2.TabIndex = 88;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图像操作";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(366, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 506);
            this.tabControl1.TabIndex = 89;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.UpDownCameraIndex);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(423, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "模板设置";
            // 
            // CreateImageUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.hWndUnit1);
            this.Name = "CreateImageUnit";
            this.Load += new System.EventHandler(this.CreateImageUnit_Load);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCameraIndex)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ImageControl.HWndUnit hWndUnit1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UpDownCameraIndex;
        private System.Windows.Forms.Button btnGetImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOffLine;
        private System.Windows.Forms.Button btnGetImagePath;
        private System.Windows.Forms.TextBox txtOffLinePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetRefImage;
        private System.Windows.Forms.Button btnShowRefImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}
