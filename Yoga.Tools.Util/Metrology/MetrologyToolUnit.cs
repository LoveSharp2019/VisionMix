using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Yoga.ImageControl;
using HalconDotNet;
using Yoga.Tools.Matching;
using Yoga.Common;

namespace Yoga.Tools.Metrology
{
    public partial class MetrologyToolUnit : ToolsSettingUnit
    {
        /* 基本变量*/
        private HWndCtrl viewCtrl;
        public ROIController ROIController;
        public MetrologyTool tool;
        public HXLDCont mShadow;
        private HImage currImage;

        private int lineWidth;

        private bool updateLineProfile;
        public MetrologyToolUnit(MetrologyTool tool)
        {
            InitializeComponent();
            locked = true;
            viewCtrl =hWndUnit1.HWndCtrl;
            ROIController = new ROIController();

            viewCtrl.useROIController(ROIController);
            this.tool = tool;
            mShadow = new HXLDCont();

            viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateShow);
            tool.NotifyMetrologyObserver = new MetrologyDelegate(UpdateMeasureResults);
            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);
            base.Init(tool.Name, tool);
            locked = true;
        }
        public override void ShowResult()
        {
            tool.CreateMetrologyModel();
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
            //tool.SetTrainingImage(matchingTool.GetTrainingImage());
            currImage = tool.ImageRefIn;

            ROIController.Reset();
            viewCtrl.ResetWindow();
            viewCtrl.SetDispLevel(ShowMode.IncludeROI);
            //UpdateView();

        }

        private void LoadSettings()
        {
            viewCtrl.SetDispLevel(ShowMode.IncludeROI);

            ROIController.ROIList = tool.ROIList;

            currImage = tool.ImageRefIn;
            //如果无当前图像就表示为新建窗体,不用加载其他数据
            if (currImage == null)
            {
                return;
            }

            int index = 0;
            foreach (ROI roi in ROIController.ROIList)
            {
                roi.ReCreateROI();

            }
            tool.CreateMetrologyModel();
            foreach (ROI roi in ROIController.ROIList)
            {

                locked = true;
                tool.ActivedRoiIndex = index;
                index++;
                UpdateShow(null, new ViewEventArgs(ViewMessage.CreatedROI));
                locked = false;
            }
            if (index > 0)
            {
                UpdateShow(null, new ViewEventArgs(ViewMessage.ActivatedROI));
            }
            //UpdateView();
        }

        private void MetrologyToolUnit_Load(object sender, EventArgs e)
        {
           
            Init();

            LoadSettings();
        }
        private void Init()
        {
            locked = true;
            MinEdgAmplUpDown.Value = (int)MetrologyParam.INIT_THRESHOLD;
            SmoothingUpDown.Value = (int)(MetrologyParam.INIT_SIGMA * 10);
            MeasureLength1UpDown.Value = (decimal)MetrologyParam.INIT_LENGTH1;
            MeasureLength2UpDown.Value = (decimal)MetrologyParam.INIT_LENGTH2;

            numMeasuresUpDown.Value = (int)MetrologyParam.INIT_NUM_MEASURE;
            TransitionComboBox.SelectedIndex = 0;

            MetrologyMethodComboBox.DataSource = System.Enum.GetNames(typeof(MetrologyMethod));
            MetrologyMethodComboBox.SelectedIndex = this.MetrologyMethodComboBox.FindString(tool.MetrologyMethod.ToString());

            imageScaleUpDown.Value = (decimal)1.0;
            imageOffsetUpDown.Value = 0;

            txtOffset.Text = tool.Offset.ToString();
            txtScale.Text = tool.Scale.ToString();

            ResetMinEdgAmplButton.ForeColor = System.Drawing.Color.Gray;
            ResetMeasureLength1Button.ForeColor = System.Drawing.Color.Gray;
            ResetMeasureLength2Button.ForeColor = System.Drawing.Color.Gray;
            ResetSmoothingButton.ForeColor = System.Drawing.Color.Gray;
            ResetMinScoreButton.ForeColor = System.Drawing.Color.Gray;

            TransWCoordCheckBox.Enabled = false;
            TransWCoordCheckBox.Checked = false;
            UnitComboBox.SelectedIndex = 1;
            UnitPanel.Enabled = false;

            lineWidth = 2;
            mat2DMangerUnit1.Init(tool.Mat2DManger);
            locked = false;
        }
        /********************************************************************/
        /*             Update methods invoked by delegates                  */
        /********************************************************************/

        /// <summary>
        /// Updates HALCON display and result table according to new measure 
        /// results.
        /// </summary>
        /// <param name="mode">Type of measure update</param>
        void UpdateMeasureResults(MetrologyMessage mode)
        {

            txtStatus.Text = "";

            switch (mode)
            {
                case MetrologyMessage.ErrReadingFile:
                    MessageBox.Show("读取文件异常\n" + tool.ExceptionText,
                        "测量助手",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    tool.ExceptionText = "";
                    break;
                case MetrologyMessage.EventUpdateMetrology:
                case MetrologyMessage.EventUpdateResultXLD:
                case MetrologyMessage.EventUpdateRemoveObject:
                    UpdateView();
                    break;
                default:
                    break;
            }

            if (tool.ExceptionText != "")
            {

                txtStatus.Text = " 测量异常: " + tool.ExceptionText;
            }
            UpdateCodeTable();
        }


        /// <summary>
        /// Responds to changes in the selected ROI.
        /// </summary>
        /// <param name="mode">Type of ROI update</param>
        void UpdateShow(object sender, ViewEventArgs e)
        {
            switch (e.ViewMessage)
            {
                case ViewMessage.CreatedROI:
                    if (locked == false)
                    {
                        tool.AddMeasureObject();
                        tool.CreateMetrologyModel();
                    }
                    UpdateROIClickList("new");
                    UpdateMetrologyParam();
                    break;
                case ViewMessage.ActivatedROI:
                    tool.ChangeActiveROI(ROIController.ActiveRoiIdx);
                    UpdateROIClickList("active");
                    UpdateMetrologyParam();
                    break;
                case ViewMessage.MovingROI:
                    tool.CreateMetrologyModel();
                    break;
                case ViewMessage.DeletedActROI:
                    tool.RemoveMeasureObjectActIdx(ROIController.getDelROIIdx());
                    UpdateROIClickList("removeIdx");
                    UpdateMetrologyParam();
                    break;
                case ViewMessage.DelectedAllROIs:
                    //测量参数数据清除
                    tool.ParamList.Clear();
                    tool.RemoveAllMeasureObjects();
                    UpdateROIClickList("removeAll");
                    UpdateMetrologyParam();
                    break;
                default:
                    break;
            }

            if (updateLineProfile)
                PaintGraph();

        }

        private void UpdateMetrologyParam()
        {
            int index = ROIController.ActiveRoiIdx;
            if (index == -1)
            {
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = true;
                MetrologyParam param = tool.ParamList[index];
                locked = true;
                MinEdgAmplUpDown.Value = (decimal)param.Threshold.D;
                SmoothingUpDown.Value = (decimal)(param.Sigma.D * 10.0);
                MeasureLength1UpDown.Value = (decimal)(param.Length1.D);
                MeasureLength2UpDown.Value = (decimal)(param.Length2.D);
                MinScoreUpDown.Value = (decimal)(param.MinScore * 100.0);
                numMeasuresUpDown.Value = (decimal)param.NumMeasures;
                imageScaleUpDown.Value = (decimal)param.SacleImagePretreatment.Scale;
                imageOffsetUpDown.Value = (decimal)param.SacleImagePretreatment.Offset;

                switch (param.Transition)
                {
                    case MetrologyParam.TRANSITION_UNIFORM:
                        TransitionComboBox.SelectedIndex = 0;
                        break;
                    case MetrologyParam.TRANSITION_NEGATIVE:
                        TransitionComboBox.SelectedIndex = 1;
                        break;
                    case MetrologyParam.TRANSITION_POSTTIVE:
                        TransitionComboBox.SelectedIndex = 2;
                        break;
                    default:
                        break;
                }

                switch(param.MeasureSelect)
                {
                    case MetrologyParam.SELECT_ALL:
                        cbMeasureSelect.SelectedIndex = 0;
                        break;
                    case MetrologyParam.SELECT_FIRST:
                        cbMeasureSelect.SelectedIndex = 1;
                        break;
                    case MetrologyParam.SELECT_LAST:
                        cbMeasureSelect.SelectedIndex = 2;
                        break;
                    default:
                        break;
                }
                locked = false;
            }
        }


        /// <summary>
        /// Triggers an update of the HALCON window. Displays all valid iconic 
        /// objects.
        /// </summary>
        public void UpdateView()
        {
            if (currImage == null)
            {
                viewCtrl.ClearList();
            }
            else
            {
                viewCtrl.AddIconicVar(currImage);
                Tool.RuningFinish = true;
                tool.ShowResult(viewCtrl);
                txtStatus.Text = "计算结果: " + tool.Result;
            }
            viewCtrl.Repaint();
            //Util.Notify("测量结果更新");
        }


        /// <summary>
        /// 给结果图形添加阴影效果
        /// </summary>
        /// <param name="edges">
        /// Add shadows along the edges provided
        /// </param>
        public void UpdateShadowContours(HObject edges)
        {
            double shift;
            HXLDCont shadow1, shadow2;
            HHomMat2D hom2D = new HHomMat2D();

            mShadow.Dispose();
            mShadow.GenEmptyObj();

            shift = Math.Min(0.5 * lineWidth, 2.0);

            hom2D.HomMat2dIdentity();
            hom2D = hom2D.HomMat2dTranslate(shift, 1);
            shadow1 = ((HXLDCont)edges).AffineTransContourXld(hom2D);

            hom2D.HomMat2dIdentity();
            hom2D = hom2D.HomMat2dTranslate(1, shift);
            shadow2 = ((HXLDCont)edges).AffineTransContourXld(hom2D);

            mShadow = mShadow.ConcatObj(shadow1);
            mShadow = mShadow.ConcatObj(shadow2);

            shadow1.Dispose();
            shadow2.Dispose();
        }


        /// <summary>
        /// 修改表头
        /// </summary>
        public void UpdateMeasureResultComposition()
        {
            List<string> composition;
            ColumnHeader header;
            int length;
            int size = -1;
            //表头
            composition = tool.GetMeasureResultComposition();
            if (composition == null)
            {
                return;
            }
            length = composition.Count;
            EdgeResultListView.Columns.Clear();

            if (length <= 8 && length > 0)
                size = (EdgeResultListView.Size.Width / length) - 1;

            for (int i = 0; i < length; i++)
            {
                header = new ColumnHeader();
                header.Text = composition[i];
                header.Width = (size != -1) ? size : 20 + header.Text.Length * 5;
                EdgeResultListView.Columns.Add(header);
            }

            UpdateCodeTable();
        }
        /// <summary>创建数据结果表</summary>
        public void UpdateCodeTable()
        {
            List<HTuple> table;
            ListViewItem item;
            int rowCount, colCount;
            string val;
            rowCount = 0;

            table = tool.GetMeasureTableData();
            EdgeResultListView.Items.Clear();

            if (table == null || (colCount = table.Count) == 0 || table[0] == null)
                return;

            for (int i = 0; i < colCount; i++)
                rowCount = Math.Max(rowCount, (table[i]).Length);

            for (int i = 0; i < rowCount; i++)
            {
                val = (i >= (table[0]).Length) ? "" : (table[0])[i].D.ToString("f6");
                item = new ListViewItem(val);

                for (int j = 1; j < colCount; j++)
                {
                    val = (i >= (table[j]).Length) ? "" : (table[j])[i].D.ToString("f6");
                    item.SubItems.Add(val);
                }
                EdgeResultListView.Items.Add(item);
            }
        }


        /// <summary>
        /// Updates listbox displaying current set of ROIs.
        /// </summary>
        public void UpdateROIClickList(string mode)
        {
            int count = ROIController.ROIList.Count;

            switch (mode)
            {
                case "active":
                    ActiveROIListBox.SelectedIndex = tool.ActivedRoiIndex;
                    break;
                case "new":
                    ActiveROIListBox.Items.Add("ROI " + (tool.ActivedRoiIndex + 1).ToString("d2"));
                    ActiveROIListBox.SelectedIndex = tool.ActivedRoiIndex;
                    break;
                case "removeIdx": 
                    ActiveROIListBox.Items.Clear();
                    for (int i = 1; i <= count; i++)
                        ActiveROIListBox.Items.Add("ROI " + i.ToString("d2"));
                    break;
                case "removeAll":
                    ActiveROIListBox.Items.Clear();
                    break;
            }
        }


        /// <summary>
        /// Updates function plot (line profile) of measure projection for the 
        /// selected ROI.
        /// </summary>
        public void PaintGraph()
        {
            double[] grayVals;

            grayVals = tool.GetMeasureProjection();
            functionPlotUnit1.SetFunctionPlotValue(grayVals);
            functionPlotUnit1.ComputeStatistics(grayVals);
        }
        private void LineButton_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROILine());
        }

        private void CircArcButton_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShape(new ROICircularArc());
        }

        private void DeleteActRoiButton_Click(object sender, EventArgs e)
        {
            ROIController.RemoveActive();
        }

        private void ResetROIButton_Click(object sender, EventArgs e)
        {
            ROIController.Reset();
            UpdateView();
        }

        private void MeasureLength1UpDown_ValueChanged(object sender, EventArgs e)
        {

            int val = (int)MeasureLength1UpDown.Value;
            MeasureLength1TrackBar.Value = val;
            if (locked == false)
            {
                setMeasureLength1(val);
            }
        }

        private void MeasureLength1TrackBar_Scroll(object sender, EventArgs e)
        {
            MeasureLength1UpDown.Value = MeasureLength1TrackBar.Value;
            MeasureLength1UpDown.Refresh();
        }

        private void ResetMeasureLength1Button_Click(object sender, EventArgs e)
        {
            MeasureLength1UpDown.Value = (int)MetrologyParam.INIT_LENGTH1;
            ResetMeasureLength1Button.ForeColor = System.Drawing.Color.Gray;
        }
        private void setMeasureLength1(int val)
        {
            ResetMeasureLength1Button.ForeColor = System.Drawing.Color.Black;
            tool.SetMeasureLength1(val);
        }

        private void MeasureLength2UpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)MeasureLength2UpDown.Value;
            MeasureLength2TrackBar.Value = val;
            if (locked == false)
            {
                setMeasureLength2(val);
            }
        }

        private void MeasureLength2TrackBar_Scroll(object sender, EventArgs e)
        {
            MeasureLength2UpDown.Value = MeasureLength2TrackBar.Value;
            MeasureLength2UpDown.Refresh();
        }

        private void ResetMeasureLength2Button_Click(object sender, EventArgs e)
        {
            MeasureLength2UpDown.Value = (int)MetrologyParam.INIT_LENGTH2;
            ResetMeasureLength2Button.ForeColor = System.Drawing.Color.Gray;
        }
        private void setMeasureLength2(int val)
        {
            ResetMeasureLength2Button.ForeColor = System.Drawing.Color.Black;

            tool.SetMeasureLength2(val);
        }

        #region 设置sigma
        private void SmoothingUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)SmoothingUpDown.Value;
            SmoothingTrackBar.Value = val;
            if (locked == false)
            {
                setSigma(val);
            }
        }

        private void SmoothingTrackBar_Scroll(object sender, EventArgs e)
        {
            SmoothingUpDown.Value = SmoothingTrackBar.Value;
            SmoothingUpDown.Refresh();
        }

        private void ResetSmoothingButton_Click(object sender, EventArgs e)
        {
            SmoothingUpDown.Value = (int)(MetrologyParam.INIT_SIGMA * 10);
            ResetSmoothingButton.ForeColor = System.Drawing.Color.Gray;
        }
        private void setSigma(int val)
        {
            double valD = val / 10.0;

            ResetSmoothingButton.ForeColor = System.Drawing.Color.Black;

            tool.SetSigma(valD);
        }


        #endregion

        private void MinScoreUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)MinScoreUpDown.Value;
            MinScoreTrackBar.Value = val;
            if (locked == false)
            {
                setMinScore(val);
            }
        }

        private void MinScoreTrackBar_Scroll(object sender, EventArgs e)
        {
            MinScoreUpDown.Value = MinScoreTrackBar.Value;
            MinScoreUpDown.Refresh();
        }

        private void ResetMinScoreButton_Click(object sender, EventArgs e)
        {
            MinScoreUpDown.Value = (int)(MetrologyParam.INIT_MIN_SCORE * 100);
            ResetMinScoreButton.ForeColor = System.Drawing.Color.Gray;
        }
        private void setMinScore(int val)
        {
            double valD = val / 100.0;

            ResetMinScoreButton.ForeColor = System.Drawing.Color.Black;

            tool.SetMinScore(valD);
        }

        #region 设置min EdgeAmpl
        private void MinEdgAmplUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)MinEdgAmplUpDown.Value;
            MinEdgAmplTrackBar.Value = val;
            if (locked == false)
            {
                setMinEdgeAmpl(val);
            }
        }

        private void MinEdgAmplTrackBar_Scroll(object sender, EventArgs e)
        {
            MinEdgAmplUpDown.Value = MinEdgAmplTrackBar.Value;
            MinEdgAmplUpDown.Refresh();
        }

        private void ResetMinEdgAmplButton_Click(object sender, EventArgs e)
        {
            MinEdgAmplUpDown.Value = (int)MetrologyParam.INIT_THRESHOLD;
            ResetMinEdgAmplButton.ForeColor = System.Drawing.Color.Gray;
        }
        private void setMinEdgeAmpl(int val)
        {
            ResetMinEdgAmplButton.ForeColor = System.Drawing.Color.Black;
            tool.SetMinEdgeAmpl(val);
        }


        #endregion
        private void TransitionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked == false)
            {
                switch (TransitionComboBox.SelectedIndex)
                {
                    case 0:
                        tool.SetTransition(MetrologyParam.TRANSITION_UNIFORM);
                        break;
                    case 1:
                        tool.SetTransition(MetrologyParam.TRANSITION_NEGATIVE);
                        break;
                    case 2:
                        tool.SetTransition(MetrologyParam.TRANSITION_POSTTIVE);
                        break;

                }
            }
        }
        private void ScaleUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked == false)
            {
                tool.SetImageScale((double)imageScaleUpDown.Value);
            }
        }

        private void OffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (locked == false)
            {
                tool.SetImageOffset((int)imageOffsetUpDown.Value);
            }
        }
        private void TransWCoordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TransWCoordCheckBox.Checked)
                UnitPanel.Enabled = true;
            else
                UnitPanel.Enabled = false;

            //mAssistant.setTransWorldCoord(TransWCoordCheckBox.Checked);
        }

        private void UnitComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //mAssistant.setUnit((string)UnitComboBox.SelectedItem);
        }
        //--------------------
        private void LoadCamParamButton_Click(object sender, EventArgs e)
        {
            HTuple buttonType, valType;
            string file;
            string[] val;
            bool isCalFile;


            txtStatus.Text = "";
            buttonType = new HTuple(sender.ToString());
            valType = buttonType.TupleStrrstr(new HTuple("相机参数"));

            if (valType[0].I > 0)
                openCamparFileDialog.FilterIndex = 1;
            else
                openCamparFileDialog.FilterIndex = 2;

            openCamparFileDialog.FileName = "";

            if (openCamparFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openCamparFileDialog.FileName;
                txtStatus.Text = "";

                if ((isCalFile = file.EndsWith(".cal")) || file.EndsWith(".dat"))
                {

                    try
                    {
                        if (valType[0].I > 0)
                        {
                            tool.SetCamParFile(file);
                            val = file.Split(new Char[] { '\\' });
                            file = val[val.Length - 1];
                            CalibCamTextBox.Text = file;

                        }
                        else
                        {
                            tool.SetCamPoseFile(file);
                            val = file.Split(new Char[] { '\\' });
                            file = val[val.Length - 1];
                            CalibPoseTextBox.Text = file;
                        }
                    }
                    catch (HOperatorException ex)
                    {
                        Util.WriteLog(this.GetType(), ex);
                        if (valType[0].I > 0)
                            CalibCamTextBox.Text = "*.cal";
                        else
                            CalibPoseTextBox.Text = "*.dat";

                        txtStatus.Text = tool.ExceptionText;
                        valType = tool.ExceptionText;
                        MessageBox.Show("File is corrupted or has a wrong format:\nPlease check if you chose a valid calibration file (or format)!",
                                         "Measure Assistant",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);

                    }

                }
                else
                {
                    MessageBox.Show("Fileformat is wrong: Data is not a calibration file!",
                                     "Measure Assistant",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
                }


                if (tool.IsCalibValid)
                {
                    TransWCoordCheckBox.Enabled = true;
                }
                else
                {
                    TransWCoordCheckBox.Enabled = false;
                    TransWCoordCheckBox.Checked = false;
                }
            }
        }

        private void ActiveROIListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ROIController.ActiveRoiIdx = ActiveROIListBox.SelectedIndex;

            tool.ActivedRoiIndex = ROIController.ActiveRoiIdx;
            UpdateView();
            UpdateMeasureResultComposition();
            UpdateCodeTable();

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tab = tabControl.SelectedIndex;
            updateLineProfile = false;

            if (tab == 2)
            {
                updateLineProfile = true;
                PaintGraph();
            }
        }

        private void MetrologyMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked == false)
            {
                tool.MetrologyMethod = (MetrologyMethod)Enum.Parse(typeof(MetrologyMethod),
                    MetrologyMethodComboBox.SelectedItem.ToString(), false);
                tool.RunRef();
                UpdateMeasureResults(MetrologyMessage.EventUpdateMetrology);
            }
        }

        private void numMeasuresUpDown_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)numMeasuresUpDown.Value;
            numMeasuresTrackBar.Value = val;
            if (locked == false)
            {
                setNumMeasures(val);
            }
        }

        private void numMeasuresTrackBar_Scroll(object sender, EventArgs e)
        {
            numMeasuresUpDown.Value = numMeasuresTrackBar.Value;
            numMeasuresUpDown.Refresh();
        }

        private void ResetNumMeasuresButton_Click(object sender, EventArgs e)
        {
            numMeasuresUpDown.Value = (int)MetrologyParam.INIT_NUM_MEASURE;
            ResetNumMeasuresButton.ForeColor = System.Drawing.Color.Gray;
        }

        void setNumMeasures( int num)
        {
            ResetNumMeasuresButton.ForeColor = System.Drawing.Color.Black;

            tool.SetNumMeasures(num);
        }
        private bool convrtToDouble(TextBox tb, out double result)
        {
            result = 0;
            if (double.TryParse(tb.Text.Trim(), out result) == false)
            {
                MessageBox.Show("数据输入异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tb.Focus();
                return false;
            }
            return true;

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            double scale, offset;

            if (convrtToDouble(txtScale, out scale) == false)
            {
                return;
            }

            if (convrtToDouble(txtOffset, out offset) == false)
            {
                return;
            }
            tool.Scale = scale;
            tool.Offset = offset;
            tool.RunRef();
            UpdateMeasureResults(MetrologyMessage.EventUpdateMetrology);
        }

        private void cbMeasureSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked == false)
            {
                switch (cbMeasureSelect.SelectedIndex)
                {
                    case 0:
                        tool.SetMeasureSelect(MetrologyParam.SELECT_ALL);
                        break;
                    case 1:
                        tool.SetMeasureSelect(MetrologyParam.SELECT_FIRST);
                        break;
                    case 2:
                        tool.SetMeasureSelect(MetrologyParam.SELECT_LAST);
                        break;

                }
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
