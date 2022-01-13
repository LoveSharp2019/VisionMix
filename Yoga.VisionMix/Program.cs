using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Yoga;

namespace Yoga.VisionMix
{
   
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine("_______________程序开始运行____________________________");


            //System.Collections.Generic.Dictionary<Tools.Base.MKey, object> JobDataDic =

            //new System.Collections.Generic.Dictionary<Tools.Base.MKey, object>();

            //JobDataDic.Add(key1, null);
            //bool tt7 = JobDataDic.ContainsKey(key2);
            //JobDataDic.Add(key2, null);
            //string files = Environment.CurrentDirectory + "\\" + project +
            //                              "\\NgImage\\" + "\\相机" + imageData.CameraIndex + "\\" + timeNow + ".png";

            Debug.WriteLine("主线程编号为:" + Thread.CurrentThread.ManagedThreadId);
            //全局异常捕捉
            Application.ThreadException += Application_ThreadException; //UI线程异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; //多线程

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
            //if (IsAdministrator() == false)
            //{
            //    MessageBox.Show("请以管理员身份打开软件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    System.Threading.Thread.Sleep(100);
            //    // 终止此进程并为基础操作系统提供指定的退出代码。
            //    System.Environment.Exit(1);
            //} 
#endif
            bool createNew;
            // 如果指定的命名系统互斥体已存在，则为false
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
#if DEBUG
                    /* This is a special debug setting needed only for GigE cameras.
                        See 'Building Applications with pylon' in the Programmer's Guide. */
                    Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);
#endif
                    UserSetting.Instance.ReadSetting();
                    IniStatus.Instance.ReadINI();
                    Wrapper.Fun.InitSystem();
                    Yoga.VisionMix.Frame.FrmMain frm = new Yoga.VisionMix.Frame.FrmMain();
                    Application.Run(frm);
                }
                // 程序已经运行的情况，则弹出消息提示并终止此次运行
                else
                {
                    MessageBox.Show("软件重复打开,即将退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Threading.Thread.Sleep(100);
                    // 终止此进程并为基础操作系统提供指定的退出代码。
                    System.Environment.Exit(1);
                }
            }


        }
        /// <summary>
        /// 确定当前主体是否属于具有指定 Administrator 的 Windows 用户组
        /// </summary>
        /// <returns>如果当前主体是指定的 Administrator 用户组的成员，则为 true；否则为 false。</returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        //UI线程异常
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "发生未捕获主线程异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Yoga.Common.Util.WriteLog(typeof(Program), "发生未捕获主线程异常_" + e.Exception.ToString());
        }

        //多线程异常
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ExceptionObject.ToString(), "发生未捕获多线程异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Yoga.Common.Util.WriteLog(typeof(Program), "发生未捕获多线程异常_" + e.ExceptionObject.ToString());
        }
    }
}
