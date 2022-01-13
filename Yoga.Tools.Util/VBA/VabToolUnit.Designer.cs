namespace Yoga.Tools.VBA
{
    partial class VabToolUnit
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
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_ErrorInfo = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCompile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(3, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 504);
            this.panel1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(794, 398);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "脚本程序";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_ErrorInfo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 398);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(794, 106);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "错误提示";
            // 
            // txt_ErrorInfo
            // 
            this.txt_ErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ErrorInfo.Location = new System.Drawing.Point(3, 17);
            this.txt_ErrorInfo.Multiline = true;
            this.txt_ErrorInfo.Name = "txt_ErrorInfo";
            this.txt_ErrorInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_ErrorInfo.Size = new System.Drawing.Size(788, 86);
            this.txt_ErrorInfo.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(566, 58);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(98, 29);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnCompile
            // 
            this.btnCompile.Location = new System.Drawing.Point(670, 58);
            this.btnCompile.Name = "btnCompile";
            this.btnCompile.Size = new System.Drawing.Size(106, 29);
            this.btnCompile.TabIndex = 7;
            this.btnCompile.Text = "编 译";
            this.btnCompile.UseVisualStyleBackColor = true;
            this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
            // 
            // VabToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCompile);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.panel1);
            this.Name = "VabToolUnit";
            this.Load += new System.EventHandler(this.VabToolUnit_Load);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnRun, 0);
            this.Controls.SetChildIndex(this.btnCompile, 0);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCompile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_ErrorInfo;
    }
}
