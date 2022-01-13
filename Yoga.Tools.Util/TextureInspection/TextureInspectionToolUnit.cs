using System;
using System.Windows.Forms;
using HalconDotNet;
using System.Collections.Generic;
using Yoga.Tools.Matching;
using System.Linq;
using Yoga.ImageControl;

namespace Yoga.Tools.TextureInspection
{
    public partial class TextureInspectionToolUnit : ToolsSettingUnit
    {
        private TextureInspectionTool tool;
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;

        private HRegion ModelRegion;
        private HImage CurrentImg;

        public TextureInspectionToolUnit(TextureInspectionTool tool)
        {
            InitializeComponent();
            locked = true;
            mView = hWndUnit1.HWndCtrl;
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
                    //tool.ModelRegion = ModelRegion;
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
            ROIController.ROIList = tool.ROIList;
            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);
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


        }
        private void ChangeShowMode(ShowMode showMode)
        {
            mView.SetDispLevel(showMode);
            //switch (showMode)
            //{
            //    case ShowMode.IncludeROI:
            //        groupBoxCreateROI.Enabled = true;
            //        break;
            //    case ShowMode.ExcludeROI:
            //        groupBoxCreateROI.Enabled = false;
            //        break;
            //    default:
            //        break;
            //}
        }
        private void showCheckResult()
        {

            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            mView.AddIconicVar(CurrentImg);
            Tool.RuningFinish = true;

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
        public void ShowModelGraphics()
        {
            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            mView.AddIconicVar(CurrentImg);
            if (ModelRegion != null)
            {
                mView.ChangeGraphicSettings(Mode.COLOR, "green");
                mView.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                mView.AddIconicVar(ModelRegion);
            }
            mView.Repaint();
        }

        public void ShowCurrentImage()
        {

            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (CurrentImg == null)
            {
                return;
            }
            mView.AddIconicVar(CurrentImg);
            mView.Repaint();
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
                mView.ClearList();
                mView.Repaint();
            }
        }

        private void trainImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (trainImgListBox.SelectedIndex < 0)
            {
                mView.ClearList();
                mView.Repaint();
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

            if (trainImgListBox.Items.Count < 1 )
            {
                MessageBox.Show("无训练图像参数" , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            ChangeShowMode(ShowMode.ExcludeROI);
            lblResult.Text = "模板训练中...";
            Application.DoEvents();
            try
            {
                tool.TrainTextureInspectionModel();
                lblResult.Text = "训练完成";
            }
            catch (Exception ex)
            {
                MessageBox.Show("训练发生异常"+ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

                mView.ClearList();
                ChangeShowMode(ShowMode.ExcludeROI);
                mView.Repaint();
            }
        }

        private void testModelButton_Click(object sender, EventArgs e)
        {
            if (testImgListBox.Items.Count != 0)
                ChangeShowMode(ShowMode.ExcludeROI);

            CurrentImg = tool.ImageTestIn;
            lblResult.Text = "纹理检测中...";

            tool.Run(CurrentImg);
            ChangeShowMode(ShowMode.ExcludeROI);
            showCheckResult();
        }

        private void testImgListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file;

            if (testImgListBox.SelectedIndex < 0)
            {
                mView.ClearList();
                mView.Repaint();
                return;
            }

            file = (string)testImgListBox.SelectedItem;
            tool.SetTestImage(file);
            if (FindAlwaysCheckBox.Checked)
            {
                CurrentImg = tool.ImageTestIn;
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
            OpeningRadiusUpDown.Value = (decimal)TextureInspectionTool.OpeningRadiusInit;
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

            if (var == TextureInspectionTool.OpeningRadiusInit)
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
            leastAreaUpDown.Value = (decimal)TextureInspectionTool.LeastAreaInit;
            leastAreaUpDown.Refresh();
        }
        private void leastAreaUpDown_ValueChanged(object sender, EventArgs e)
        {
            double var = (double)leastAreaUpDown.Value;
            leastAreaTrackBar.Value = (int)(var * 10.0);

            if (var == TextureInspectionTool.LeastAreaInit)
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


        private void PrintCheckToolUnit_Load(object sender, EventArgs e)
        {

        }
    }
}
