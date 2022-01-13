namespace Yoga.VisionMix.Units
{
    partial class ToolDgv
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
            this.dgvTools = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTools)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTools
            // 
            this.dgvTools.AllowUserToAddRows = false;
            this.dgvTools.AllowUserToResizeColumns = false;
            this.dgvTools.AllowUserToResizeRows = false;
            this.dgvTools.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTools.Location = new System.Drawing.Point(3, 17);
            this.dgvTools.MultiSelect = false;
            this.dgvTools.Name = "dgvTools";
            this.dgvTools.RowTemplate.Height = 23;
            this.dgvTools.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTools.Size = new System.Drawing.Size(374, 429);
            this.dgvTools.TabIndex = 82;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvTools);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 449);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ToolDgv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ToolDgv";
            this.Size = new System.Drawing.Size(380, 449);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTools)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTools;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
