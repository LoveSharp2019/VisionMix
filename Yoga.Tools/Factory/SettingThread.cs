using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Tools
{
    /// <summary>
    /// 设置处理线程
    /// </summary>
    public class SettingThread
    {
        ToolBase tool;
        int runCount;
        bool isRunning = false;
        public event EventHandler<EventArgs> RunFinishEvent;

        private object lockObj = new object();

        public int RunCount
        {
            get
            {
                lock (lockObj)
                {
                    return runCount;
                }
            }

            set
            {
                lock (lockObj)
                {
                    runCount = value;
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                lock (lockObj)
                {
                    return isRunning;
                }
            }

            set
            {
                lock (lockObj)
                {
                    isRunning = value;
                }
            }
        }

        public SettingThread(ToolBase tool)
        {
            this.tool = tool;
        }
        private void Run()
        {
            Task.Run(new Action(() =>
            {
                IsRunning = true;
                try
                {
                    while (RunCount > 0)
                    {
                        RunCount = 0;

                        tool.RunRef();
                        if (RunFinishEvent != null)
                        {
                            RunFinishEvent(this, null);
                        }

                    }

                }
                catch (Exception /*ex*/)
                {
                    Common.Util.Notify("发生异常,请检查参数" );
                }
                finally
                {
                    IsRunning = false;
                }

            }));

        }
        public void AddTask()
        {
            RunCount++;
            if (IsRunning == false)
            {
                Run();
            }
        }

    }
}
