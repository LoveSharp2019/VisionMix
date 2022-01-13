using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Tools.Factory
{
    public partial class SacleImagePretreatmentUnit : UserControl
    {

        bool locked = false;

        public SacleImagePretreatmentUnit()
        {
            InitializeComponent();
        }

        private void ScaleUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
        }

        private void OffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
        }
    }
}
