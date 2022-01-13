namespace Yoga.Tools.BarCode
{
    partial class BarCodeToolUnit
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
            this.groupBoxCreateROI = new System.Windows.Forms.GroupBox();
            this.btnDeleteSearchRegion = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxCodeTypeWant = new System.Windows.Forms.ComboBox();
            this.chkUseTextCompare = new System.Windows.Forms.CheckBox();
            this.groupBoxStrSub = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.UpDownStartIndex = new System.Windows.Forms.NumericUpDown();
            this.UpDownLength = new System.Windows.Forms.NumericUpDown();
            this.reduceRect1Button = new System.Windows.Forms.Button();
            this.mat2DMangerUnit1 = new Yoga.Tools.Mat2DMangerUnit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCreateModel = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.groupBoxCreateROI.SuspendLayout();
            this.groupBoxStrSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStartIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownLength)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageCreateModel.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // groupBoxCreateROI
            // 
            this.groupBoxCreateROI.Controls.Add(this.btnDeleteSearchRegion);
            this.groupBoxCreateROI.Controls.Add(this.label3);
            this.groupBoxCreateROI.Controls.Add(this.cbxCodeTypeWant);
            this.groupBoxCreateROI.Controls.Add(this.chkUseTextCompare);
            this.groupBoxCreateROI.Controls.Add(this.groupBoxStrSub);
            this.groupBoxCreateROI.Controls.Add(this.reduceRect1Button);
            this.groupBoxCreateROI.Location = new System.Drawing.Point(24, 20);
            this.groupBoxCreateROI.Name = "groupBoxCreateROI";
            this.groupBoxCreateROI.Size = new System.Drawing.Size(312, 235);
            this.groupBoxCreateROI.TabIndex = 77;
            this.groupBoxCreateROI.TabStop = false;
            this.groupBoxCreateROI.Text = "参数设置";
            // 
            // btnDeleteSearchRegion
            // 
            this.btnDeleteSearchRegion.Location = new System.Drawing.Point(135, 203);
            this.btnDeleteSearchRegion.Name = "btnDeleteSearchRegion";
            this.btnDeleteSearchRegion.Size = new System.Drawing.Size(80, 26);
            this.btnDeleteSearchRegion.TabIndex = 89;
            this.btnDeleteSearchRegion.Text = "删除搜索框";
            this.btnDeleteSearchRegion.Click += new System.EventHandler(this.btnDeleteSearchRegion_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 87;
            this.label3.Text = "编码格式";
            // 
            // cbxCodeTypeWant
            // 
            this.cbxCodeTypeWant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCodeTypeWant.FormattingEnabled = true;
            this.cbxCodeTypeWant.Items.AddRange(new object[] {
            "auto",
            "2/5 Industrial",
            "2/5 Interleaved",
            "Codabar",
            "Code 128",
            "Code 39",
            "Code 93",
            "EAN-13 Add-On 2",
            "EAN-13 Add-On 5",
            "EAN-13",
            "EAN-8 Add-On 2",
            "EAN-8 Add-On 5",
            "EAN-8",
            "GS1 DataBar Expanded Stacked",
            "GS1 DataBar Expanded",
            "GS1 DataBar Limited",
            "GS1 DataBar Omnidir",
            "GS1 DataBar Stacked Omnidir",
            "GS1 DataBar Stacked",
            "GS1 DataBar Truncated",
            "GS1-128",
            "MSI",
            "PharmaCode",
            "UPC-A Add-On 2",
            "UPC-A Add-On 5",
            "UPC-A",
            "UPC-E Add-On 2",
            "UPC-E Add-On 5",
            "UPC-E"});
            this.cbxCodeTypeWant.Location = new System.Drawing.Point(95, 147);
            this.cbxCodeTypeWant.Name = "cbxCodeTypeWant";
            this.cbxCodeTypeWant.Size = new System.Drawing.Size(157, 20);
            this.cbxCodeTypeWant.TabIndex = 88;
            this.cbxCodeTypeWant.SelectedIndexChanged += new System.EventHandler(this.cbxCodeTypeWant_SelectedIndexChanged);
            // 
            // chkUseTextCompare
            // 
            this.chkUseTextCompare.AutoSize = true;
            this.chkUseTextCompare.Location = new System.Drawing.Point(25, 20);
            this.chkUseTextCompare.Name = "chkUseTextCompare";
            this.chkUseTextCompare.Size = new System.Drawing.Size(120, 16);
            this.chkUseTextCompare.TabIndex = 86;
            this.chkUseTextCompare.Text = "使用模板文字对比";
            this.chkUseTextCompare.UseVisualStyleBackColor = true;
            this.chkUseTextCompare.CheckedChanged += new System.EventHandler(this.chkUseTextCompare_CheckedChanged);
            // 
            // groupBoxStrSub
            // 
            this.groupBoxStrSub.Controls.Add(this.label5);
            this.groupBoxStrSub.Controls.Add(this.label6);
            this.groupBoxStrSub.Controls.Add(this.UpDownStartIndex);
            this.groupBoxStrSub.Controls.Add(this.UpDownLength);
            this.groupBoxStrSub.Enabled = false;
            this.groupBoxStrSub.Location = new System.Drawing.Point(13, 42);
            this.groupBoxStrSub.Name = "groupBoxStrSub";
            this.groupBoxStrSub.Size = new System.Drawing.Size(247, 81);
            this.groupBoxStrSub.TabIndex = 85;
            this.groupBoxStrSub.TabStop = false;
            this.groupBoxStrSub.Text = "参数设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 82;
            this.label5.Text = "字符结束位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 81;
            this.label6.Text = "字符起始位";
            // 
            // UpDownStartIndex
            // 
            this.UpDownStartIndex.Location = new System.Drawing.Point(82, 20);
            this.UpDownStartIndex.Name = "UpDownStartIndex";
            this.UpDownStartIndex.Size = new System.Drawing.Size(120, 21);
            this.UpDownStartIndex.TabIndex = 16;
            this.UpDownStartIndex.ValueChanged += new System.EventHandler(this.UpDownStartIndex_ValueChanged);
            // 
            // UpDownLength
            // 
            this.UpDownLength.Location = new System.Drawing.Point(82, 47);
            this.UpDownLength.Name = "UpDownLength";
            this.UpDownLength.Size = new System.Drawing.Size(120, 21);
            this.UpDownLength.TabIndex = 17;
            this.UpDownLength.ValueChanged += new System.EventHandler(this.UpDownLength_ValueChanged);
            // 
            // reduceRect1Button
            // 
            this.reduceRect1Button.Location = new System.Drawing.Point(13, 203);
            this.reduceRect1Button.Name = "reduceRect1Button";
            this.reduceRect1Button.Size = new System.Drawing.Size(80, 26);
            this.reduceRect1Button.TabIndex = 13;
            this.reduceRect1Button.Text = "画搜索框";
            this.reduceRect1Button.Click += new System.EventHandler(this.reduceRect1Button_Click);
            // 
            // mat2DMangerUnit1
            // 
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(18, 317);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "字符长度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "字符起始位";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCreateModel);
            this.tabControl.Location = new System.Drawing.Point(354, 72);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(359, 513);
            this.tabControl.TabIndex = 76;
            // 
            // tabPageCreateModel
            // 
            this.tabPageCreateModel.Controls.Add(this.groupBoxCreateModel);
            this.tabPageCreateModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateModel.Name = "tabPageCreateModel";
            this.tabPageCreateModel.Size = new System.Drawing.Size(351, 487);
            this.tabPageCreateModel.TabIndex = 0;
            this.tabPageCreateModel.Text = "  创建模型";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.TestModelButton);
            this.groupBoxCreateModel.Controls.Add(this.groupBoxCreateROI);
            this.groupBoxCreateModel.Controls.Add(this.mat2DMangerUnit1);
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(342, 484);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(30, 281);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(105, 30);
            this.TestModelButton.TabIndex = 73;
            this.TestModelButton.Text = "测试图像";
            this.TestModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(18, 375);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(281, 182);
            this.txtResult.TabIndex = 79;
            this.txtResult.Text = "结果";
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(2, 72);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(346, 271);
            this.hWndUnit1.TabIndex = 80;
            // 
            // BarCodeToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.tabControl);
            this.Name = "BarCodeToolUnit";
            this.Load += new System.EventHandler(this.Code1DToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.groupBoxCreateROI.ResumeLayout(false);
            this.groupBoxCreateROI.PerformLayout();
            this.groupBoxStrSub.ResumeLayout(false);
            this.groupBoxStrSub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownStartIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownLength)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.GroupBox groupBoxCreateROI;
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
        private System.Windows.Forms.CheckBox chkUseTextCompare;
        private System.Windows.Forms.GroupBox groupBoxStrSub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxCodeTypeWant;
        private System.Windows.Forms.Button btnDeleteSearchRegion;
    }
}
