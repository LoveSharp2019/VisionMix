using System;
using System.Windows.Forms;
using HalconDotNet;
using Yoga.ImageControl;
using System.Collections.Generic;
using System.Linq;

namespace Yoga.Tools.BarCode
{
    public partial class BarCodeToolUnit : ToolsSettingUnit
    {
        private BarCodeTool tool;
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;
        public BarCodeToolUnit(BarCodeTool tool)
        {
            InitializeComponent();
            mView = hWndUnit1.HWndCtrl;

            ROIController = new ROIController();

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);
            this.tool = tool;
            locked = true;
            Init();
            HideMax();
            HideMin();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void ShowResult()
        {
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

                    ROI searchRoi = ROIController.ROIList.Find(x => x.OperatorFlag == ROIOperation.None);
                    if (searchRoi != null)
                    {
                        tool.SearchRegion = searchRoi.GetRegion();
                        tool.ROIList = ROIController.ROIList;
                        ShowModelGraphics();
                    }
                    else
                    {
                        tool.SearchRegion = null;
                        tool.ROIList = ROIController.ROIList;
                        ShowModelGraphics();
                    }
                    break;
                case ViewMessage.ErrReadingImage:
                    //MessageBox.Show("文件读取异常 \n" + mView.exceptionText,
                    //    "一维码助手",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);
                    break;
                default:
                    break;
            }
        }
        private void Init()
        {
            if (tool.ROIList != null)
            {
                ROIController.ROIList = tool.ROIList;
            }
            mat2DMangerUnit1.Init(tool.Mat2DManger);
            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);

            UpDownStartIndex.Value = tool.StartIndex;
            UpDownLength.Value = tool.Length;
            chkUseTextCompare.Checked = tool.UseTextCompare;
            cbxCodeTypeWant.Text = tool.WandCodeType;
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
            if (tool.ImageRefIn != null)
            {
                mView.ClearList();
                mView.AddIconicVar(tool.ImageRefIn);
                mView.Repaint();
            }
        }
        private void reduceRect1Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShapeNoOperator(new ROIRectangle2("搜索框"));
        }
        private void ShowModelGraphics()
        {
            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (tool.ImageRefIn == null)
            {
                return;
            }
            mView.AddIconicVar(tool.ImageRefIn);
            Tool.RuningFinish = true;
            tool.ShowResult(mView);
            mView.Repaint();
        }
        private void TestModelButton_Click(object sender, EventArgs e)
        {
            txtResult.Text = "查找中";
            tool.RunRef();
            HTuple barCodeMessage = tool.Result;
            string resultMessage = string.Format("查找结果\r\n编码格式: {0}\r\n找到字符{1}\r\n结果字符串: {2}\r\n用时:{3:f2}",
                tool.CodeType, tool.BarCodeMessage, tool.Result,tool.ExecutionTime);
            ShowModelGraphics();
            if (tool.Result!=null&&tool.Result!="")
            {
                tool.Target = tool.Result;
            }
            txtResult.Text = resultMessage;
        }
        private void Code1DToolUnit_Load(object sender, EventArgs e)
        {
           
            if (tool.ImageRefIn != null)
            {
                mView.ClearList();
                mView.AddIconicVar(tool.ImageRefIn);
                mView.Repaint();
            }
        }

        private void UpDownStartIndex_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.StartIndex = (int)UpDownStartIndex.Value;
        }

        private void UpDownLength_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.Length = (int)UpDownLength.Value;
        }

        private void chkUseTextCompare_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxStrSub.Enabled = chkUseTextCompare.Checked;
            if (locked)
            {
                return;
            }
            tool.UseTextCompare = chkUseTextCompare.Checked;
        }

        private void btnDeleteSearchRegion_Click(object sender, EventArgs e)
        {
            ROIController.Reset();
        }

        private void cbxCodeTypeWant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.WandCodeType = cbxCodeTypeWant.Text;
        }
    }
}
