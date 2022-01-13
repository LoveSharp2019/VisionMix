namespace Yoga.Test
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.sevenSegmentArray1 = new Yoga.Common.UI.SevenSegmentArray();
            this.button1 = new System.Windows.Forms.Button();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.hWndUnit2 = new Yoga.ImageControl.HWndUnit();
            this.hWndUnit3 = new Yoga.ImageControl.HWndUnit();
            this.SuspendLayout();
            // 
            // sevenSegmentArray1
            // 
            this.sevenSegmentArray1.ArrayCount = 4;
            this.sevenSegmentArray1.ColorBackground = System.Drawing.Color.Gray;
            this.sevenSegmentArray1.ColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.sevenSegmentArray1.ColorLight = System.Drawing.Color.Red;
            this.sevenSegmentArray1.DecimalShow = true;
            this.sevenSegmentArray1.ElementPadding = new System.Windows.Forms.Padding(4);
            this.sevenSegmentArray1.ElementWidth = 10;
            this.sevenSegmentArray1.ItalicFactor = 0F;
            this.sevenSegmentArray1.Location = new System.Drawing.Point(56, 92);
            this.sevenSegmentArray1.Name = "sevenSegmentArray1";
            this.sevenSegmentArray1.Size = new System.Drawing.Size(191, 117);
            this.sevenSegmentArray1.TabIndex = 0;
            this.sevenSegmentArray1.TabStop = false;
            this.sevenSegmentArray1.Value = null;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "打印测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(343, 25);
            this.hWndUnit1.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(311, 305);
            this.hWndUnit1.TabIndex = 2;
            // 
            // hWndUnit2
            // 
            this.hWndUnit2.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit2.CameraMessage = null;
            this.hWndUnit2.Location = new System.Drawing.Point(56, 25);
            this.hWndUnit2.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit2.Name = "hWndUnit2";
            this.hWndUnit2.Size = new System.Drawing.Size(311, 305);
            this.hWndUnit2.TabIndex = 3;
            // 
            // hWndUnit3
            // 
            this.hWndUnit3.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit3.CameraMessage = null;
            this.hWndUnit3.Location = new System.Drawing.Point(131, 12);
            this.hWndUnit3.MinimumSize = new System.Drawing.Size(10, 10);
            this.hWndUnit3.Name = "hWndUnit3";
            this.hWndUnit3.Size = new System.Drawing.Size(311, 305);
            this.hWndUnit3.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 308);
            this.Controls.Add(this.hWndUnit3);
            this.Controls.Add(this.hWndUnit2);
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sevenSegmentArray1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Common.UI.SevenSegmentArray sevenSegmentArray1;
        private System.Windows.Forms.Button button1;
        private ImageControl.HWndUnit hWndUnit1;
        private ImageControl.HWndUnit hWndUnit2;
        private ImageControl.HWndUnit hWndUnit3;
    }
}

