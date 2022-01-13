using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yoga.Common.MotionControl
{
    public abstract class MoveCard : CommBase
    {
        //(1) 细分
        public const int RES_1 = 1600;
        public const int RES_2 = 1600;
        public const int RES_3 = 1600;
        public const int RES_4 = 120000;

        //(2) 螺距
        public const int SCR_1 = 1;
        public const int SCR_2 = 2;
        public const int SCR_3 = 2;
        public const int SCR_4 = 360;


        //(3) 脉冲当量
        public const double SCALE_1  =(RES_1 * 1.0)/(SCR_1 * 1.0);
        public const double SCALE_2 = (RES_2 * 1.0) / (SCR_2 * 1.0);
        public const double SCALE_3 = (RES_3 * 1.0) / (SCR_3 * 1.0);
        public const double SCALE_4 = (RES_4 * 1.0) / (SCR_4 * 1.0);

        //(4) 复位参数设置
        public static readonly double[] HOM_VEL_STEP1 = new double[4]{ 20.0,20.0,20.0 , 20.0 };


        public const double HOM_ACC_STEP1_1 = (2.0);
        public const double HOM_ACC_STEP1_2 = (2.0);
        public const double HOM_ACC_STEP1_3 = (2.0);
        public const double HOM_ACC_STEP1_4 = (2.0);
        public const double HOM_ACC_STEP1_5 = (2.0);
        public const double HOM_ACC_STEP1_6 = (2.0);
        public const double HOM_ACC_STEP1_7 = (2.0);
        public const double HOM_ACC_STEP1_8 = (2.0);

        public const double HOM_VEL_STEP2_1 = (10.0);
        public const double HOM_VEL_STEP2_2 = (10.0);
        public const double HOM_VEL_STEP2_3 = (10.0);
        public const double HOM_VEL_STEP2_4 = (10.0);
        public const double HOM_VEL_STEP2_5 = (10.0);
        public const double HOM_VEL_STEP2_6 = (10.0);
        public const double HOM_VEL_STEP2_7 = (10.0);
        public const double HOM_VEL_STEP2_8 = (10.0);

        public const double HOM_ACC_STEP2_1 = (1.0);
        public const double HOM_ACC_STEP2_2 = (1.0);
        public const double HOM_ACC_STEP2_3 = (1.0);
        public const double HOM_ACC_STEP2_4 = (1.0);
        public const double HOM_ACC_STEP2_5 = (1.0);
        public const double HOM_ACC_STEP2_6 = (1.0);
        public const double HOM_ACC_STEP2_7 = (1.0);
        public const double HOM_ACC_STEP2_8 = (1.0);

        /// <summary>
        /// 运行速度 默认5脉冲/控制周期
        /// </summary>
        protected double speedRun = 0.5;
        protected bool homeFinish = false;
        protected const int axisNum = 3;
        protected ManualResetEvent commWaitResetEvent = new ManualResetEvent(true);

        public double SpeedRun
        {
            get
            {
                return speedRun;
            }

            set
            {
                speedRun = value;
            }
        }
        public bool HomeFinish
        {
            get
            {
                return homeFinish;
            }
        }
        public abstract void RunningError(string act, short status);
        public abstract bool InitCard();

        /// <summary>
        /// 暂停通信线程,并等待通信线程不在运行状态
        /// </summary>
        public virtual void SetCommPause()
        {
            commWaitResetEvent.Reset();
            if (procPLC != null)
            {
                while (procPLC.ThreadState == ThreadState.Running)
                {
                    Thread.Sleep(10);
                }
            }
        }
        /// <summary>
        /// 取消暂停通信程序
        /// </summary>
        public void CancleCommPause()
        {
            commWaitResetEvent.Set();
        }
        public virtual void AxisPosMove(int axisIndex, int pos, bool isAbsolute)
        {
           // speedRun = ConfigManager.Instance.SpeedRun;
            AxisPosMove(axisIndex, pos, speedRun, isAbsolute);
        }
        public abstract void AxisPosMove(int axisIndex, int pos, double speed, bool isAbsolute);

        public virtual void MoveToPos(int  axis1,int axis2, int axis3)
        {
            if (homeFinish == false)
            {
                Util.Notify("控制卡未回原点");
                return;
            }
            MoveToPosNoWait(axis1, axis2, axis3);
            WaitAllRunFinish();
        }
        /// <summary>
        /// 到达目标位置,未等待结束
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public virtual void MoveToPosNoWait(int axis1, int axis2, int axis3)
        {
            if (homeFinish == false)
            {
                Util.Notify("控制卡未回原点");
                return;
            }

            AxisPosMove(1, axis1, speedRun, true);
            AxisPosMove(2, axis2, speedRun, true);
            AxisPosMove(3, axis3, speedRun, true);
        }

        public abstract void Homing(short axisIndex, int pos);

        public virtual void HomeAll()
        {
            if (isLink == false)
            {
                Util.Notify("控制卡未连接");
                return;
            }
            homeFinish = false;
            Task home1 = Task.Run(() => Homing(1, -2000000));
            Task home2 = Task.Run(() => Homing(2, 2000000));
            Task home3 = Task.Run(() => Homing(3, 2000000));
            Task.WaitAll(home1, home2, home3);
            homeFinish = true;
        }
        public virtual void WaitAllRunFinish()
        {
            for (int i = 0; i < axisNum; i++)
            {
                WaitMoved((short)(i + 1));
            }
        }
        public abstract void WaitMoved(short axis);
        public abstract void WaitMove2End(short axis);
    }
}
