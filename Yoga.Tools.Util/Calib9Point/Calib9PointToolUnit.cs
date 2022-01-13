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

namespace Yoga.Tools.Calib9Point
{
    public partial class Calib9PointToolUnit : ToolsSettingUnit
    {
        Calib9PointTool tool;

        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        public Calib9PointToolUnit(Calib9PointTool tool)
        {
            InitializeComponent();
            mView = hWndUnit1.HWndCtrl;
            this.tool = tool;
            base.Init(tool.Name, tool);
            this.dgvRealPos.DataSource = tool.Points;
            this.dgvRealPos.Refresh();
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            tool.GetImage();
            if (tool.TestImage != null)
            {
                mView.AddIconicVar(tool.TestImage);
                mView.Repaint();
            }
        }

        private void btnMatchingSetting_Click(object sender, EventArgs e)
        {
            tool.MatchingTool.ClearTestData();
            tool.MatchingTool.ImageRefIn = tool.TrainImage;
            ToolsSettingUnit settingUnit = tool.MatchingTool.GetSettingUnit() as ToolsSettingUnit;

            frmToolSetting toolSetting = new frmToolSetting(settingUnit);

            toolSetting.ShowDialog();
        }

        private void btnCalib_Click(object sender, EventArgs e)
        {
            try
            {
                tool.DoCalibration();
                DateTime dt = DateTime.Now;
                string timeNow = dt.ToString("yyyy/MM/dd HH:mm:ss");
                txtResult.Text = string.Format("{0}标定成功,\r\n误差X:{1}mm,\r\nY: {2}mm", timeNow, tool.ErrRowMax, tool.ErrColMax);
            }
            catch(Exception ex  )
            {
                txtResult.Text = "标定失败: " + ex.Message;
            }
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRealPos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }
        private void btnSetModelImage_Click(object sender, EventArgs e)
        {
            if (tool.TestImage == null || tool.TestImage.IsInitialized() == false)
            {
                MessageBox.Show("当前无新的图像数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tool.TrainImage = tool.TestImage;
            ShowRefImage();
        }

        private void Calib9PointToolUnit_Load(object sender, EventArgs e)
        {
            ShowRefImage();
        }
        private void ShowRefImage()
        {
            if (tool.TrainImage != null && tool.TrainImage.IsInitialized())
            {
                mView.AddIconicVar(tool.TrainImage);
                mView.AddText("模板图像", 20, 20, 20, "blue");
                mView.Repaint();
            }
        }

        private void btnMatchingTest_Click(object sender, EventArgs e)
        {
            txtResult.Text = "定位中";
            txtResult.Text = tool.DoMatching();
            if (tool.TestImage != null && tool.TestImage.IsInitialized())
            {
                mView.AddIconicVar(tool.TestImage);
                tool.MatchingTool.ShowResult(mView);
                mView.Repaint();
            }
        }

        private void rdbtnPos_CheckedChanged(object sender, EventArgs e)
        {
            int index = int.Parse(((RadioButton)sender).Text);
            dgvRealPos.ClearSelection();
            dgvRealPos.Rows[index - 1].Cells[0].Selected = true;
        }

        private void btnPixImput_Click(object sender, EventArgs e)
        {
            if (dgvRealPos.SelectedCells.Count < 1)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Matching.MatchingResult result = tool.MatchingTool.MatchingResults;
            if (result == null || result.Count != 1)
            {
                MessageBox.Show("未获取到位置数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dgvRealPos.SelectedCells[0].RowIndex;
            string message = string.Format("是否修改点{0}的像素数据?", index + 1);
            DialogResult r1 = MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (r1 != DialogResult.OK)
            {
                return;
            }
            tool.Points[index].Px = result.Col.D;
            tool.Points[index].Py = result.Row.D;
            dgvRealPos.Refresh();
        }

        private void btnVerification_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            Matching.MatchingResult result = tool.MatchingTool.MatchingResults;
            if (result == null || result.Count != 1)
            {
                MessageBox.Show("未获取到位置数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HTuple rx, ry;
            tool.ImagePointsToWorldPlane(result.Col.D, result.Row.D, out rx, out ry);
            txtResult.Text= string.Format("像素:x{0:f4} y{1:f4},结果:x{2:f4} y{3:f4}", result.Col.D, result.Row.D,  rx.D,  ry.D);
        }

        private void dgvRealPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                if (sender != null && sender.GetType() == typeof(DataGridView))
                {
                    try
                    {
                        // 获取剪切板的内容，并按行分割
                        string pasteText = Clipboard.GetText();
                        if (string.IsNullOrEmpty(pasteText))
                            return;
                        string[] lines = pasteText.Split(Environment.NewLine.ToCharArray());

                        BindingList<CalibPos> points = new BindingList<CalibPos>();
                        foreach (string line in lines)
                        {
                            if (string.IsNullOrEmpty(line.Trim()))
                                continue;
                            // 按 Tab 分割数据
                            string[] vals = line.Split('\t');
                            CalibPos pos = new CalibPos();
                            pos.Px =double.Parse( vals[0]);
                            pos.Py=double.Parse(vals[1]);
                            pos.Rx = double.Parse(vals[2]);
                            pos.Ry = double.Parse(vals[3]);
                            points.Add(pos);
                        }
                        if (points.Count==9)
                        {
                            tool.Points = points;
                            this.dgvRealPos.DataSource = tool.Points;
                            this.dgvRealPos.Refresh();
                        }
                        else
                        {
                            throw new Exception("数据格式不对,需要9*9表格数据");
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("数据粘贴异常:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
