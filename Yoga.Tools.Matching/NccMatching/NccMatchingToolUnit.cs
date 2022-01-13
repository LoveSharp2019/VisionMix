using System;
using System.Collections.Generic;
using System.Windows.Forms;

using HalconDotNet;
using System.IO;
using Yoga.Tools.Matching;
using Yoga.ImageControl;

namespace Yoga.Tools.NccMatching
{
    public partial class NccMatchingToolUnit : ToolsSettingUnit
    {
        NccMatchingTool tool;
        /// <summary>halcon窗体控制</summary>
        private HWndCtrl hWndCrtl;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;
        private List<string> omcFilelist = new List<string>();

        private HImage CurrentImg;
        public NccMatchingToolUnit(NccMatchingTool tool)
        {
            InitializeComponent();
            hWndCrtl = hWndUnit1.HWndCtrl;

            ROIController = new ROIController();

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            hWndCrtl.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);
            this.tool = tool;
            if (tool.ROIList != null)
            {
                ROIController.ROIList = tool.ROIList;
            }

            locked = true;
            Init();
            
            base.Init(tool.Name, tool);
            locked = false;
        }

        public override void ShowResult()
        {
            btnShowRefImage_Click(null, null);
        }
        private void Init()
        {
            ckbPyramidLevelAuto.Checked = tool.IsAutoNumLevels;
            PyramidLevelUpDown.Enabled =! ckbPyramidLevelAuto.Checked;
            PyramidLevelUpDown.Value = tool.NumLevels;
            AngleExtentUpDown.Value =(decimal) tool.AngleExtent;
            MinScoreUpDown.Value=(decimal) tool.MinScore;
            NumMatchesUpDown.Value= tool.NumMatches;

            comboBoxAccuracy.DataSource = System.Enum.GetNames(typeof(Level));
            comboBoxAccuracy.SelectedIndex = this.comboBoxAccuracy.FindString(tool.Accuracy.ToString());

            chkUseCalib.Checked = tool.IsCalibOut;
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
                        //ShowModelGraphics();
                    }
                    tool.ModelRegion = ROIController.GetModelRegion();
                    tool.ROIList = ROIController.ROIList;
                    if (tool.ImageRefIn != null)
                    {
                        hWndCrtl.ClearList();
                        hWndCrtl.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());

                        hWndCrtl.AddIconicVar(tool.ImageRefIn);
                        hWndCrtl.Repaint();
                    }

                    break;
                case ViewMessage.ErrReadingImage:
           
                    break;
                default:
                    break;
            }
        }
        private void ShowModelGraphics()
        {
            DateTime dt = DateTime.Now;
            string timeNow = dt.ToString("HH:mm:ss");
            string resultMessage = string.Format("{0}: 查找结果\r\n结果分数{1}  用时{2:f2}ms",
              timeNow, tool.Result, tool.ExecutionTime);
            txtResult.Text = resultMessage;

            if (tool.ImageRefIn == null)
            {
                return;
            }
            hWndCrtl.ClearList();
            hWndCrtl.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());

            hWndCrtl.AddIconicVar(CurrentImg);
            Tool.RuningFinish = true;
            tool.ShowResult(hWndCrtl);
            hWndCrtl.Repaint();
        }
        private void TestModelButton_Click(object sender, EventArgs e)
        {
            txtResult.Text = "训练中";
            try
            {
                ChangeShowMode(ShowMode.ExcludeROI);
                tool.CreateNccMatchingTool(true);
                tool.RunRef();
                CurrentImg = tool.ImageRefIn;
                //if (tool.IsAutoNumLevels)
                {
                    locked = true;
                    PyramidLevelUpDown.Value = tool.NumLevels;
                    locked = false;
                }
                ShowModelGraphics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OCRToolUnit_Load(object sender, EventArgs e)
        {
            if (tool.ImageRefIn != null)
            {
                hWndCrtl.ClearList();
                hWndCrtl.AddIconicVar(tool.ImageRefIn);
                hWndCrtl.Repaint();
            }
        }

        private void ChangeShowMode(ShowMode showMode)
        {
            hWndCrtl.SetDispLevel(showMode);
            switch (showMode)
            {
                case ShowMode.IncludeROI:
                    groupBoxCreateROI.Enabled = true;
                    groupBox2.Enabled = true;
                    break;
                case ShowMode.ExcludeROI:
                    groupBoxCreateROI.Enabled = false;
                    groupBox2.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void btnShowRefImage_Click(object sender, EventArgs e)
        {
            ChangeShowMode(ShowMode.IncludeROI);

            if (tool.ImageRefIn != null)
            {
                hWndCrtl.ClearList();
                hWndCrtl.AddIconicVar(tool.ImageRefIn);
                hWndCrtl.Repaint();
            }
        }

        public void ShowCurrentImage()
        {

            hWndCrtl.ClearList();
            hWndCrtl.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            hWndCrtl.AddIconicVar(CurrentImg);
            hWndCrtl.Repaint();
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

                hWndCrtl.ClearList();
                ChangeShowMode(ShowMode.ExcludeROI);
                hWndCrtl.Repaint();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                ChangeShowMode(ShowMode.ExcludeROI);

            if (tool.ImageTestIn == null || tool.ImageTestIn.IsInitialized() == false)
            {
                return;
            }
            CurrentImg = tool.ImageTestIn;
            //if (tool.Mat2DManger.UseMat2D)
            //{
            //    ToolBase toolTmp = tool.Mat2DManger.MatchingTool as ToolBase;
            //    toolTmp.Run(CurrentImg);
            //}
            tool.RunRef();

            ChangeShowMode(ShowMode.ExcludeROI);
            ShowModelGraphics();
        }

        private void testImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (testImgListBox.SelectedIndex < 0)
            {
                hWndCrtl.ClearList();
                hWndCrtl.Repaint();
                return;
            }

            file = (string)testImgListBox.SelectedItem;
            tool.SetTestImage(file);
            if (FindAlwaysCheckBox.Checked)
            {
                btnTest_Click(null, null);
            }
            else if (testImgListBox.Items.Count > 0)
            {
                file = (string)testImgListBox.SelectedItem;
                CurrentImg = tool.GetImageTest(file);

                ChangeShowMode(ShowMode.ExcludeROI);

                ShowCurrentImage();
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
            tool.SearchRegion = null;
        }

        private void reduceRect1Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShapeNoOperator(new ROIRectangle2("查找区域"));
        }

        private void PyramidLevelUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.NumLevels = (int)PyramidLevelUpDown.Value;
        }

        private void ckbPyramidLevelAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            PyramidLevelUpDown.Enabled = !ckbPyramidLevelAuto.Checked;
            tool.IsAutoNumLevels = ckbPyramidLevelAuto.Checked;
            PyramidLevelUpDown.Value = tool.NumLevels;
        }

        private void AngleExtentUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.AngleExtent =(double) AngleExtentUpDown.Value;
        }

        private void MinScoreUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MinScore =(double) MinScoreUpDown.Value;
        }

        private void NumMatchesUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.NumMatches =(int) NumMatchesUpDown.Value;
        }

        private void comboBoxAccuracy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.Accuracy = (Level)Enum.Parse(typeof(Level),
                    comboBoxAccuracy.SelectedItem.ToString(), false);
        }

        private void chkUseCalib_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.IsCalibOut = chkUseCalib.Checked;
        }
    }
}
