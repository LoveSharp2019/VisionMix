using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.Common.Basic;

namespace Yoga.VisionMix.Frame
{
    public partial class frmProjectManger : Form
    {
        string path = Environment.CurrentDirectory + "\\project";
        public frmProjectManger()
        {
            InitializeComponent();
        }

        private void btnAddNewProject_Click(object sender, EventArgs e)
        {
            frmImputText frm = new frmImputText("项目名称");
            DialogResult result = frm.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            if (lsbAllProject.Items.Contains(frm.ImputText)!=false)
            {
                MessageBox.Show("名称");
            }
        }

        private void frmProjectManger_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(path))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(path); //新建文件夹   
            }
            List<string> picPathList = new List<string>();
            lsbAllProject.Items.Clear();
            //获取指定文件夹的所有文件  
            string[] paths = Directory.GetFiles(path);
            foreach (var item in paths)
            {
                //获取文件后缀名  
                string extension = Path.GetExtension(item).ToLower();
                if (extension == ".prj")
                {
                    string prj = Path.GetFileNameWithoutExtension(item);
                    lsbAllProject.Items.Add(prj);
                }
            }
            txtCurrentProject.Text = Path.GetFileNameWithoutExtension(UserSetting.Instance.ProjectPath);

        }

        private void btnSetCurrentProject_Click(object sender, EventArgs e)
        {
            string name = lsbAllProject.SelectedItem.ToString();
            if (txtCurrentProject.Text== name)
            {
                return;
            }
            UserSetting.Instance.ProjectPath = name;
            txtCurrentProject.Text = name;
        }
    }
}
