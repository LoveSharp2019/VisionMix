using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.Camera;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.Common.Helpers;
using Yoga.VisionMix.Units;

namespace Yoga.VisionMix.Frame
{
    public partial class FrmMain : Form
    {

        AutoUnit autoUnit;
        public FrmMain()
        {

            InitializeComponent();

            this.Size = new System.Drawing.Size(IniStatus.Instance.WindowWidth, IniStatus.Instance.WindowHeigth);

            //this.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            //this.Location = (Point)new Size(0, 0);         //窗体的起始位置为(x,y)



            autoUnit = new AutoUnit(this);
            lblCommStatus.Text = "";
            lblUserMode.Text = "";
            autoUnit.BorderStyle = BorderStyle.None;
            autoUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Controls.Add(autoUnit);

            string path = Environment.CurrentDirectory + "\\project";

            FileInfo fi = new FileInfo(path);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            openProjectFileDialog.InitialDirectory = path;

            saveProjectFileDialog.InitialDirectory = path;

            tsCuurrentProject.Text = System.IO.Path.GetFileNameWithoutExtension
                (UserSetting.Instance.ProjectPath);
            bool login = false;
            bool isDebug = false;
#if DEBUG
            login = true;
            isDebug = true;
#endif
            LoginSetting(login);

            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string time = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).
                ToString("yyyy_MM_dd");
            string v1 = " V" + version + "_" + time;
            string debugFlag = isDebug ? "测试版" : "";
            this.Text = $"视觉软件 VisionMix V{v1} {debugFlag}";
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("即将退出软件,是否保存当前设置?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            //新增取消软件关闭功能
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            else if (result == DialogResult.Yes)
            {
                try
                {
                    AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);
                }
                catch (Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    string message = ex.Message;
                    MessageBox.Show("工程数据保存失败" + message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Application.Exit();
            CameraManger.Close();
            AppManger.AppInterlockHelper.Instance.Close();
            PLC.PanasonicPLCComm.Instance.CloseCom();
            //关闭所有相机处理线程
            foreach (var item in autoUnit.runTheadDataDic.Values)
            {
                item.Stop();
            }
            //关闭所有线程-此处有点暴力-待删除
            Process.GetCurrentProcess().Kill();

            Application.ExitThread();
        }
        public void ShowErr(string msg)
        {
            tsErrMessage.Text = msg;
            tsErrMessage.BackColor = System.Drawing.Color.Red;
        }

        public void ShowCommStatus()
        {
            string interlockMode = EnumHelper.GetDescription(AppManger.AppInterlockHelper.Instance.CommunicationParam.InterlockMode);
            if (AppManger.AppInterlockHelper.Instance.CommunicationParam.IsExtTrigger)
            {
                lblCommStatus.Text = string.Format("|通信模式:{0}|相机外触发", interlockMode);
            }
            else
            {
                lblCommStatus.Text = string.Format("|通信模式:{0}", interlockMode);
            }
        }
        private void timerTick_Tick(object sender, EventArgs e)
        {
            tsTime.Text = DateTime.Now.Date.ToString("yyyy/MM/dd") + " " + DateTime.Now.ToLongTimeString().ToString();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void 打开项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string files;
            if (openProjectFileDialog.ShowDialog() == DialogResult.OK)
            {
                files = openProjectFileDialog.FileName;

                UserSetting.Instance.ProjectPath = files;
                tsCuurrentProject.Text = System.IO.Path.GetFileNameWithoutExtension
                (UserSetting.Instance.ProjectPath);
                autoUnit.LoadProject(true);

            }
        }
        private async void 项目另存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string files;

            if (saveProjectFileDialog.ShowDialog() == DialogResult.OK)
            {
                files = saveProjectFileDialog.FileName;
                await Task.Run(() =>
                {
                    try
                    {
                        AppManger.ProjectData.Instance.SaveProject(files);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存失败:" + ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                UserSetting.Instance.ProjectPath = files;
                tsCuurrentProject.Text = System.IO.Path.GetFileNameWithoutExtension
                (UserSetting.Instance.ProjectPath);
                autoUnit.LoadProject(false);
            }
        }
        private async void 保存项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    AppManger.ProjectData.Instance.SaveProject(UserSetting.Instance.ProjectPath);
                    //LoadProject(true);
                    //ToolsFactory.ReadTools(UserSetting.Instance.ProjectPath);

                    this.Invoke(
                  (MethodInvoker)delegate
                  {
                      UserSetting.Instance.SaveSetting();
                      //InitResultTablte();
                      MessageBox.Show("数据保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
                            );
                }
                catch (Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    string message = ex.Message;
                    MessageBox.Show(message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }/* if */

        private void 通信设定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCommSetting frm = new frmCommSetting();
            frm.ShowDialog();
            autoUnit.RefreshBtnRunVisible();
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin Frame_Login = new frmLogin();
            bool isLogin = false;
            if (Frame_Login.ShowDialog() != DialogResult.OK)
            {
                isLogin = false;
                //_pMainFrame.Frame_Show.FrameAuto.LoginSetting(false);
                //btnSetting.Enabled = false;
                //return;
            }
            else
            {
                isLogin = true;
                //timerLogin.Interval = 1000 * 60 * IniStatus.Instance.LogionDelay;
                //timerLogin.Enabled = true;
            }

            LoginSetting(isLogin);
        }

        private void LoginSetting(bool isLogin)
        {
            autoUnit.LoginSetting(isLogin);
            设置ToolStripMenuItem.Enabled = isLogin;
            string mode = isLogin ? "管理员" : "操作员";
            lblUserMode.Text = $"|权限:{mode}";
        }

        private void 项目管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prjOldName = UserSetting.Instance.ProjectPath;
            frmProjectManger frm = new frmProjectManger();
            frm.ShowDialog();
            if (UserSetting.Instance.ProjectPath == prjOldName)
            {
                return;
            }
            autoUnit.LoadProject(true);
        }

        private void 清除异常ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsErrMessage.Text = "";
            tsErrMessage.BackColor = DefaultBackColor;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Common.UI.FrmLoad frmLoad = new Common.UI.FrmLoad(Application.StartupPath + @"\Res\Loading\");

            //Common.UI.Parameter.Name = frmAbout.AssemblyProduct;
            //Common.UI.Parameter.Year = DateTime.Now.ToString("yyyy");
            //Common.UI.Parameter.Version = frmAbout.AssemblyVersion;
            //Common.UI.Parameter.CopyRight = frmAbout.AssemblyCopyright;
            //Common.UI.Parameter.CompanyName = frmAbout.AssemblyCompany;
            //Common.UI.Parameter.Status = "初始化中...";
            //Common.UI.Parameter.StatusNum = 0;
            //frmLoad.Show();

            //frmLoad.UpdateProgress("相机加载...", 0, 30, 500);
            //frmWait.AsyncMethod += ((obj, args) =>
            {

                //注册认证
#if !DEBUG
            
            //string key = "";
            //key = Common.RegisterHelper.getMNum();
            // if (UserSetting.Instance.SoftKey == null || UserSetting.Instance.SoftKey != key)
            //{
            //    if (UserSetting.Instance.SoftKey == null)
            //    {
            //        MessageBox.Show("验证码错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //        //      Common.Util.Notify("验证码错误");
            //    }
            //    else
            //    {
            //        // MessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    frmRegistered registered = new frmRegistered();
            //    registered.ShowDialog();
            //}
            //if (UserSetting.Instance.SoftKey == null || UserSetting.Instance.SoftKey != key)
            //{
            //    // 终止此进程并为基础操作系统提供指定的退出代码。
            //    System.Environment.Exit(1);
            //}
#endif
                //打开相机
                try
                {
                    if (CameraManger.Open())
                    {
                        int cameraCount = IniStatus.Instance.CamearCount;
                        List<int> cameraList = new List<int>();
                        for (int i = 0; i < cameraCount; i++)
                        {
                            cameraList.Add(i + 1);
                        }
                        Dictionary<int, CameraBase> cameraDic = CameraManger.CameraDic;
                        foreach (var item in cameraList)
                        {
                            if (cameraDic.ContainsKey(item) == false)
                            {
                                MessageHelper.ShowError("相机序号设置异常");
                                //return;
                            }

                        }
                    }
                    else
                    {
                        StatusManger.Instance.RuningStatus = RuningStatus.系统异常;
                        Common.Util.Notify(Level.Err, "相机打开失败");
                    }
                }
                catch (Exception ex)
                {

                    MessageHelper.ShowError("相机打开出现异常");
                    Common.Util.WriteLog(this.GetType(), ex);
                    StatusManger.Instance.RuningStatus = RuningStatus.系统异常;
                    Common.Util.Notify(Level.Err, "相机打开出现异常");
                }

                try
                {
                    IOSerial.Instance.Rs232Param = UserSetting.Instance.IoRs232Param;
                    if (UserSetting.Instance.IoRs232Param.Use)
                    {
                        IOSerial.Instance.InitSerial();
                    }
                }
                catch (Exception ex)
                {
                    Common.Util.WriteLog(this.GetType(), ex);
                    StatusManger.Instance.RuningStatus = RuningStatus.系统异常;
                    Common.Util.Notify(Level.Err, "io输出串口打开异常");
                }
                //frmLoad.UpdateProgress("工程数据加载...", 30, 50, 500);
                autoUnit.LoadProject(true);
                //frmLoad.Close();
            }
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                e.Handled = true;       //将Handled设置为true，指示已经处理过KeyDown事件   
                保存项目ToolStripMenuItem.PerformClick(); //执行单击button1的动作   
            }
        }
    }
}
