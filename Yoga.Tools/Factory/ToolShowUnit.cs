using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Tools.Factory
{
    public partial class ToolShowUnit : UserControl
    {
        Dictionary<int, TreeNode> ToolTreeNodeDic = new Dictionary<int, TreeNode>();
        Dictionary<int, int> ToolsCameraIndexDic = new Dictionary<int, int>();

        int currentSettingKey;
        public ToolShowUnit()
        {
            InitializeComponent();
        }

        public int GetCameraIndex(int settingKey)
        {
            if (ToolsCameraIndexDic.ContainsKey(settingKey))
            {
                return ToolsCameraIndexDic[settingKey];
            }
            List<ToolBase> toolList = ToolsFactory.GetToolList(settingKey);
            CreateImage.CreateImageTool tool = toolList[0] as CreateImage.CreateImageTool;
            int cameraIndex = tool.CameraIndex;
            ToolsCameraIndexDic.Add(settingKey, cameraIndex);
            return cameraIndex;
        }
        public int CurrentCameraIndex
        {
            get
            {
                return GetCameraIndex(CurrentSettingKey);
            }
        }
        public int CurrentSettingKey
        {
            get
            {
                if (currentSettingKey < 1)
                {
                    currentSettingKey = 1;
                }
                return currentSettingKey;
            }
            private set
            {
                currentSettingKey = value;
            }
        }
        private void 添加工具组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int settingKey = ToolsFactory.ToolsDic.Keys.Max() + 1;
         

            int cameraWant = settingKey;
            if (cameraWant>Camera.CameraManger.CameraDic.Count)
            {
                cameraWant = Camera.CameraManger.CameraDic.Count;
            }
            frmSelectCameraIndex frmSelect = new frmSelectCameraIndex(cameraWant);
            frmSelect.ShowDialog();
            if (frmSelect.DialogResult!= DialogResult.OK)
            {
                return;
            }
            List<ToolBase> toolsNew = ToolsFactory.GetToolList(settingKey);
            CreateImage.CreateImageTool toolCamrea = toolsNew[0] as CreateImage.CreateImageTool;
            toolCamrea.CameraIndex = frmSelect.CameraIndex;
            TreeNode nodeSetting = AddSettingNode(settingKey);
            InitToolsByIndex(nodeSetting, settingKey);
            nodeSetting.ExpandAll();
        }

        private TreeNode AddSettingNode(int settingKey)
        {
            TreeNode nodeSetting = new TreeNode();
            nodeSetting.Name = settingKey.ToString();
            nodeSetting.Text = string.Format("工具组{0}", settingKey);
            nodeSetting.SelectedImageIndex = 0;
            treeViewAllTools.Nodes.Add(nodeSetting);

            if (ToolTreeNodeDic.ContainsKey(settingKey))
            {
                ToolTreeNodeDic[settingKey] = nodeSetting;
            }
            else
            {
                ToolTreeNodeDic.Add(settingKey, nodeSetting);
            }
            return nodeSetting;
        }
        public void LoginSetting(bool isLogin)
        {
            //this.groupBoxUserSetting.Enabled = isLogin;
            this.groupBox1.Enabled = isLogin;
            //this.treeView1.Enabled = isLogin;
            //this.btnRun.Enabled = isLogin;
            //this.btnInterlocking.Enabled = isLogin;
            //this.toolShowUnit1.Enabled = isLogin;
        }
        public void InitTreeView()
        {
            int count = ToolsFactory.ToolsDic.Count;
            treeViewAllTools.Nodes.Clear();
            foreach (int settingKey in ToolsFactory.ToolsDic.Keys)
            {
                TreeNode nodeSetting = AddSettingNode(settingKey);
                InitToolsByIndex(nodeSetting, settingKey);
            }
            CurrentSettingKey = 1;
            treeViewAllTools.ExpandAll();
            if (treeViewAllTools.Nodes.Count > 0)
            {
                treeViewAllTools.SelectedNode = treeViewAllTools.Nodes[0];
            }
        }

        private void 添加工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //首先判断是否选定组件中节点的位置
            if (treeViewAllTools.SelectedNode == null)
            {
                MessageBox.Show("请先选择工具节点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TreeNode nodeSetting = null;
            if (treeViewAllTools.SelectedNode.Parent == null)
            {
                nodeSetting = treeViewAllTools.SelectedNode;
            }
            else
            {
                nodeSetting = treeViewAllTools.SelectedNode.Parent;
            }
            int settingKey = int.Parse(nodeSetting.Name);
            frmToolSelect select = new frmToolSelect(settingKey);
            DialogResult result = select.ShowDialog();
            if (result == DialogResult.OK)
            {
                InitToolsByIndex(nodeSetting, settingKey);
                nodeSetting.ExpandAll();
            }
        }

        private void InitToolsByIndex(TreeNode nodeSetting, int settingKey)
        {
            nodeSetting.Nodes.Clear();
            int index = 0;
            foreach (ToolBase item in ToolsFactory.GetToolList(settingKey))
            {
                TreeNode nodeTool = new TreeNode();
                nodeTool.Name = index.ToString();
                nodeTool.Text = string.Format("{0} {1}", item.Name, item.Note);
                nodeTool.SelectedImageIndex = 1;
                nodeSetting.Nodes.Add(nodeTool);

                index++;
            }
            InitStatus(settingKey);
        }

        public void InitStatus(int settingKey)
        {
            if (ToolTreeNodeDic.ContainsKey(settingKey) == false)
            {
                Common.Util.Notify(string.Format("工具组{0}显示未初始化!!", settingKey));
                return;
            }
            foreach (TreeNode item in ToolTreeNodeDic[settingKey].Nodes)
            {
                item.ImageIndex = 2;

            }
        }
        public void ShowStatus(RunStatus runStatus)
        {
            textBox2.Text = runStatus.ResultMessage;

            int index = 0;
            treeViewAllTools.BeginUpdate();
            treeViewAllTools.SelectedNode = null;

            foreach (TreeNode item in ToolTreeNodeDic[runStatus.SettingIndex].Nodes)
            {
                int indexImage = 0;
                if (runStatus.RunStatusList[index] ==true)
                {
                    indexImage = 2;
                }
                else
                {
                    indexImage = 3;
                }
                item.ImageIndex = indexImage;
                index++;
            }
            treeViewAllTools.EndUpdate();
            textBox2.Refresh();
            treeViewAllTools.Refresh();

        }
        public void ShowStatus(int settingKey)
        {
            if (ToolTreeNodeDic.ContainsKey(settingKey) == false)
            {
                Common.Util.Notify(string.Format("工具组{0}显示未初始化!!", settingKey));
                return;
            }
            int index = 0;
            treeViewAllTools.SelectedNode = null;
            List<ToolBase> tools = ToolsFactory.GetToolList(settingKey);
            foreach (TreeNode item in ToolTreeNodeDic[settingKey].Nodes)
            {
                int indexImage = 0;
                if (tools[index].IsOk||(tools[index] is IToolRun ==false))
                {
                    indexImage = 2;
                }
                else
                {
                    indexImage = 3;
                }
                item.ImageIndex = indexImage;
                index++;
            }
        }

        private void GetCurrentSettingkey(TreeNode nodeSelect)
        {
            TreeNode nodeSetting = null;
            if (nodeSelect.Parent == null)
            {
                nodeSetting = nodeSelect;
            }
            else
            {
                nodeSetting = nodeSelect.Parent;
            }
            CurrentSettingKey = int.Parse(nodeSetting.Name);
        }

        private void 删除工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewAllTools.SelectedNode == null || treeViewAllTools.SelectedNode.Parent == null)
            {
                MessageBox.Show("请先选择工具节点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            TreeNode nodeSetting = treeViewAllTools.SelectedNode.Parent;
            TreeNode nodeTool = treeViewAllTools.SelectedNode;
            int settingKey = int.Parse(nodeSetting.Name);

            int index = int.Parse(nodeTool.Name);
            if (index == 0)
            {
                MessageBox.Show("工具1为图像采集工具,无法删除", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ToolBase tool = ToolsFactory.GetToolList(settingKey)[index];
            string message = "";
            if (tool is IToolRun)
            {
                message = string.Format("是否删除工具{0}?", tool.Name);
            }
            else
            {
                message = string.Format("{0}为设置类工具,若删除需要保存后重新打开!",tool.Name);
            }
            DialogResult result = MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result != DialogResult.Yes)
            {
                return;
            }



            ToolsFactory.DeleteTool(settingKey, index);
            InitToolsByIndex(nodeSetting, settingKey);
        }

        private void 删除工具组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewAllTools.SelectedNode == null)
            {
                MessageBox.Show("请先选择工具节点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNode nodeSetting = null;
            if (treeViewAllTools.SelectedNode.Parent == null)
            {
                nodeSetting = treeViewAllTools.SelectedNode;
            }
            else
            {
                nodeSetting = treeViewAllTools.SelectedNode.Parent;
            }
            int settingKey = int.Parse(nodeSetting.Name);

            if (settingKey == 1)
            {
                MessageBox.Show("工具组1为默认工具组无法删除", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult result = MessageBox.Show("是否删除当前工具组合? ", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result != DialogResult.Yes)
            {
                return;
            }
            ToolsFactory.DeleteToolDic(settingKey);
            treeViewAllTools.Nodes.Remove(nodeSetting);
            if (ToolsCameraIndexDic.ContainsKey(settingKey))
            {
                ToolsCameraIndexDic.Remove(settingKey);
            }         
        }

        private void treeViewAllTools_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                textBox2.Text = "";

                GetCurrentSettingkey(e.Node);

                TreeNode nodeTool = e.Node;

                if (nodeTool.Parent == null)
                {
                    textBox2.Text = string.Format("当前工具组:{0}", nodeTool.Text);
                    return;
                }

                TreeNode nodeSetting = nodeTool.Parent;

                int settingKey = int.Parse(nodeSetting.Name);

                int index = int.Parse(nodeTool.Name);

                ToolBase tool = ToolsFactory.GetToolList(settingKey)[index];
                treeViewAllTools.PathSeparator = "/";
                string str1 = "";

                if (tool != null)
                {

                    str1 += "当前工具:" + nodeTool.FullPath;
                    str1 += Environment.NewLine + "运行结果:" + (tool.Result == null || tool.Result == "" ? "空" : tool.Result);
                    str1 += Environment.NewLine + "运行状态:" + ((tool.IsOk == true||(tool is IToolRun==false)) ? "OK" : "NG");
                    str1 += Environment.NewLine + "耗时(ms):" + tool.ExecutionTime.ToString("f2");
                }
                textBox2.Text = str1;

            }
        }

        private void treeViewAllTools_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectNode = e.Node;
            if (selectNode == null || selectNode.Parent == null)
            {
                //MessageBox.Show("请先选择工具节点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            TreeNode nodeSetting = selectNode.Parent;
            TreeNode nodeTool = selectNode;
            int settingKey = int.Parse(nodeSetting.Name);

            int index = int.Parse(nodeTool.Name);

            ToolBase tool = ToolsFactory.GetToolList(settingKey)[index];
            if (tool != null)
            {
                tool.ClearTestData();
                ToolsSettingUnit settingUnit = tool.GetSettingUnit() as ToolsSettingUnit;
                if (settingUnit != null)
                {
                    frmToolSetting toolSetting = new frmToolSetting(settingUnit);

                    toolSetting.ShowDialog();
                    nodeTool.Text = string.Format("{0} {1}", tool.Name, tool.Note);
                }

            }
        }

        private void treeViewAllTools_MouseEnter(object sender, EventArgs e)
        {
            treeViewAllTools.Focus();
        }
    }
}
