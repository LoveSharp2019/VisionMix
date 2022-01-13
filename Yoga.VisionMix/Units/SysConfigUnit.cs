using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing.Drawing2D;
using Yoga.VisionMix.Frame;
using Yoga.Common.Basic;

namespace Yoga.VisionMix.Units
{
    public partial class SysConfigUnit : UserControl
    {
        FrmMain _pMainFrame;
        public SysConfigUnit(FrmMain mainframe)
        {
            _pMainFrame = mainframe;
            InitializeComponent();
        }

        private void UpdateOnce_Tick(object sender, EventArgs e)
        {

            UpdateOnce.Enabled = false;
        }


        /// <summary>
        /// 配置数据加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysConfigFrame_Load(object sender, EventArgs e)
        {
            //RefushSetting();
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

                CommunicationParam rs232Param = AppManger.ProjectData.Instance.CommunicationParam;
                drpComList.Text = rs232Param.ComName;
                drpBaudRate.Text = rs232Param.BaudRate;
                // int x= Convert.ToInt16(Tools.ToolsFactory.Rs232Param.Parity);
                drpParity.SelectedIndex = Convert.ToInt16(rs232Param.Parity);
                drpDataBits.Text = rs232Param.DataBits;
                drpStopBits.Text = rs232Param.StopBits;

                chkUsePlc.Checked = rs232Param.Use;
                groupBox6.Enabled = chkUsePlc.Checked;

                txtPlcData1.Text = rs232Param.PlcData[0].ToString();
                txtPlcData2.Text = rs232Param.PlcData[1].ToString();
                txtPlcData3.Text = rs232Param.PlcData[2].ToString();
                txtPlcData4.Text = rs232Param.PlcData[3].ToString();
                txtPlcData5.Text = rs232Param.PlcData[4].ToString();


                drpComList1.Text = UserSetting.Instance.IoRs232Param.ComName;
                drpBaudRate1.Text = UserSetting.Instance.IoRs232Param.BaudRate;
                drpParity1.SelectedIndex = Convert.ToInt16(UserSetting.Instance.IoRs232Param.Parity);
                drpDataBits1.Text = UserSetting.Instance.IoRs232Param.DataBits;
                drpStopBits1.Text = UserSetting.Instance.IoRs232Param.StopBits;

                chkUseIO.Checked = UserSetting.Instance.IoRs232Param.Use;
                groupBox3.Enabled = chkUseIO.Checked;
            }
            catch
            {

            }
            #endregion
        }

        private void Btn_SaveCOM_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定保存参数？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }
            ////串口通讯
            try
            {
                CommunicationParam rs232Param = AppManger.ProjectData.Instance.CommunicationParam;
                rs232Param.Use = chkUsePlc.Checked;
                if (chkUsePlc.Checked)
                {

                    rs232Param.ComName = drpComList.Text;
                    rs232Param.BaudRate = drpBaudRate.Text;

                    rs232Param.Parity = drpParity.SelectedIndex.ToString();
                    rs232Param.DataBits = drpDataBits.Text;
                    rs232Param.StopBits = drpStopBits.Text;
                }

                AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);

                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show("保存失败 :"+ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                UpdateOnce.Enabled = true;
        }

        private void btnSavePLCData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定保存参数？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }
            ////串口通讯
            try
            {
                CommunicationParam rs232Param = AppManger.ProjectData.Instance.CommunicationParam;
                rs232Param.PlcData[0] =int.Parse( txtPlcData1.Text);
                rs232Param.PlcData[1] = int.Parse(txtPlcData2.Text);
                rs232Param.PlcData[2] = int.Parse(txtPlcData3.Text);
                rs232Param.PlcData[3] = int.Parse(txtPlcData4.Text);
                rs232Param.PlcData[4] = int.Parse(txtPlcData5.Text);

                AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);
                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PLC.PanasonicSerial.Instance.WriteDT("D2400", "D2409", rs232Param.PlcData);
                MessageBox.Show("数据写入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常 :" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkUsePlc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsePlc.Checked)
            {
                groupBox6.Enabled = true;
            }
            else
            {
                groupBox6.Enabled = false;
            }
        }

        private void Btn_SaveCOM1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定保存参数？", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }
            ////串口通讯
            try
            {
                UserSetting.Instance.IoRs232Param.Use = chkUseIO.Checked;
                if (chkUseIO.Checked)
                {
                    UserSetting.Instance.IoRs232Param.ComName = drpComList1.Text;
                    UserSetting.Instance.IoRs232Param.BaudRate = drpBaudRate1.Text;

                    UserSetting.Instance.IoRs232Param.Parity = drpParity1.SelectedIndex.ToString();
                    UserSetting.Instance.IoRs232Param.DataBits = drpDataBits1.Text;
                    UserSetting.Instance.IoRs232Param.StopBits = drpStopBits1.Text;
                }
                UserSetting.Instance.SaveSetting();
                Camera.IOSerial.Instance.Rs232Param = UserSetting.Instance.IoRs232Param;

                MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败 :" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkUseIO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseIO.Checked)
            {
                groupBox3.Enabled = true;
            }
            else
            {
                groupBox3.Enabled = false;
            }
        }
    }
}
