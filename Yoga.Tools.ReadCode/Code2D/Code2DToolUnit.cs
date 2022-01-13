using System;
using System.Windows.Forms;
using HalconDotNet;
using Yoga.ImageControl;
using System.Collections.Generic;
using System.Linq;

namespace Yoga.Tools.Code2D
{
    public partial class Code2DToolUnit : ToolsSettingUnit
    {
        
        private Code2DTool tool;
        /// <summary>halcon窗体控制</summary>
        public HWndCtrl mView;
        /// <summary>ROI 管理控制</summary>
		public ROIController ROIController;

        public Code2DToolUnit(Code2DTool tool)
        {
            InitializeComponent();
            mView =hWndUnit1.HWndCtrl;
            this.tool = tool;
            ROIController = new ROIController();

            ROIController.ROINotifyEvent += new EventHandler<ViewEventArgs>(UpdateViewData);

            locked=true;
            mView.useROIController(ROIController);

            ROIController.SetROISign(ROIOperation.Positive);

            Init();
            HideMax();
            HideMin();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void ShowResult()
        {
            mView.ClearList();
            mView.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            if (tool.ImageTestIn == null)
            {
                tool.ImageTestIn = tool.ImageRefIn;
            }
            if (tool.ImageTestIn == null)
            {
                return;
            }
            mView.AddIconicVar(tool.ImageTestIn);
            tool.ShowResult(mView);
            mView.Repaint();
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
                    tool.SearchRegion=null ;
                    if (searchRoi != null)
                    {
                        tool.SearchRegion = searchRoi.GetRegion();
                        tool.UpdateRegionAffine(false);
                    }
                    tool.ROIList = ROIController.ROIList;
                    ShowResult();
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

            cbxCodeTypeWant.Text = tool.WandCodeType.S;
            UpDownStartIndex.Value = tool.StartIndex;
            UpDownLength.Value = tool.Length;
            chkUseTextCompare.Checked = tool.UseTextCompare;
            chkUseZxing.Checked = tool.UseZxing;
            cmbFindMode.DataSource = System.Enum.GetNames(typeof(FindMode));
            cmbFindMode.SelectedIndex = this.cmbFindMode.FindString(tool.FindMode.ToString());

            mView.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            mat2DMangerUnit1.Init(tool.Mat2DManger);
            tool.Mat2DManger.SelectMatchingToolObserver = new SelectMatchingToolDelegate(On_SelectMatchingTool);
            UpdateModelList();

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

        public override void Clear()
        {
            base.Clear();
            ROIController.ROINotifyEvent -= new EventHandler<ViewEventArgs>(UpdateViewData);
            //如果已经训练了数据但是没有添加到字典且字典没有数据就默认添加到字典
            if (tool.DataCode2d!=null&& tool.DataCode2d.IsInitialized()&&tool.DataCode2DDic.Count==0)
            {
                tool.DataCode2DDic.Add("默认", tool.DataCode2d.Clone());
                tool.DataCode2d.Dispose();
                tool.DataCode2d = null;
            }
        }

        private void reduceRect1Button_Click(object sender, EventArgs e)
        {
            ROIController.SetROIShapeNoOperator(new ROIRectangle2("搜索框"));
        }

        private void TestModelButton_Click(object sender, EventArgs e)
        {
            txtResult.Text = "模板训练中...";
            txtResult.Refresh();
            tool.RunRef();

            string resultMessage = string.Format("查找结果\r\n编码格式: {0}\r\n找到文字: {1}\r\n结果字符串: {2}",
                tool.CodeType, tool.BarCodeMessage,tool.Result);

            ShowResult();
            if (tool.Result != null && tool.Result != "")
            {
                tool.Target = tool.Result;
            }
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

        private void cbxCodeTypeWant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.WandCodeType = cbxCodeTypeWant.Text;
        }

        private void btnDeleteSearchRegion_Click(object sender, EventArgs e)
        {
            ROIController.Reset();
        }

        private void chkUseZxing_CheckedChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.UseZxing = chkUseZxing.Checked;
        }

        private void cmbFindMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            tool.FindMode = (FindMode)Enum.Parse(typeof(FindMode), cmbFindMode.SelectedItem.ToString(), false);
        }

        private void UpdateModelList()
        {
            ModelListBox.Items.Clear();

            foreach (var item in tool.DataCode2DDic)
            {
                ModelListBox.Items.Add(item.Key);
            }
            if (ModelListBox.Items.Count>0)
            {
                ModelListBox.SelectedIndex = 0;
            }
        }
        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            int count;
            if ((count = ModelListBox.SelectedIndex) < 0)
                return;

            string key = (string)ModelListBox.SelectedItem;

            //将选择项下移
            if ((--count) < 0)
                count += 2;

            if ((count < ModelListBox.Items.Count))
            {
                ModelListBox.SelectedIndex = count;
            }

            tool.DelModel(key);
            ModelListBox.Items.Remove(key);
            ModelListBox.Focus();
        }

        private void btnDelAllModel_Click(object sender, EventArgs e)
        {
            if (ModelListBox.Items.Count > 0)
            {
                ModelListBox.Items.Clear();
                tool.DelAllModel();
                mView.ClearList();
                mView.Repaint();
            }
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            string key = txtModelKeyName.Text;
            if (key==null||key.Length<1)
            {
                MessageBox.Show("请输出模板标记" , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tool.DataCode2d==null||tool.DataCode2d.IsInitialized()==false)
            {
                MessageBox.Show("当前无训练数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tool.DataCode2DDic.ContainsKey(key))
            {
               DialogResult result=  MessageBox.Show("当前模板标记已经存在,是否替换?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result!= DialogResult.Yes)
                {
                    return;
                }
                tool.DataCode2DDic[key] = tool.DataCode2d.Clone();
                tool.DataCode2d.Dispose();
                tool.DataCode2d = null;

                MessageBox.Show($"模板:{key} 修改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                tool.DataCode2DDic.Add(key, tool.DataCode2d.Clone());
                tool.DataCode2d.Dispose();
                tool.DataCode2d = null;

                UpdateModelList();
                MessageBox.Show($"模板:{key} 添加完成完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            if (tool.ImageSoureTool==null||(tool.ImageSoureTool is CreateImage.CreateImageTool)==false)
            {
                MessageBox.Show("图像源请选择图像采集工具", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //采集图像
            tool.ImageSoureTool.Run();

            //链接图像
            tool.LinkTestImage();
            if (tool.ImageTestIn != null)
            {
                mView.AddIconicVar(tool.ImageTestIn);
                mView.Repaint();
            }
        }
    }
}
