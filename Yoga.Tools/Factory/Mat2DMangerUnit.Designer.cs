namespace Yoga.Tools
{
    partial class Mat2DMangerUnit
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
            this.groupBoxSelectMat2DTool = new System.Windows.Forms.GroupBox();
            this.cmbMatchingToolSelect = new System.Windows.Forms.ComboBox();
            this.chkUseMat2D = new System.Windows.Forms.CheckBox();
            this.groupBoxSelectMat2DTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSelectMat2DTool
            // 
            this.groupBoxSelectMat2DTool.Controls.Add(this.cmbMatchingToolSelect);
            this.groupBoxSelectMat2DTool.Controls.Add(this.chkUseMat2D);
            this.groupBoxSelectMat2DTool.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSelectMat2DTool.Name = "groupBoxSelectMat2DTool";
            this.groupBoxSelectMat2DTool.Size = new System.Drawing.Size(196, 108);
            this.groupBoxSelectMat2DTool.TabIndex = 16;
            this.groupBoxSelectMat2DTool.TabStop = false;
            this.groupBoxSelectMat2DTool.Text = "选择定位工具";
            // 
            // cmbMatchingToolSelect
            // 
            this.cmbMatchingToolSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMatchingToolSelect.Enabled = false;
            this.cmbMatchingToolSelect.FormattingEnabled = true;
            this.cmbMatchingToolSelect.Location = new System.Drawing.Point(33, 32);
            this.cmbMatchingToolSelect.Name = "cmbMatchingToolSelect";
            this.cmbMatchingToolSelect.Size = new System.Drawing.Size(121, 20);
            this.cmbMatchingToolSelect.TabIndex = 0;
            this.cmbMatchingToolSelect.SelectedIndexChanged += new System.EventHandler(this.cmbMatchingToolSelect_SelectedIndexChanged);
            // 
            // chkUseMat2D
            // 
            this.chkUseMat2D.AutoSize = true;
            this.chkUseMat2D.Location = new System.Drawing.Point(33, 71);
            this.chkUseMat2D.Name = "chkUseMat2D";
            this.chkUseMat2D.Size = new System.Drawing.Size(96, 16);
            this.chkUseMat2D.TabIndex = 14;
            this.chkUseMat2D.Text = "使用定位工具";
            this.chkUseMat2D.UseVisualStyleBackColor = true;
            this.chkUseMat2D.CheckedChanged += new System.EventHandler(this.chkUseMat2D_CheckedChanged);
            // 
            // Mat2DMangerUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxSelectMat2DTool);
            this.Name = "Mat2DMangerUnit";
            this.Size = new System.Drawing.Size(208, 120);
            this.Load += new System.EventHandler(this.Mat2DMangerUnit_Load);
            this.groupBoxSelectMat2DTool.ResumeLayout(false);
            this.groupBoxSelectMat2DTool.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSelectMat2DTool;
        private System.Windows.Forms.ComboBox cmbMatchingToolSelect;
        private System.Windows.Forms.CheckBox chkUseMat2D;
    }
}
