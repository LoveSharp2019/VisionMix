namespace Yoga.Tools.Calib9Point
{
    partial class Calib9PointToolUnit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.btnSetModelImage = new System.Windows.Forms.Button();
            this.btnMatchingSetting = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnCalib = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.dgvRealPos = new System.Windows.Forms.DataGridView();
            this.saveResultDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnMatchingTest = new System.Windows.Forms.Button();
            this.groupBoxPointPos = new System.Windows.Forms.GroupBox();
            this.rdbtn9 = new System.Windows.Forms.RadioButton();
            this.rdbtn8 = new System.Windows.Forms.RadioButton();
            this.rdbtn7 = new System.Windows.Forms.RadioButton();
            this.rdbtn6 = new System.Windows.Forms.RadioButton();
            this.rdbtn5 = new System.Windows.Forms.RadioButton();
            this.rdbtn4 = new System.Windows.Forms.RadioButton();
            this.rdbtn3 = new System.Windows.Forms.RadioButton();
            this.rdbtn2 = new System.Windows.Forms.RadioButton();
            this.rdbtn1 = new System.Windows.Forms.RadioButton();
            this.btnPixImput = new System.Windows.Forms.Button();
            this.btnVerification = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealPos)).BeginInit();
            this.groupBoxPointPos.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(5, 53);
            this.hWndUnit1.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(653, 447);
            this.hWndUnit1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLoadImg);
            this.groupBox3.Controls.Add(this.btnSetModelImage);
            this.groupBox3.Controls.Add(this.btnMatchingSetting);
            this.groupBox3.Location = new System.Drawing.Point(684, 47);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(395, 48);
            this.groupBox3.TabIndex = 79;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.Location = new System.Drawing.Point(18, 14);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(80, 28);
            this.btnLoadImg.TabIndex = 4;
            this.btnLoadImg.Text = "采集图像";
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // btnSetModelImage
            // 
            this.btnSetModelImage.Location = new System.Drawing.Point(131, 13);
            this.btnSetModelImage.Name = "btnSetModelImage";
            this.btnSetModelImage.Size = new System.Drawing.Size(105, 30);
            this.btnSetModelImage.TabIndex = 73;
            this.btnSetModelImage.Text = "设为模板";
            this.btnSetModelImage.Click += new System.EventHandler(this.btnSetModelImage_Click);
            // 
            // btnMatchingSetting
            // 
            this.btnMatchingSetting.Location = new System.Drawing.Point(267, 13);
            this.btnMatchingSetting.Name = "btnMatchingSetting";
            this.btnMatchingSetting.Size = new System.Drawing.Size(90, 28);
            this.btnMatchingSetting.TabIndex = 74;
            this.btnMatchingSetting.Text = "定位设置";
            this.btnMatchingSetting.Click += new System.EventHandler(this.btnMatchingSetting_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // btnCalib
            // 
            this.btnCalib.Location = new System.Drawing.Point(895, 189);
            this.btnCalib.Name = "btnCalib";
            this.btnCalib.Size = new System.Drawing.Size(90, 28);
            this.btnCalib.TabIndex = 80;
            this.btnCalib.Text = "标定";
            this.btnCalib.Click += new System.EventHandler(this.btnCalib_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(19, 506);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(257, 126);
            this.txtResult.TabIndex = 81;
            this.txtResult.Text = "结果";
            // 
            // dgvRealPos
            // 
            this.dgvRealPos.AllowUserToAddRows = false;
            this.dgvRealPos.AllowUserToResizeColumns = false;
            this.dgvRealPos.AllowUserToResizeRows = false;
            this.dgvRealPos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRealPos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRealPos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRealPos.Location = new System.Drawing.Point(675, 305);
            this.dgvRealPos.Name = "dgvRealPos";
            this.dgvRealPos.RowTemplate.Height = 20;
            this.dgvRealPos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRealPos.Size = new System.Drawing.Size(404, 234);
            this.dgvRealPos.TabIndex = 83;
            this.dgvRealPos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvRealPos_RowStateChanged);
            this.dgvRealPos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvRealPos_KeyDown);
            // 
            // saveResultDialog
            // 
            this.saveResultDialog.Filter = "结果文件|*.csv;";
            // 
            // btnMatchingTest
            // 
            this.btnMatchingTest.Location = new System.Drawing.Point(702, 112);
            this.btnMatchingTest.Name = "btnMatchingTest";
            this.btnMatchingTest.Size = new System.Drawing.Size(90, 28);
            this.btnMatchingTest.TabIndex = 75;
            this.btnMatchingTest.Text = "位置获取";
            this.btnMatchingTest.Click += new System.EventHandler(this.btnMatchingTest_Click);
            // 
            // groupBoxPointPos
            // 
            this.groupBoxPointPos.Controls.Add(this.rdbtn9);
            this.groupBoxPointPos.Controls.Add(this.rdbtn8);
            this.groupBoxPointPos.Controls.Add(this.rdbtn7);
            this.groupBoxPointPos.Controls.Add(this.rdbtn6);
            this.groupBoxPointPos.Controls.Add(this.rdbtn5);
            this.groupBoxPointPos.Controls.Add(this.rdbtn4);
            this.groupBoxPointPos.Controls.Add(this.rdbtn3);
            this.groupBoxPointPos.Controls.Add(this.rdbtn2);
            this.groupBoxPointPos.Controls.Add(this.rdbtn1);
            this.groupBoxPointPos.Location = new System.Drawing.Point(702, 155);
            this.groupBoxPointPos.Name = "groupBoxPointPos";
            this.groupBoxPointPos.Size = new System.Drawing.Size(176, 124);
            this.groupBoxPointPos.TabIndex = 85;
            this.groupBoxPointPos.TabStop = false;
            this.groupBoxPointPos.Text = "点位选择";
            // 
            // rdbtn9
            // 
            this.rdbtn9.AutoSize = true;
            this.rdbtn9.Location = new System.Drawing.Point(125, 101);
            this.rdbtn9.Name = "rdbtn9";
            this.rdbtn9.Size = new System.Drawing.Size(29, 16);
            this.rdbtn9.TabIndex = 8;
            this.rdbtn9.TabStop = true;
            this.rdbtn9.Text = "9";
            this.rdbtn9.UseVisualStyleBackColor = true;
            this.rdbtn9.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn8
            // 
            this.rdbtn8.AutoSize = true;
            this.rdbtn8.Location = new System.Drawing.Point(78, 101);
            this.rdbtn8.Name = "rdbtn8";
            this.rdbtn8.Size = new System.Drawing.Size(29, 16);
            this.rdbtn8.TabIndex = 7;
            this.rdbtn8.TabStop = true;
            this.rdbtn8.Text = "8";
            this.rdbtn8.UseVisualStyleBackColor = true;
            this.rdbtn8.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn7
            // 
            this.rdbtn7.AutoSize = true;
            this.rdbtn7.Location = new System.Drawing.Point(31, 101);
            this.rdbtn7.Name = "rdbtn7";
            this.rdbtn7.Size = new System.Drawing.Size(29, 16);
            this.rdbtn7.TabIndex = 6;
            this.rdbtn7.TabStop = true;
            this.rdbtn7.Text = "7";
            this.rdbtn7.UseVisualStyleBackColor = true;
            this.rdbtn7.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn6
            // 
            this.rdbtn6.AutoSize = true;
            this.rdbtn6.Location = new System.Drawing.Point(31, 61);
            this.rdbtn6.Name = "rdbtn6";
            this.rdbtn6.Size = new System.Drawing.Size(29, 16);
            this.rdbtn6.TabIndex = 5;
            this.rdbtn6.TabStop = true;
            this.rdbtn6.Text = "6";
            this.rdbtn6.UseVisualStyleBackColor = true;
            this.rdbtn6.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn5
            // 
            this.rdbtn5.AutoSize = true;
            this.rdbtn5.Location = new System.Drawing.Point(78, 61);
            this.rdbtn5.Name = "rdbtn5";
            this.rdbtn5.Size = new System.Drawing.Size(29, 16);
            this.rdbtn5.TabIndex = 4;
            this.rdbtn5.TabStop = true;
            this.rdbtn5.Text = "5";
            this.rdbtn5.UseVisualStyleBackColor = true;
            this.rdbtn5.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn4
            // 
            this.rdbtn4.AutoSize = true;
            this.rdbtn4.Location = new System.Drawing.Point(125, 61);
            this.rdbtn4.Name = "rdbtn4";
            this.rdbtn4.Size = new System.Drawing.Size(29, 16);
            this.rdbtn4.TabIndex = 3;
            this.rdbtn4.TabStop = true;
            this.rdbtn4.Text = "4";
            this.rdbtn4.UseVisualStyleBackColor = true;
            this.rdbtn4.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn3
            // 
            this.rdbtn3.AutoSize = true;
            this.rdbtn3.Location = new System.Drawing.Point(125, 21);
            this.rdbtn3.Name = "rdbtn3";
            this.rdbtn3.Size = new System.Drawing.Size(29, 16);
            this.rdbtn3.TabIndex = 2;
            this.rdbtn3.TabStop = true;
            this.rdbtn3.Text = "3";
            this.rdbtn3.UseVisualStyleBackColor = true;
            this.rdbtn3.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn2
            // 
            this.rdbtn2.AutoSize = true;
            this.rdbtn2.Location = new System.Drawing.Point(78, 21);
            this.rdbtn2.Name = "rdbtn2";
            this.rdbtn2.Size = new System.Drawing.Size(29, 16);
            this.rdbtn2.TabIndex = 1;
            this.rdbtn2.TabStop = true;
            this.rdbtn2.Text = "2";
            this.rdbtn2.UseVisualStyleBackColor = true;
            this.rdbtn2.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // rdbtn1
            // 
            this.rdbtn1.AutoSize = true;
            this.rdbtn1.Location = new System.Drawing.Point(31, 21);
            this.rdbtn1.Name = "rdbtn1";
            this.rdbtn1.Size = new System.Drawing.Size(29, 16);
            this.rdbtn1.TabIndex = 0;
            this.rdbtn1.TabStop = true;
            this.rdbtn1.Text = "1";
            this.rdbtn1.UseVisualStyleBackColor = true;
            this.rdbtn1.CheckedChanged += new System.EventHandler(this.rdbtnPos_CheckedChanged);
            // 
            // btnPixImput
            // 
            this.btnPixImput.Location = new System.Drawing.Point(895, 155);
            this.btnPixImput.Name = "btnPixImput";
            this.btnPixImput.Size = new System.Drawing.Size(90, 28);
            this.btnPixImput.TabIndex = 86;
            this.btnPixImput.Text = "像素导入";
            this.btnPixImput.Click += new System.EventHandler(this.btnPixImput_Click);
            // 
            // btnVerification
            // 
            this.btnVerification.Location = new System.Drawing.Point(895, 233);
            this.btnVerification.Name = "btnVerification";
            this.btnVerification.Size = new System.Drawing.Size(90, 28);
            this.btnVerification.TabIndex = 87;
            this.btnVerification.Text = "验证";
            this.btnVerification.Click += new System.EventHandler(this.btnVerification_Click);
            // 
            // Calib9PointToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnVerification);
            this.Controls.Add(this.btnPixImput);
            this.Controls.Add(this.groupBoxPointPos);
            this.Controls.Add(this.btnMatchingTest);
            this.Controls.Add(this.dgvRealPos);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnCalib);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.hWndUnit1);
            this.Name = "Calib9PointToolUnit";
            this.Size = new System.Drawing.Size(1110, 640);
            this.Load += new System.EventHandler(this.Calib9PointToolUnit_Load);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.btnCalib, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.Controls.SetChildIndex(this.dgvRealPos, 0);
            this.Controls.SetChildIndex(this.btnMatchingTest, 0);
            this.Controls.SetChildIndex(this.groupBoxPointPos, 0);
            this.Controls.SetChildIndex(this.btnPixImput, 0);
            this.Controls.SetChildIndex(this.btnVerification, 0);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRealPos)).EndInit();
            this.groupBoxPointPos.ResumeLayout(false);
            this.groupBoxPointPos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageControl.HWndUnit hWndUnit1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLoadImg;
        private System.Windows.Forms.Button btnSetModelImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnMatchingSetting;
        private System.Windows.Forms.Button btnCalib;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.DataGridView dgvRealPos;
        private System.Windows.Forms.SaveFileDialog saveResultDialog;
        private System.Windows.Forms.Button btnMatchingTest;
        private System.Windows.Forms.GroupBox groupBoxPointPos;
        private System.Windows.Forms.RadioButton rdbtn9;
        private System.Windows.Forms.RadioButton rdbtn8;
        private System.Windows.Forms.RadioButton rdbtn7;
        private System.Windows.Forms.RadioButton rdbtn6;
        private System.Windows.Forms.RadioButton rdbtn5;
        private System.Windows.Forms.RadioButton rdbtn4;
        private System.Windows.Forms.RadioButton rdbtn3;
        private System.Windows.Forms.RadioButton rdbtn2;
        private System.Windows.Forms.RadioButton rdbtn1;
        private System.Windows.Forms.Button btnPixImput;
        private System.Windows.Forms.Button btnVerification;
    }
}
