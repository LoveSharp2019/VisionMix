using System;
using System.Drawing;
using System.Windows.Forms;
using HalconDotNet;

using System.Threading.Tasks;
using Yoga.VisionMix.Frame;
using Yoga.Tools;
using Yoga.Common;
using System.Linq;
using System.Data;
using System.Collections.Generic;

using System.Threading;
using Yoga.Camera;
using Yoga.ImageControl;
using Yoga.VisionMix.Scheduling;
using Yoga.VisionMix.AppManger;
using Yoga.Common.Helpers;
using Yoga.Common.Basic;
using Yoga.Tools.Factory;

namespace Yoga.VisionMix.Units
{
    public partial class AutoUnit : UserControl
    {
        #region 字段
        FrmMain mainframe;

        bool isTestMode = false;
        public Dictionary<int, CameraShow> CameraShowUnitDic = new Dictionary<int, CameraShow>();

        private CameraBase cameraSelect;

        private int currentSettingIndex = 1;
        //创建一个空表
        DataTable dtResult = new DataTable();
        DataTable dtResultShow = new DataTable();
        UpdateUI updateUI;

        MessageQueue messageQueue;
        int cameraCount;
        /// <summary>
        /// 相机图像处理流程,其以相机编号为key 1开头
        /// </summary>
        public Dictionary<int, RunTheadData> runTheadDataDic = new Dictionary<int, RunTheadData>();


        TableLayoutPanel tabPanel;
        Wrapper.UtilManaged utilManagedCli;

        public DataTable DtResultShow
        {
            get
            {
                return dtResultShow;
            }

            private set
            {
                dtResultShow = value;
            }
        }

        //static CallbackFun HandleEventX;
        #endregion
        void ShowCPPMessage(string message)
        {
            SetNotifyMessage(message);
        }

        public enum enTest
        {
            [EnumDescription("max")]
            最大,
            [EnumDescription("min")]
            最小
        }
        public AutoUnit(FrmMain mainframe)
        {
            InitializeComponent();
            this.mainframe = mainframe;

            updateUI = new UpdateUI(this);
            messageQueue = new MessageQueue(this.txtLog);
            enTest tt = enTest.最大;
            string[] ll = Enum.GetNames(enTest.最大.GetType());
            string name = EnumHelper.GetDescription(tt);
            enTest en1 = (enTest)Enum.Parse(typeof(enTest), "最大");

            enTest[] t1t = Common.Basic.SoftBasic.GetEnumValues<enTest>();
            //System.Environment.FailFast("引发异常-------");
            //HandleEventX = ShowCPPMessage;
            //RegisterMessageCallBack(HandleEventX);
            utilManagedCli = new Wrapper.UtilManaged();
            utilManagedCli.NetCallback = ShowCPPMessage;
            Util.MessageEvent += Util_MessageEvent;
            CameraManger.CameraInitFinishEvent += CameraManger_CameraInitFinishEvent;
            cameraCount = IniStatus.Instance.CamearCount;

            AppInterlockHelper.Instance.PortDataReciveEvent += App_portDataReciveEvent;
            if (cameraCount > 4)
            {
                tabPanel = tableLayoutPanelMax8;
            }
            else
            {
                tabPanel = tableLayoutPanelHWnd;
            }
            if (cameraCount == 1)
            {
                tscobCameraSelect.Visible = false;
                tsbShowAll.Visible = false;
                //tabPanel = nu;
                //groupBoxCameraSelect.Visible = false;
            }
        }

        private void Util_MessageEvent(object sender, MessageEventArgs e)
        {
            SetNotifyMessage(e.Message);

            if (e.level == Level.Err)
            {
                this.BeginInvoke(new Action(() =>
                {
                    mainframe.ShowErr(e.Message);
                }));
            }
        }

        private void App_portDataReciveEvent(object sender, Common.Helpers.PortDataReciveEventArgs e)
        {
            if (isTestMode)
            {
                Util.Notify(Level.Err, "---收到触发命令,测试模式下不响应");
                return;
            }
            string code = e.Data.ToLower();
            int settingIndexRec = 0;
            //System.Diagnostics.Debug.WriteLine("网络线程:" + Thread.CurrentThread.ManagedThreadId);
            if (code == "start")
            {
                Util.Notify("---收到触发命令,工具集1开始运行");
                int cameraKey = toolShowUnit1.GetCameraIndex(1);
                runTheadDataDic[cameraKey].TrigerRun(1, false);
            }
            else if (int.TryParse(code, out settingIndexRec) && ToolsFactory.ToolsDic.ContainsKey(settingIndexRec))
            {
                Util.Notify($"---收到触发命令,工具集{settingIndexRec}开始运行");
                int cameraKey = toolShowUnit1.GetCameraIndex(settingIndexRec);
                runTheadDataDic[cameraKey].TrigerRun(settingIndexRec, false);
            }
            else
            {
                Util.Notify(string.Format("收到异常命令{0}", e.Data));
            }
        }

        private void InitRunData()
        {
            tabPanel.BringToFront();
            tabPanel.Dock = DockStyle.Fill;
            CameraShowUnitDic.Clear();
            //必须手动释放显示控件以清除native的图像集合数据
            foreach (var item in this.tabPanel.Controls)
            {
                UserControl control = item as UserControl;

                if (control != null)
                {
                    control.Dispose();
                }
            }
            foreach (var item in this.panelHWndMax.Controls)
            {
                UserControl control = item as UserControl;

                if (control != null)
                {
                    control.Dispose();
                }
            }

            this.tabPanel.Controls.Clear();
            this.panelHWndMax.Controls.Clear();
            tscobCameraSelect.Items.Clear();

            if (runTheadDataDic.Count > 0)
            {
                foreach (var item in runTheadDataDic.Values)
                {
                    item.Stop();
                }
            }
            runTheadDataDic.Clear();
            //runThreadDic.Clear();
            InitResultTablte();
            for (int i = 0; i < cameraCount; i++)
            {
                tscobCameraSelect.Items.Add(string.Format("相机{0}", i + 1));

                CameraShow showUnit = new CameraShow();
                showUnit.Init(i + 1);

                CameraShowUnitDic.Add(i + 1, showUnit);
                runTheadDataDic.Add(i + 1, new RunTheadData(this, i + 1));
                //runThreadDic.Add(i + 1, new Thread(new ThreadStart(runTheadDataDic[i + 1].Run)));
                //runThreadDic[i + 1].Start();
                if (cameraCount < 7)
                {
                    tabPanel.RowCount = 2;
                }
                if (cameraCount < 5)//小于4个相机的处理
                {
                    switch (i + 1)
                    {
                        case 1:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 0, 0);
                            if (cameraCount == 2)
                            {
                                tabPanel.SetRowSpan(showUnit.HWndUnit, 2);
                            }
                            break;
                        case 2:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 1, 0);
                            if (cameraCount == 2)
                            {
                                tabPanel.SetRowSpan(showUnit.HWndUnit, 2);
                            }
                            break;
                        case 3:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 0, 1);
                            break;
                        case 4:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 1, 1);
                            break;
                        default:
                            break;
                    }
                }
                else//大于4个相机的处理
                {
                    switch (i + 1)
                    {
                        case 1:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 0, 0);
                            break;
                        case 2:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 1, 0);
                            break;
                        case 3:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 2, 0);
                            break;
                        case 4:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 0, 1);
                            break;
                        case 5:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 1, 1);
                            break;
                        case 6:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 2, 1);
                            break;
                        case 7:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 0, 2);
                            break;
                        case 8:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 1, 2);
                            break;
                        case 9:
                            this.tabPanel.Controls.Add(showUnit.HWndUnit, 2, 2);
                            break;
                        default:
                            break;
                    }
                }
            }
            tscobCameraSelect.SelectedIndex = 0;
            //panelDgvTools.Controls.Add(CameraShowUnitDic[1].DgvTool);
            //CameraShowUnitDic[1].InitDgvShow();
        }

        private delegate void CameraInitFinishDelegate(object sender, EventArgs e);
        private void CameraManger_CameraInitFinishEvent(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CameraInitFinishDelegate(CameraManger_CameraInitFinishEvent), new object[] { sender, e });
                return;
            }
            foreach (var item in CameraManger.CameraDic)
            {
                item.Value.ImageEvent += Camera_ImageEvent;
            }
            if (CameraManger.CameraDic.ContainsKey(1))
            {
                cameraSelect = CameraManger.CameraDic[1];
            }
            tabPanel.Dock = DockStyle.Fill;
        }
        private void Camera_ImageEvent(object sender, ImageEventArgs e)
        {
            if (e.CameraIndex > cameraCount)
            {
                SetNotifyMessage(string.Format("相机{0}收到图像,与相机总数不符!!", e.CameraIndex));
                return;
            }

            if (e.Command == Command.ExtTrigger)
            {
                //运行模式
                runTheadDataDic[e.CameraIndex].TrigerRun(e, 1000, true);
            }
            else
            {
                //#if DEBUG
                //                Util.Notify(string.Format("显示图像线程ID:{0}", Thread.CurrentThread.ManagedThreadId)); 
                //#endif
                //测试模式
                if (isTestMode)
                {
                    runTheadDataDic[e.CameraIndex].TrigerRun(e, 1, false);
                }
                else
                {
                    if (runTheadDataDic[e.CameraIndex].ImageRunFinishSignal.WaitOne(1))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            //显示图像
                            HWndCtrl hWndCtrl = CameraShowUnitDic[e.CameraIndex].HWndUnit.HWndCtrl;
                            HTuple t0;
                            HOperatorSet.CountSeconds(out t0);
                            hWndCtrl.ClearList();
                            hWndCtrl.AddIconicVar(e.CameraImage);
                            hWndCtrl.Repaint();
                            //Application.DoEvents();//不要使用liscence,否则不对就会造成卡顿严重//高帧率下显示会卡顿
                            e.Dispose();
                            string cameraInfo = CameraManger.CameraDic[e.CameraIndex].GetCameraAcqInfo();
                            CameraShowUnitDic[e.CameraIndex].HWndUnit.CameraMessage = string.Format(
                               "相机{0} {1}", e.CameraIndex, cameraInfo);
                            runTheadDataDic[e.CameraIndex].ImageRunFinishSignal.Set();
                        }));
                    }
                    else
                    {
                        e.Dispose();
                        //if (this.InvokeRequired)
                        //{
                        //    Thread.Sleep(10);
                        //}
                    }
                }
            }

        }
        #region 通知消息显示


        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //public delegate void CallbackFun(string i);

        ////声明DLL中的回调函数,即事件
        //[DllImport("Native.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void RegisterMessageCallBack([MarshalAs(UnmanagedType.FunctionPtr)] CallbackFun pCallbackFun);


        private delegate void SetNotifyMessageDelegate(string message);
        /// <summary>显示来自通信处理的消息</summary>
        /// <param name="message">要显示的消息</param>
        private void SetNotifyMessage(string message)
        {

            messageQueue.ShowMessage(message);
            //if (this.InvokeRequired)
            //{
            //    this.BeginInvoke(new SetNotifyMessageDelegate(SetNotifyMessage), new object[] { message });
            //    return;
            //}
            //DataGridViewRowCollection rows = dataGridViewMessage.Rows;

            //// 1行追加
            //rows.Add();

            //// 最終行
            //DataGridViewCellCollection cells = rows[rows.Count - 1].Cells;

            //cells["columnDateTime"].Value = DateTime.Now.ToString("HH:mm:ss");
            //cells["columnMessage"].Value = message;

            //// 指定件数を超過したら先頭行を削除
            //if (rows.Count > 1000)
            //{
            //    rows.RemoveAt(0);
            //}
            //dataGridViewMessage.CurrentCell = this.dataGridViewMessage.Rows[rows.Count - 1].Cells[0];
        }
        #endregion

        public void LoginSetting(bool isLogin)
        {
            //this.groupBoxUserSetting.Enabled = isLogin;
            this.btnDeleteResult.Enabled = isLogin;
            this.btnRun.Enabled = isLogin;
            this.btnInterlocking.Enabled = isLogin;
            this.toolShowUnit1.LoginSetting(isLogin);
            toolStrip1.Enabled = isLogin;
        }
        public void LoadProject(bool loadTool, Common.UI.FrmLoad frmLoad = null)
        {

            InitRunData();

            ChangeSize(1);

            try
            {
                if (loadTool)
                {
                    AppManger.ProjectData.Instance.ReadProject(UserSetting.Instance.ProjectPath);
                }
            }
            catch (Exception ex)
            {
                Common.Util.WriteLog(this.GetType(), ex);
                StatusManger.Instance.RuningStatus = RuningStatus.系统异常;
                Common.Util.Notify(Level.Err, "项目加载异常");
            }
            frmLoad?.UpdateProgress("显示数据初始化...", 50, 70, 500);
            InitResultTablte();
            try
            {
                toolShowUnit1.InitTreeView();
            }
            catch (Exception /*ex*/)
            {
                ;
            }
            frmLoad?.UpdateProgress("相机参数设置...", 70, 80, 500);
            foreach (var item in CameraManger.CameraDic)
            {
                CameraPram cameraPram = AppManger.ProjectData.Instance.GetCameraPram(item.Key);

                item.Value.ShuterCur = cameraPram.Shutter;
                item.Value.GainCur = cameraPram.Gain;
                item.Value.TriggerDelayAbs = cameraPram.TriggerDelayAbs;
                item.Value.LineDebouncerTimeAbs = cameraPram.LineDebouncerTimeAbs;
                item.Value.OutLineTime = cameraPram.OutLineTime;
                item.Value.ImageAngle = cameraPram.ImageAngle;
                Thread.Sleep(1);
            }

            if (CameraManger.CameraDic.Count > 0)
            {
                Common.Util.Notify("相机参数设置完成");
            }
            frmLoad?.UpdateProgress("外部通信初始化...", 80, 100, 500);
            try
            {
                AppInterlockHelper.Instance.CommunicationParam = AppManger.ProjectData.Instance.CommunicationParam;
                AppInterlockHelper.Instance.Open();
                RefreshBtnRunVisible();
                StatusManger.Instance.RuningStatus = RuningStatus.等待运行;

            }
            catch (Exception ex)
            {
                Common.Util.WriteLog(this.GetType(), ex);
                StatusManger.Instance.RuningStatus = RuningStatus.系统异常;
                MessageHelper.ShowError("外部通信异常,通信打开失败");
            }
            StatusManger.Instance.TestInitFinish = true;
            UserSetting.Instance.SaveSetting();


        }

        public void RefreshBtnRunVisible()
        {
            bool flag = AppInterlockHelper.Instance.CommunicationParam.IsExtTrigger;
            btnRun.Visible = flag;
            brnTestNgSigin.Visible = flag;
            if (flag == true)
            {
                StartExtTrigger();
            }
            mainframe.ShowCommStatus();
        }
        private void InitResultTablte()
        {
            dtResult.Columns.Clear();
            dtResult.Columns.Add("相机", typeof(string));
            dtResult.Columns.Add("时间", typeof(string));

            dtResult.Columns.Add("正常计数", typeof(string));
            dtResult.Columns.Add("失败计数", typeof(string));
            dtResult.Columns.Add("异常率", typeof(string));
            dtResult.Columns.Add("节拍时间", typeof(string));

            DtResultShow = dtResult.Clone();
            DtResultShow.Rows.Clear();
            for (int i = 0; i < cameraCount; i++)
            {
                DataRow dr = DtResultShow.NewRow();
                dr["正常计数"] = 0;
                dr["失败计数"] = 0;
                dr["相机"] = "相机" + (i + 1);
                DtResultShow.Rows.Add(dr);
            }
            dgvResultShow.DataSource = DtResultShow;
        }
        public void RefreshUI(RunStatus runStatus)
        {
            //ShowStatus( runStatus);
            updateUI.Update(runStatus);
        }
        private void SaveResult(int index, string path)
        {

            HTuple start;

            HOperatorSet.CountSeconds(out start);

            dtResult.Rows.Clear();

            try
            {
                DataRow dr = dtResult.NewRow();
                dr["时间"] = DtResultShow.Rows[index]["时间"]; //通过名称赋值
                dr["相机"] = DtResultShow.Rows[index]["相机"];
                dr["正常计数"] = DtResultShow.Rows[index]["正常计数"];
                dr["失败计数"] = DtResultShow.Rows[index]["失败计数"];
                dr["异常率"] = DtResultShow.Rows[index]["异常率"];
                dr["节拍时间"] = DtResultShow.Rows[index]["节拍时间"];

                dtResult.Rows.Add(dr);
                Common.FileAct.CsvFiles.AppendWriteCSV(dtResult, path);

                //showTime(index, start, "数据保存完成");
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                SetNotifyMessage(string.Format("运行结果保存出现异常"));
            }
        }

        private void ChangeSize(int index)
        {
            if (cameraCount == 1)
            {
                foreach (var item in CameraShowUnitDic.Values)
                {
                    item.HWndUnit.Parent = panelHWndMax;
                }

                panelHWndMax.BringToFront();
                panelHWndMax.Dock = DockStyle.Fill;
                //panelHWndMax.Dock = DockStyle.None;

                //显示刷新
                foreach (var item in CameraShowUnitDic.Values)
                {
                    item.HWndUnit.HWndCtrl.Repaint();
                }

                return;
            }

            //txtCurrentCamera.Text = "无";
            if (index == -1)
            {


                foreach (var item in CameraShowUnitDic.Values)
                {
                    item.HWndUnit.Parent = tabPanel;
                }

                tabPanel.BringToFront();
                tabPanel.Dock = DockStyle.Fill;
                panelHWndMax.Dock = DockStyle.None;

                //显示刷新
                foreach (var item in CameraShowUnitDic.Values)
                {
                    item.HWndUnit.HWndCtrl.Repaint();
                }
            }
            else
            {
                if (CameraShowUnitDic.ContainsKey(index) == false)
                {
                    MessageBox.Show("相机未初始化", "提示",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                currentSettingIndex = index;
                this.tabPanel.Controls.Remove(CameraShowUnitDic[index].HWndUnit);
                CameraShowUnitDic[index].HWndUnit.Parent = panelHWndMax;

                panelHWndMax.BringToFront();
                CameraShowUnitDic[index].HWndUnit.BringToFront();
                panelHWndMax.Dock = DockStyle.Fill;
                tabPanel.Dock = DockStyle.None;
                if (CameraManger.CameraDic.ContainsKey(index))
                {
                    cameraSelect = CameraManger.CameraDic[index];

                }
                CameraShowUnitDic[index].InitDgvShow();
                foreach (var item in CameraShowUnitDic.Values)
                {
                    item.HWndUnit.HWndCtrl.Repaint();
                }
            }
        }

        #region 窗体控件响应
        private  void AutoUnit_Load(object sender, EventArgs e)
        {
            StatusManger statusManger = StatusManger.Instance;
            statusManger.RuningStatus = RuningStatus.初始化;

            //默认联机中
            btnInterlocking_Click(null, null);

          
           
            //InitDgvShow();
            //ChangeSize(1);
            timerInit.Enabled = true;

        }

        private void btnDeleteResult_Click(object sender, EventArgs e)
        {
            //if (StatusManger.Instance.IsChecker == false)
            //{
            //    MessageBox.Show("点检模式下才能清除数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            DialogResult result = MessageBox.Show("是否清除统计结果", "提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                DateTime dt = DateTime.Now;
                string timeNow = dt.ToString();
                string dayNow = dt.ToString("yyyy_MM_dd");
                string project = System.IO.Path.GetFileNameWithoutExtension
                    (UserSetting.Instance.ProjectPath);

                for (int i = 0; i < DtResultShow.Rows.Count; i++)
                {
                    string files = string.Format("D:\\data\\{0}\\清零保存\\{0}_相机{1}_{2}.csv", project, i + 1, dayNow);

                    runTheadDataDic[i + 1].ResetCount();
                    
                    SaveResult(i, files);
                }

                foreach (var item in CameraManger.CameraDic.Values)
                {
                    item.ResetFps();
                }

                InitResultTablte();
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                SetNotifyMessage(string.Format("运行结果保存出现异常"));
            }

        }

        private void btnInterlocking_Click(object sender, EventArgs e)
        {
            StatusManger status = StatusManger.Instance;
            if (status.IsInterlocking)
            {
                status.IsInterlocking = false;
                btnInterlocking.BackColor = SystemColors.Control;
                btnInterlocking.Text = "联机";
            }
            else
            {
                status.IsInterlocking = true;
                btnInterlocking.BackColor = Color.Green;
                btnInterlocking.Text = "联机中";
            }
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            StatusManger status = StatusManger.Instance;

            if (CameraManger.CameraDic == null || CameraManger.CameraDic.Count < IniStatus.Instance.CamearCount)
            {
                MessageBox.Show("相机个数异常,无法开始", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (status.RuningStatus == RuningStatus.系统异常)
            {
                MessageBox.Show("系统出现异常,请检查设置", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in CameraManger.CameraDic.Values)
            {
                if (item.IsLink == false)
                {
                    MessageBox.Show(string.Format("相机{0}未连接", item.CameraIndex));
                    return;
                }
                if (item.IsContinuousShot)
                {
                    item.ContinuousShotStop();
                }
            }
            if (CameraManger.CameraDic[1].IsExtTrigger)
            {
                StopExtTrigger();
            }
            else
            {
                StartExtTrigger();
            }
        }

        private void StopExtTrigger()
        {
            StatusManger status1 = StatusManger.Instance;
            foreach (var item in CameraManger.CameraDic.Values)
            {
                item.ContinuousShotStop();
            }

            btnRun.Text = "运行";
            btnRun.BackColor = SystemColors.Control;
            status1.RuningStatus = RuningStatus.等待运行;
            Util.Notify("外触发关闭");
            //Thread.Sleep(100);
        }

        private void StartExtTrigger()
        {
            StatusManger status1 = StatusManger.Instance;
            foreach (var item in CameraManger.CameraDic.Values)
            {
                item.SetExtTrigger();
            }
            CameraBase camera = CameraManger.CameraDic.Values.ToList().Find(x => x.IsExtTrigger == false);
            if (camera == null && CameraManger.CameraDic.Count > 0)
            {
                btnRun.Text = "运行中";
                btnRun.BackColor = Color.Green;
                status1.RuningStatus = RuningStatus.系统运行中;
                SetNotifyMessage(string.Format("等待外部触发"));
            }
            else
            {
                if (camera == null)
                {
                    SetNotifyMessage(string.Format("相机未连接,无法开始运行"));
                }
                else
                {
                    SetNotifyMessage(string.Format("相机{0}开始外触发运行异常", camera.CameraIndex));

                }
            }
        }

        private void timerInit_Tick(object sender, EventArgs e)
        {
            if (cameraCount != 1)
            {
                ChangeSize(-1);
            }
            timerInit.Enabled = false;
            //pictureUnit1.ShowScreenshots();
        }
        private void brnTestNgSigin_Click(object sender, EventArgs e)
        {

            if (CameraManger.CameraDic.ContainsKey(currentSettingIndex))
            {
                CameraManger.CameraDic[currentSettingIndex].Output();
            }
            else
            {
                MessageBox.Show("相机未连接", "提示",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void action()
        {


            HWndCtrl hWndCtrl = CameraShowUnitDic[1].HWndUnit.HWndCtrl;
            // Local iconic variables 

            HObject ho_Image, ho_Cross, ho_Cross1, ho_RegionLines = null;
            HObject ho_Cross2;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_flag = null;
            HTuple hv_color = null, hv_r1 = null, hv_c1 = null, hv_r2 = null;
            HTuple hv_c2 = null, hv_Index = null, hv_r3 = new HTuple();
            HTuple hv_c3 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Cross1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_Cross2);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, @"E:\1.bmp");
            HImage img = new HImage(ho_Image);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hWndCtrl.AddIconicVar(img);
            hv_flag = 0;
            hv_color = "red";
            hv_r1 = 100;
            hv_c1 = 50;
            hv_r2 = 150;
            hv_c2 = 250;
            //dev_update_off();
            ho_Cross.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross, hv_r1, hv_c1, 60, 0.785398);
            ho_Cross1.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross1, hv_r2, hv_c2, 60, 0.785398);
            hWndCtrl.AddIconicVar(ho_Cross);
            hWndCtrl.AddIconicVar(ho_Cross1);
            for (hv_Index = 1; (int)hv_Index <= 90; hv_Index = (int)hv_Index + 1)
            {

                if ((int)(new HTuple(hv_flag.TupleEqual(0))) != 0)
                {
                    hv_flag = hv_flag + 1;
                    hv_color = "red";
                }
                else
                {
                    hv_flag = 0;
                    hv_color = "green";
                }
                ho_RegionLines.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines, 100, 50, 150 + hv_Index, 250 + hv_Index);
                hv_r3 = 150 + hv_Index;
                hv_c3 = 250 + hv_Index;
                hWndCtrl.ChangeGraphicSettings(Mode.COLOR, hv_color);
                hWndCtrl.AddIconicVar(ho_RegionLines);
            }
            ho_Cross2.Dispose();
            HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_r3, hv_c3, 60, 0.785398);
            hWndCtrl.AddIconicVar(ho_Cross2);
            hWndCtrl.Repaint();
            //ho_Image.Dispose();
            ho_Cross.Dispose();
            ho_Cross1.Dispose();
            ho_RegionLines.Dispose();
            ho_Cross2.Dispose();

        }
        #endregion

        private void tsbCamera1Setting_Click(object sender, EventArgs e)
        {
            if (cameraSelect == null)
            {
                MessageBox.Show("当前无相机选择/当前相机未连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (cameraSelect is Camera.DirectShowCamera)
            //{
            //    MessageBox.Show("usb相机无参数设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            frmCameraSetting settingForm = new frmCameraSetting(cameraSelect);
            DialogResult result = settingForm.ShowDialog();
            CameraPram cameraPram = AppManger.ProjectData.Instance.GetCameraPram(cameraSelect.CameraIndex);
            if (result == DialogResult.OK)
            {

                cameraPram.Shutter = cameraSelect.ShuterCur;
                cameraPram.Gain = cameraSelect.GainCur;

                cameraPram.TriggerDelayAbs = cameraSelect.TriggerDelayAbs;
                cameraPram.LineDebouncerTimeAbs = cameraSelect.LineDebouncerTimeAbs;
                cameraPram.OutLineTime = cameraSelect.OutLineTime;
                cameraPram.ImageAngle = cameraSelect.ImageAngle;
                //camera1.SetExtTrigger();
                try
                {
                    AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);
                    UserSetting.Instance.SaveSetting();
                    SetNotifyMessage(string.Format("相机设置已保存"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败:" + ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                cameraSelect.ShuterCur = cameraPram.Shutter;
                cameraSelect.GainCur = cameraPram.Gain;

                cameraSelect.TriggerDelayAbs = cameraPram.TriggerDelayAbs;
                cameraSelect.LineDebouncerTimeAbs = cameraPram.LineDebouncerTimeAbs;
                cameraSelect.OutLineTime = cameraPram.OutLineTime;
                cameraSelect.ImageAngle = cameraPram.ImageAngle;
                //camera1.SetExtTrigger();
            }
        }

        private void tsbVido_Click(object sender, EventArgs e)
        {
            if (isTestMode == true)
            {
                //不能弹窗,会造成窗体不响应
                //MessageBox.Show("连续测试中 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cameraSelect == null)
            {
                MessageBox.Show("相机未连接 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cameraSelect.IsLink == false)
            {
                CameraPram cameraPram = AppManger.ProjectData.Instance.GetCameraPram(cameraSelect.CameraIndex);
                if (cameraSelect.Open())
                {
                    cameraSelect.ShuterCur = cameraPram.Shutter;
                    cameraSelect.GainCur = cameraPram.Gain;
                    cameraSelect.TriggerDelayAbs = cameraPram.TriggerDelayAbs;
                    cameraSelect.LineDebouncerTimeAbs = cameraPram.LineDebouncerTimeAbs;
                    cameraSelect.OutLineTime = cameraPram.OutLineTime;
                    cameraSelect.ImageAngle = cameraPram.ImageAngle;
                }
                if (cameraSelect.IsLink == false)
                {
                    MessageBox.Show("相机未连接 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            //修改关联的运行按钮状态
            btnRun.Text = "运行";
            btnRun.BackColor = SystemColors.Control;
            //cameraSelect.OneShot();
            //return;
            if (cameraSelect.IsContinuousShot)
            {
                cameraSelect.ContinuousShotStop();
                //camera1.SetExtTrigger();
            }
            else
            {
                cameraSelect.ContinuousShot();
            }
        }


        private void tsbShowAll_Click(object sender, EventArgs e)
        {
            ChangeSize(-1);
        }
        private void MySelectStdCPlash(HImage image, int thresholdMin, int thresholdMax, out HRegion resultRegion)
        {
            HRegion regionBin = image.Threshold((double)thresholdMin, thresholdMax);
            HRegion conn = regionBin.Connection();
            regionBin.Dispose();
            resultRegion = conn.SelectShapeStd("max_area", 70);
            conn.Dispose();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //action();
            HWndCtrl hWndCtrl = CameraShowUnitDic[1].HWndUnit.HWndCtrl;
            HImage image = new HImage();
            image.ReadImage("printer_chip\\printer_chip_01");
            HRegion region = null;
            int count = 100;
            HTuple st1, sn1;
            HOperatorSet.CountSeconds(out st1);
            for (int i = 0; i < count; i++)
            {
                //if (region!=null&&region.IsInitialized())
                //{
                //    region.Dispose();
                //}
                MySelectStdCPlash(image, 128, 255, out region);
            }
            HOperatorSet.CountSeconds(out sn1);
            double tt = (sn1 - st1) * 1000.0;
            Util.Notify(string.Format("c#方法用时{0:f4}ms", tt));

            HOperatorSet.CountSeconds(out st1);
            for (int i = 0; i < count; i++)
            {
                //if (region != null && region.IsInitialized())
                //{
                //    region.Dispose();
                //}
                Wrapper.Fun.MySelectStd(image, 128, 255, out region);
            }
            HOperatorSet.CountSeconds(out sn1);
            double tt1 = (sn1 - st1) * 1000.0;
            Util.Notify(string.Format("本地方法用时{0:f4}ms", tt1));

            double tt3 = tt - tt1;
            if (tt3 < 0)
            {
                Util.Notify(string.Format("c#方法更快,平均每次快{0:f4}ms", -tt3 / count));
            }
            else
            {
                Util.Notify(string.Format("本地方法更快,平均每次快{0:f4}ms", tt3 / count));
            }
            //MySelectStdCPlash(image,128,255,out region);
            Wrapper.Fun.MySelectStd(image, 128, 255, out region);
            hWndCtrl.AddIconicVar(image);
            hWndCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
            hWndCtrl.AddIconicVar(region);
            hWndCtrl.Repaint();
            // Ax + By + Cz + D=0
            //A    1
            //B   2
            //C  - 1
            //D   -4

            //平面拟合测试结果正确
            //Tools.PlaneFun.PlaneFun.PlaneInfo p1 = new Tools.PlaneFun.PlaneFun.PlaneInfo();
            //double[] x = new double[5];
            //double[] y = new double[5];
            //double[] z = new double[5];

            //x[0] = 1;
            //x[1] = 2;
            //x[2] = 3;
            //x[3] = 4;
            //x[4] = 5;

            //y[0] = 2;
            //y[1] = 3;
            //y[2] = 4;
            //y[3] = 6;
            //y[4] = 3;

            //z[0] = 1;
            //z[1] = 4;
            //z[2] = 7;
            //z[3] = 12;
            //z[4] = 7;
            //bool f = Tools.PlaneFun.PlaneFun.FitPlane(5, x, y, z, ref p1);

            //double dd = Tools.PlaneFun.PlaneFun.DisPntToPlane(5, 7, 12, ref p1);

            //double fff = Tools.PlaneFun.PlaneFun.Flatness(5, x, y, z, ref p1);
            //string dat = fff.ToString("f4");
            //bool f = FitPlaneFun.FitPlane1( ref p1);
            //FitPlaneFun.testStru1 stru1 = new FitPlaneFun.testStru1();
            //FitPlaneFun.FitPlane(ref p1);
            //Util.Notify(p1.A.ToString());
            ;
        }

        private void tscobCameraSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sleectIndex = tscobCameraSelect.SelectedIndex + 1;
            ChangeSize(sleectIndex);
        }

        private void tsbCamera_Click(object sender, EventArgs e)
        {
            //Common.ImageProcess.HImageExt test = new Common.ImageProcess.HImageExt();

            //test.ReadImage(@"D:\12.bmp");
            //CameraShowUnitDic[1].HWndUnit.HWndCtrl.AddIconicVar(test.Clone());
            //CameraShowUnitDic[1].HWndUnit.HWndCtrl.Repaint();
            //return;
            if (StatusManger.Instance.TestInitFinish == false)
            {
                MessageBox.Show("请等待初始化完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int settingKey = toolShowUnit1.CurrentSettingKey;
            int cameraIndex = toolShowUnit1.CurrentCameraIndex;
            Tools.CreateImage.CreateImageTool createImageTool = ToolsFactory.GetToolList(settingKey)[0] as Tools.CreateImage.CreateImageTool;
            if (createImageTool != null && createImageTool.OffLineMode == true)
            {
                runTheadDataDic[cameraIndex].TrigerRun(settingKey, true);
                return;
            }

            if (cameraSelect == null)
            {
                MessageBox.Show("相机未连接 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cameraSelect.IsLink == false)
            {
                CameraPram cameraPram = AppManger.ProjectData.Instance.GetCameraPram(cameraSelect.CameraIndex);
                if (cameraSelect.Open())
                {
                    cameraSelect.ShuterCur = cameraPram.Shutter;
                    cameraSelect.GainCur = cameraPram.Gain;
                    cameraSelect.TriggerDelayAbs = cameraPram.TriggerDelayAbs;
                    cameraSelect.LineDebouncerTimeAbs = cameraPram.LineDebouncerTimeAbs;
                    cameraSelect.OutLineTime = cameraPram.OutLineTime;
                    cameraSelect.ImageAngle = cameraPram.ImageAngle;
                }
                if (cameraSelect.IsLink == false)
                {
                    MessageBox.Show("相机未连接 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            //修改关联的运行按钮状态
            btnRun.Text = "运行";
            btnRun.BackColor = SystemColors.Control;
            //cameraSelect.OneShot();
            //return;
            if (cameraSelect.IsContinuousShot)
            {
                cameraSelect.ContinuousShotStop();
                isTestMode = false;
            }
            else
            {
                cameraSelect.ContinuousShot();
                isTestMode = true;
            }
        }
    }
}
