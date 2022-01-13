namespace Yoga.Tools.Factory
{
    partial class SacleImagePretreatmentUnit
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
            this.ScaleUpDown = new System.Windows.Forms.NumericUpDown();
            this.OffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ScaleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "比例";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "偏移";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ScaleUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.OffsetUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(142, 80);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像增强";
            // 
            // SacleImagePretreatmentUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SacleImagePretreatmentUnit";
            this.Size = new System.Drawing.Size(156, 92);
            ((System.ComponentModel.ISupportInitialize)(this.ScaleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ScaleUpDown;
        private System.Windows.Forms.NumericUpDown OffsetUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
