using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;


namespace Yoga.Common.MotionControl
{
    /// <summary>
    /// 固高GT400运动控制卡通信
    /// </summary>
  public  class Gt400Comm : CommBase
    {
        /// <summary>类的实例</summary>
        protected static Gt400Comm instance = null;


        CancellationTokenSource source = new CancellationTokenSource();
        /// <summary>
        /// 轴动作锁
        /// </summary>
        private readonly object cardActLock = new object();

        const int axisNum = 2;
        /// <summary>
        /// 运行加速度 
        /// </summary>
        private double accRun = 0.1;
        /// <summary>
        /// 运行速度 默认5脉冲/控制周期
        /// </summary>
        private double speedRun = 0.5;


        private bool homeFinish = false;

        /// <summary>
        /// y轴回原点运行加速度 
        /// </summary>
        private double accHomeY = 0.1;
        /// <summary>
        /// y轴回原点运行速度 
        /// </summary>
        private double speedHomeY = 0.2;
        /// <summary>
        /// x轴回原点运行加速度 
        /// </summary>
        private double accHomeX = 0.1;
        /// <summary>
        /// x轴回原点运行速度 
        /// </summary>
        private double speedHomeX = 0.2;
        protected Thread getCardInfoThread = null;

        private ManualResetEvent commWaitResetEvent = new ManualResetEvent(true);

        public static Gt400Comm Instance
        {
            get
            {
                if (instance == null)
                    instance = new Gt400Comm();
                return instance;
            }
        }
        /// <summary>
        /// 动作取消源头 每次使用要new
        /// </summary>
        public CancellationTokenSource Source
        {
            get
            {
                return source;
            }
        }

        public bool HomeFinish
        {
            get
            {
                return homeFinish;
            }
        }
        public bool ResetSoure()
        {
            //int es = GetDi(Define.DiEmergencyStop);
            //if (es == 0)
            //{
            //    source = new CancellationTokenSource();
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
            
        public bool InitCard()
        {
            try
            {
                short rtn;
                lock (cardActLock)
                {
                    //连接控制器
                    rtn = GT400.GT_Open();
                    Error("打开控制卡", rtn);
                    if (0 != rtn)
                    {
                        isLink = false;
                        return false;
                    }

                    rtn = GT400.GT_Reset();
                    runningError("控制卡复位", rtn);
                    if (0 != rtn)
                    {
                        isLink = false;
                        return false;
                    }


                    ushort sense = 0xffff;
                    rtn = GT400.GT_LmtSns(sense);     //设置限位开关触发电平
                    runningError("设置限位开关电平", rtn);
                    if (0 != rtn)
                    {
                        isLink = false;
                        return false;
                    }
                }
                for (int i = 0; i < axisNum; i++)
                {
                    lock (cardActLock)
                    {
                        //设置轴参数
                        rtn = GT400.GT_Axis((ushort)(i + 1));     //切换到测试轴
                        runningError("设置当前轴", rtn);
                        if (0 != rtn)
                        {
                            isLink = false;
                            return false;
                        }
                        rtn = GT400.GT_ClrSts();           //清除不正常状态
                        runningError("GT_ClrSts", rtn);
                        if (0 != rtn)
                        {
                            isLink = false;
                            return false;
                        }
                        rtn = GT400.GT_StepDir();          //脉冲方式为‘脉冲+方向’，必须与驱动器的设置一致
                        runningError("GT_StepDir", rtn);
                        if (0 != rtn)
                        {
                            isLink = false;
                            return false;
                        }
                        //闭环模式下必须设置PID


                        rtn = GT400.GT_AxisOn();           //伺服使能，如果驱动器没有上伺服，请检查：1、测试轴是否是axis 1，2、端子板及驱动器是否上电
                        runningError("GT_AxisOn", rtn);
                        if (0 != rtn)
                        {
                            isLink = false;
                            return false;
                        }

                        rtn = GT400.GT_PrflS();
                        //rtn = GT400.GT_PrflT();
                        runningError("当前轴设定为S曲线", rtn);
                        if (0 != rtn)
                        {
                            isLink = false;
                            return false;
                        }
                    }
                }

                isLink = true;
                return true;
            }
            catch (Exception ex)
            {
                Util.Notify("控制卡打开失败 错误原因" + ex.Message);
                //Notify("控制卡打开失败 错误原因" + ex.Message);
                CloseCom();
                isLink = false;
                return false;
            }
        }

        /// <summary>
        /// 暂停通信线程,并等待通信线程不在运行状态
        /// </summary>
        public void SetCommPause()
        {
            commWaitResetEvent.Reset();
            if (procPLC!=null)
            {
                while (procPLC.ThreadState== ThreadState.Running)
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
        protected override void ProcDataDeal()
        {

            while (isLink)
            {
                // 状态读取
                try
                {
                    commWaitResetEvent.WaitOne();

                    #region INT读取
                    short rtn;
                    int ofst;
                    ReadInput();

                    #endregion
                    Thread.Sleep(3);

                    #region OUT写入
                    //// 锁定,从共享存储器获取要写入plc的数据
                    //mutexShMem.WaitOne();
                    //short data = new short();
                    //ofst = DMO_OFST;

                    //data = Marshal.ReadInt16(mapAddrPtr, ofst);
                    //// 解除
                    //mutexShMem.ReleaseMutex();
                    ////状态取反后输出
                    //ushort ioOutput = (ushort)(~data);
                    //lock (cardActLock)
                    //{
                    //    GT400.GT_ExOpt(ioOutput);
                    //}
                    #endregion

                    commWaitResetEvent.WaitOne();

                    #region 轴状态获取

                    ushort[] status = new ushort[axisNum];
                    int[] pos = new int[axisNum];
                    for (int i = 0; i < axisNum; i++)
                    {
                        lock (cardActLock)
                        {
                            rtn = GT400.GT_Axis((ushort)(i + 1));
                            runningError("当前轴设置", rtn);

                            rtn = GT400.GT_GetSts(out status[i]);
                            runningError("当前轴状态读取", rtn);

                            rtn = GT400.GT_GetPos(out pos[i]);
                            runningError("当前轴位置信息读取", rtn);
                        }
                        Thread.Sleep(3);
                    }

                    int count = 0;
                    for (int i = 0; i < axisNum; i++)
                    {
                        mutexShMem.WaitOne();

                        // 共有存储在存储器
                        ofst = DMI_OFST + (1 + i) * Marshal.SizeOf(typeof(short));
                        //将读取到的PLC数据写入到相应的内存区域
                        Marshal.WriteInt16(mapAddrPtr, ofst, (short)status[i]);


                        //位置信息
                        ofst = DMI_OFST + (DMO_A_START + count) * Marshal.SizeOf(typeof(int));

                        count++;
                        Marshal.WriteInt32(mapAddrPtr, ofst, pos[i]);
                        ofst = DMI_OFST + (DMO_A_START + count) * Marshal.SizeOf(typeof(int));
                        count++;
                        // 解除
                        mutexShMem.ReleaseMutex();
                    }
                    #endregion
                    Thread.Sleep(3);

                    commWaitResetEvent.WaitOne();
                    ////Measurement.MeasurementManager measureManger = Measurement.MeasurementManager.Instance;
                    ////int left = GetDi(Define.DiLeftButton);
                    ////int es = GetDi(Define.DiEmergencyStop);

                    //if (left == 1 && leftButtonBefore == 0 && 
                    //    measureManger.InMeasurement == false&&
                    //    es==0)
                    //{
                        
                    //    try
                    //    {
                    //        source = new CancellationTokenSource();
                    //        Task.Run(() =>
                    //        {
                    //            measureManger.RunMeasurementLeft();
                    //        }, source.Token);
                    //    }
                    //    catch (AggregateException)//捕获紧急停止按钮
                    //    {
                    //        //MessageBox.Show("停止按钮被按下", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}
                    //leftButtonBefore = left;

                    //int right = GetDi(Define.DiRightButton);
                    //if (right == 1 &&
                    //    rightButtonBefore == 0 &&
                    //    measureManger.InMeasurement == false&&
                    //    es==0)
                    //{

                    //    try
                    //    {
                    //        source = new CancellationTokenSource();
                    //        Task.Run(() =>
                    //        {
                    //            measureManger.RunMeasurementRight();
                    //        }, source.Token);
                    //    }
                    //    catch (AggregateException)//捕获紧急停止按钮
                    //    {
                    //        //MessageBox.Show("停止按钮被按下", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }

                    //}
                    //rightButtonBefore = right;

                    //if (es==1&&esButtonBefore==0)
                    //{
                    //    measureManger.InMeasurement = false;
                    //    //MessageBox.Show("停止按钮被按下", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //esButtonBefore = es;
                }
                catch (Exception)
                {

                }
                Thread.Sleep(3);
            }
            CloseCom();
        }
        /// <summary>
        /// 读取输入信息
        /// </summary>
        private void ReadInput()
        {
            ushort ioInput;
            short rtn;
            lock (cardActLock)
            {
                rtn = GT400.GT_ExInpt(out ioInput);
            }
            runningError("io输入读取", rtn);
            //固高数据为低电平为1,实际要取反
            ioInput = (ushort)~ioInput;

            mutexShMem.WaitOne();
            // 共有存储在存储器
            int ofst = DMI_OFST + 0 * Marshal.SizeOf(typeof(short));
            //将读取到的PLC数据写入到相应的内存区域
            Marshal.WriteInt16(mapAddrPtr, ofst, (short)ioInput);
            // 解除
            mutexShMem.ReleaseMutex();
        }

        public override void CloseCom()
        {
            base.CloseCom();
            try
            {
                if (getCardInfoThread != null)
                {
                    if (!getCardInfoThread.Join(1000))
                    {
                        getCardInfoThread.Abort();
                    }
                    getCardInfoThread = null;
                }
                lock (cardActLock)
                {
                    short rtn = GT400.GT_Close();
                    Error("GT_Close", rtn);
                }
                isLink = false;
            }
            catch
            {
                // ignored
            }
        }

        public override void SetDo(int ch, int data)
        {
            int idx = ch / 16;
            short rtn = 0;
            runningError("控制卡复位", rtn);

            if (idx == 0)
            {
                lock (cardActLock)
                {
                    ushort result;
                    if (data == 1)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = 1;
                    }
                    rtn = GT400.GT_ExOptBit((ushort)ch, result);
                }
                runningError("单点位输出", rtn);
            }
            base.SetDo(ch, data);
        }
        /// <summary>
        /// 设置运动速度参数,运动前更新有效
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="speed"></param>
        public void SetRunSpeed(double acc, double speed)
        {
            this.accRun = acc;
            this.speedRun = speed;
        }
        private void Error(string cmd, short rtn)
        {
            string info;

            switch (rtn)
            {
                case 0:
                    info = cmd + ":ok";
                    break;
                case 1:
                default:
                    info = cmd + ":错误 错误代码: " + rtn;
                    Util .WriteLog(this.GetType(), "运动控制卡异常:" + info);
                    break;
            }
            Util.Notify(info);
        }

        /// <summary>
        /// 运行过程中产生的错误报告
        /// </summary>
        /// <param name="cmd">运行的命令</param>
        /// <param name="rtn">命令运行结果</param>
        private void runningError(string cmd, short rtn)
        {
            string info;
            //返回0为正常值不触发提示
            switch (rtn)
            {
                case 0:
                    //info = cmd + ":成功";
                    //Notify(info);
                    break;
                case 1:
                default:
                    info = cmd + ":错误 错误代码" + rtn;
                    Util.WriteLog(this.GetType(), "运动控制卡异常:" + info);
                    Util.Notify(info);
                    break;
            }


        }

        public void EmgStop()
        {
            lock (cardActLock)
            {
                GT400.GT_AbptStp();
                //GT400.GT_EStpMtn();
            }
        }

        public void ImdStop()
        {
            lock (cardActLock)
            {
                GT400.GT_SmthStp();
            }
        }
        /// <summary>
        /// 指定轴运动到指定位置
        /// </summary>
        /// <param name="axisIndex">轴编号</param>
        /// <param name="pos">位移量</param>
        /// <param name="isAbsolute">绝对运动</param>
        public void AxisPosMove(int axisIndex, int pos, bool isAbsolute)
        {
            ushort testAxis = (ushort)axisIndex;

            if (homeFinish == false)
            {
                Util.Notify("控制卡未回原点");
                return;
            }
            lock (cardActLock)
            {
                short rtn;
                int prfPos = 0;

                rtn = GT400.GT_Axis(testAxis);

                ushort axisSts;
                rtn = GT400.GT_GetSts(out axisSts);//如果测试轴正在运动则退出

                runningError("当前轴状态状态查询", rtn);
                if (0 != (axisSts & 0x400))
                {
                    Util.Notify("轴动作中");
                    //return;
                }

                rtn = GT400.GT_ClrSts();           //清除不正常状态
                runningError("当前轴状态异常状态清除", rtn);
                if (0 != rtn)
                {

                    return;
                }

                if (isAbsolute)
                {
                    prfPos = pos;
                }
                else
                {
                    rtn = GT400.GT_GetPrfPos(out prfPos);
                    runningError("当前轴位置读取", rtn);
                    prfPos += pos;
                }
                //Thread.Sleep(2);

                //Thread.Sleep(2);
                rtn = GT400.GT_SetMAcc(accRun);
                //rtn = GT400.GT_SetAcc(accRun);
                runningError("当前轴加速度设置", rtn);
                //Thread.Sleep(2);
                rtn = GT400.GT_SetVel(speedRun);
                runningError("当前轴目标速度设置", rtn);
                //Thread.Sleep(2);
                rtn = GT400.GT_SetPos(prfPos);
                runningError("当前轴目标位置设置", rtn);

                rtn = GT400.GT_SetJerk(0.087);
                runningError("当前轴加加速度设置", rtn);
                //Thread.Sleep(2);
                rtn = GT400.GT_Update();
                runningError("当前轴参数更新", rtn);
            }
        }
        /// <summary>
        /// 运动到指定位置(绝对位置)
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public void MoveToPos(int xPos, int yPos)
        {
            if (homeFinish == false)
            {
                Util.Notify("控制卡未回原点");
                return;
            }
            MoveToPosNoWait(xPos, yPos);
            WaitAllRunFinish();
        }
        /// <summary>
        /// 到达目标位置,未等待结束
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public void MoveToPosNoWait(int xPos, int yPos)
        {
            if (homeFinish == false)
            {
                Util.Notify("控制卡未回原点");
                return;
            }
         
            AxisPosMove(1, xPos, true);
            AxisPosMove(2, yPos, true);
        }
        public void Homing(int axisIndex, int pos)
        {
            if (isLink == false)
            {
                Util.Notify("控制卡未连接");
                return;
            }
            double acc = 0, speed = 0;
            if (axisIndex == 1)
            {
                acc = accHomeX;
                speed = speedHomeX;
            }
            if (axisIndex == 2)
            {
                acc = accHomeY;
                speed = speedHomeY;
            }
            Homing(axisIndex, pos, acc, speed);
        }
        /// <summary>
        /// 指定轴回原点
        /// </summary>
        /// <param name="axisIndex"></param>
        public void Homing(int axisIndex, int pos, double acc, double speed)
        {
            short rtn;
            ushort axisSts;

            int prfPos;
            if (isLink == false)
            {
                Util.Notify("控制卡未连接");
                return;
            }
            lock (cardActLock)
            {
                Util.Notify(axisIndex + "开始回原点");
                rtn = GT400.GT_Axis((ushort)axisIndex);


                rtn = GT400.GT_GetSts(out axisSts);

                runningError("当前轴状态状态查询", rtn);
                if (0 != (axisSts & 0x400))
                {
                    Util.Notify("轴动作中");
                    return;
                }

                rtn = GT400.GT_ClrSts();           //清除不正常状态
                runningError("当前轴状态异常状态清除", rtn);
                if (0 != rtn)
                {
                    return;
                }
                //在当前位置基础上，以S曲线向前运动指定个脉冲

                rtn = GT400.GT_SetMAcc(acc);
                //rtn = GT400.GT_SetAcc(acc);
                runningError("当前轴加速度设置", rtn);
                rtn = GT400.GT_SetVel(speed);
                runningError("当前轴目标速度设置", rtn);
                rtn = GT400.GT_SetPos(pos);
                runningError("当前轴目标位置设置", rtn);
                rtn = GT400.GT_SetJerk(0.087);
                runningError("当前轴加加速度设置", rtn);
                rtn = GT400.GT_Update();
                runningError("当前轴参数更新", rtn);

                Thread.Sleep(10);

                waitCurrentRunFinish();

                rtn = GT400.GT_GetPrfPos(out prfPos);
                runningError("当前轴位置读取", rtn);

                //通过判断 pos正负来确定返回位置
                if (pos >= 0)
                {
                    prfPos -= 100;
                }
                else
                {
                    prfPos += 100;
                }

                rtn = GT400.GT_ClrSts();           //清除不正常状态
                runningError("当前轴状态异常状态清除", rtn);

                rtn = GT400.GT_SetPos(prfPos);
                runningError("当前轴目标位置设置", rtn);
                rtn = GT400.GT_Update();
                runningError("当前轴参数更新", rtn);

                //MyNotify.Notify("轴小段后退中");
                waitCurrentRunFinish();
                //MyNotify.Notify("轴小段后退完成");
                Thread.Sleep(10);

                rtn = GT400.GT_ClrSts();           //清除不正常状态
                runningError("当前轴状态异常状态清除", rtn);


                rtn = GT400.GT_SetVel(speedHomeY / 10.0);
                runningError("当前轴目标速度设置", rtn);
                rtn = GT400.GT_SetPos(pos);
                runningError("当前轴目标位置设置", rtn);
                rtn = GT400.GT_Update();
                runningError("当前轴参数更新", rtn);

                waitCurrentRunFinish();

                rtn = GT400.GT_ZeroPos();
                runningError("轴位置状态清零", rtn);
                Util.Notify(axisIndex + "轴原点回归完成");
            }
        }


        public void HomeAll()
        {
            if (isLink == false)
            {
                Util.Notify("控制卡未连接");
                return;
            }
            homeFinish = false;
            Homing(2, -2000000);
            Homing(1, -2000000);
            homeFinish = true;
        }
        /// <summary>
        /// 等待所有轴运行完成
        /// </summary>
        public void WaitAllRunFinish()
        {
            short rtn;
            lock (cardActLock)
            {
                rtn = GT400.GT_Axis(1);

                waitCurrentRunFinish();
            }
            Thread.Sleep(10);
            lock (cardActLock)
            {
                rtn = GT400.GT_Axis(2);

                waitCurrentRunFinish();
            }
        }
        private void waitCurrentRunFinish()
        {
            Thread.Sleep(2);
            //读取输入信息
            ReadInput();
            //int es = GetDi(Define.DiEmergencyStop);
            //if (es == 1)
            //{
            //    EmgStop();
            //    CancleCommPause();
            //    source.Cancel();
            //    source.Token.ThrowIfCancellationRequested();
            //}
            lock (cardActLock)
            {
                short rtn;
                ushort axisSts;
                rtn = GT400.GT_GetSts(out axisSts);
                runningError("当前轴状态状态查询", rtn);
                bool showTmp = true;
                //判断是否在运动中
                while (0 != (axisSts & 0x400))
                {
                    rtn = GT400.GT_GetSts(out axisSts);
                    runningError("当前轴状态状态查询", rtn);
                    if (showTmp)
                    {
                        showTmp = false;
                    }

                    Thread.Sleep(10);
                }
            }
            
        }
    }
}
