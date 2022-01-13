namespace Yoga.Tools.ExtractColor
{
    partial class ExtractColorToolUnit
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
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.mat2DMangerUnit1 = new Yoga.Tools.Mat2DMangerUnit();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCreateModel = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.btnResetPoint = new System.Windows.Forms.Button();
            this.btnDelPoint = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.roiActUnit1 = new Yoga.ImageControl.ROIActUnit();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.tabControl.SuspendLayout();
            this.tabPageCreateModel.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // mat2DMangerUnit1
            // 
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(23, 312);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 83;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCreateModel);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(576, 62);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(480, 560);
            this.tabControl.TabIndex = 76;
            // 
            // tabPageCreateModel
            // 
            this.tabPageCreateModel.Controls.Add(this.groupBoxCreateModel);
            this.tabPageCreateModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateModel.Name = "tabPageCreateModel";
            this.tabPageCreateModel.Size = new System.Drawing.Size(472, 534);
            this.tabPageCreateModel.TabIndex = 0;
            this.tabPageCreateModel.Text = "  创建模型";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.btnResetPoint);
            this.groupBoxCreateModel.Controls.Add(this.btnDelPoint);
            this.groupBoxCreateModel.Controls.Add(this.btnAddPoint);
            this.groupBoxCreateModel.Controls.Add(this.mat2DMangerUnit1);
            this.groupBoxCreateModel.Controls.Add(this.TestModelButton);
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(470, 526);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // btnResetPoint
            // 
            this.btnResetPoint.Location = new System.Drawing.Point(30, 197);
            this.btnResetPoint.Name = "btnResetPoint";
            this.btnResetPoint.Size = new System.Drawing.Size(105, 30);
            this.btnResetPoint.TabIndex = 86;
            this.btnResetPoint.Text = "重置检测点";
            this.btnResetPoint.Click += new System.EventHandler(this.btnResetPoint_Click);
            // 
            // btnDelPoint
            // 
            this.btnDelPoint.Location = new System.Drawing.Point(30, 161);
            this.btnDelPoint.Name = "btnDelPoint";
            this.btnDelPoint.Size = new System.Drawing.Size(105, 30);
            this.btnDelPoint.TabIndex = 85;
            this.btnDelPoint.Text = "移除检测点";
            this.btnDelPoint.Click += new System.EventHandler(this.btnDelPoint_Click);
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(30, 125);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(105, 30);
            this.btnAddPoint.TabIndex = 84;
            this.btnAddPoint.Text = "添加检测点(&F)";
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(30, 276);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(105, 30);
            this.TestModelButton.TabIndex = 73;
            this.TestModelButton.Text = "测试图像";
            this.TestModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(19, 446);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(281, 182);
            this.txtResult.TabIndex = 80;
            this.txtResult.Text = "第一行\\r\\n第二行";
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(5, 62);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(565, 378);
            this.hWndUnit1.TabIndex = 81;
            // 
            // roiActUnit1
            // 
            this.roiActUnit1.Location = new System.Drawing.Point(336, 423);
            this.roiActUnit1.Name = "roiActUnit1";
            this.roiActUnit1.RoiController = null;
            this.roiActUnit1.Size = new System.Drawing.Size(202, 155);
            this.roiActUnit1.TabIndex = 82;
            this.roiActUnit1.UseSearchRoi = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNote);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(472, 534);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "说明";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtNote
            // 
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Location = new System.Drawing.Point(0, 0);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ReadOnly = true;
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtNote.Size = new System.Drawing.Size(472, 534);
            this.txtNote.TabIndex = 81;
            this.txtNote.Text = "第一行\\r\\n第二行";
            // 
            // ExtractColorToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roiActUnit1);
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.tabControl);
            this.Name = "ExtractColorToolUnit";
            this.Size = new System.Drawing.Size(1110, 640);
            this.Load += new System.EventHandler(this.Code2DToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.roiActUnit1, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCreateModel;
        private System.Windows.Forms.GroupBox groupBoxCreateModel;
        private System.Windows.Forms.Button TestModelButton;
        private System.Windows.Forms.TextBox txtResult;
        private ImageControl.HWndUnit hWndUnit1;
        private Mat2DMangerUnit mat2DMangerUnit1;
        private ImageControl.ROIActUnit roiActUnit1;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button btnDelPoint;
        private System.Windows.Forms.Button btnResetPoint;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtNote;
    }
}
