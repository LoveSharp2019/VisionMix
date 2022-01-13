using System;
using System.Windows.Forms;
using HalconDotNet;
using Yoga.ImageControl;
using System.Collections.Generic;
using Yoga.Tools.Matching;
using System.Linq;

namespace Yoga.Tools.PretreatImage
{
    public partial class PretreatImageToolUnit : ToolsSettingUnit
    {
        
        private PretreatImageTool tool;
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;
        
        public PretreatImageToolUnit(PretreatImageTool tool)
        {
            InitializeComponent();
            mView =hWndUnit1.HWndCtrl;
            this.tool = tool;
            ROIController = new ROIController();

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);
            tool.SettingThread.RunFinishEvent += SettingThread_RunFinishEvent;
            locked=true;
            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);

            Init();
            HideMax();
            HideMin();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void Clear()
        {
            base.Clear();
            ROIController.ROINotifyEvent -=new EventHandler<ViewEventArgs>(UpdateViewData);
            tool.SettingThread.RunFinishEvent -=SettingThread_RunFinishEvent;
        }
        private void SettingThread_RunFinishEvent(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(()=>
            {
                try
                {
                    //tool.uiCount++;
                    //Common.Util.Notify("显示刷新" + tool.uiCount);
                    ShowResult();
                }catch(Exception ex)
                {
                    Common.Util.Notify("发生异常" + ex.Message);
                }
            }));
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
                    }
                    else
                    {
                        tool.SearchRegion = null;                        
                    }
                    tool.ROIList = ROIController.ROIList;
                    //tool.RunRef();
                    //ShowResult();

                    tool.SettingThread.AddTask();
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

            mat2DMangerUnit1.Init(tool.Mat2DManger);
            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);

            ScaleUpDown.Value =(decimal) tool.ScaleImagePram.Scale;
            OffsetUpDown.Value = tool.ScaleImagePram.Offset;
            chkScaleImage.Checked= tool.ScaleImagePram.Use;
            groupBoxScaleImage.Enabled = chkScaleImage.Checked;


            chkEmphasize.Checked = tool.EmphasizeImagePram.Use;
            groupBoxEmphasize.Enabled = chkEmphasize.Checked;

            UpDownMaskWidth.Value = tool.EmphasizeImagePram.MaskWidth;
            UpDownMaskHeight.Value = tool.EmphasizeImagePram.MaskHeight;
            UpDownFactor.Value = (decimal)tool.EmphasizeImagePram.Factor;

            chkUseMedian.Checked = tool.MedianImagePram.Use;
            groupBoxMedian.Enabled = chkUseMedian.Checked;

            UpDownMedianRadius.Value = tool.MedianImagePram.Radius;

            chkSobelAmp.Checked = tool.SobelAmpImagePram.Use;
            MetricBoxSobelAmpFilterType.Text = tool.SobelAmpImagePram.FilterType;
            nudSobelAmpSize.Value = tool.SobelAmpImagePram.Size;

            chkShowResultImage.Checked = tool.IsShowResultImage;
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
            ROIController.SetROIShapeNoOperator(new ROIRectangle2("预处理区域"));
        }       
        private void SacleImageToolUnit_Load(object sender, EventArgs e)
        {
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        public override void ShowResult()
        {
            if (tool.ImageRefIn != null)
            {
                mView.ClearList();
                mView.AddIconicVar(tool.ImageRefIn);
                tool.ShowPretreatImage(mView);
                mView.Repaint();
            }
        }
        private void btnDeleteSearchRegion_Click(object sender, EventArgs e)
        {
            ROIController.Reset();
        }

        private void ScaleUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.ScaleImagePram.Scale = (double)ScaleUpDown.Value;
            //tool.RunRef();
            //ShowResult();

            tool.SettingThread.AddTask();
        }

        private void OffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.ScaleImagePram.Offset = (int)OffsetUpDown.Value;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void chkScaleImage_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxScaleImage.Enabled = chkScaleImage.Checked;
            if (locked)
            {
                return;
            }
            tool.ScaleImagePram.Use = chkScaleImage.Checked;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void chkEmphasize_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxEmphasize.Enabled = chkEmphasize.Checked;
            if (locked)
            {
                return;
            }
            tool.EmphasizeImagePram.Use = chkEmphasize.Checked;
            //tool.RunRef();
            //ShowResult();

            tool.SettingThread.AddTask();
        }

        private void UpDownMaskWidth_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.EmphasizeImagePram.MaskWidth= (int)UpDownMaskWidth.Value;
            //tool.RunRef();
            //ShowResult();

            tool.SettingThread.AddTask();
        }

        private void UpDownMaskHeight_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.EmphasizeImagePram.MaskHeight = (int)UpDownMaskHeight.Value;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void UpDownFactor_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.EmphasizeImagePram.Factor = (double)UpDownFactor.Value;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void chkUseMedian_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxMedian.Enabled = chkUseMedian.Checked;
            if (locked)
            {
                return;
            }
            tool.MedianImagePram.Use = chkUseMedian.Checked;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void UpDownMedianRadius_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MedianImagePram.Radius = (int)UpDownMedianRadius.Value;
            //tool.RunRef();
            //ShowResult();
            tool.SettingThread.AddTask();
        }

        private void chkSobelAmp_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxScaleImage.Enabled = chkSobelAmp.Checked;
            if (locked)
            {
                return;
            }
            tool.SobelAmpImagePram.Use = chkSobelAmp.Checked;
            tool.SettingThread.AddTask();
        }

        private void MetricBoxSobelAmpFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.SobelAmpImagePram.FilterType = MetricBoxSobelAmpFilterType.Text;
            tool.SettingThread.AddTask();
        }

        private void nudSobelAmpSize_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.SobelAmpImagePram.Size =(int) nudSobelAmpSize.Value;
            tool.SettingThread.AddTask();
        }

        private void chkShowResultImage_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.IsShowResultImage = chkShowResultImage.Checked;
        }
    }
}
