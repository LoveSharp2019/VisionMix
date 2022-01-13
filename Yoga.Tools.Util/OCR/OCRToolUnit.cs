using System;
using System.Collections.Generic;
using System.Windows.Forms;

using HalconDotNet;
using System.IO;
using Yoga.Tools.Matching;
using Yoga.ImageControl;

namespace Yoga.Tools.OCR
{
    public partial class OCRToolUnit : ToolsSettingUnit
    {
        OCRTool tool;
        /// <summary>halcon窗体控制</summary>
        private HWndCtrl hWndCrtl;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;
        private List<string> omcFilelist = new List<string>();

        private HImage CurrentImg;
        public OCRToolUnit(OCRTool tool)
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
            mat2DMangerUnit1.Init(tool.Mat2DManger);

            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);
            locked = false;
        }
        public override void ShowResult()
        {
            btnShowRefImage_Click(null, null);
        }
        private void On_SelectMatchingTool(IMatching matchingTool)
        {
            if (matchingTool == null)
            {
                return;
            }
            if (locked)
            {
                return;
            }
            if (tool.ImageRefIn != null)
            {
                hWndCrtl.ClearList();
                hWndCrtl.AddIconicVar(tool.ImageRefIn);
                hWndCrtl.Repaint();
            }
        }

        private void Init()
        {
            #region 字库下拉框初始化
            string path = Environment.CurrentDirectory + "\\ocr";
            DirectoryInfo dir = new DirectoryInfo(path);

            if (dir.Exists)
            {
                FileInfo[] inf = dir.GetFiles();
                omcFilelist.Clear();
                foreach (var item in inf)
                {
                    if (item.Extension.Equals(".omc")|| item.Extension.Equals(".occ"))
                    {
                        omcFilelist.Add(item.FullName);
                        comboBoxMplClass.Items.Add(Path.GetFileNameWithoutExtension(item.FullName));
                    }
                }

            }
            if (omcFilelist.Count > 0)
            {
                int index = omcFilelist.FindIndex(x => x == tool.OcrClassPath);
                if (index > -1)
                {
                    comboBoxMplClass.SelectedIndex = index;
                }
            }
            #endregion

            UpDownMinContrast.Value = (decimal)tool.MinContrast;
            UpDownMaxCharHeight.Value = tool.CharHeight;
            UpDownMinCharHeight.Value = tool.MinCharHeight;

            UpDownMinCharWidth.Value = tool.MinCharWidth;
            UpDownMaxCharWidht.Value = tool.MaxCharWidth;

            UpDownMinStrokeWidth.Value = tool.MinStrokeWidth;
            UpDownMaxStrokeWidht.Value = tool.MaxStrokeWidth;

            chkAddFragments.Checked = tool.IsAddFragments;
            txtLineSeparators.Text = tool.TextLineSeparators;
            txtLineStructure.Text = tool.TextLineStructure;
            chkIsDotPrint.Checked = tool.IsDotPrint;
            txtExpression.Text = tool.Expression;
            switch (tool.Polarity)
            {
                case "dark_on_light":
                    // radioButtonWhite.Checked = false;
                    radioButtonBlack.Checked = true;
                    break;
                case "light_on_dark":
                    radioButtonWhite.Checked = true;
                    // radioButtonBlack.Checked = false;
                    break;
                case "both":
                    radioButtonBoth.Checked = true;
                    break;
                default:
                    break;
            }
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
            string resultMessage = string.Format("{0}: 查找结果\r\n找到字符{1}分数{2}  用时{3:f2}ms",
              timeNow, tool.ResultText, tool.Result, tool.ExecutionTime);
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
            txtResult.Text = "查找中";
            try
            {
                ChangeShowMode(ShowMode.ExcludeROI);
                tool.CreateOCRTool();
                tool.RunRef();
                CurrentImg = tool.ImageRefIn;
                ShowModelGraphics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "查找异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButtonWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            if (radioButtonWhite.Checked)
            {
                tool.Polarity = "light_on_dark";
            }
            else if (radioButtonBlack.Checked)
            {
                tool.Polarity = "dark_on_light";
            }
            else
            {
                tool.Polarity = "both";
            }
        }

        private void comboBoxMplClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            if (comboBoxMplClass.SelectedIndex < 0)
            {
                return;
            }
            tool.OcrClassPath = omcFilelist[comboBoxMplClass.SelectedIndex];
        }

        private void txtLineSeparators_TextChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.TextLineSeparators = txtLineSeparators.Text;
        }

        private void txtLineStructure_TextChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.TextLineStructure = txtLineStructure.Text;
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

        private void UpDownMinContrast_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MinContrast = (double)UpDownMinContrast.Value;
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.Expression = txtExpression.Text;
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
            if (tool.Mat2DManger.UseMat2D)
            {
                ToolBase toolTmp = tool.Mat2DManger.MatchingTool as ToolBase;
                toolTmp.Run(CurrentImg);
            }
            tool.Run(CurrentImg);

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

        private void chkIsDotPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.IsDotPrint = chkIsDotPrint.Checked;
        }

        private void UpDownMaxCharHeight_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.CharHeight = (int)UpDownMaxCharHeight.Value;
        }

        private void UpDownMinCharHeight_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MinCharHeight = (int)UpDownMinCharHeight.Value;
        }

        private void UpDownMinCharWidth_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MinCharWidth = (int)UpDownMinCharWidth.Value;
        }

        private void UpDownMaxCharWidht_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MaxCharWidth = (int)UpDownMaxCharWidht.Value;
        }

        private void UpDownMinStrokeWidth_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MinStrokeWidth = (int)UpDownMinStrokeWidth.Value;
        }

        private void UpDownMaxStrokeWidht_ValueChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.MaxStrokeWidth = (int)UpDownMaxStrokeWidht.Value;
        }

        private void chkAddFragments_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.IsAddFragments = chkAddFragments.Checked;
        }
    }
}
