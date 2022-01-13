namespace Yoga.Common.UI
{
    partial class IpTextBoxExt
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dot3 = new System.Windows.Forms.Label();
            this.dot2 = new System.Windows.Forms.Label();
            this.dot1 = new System.Windows.Forms.Label();
            this.tb_ip4 = new System.Windows.Forms.TextBox();
            this.tb_ip3 = new System.Windows.Forms.TextBox();
            this.tb_ip2 = new System.Windows.Forms.TextBox();
            this.tb_ip1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dot3);
            this.panel1.Controls.Add(this.dot2);
            this.panel1.Controls.Add(this.dot1);
            this.panel1.Controls.Add(this.tb_ip4);
            this.panel1.Controls.Add(this.tb_ip3);
            this.panel1.Controls.Add(this.tb_ip2);
            this.panel1.Controls.Add(this.tb_ip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 26);
            this.panel1.TabIndex = 7;
            // 
            // dot3
            // 
            this.dot3.Location = new System.Drawing.Point(124, 7);
            this.dot3.Name = "dot3";
            this.dot3.Size = new System.Drawing.Size(6, 10);
            this.dot3.TabIndex = 6;
            this.dot3.Text = ".";
            // 
            // dot2
            // 
            this.dot2.Location = new System.Drawing.Point(79, 7);
            this.dot2.Name = "dot2";
            this.dot2.Size = new System.Drawing.Size(6, 10);
            this.dot2.TabIndex = 5;
            this.dot2.Text = ".";
            // 
            // dot1
            // 
            this.dot1.BackColor = System.Drawing.Color.Transparent;
            this.dot1.Location = new System.Drawing.Point(34, 7);
            this.dot1.Name = "dot1";
            this.dot1.Size = new System.Drawing.Size(6, 10);
            this.dot1.TabIndex = 4;
            this.dot1.Text = ".";
            // 
            // tb_ip4
            // 
            this.tb_ip4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ip4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ip4.Location = new System.Drawing.Point(135, 3);
            this.tb_ip4.MaxLength = 3;
            this.tb_ip4.Name = "tb_ip4";
            this.tb_ip4.ShortcutsEnabled = false;
            this.tb_ip4.Size = new System.Drawing.Size(30, 16);
            this.tb_ip4.TabIndex = 3;
            this.tb_ip4.Tag = "false";
            this.tb_ip4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ip4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.tb_ip4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IpTextBox_MouseUp);
            // 
            // tb_ip3
            // 
            this.tb_ip3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ip3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ip3.Location = new System.Drawing.Point(91, 3);
            this.tb_ip3.MaxLength = 3;
            this.tb_ip3.Name = "tb_ip3";
            this.tb_ip3.ShortcutsEnabled = false;
            this.tb_ip3.Size = new System.Drawing.Size(30, 16);
            this.tb_ip3.TabIndex = 2;
            this.tb_ip3.Tag = "false";
            this.tb_ip3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ip3.TextChanged += new System.EventHandler(this.tb_ip3_TextChanged);
            this.tb_ip3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.tb_ip3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IpTextBox_MouseUp);
            // 
            // tb_ip2
            // 
            this.tb_ip2.BackColor = System.Drawing.SystemColors.Window;
            this.tb_ip2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ip2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ip2.Location = new System.Drawing.Point(47, 3);
            this.tb_ip2.MaxLength = 3;
            this.tb_ip2.Name = "tb_ip2";
            this.tb_ip2.ShortcutsEnabled = false;
            this.tb_ip2.Size = new System.Drawing.Size(30, 16);
            this.tb_ip2.TabIndex = 1;
            this.tb_ip2.Tag = "false";
            this.tb_ip2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ip2.TextChanged += new System.EventHandler(this.tb_ip2_TextChanged);
            this.tb_ip2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.tb_ip2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IpTextBox_MouseUp);
            // 
            // tb_ip1
            // 
            this.tb_ip1.BackColor = System.Drawing.SystemColors.Window;
            this.tb_ip1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_ip1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_ip1.Location = new System.Drawing.Point(3, 3);
            this.tb_ip1.MaxLength = 3;
            this.tb_ip1.Name = "tb_ip1";
            this.tb_ip1.ShortcutsEnabled = false;
            this.tb_ip1.Size = new System.Drawing.Size(30, 16);
            this.tb_ip1.TabIndex = 0;
            this.tb_ip1.Tag = "false";
            this.tb_ip1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_ip1.TextChanged += new System.EventHandler(this.tb_ip1_TextChanged);
            this.tb_ip1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IPv4TextBox_KeyPress);
            this.tb_ip1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IpTextBox_MouseUp);
            // 
            // IpTextBoxExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Name = "IpTextBoxExt";
            this.Size = new System.Drawing.Size(175, 26);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label dot3;
        private System.Windows.Forms.Label dot2;
        private System.Windows.Forms.Label dot1;
        private System.Windows.Forms.TextBox tb_ip3;
        private System.Windows.Forms.TextBox tb_ip2;
        private System.Windows.Forms.TextBox tb_ip1;
        private System.Windows.Forms.TextBox tb_ip4;
       
    }
}
