using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Tools.Factory
{
    public partial class frmSelectCameraIndex : Form
    {
        int cameraIndex = 1;

        public int CameraIndex
        {
            get
            {
                return cameraIndex;
            }
            private set
            {
                cameraIndex = value;
            }
        }

        public frmSelectCameraIndex(int cameraWant)
        {
            InitializeComponent();

            cmbCameraSelect.Items.Clear();
            int count = Camera.CameraManger.CameraDic.Count;
            for (int i = 1; i < count + 1; i++)
            {
                cmbCameraSelect.Items.Add(string.Format("相机{0}", i));
            }
            if (cameraWant< count + 1)
            {
                cmbCameraSelect.SelectedIndex = cameraWant-1;
            }
            
        }

        private void cmbCameraSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraIndex = cmbCameraSelect.SelectedIndex+1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
                this.DialogResult = DialogResult.OK;
            
        }
    }
}
