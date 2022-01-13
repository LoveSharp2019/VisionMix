namespace Yoga.Tools.Factory
{
    partial class ToolShowUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolShowUnit));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加工具组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.删除工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除工具组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewAllTools = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加工具ToolStripMenuItem,
            this.添加工具组ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.删除工具ToolStripMenuItem,
            this.删除工具组ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 98);
            // 
            // 添加工具ToolStripMenuItem
            // 
            this.添加工具ToolStripMenuItem.Name = "添加工具ToolStripMenuItem";
            this.添加工具ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.添加工具ToolStripMenuItem.Text = "添加工具";
            this.添加工具ToolStripMenuItem.Click += new System.EventHandler(this.添加工具ToolStripMenuItem_Click);
            // 
            // 添加工具组ToolStripMenuItem
            // 
            this.添加工具组ToolStripMenuItem.Name = "添加工具组ToolStripMenuItem";
            this.添加工具组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.添加工具组ToolStripMenuItem.Text = "添加工具组";
            this.添加工具组ToolStripMenuItem.Click += new System.EventHandler(this.添加工具组ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // 删除工具ToolStripMenuItem
            // 
            this.删除工具ToolStripMenuItem.Name = "删除工具ToolStripMenuItem";
            this.删除工具ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除工具ToolStripMenuItem.Text = "删除工具";
            this.删除工具ToolStripMenuItem.Click += new System.EventHandler(this.删除工具ToolStripMenuItem_Click);
            // 
            // 删除工具组ToolStripMenuItem
            // 
            this.删除工具组ToolStripMenuItem.Name = "删除工具组ToolStripMenuItem";
            this.删除工具组ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除工具组ToolStripMenuItem.Text = "删除工具组";
            this.删除工具组ToolStripMenuItem.Click += new System.EventHandler(this.删除工具组ToolStripMenuItem_Click);
            // 
            // treeViewAllTools
            // 
            this.treeViewAllTools.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAllTools.ContextMenuStrip = this.contextMenuStrip1;
            this.treeViewAllTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAllTools.ImageIndex = 0;
            this.treeViewAllTools.ImageList = this.imageList1;
            this.treeViewAllTools.Location = new System.Drawing.Point(3, 17);
            this.treeViewAllTools.Name = "treeViewAllTools";
            this.treeViewAllTools.SelectedImageIndex = 0;
            this.treeViewAllTools.Size = new System.Drawing.Size(252, 186);
            this.treeViewAllTools.TabIndex = 1;
            this.treeViewAllTools.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewAllTools_NodeMouseClick);
            this.treeViewAllTools.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewAllTools_NodeMouseDoubleClick);
            this.treeViewAllTools.MouseEnter += new System.EventHandler(this.treeViewAllTools_MouseEnter);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "options.png");
            this.imageList1.Images.SetKeyName(1, "UnStart.png");
            this.imageList1.Images.SetKeyName(2, "OK.png");
            this.imageList1.Images.SetKeyName(3, "ng.png");
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(3, 17);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(252, 47);
            this.textBox2.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewAllTools);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 206);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具总览";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.38596F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.61403F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 285);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 67);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "状态";
            // 
            // ToolShowUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ToolShowUnit";
            this.Size = new System.Drawing.Size(264, 285);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 添加工具组ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除工具组ToolStripMenuItem;
        private System.Windows.Forms.TreeView treeViewAllTools;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ImageList imageList1;
    }
}
