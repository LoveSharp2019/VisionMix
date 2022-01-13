

namespace Yoga.VisionMix.Units
{
    partial class AutoUnit
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUnit));
            this.openImageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmAddTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDelTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSavePrj = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMax8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelHWnd = new System.Windows.Forms.TableLayoutPanel();
            this.panelHWndMax = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvResultShow = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.brnTestNgSigin = new System.Windows.Forms.Button();
            this.btnInterlocking = new System.Windows.Forms.Button();
            this.btnDeleteResult = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.toolShowUnit1 = new Yoga.Tools.Factory.ToolShowUnit();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tscobCameraSelect = new System.Windows.Forms.ToolStripComboBox();
            this.tsbCamera1Setting = new System.Windows.Forms.ToolStripButton();
            this.tsbVido = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCameraTest = new System.Windows.Forms.ToolStripButton();
            this.tsbShowAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStripProject.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultShow)).BeginInit();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openImageFileDialog
            // 
            this.openImageFileDialog.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openImageFileDialog.RestoreDirectory = true;
            // 
            // timerInit
            // 
            this.timerInit.Tick += new System.EventHandler(this.timerInit_Tick);
            // 
            // contextMenuStripProject
            // 
            this.contextMenuStripProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddTool,
            this.tsbDelTool,
            this.tsbSavePrj});
            this.contextMenuStripProject.Name = "contextMenuStripProject";
            this.contextMenuStripProject.Size = new System.Drawing.Size(193, 70);
            // 
            // tsmAddTool
            // 
            this.tsmAddTool.Name = "tsmAddTool";
            this.tsmAddTool.Size = new System.Drawing.Size(192, 22);
            this.tsmAddTool.Text = "添加工具";
            // 
            // tsbDelTool
            // 
            this.tsbDelTool.Name = "tsbDelTool";
            this.tsbDelTool.Size = new System.Drawing.Size(192, 22);
            this.tsbDelTool.Text = "删除工具";
            // 
            // tsbSavePrj
            // 
            this.tsbSavePrj.Name = "tsbSavePrj";
            this.tsbSavePrj.Size = new System.Drawing.Size(192, 22);
            this.tsbSavePrj.Text = "toolStripMenuItem1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.64975F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.35025F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1379, 712);
            this.tableLayoutPanel1.TabIndex = 83;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tableLayoutPanel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1051, 706);
            this.panel4.TabIndex = 53;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.10811F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.89189F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1051, 706);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 580);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanelMax8);
            this.panel3.Controls.Add(this.tableLayoutPanelHWnd);
            this.panel3.Controls.Add(this.panelHWndMax);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1045, 580);
            this.panel3.TabIndex = 0;
            // 
            // tableLayoutPanelMax8
            // 
            this.tableLayoutPanelMax8.ColumnCount = 3;
            this.tableLayoutPanelMax8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.Location = new System.Drawing.Point(685, 68);
            this.tableLayoutPanelMax8.Name = "tableLayoutPanelMax8";
            this.tableLayoutPanelMax8.RowCount = 3;
            this.tableLayoutPanelMax8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelMax8.Size = new System.Drawing.Size(94, 44);
            this.tableLayoutPanelMax8.TabIndex = 92;
            // 
            // tableLayoutPanelHWnd
            // 
            this.tableLayoutPanelHWnd.ColumnCount = 2;
            this.tableLayoutPanelHWnd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHWnd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHWnd.Location = new System.Drawing.Point(461, 108);
            this.tableLayoutPanelHWnd.Name = "tableLayoutPanelHWnd";
            this.tableLayoutPanelHWnd.RowCount = 2;
            this.tableLayoutPanelHWnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHWnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHWnd.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanelHWnd.TabIndex = 91;
            // 
            // panelHWndMax
            // 
            this.panelHWndMax.Location = new System.Drawing.Point(58, 99);
            this.panelHWndMax.Name = "panelHWndMax";
            this.panelHWndMax.Size = new System.Drawing.Size(358, 203);
            this.panelHWndMax.TabIndex = 90;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 589);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 114);
            this.panel1.TabIndex = 88;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.4689F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.5311F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1045, 114);
            this.tableLayoutPanel4.TabIndex = 89;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvResultShow);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(814, 108);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果统计";
            // 
            // dgvResultShow
            // 
            this.dgvResultShow.AllowUserToAddRows = false;
            this.dgvResultShow.AllowUserToDeleteRows = false;
            this.dgvResultShow.AllowUserToResizeRows = false;
            this.dgvResultShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultShow.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResultShow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResultShow.ColumnHeadersHeight = 18;
            this.dgvResultShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultShow.Location = new System.Drawing.Point(3, 17);
            this.dgvResultShow.MultiSelect = false;
            this.dgvResultShow.Name = "dgvResultShow";
            this.dgvResultShow.ReadOnly = true;
            this.dgvResultShow.RowHeadersVisible = false;
            this.dgvResultShow.RowTemplate.Height = 21;
            this.dgvResultShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultShow.Size = new System.Drawing.Size(808, 88);
            this.dgvResultShow.TabIndex = 89;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.brnTestNgSigin);
            this.panel6.Controls.Add(this.btnInterlocking);
            this.panel6.Controls.Add(this.btnDeleteResult);
            this.panel6.Controls.Add(this.btnRun);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(823, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(219, 108);
            this.panel6.TabIndex = 0;
            // 
            // brnTestNgSigin
            // 
            this.brnTestNgSigin.Location = new System.Drawing.Point(18, 4);
            this.brnTestNgSigin.Name = "brnTestNgSigin";
            this.brnTestNgSigin.Size = new System.Drawing.Size(75, 23);
            this.brnTestNgSigin.TabIndex = 87;
            this.brnTestNgSigin.Text = "异常测试";
            this.brnTestNgSigin.UseVisualStyleBackColor = true;
            this.brnTestNgSigin.Visible = false;
            this.brnTestNgSigin.Click += new System.EventHandler(this.brnTestNgSigin_Click);
            // 
            // btnInterlocking
            // 
            this.btnInterlocking.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInterlocking.Location = new System.Drawing.Point(18, 62);
            this.btnInterlocking.Name = "btnInterlocking";
            this.btnInterlocking.Size = new System.Drawing.Size(75, 27);
            this.btnInterlocking.TabIndex = 87;
            this.btnInterlocking.Text = "联机";
            this.btnInterlocking.UseVisualStyleBackColor = true;
            this.btnInterlocking.Visible = false;
            this.btnInterlocking.Click += new System.EventHandler(this.btnInterlocking_Click);
            // 
            // btnDeleteResult
            // 
            this.btnDeleteResult.Location = new System.Drawing.Point(18, 33);
            this.btnDeleteResult.Name = "btnDeleteResult";
            this.btnDeleteResult.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteResult.TabIndex = 86;
            this.btnDeleteResult.Text = "计数清零";
            this.btnDeleteResult.UseVisualStyleBackColor = true;
            this.btnDeleteResult.Click += new System.EventHandler(this.btnDeleteResult_Click);
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.Location = new System.Drawing.Point(112, 10);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(94, 63);
            this.btnRun.TabIndex = 88;
            this.btnRun.Text = "运行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Visible = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.toolShowUnit1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBoxInfo, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1060, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.91139F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.08861F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(316, 706);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // toolShowUnit1
            // 
            this.toolShowUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolShowUnit1.Location = new System.Drawing.Point(3, 3);
            this.toolShowUnit1.Name = "toolShowUnit1";
            this.toolShowUnit1.Size = new System.Drawing.Size(310, 367);
            this.toolShowUnit1.TabIndex = 100;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.txtLog);
            this.groupBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInfo.Location = new System.Drawing.Point(3, 376);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(310, 327);
            this.groupBoxInfo.TabIndex = 101;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "日志";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(3, 17);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(304, 307);
            this.txtLog.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panel5);
            this.panelMain.Controls.Add(this.toolStrip1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1379, 778);
            this.panelMain.TabIndex = 84;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 66);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1379, 712);
            this.panel5.TabIndex = 9;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscobCameraSelect,
            this.tsbCamera1Setting,
            this.tsbVido,
            this.toolStripSeparator1,
            this.tsbCameraTest,
            this.tsbShowAll,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1379, 66);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tscobCameraSelect
            // 
            this.tscobCameraSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscobCameraSelect.Name = "tscobCameraSelect";
            this.tscobCameraSelect.Size = new System.Drawing.Size(121, 66);
            this.tscobCameraSelect.SelectedIndexChanged += new System.EventHandler(this.tscobCameraSelect_SelectedIndexChanged);
            // 
            // tsbCamera1Setting
            // 
            this.tsbCamera1Setting.Image = global::Yoga.VisionMix.Properties.Resources.Setting;
            this.tsbCamera1Setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCamera1Setting.Name = "tsbCamera1Setting";
            this.tsbCamera1Setting.Size = new System.Drawing.Size(60, 63);
            this.tsbCamera1Setting.Text = "相机设置";
            this.tsbCamera1Setting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCamera1Setting.Click += new System.EventHandler(this.tsbCamera1Setting_Click);
            // 
            // tsbVido
            // 
            this.tsbVido.AutoSize = false;
            this.tsbVido.Image = global::Yoga.VisionMix.Properties.Resources.video;
            this.tsbVido.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVido.Name = "tsbVido";
            this.tsbVido.Size = new System.Drawing.Size(60, 63);
            this.tsbVido.Text = "连续/取消";
            this.tsbVido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbVido.Click += new System.EventHandler(this.tsbVido_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 66);
            // 
            // tsbCameraTest
            // 
            this.tsbCameraTest.Image = global::Yoga.VisionMix.Properties.Resources.test;
            this.tsbCameraTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCameraTest.Name = "tsbCameraTest";
            this.tsbCameraTest.Size = new System.Drawing.Size(69, 63);
            this.tsbCameraTest.Text = " 测试/取消";
            this.tsbCameraTest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbCameraTest.Click += new System.EventHandler(this.tsbCamera_Click);
            // 
            // tsbShowAll
            // 
            this.tsbShowAll.Image = global::Yoga.VisionMix.Properties.Resources.saveAs;
            this.tsbShowAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowAll.Name = "tsbShowAll";
            this.tsbShowAll.Size = new System.Drawing.Size(60, 63);
            this.tsbShowAll.Text = "显示所有";
            this.tsbShowAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbShowAll.Click += new System.EventHandler(this.tsbShowAll_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 63);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // AutoUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelMain);
            this.Name = "AutoUnit";
            this.Size = new System.Drawing.Size(1379, 778);
            this.Load += new System.EventHandler(this.AutoUnit_Load);
            this.contextMenuStripProject.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultShow)).EndInit();
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openImageFileDialog;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripProject;
        private System.Windows.Forms.ToolStripMenuItem tsmAddTool;
        private System.Windows.Forms.ToolStripMenuItem tsbDelTool;
        private System.Windows.Forms.ToolStripMenuItem tsbSavePrj;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvResultShow;
        private System.Windows.Forms.Button btnInterlocking;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button brnTestNgSigin;
        private System.Windows.Forms.Button btnDeleteResult;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMax8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHWnd;
        private System.Windows.Forms.Panel panelHWndMax;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        public Tools.Factory.ToolShowUnit toolShowUnit1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbVido;
        private System.Windows.Forms.ToolStripButton tsbCamera1Setting;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStripButton tsbCameraTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbShowAll;
        private System.Windows.Forms.ToolStripComboBox tscobCameraSelect;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.TextBox txtLog;
    }
}
