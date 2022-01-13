using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Yoga.Common.UI
{
    public partial class AgileProgressBar : UserControl
    {
        public AgileProgressBar()
        {
            InitializeComponent();
        }

        #region Total
        private int total = 10;
        public int Total
        {
            get { return total; }
            set { total = value; }
        } 
        #endregion

        public void SetProgress(int val)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new  Action<int> (this.SetProgress), val);
            }
            else
            {
                this.progressBar1.Maximum = this.total;
                this.progressBar1.Value = val;

                int percent = val *1000 /this.total ;
                this.label1.Text = (percent / 10.0).ToString() + "%";
            }
        }

        public void MessageBoxShow(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(this.MessageBoxShow), msg);
            }
            else
            {
                MessageBox.Show(msg);
            }

        }
    }   
}
