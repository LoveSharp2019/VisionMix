namespace Yoga.Tools.AngleFind
{
    partial class AngleFindToolUnit
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCreateModel = new System.Windows.Forms.TabPage();
            this.groupBoxCreateModel = new System.Windows.Forms.GroupBox();
            this.TestModelButton = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.hWndUnit1 = new Yoga.ImageControl.HWndUnit();
            this.btnSetCircleROI = new System.Windows.Forms.Button();
            this.btnSetAngleROI = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageCreateModel.SuspendLayout();
            this.groupBoxCreateModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "图像文件 |*.bmp;*.png;*.tif;*.jpg|all files (*.*)|*.*";
            this.openFileDialog2.Multiselect = true;
            // 
            // mat2DMangerUnit1
            // 
            this.mat2DMangerUnit1.Location = new System.Drawing.Point(16, 20);
            this.mat2DMangerUnit1.Name = "mat2DMangerUnit1";
            this.mat2DMangerUnit1.Size = new System.Drawing.Size(208, 120);
            this.mat2DMangerUnit1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "字符长度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "字符起始位";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCreateModel);
            this.tabControl.Location = new System.Drawing.Point(354, 72);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(359, 513);
            this.tabControl.TabIndex = 76;
            // 
            // tabPageCreateModel
            // 
            this.tabPageCreateModel.Controls.Add(this.groupBoxCreateModel);
            this.tabPageCreateModel.Location = new System.Drawing.Point(4, 22);
            this.tabPageCreateModel.Name = "tabPageCreateModel";
            this.tabPageCreateModel.Size = new System.Drawing.Size(351, 487);
            this.tabPageCreateModel.TabIndex = 0;
            this.tabPageCreateModel.Text = "  创建模型";
            // 
            // groupBoxCreateModel
            // 
            this.groupBoxCreateModel.Controls.Add(this.btnSetAngleROI);
            this.groupBoxCreateModel.Controls.Add(this.btnSetCircleROI);
            this.groupBoxCreateModel.Controls.Add(this.TestModelButton);
            this.groupBoxCreateModel.Controls.Add(this.mat2DMangerUnit1);
            this.groupBoxCreateModel.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCreateModel.Name = "groupBoxCreateModel";
            this.groupBoxCreateModel.Size = new System.Drawing.Size(342, 484);
            this.groupBoxCreateModel.TabIndex = 55;
            this.groupBoxCreateModel.TabStop = false;
            // 
            // TestModelButton
            // 
            this.TestModelButton.Location = new System.Drawing.Point(30, 281);
            this.TestModelButton.Name = "TestModelButton";
            this.TestModelButton.Size = new System.Drawing.Size(105, 30);
            this.TestModelButton.TabIndex = 73;
            this.TestModelButton.Text = "测试图像";
            this.TestModelButton.Click += new System.EventHandler(this.TestModelButton_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(18, 361);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtResult.Size = new System.Drawing.Size(281, 182);
            this.txtResult.TabIndex = 79;
            this.txtResult.Text = "结果";
            // 
            // hWndUnit1
            // 
            this.hWndUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.hWndUnit1.CameraMessage = null;
            this.hWndUnit1.Location = new System.Drawing.Point(2, 72);
            this.hWndUnit1.Name = "hWndUnit1";
            this.hWndUnit1.Size = new System.Drawing.Size(346, 271);
            this.hWndUnit1.TabIndex = 80;
            // 
            // btnSetCircleROI
            // 
            this.btnSetCircleROI.Location = new System.Drawing.Point(41, 171);
            this.btnSetCircleROI.Name = "btnSetCircleROI";
            this.btnSetCircleROI.Size = new System.Drawing.Size(75, 23);
            this.btnSetCircleROI.TabIndex = 74;
            this.btnSetCircleROI.Text = "确定圆形";
            this.btnSetCircleROI.UseVisualStyleBackColor = true;
            this.btnSetCircleROI.Click += new System.EventHandler(this.btnSetCircleROI_Click);
            // 
            // btnSetAngleROI
            // 
            this.btnSetAngleROI.Location = new System.Drawing.Point(152, 170);
            this.btnSetAngleROI.Name = "btnSetAngleROI";
            this.btnSetAngleROI.Size = new System.Drawing.Size(75, 23);
            this.btnSetAngleROI.TabIndex = 75;
            this.btnSetAngleROI.Text = "确定角度";
            this.btnSetAngleROI.UseVisualStyleBackColor = true;
            this.btnSetAngleROI.Click += new System.EventHandler(this.btnSetAngleROI_Click);
            // 
            // AngleFindToolUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.hWndUnit1);
            this.Controls.Add(this.tabControl);
            this.Name = "AngleFindToolUnit";
            this.Load += new System.EventHandler(this.Code1DToolUnit_Load);
            this.Controls.SetChildIndex(this.tabControl, 0);
            this.Controls.SetChildIndex(this.hWndUnit1, 0);
            this.Controls.SetChildIndex(this.txtResult, 0);
            this.tabControl.ResumeLayout(false);
            this.tabPageCreateModel.ResumeLayout(false);
            this.groupBoxCreateModel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCreateModel;
        private System.Windows.Forms.GroupBox groupBoxCreateModel;
        private System.Windows.Forms.Button TestModelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResult;
        private ImageControl.HWndUnit hWndUnit1;
        private Mat2DMangerUnit mat2DMangerUnit1;
        private System.Windows.Forms.Button btnSetAngleROI;
        private System.Windows.Forms.Button btnSetCircleROI;
    }
}
