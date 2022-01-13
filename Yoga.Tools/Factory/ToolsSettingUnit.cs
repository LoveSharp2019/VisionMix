using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yoga.Tools
{
    [ToolboxItem(false)]
    public  partial class ToolsSettingUnit : UserControl
    {
        Tools.ToolBase tool;
        protected bool locked = false;
        public ToolBase Tool
        {
            get
            {
                return tool;
            }
        }

        public void HideMax()
        {
            txtCommMax.Visible = false;
            labelComm2.Visible = false;
        }
        public void HideMin()
        {
            txtCommMin.Visible = false;
            labelComm1.Visible = false;
        }

        public void Init(string name, ToolBase tool)
        {
            this.lblCommToolsName.Text = name+":";
            this.tool = tool;
            txtCommMin.Text = tool.Min.ToString();
            txtCommMax.Text = tool.Max.ToString();
            txtCommNote.Text = tool.Note;
            chkIsOutputResults.Checked = tool.IsOutputResults;

            if (tool is CreateImage.CreateImageTool)
            {
                cmbImageSoure.Visible = false;
                labelCommImageSoure.Visible = false;
            }
            else
            {
                List<ToolBase> tools = ToolsFactory.GetToolList(tool.SettingIndex);
                cmbImageSoure.Items.Clear();
                foreach (ToolBase item in tools)
                {
                    if (item==this.tool)
                    {
                        break;
                    }
                    if (item is ICreateImage)
                    {
                        cmbImageSoure.Items.Add(item.Name);
                    }
                }
                cmbImageSoure.Text = tool.ImageSoureToolName;
            }
            if (tool.IsSubTool)
            {
                groupBoxCommSetting.Visible = false;
            }
        }

        public ToolsSettingUnit()
        {
            InitializeComponent();
            
        }
        public virtual void ShowTranResult()
        {

        }
        public virtual void ShowResult()
        {

        }
        public virtual void Clear()
        {
            if (tool==null)
            {
                return;
            }
            tool.ClearTestData();
            //将显示控件释放
            foreach (var item in this.Controls)
            {
                if (item is ImageControl.HWndUnit)
                {
                    ((ImageControl.HWndUnit)item).Dispose();
                }
                
            }
            tool.ClearResult();
            //foreach (var item in ToolsFactory.ToolsDic[tool.SettingIndex])
            //{
            //    item.ClearResult();
            //}
        }

        private void txtCommMin_TextChanged(object sender, System.EventArgs e)
        {
            if (locked)
            {
                return;
            }
            double min;
            if (double.TryParse(txtCommMin.Text, out min) == false)
            {
                return;
            }
            tool.Min = min;
        }

        private void txtCommMax_TextChanged(object sender, System.EventArgs e)
        {
            if (locked)
            {
                return;
            }
            double max;
            if (double.TryParse(txtCommMax.Text, out max) == false)
            {
                return;
            }
            tool.Max = max;
        }

        private void chkIsOutputResults_CheckedChanged(object sender, System.EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.IsOutputResults = chkIsOutputResults.Checked;
        }

        private void txtCommNote_TextChanged(object sender, System.EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.Note = txtCommNote.Text;
        }

        private void cmbImageSoure_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.ImageSoureToolName = cmbImageSoure.Text;
            ShowResult();
        }
    }
}
