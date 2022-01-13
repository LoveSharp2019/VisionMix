namespace Yoga.Common.UI
{
    partial class frmWaitingBox
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewMessage = new System.Windows.Forms.DataGridView();
            this.columnMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.dataGridViewMessage);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.labMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 148);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Yoga.Common.Properties.Resources.waiting;
            this.pictureBox1.Location = new System.Drawing.Point(3, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(143, 133);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labMessage.Location = new System.Drawing.Point(152, 8);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(170, 16);
            this.labMessage.TabIndex = 0;
            this.labMessage.Text = "正在处理数据，请稍后...";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridViewMessage
            // 
            this.dataGridViewMessage.AllowUserToAddRows = false;
            this.dataGridViewMessage.AllowUserToDeleteRows = false;
            this.dataGridViewMessage.AllowUserToResizeRows = false;
            this.dataGridViewMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMessage.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.dataGridViewMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMessage.ColumnHeadersVisible = false;
            this.dataGridViewMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnMessage});
            this.dataGridViewMessage.Location = new System.Drawing.Point(155, 27);
            this.dataGridViewMessage.MultiSelect = false;
            this.dataGridViewMessage.Name = "dataGridViewMessage";
            this.dataGridViewMessage.ReadOnly = true;
            this.dataGridViewMessage.RowHeadersVisible = false;
            this.dataGridViewMessage.RowTemplate.Height = 21;
            this.dataGridViewMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMessage.Size = new System.Drawing.Size(187, 114);
            this.dataGridViewMessage.TabIndex = 4;
            this.dataGridViewMessage.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewMessage_RowStateChanged);
            // 
            // columnMessage
            // 
            this.columnMessage.HeaderText = "Message";
            this.columnMessage.Name = "columnMessage";
            // 
            // frmWaitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(353, 156);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWaitingBox";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWaitingBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWaitingBox_FormClosing);
            this.Shown += new System.EventHandler(this.frmWaitingBox_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labMessage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dataGridViewMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMessage;
    }
}