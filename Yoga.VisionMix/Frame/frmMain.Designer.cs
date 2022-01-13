using System;

namespace Yoga.VisionMix.Frame
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登陆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目另存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.通信设定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除异常ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运动控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.点动操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iO监视ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.速度设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsCuurrentProject = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUserMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCommStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsErrMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.openProjectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveProjectFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerTick
            // 
            this.timerTick.Enabled = true;
            this.timerTick.Tick += new System.EventHandler(this.timerTick_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.运动控制ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登陆ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 登陆ToolStripMenuItem
            // 
            this.登陆ToolStripMenuItem.Name = "登陆ToolStripMenuItem";
            this.登陆ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.登陆ToolStripMenuItem.Text = "登陆";
            this.登陆ToolStripMenuItem.Click += new System.EventHandler(this.登陆ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开项目ToolStripMenuItem,
            this.项目另存ToolStripMenuItem,
            this.保存项目ToolStripMenuItem,
            this.项目管理ToolStripMenuItem,
            this.toolStripSeparator1,
            this.通信设定ToolStripMenuItem,
            this.清除异常ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 打开项目ToolStripMenuItem
            // 
            this.打开项目ToolStripMenuItem.Name = "打开项目ToolStripMenuItem";
            this.打开项目ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开项目ToolStripMenuItem.Text = "打开项目";
            this.打开项目ToolStripMenuItem.Click += new System.EventHandler(this.打开项目ToolStripMenuItem_Click);
            // 
            // 项目另存ToolStripMenuItem
            // 
            this.项目另存ToolStripMenuItem.Name = "项目另存ToolStripMenuItem";
            this.项目另存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.项目另存ToolStripMenuItem.Text = "项目另存";
            this.项目另存ToolStripMenuItem.Click += new System.EventHandler(this.项目另存ToolStripMenuItem_Click);
            // 
            // 保存项目ToolStripMenuItem
            // 
            this.保存项目ToolStripMenuItem.Name = "保存项目ToolStripMenuItem";
            this.保存项目ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存项目ToolStripMenuItem.Text = "保存项目";
            this.保存项目ToolStripMenuItem.Click += new System.EventHandler(this.保存项目ToolStripMenuItem_Click);
            // 
            // 项目管理ToolStripMenuItem
            // 
            this.项目管理ToolStripMenuItem.Name = "项目管理ToolStripMenuItem";
            this.项目管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.项目管理ToolStripMenuItem.Text = "项目管理";
            this.项目管理ToolStripMenuItem.Visible = false;
            this.项目管理ToolStripMenuItem.Click += new System.EventHandler(this.项目管理ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 通信设定ToolStripMenuItem
            // 
            this.通信设定ToolStripMenuItem.Name = "通信设定ToolStripMenuItem";
            this.通信设定ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.通信设定ToolStripMenuItem.Text = "通信设定";
            this.通信设定ToolStripMenuItem.Click += new System.EventHandler(this.通信设定ToolStripMenuItem_Click);
            // 
            // 清除异常ToolStripMenuItem
            // 
            this.清除异常ToolStripMenuItem.Name = "清除异常ToolStripMenuItem";
            this.清除异常ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清除异常ToolStripMenuItem.Text = "清除异常";
            this.清除异常ToolStripMenuItem.Click += new System.EventHandler(this.清除异常ToolStripMenuItem_Click);
            // 
            // 运动控制ToolStripMenuItem
            // 
            this.运动控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.点动操作ToolStripMenuItem,
            this.iO监视ToolStripMenuItem,
            this.速度设置ToolStripMenuItem});
            this.运动控制ToolStripMenuItem.Name = "运动控制ToolStripMenuItem";
            this.运动控制ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.运动控制ToolStripMenuItem.Text = "运动控制";
            this.运动控制ToolStripMenuItem.Visible = false;
            // 
            // 点动操作ToolStripMenuItem
            // 
            this.点动操作ToolStripMenuItem.Name = "点动操作ToolStripMenuItem";
            this.点动操作ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.点动操作ToolStripMenuItem.Text = "点动操作";
            // 
            // iO监视ToolStripMenuItem
            // 
            this.iO监视ToolStripMenuItem.Name = "iO监视ToolStripMenuItem";
            this.iO监视ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.iO监视ToolStripMenuItem.Text = "IO监视";
            // 
            // 速度设置ToolStripMenuItem
            // 
            this.速度设置ToolStripMenuItem.Name = "速度设置ToolStripMenuItem";
            this.速度设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.速度设置ToolStripMenuItem.Text = "速度设置";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsCuurrentProject,
            this.lblUserMode,
            this.lblCommStatus,
            this.tsErrMessage,
            this.tsTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1350, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "当前工程:";
            // 
            // tsCuurrentProject
            // 
            this.tsCuurrentProject.Name = "tsCuurrentProject";
            this.tsCuurrentProject.Size = new System.Drawing.Size(0, 17);
            // 
            // lblUserMode
            // 
            this.lblUserMode.Name = "lblUserMode";
            this.lblUserMode.Size = new System.Drawing.Size(22, 17);
            this.lblUserMode.Text = "11";
            // 
            // lblCommStatus
            // 
            this.lblCommStatus.Name = "lblCommStatus";
            this.lblCommStatus.Size = new System.Drawing.Size(22, 17);
            this.lblCommStatus.Text = "11";
            // 
            // tsErrMessage
            // 
            this.tsErrMessage.Name = "tsErrMessage";
            this.tsErrMessage.Size = new System.Drawing.Size(1200, 17);
            this.tsErrMessage.Spring = true;
            // 
            // tsTime
            // 
            this.tsTime.Name = "tsTime";
            this.tsTime.Size = new System.Drawing.Size(32, 17);
            this.tsTime.Text = "时间";
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1350, 683);
            this.panelMain.TabIndex = 9;
            // 
            // openProjectFileDialog
            // 
            this.openProjectFileDialog.Filter = "工程文件|*.prj";
            // 
            // saveProjectFileDialog
            // 
            this.saveProjectFileDialog.Filter = "工程文件|*.prj";
            this.saveProjectFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "bmp|*.bmp|All files|*.*";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视觉检测软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion
        private System.Windows.Forms.Timer timerTick;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登陆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目另存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运动控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 点动操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iO监视ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 速度设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsCuurrentProject;
        private System.Windows.Forms.ToolStripStatusLabel tsErrMessage;
        private System.Windows.Forms.ToolStripStatusLabel tsTime;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.OpenFileDialog openProjectFileDialog;
        private System.Windows.Forms.SaveFileDialog saveProjectFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 通信设定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblCommStatus;
        private System.Windows.Forms.ToolStripMenuItem 清除异常ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblUserMode;
    }
}

