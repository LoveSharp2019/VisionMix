using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.ImageControl;
using Yoga.Tools;

namespace Yoga.VisionMix.Units
{
   public struct CameraShow
    {
        public HWndUnit HWndUnit;
        //public DataGridView DgvTool;
        public int SettingIndex;
        public void Init(int cameraIndex)
        {
            SettingIndex = cameraIndex;
            HWndUnit = new HWndUnit();
            HWndUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            HWndUnit.Name = cameraIndex.ToString();
            HWndUnit.CameraMessage = string.Format("相机{0}",cameraIndex);

            HWndUnit.HWndCtrl.SetBackgroundColor("#262626");
            //DgvTool = new DataGridView();
            //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            //this.DgvTool.AllowUserToAddRows = false;
            //this.DgvTool.AllowUserToResizeColumns = false;
            //this.DgvTool.AllowUserToResizeRows = false;
            //this.DgvTool.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            //dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            //dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            //dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            //dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            //this.DgvTool.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            //this.DgvTool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.DgvTool.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.DgvTool.Location = new System.Drawing.Point(3, 147);
            //this.DgvTool.MultiSelect = false;
            //this.DgvTool.Name = settingIndex.ToString();
            //this.DgvTool.RowHeadersVisible = false;
            //this.DgvTool.RowTemplate.Height = 40;
            //this.DgvTool.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            //this.DgvTool.Size = new System.Drawing.Size(468, 262);
            //this.DgvTool.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTools_CellClick);
            //this.DgvTool.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTools_CellMouseDoubleClick);
            //this.DgvTool.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvTools_DataError);
        }
        public void InitDgvShow()
        {
            //DgvTool.Refresh();
            ////DgvTool.Columns["ToolName"].HeaderCell.Value = "名称";
            ////DgvTool.Columns["Result"].HeaderCell.Value = "结果值";
            ////DgvTool.Columns["Enable"].HeaderCell.Value = "有效";
            ////DgvTool.Columns["TargetText"].HeaderCell.Value = "标准";
            ////DgvTool.Columns["Min"].HeaderCell.Value = "最小";
            ////DgvTool.Columns["Max"].HeaderCell.Value = "最大";
            ////DgvTool.Columns["IsOk"].HeaderCell.Value = "结果";

            ////DgvTool.Columns["IsOk"].ToolTipText = "运行结果,此列不能修改";
            //int count = DgvTool.Columns.Count;
            //int width = DgvTool.Width;
            //int width0 = (int)(width / (count * 1.3));

            ////DgvTool.Columns["Enable"].Width = width0;
            ////DgvTool.Columns["Min"].Width = width0;
            ////DgvTool.Columns["Max"].Width = width0;

            //int width1 = (width - width0 * 3) / 4;
            ////DgvTool.Columns["ToolName"].Width = width1;
            ////DgvTool.Columns["Result"].Width = width1;
            ////DgvTool.Columns["TargetText"].Width = width1;
            ////DgvTool.Columns["IsOk"].Width = width1;
            //DgvTool.Refresh();
        }
        private void dgvTools_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("表格数据异常,原因: " + e.Exception.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvTools_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (DgvTool.CurrentRow == null)
            //{
            //    return;
            //}
            //int rowIndex = DgvTool.CurrentRow.Index;
            //int colIndex = DgvTool.CurrentCell.ColumnIndex;
            //if (colIndex != 0)
            //{
            //    return;
            //}
            //ToolBase tool = ToolsFactory.ToolsDic[SettingIndex][rowIndex];
            //if (tool != null)
            //{
            //    tool.ClearTestData();
            //    ToolsSettingUnit settingUnit = tool.GetSettingUnit() as ToolsSettingUnit;
            //    if (settingUnit!=null)
            //    {
            //        frmToolSetting toolSetting = new frmToolSetting(settingUnit);

            //        toolSetting.ShowDialog();
            //    }
               
            //}
        }

        private void dgvTools_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DgvTool.Refresh();
        }
    }
}
