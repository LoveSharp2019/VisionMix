using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Common.Basic
{
    public partial class frmImputText : Form
    {
        public string ImputText { get; set; }
        public frmImputText(string description)
        {
            InitializeComponent();
            this.Text =string.Format("请输入{0}:", description);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ImputText = txtData.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
