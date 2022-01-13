namespace Yoga.VisionMix.Frame
{
    partial class frmProjectManger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentProject = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbAllProject = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddNewProject = new System.Windows.Forms.Button();
            this.btnSetCurrentProject = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.5F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 262);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtCurrentProject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(174, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(107, 125);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前项目";
            // 
            // txtCurrentProject
            // 
            this.txtCurrentProject.Location = new System.Drawing.Point(3, 32);
            this.txtCurrentProject.Name = "txtCurrentProject";
            this.txtCurrentProject.ReadOnly = true;
            this.txtCurrentProject.Size = new System.Drawing.Size(101, 21);
            this.txtCurrentProject.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsbAllProject);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(165, 256);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "所有项目";
            // 
            // lsbAllProject
            // 
            this.lsbAllProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbAllProject.FormattingEnabled = true;
            this.lsbAllProject.ItemHeight = 12;
            this.lsbAllProject.Location = new System.Drawing.Point(3, 17);
            this.lsbAllProject.Name = "lsbAllProject";
            this.lsbAllProject.Size = new System.Drawing.Size(159, 236);
            this.lsbAllProject.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSetCurrentProject);
            this.groupBox2.Controls.Add(this.btnAddNewProject);
            this.groupBox2.Location = new System.Drawing.Point(174, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(107, 100);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目操作";
            // 
            // btnAddNewProject
            // 
            this.btnAddNewProject.Location = new System.Drawing.Point(23, 20);
            this.btnAddNewProject.Name = "btnAddNewProject";
            this.btnAddNewProject.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewProject.TabIndex = 4;
            this.btnAddNewProject.Text = "新增项目";
            this.btnAddNewProject.UseVisualStyleBackColor = true;
            this.btnAddNewProject.Click += new System.EventHandler(this.btnAddNewProject_Click);
            // 
            // btnSetCurrentProject
            // 
            this.btnSetCurrentProject.Location = new System.Drawing.Point(23, 49);
            this.btnSetCurrentProject.Name = "btnSetCurrentProject";
            this.btnSetCurrentProject.Size = new System.Drawing.Size(75, 23);
            this.btnSetCurrentProject.TabIndex = 5;
            this.btnSetCurrentProject.Text = "置为当前";
            this.btnSetCurrentProject.UseVisualStyleBackColor = true;
            this.btnSetCurrentProject.Click += new System.EventHandler(this.btnSetCurrentProject_Click);
            // 
            // frmProjectManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmProjectManger";
            this.Text = "项目管理";
            this.Load += new System.EventHandler(this.frmProjectManger_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCurrentProject;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddNewProject;
        private System.Windows.Forms.ListBox lsbAllProject;
        private System.Windows.Forms.Button btnSetCurrentProject;
    }
}