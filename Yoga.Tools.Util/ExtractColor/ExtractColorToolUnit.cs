using System;
using System.Windows.Forms;
using HalconDotNet;
using Yoga.ImageControl;
using System.Collections.Generic;
using Yoga.Tools.Matching;
using System.Linq;

namespace Yoga.Tools.ExtractColor
{
    public partial class ExtractColorToolUnit : ToolsSettingUnit
    {
        private ExtractColorTool tool;
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;

        public ExtractColorToolUnit(ExtractColorTool tool)
        {
            InitializeComponent();
            mView =hWndUnit1.HWndCtrl;
            this.tool = tool;
            ROIController = new ROIController();

            roiActUnit1.RoiController = ROIController;

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            locked=true;
            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);

            Init();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void ShowResult()
        {
            TestModelButton_Click(null, null);
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

                    ROI searchRoi = ROIController.ROIList.Find(x => x.OperatorFlag == ROIOperation.None);
                    if (searchRoi != null)
                    {
                        tool.SearchRegion = searchRoi.GetRegion();                        
                        //ShowModelGraphics();
                    }
                    tool.ROIList = ROIController.ROIList;
                    TestModelButton_Click(null, null);
                    break;
                case ViewMessage.ErrReadingImage:
                   
                    break;
                default:
                    break;
            }
        }
        private void Init()
        {
            if (tool.ROIList!=null)
            {
                ROIController.ROIList = tool.ROIList;
            }
            txtNote.Text = "说明:\r\n 1.使用创建roi中的 搜索矩形 创建查找颜色范围 \r\n 2.添加颜色检测点 可使用快捷键alt+f";
            mView.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            mat2DMangerUnit1.Init(tool.Mat2DManger);
            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);


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
            txtResult.Text = "";
            //if (tool.Mat2DManger.UseMat2D)
            //{
            //    ToolBase toolTmp = tool.Mat2DManger.MatchingTool as ToolBase;
            //    toolTmp.Run(tool.ImageRefIn);
            //}
            tool.RunRef();

            string resultMessage = string.Format("查找结果\r\n面积: {0}",
                tool.Result);

            ShowModelGraphics();
            txtResult.Text = resultMessage;
        }

        private void Code2DToolUnit_Load(object sender, EventArgs e)
        {

            if (tool.ImageRefIn != null)
            {
                mView.ClearList();
                mView.AddIconicVar(tool.ImageRefIn);
                mView.Repaint();
            }
            TestModelButton_Click(null, null);
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            double x, y;
            try
            {
                mView.DrawPoint(out x, out y);
                tool.AddColorPoint(x, y);
                TestModelButton_Click(null, null);
            }
            catch(Exception)
            {

            }
        }

        private void btnDelPoint_Click(object sender, EventArgs e)
        {
            try
            {
                tool.DelLeastPoint();
                TestModelButton_Click(null, null);
            }
            catch (Exception)
            {

            }
        }

        private void btnResetPoint_Click(object sender, EventArgs e)
        {
            try
            {
                tool.ResetColorPoint();
                TestModelButton_Click(null, null);
            }
            catch (Exception)
            {

            }
        }
    }
}
