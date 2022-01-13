using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.ImageControl;
using HalconDotNet;
using Yoga.Tools.Matching;

namespace Yoga.Tools.DynThreshold
{
    public partial class DynThresholdToolUnit : ToolsSettingUnit
    {
        private DynThresholdTool tool;
        /// <summary>halcon窗体控制</summary>
        private HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
        private ROIController ROIController;
        public DynThresholdToolUnit(DynThresholdTool tool)
        {
            InitializeComponent();

            this.tool = tool;

            mView = hWndUnit1.HWndCtrl;
            ROIController = new ROIController();
            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);

            if (tool.ROIList != null)
            {
                ROIController.ROIList = tool.ROIList;
            }
            locked = true;
            Init();
            HideMax();
            HideMin();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void ShowResult()
        {
            btnTest_Click(null, null);
        }
        private void Init()
        {
            UpDownMaskWidth.Value = tool.MaskWidth;
            UpDownMaskHeight.Value = tool.MaskHeight;
            UpDownOffset.Value = tool.Offset;
            cmbLightDark.DataSource = System.Enum.GetNames(typeof(LightDark));
            cmbLightDark.SelectedIndex = this.cmbLightDark.FindString(tool.LightDark.ToString());

            OpeningRadiusUpDown.Value = (decimal)tool.OpeningRadius;
            ClosingRadiusUpDown.Value = (decimal)tool.ClosingRadius;

            leastAreaUpDown.Value = (decimal)tool.LeastArea;

            Mat2DManger mat2DManger = tool.Mat2DManger;
            mat2DManger.UseMat2DAlways = false;
            mat2DMangerUnit1.Init(mat2DManger);


            mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);
            if (tool.MatchingTool != null)
            {
                ToolBase toolTmp = tool as ToolBase;
                ShowRefImage();
            }
        }

        private void ShowRefImage()
        {
            ChangeShowMode(ShowMode.IncludeROI);
            mView.ClearList();
            mView.AddIconicVar(tool.ImageRefIn);
           // tool.ShowResult(mView);
            mView.Repaint();
        }
        private void ChangeShowMode(ShowMode showMode)
        {
            mView.SetDispLevel(showMode);
            switch (showMode)
            {
                case ShowMode.IncludeROI:
                    groupBoxCreateROI.Enabled = true;
                    break;
                case ShowMode.ExcludeROI:
                    groupBoxCreateROI.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void On_SelectMatchingTool(IMatching matchingTool)
        {
            if (matchingTool == null )
            {
                return;
            }
            if (locked)
            {
                return;
            }
            tool.MatchingTool = matchingTool;

            ShowRefImage();
        }

        private void UpdateViewData(object sender, ViewEventArgs e)
        {
            switch (e.ViewMessage)
            {
                case ViewMessage.ChangedROISign:
                case ViewMessage.DeletedActROI:
                case ViewMessage.DelectedAllROIs:
                case ViewMessage.UpdateROI:
                case ViewMessage.ActivatedROI:
                case ViewMessage.CreatedROI:
                    bool genROI = ROIController.DefineModelROI();
                    tool.ModelRegion = ROIController.GetModelRegion();
                    tool.ROIList = ROIController.ROIList;
                    btnTest_Click(null, null);
                    break;
                case ViewMessage.ErrReadingImage:

                    break;
                default:
                    break;
            }
        }
        private void UpDownMaskWidth_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }

            tool.MaskWidth = (int)UpDownMaskWidth.Value;
            btnTest_Click(null, null);
        }

        private void UpDownMaskHeight_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MaskHeight = (int)UpDownMaskHeight.Value;
            btnTest_Click(null, null);
        }

        private void UpDownOffset_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.Offset = (int)UpDownOffset.Value;
            btnTest_Click(null, null);
        }

        private void cmbLightDark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.LightDark = (LightDark)Enum.Parse(typeof(LightDark), cmbLightDark.SelectedItem.ToString(), false);
            btnTest_Click(null, null);
        }

        private void OpeningRadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.OpeningRadius =(double) OpeningRadiusUpDown.Value;
            btnTest_Click(null, null);
        }

        private void ClosingRadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }

            tool.ClosingRadius = (double)ClosingRadiusUpDown.Value;
            btnTest_Click(null, null);
        }
        private void leastAreaUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.LeastArea = (double)leastAreaUpDown.Value;
            btnTest_Click(null, null);
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            lblResult.Text = "运行中...";
            //if (tool.Mat2DManger.UseMat2D)
            //{
            //    ToolBase tool1 = tool.Mat2DManger.MatchingTool as ToolBase;
            //    tool1.Run(tool.ImageRefIn);
            //}
          
            tool.RunRef();

            mView.ClearList();
            mView.AddIconicVar(tool.ImageRefIn);
            tool.RuningFinish = true;
            tool.ShowResult(mView);
            mView.Repaint();

            if (tool.IsOk == false)
            {
                int number;
                if (int.TryParse(tool.Result, out number))
                {
                    lblResult.Text = string.Format("找到异常个数为: {0},用时 : {1} ms", tool.Result, tool.ExecutionTime);
                }
                else
                {
                    lblResult.Text = string.Format("检查失败,用时 : {0} ms", tool.ExecutionTime);

                }
            }
            else
            {
                lblResult.Text = string.Format("未找到异常,用时 : {0} ms", tool.ExecutionTime);

            }
        }

        private void addToROIButton_CheckedChanged(object sender, EventArgs e)
        {
            if (addToROIButton.Checked)
                ROIController.SetROISign(ROIOperation.Positive);
        }

        private void subFromROIButton_CheckedChanged(object sender, EventArgs e)
        {
            if (subFromROIButton.Checked)
                ROIController.SetROISign(ROIOperation.Negative);
        }

        private void rect1Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROIRectangle1());
        }

        private void rect2Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROIRectangle2("检查区域"));
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROICircle());
        }

        private void delROIButton_Click(object sender, EventArgs e)
        {

            ROIController.RemoveActive();
        }

        private void delAllROIButton_Click(object sender, EventArgs e)
        {
            ROIController.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DynThresholdToolUnit_Load(object sender, EventArgs e)
        {
            if (tool.ImageRefIn != null)
            {
                mView.ClearList();
                mView.AddIconicVar(tool.ImageRefIn);
                mView.Repaint();
            }
        }
    }
}
