using System;

namespace Yoga.VisionMix
{
    public enum RuningStatus
    {
        系统异常,
        初始化,
        等待运行,
        系统运行中
    }
    /// <summary>
    /// 系统运行状态管理
    /// </summary>
    public class StatusManger
    {
        private static StatusManger instance;

        bool testInitFinish;
        private int okCount;
        private int ngCount;
        private RuningStatus runingStatus = RuningStatus.初始化;
        private bool runingResult = true;
        private object lockObject = new object();

        private double cycleTime;
        public event EventHandler<EventArgs> StatusEvent;
        private bool isInterlocking = false;
        private bool isChecker = false;
        public static StatusManger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StatusManger();
                }
                return instance;
            }
        }

        public RuningStatus RuningStatus
        {
            get
            {
                lock (lockObject)
                {
                    return runingStatus;
                }

            }

            set
            {
                lock (lockObject)
                {
                    if (runingStatus== RuningStatus.系统异常)
                    {
                        return;
                    }
                    runingStatus = value;
                    if (StatusEvent != null)
                    {
                        StatusEvent(this, null);
                    }
                }
            }
        }

        public int OkCount
        {
            get
            {
                lock (lockObject)
                {
                    return okCount;
                }
            }

            set
            {
                lock (lockObject)
                {
                    okCount = value;
                }
            }
        }

        public int NgCount
        {
            get
            {
                lock (lockObject)
                {
                    return ngCount;
                }
            }

            set
            {
                lock (lockObject)
                {
                    ngCount = value;
                }
            }
        }

        public bool RuningResult
        {
            get
            {
                lock (lockObject)
                {
                    return runingResult;
                }
            }

            set
            {
                lock (lockObject)
                {
                    runingResult = value;
                    if (StatusEvent != null)
                    {
                        StatusEvent(this, null);
                    }
                }
            }
        }

        public double CycleTime
        {
            get
            {
                lock (lockObject)
                {
                    return cycleTime;
                }
            }
            set
            {
                lock (lockObject)
                {
                    cycleTime = value;
                }
            }
        }
        /// <summary>
        /// 联机状态中
        /// </summary>
        public bool IsInterlocking
        {
            get
            {
                lock (lockObject)
                {
                    return isInterlocking;
                }
            }

            set
            {
                lock (lockObject)
                {
                    isInterlocking = value;
                }
            }
        }

        public bool IsChecker
        {
            get
            {
                lock (lockObject)
                {
                    return isChecker;
                }
            }
            set
            {
                lock (lockObject)
                {
                    isChecker = value;
                }
            }
        }

        public bool TestInitFinish
        {
            get
            {
                return testInitFinish;
            }

            set
            {
                testInitFinish = value;
            }
        }
    }
}
