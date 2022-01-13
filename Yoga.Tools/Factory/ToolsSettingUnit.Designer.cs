namespace Yoga.Tools
{
    partial class ToolsSettingUnit
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
            this.lblCommToolsName = new System.Windows.Forms.Label();
            this.txtCommMin = new System.Windows.Forms.TextBox();
            this.labelComm1 = new System.Windows.Forms.Label();
            this.labelComm2 = new System.Windows.Forms.Label();
            this.txtCommMax = new System.Windows.Forms.TextBox();
            this.groupBoxCommSetting = new System.Windows.Forms.GroupBox();
            this.txtCommNote = new System.Windows.Forms.TextBox();
            this.chkIsOutputResults = new System.Windows.Forms.CheckBox();
            this.cmbImageSoure = new System.Windows.Forms.ComboBox();
            this.labelCommImageSoure = new System.Windows.Forms.Label();
            this.groupBoxCommSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCommToolsName
            // 
            this.lblCommToolsName.AutoSize = true;
            this.lblCommToolsName.Location = new System.Drawing.Point(17, 24);
            this.lblCommToolsName.Name = "lblCommToolsName";
            this.lblCommToolsName.Size = new System.Drawing.Size(53, 12);
            this.lblCommToolsName.TabIndex = 3;
            this.lblCommToolsName.Text = "工具名称";
            this.lblCommToolsName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCommMin
            // 
            this.txtCommMin.Location = new System.Drawing.Point(306, 20);
            this.txtCommMin.Name = "txtCommMin";
            this.txtCommMin.Size = new System.Drawing.Size(61, 21);
            this.txtCommMin.TabIndex = 4;
            this.txtCommMin.TextChanged += new System.EventHandler(this.txtCommMin_TextChanged);
            // 
            // labelComm1
            // 
            this.labelComm1.AutoSize = true;
            this.labelComm1.Location = new System.Drawing.Point(259, 24);
            this.labelComm1.Name = "labelComm1";
            this.labelComm1.Size = new System.Drawing.Size(41, 12);
            this.labelComm1.TabIndex = 5;
            this.labelComm1.Text = "最小值";
            // 
            // labelComm2
            // 
            this.labelComm2.AutoSize = true;
            this.labelComm2.Location = new System.Drawing.Point(373, 22);
            this.labelComm2.Name = "labelComm2";
            this.labelComm2.Size = new System.Drawing.Size(41, 12);
            this.labelComm2.TabIndex = 7;
            this.labelComm2.Text = "最大值";
            // 
            // txtCommMax
            // 
            this.txtCommMax.Location = new System.Drawing.Point(420, 18);
            this.txtCommMax.Name = "txtCommMax";
            this.txtCommMax.Size = new System.Drawing.Size(61, 21);
            this.txtCommMax.TabIndex = 6;
            this.txtCommMax.TextChanged += new System.EventHandler(this.txtCommMax_TextChanged);
            // 
            // groupBoxCommSetting
            // 
            this.groupBoxCommSetting.Controls.Add(this.labelCommImageSoure);
            this.groupBoxCommSetting.Controls.Add(this.cmbImageSoure);
            this.groupBoxCommSetting.Controls.Add(this.txtCommNote);
            this.groupBoxCommSetting.Controls.Add(this.chkIsOutputResults);
            this.groupBoxCommSetting.Controls.Add(this.lblCommToolsName);
            this.groupBoxCommSetting.Controls.Add(this.labelComm2);
            this.groupBoxCommSetting.Controls.Add(this.txtCommMin);
            this.groupBoxCommSetting.Controls.Add(this.txtCommMax);
            this.groupBoxCommSetting.Controls.Add(this.labelComm1);
            this.groupBoxCommSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCommSetting.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCommSetting.Name = "groupBoxCommSetting";
            this.groupBoxCommSetting.Size = new System.Drawing.Size(800, 47);
            this.groupBoxCommSetting.TabIndex = 9;
            this.groupBoxCommSetting.TabStop = false;
            this.groupBoxCommSetting.Text = "工具公共属性";
            // 
            // txtCommNote
            // 
            this.txtCommNote.Location = new System.Drawing.Point(76, 18);
            this.txtCommNote.Name = "txtCommNote";
            this.txtCommNote.Size = new System.Drawing.Size(177, 21);
            this.txtCommNote.TabIndex = 10;
            this.txtCommNote.TextChanged += new System.EventHandler(this.txtCommNote_TextChanged);
            // 
            // chkIsOutputResults
            // 
            this.chkIsOutputResults.AutoSize = true;
            this.chkIsOutputResults.Location = new System.Drawing.Point(504, 23);
            this.chkIsOutputResults.Name = "chkIsOutputResults";
            this.chkIsOutputResults.Size = new System.Drawing.Size(72, 16);
            this.chkIsOutputResults.TabIndex = 8;
            this.chkIsOutputResults.Text = "结果输出";
            this.chkIsOutputResults.UseVisualStyleBackColor = true;
            this.chkIsOutputResults.CheckedChanged += new System.EventHandler(this.chkIsOutputResults_CheckedChanged);
            // 
            // cmbImageSoure
            // 
            this.cmbImageSoure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageSoure.FormattingEnabled = true;
            this.cmbImageSoure.Location = new System.Drawing.Point(637, 18);
            this.cmbImageSoure.Name = "cmbImageSoure";
            this.cmbImageSoure.Size = new System.Drawing.Size(121, 20);
            this.cmbImageSoure.TabIndex = 11;
            this.cmbImageSoure.SelectedIndexChanged += new System.EventHandler(this.cmbImageSoure_SelectedIndexChanged);
            // 
            // labelCommImageSoure
            // 
            this.labelCommImageSoure.AutoSize = true;
            this.labelCommImageSoure.Location = new System.Drawing.Point(582, 24);
            this.labelCommImageSoure.Name = "labelCommImageSoure";
            this.labelCommImageSoure.Size = new System.Drawing.Size(41, 12);
            this.labelCommImageSoure.TabIndex = 12;
            this.labelCommImageSoure.Text = "图像源";
            // 
            // ToolsSettingUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCommSetting);
            this.Name = "ToolsSettingUnit";
            this.Size = new System.Drawing.Size(800, 600);
            this.groupBoxCommSetting.ResumeLayout(false);
            this.groupBoxCommSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCommToolsName;
        private System.Windows.Forms.TextBox txtCommMin;
        private System.Windows.Forms.Label labelComm1;
        private System.Windows.Forms.Label labelComm2;
        private System.Windows.Forms.TextBox txtCommMax;
        private System.Windows.Forms.GroupBox groupBoxCommSetting;
        private System.Windows.Forms.CheckBox chkIsOutputResults;
        private System.Windows.Forms.TextBox txtCommNote;
        private System.Windows.Forms.Label labelCommImageSoure;
        private System.Windows.Forms.ComboBox cmbImageSoure;
    }
}
