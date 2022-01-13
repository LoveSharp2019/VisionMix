using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.VisionMix.AppManger;

namespace Yoga.VisionMix.Frame
{
    public partial class frmCommSetting : Form
    {

        public frmCommSetting()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("参数修改完成？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }
            ////串口通讯
            try
            {
                CommunicationParam communicationParam = AppManger.ProjectData.Instance.CommunicationParam;

                communicationParam.ComName = drpComList.Text;
                communicationParam.BaudRate = drpBaudRate.Text;

                communicationParam.Parity = drpParity.SelectedIndex.ToString();
                communicationParam.DataBits = drpDataBits.Text;
                communicationParam.StopBits = drpStopBits.Text;

                communicationParam.TcpIP = ipTextBoxExt1.Text;
                communicationParam.TcpPort = nmServerPort.Value.ToString();
                communicationParam.InterlockMode = (InterlockMode)Enum.Parse(typeof(InterlockMode), cmbCommType.SelectedItem.ToString(), false);
                communicationParam.IsExtTrigger = this.chkIsExtTrigger.Checked;
                AppInterlockHelper.Instance.CommunicationParam = communicationParam;

                //SerialHelper.Instance.Rs232Param = AppManger.ProjectData.Instance.Rs232Param;
                try
                {
                    AppInterlockHelper.Instance.Open();
                }
                catch(Exception /*ex*/)
                {

                }
                if (AppInterlockHelper.Instance.IsLink)
                {
                    AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);

                    MessageBox.Show("通信已打开,参数保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("通信打开失败,请检查设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败 :" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefushSetting()
        {
            #region 设置数据加载
            //串口数据初始化
            //  drpComList.Items.AddRange(SerialPort.GetPortNames());
            if (drpComList.Items.Count > 0)
            {
                drpComList.SelectedIndex = 0;
            }
            //数据设置默认值
            if (drpComList.Items.Count > 0)
            {
                drpComList.SelectedIndex = 0;
            }
            //数据加载设置数据
            try//第一次加载时候无setting数据
            {

                CommunicationParam communicationParam = AppManger.ProjectData.Instance.CommunicationParam;
                drpComList.Text = communicationParam.ComName;
                drpBaudRate.Text = communicationParam.BaudRate;
                // int x= Convert.ToInt16(Tools.ToolsFactory.Rs232Param.Parity);
                drpParity.SelectedIndex = Convert.ToInt16(communicationParam.Parity);
                drpDataBits.Text = communicationParam.DataBits;
                drpStopBits.Text = communicationParam.StopBits;

                ipTextBoxExt1.Text = communicationParam.TcpIP;
                nmServerPort.Value = ushort.Parse(communicationParam.TcpPort);

                chkIsExtTrigger.Checked = communicationParam.IsExtTrigger;

                cmbCommType.DataSource = System.Enum.GetNames(typeof(InterlockMode));
                cmbCommType.SelectedIndex = this.cmbCommType.FindString(communicationParam.InterlockMode.ToString());

            }
            catch
            {

            }
            #endregion
        }
        private void frmCommSetting_Load(object sender, EventArgs e)
        {
            RefushSetting();
        }

    }
}
