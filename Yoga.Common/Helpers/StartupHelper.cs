using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.Helpers
{
    public class StartupHelper
    {

        private static string regAll = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private static string regCurrent = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run";
        private static string commonStartup = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
        private static string startup = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        public static bool Register(string name, string file, bool start = true, bool allUser = true)
        {
            if (start)
            {
                //注册开机启动注册表项
                if (allUser)
                {
                    if (RegisterTool.SetValue(regAll, name, file)) return true;
                }
                else
                {
                    if (RegisterTool.SetValue(regCurrent, name, file)) return true;
                }
            }
            else
            {
                //移除开机启动注册表项
                if (allUser)
                {
                    if (RegisterTool.DeleteValue(regAll, name)) return true;
                }
                else
                {
                    if (RegisterTool.DeleteValue(regCurrent, name)) return true;
                }
            }
            return false;
        }
       
    }
}
