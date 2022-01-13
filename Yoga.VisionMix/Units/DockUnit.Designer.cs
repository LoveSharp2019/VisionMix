namespace Yoga.VisionMix.Units
{
    partial class DockUnit
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
            this.components = new System.ComponentModel.Container();
            this.Label_Version = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Label_Time = new System.Windows.Forms.Label();
            this.Label_Date = new System.Windows.Forms.Label();
            this.Clock = new System.Windows.Forms.Timer(this.components);
            this.btnLogin = new System.Windows.Forms.Button();
            this.Btn_IO = new System.Windows.Forms.Button();
            this.Btn_Automation = new System.Windows.Forms.Button();
            this.Btn_About = new System.Windows.Forms.Button();
            this.Btn_Quit = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnSoftKeyBoard = new System.Windows.Forms.Button();
            this.timerLogin = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Version
            // 
            this.Label_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_Version.AutoSize = true;
            this.Label_Version.Location = new System.Drawing.Point(5, 655);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(41, 12);
            this.Label_Version.TabIndex = 16;
            this.Label_Version.Text = "V1.0.0";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.Label_Time);
            this.panel1.Controls.Add(this.Label_Date);
            this.panel1.Location = new System.Drawing.Point(-2, 575);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 77);
            this.panel1.TabIndex = 15;
            // 
            // Label_Time
            // 
            this.Label_Time.AutoSize = true;
            this.Label_Time.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.Label_Time.Location = new System.Drawing.Point(-2, 47);
            this.Label_Time.Name = "Label_Time";
            this.Label_Time.Size = new System.Drawing.Size(44, 16);
            this.Label_Time.TabIndex = 1;
            this.Label_Time.Text = "Time";
            // 
            // Label_Date
            // 
            this.Label_Date.AutoSize = true;
            this.Label_Date.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.Label_Date.Location = new System.Drawing.Point(-2, 17);
            this.Label_Date.Name = "Label_Date";
            this.Label_Date.Size = new System.Drawing.Size(44, 16);
            this.Label_Date.TabIndex = 1;
            this.Label_Date.Text = "Date";
            // 
            // Clock
            // 
            this.Clock.Enabled = true;
            this.Clock.Interval = 500;
            this.Clock.Tick += new System.EventHandler(this.Clock_Tick);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(2, 50);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(161, 55);
            this.btnLogin.TabIndex = 18;
            this.btnLogin.Text = "用户登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Btn_IO
            // 
            this.Btn_IO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_IO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_IO.Location = new System.Drawing.Point(1, 409);
            this.Btn_IO.Name = "Btn_IO";
            this.Btn_IO.Size = new System.Drawing.Size(161, 55);
            this.Btn_IO.TabIndex = 19;
            this.Btn_IO.Text = "I/O";
            this.Btn_IO.UseVisualStyleBackColor = true;
            this.Btn_IO.Visible = false;
            this.Btn_IO.Click += new System.EventHandler(this.Btn_IO_Click);
            // 
            // Btn_Automation
            // 
            this.Btn_Automation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Automation.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Automation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Automation.Location = new System.Drawing.Point(1, 160);
            this.Btn_Automation.Name = "Btn_Automation";
            this.Btn_Automation.Size = new System.Drawing.Size(161, 55);
            this.Btn_Automation.TabIndex = 21;
            this.Btn_Automation.Text = "运行画面";
            this.Btn_Automation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Automation.UseVisualStyleBackColor = false;
            this.Btn_Automation.Click += new System.EventHandler(this.Btn_Automation_Click);
            // 
            // Btn_About
            // 
            this.Btn_About.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_About.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_About.Location = new System.Drawing.Point(1, 215);
            this.Btn_About.Name = "Btn_About";
            this.Btn_About.Size = new System.Drawing.Size(161, 55);
            this.Btn_About.TabIndex = 10;
            this.Btn_About.Text = "关于软件";
            this.Btn_About.UseVisualStyleBackColor = true;
            this.Btn_About.Click += new System.EventHandler(this.Btn_About_Click);
            // 
            // Btn_Quit
            // 
            this.Btn_Quit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Quit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Quit.Location = new System.Drawing.Point(1, 270);
            this.Btn_Quit.Name = "Btn_Quit";
            this.Btn_Quit.Size = new System.Drawing.Size(161, 55);
            this.Btn_Quit.TabIndex = 9;
            this.Btn_Quit.Text = "退出";
            this.Btn_Quit.UseVisualStyleBackColor = true;
            this.Btn_Quit.Click += new System.EventHandler(this.Btn_Quit_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.Enabled = false;
            this.btnSetting.Location = new System.Drawing.Point(2, 105);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(161, 55);
            this.btnSetting.TabIndex = 22;
            this.btnSetting.Text = "系统设置";
            this.btnSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnSoftKeyBoard
            // 
            this.btnSoftKeyBoard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSoftKeyBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSoftKeyBoard.Location = new System.Drawing.Point(1, 325);
            this.btnSoftKeyBoard.Name = "btnSoftKeyBoard";
            this.btnSoftKeyBoard.Size = new System.Drawing.Size(161, 55);
            this.btnSoftKeyBoard.TabIndex = 23;
            this.btnSoftKeyBoard.Text = "开/关软键盘";
            this.btnSoftKeyBoard.UseVisualStyleBackColor = true;
            this.btnSoftKeyBoard.Visible = false;
            this.btnSoftKeyBoard.Click += new System.EventHandler(this.btnSoftKeyBoard_Click);
            // 
            // timerLogin
            // 
            this.timerLogin.Interval = 1000;
            this.timerLogin.Tick += new System.EventHandler(this.timerLogin_Tick);
            // 
            // DockUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSoftKeyBoard);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.Btn_Automation);
            this.Controls.Add(this.Btn_IO);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Btn_Quit);
            this.Controls.Add(this.Btn_About);
            this.Name = "DockUnit";
            this.Size = new System.Drawing.Size(163, 689);
            this.Load += new System.EventHandler(this.DockFrame_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label_Time;
        private System.Windows.Forms.Label Label_Date;
        private System.Windows.Forms.Timer Clock;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button Btn_IO;
        private System.Windows.Forms.Button Btn_Automation;
        private System.Windows.Forms.Button Btn_About;
        private System.Windows.Forms.Button Btn_Quit;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnSoftKeyBoard;
        private System.Windows.Forms.Timer timerLogin;
    }
}
