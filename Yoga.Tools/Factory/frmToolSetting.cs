using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yoga.Tools
{
    public partial class frmToolSetting : Form
    {
        private ToolsSettingUnit settingUnit;


        public frmToolSetting(ToolsSettingUnit settingUnit)
        {
            InitializeComponent();
            this.settingUnit = settingUnit;
            Size size = settingUnit.Size;
            this.Size = new Size(size.Width + 10, size.Height + 10);
            this.panel1.Dock = DockStyle.Fill;
            settingUnit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(settingUnit);
        }

        private void frmToolSetting_Load(object sender, EventArgs e)
        {
            settingUnit.ShowTranResult();
        }

        private void frmToolSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                settingUnit.Tool.SerializeCheck();
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show("数据异常:" +ex.Message+ " 是否继续退出?",
                   "异常", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            settingUnit.Clear();
        }
    }

}
