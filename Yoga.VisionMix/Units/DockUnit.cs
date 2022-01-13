using System;
using System.Reflection;
using System.Windows.Forms;
using Yoga.VisionMix.Frame;
using System.Diagnostics;

namespace Yoga.VisionMix.Units
{
    public partial class DockUnit : UserControl
    {
        FrmMain _pMainFrame;
        Process kbpr;
        public DockUnit(FrmMain mainframe)
        {
            _pMainFrame = mainframe;
            InitializeComponent();
        }
        private void Btn_Quit_Click(object sender, EventArgs e)
        {
            _pMainFrame.Close();

        }


        private void Btn_IO_Click(object sender, EventArgs e)
        {
            //_pMainFrame.Frame_Show.ShowControls(_pMainFrame.Frame_Show.FrameIO);
        }


        private void Btn_Automation_Click(object sender, EventArgs e)
        {
            //_pMainFrame.Frame_Show.ShowControls(_pMainFrame.Frame_Show.FrameAuto);
        }


        private void Btn_About_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            Label_Date.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
            Label_Time.Text = DateTime.Now.ToLongTimeString().ToString();
        }

        private void DockFrame_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string time = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).
                ToString("yy_MM_dd");
            Label_Version.Text = "V"+ version+"\r\n"+time;
        }
        private void btnLogin_Click(object sender, EventArgs e)
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
                timerLogin.Interval = 1000 * 60* IniStatus.Instance.LogionDelay;
                timerLogin.Enabled = true;
            }

            LoginSetting(isLogin);
        }

        public void LoginSetting(bool isLogin)
        {
            //_pMainFrame.Frame_Show.FrameAuto.LoginSetting(isLogin);
            btnSetting.Enabled = isLogin;
           // Btn_Quit.Enabled = isLogin;
        }
        private void btnSetting_Click(object sender, EventArgs e)
        {
            //_pMainFrame.Frame_Show.ShowControls(_pMainFrame.Frame_Show.FrameConfig);
        }

        private void btnSoftKeyBoard_Click(object sender, EventArgs e)
        {
            try
            {
                if (kbpr != null && !kbpr.HasExited)
                {
                    kbpr.Kill();
                }
                else
                {
                    kbpr = System.Diagnostics.Process.Start("osk.exe"); // 打开系统键盘
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("软键盘操作异常:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            LoginSetting(false);
            timerLogin.Enabled = false;
        }
    }
}
