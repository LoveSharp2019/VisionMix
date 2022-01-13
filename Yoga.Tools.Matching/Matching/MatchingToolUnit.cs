using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HalconDotNet;
using Yoga.ImageControl;

namespace Yoga.Tools.Matching
{
    public partial class MatchingToolUnit : ToolsSettingUnit
    {
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
        public ROIController ROIController;

        public MatchingTool macthingTool;

        private HImage CurrentImg;
        private HXLD ModelContour;
        //private MatchingParam parameterSet;
        private MatchingOpt optInstance;
        private MatchingOptSpeed speedOptHandler;
        private MatchingOptStatistics inspectOptHandler;
        private Color createModelWindowMode;
        private Color trainModelWindowMode;
        public MatchingToolUnit(MatchingTool macthingTool)
        {
            InitializeComponent();


            mView = hWndUnit1.HWndCtrl;

            ROIController = new ROIController();

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            roiActUnit1.RoiController = ROIController;
            createModelWindowMode = Color.RoyalBlue;
            trainModelWindowMode = Color.Chartreuse;


            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);

            locked = true;

            Init();

            chkUseCalib.Checked = macthingTool.IsCalibOut;
            SetMatchingAssistant(macthingTool);

            speedOptHandler = new MatchingOptSpeed(macthingTool);
            speedOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);

            inspectOptHandler = new MatchingOptStatistics(macthingTool);
            inspectOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);
            HideMax();
            base.Init(macthingTool.Name, macthingTool);

            locked = false;
        }
        public override void ShowResult()
        {
            changeWindowMode(ShowMode.IncludeROI);
            UpdateROI();

        }
        private void Init()
        {
            //MetricBox.Items.Clear();
            //string[] MetricStr= Enum.GetNames(typeof(Metric));
            //foreach (var item in MetricStr)
            //{
            //    MetricBox.Items.Add(item);
            //}
        }
        /// <summary>
        /// 直接设置匹配参数(序列化后传递过来)
        /// </summary>
        /// <param name="param"></param>
        public void SetMatchingAssistant(MatchingTool matchingAssistant)
        {

            locked = true;
            this.macthingTool = matchingAssistant;
            //此处未序列化
            macthingTool.TestImageDic = new Dictionary<string, HImage>();

            macthingTool.NotifyIconObserver = new MatchingDelegate(UpdateMatching);
            macthingTool.NotifyParamObserver = new AutoParamDelegate(UpdateButton);


            speedOptHandler = new MatchingOptSpeed(macthingTool);
            speedOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);

            inspectOptHandler = new MatchingOptStatistics(macthingTool);
            inspectOptHandler.NotifyStatisticsObserver = new StatisticsDelegate(UpdateStatisticsData);
            UpdateButton(MatchingParam.AUTO_CONTRAST);
            UpdateButton(MatchingParam.AUTO_ANGLE_STEP);
            UpdateButton(MatchingParam.AUTO_MIN_CONTRAST);

            UpdateButton(MatchingParam.AUTO_NUM_LEVEL);
            UpdateButton(MatchingParam.AUTO_OPTIMIZATION);
            UpdateButton(MatchingParam.AUTO_SCALE_STEP);
            UpdateButton(MatchingParam.BUTTON_ANGLE_EXTENT);
            UpdateButton(MatchingParam.BUTTON_ANGLE_START);

            UpdateButton(MatchingParam.BUTTON_METRIC);
            UpdateButton(MatchingParam.BUTTON_SCALE_MAX);
            UpdateButton(MatchingParam.BUTTON_SCALE_MIN);
            UpdateButton(MatchingParam.BUTTON_MINSCORE);
            UpdateButton(MatchingParam.BUTTON_GREEDINESS);

            UpdateButton(MatchingParam.BUTTON_NUM_MATCHES);
            UpdateButton(MatchingParam.RANGE_ANGLE_STEP);
            UpdateButton(MatchingParam.RANGE_SCALE_STEP);
            UpdateButton(MatchingParam.BUTTON_MAX_OVERLAP);
            UpdateButton(MatchingParam.BUTTON_SUB_PIXEL);

            updateAutoCheckBoxState();


            if (macthingTool.ROIList != null)
            {
                ROIController.ROIList = macthingTool.ROIList;
            }

            UpdateROI();

            macthingTool.InitMatchingResult();
           // ModelContour = macthingTool.GetLoadedModelContour();
            InitModelContour();
          //  UpdateMatching(MatchingStatus.UpdateXLD);

            
            locked = false;

        }
        private void  InitModelContour()
        {
            if (macthingTool.MatchingResults.Contour!=null&&macthingTool.ModelRegion!=null)
            {
                double row,col;
                macthingTool.ModelRegion.AreaCenter(out row, out col);

                HHomMat2D mat2d = new HHomMat2D();
                mat2d.VectorAngleToRigid(0, 0, 0,
                    row, col, 0);
               ModelContour= mat2d.AffineTransContourXld(macthingTool.MatchingResults.Contour);

            }
        }
        private void updateAutoCheckBoxState()
        {
            bool lockTmp = locked;
            locked = true;
            foreach (string mode in macthingTool.ParameterSet.ParamAutoList)
            {
                switch (mode)
                {
                    case MatchingParam.AUTO_ANGLE_STEP:
                        ckbAngleStepAuto.Checked = true;
                        break;
                    case MatchingParam.AUTO_CONTRAST:
                        ckbContrastAuto.Checked = true;
                        break;
                    case MatchingParam.AUTO_MIN_CONTRAST:
                        ckbMinContrastAuto.Checked = true;
                        break;
                    case MatchingParam.AUTO_NUM_LEVEL:
                        ckbPyramidLevelAuto.Checked = true;
                        break;
                    case MatchingParam.AUTO_OPTIMIZATION:
                        ckbOptimizationAuto.Checked = true;
                        break;
                    case MatchingParam.AUTO_SCALE_STEP:
                        ckbScaleStepAuto.Checked = true;
                        break;
                    default: break;
                }

            }
            locked = lockTmp;
        }
        public override void ShowTranResult()
        {
            locked = true;

            macthingTool.DetectionContour = null;
       
            FindAlwaysCheckBox.Checked = false;

            if (macthingTool.ImageRefIn == null)
                return;

            //mView.resetAll();

            if (macthingTool.onExternalModelID)
                ModelContour = macthingTool.GetLoadedModelContour();

            // to add ROI instances to the display as well                                
            if (tabControl.SelectedIndex != 0)
            {
                tabControl.SelectedIndex = 0;
            }
            else
            {
                changeWindowMode(ShowMode.IncludeROI);
                CreateModelGraphics();
                mView.Repaint();
            }
            locked = false;
        }    
        private void ContrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)ContrastUpDown.Value;
            ContrastTrackBar.Value = val;
            MinContrastTrackBar.Maximum = val;
            MinContrastUpDown.Maximum = val;

            if (!locked)
                setContrast(val);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_CONTRAST))
            {
                ckbContrastAuto.Checked = false;
            }
        }

        private void ContrastTrackBar_Scroll(object sender, EventArgs e)
        {
            ContrastUpDown.Value = ContrastTrackBar.Value;
            ContrastUpDown.Refresh();
        }
        private void ckbContrastAuto_Click(object sender, EventArgs e)
        {
            if (ckbContrastAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_CONTRAST);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_CONTRAST);
            }
        }
        private void setContrast(int val)
        {
            macthingTool.SetContrast(val);
        }

        private void MinScaleUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal val = MinScaleUpDown.Value;
            MinScaleTrackBar.Value = (int)(val * 100);

            if (val >= MaxScaleUpDown.Value)
                MaxScaleUpDown.Value = val;

            if (!locked)
                setMinScale(val);
        }

        private void MinScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            MinScaleUpDown.Value = (decimal)(MinScaleTrackBar.Value / 100.0);
            MinScaleUpDown.Refresh();
        }
        private void setMinScale(decimal val)
        {
            macthingTool.SetMinScale((double)val);
        }

        private void MaxScaleUpDown_ValueChanged(object sender, EventArgs e)
        {

            decimal val = MaxScaleUpDown.Value;
            MaxScaleTrackBar.Value = (int)(val * 100);

            if (val <= MinScaleUpDown.Value)
                MinScaleUpDown.Value = val;

            if (!locked)
                setMaxScale(val);
        }

        private void MaxScaleTrackBar_Scroll(object sender, EventArgs e)
        {
            MaxScaleUpDown.Value = (decimal)(MaxScaleTrackBar.Value / 100.0);
            MaxScaleUpDown.Refresh();
        }
        private void setMaxScale(decimal val)
        {
            macthingTool.SetMaxScale((double)val);
        }

        private void ScaleStepUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal val = ScaleStepUpDown.Value;
            ScaleStepTrackBar.Value = (int)(val * 1000);

            if (!locked)
                setScaleStep(val);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_SCALE_STEP))
            {
                ckbScaleStepAuto.Checked = false;
            }
        }

        private void ScaleStepTrackBar_Scroll(object sender, EventArgs e)
        {
            ScaleStepUpDown.Value = (decimal)(ScaleStepTrackBar.Value / 1000.0);
            ScaleStepUpDown.Refresh();
        }
        private void ckbScaleStepAuto_Click(object sender, EventArgs e)
        {
            if (ckbScaleStepAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_SCALE_STEP);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_SCALE_STEP);
            }

        }
        private void setScaleStep(decimal val)
        {
            macthingTool.SetScaleStep((double)val);
        }

        private void StartingAngleUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)StartingAngleUpDown.Value;
            StartingAngleTrackBar.Value = val;

            if (!locked)
                setStartingAngle(val);
        }

        private void StartingAngleTrackBar_Scroll(object sender, EventArgs e)
        {
            StartingAngleUpDown.Value = StartingAngleTrackBar.Value;
            StartingAngleUpDown.Refresh();
        }
        private void setStartingAngle(int val)
        {
            double rad = val * Math.PI / 180;
            macthingTool.SetStartingAngle(rad);
        }

        private void AngleExtentUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)AngleExtentUpDown.Value;
            AngleExtentTrackBar.Value = val;

            if (!locked)
                setAngleExtent(val);
        }

        private void AngleExtentTrackBar_Scroll(object sender, EventArgs e)
        {
            AngleExtentUpDown.Value = AngleExtentTrackBar.Value;
            AngleExtentUpDown.Refresh();
        }
        private void setAngleExtent(int val)
        {
            double rad = val * Math.PI / 180;
            macthingTool.SetAngleExtent(rad);
        }

        private void AngleStepUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal val = AngleStepUpDown.Value;
            AngleStepTrackBar.Value = (int)(val * 10);

            if (!locked)
                setAngleStep(val);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_ANGLE_STEP))
            {
                ckbAngleStepAuto.Checked = false;
            }
        }

        private void AngleStepTrackBar_Scroll(object sender, EventArgs e)
        {
            AngleStepUpDown.Value = (decimal)(AngleStepTrackBar.Value / 10.0);
            AngleStepUpDown.Refresh();
        }
        private void ckbAngleStepAuto_Click(object sender, EventArgs e)
        {
            if (ckbAngleStepAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_ANGLE_STEP);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_ANGLE_STEP);
            }
        }

        private void setAngleStep(decimal val)
        {
            double rad = (double)val * Math.PI / 180.0;
            macthingTool.SetAngleStep(rad);
        }

        private void PyramidLevelUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)PyramidLevelUpDown.Value;
            PyramidLevelTrackBar.Value = val;

            if (!locked)
                setPyramidLevel(val);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_NUM_LEVEL))
            {
                ckbPyramidLevelAuto.Checked = false;
            }
        }

        private void PyramidLevelTrackBar_Scroll(object sender, EventArgs e)
        {
            PyramidLevelUpDown.Value = PyramidLevelTrackBar.Value;
            PyramidLevelUpDown.Refresh();
        }
        private void ckbPyramidLevelAuto_Click(object sender, EventArgs e)
        {
            if (ckbPyramidLevelAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_NUM_LEVEL);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_NUM_LEVEL);
            }
        }
        private void setPyramidLevel(int val)
        {
            macthingTool.SetPyramidLevel(val);
        }

        private void MetricBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!locked)
                macthingTool.SetMetric(MetricBox.Text);
        }

        private void OptimizationBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!locked)
                macthingTool.SetOptimization(OptimizationBox.Text);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_OPTIMIZATION))
            {
                ckbOptimizationAuto.Checked = false;
            }
        }
        private void ckbOptimizationAuto_Click(object sender, EventArgs e)
        {
            if (ckbOptimizationAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_OPTIMIZATION);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_OPTIMIZATION);
            }
        }
        private void MinContrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)MinContrastUpDown.Value;
            MinContrastTrackBar.Value = val;

            if (!locked)
                setMinContrast(val);

            if (!macthingTool.ParameterSet.IsAuto(MatchingParam.AUTO_MIN_CONTRAST))
            {
                ckbMinContrastAuto.Checked = false;
            }
        }

        private void MinContrastTrackBar_Scroll(object sender, EventArgs e)
        {
            MinContrastUpDown.Value = MinContrastTrackBar.Value;
            MinContrastUpDown.Refresh();
        }
        private void ckbMinContrastAuto_Click(object sender, EventArgs e)
        {
            if (ckbMinContrastAuto.Checked)
            {
                macthingTool.SetAuto(MatchingParam.AUTO_MIN_CONTRAST);
            }
            else
            {
                macthingTool.RemoveAuto(MatchingParam.AUTO_MIN_CONTRAST);
            }
        }
        private void setMinContrast(int val)
        {
            macthingTool.SetMinContrast(val);
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
                    if (macthingTool.AddTestImages(files[i]))
                        testImgListBox.Items.Add(files[i]);
                }

                if (testImgListBox.SelectedIndex < 0 && testImgListBox.Items.Count != 0)
                {
                    CurrentImg = macthingTool.GetImageTest((string)testImgListBox.Items[0]);
                    testImgListBox.SelectedIndex = 0;
                    macthingTool.ResetMatchingResult();
                    ShowModelGraphics();
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

            macthingTool.RemoveTestImage(fileName);
            testImgListBox.Items.Remove(fileName);
        }

        private void deleteAllTestImgButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count > 0)
            {
                testImgListBox.Items.Clear();
                macthingTool.RemoveTestImage();
                macthingTool.DetectionContour = null;

                mView.ClearList();
                changeWindowMode(ShowMode.ExcludeROI);
                mView.Repaint();
            }
        }

        private void testImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (testImgListBox.SelectedIndex < 0)
            {
                mView.ClearList();
                changeWindowMode(ShowMode.ExcludeROI);
                mView.Repaint();
                return;
            }
            mView.ClearList();
            macthingTool.DetectionContour = null;
            file = (string)testImgListBox.SelectedItem;
            macthingTool.SetTestImage(file);

            changeWindowMode(ShowMode.ExcludeROI);

            if (FindAlwaysCheckBox.Checked
                && (macthingTool.onExternalModelID || (macthingTool.ModelRegion != null)))
            {
                macthingTool.ApplyFindModel();
            }
            else
            {
                CurrentImg = macthingTool.GetCurrTestImage();
                mView.AddIconicVar(CurrentImg);
                mView.Repaint();
            }
        }

        private void displayTestImgButton_Click(object sender, EventArgs e)
        {

            string file;

            if (testImgListBox.Items.Count == 0)
            {
                mView.ClearList();
                changeWindowMode(ShowMode.ExcludeROI);
                mView.Repaint();

                UpdateMatching(MatchingStatus.ErrNoTestImage);
                return;
            }


            file = (string)testImgListBox.SelectedItem;
            CurrentImg = macthingTool.GetImageTest(file);

            changeWindowMode(ShowMode.ExcludeROI, CurrentImg);

            if (!FindAlwaysCheckBox.Checked)
                macthingTool.DetectionContour = null;
            macthingTool.ResetMatchingResult();
            ShowModelGraphics();

            if (ModelContour != null && FindAlwaysCheckBox.Checked)
            {
                macthingTool.ApplyFindModel();
            }
            else
            {
                mView.Repaint();
            }
        }

        private void findModelButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                changeWindowMode(ShowMode.ExcludeROI);
            macthingTool.ApplyFindModel();
        }

        private void FindAlwaysCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = FindAlwaysCheckBox.Checked;

            if (flag && testImgListBox.Items.Count != 0)
                changeWindowMode(ShowMode.ExcludeROI);

            macthingTool.SetFindAlways(flag);
        }

        private void MinScoreUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal val = MinScoreUpDown.Value;
            MinScoreTrackBar.Value = (int)(val * 100);

            if (!locked)
                setMinScore(val);
        }

        private void MinScoreTrackBar_Scroll(object sender, EventArgs e)
        {
            MinScoreUpDown.Value = (decimal)(MinScoreTrackBar.Value / 100.0);
            MinScoreUpDown.Refresh();
        }
        private void setMinScore(decimal val)
        {
            if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                changeWindowMode(ShowMode.ExcludeROI);

            macthingTool.SetMinScore((double)val);
        }

        private void NumMatchesUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)NumMatchesUpDown.Value;
            NumMatchesTrackBar.Value = val;
            InspectMaxNoMatchUpDown.Value = val;

            if (!locked)
                setNumMatches(val);
        }

        private void NumMatchesTrackBar_Scroll(object sender, EventArgs e)
        {
            NumMatchesUpDown.Value = NumMatchesTrackBar.Value;
            NumMatchesUpDown.Refresh();
        }
        private void setNumMatches(int val)
        {
            if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                changeWindowMode(ShowMode.ExcludeROI);

            if (val == 0)
            {
                if (FindMaxNoOfModelButton.Checked == true)
                    FindAtLeastOneModelButton.Checked = true;

                FindMaxNoOfModelButton.Enabled = false;
            }
            else
            {
                FindMaxNoOfModelButton.Enabled = true;
            }


            macthingTool.SetNumMatches(val);
        }

        private void GreedinessUpDown_ValueChanged(object sender, EventArgs e)
        {
            double val = (double)GreedinessUpDown.Value;
            GreedinessTrackBar.Value = (int)(val * 100);

            if (!locked)
                setGreediness(val);
        }

        private void GreedinessTrackBar_Scroll(object sender, EventArgs e)
        {
            GreedinessUpDown.Value = (decimal)(GreedinessTrackBar.Value / 100.0);
            GreedinessUpDown.Refresh();
        }
        private void setGreediness(double val)
        {
            if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                changeWindowMode(ShowMode.ExcludeROI);

            macthingTool.SetGreediness(val);
        }

        private void MaxOverlapUpDown_ValueChanged(object sender, EventArgs e)
        {
            double val = (double)MaxOverlapUpDown.Value;
            MaxOverlapTrackBar.Value = (int)(val * 100);

            if (!locked)
                setMaxOverlap(val);
        }

        private void MaxOverlapTrackBar_Scroll(object sender, EventArgs e)
        {
            MaxOverlapUpDown.Value = (decimal)(MaxOverlapTrackBar.Value / 100.0);
            MaxOverlapUpDown.Refresh();
        }
        private void setMaxOverlap(double val)
        {
            if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                changeWindowMode(ShowMode.ExcludeROI);

            macthingTool.SetMaxOverlap(val);
        }

        private void SubPixelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!locked)
            {
                if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                    changeWindowMode(ShowMode.ExcludeROI);

                macthingTool.SetSubPixel(SubPixelBox.Text);
            }
        }

        private void LastPyrLevUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)LastPyrLevUpDown.Value;
            LastPyrLevTrackBar.Value = val;

            if (!locked)
                setLastPyramidLevel(val);
        }

        private void LastPyrLevTrackBar_Scroll(object sender, EventArgs e)
        {
            LastPyrLevUpDown.Value = LastPyrLevTrackBar.Value;
            LastPyrLevUpDown.Refresh();
        }
        private void setLastPyramidLevel(int val)
        {
            if (testImgListBox.Items.Count != 0 && FindAlwaysCheckBox.Checked)
                changeWindowMode(ShowMode.ExcludeROI);

            macthingTool.SetLastPyramLevel(val);
        }

        private void FindNoOfInstButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!locked && FindNoOfInstButton.Checked)
                macthingTool.SetRecogSpeedMode(MatchingParam.RECOGM_MANUALSELECT);
        }

        private void FindAtLeastOneModelButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!locked && FindAtLeastOneModelButton.Checked)
                macthingTool.SetRecogSpeedMode(MatchingParam.RECOGM_ATLEASTONE);
        }

        private void FindMaxNoOfModelButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!locked && FindMaxNoOfModelButton.Checked)
                macthingTool.SetRecogSpeedMode(MatchingParam.RECOGM_MAXNUMBER);
        }

        private void RecogNoManSelUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)RecogNoManSelUpDown.Value;
            if (!locked)
                macthingTool.SetRecogManualSelection(val);
        }

        private void RecognRateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val = RecognRateComboBox.SelectedIndex;

            if (!locked)
                macthingTool.SetRecogRateOption(val);
        }

        private void recogRateUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)recogRateUpDown.Value;
            recogRateTrackBar.Value = val;
            macthingTool.SetRecogitionRate(val);
        }

        private void recogRateTrackBar_Scroll(object sender, EventArgs e)
        {
            recogRateUpDown.Value = recogRateTrackBar.Value;
            recogRateUpDown.Refresh();
        }

        private void OptimizeButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                changeWindowMode(ShowMode.ExcludeROI);

            if (!timer.Enabled)
            {
                FindAlwaysCheckBox.Checked = false;
                optInstance = speedOptHandler;
                optInstance.Reset();
                UpdateStatisticsData(MatchingStatus.UpdateRecogVals);
                UpdateStatisticsData(MatchingStatus.UpdateRecogOptimumVals);
                UpdateStatisticsData(MatchingStatus.UpdateRecogSattisticStatus);

                OptimizeButton.Text = "停止";
                macthingTool.onTimer = true;
                timer.Enabled = true;
            }
            else
            {
                timer.Enabled = false;
                OptimizeButton.Text = "执行优化";
                macthingTool.onTimer = false;
                UpdateStatisticsData(MatchingStatus.RunFailed);
            }
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                changeWindowMode(ShowMode.ExcludeROI);

            if (!timer.Enabled)
            {
                FindAlwaysCheckBox.Checked = false;
                optInstance = inspectOptHandler;
                optInstance.Reset();
                UpdateStatisticsData(MatchingStatus.UpdateInspRecograte);
                UpdateStatisticsData(MatchingStatus.UpdateInspStatistics);

                StatisticsButton.Text = "停止";
                timer.Enabled = true;
                macthingTool.onTimer = true;
            }
            else
            {
                timer.Enabled = false;
                StatisticsButton.Text = "执行";
                macthingTool.onTimer = false;
            }
        }

        private void InspectMaxNoMatchUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (InspectMaxNoMatchUpDown.Value != NumMatchesUpDown.Value)
                NumMatchesUpDown.Value = (int)InspectMaxNoMatchUpDown.Value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            bool run;
            run = optInstance.ExecuteStep();

            if (!run)
            {
                timer.Enabled = false;
                optInstance.Stop();
                macthingTool.onTimer = false;

                OptimizeButton.Text = "执行优化";
                StatisticsButton.Text = "执行";
            }
        }
        /**********************************************************************/
        /**********************************************************************/
        /*                     Delegate Routines                              */
        /**********************************************************************/
        /**********************************************************************/

        /// <summary>
        /// This method is invoked if changes occur in the HWndCtrl instance
        /// or the ROIController. In either case, the HALCON 
        /// window needs to be updated/repainted.
        /// </summary>
        public void UpdateViewData(object sender, ViewEventArgs e)
        {
            switch (e.ViewMessage)
            {
                case ViewMessage.DelectedAllROIs:

                    
                    macthingTool.Reset();
                    break;
                case ViewMessage.ChangedROISign:
                case ViewMessage.DeletedActROI:
                
                case ViewMessage.UpdateROI:
                //case ViewMessage.ActivatedROI:
                case ViewMessage.CreatedROI:
                    ModelContour = null;
                    macthingTool.DetectionContour = null;
                    UpdateROI();

                    //macthingTool.SetModelROI(ModelRegion);
                    break;
                case ViewMessage.ErrReadingImage:

                    break;
                default:
                    break;
            }
        }

        private void UpdateROI()
        {
            macthingTool.SearchRegion = null;
            bool genROI = ROIController.DefineModelROI();
            macthingTool.ModelRegion = ROIController.GetModelRegion();
            ROI searchRoi = ROIController.ROIList.Find(x => x.OperatorFlag == ROIOperation.None);
            if (searchRoi != null)
            {
                macthingTool.SearchRegion = searchRoi.GetRegion();
            }
            macthingTool.ROIList = ROIController.ROIList;
        }

        /// <summary>
        /// This method is invoked for any changes in the 
        /// MatchingAssistant, concerning the model creation and
        /// the model finding. Also changes in the display mode 
        /// (e.g., pyramid level) are mapped here.
        /// </summary>
        public void UpdateMatching(MatchingStatus val)
        {
            bool paint = false;
            switch (val)
            {
                case MatchingStatus.UpdateXLD:
                    ModelContour = macthingTool.GetModelContour();
                    CreateModelGraphics();
                    
                    //ShowModelGraphics();
                    paint = true;
                    break;
                case MatchingStatus.UpdateDetectiongResult:
                    CurrentImg = macthingTool.GetCurrTestImage();
                    ShowModelGraphics();
                    MatchingResult result = macthingTool.MatchingResults;
                    if (result.Score.Length > 0)
                    {
                        lblTestResult.BackColor = Color.White;
                        lblTestResult.Text = string.Format("查找结果 分数: {0:F2}-角度: {1:F2}°-行: {2:F2}-列: {3:F2}-用时: {4:F2}ms",
                            result.Score.D, result.Angle.TupleDeg().D, result.Row.D, result.Col.D, result.TimeFound);
                    }
                    else
                    {
                        lblTestResult.BackColor = Color.Red;
                        lblTestResult.Text = string.Format("查找结果 查找失败,用时{0:F2}ms", result.TimeFound);
                    }
                    paint = true;
                    break;
                case MatchingStatus.UpdateTestView:
                    CurrentImg = macthingTool.GetCurrTestImage();
                    ShowModelGraphics();
                    break;
                case MatchingStatus.ErrWriteShapeModel:

                    lblTestResult.Text = "文件写入异常:" + macthingTool.ExceptionText;

                    break;
                case MatchingStatus.ErrReadShapeModel:
                    lblTestResult.Text = "读取模板文件异常:" + macthingTool.ExceptionText;
                    break;
                case MatchingStatus.ErrNoModelDefined:

                    lblTestResult.Text = "请定义模板区域:" + macthingTool.ExceptionText;

                    paint = true;
                    break;
                case MatchingStatus.ErrNoSearchRegion:

                    paint = true;
                    break;
                case MatchingStatus.ErrNoImage:

                    lblTestResult.Text = "请定义图像:" + macthingTool.ExceptionText;

                    break;
                case MatchingStatus.ErrNoTestImage:
                    lblTestResult.Text = "请加载测试图像:";

                    paint = true;
                    break;
                case MatchingStatus.ErrNoValidFile:

                    lblTestResult.Text = "选择的文件不是以.shm后缀的模板文件";

                    break;
                case MatchingStatus.ErrReadingImage:
                    UpdateViewData(null, new ViewEventArgs(ViewMessage.ErrReadingImage));
                    break;
                default:
                    break;
            }
            if (paint)
                mView.Repaint();
        }


        /// <summary>
        /// Calculates new optimal values for a parameter, if the parameter is
        /// in the auto-mode list. The new settings are forwarded to the GUI
        /// components to update the display.
        /// </summary>
        public void UpdateButton(string mode)
        {
            int[] r;
            locked = true;
            switch (mode)
            {
                case MatchingParam.AUTO_ANGLE_STEP:

                    r = macthingTool.GetStepRange(MatchingParam.RANGE_ANGLE_STEP);
                    AngleStepTrackBar.Minimum = r[0];
                    AngleStepUpDown.Minimum = (decimal)(r[0] / 10.0);
                    AngleStepTrackBar.Maximum = r[1];
                    AngleStepUpDown.Maximum = (decimal)(r[1] / 10.0);

                    decimal dat = (decimal)((macthingTool.ParameterSet.AngleStep) * 180.0 / Math.PI);

                    if (AngleStepUpDown.Minimum > dat)
                    {
                        AngleStepTrackBar.Minimum = (int)dat * 10;
                        AngleStepUpDown.Minimum = (decimal)(dat);
                    }
                    if (AngleStepUpDown.Maximum < dat)
                    {
                        AngleStepTrackBar.Maximum = (int)(dat * 10);
                        AngleStepUpDown.Maximum = dat;
                    }
                    AngleStepUpDown.Value = dat;
                    break;
                case MatchingParam.AUTO_CONTRAST:
                    ContrastUpDown.Value = macthingTool.ParameterSet.Contrast;
                    break;
                case MatchingParam.AUTO_MIN_CONTRAST:
                    MinContrastUpDown.Value = macthingTool.ParameterSet.MinContrast;
                    break;
                case MatchingParam.AUTO_NUM_LEVEL:
                    PyramidLevelUpDown.Value = macthingTool.ParameterSet.PyramidLevel;
                    break;
                case MatchingParam.AUTO_OPTIMIZATION:
                    OptimizationBox.Text = macthingTool.ParameterSet.Optimization;
                    break;
                case MatchingParam.AUTO_SCALE_STEP:
                    decimal dat1 = (decimal)(macthingTool.ParameterSet.ScaleStep);
                    if (ScaleStepUpDown.Minimum > dat1)
                    {
                        ScaleStepTrackBar.Minimum = (int)(dat1 * 1000);
                        ScaleStepUpDown.Minimum = dat1;
                    }
                    if (ScaleStepUpDown.Maximum < dat1)
                    {
                        ScaleStepTrackBar.Maximum = (int)(dat1 * 1000);
                        ScaleStepUpDown.Maximum = dat1;
                    }
                    ScaleStepUpDown.Value = dat1;
                    break;
                case MatchingParam.BUTTON_ANGLE_EXTENT:
                    AngleExtentUpDown.Value = (int)(macthingTool.ParameterSet.AngleExtent * 180.0 / Math.PI);
                    break;
                case MatchingParam.BUTTON_ANGLE_START:
                    StartingAngleUpDown.Value = (int)(macthingTool.ParameterSet.StartingAngle * 180.0 / Math.PI);
                    break;
                case MatchingParam.BUTTON_METRIC:
                    MetricBox.Text = macthingTool.ParameterSet.Metric;
                    break;
                case MatchingParam.BUTTON_SCALE_MAX:
                    MaxScaleUpDown.Value = (decimal)(macthingTool.ParameterSet.MaxScale);
                    break;
                case MatchingParam.BUTTON_SCALE_MIN:
                    MinScaleUpDown.Value = (decimal)(macthingTool.ParameterSet.MinScale);
                    break;
                case MatchingParam.BUTTON_MINSCORE:
                    MinScoreUpDown.Value = (decimal)(macthingTool.ParameterSet.MinScore);
                    break;
                case MatchingParam.BUTTON_GREEDINESS:
                    GreedinessUpDown.Value = (decimal)(macthingTool.ParameterSet.Greediness);
                    break;
                case MatchingParam.BUTTON_NUM_MATCHES:
                    NumMatchesUpDown.Value = macthingTool.ParameterSet.NumMatches;
                    break;
                case MatchingParam.BUTTON_MAX_OVERLAP:
                    MaxOverlapUpDown.Value = (decimal)macthingTool.ParameterSet.MaxOverlap;
                    break;
                case MatchingParam.BUTTON_SUB_PIXEL:
                    SubPixelBox.Text = macthingTool.ParameterSet.Subpixel;
                    break;
                case MatchingParam.H_ERR_MESSAGE:
                    //MessageBox.Show("参数异常! \n" + Tool.ExceptionText,
                    //    "匹配工具",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Exclamation);
                    break;
                case MatchingParam.RANGE_ANGLE_STEP:
                    r = macthingTool.GetStepRange(MatchingParam.RANGE_ANGLE_STEP);
                    AngleStepTrackBar.Minimum = r[0];
                    AngleStepUpDown.Minimum = (decimal)(r[0] / 10.0);
                    AngleStepTrackBar.Maximum = r[1];
                    AngleStepUpDown.Maximum = (decimal)(r[1] / 10.0);


                    decimal dat2 = (decimal)((macthingTool.ParameterSet.AngleStep) * 180.0 / Math.PI);

                    if (AngleStepUpDown.Minimum > dat2)
                    {
                        AngleStepTrackBar.Minimum = (int)dat2 * 10;
                        AngleStepUpDown.Minimum = (decimal)(dat2);
                    }
                    if (AngleStepUpDown.Maximum < dat2)
                    {
                        AngleStepTrackBar.Maximum = (int)(dat2 * 10);
                        AngleStepUpDown.Maximum = dat2;
                    }
                    AngleStepUpDown.Value = dat2;
                    break;
                case MatchingParam.RANGE_SCALE_STEP:
                    r = macthingTool.GetStepRange(MatchingParam.RANGE_SCALE_STEP);
                    ScaleStepTrackBar.Minimum = r[0];
                    ScaleStepUpDown.Minimum = (decimal)(r[0] / 1000.0);
                    ScaleStepTrackBar.Maximum = r[1];
                    ScaleStepUpDown.Maximum = (decimal)(r[1] / 1000.0);


                    decimal dat3 = (decimal)(macthingTool.ParameterSet.ScaleStep);
                    if (ScaleStepUpDown.Minimum > dat3)
                    {
                        ScaleStepTrackBar.Minimum = (int)(dat3 * 1000);
                        ScaleStepUpDown.Minimum = dat3;
                    }
                    if (ScaleStepUpDown.Maximum < dat3)
                    {
                        ScaleStepTrackBar.Maximum = (int)(dat3 * 1000);
                        ScaleStepUpDown.Maximum = dat3;
                    }

                    ScaleStepUpDown.Value = dat3;
                    break;
                default:
                    break;
            }
            locked = false;
        }


        /// <summary>
        /// This method is invoked when the inspection tab or the 
        /// recognition tab are triggered to compute the optimized values
        /// and to forward the results to the display.
        /// </summary>
        public void UpdateStatisticsData(MatchingStatus mode)
        {

            switch (mode)
            {
                case MatchingStatus.UpdateRecogSattisticStatus:
                    labelOptStatus.Text = optInstance.StatusString;
                    break;
                case MatchingStatus.UpdateRecogVals:
                    labelRecogn11.Text = optInstance.RecogTabOptimizationData[0];
                    labelRecogn12.Text = optInstance.RecogTabOptimizationData[1];
                    labelRecogn13.Text = optInstance.RecogTabOptimizationData[2];
                    labelRecogn14.Text = optInstance.RecogTabOptimizationData[3];
                    break;
                case MatchingStatus.UpdateRecogOptimumVals:
                    labelRecogn21.Text = optInstance.RecogTabOptimizationData[4];
                    labelRecogn22.Text = optInstance.RecogTabOptimizationData[5];
                    labelRecogn23.Text = optInstance.RecogTabOptimizationData[6];
                    labelRecogn24.Text = optInstance.RecogTabOptimizationData[7];
                    break;
                case MatchingStatus.UpdateInspRecograte:
                    labelInspect01.Text = optInstance.InspectTabRecogRateData[0];
                    labelInspect02.Text = optInstance.InspectTabRecogRateData[1];
                    labelInspect03.Text = optInstance.InspectTabRecogRateData[2];
                    labelInspect04.Text = optInstance.InspectTabRecogRateData[3];
                    labelInspect05.Text = optInstance.InspectTabRecogRateData[4];
                    break;
                case MatchingStatus.UpdateInspStatistics:
                    labelInspect11.Text = optInstance.InspectTabStatisticsData[0];
                    labelInspect21.Text = optInstance.InspectTabStatisticsData[1];
                    labelInspect31.Text = optInstance.InspectTabStatisticsData[2];

                    labelInspect12.Text = optInstance.InspectTabStatisticsData[3];
                    labelInspect22.Text = optInstance.InspectTabStatisticsData[4];
                    labelInspect32.Text = optInstance.InspectTabStatisticsData[5];

                    labelInspect13.Text = optInstance.InspectTabStatisticsData[6];
                    labelInspect23.Text = optInstance.InspectTabStatisticsData[7];
                    labelInspect33.Text = optInstance.InspectTabStatisticsData[8];

                    labelInspect14.Text = optInstance.InspectTabStatisticsData[9];
                    labelInspect24.Text = optInstance.InspectTabStatisticsData[10];
                    labelInspect34.Text = optInstance.InspectTabStatisticsData[11];

                    labelInspect15.Text = optInstance.InspectTabStatisticsData[12];
                    labelInspect25.Text = optInstance.InspectTabStatisticsData[13];
                    labelInspect35.Text = optInstance.InspectTabStatisticsData[14];

                    labelInspect16.Text = optInstance.InspectTabStatisticsData[15];
                    labelInspect26.Text = optInstance.InspectTabStatisticsData[16];
                    labelInspect36.Text = optInstance.InspectTabStatisticsData[17];

                    labelInspect17.Text = optInstance.InspectTabStatisticsData[18];
                    labelInspect27.Text = optInstance.InspectTabStatisticsData[19];
                    labelInspect37.Text = optInstance.InspectTabStatisticsData[20];
                    break;
                case MatchingStatus.UpdateTestErr:
                    MessageBox.Show("优化失败! \n 请检查模板是否定义\n(例如是否创建模板数据)!?",
                        "匹配助手",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    break;
                case MatchingStatus.UpdateRecogErr:
                    labelOptStatus.Text = "分析失败";
                    MessageBox.Show("没有适当的参数组匹配 - \n" +
                        "请检查模板匹配参数设置!",
                        "匹配助手",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    break;
                case MatchingStatus.ErrNoTestImage:
                    UpdateMatching(MatchingStatus.ErrNoTestImage);
                    break;
                case MatchingStatus.RunSucccessful:
                    UpdateButton(MatchingParam.BUTTON_GREEDINESS);
                    UpdateButton(MatchingParam.BUTTON_MINSCORE);
                    break;
                case MatchingStatus.RunFailed:
                    setMinScore((int)MinScoreUpDown.Value);
                    setGreediness((int)GreedinessUpDown.Value);
                    break;
                default:
                    break;
            }
        }


        /********************************************************************/
        /********************************************************************/
        private void CreateModelGraphics()
        {
            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            mView.AddIconicVar(CurrentImg);
            mView.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            macthingTool.ShowTrainResult(mView);
            //if (macthingTool.ModelRegion != null)
            //{
            //    mView.ChangeGraphicSettings(Mode.COLOR, "blue");
            //    mView.ChangeGraphicSettings(Mode.LINEWIDTH, 3);
            //    mView.AddIconicVar(macthingTool.ModelRegion);
            //}
            if (ModelContour != null)
            {
                mView.ChangeGraphicSettings(Mode.COLOR, "red");
                mView.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                mView.AddIconicVar(ModelContour);
            }

            //macthingTool.RuningFinish = true;
            // Tool.ShowResult(mView);
        }

        private void ShowModelGraphics()
        {
            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            mView.AddIconicVar(CurrentImg);
            Tool.RuningFinish = true;
            macthingTool.ShowResult(mView);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                changeWindowMode(ShowMode.IncludeROI);
                CreateModelGraphics();
            }
            else
            {
                changeWindowMode(ShowMode.ExcludeROI);
                ShowModelGraphics();
            }
            mView.Repaint();
        }
        private void changeWindowMode(ShowMode mode)
        {
            HImage img = null;

            if (mode == ShowMode.ExcludeROI)
                img = macthingTool.GetCurrTestImage();

            changeWindowMode(mode, img);
        }

        private void changeWindowMode(ShowMode mode, HImage testImage)
        {
            mView.SetDispLevel(mode);

            switch (mode)
            {
                case ShowMode.IncludeROI:
                    CurrentImg = macthingTool.ImageRefIn;
                    if (macthingTool.onExternalModelID)
                    {
                    }
                    else if (CurrentImg == null)
                    {
                    }
                    else
                    {
                        //groupBoxCreateROI.Enabled = true;
                    }
                    break;

                case ShowMode.ExcludeROI: ;
                    CurrentImg = testImage;
                    //groupBoxCreateROI.Enabled = false;
                    break;
            }
        }

        private void chkUseCalib_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            Tool.IsCalibOut = chkUseCalib.Checked;
        }
    }
}
