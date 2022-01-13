using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Tools.CreateImage
{
    public partial class CreateImageUnit : ToolsSettingUnit
    {
        CreateImageTool tool;
        public CreateImageUnit(CreateImageTool tool)
        {
            InitializeComponent();
            this.tool = tool;
            locked = true;
            Init();
            HideMax();
            HideMin();
            base.Init(tool.Name, tool);
            locked = false;
        }

        private void Init()
        {
            UpDownCameraIndex.Value = tool.CameraIndex;
            chkOffLine.Checked = tool.OffLineMode;
            txtOffLinePath.Text = tool.OffLineImagePath;
        }
        public override void ShowResult()
        {
        }
        private void UpDownCameraIndex_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.CameraIndex = (int)UpDownCameraIndex.Value;
        }

        private void btnGetImagePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择图片文件存放目录";
            if (tool.OffLineImagePath != "")
            {
                folder.SelectedPath = tool.OffLineImagePath;
            }
            
            if (folder.ShowDialog() == DialogResult.OK)
            {

                string sPath = folder.SelectedPath;
                tool.OffLineImagePath = sPath;
                txtOffLinePath.Text = sPath;
            }
        }

        private void chkOffLine_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.OffLineMode = chkOffLine.Checked;
        }

        private void btnGetImage_Click(object sender, EventArgs e)
        {
            tool.SetExtTriggerDataOff();
            tool.Run();
            tool.ShowImage(hWndUnit1.HWndCtrl);
            hWndUnit1.HWndCtrl.Repaint();
        }

        private void btnSetRefImage_Click(object sender, EventArgs e)
        {
            if (tool.ImageTestOut == null|| tool.ImageTestOut.IsInitialized()==false)
            {
                MessageBox.Show("未采集到图像数据,请先采集图像", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tool.RefImage = tool.ImageTestOut.CopyImage();
            MessageBox.Show("模板图像设置完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowRefImage();
        }

        private void btnShowRefImage_Click(object sender, EventArgs e)
        {
            ShowRefImage();
        }

        private void  ShowRefImage()
        {
            if (tool.RefImage != null && tool.RefImage.IsInitialized())
            {
                hWndUnit1.HWndCtrl.AddIconicVar(tool.RefImage);
                hWndUnit1.HWndCtrl.AddText("模板图像", 20, 20, 20, "blue");
                hWndUnit1.HWndCtrl.Repaint();
            }
        }
        private void CreateImageUnit_Load(object sender, EventArgs e)
        {
            ShowRefImage();
        }
    }
}
