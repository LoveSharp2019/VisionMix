using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Yoga.Tools
{
    public partial class Mat2DMangerUnit : UserControl
    {
        private List<string> mat2dTools;
        private Mat2DManger mat2dManger;

  

        public Mat2DMangerUnit()
        {
            InitializeComponent();
        }
        public void Init(Mat2DManger mat2dManger )
        {
            this.mat2dManger = mat2dManger;
            mat2dTools = mat2dManger.Mat2dTools;
            chkUseMat2D.Checked = mat2dManger.UseMat2D;
            if (mat2dManger.UseMat2DAlways)
            {
                chkUseMat2D.Enabled = false;
            }
            if (mat2dManger.CanSelectTool==false)
            {
                groupBoxSelectMat2DTool.Enabled = false;
            }
            foreach (var item in mat2dTools)
            {
                cmbMatchingToolSelect.Items.Add(item);
            }
            if (mat2dTools != null && mat2dTools.Count > 0)
            {
                int index = mat2dTools.FindIndex(x => x == mat2dManger.Mat2DToolName);
                if (index != -1)
                {
                    cmbMatchingToolSelect.SelectedIndex = index;
                }
            }
        }
        private void cmbMatchingToolSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mat2dManger==null)
            {
                return;
            }
            int index = cmbMatchingToolSelect.SelectedIndex;

            if (index > -1)
            {
                mat2dManger.Mat2DToolName = mat2dTools[index];
                IMatching matchingTool = ToolsFactory.GetToolList(mat2dManger.SettingIndex).Find(
                 x => x.Name == mat2dTools[index]) as IMatching;
                if (matchingTool != null)
                {
                    mat2dManger.Mat2DToolName = ((ToolBase)matchingTool).Name;
                    if (mat2dManger.SelectMatchingToolObserver!=null)
                    {
                        mat2dManger.SelectMatchingToolObserver(matchingTool);
                    }
                }
            }
        }

        private void chkUseMat2D_CheckedChanged(object sender, EventArgs e)
        {
            if (mat2dManger == null)
            {
                return;
            }
            cmbMatchingToolSelect.Enabled = chkUseMat2D.Checked;
            mat2dManger.UseMat2D = chkUseMat2D.Checked;
        }

        private void Mat2DMangerUnit_Load(object sender, EventArgs e)
        {
          

        }
    }
}
