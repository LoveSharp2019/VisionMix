using System;
using System.Windows.Forms;
using HalconDotNet;
using System.Collections.Generic;
using Yoga.Tools.Matching;
using System.Linq;
using Yoga.ImageControl;

namespace Yoga.Tools.PrintCheck
{
    public partial class PrintCheckToolUnit : ToolsSettingUnit
    {
        private PrintCheckTool tool;
        /// <summary>halcon窗体控制</summary>
        private HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;

        private HRegion ModelRegion;
        private HImage CurrentImg;

        public HWndCtrl MView
        {
            get
            {
                return hWndUnit1.HWndCtrl;
            }
        }

        public PrintCheckToolUnit(PrintCheckTool tool)
        {
            InitializeComponent();
            mView= hWndUnit1.HWndCtrl;
            locked = true;
            this.tool = tool;
            Init();
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
                    ModelRegion = ROIController.GetModelRegion();
                    tool.ModelRegion = ModelRegion;
                    tool.ROIList = ROIController.ROIList;
                    ShowModelGraphics();
                    break;
                case ViewMessage.ErrReadingImage:
                   
                    break;
                default:
                    break;
            }
        }
        private void Init()
        {
            ROIController = new ROIController();
            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            MView.useROIController(ROIController);

            if (tool.ROIList != null)
            {
                ROIController.ROIList = tool.ROIList;
            }

            ROIController.SetROISign(ROIOperation.Positive);
            tool.PrintCheckEvent += Assistant_PrintCheckEvent;

            AbsThresholdUpDown.Value = (decimal)tool.AbsThreshold;
            VarThresholdUpDown.Value = (decimal)tool.VarThreshold;
            //if (tool.OpeningRadius == 0)
            //{
            //    tool.OpeningRadius = PrintCheckTool.OpeningRadiusInit;
            //}
            OpeningRadiusUpDown.Value = (decimal)tool.OpeningRadius;
            //if (tool.LeastArea == 0)
            //{
            //    tool.LeastArea = PrintCheckTool.LeastAreaInit;
            //}
            leastAreaUpDown.Value = (decimal)tool.LeastArea;

            groupBox3.Enabled = tool.HaveTrainData;
            Mat2DManger mat2DManger = tool.Mat2DManger;
            mat2DManger.UseMat2DAlways = true;
            mat2DMangerUnit1.Init(mat2DManger);

            mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);

            if (tool.MatchingTool != null)
            {
                ShowRefImage();

            }

            if (tool.IsSingleImageMode)
            {
                chkSingleImage.Checked = true;
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
            CurrentImg = tool.ImageRefIn;
            ShowCurrentImage();
        }
        private void Assistant_PrintCheckEvent(object sender, PrintCheckEventArgs e)
        {
            switch (e.PrintCheckMessage)
            {
                case PrintCheckMessage.ErrNoMatchingTool:
                    lblResult.Text = "匹配工具异常,请选择匹配工具";
                    break;
                case PrintCheckMessage.ErrTrainImage:
                    lblResult.Text = "训练图像异常,请检查是否添加训练图像";
                    break;
                case PrintCheckMessage.ErrFindModel:
                    break;

                case PrintCheckMessage.ErrTestImage:
                    break;
                case PrintCheckMessage.ErrReadingImage:
                    lblResult.Text = "读取图像异常";
                    break;
                case PrintCheckMessage.ErrNoModelRegion:
                    lblResult.Text = "未定义模板区域";
                    break;
                case PrintCheckMessage.ErrTrain:
                    lblResult.Text = "训练失败,请检查设置";
                    groupBox3.Enabled = false;
                    break;
                case PrintCheckMessage.UpdateTrainView:
                    ChangeShowMode(ShowMode.ExcludeROI);
                    CurrentImg = tool.TrainImage;
                    ShowCurrentImage();
                    break;
                case PrintCheckMessage.UpdateTestView:
                    ChangeShowMode(ShowMode.ExcludeROI);
                    CurrentImg = tool.ImageTestIn;
                    ShowCurrentImage();
                    break;
                case PrintCheckMessage.UpdateTrainResult:
                    ChangeShowMode(ShowMode.ExcludeROI);
                    if (rbtnMeanImage.Checked)
                    {
                        CurrentImg = tool.MeanImage;
                    }
                    else if (rbtnVarImage.Checked)
                    {
                        CurrentImg = tool.VarImage;
                    }
                    ShowModelGraphics();
                    lblResult.Text = "训练成功";
                    groupBox3.Enabled = true;
                    break;
                default:
                    break;
            }
        }
        private void ChangeShowMode(ShowMode showMode)
        {
            MView.SetDispLevel(showMode);
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
        private void showCheckResult()
        {

            MView.ClearList();
            MView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            MView.AddIconicVar(CurrentImg);
            Tool.RuningFinish = true;
            ToolBase toolTmp = tool.MatchingTool as ToolBase;
            toolTmp.ShowResult(MView);
            tool.ShowResult(MView);
            MView.Repaint();

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
        public void ShowModelGraphics()
        {
            MView.ClearList();
            MView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            MView.AddIconicVar(CurrentImg);
            if (ModelRegion != null)
            {
                MView.ChangeGraphicSettings(Mode.COLOR, "green");
                MView.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                MView.AddIconicVar(ModelRegion);
            }
            MView.Repaint();
        }

        public void ShowCurrentImage()
        {

            MView.ClearList();
            MView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            MView.AddIconicVar(CurrentImg);
            MView.Repaint();
        }
        private void AbsThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            AbsThresholdUpDown.Value = AbsThresholdTrackBar.Value;
            AbsThresholdUpDown.Refresh();
        }
        private void AbsThresholdResetButton_Click(object sender, EventArgs e)
        {
            AbsThresholdUpDown.Value = (decimal)PrintCheckTool.AbsThresholdInit;
            AbsThresholdUpDown.Refresh();
        }
        private void AbsThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            double val = (double)AbsThresholdUpDown.Value;

            AbsThresholdTrackBar.Value = (int)val;
            if (val == PrintCheckTool.AbsThresholdInit)
            {
                AbsThresholdResetButton.Enabled = false;
            }
            else
            {
                AbsThresholdResetButton.Enabled = true;
            }
            if (locked == false)
            {
                tool.AbsThreshold = val;
            }
        }

        private void VarThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            VarThresholdUpDown.Value = (decimal)(VarThresholdTrackBar.Value / 10.0);
            VarThresholdUpDown.Refresh();
        }
        private void VarThresholdResetButton_Click(object sender, EventArgs e)
        {
            VarThresholdUpDown.Value = (decimal)PrintCheckTool.VarThresholdInit;
            VarThresholdUpDown.Refresh();
        }
        private void VarThresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            double var = (double)VarThresholdUpDown.Value;
            VarThresholdTrackBar.Value = (int)(var * 10.0);

            if (var == PrintCheckTool.VarThresholdInit)
            {
                VarThresholdResetButton.Enabled = false;
            }
            else
            {
                VarThresholdResetButton.Enabled = true;
            }
            if (!locked)
            {
                tool.VarThreshold = var;
            }
        }

        private void rect1Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROIRectangle1());
        }

        private void rect2Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROIRectangle2());
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
            tool.Reset();
        }

        private void loadTrainImg_Click(object sender, EventArgs e)
        {
            string[] files;
            int count = 0;

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog2.FileNames;
                count = files.Length;

                for (int i = 0; i < count; i++)
                {
                    if (tool.AddTrainImages(files[i]))
                        trainImgListBox.Items.Add(files[i]);
                }

                if (trainImgListBox.SelectedIndex < 0 && trainImgListBox.Items.Count != 0)
                {
                    CurrentImg = tool.GetTrainImage((string)trainImgListBox.Items[0]);
                    trainImgListBox.SelectedIndex = 0;
                    ShowModelGraphics();
                }
            }//if 
        }

        private void deleteTrainImgButton_Click(object sender, EventArgs e)
        {
            int count;
            if ((count = trainImgListBox.SelectedIndex) < 0)
                return;

            string fileName = (string)trainImgListBox.SelectedItem;

            if ((--count) < 0)
                count += 2;

            if ((count < trainImgListBox.Items.Count))
            {
                trainImgListBox.SelectedIndex = count;
            }

            tool.RemoveTrainImage(fileName);
            trainImgListBox.Items.Remove(fileName);
            trainImgListBox.Focus();
        }

        private void BtnDelAllTrainImage_Click(object sender, EventArgs e)
        {
            if (trainImgListBox.Items.Count > 0)
            {
                trainImgListBox.Items.Clear();
                tool.RemoveTrainImage();
                MView.ClearList();
                MView.Repaint();
            }
        }

        private void trainImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (trainImgListBox.SelectedIndex < 0)
            {
                MView.ClearList();
                MView.Repaint();
                return;
            }

            file = (string)trainImgListBox.SelectedItem;
            tool.SetTrainImage(file);
        }

        private void btnShowRefImage_Click(object sender, EventArgs e)
        {
            ShowRefImage();
        }

        private void ShowRefImage()
        {
            ChangeShowMode(ShowMode.IncludeROI);
            CurrentImg = tool.ImageRefIn;
            ShowModelGraphics();
        }

        private void btnTrainModel_Click(object sender, EventArgs e)
        {

            if (trainImgListBox.Items.Count < 1 && chkSingleImage.Checked == false)
            {
                Assistant_PrintCheckEvent(null, new PrintCheckEventArgs(PrintCheckMessage.ErrTrainImage));
                return;
            }
            ChangeShowMode(ShowMode.ExcludeROI);
            groupBox3.Enabled = false;
            lblResult.Text = "模板训练中...";
            if (chkSingleImage.Checked)
            {
                tool.TrainVariationModelSigleImage();
            }
            else
            {
                tool.TrainVariationModel();
            }
        }
        private void rbtnMeanImage_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMeanImage.Checked == false)
            {
                return;
            }
            HImage image = tool.MeanImage;
            if (image == null || image.IsInitialized() == false)
            {
                return;
            }
            ChangeShowMode(ShowMode.ExcludeROI);
            CurrentImg = image;
            ShowModelGraphics();
        }

        private void rbtnVarImage_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnVarImage.Checked == false)
            {
                return;
            }
            HImage varImage = tool.VarImage;
            if (varImage == null || varImage.IsInitialized() == false)
            {
                return;
            }
            ChangeShowMode(ShowMode.ExcludeROI);
            CurrentImg = varImage;
            ShowModelGraphics();
        }

        private void loadTestImgButton_Click(object sender, EventArgs e)
        {
            string[] files;
            int count = 0;

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog2.FileNames;
                count = files.Length;

                for (int i = 0; i < count; i++)
                {
                    if (tool.AddTestImages(files[i]))
                        testImgListBox.Items.Add(files[i]);
                }

                if (testImgListBox.SelectedIndex < 0 && testImgListBox.Items.Count != 0)
                {
                    CurrentImg = tool.GetImageTest((string)testImgListBox.Items[0]);
                    testImgListBox.SelectedIndex = 0;
                    ShowCurrentImage();
                }
            }//if 
        }

        private void deleteTestImgButton_Click(object sender, EventArgs e)
        {
            int count;
            if ((count = testImgListBox.SelectedIndex) < 0)
                return;

            string fileName = (string)testImgListBox.SelectedItem;

            if ((--count) < 0)
                count += 2;

            if ((count < testImgListBox.Items.Count))
            {
                testImgListBox.SelectedIndex = count;
            }

            tool.RemoveTestImage(fileName);
            testImgListBox.Items.Remove(fileName);
        }

        private void deleteAllTestImgButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count > 0)
            {
                testImgListBox.Items.Clear();
                tool.RemoveTestImage();

                MView.ClearList();
                ChangeShowMode(ShowMode.ExcludeROI);
                MView.Repaint();
            }
        }

        private void testModelButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                ChangeShowMode(ShowMode.ExcludeROI);

            CurrentImg = tool.ImageTestIn;
            if (tool.MatchingTool == null)
            {
                MessageBox.Show("定位工具未设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            lblResult.Text = "印刷检测中...";
            ToolBase toolTmp = tool.MatchingTool as ToolBase;
            toolTmp.Run(CurrentImg);
            if (toolTmp.IsRealOk() == false)
            {
                lblResult.Text = "定位失败,请检查设置...";
                return;
            }

            tool.Run(CurrentImg);
            ChangeShowMode(ShowMode.ExcludeROI);
            showCheckResult();
        }

        private void testImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (testImgListBox.SelectedIndex < 0)
            {
                MView.ClearList();
                MView.Repaint();
                return;
            }

            file = (string)testImgListBox.SelectedItem;
            tool.SetTestImage(file);
            if (FindAlwaysCheckBox.Checked)
            {
                CurrentImg = tool.ImageTestIn;
                if (tool.MatchingTool == null)
                {
                    MessageBox.Show("定位工具未设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                ToolBase toolTmp = tool.MatchingTool as ToolBase;
                toolTmp.Run(CurrentImg);
                tool.Run(CurrentImg);
                ChangeShowMode(ShowMode.ExcludeROI);
                showCheckResult();
            }
            else if (testImgListBox.Items.Count > 0)
            {
                file = (string)testImgListBox.SelectedItem;
                CurrentImg = tool.GetImageTest(file);

                ChangeShowMode(ShowMode.ExcludeROI);

                ShowCurrentImage();
            }
        }

        private void btnOpeningRadiusReset_Click(object sender, EventArgs e)
        {
            OpeningRadiusUpDown.Value = (decimal)PrintCheckTool.OpeningRadiusInit;
            OpeningRadiusUpDown.Refresh();
        }

        private void OpeningRadiusTrackBar_Scroll(object sender, EventArgs e)
        {
            OpeningRadiusUpDown.Value = (decimal)(OpeningRadiusTrackBar.Value / 10.0);
            OpeningRadiusUpDown.Refresh();
        }

        private void OpeningRadiusUpDown_ValueChanged(object sender, EventArgs e)
        {
            double var = (double)OpeningRadiusUpDown.Value;
            OpeningRadiusTrackBar.Value = (int)(var * 10.0);

            if (var == PrintCheckTool.OpeningRadiusInit)
            {
                btnOpeningRadiusReset.Enabled = false;
            }
            else
            {
                btnOpeningRadiusReset.Enabled = true;
            }
            if (!locked)
            {
                tool.OpeningRadius = var;
            }
        }

        private void btnLeastAreaInit_Click(object sender, EventArgs e)
        {
            leastAreaUpDown.Value = (decimal)PrintCheckTool.LeastAreaInit;
            leastAreaUpDown.Refresh();
        }
        private void leastAreaUpDown_ValueChanged(object sender, EventArgs e)
        {
            double var = (double)leastAreaUpDown.Value;
            leastAreaTrackBar.Value = (int)(var * 10.0);

            if (var == PrintCheckTool.LeastAreaInit)
            {
                btnLeastAreaInit.Enabled = false;
            }
            else
            {
                btnLeastAreaInit.Enabled = true;
            }
            if (!locked)
            {
                tool.LeastArea = var;
            }
        }

        private void leastAreaTrackBar_Scroll(object sender, EventArgs e)
        {
            leastAreaUpDown.Value = (decimal)(leastAreaTrackBar.Value / 10.0);
            leastAreaUpDown.Refresh();
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

        private void PrintCheckToolUnit_Load(object sender, EventArgs e)
        {

        }

        private void chkSingleImage_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
