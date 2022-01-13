using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.Helpers
{
    public static class ApplicationHelper
    {
        /// <summary>
        ///  启动一个应用程序/进程
        /// </summary>       
        public static void StartApplication(string appFilePath)
        {
            Process downprocess = new Process();
            downprocess.StartInfo.FileName = appFilePath;
            downprocess.Start();
        }
    }
}
