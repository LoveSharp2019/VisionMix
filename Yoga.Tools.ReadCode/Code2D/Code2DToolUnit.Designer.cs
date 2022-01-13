namespace Yoga.Tools.Code2D
{
    partial class Code2DToolUnit
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
            this.groupBoxStrSub = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UpDownLength = new System.Windows.Forms.NumericUpDown();
            this.UpDownStartIndex = new System.Windows.Forms.NumericUpDown();
            this.reduceRect1Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mat2DMangerUnit1 = new Yoga.Tools.Mat2DMangerUnit();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCreateModel = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.btnAddModel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtModelKeyName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFindMode = new System.Windows.Forms.ComboBox();
            this.chkUseZxing = new System.Windows.Forms.CheckBox();
            this.btnDeleteSearchRegion = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxCodeTypeWant = new System.Windows.Forms.ComboBox();
            this.chkUseTextCompare = new System.Windows.Forms.CheckBox();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.tabPageModelManger = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ModelListBox = new System.Windows.Forms.ListBox();
            this.btnDelAllModel = new System.Windows.Forms.Button();
            this.btnDeleteModel = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.groupBoxStrSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStartIndex)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageCreateModel.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.tabPageModelManger.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // groupBoxStrSub
            // 
            this.groupBoxStrSub.Controls.Add(this.label4);
            this.groupBoxStrSub.Controls.Add(this.label3);
            this.groupBoxStrSub.Controls.Add(this.UpDownLength);
            this.groupBoxStrSub.Controls.Add(this.UpDownStartIndex);
            this.groupBoxStrSub.Enabled = false;
            this.groupBoxStrSub.Location = new System.Drawing.Point(6, 78);
            this.groupBoxStrSub.Name = "groupBoxStrSub";
            this.groupBoxStrSub.Size = new System.Drawing.Size(247, 81);
            this.groupBoxStrSub.TabIndex = 77;
            this.groupBoxStrSub.TabStop = false;
            this.groupBoxStrSub.Text = "参数设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 82;
            this.label4.Text = "字符结束位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 81;
            this.label3.Text = "字符起始位";
            // 
            // UpDownLength
            // 
            this.UpDownLength.Location = new System.Drawing.Point(81, 47);
            this.UpDownLength.Name = "UpDownLength";
            this.UpDownLength.Size = new System.Drawing.Size(120, 21);
            this.UpDownLength.TabIndex = 80;
            this.UpDownLength.ValueChanged += new System.EventHandler(this.UpDownLength_ValueChanged);
            // 
            // UpDownStartIndex
            // 
            this.UpDownStartIndex.Location = new System.Drawing.Point(81, 20);
            this.UpDownStartIndex.Name = "UpDownStartIndex";
            this.UpDownStartIndex.Size = new System.Drawing.Size(120, 21);
            this.UpDownStartIndex.TabIndex = 79;
            this.UpDownStartIndex.ValueChanged += new System.EventHandler(this.UpDownStartIndex_ValueChanged);
            // 
            // reduceRect1Button
            // 
            this.reduceRect1Button.Location = new System.Drawing.Point(26, 228);
            this.reduceRect1Button.Name = "reduceRect1Button";
            this.reduceRect1Button.Size = new System.Drawing.Size(80, 26);
            this.reduceRect1Button.TabIndex = 13;
            this.reduceRect1Button.Text = "画搜索框";
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
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(3, 382);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 83;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCreateModel);
            this.tabControl.Controls.Add(this.tabPageModelManger);
            this.tabControl.Location = new System.Drawing.Point(390, 66);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(348, 531);
            this.tabControl.TabIndex = 76;
            // 
            // tabPageCreateModel
            // 
            this.tabPageCreateModel.Controls.Add(this.groupBoxCreateModel);
            this.tabPageCreateModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateModel.Name = "tabPageCreateModel";
            this.tabPageCreateModel.Size = new System.Drawing.Size(340, 505);
            this.tabPageCreateModel.TabIndex = 0;
            this.tabPageCreateModel.Text = "  创建模型";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.btnLoadImg);
            this.groupBoxCreateModel.Controls.Add(this.btnAddModel);
            this.groupBoxCreateModel.Controls.Add(this.label7);
            this.groupBoxCreateModel.Controls.Add(this.txtModelKeyName);
            this.groupBoxCreateModel.Controls.Add(this.label6);
            this.groupBoxCreateModel.Controls.Add(this.cmbFindMode);
            this.groupBoxCreateModel.Controls.Add(this.chkUseZxing);
            this.groupBoxCreateModel.Controls.Add(this.btnDeleteSearchRegion);
            this.groupBoxCreateModel.Controls.Add(this.label5);
            this.groupBoxCreateModel.Controls.Add(this.cbxCodeTypeWant);
            this.groupBoxCreateModel.Controls.Add(this.chkUseTextCompare);
            this.groupBoxCreateModel.Controls.Add(this.mat2DMangerUnit1);
            this.groupBoxCreateModel.Controls.Add(this.groupBoxStrSub);
            this.groupBoxCreateModel.Controls.Add(this.TestModelButton);
            this.groupBoxCreateModel.Controls.Add(this.reduceRect1Button);
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(470, 526);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.Location = new System.Drawing.Point(26, 272);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(80, 28);
            this.btnLoadImg.TabIndex = 93;
            this.btnLoadImg.Text = "采集图像";
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // btnAddModel
            // 
            this.btnAddModel.Location = new System.Drawing.Point(220, 310);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(80, 30);
            this.btnAddModel.TabIndex = 92;
            this.btnAddModel.Text = "添加模板";
            this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(154, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 91;
            this.label7.Text = "模板标记";
            // 
            // txtModelKeyName
            // 
            this.txtModelKeyName.Location = new System.Drawing.Point(220, 279);
            this.txtModelKeyName.Name = "txtModelKeyName";
            this.txtModelKeyName.Size = new System.Drawing.Size(100, 21);
            this.txtModelKeyName.TabIndex = 90;
            this.txtModelKeyName.Text = "默认";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 88;
            this.label6.Text = "查找模式";
            // 
            // cmbFindMode
            // 
            this.cmbFindMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFindMode.FormattingEnabled = true;
            this.cmbFindMode.Location = new System.Drawing.Point(96, 176);
            this.cmbFindMode.Name = "cmbFindMode";
            this.cmbFindMode.Size = new System.Drawing.Size(157, 20);
            this.cmbFindMode.TabIndex = 89;
            this.cmbFindMode.SelectedIndexChanged += new System.EventHandler(this.cmbFindMode_SelectedIndexChanged);
            // 
            // chkUseZxing
            // 
            this.chkUseZxing.AutoSize = true;
            this.chkUseZxing.Location = new System.Drawing.Point(6, 34);
            this.chkUseZxing.Name = "chkUseZxing";
            this.chkUseZxing.Size = new System.Drawing.Size(132, 16);
            this.chkUseZxing.TabIndex = 87;
            this.chkUseZxing.Text = "使用深度查找(耗时)";
            this.chkUseZxing.UseVisualStyleBackColor = true;
            this.chkUseZxing.CheckedChanged += new System.EventHandler(this.chkUseZxing_CheckedChanged);
            // 
            // btnDeleteSearchRegion
            // 
            this.btnDeleteSearchRegion.Location = new System.Drawing.Point(149, 228);
            this.btnDeleteSearchRegion.Name = "btnDeleteSearchRegion";
            this.btnDeleteSearchRegion.Size = new System.Drawing.Size(80, 26);
            this.btnDeleteSearchRegion.TabIndex = 86;
            this.btnDeleteSearchRegion.Text = "删除搜索框";
            this.btnDeleteSearchRegion.Click += new System.EventHandler(this.btnDeleteSearchRegion_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 83;
            this.label5.Text = "编码格式";
            // 
            // cbxCodeTypeWant
            // 
            this.cbxCodeTypeWant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCodeTypeWant.FormattingEnabled = true;
            this.cbxCodeTypeWant.Items.AddRange(new object[] {
            "auto",
            "QR Code",
            "Data Matrix ECC 200",
            "Aztec Code",
            "GS1 Aztec Code",
            "GS1 DataMatrix",
            "GS1 QR Code",
            "Micro QR Code",
            "PDF417"});
            this.cbxCodeTypeWant.Location = new System.Drawing.Point(96, 202);
            this.cbxCodeTypeWant.Name = "cbxCodeTypeWant";
            this.cbxCodeTypeWant.Size = new System.Drawing.Size(157, 20);
            this.cbxCodeTypeWant.TabIndex = 85;
            this.cbxCodeTypeWant.SelectedIndexChanged += new System.EventHandler(this.cbxCodeTypeWant_SelectedIndexChanged);
            // 
            // chkUseTextCompare
            // 
            this.chkUseTextCompare.AutoSize = true;
            this.chkUseTextCompare.Location = new System.Drawing.Point(6, 56);
            this.chkUseTextCompare.Name = "chkUseTextCompare";
            this.chkUseTextCompare.Size = new System.Drawing.Size(120, 16);
            this.chkUseTextCompare.TabIndex = 84;
            this.chkUseTextCompare.Text = "使用模板文字对比";
            this.chkUseTextCompare.UseVisualStyleBackColor = true;
            this.chkUseTextCompare.CheckedChanged += new System.EventHandler(this.chkUseTextCompare_CheckedChanged);
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(26, 310);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(80, 30);
            this.TestModelButton.TabIndex = 73;
            this.TestModelButton.Text = "训练图像";
            this.TestModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
            // 
            // tabPageModelManger
            // 
            this.tabPageModelManger.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageModelManger.Controls.Add(this.groupBox1);
            this.tabPageModelManger.Location = new System.Drawing.Point(4, 22);
            this.tabPageModelManger.Name = "tabPageModelManger";
            this.tabPageModelManger.Size = new System.Drawing.Size(340, 505);
            this.tabPageModelManger.TabIndex = 1;
            this.tabPageModelManger.Text = "模板管理";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ModelListBox);
            this.groupBox1.Controls.Add(this.btnDelAllModel);
            this.groupBox1.Controls.Add(this.btnDeleteModel);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 240);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模板管理";
            // 
            // ModelListBox
            // 
            this.ModelListBox.HorizontalScrollbar = true;
            this.ModelListBox.ItemHeight = 12;
            this.ModelListBox.Location = new System.Drawing.Point(6, 25);
            this.ModelListBox.Name = "ModelListBox";
            this.ModelListBox.Size = new System.Drawing.Size(184, 196);
            this.ModelListBox.TabIndex = 71;
            // 
            // btnDelAllModel
            // 
            this.btnDelAllModel.Location = new System.Drawing.Point(196, 92);
            this.btnDelAllModel.Name = "btnDelAllModel";
            this.btnDelAllModel.Size = new System.Drawing.Size(105, 30);
            this.btnDelAllModel.TabIndex = 70;
            this.btnDelAllModel.Text = "删除所有";
            this.btnDelAllModel.Click += new System.EventHandler(this.btnDelAllModel_Click);
            // 
            // btnDeleteModel
            // 
            this.btnDeleteModel.Location = new System.Drawing.Point(196, 57);
            this.btnDeleteModel.Name = "btnDeleteModel";
            this.btnDeleteModel.Size = new System.Drawing.Size(105, 30);
            this.btnDeleteModel.TabIndex = 69;
            this.btnDeleteModel.Text = "删除模板";
            this.btnDeleteModel.Click += new System.EventHandler(this.btnDeleteModel_Click);
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
            // Code2DToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.tabControl);
            this.Name = "Code2DToolUnit";
            this.Load += new System.EventHandler(this.Code2DToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.groupBoxStrSub.ResumeLayout(false);
            this.groupBoxStrSub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStartIndex)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.PerformLayout();
            this.tabPageModelManger.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.GroupBox groupBoxStrSub;
        private System.Windows.Forms.Button reduceRect1Button;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCreateModel;
        private System.Windows.Forms.GroupBox groupBoxCreateModel;
        private System.Windows.Forms.Button TestModelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown UpDownLength;
        private System.Windows.Forms.NumericUpDown UpDownStartIndex;
        private System.Windows.Forms.TextBox txtResult;
        private ImageControl.HWndUnit hWndUnit1;
        private Mat2DMangerUnit mat2DMangerUnit1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkUseTextCompare;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxCodeTypeWant;
        private System.Windows.Forms.Button btnDeleteSearchRegion;
        private System.Windows.Forms.CheckBox chkUseZxing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFindMode;
        private System.Windows.Forms.TabPage tabPageModelManger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox ModelListBox;
        private System.Windows.Forms.Button btnDelAllModel;
        private System.Windows.Forms.Button btnDeleteModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtModelKeyName;
        private System.Windows.Forms.Button btnAddModel;
        private System.Windows.Forms.Button btnLoadImg;
    }
}
