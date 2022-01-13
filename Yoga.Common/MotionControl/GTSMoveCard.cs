using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yoga.Common.MotionControl
{
    public class GTSMoveCard : MoveCard
    {
        /// <summary>类的实例</summary>
        protected static GTSMoveCard instance = null;


        public static GTSMoveCard Instance
        {
            get
            {
                if (instance == null)
                    instance = new GTSMoveCard();
                return instance;
            }
        }
        public override bool InitCard()
        {
            try
            {
                short sRtn;

                sRtn = gts.mc.GT_Open(0, 1);//打开运动控制卡
                RunningError("打开运动控制卡", sRtn);


                sRtn = gts.mc.GT_Reset();
                RunningError("控制卡复位", sRtn);

                sRtn = gts.mc.GT_LoadConfig("myGTS800.cfg");//下载配置文件
                RunningError("下载配置文件", sRtn);

                Thread.Sleep(100);
                sRtn = gts.mc.GT_ClrSts(1, 8);//清除各轴报警和限位
                RunningError("清除各轴报警和限位", sRtn);
                for (int i = 0; i < axisNum; i++)
                {
                    short axis = (short)(i + 1);
                    sRtn = gts.mc.GT_AxisOn(axis);//上伺服
                    RunningError(string.Format("轴{0}伺服开", axis), sRtn);
                    if (sRtn != 0)
                    {
                        return false;
                    }
                    gts.mc.TTrapPrm trap;
                    trap.acc = 0.5;
                    trap.dec = 0.5;
                    trap.smoothTime = 0;
                    trap.velStart = 0;

                    sRtn = gts.mc.GT_SetTrapPrm(axis, ref trap);//设置点位运动参数
                    RunningError(string.Format("轴{0}点位运动参数", axis), sRtn);

                    // 设置轴到位误差带
                    sRtn = gts.mc.GT_SetAxisBand(axis, 20, 5);
                }

                isLink = true;
                return true;
            }
            catch (Exception ex)
            {
                Util.Notify("控制卡打开失败:" + ex.Message);
                //Notify("控制卡打开失败 错误原因" + ex.Message);
                CloseCom();
                isLink = false;
                return false;
            }
        }

        public override void RunningError(string act, short status)
        {
            // 如果指令执行返回值为非0，说明指令执行错误，向屏幕输出错误结果
            if (status != 0)
            {
                string message = string.Format("运动控制异常{0}={1}", act, status);
                throw new Exception(message);
            }
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
                    ushort ioInput;
                    short rtn;
                    int lGpiValue;
                    rtn = gts.mc.GT_GetDi(gts.mc.MC_GPI, out lGpiValue);
                    RunningError("io输入读取", rtn);
                    ioInput = (ushort)lGpiValue;
                    //固高数据为低电平为1,实际要取反
                    ioInput = (ushort)~ioInput;

                    mutexShMem.WaitOne();
                    // 共有存储在存储器
                    int ofst = DMI_OFST + 0 * Marshal.SizeOf(typeof(short));
                    //将读取到的控制卡数据写入到相应的内存区域
                    Marshal.WriteInt16(mapAddrPtr, ofst, (short)ioInput);
                    // 解除
                    mutexShMem.ReleaseMutex();

                    TrigerInputReadedEvent();
                    #endregion
                    Thread.Sleep(3);
                    #region OUT写入
                    //// 锁定,从共享存储器获取要写入plc的数据
                    mutexShMem.WaitOne();
                    short data = new short();
                    ofst = DMO_OFST;

                    data = Marshal.ReadInt16(mapAddrPtr, ofst);
                    // 解除
                    mutexShMem.ReleaseMutex();
                    //状态取反后输出
                    ushort ioOutput = (ushort)(~data);

                    rtn = gts.mc.GT_SetDo(gts.mc.MC_GPO, ioOutput);
                    RunningError("io输出", rtn);
                    #endregion

                    commWaitResetEvent.WaitOne();
                    Thread.Sleep(3);
                    #region 轴状态获取

                    ushort[] status = new ushort[axisNum];
                    int[] pos = new int[axisNum];
                    for (int i = 0; i < axisNum; i++)
                    {
                        uint clk;
                        int psts;
                        rtn = gts.mc.GT_GetSts((short)(i + 1), out psts, 1, out clk);
                        RunningError(string.Format("轴{0}状态查询", (short)(i + 1)), rtn);
                        status[i] = (ushort)psts;

                        double posTmp;


                        rtn = gts.mc.GT_GetAxisPrfPos((short)(i + 1), out posTmp, 1, out clk);
                        pos[i] = (int)posTmp;
                        RunningError(string.Format("轴{0}位置数据读取", (short)(i + 1)), rtn);
                        Thread.Sleep(3);
                    }
                    //Util.Notify("轴1位置" + pos[0]);
                    int count = 0;
                    mutexShMem.WaitOne();
                    for (int i = 0; i < axisNum; i++)
                    {                      
                        // 共有存储在存储器
                        ofst = DMI_OFST + (1 + i) * Marshal.SizeOf(typeof(short));
                        //轴状态
                        Marshal.WriteInt16(mapAddrPtr, ofst, (short)status[i]);


                        //位置信息
                        ofst = DMI_OFST + (DMO_A_START + count) * Marshal.SizeOf(typeof(int));

                        count++;
                        Marshal.WriteInt32(mapAddrPtr, ofst, pos[i]);
                        ofst = DMI_OFST + (DMO_A_START + count) * Marshal.SizeOf(typeof(int));
                        count++;
                       
                    }
                    // 解除
                    mutexShMem.ReleaseMutex();
                    #endregion
                    Thread.Sleep(3);

                    commWaitResetEvent.WaitOne();
                }
                catch (Exception ex)
                {
                    Util.Notify("控制卡通信失败" + ex.Message);

                    CloseCom();
                    isLink = false;
                }
                Thread.Sleep(3);
            }
            CloseCom();
        }

        public override void CloseCom()
        {
            base.CloseCom();
            try
            {
                short rtn = gts.mc.GT_Close();
                RunningError("关闭控制卡", rtn);
                isLink = false;
            }
            catch
            {
                // ignored
            }
        }

        public override void AxisPosMove(int axisIndex, int pos, double speed, bool isAbsolute)
        {
            short testAxis = (short)axisIndex;

            short sRtn;
            int prfPos = 0;
            try
            {
                sRtn = gts.mc.GT_ClrSts(1, 8);//清除各轴报警和限位
                RunningError("轴状态清除", sRtn);
                sRtn = gts.mc.GT_SetVel(testAxis, speed);//设置目标速度
                RunningError(string.Format("轴{0}目标速度设置", testAxis), sRtn);

                if (isAbsolute)
                {
                    prfPos = pos;
                }
                else
                {
                    double pos1 = 0;
                    uint clk;
                    sRtn = gts.mc.GT_GetPrfPos(testAxis, out pos1, 1, out clk);
                    RunningError("当前轴位置读取", sRtn);
                    prfPos = pos + (int)pos1;
                }
                //Thread.Sleep(2);
                sRtn = gts.mc.GT_SetPos(testAxis, prfPos);//设置目标位置
                RunningError(string.Format("轴{0}目标位置设置", testAxis), sRtn);
                sRtn = gts.mc.GT_Update(1 << (testAxis - 1));//更新轴运动
                RunningError(string.Format("轴{0}状态更新", testAxis), sRtn);
            }
            catch (Exception ex)
            {
                Util.Notify("控制卡位置运动异常" + ex.Message);
                CloseCom();
                isLink = false;
            }
        }

        public override void Homing(short axisIndex, int pos)
        {
            if (isLink == false)
            {
                Util.Notify("控制卡未连接");
                return;
            }

            double speed = 10;
            try
            {
                //去一个端点
                AxisPosMove(axisIndex, pos, speed, true);
               // Util.Notify(string.Format("轴{0}去端点", axisIndex));

                WaitMove2End(axisIndex);
               // Util.Notify(string.Format("轴{0}到达端点", axisIndex));
                double pos1;
                uint clk;
                gts.mc.GT_GetPrfPos(axisIndex, out pos1, 1, out clk);
                //后退小段
                if (pos > 0)
                {
                    pos1 -= 5000;
                }
                else
                {
                    pos1 += 5000;
                }

                AxisPosMove(axisIndex, (int)pos1, speed, true);
                Thread.Sleep(1000);

                WaitMoved(axisIndex);

               // Util.Notify(string.Format("轴{0}后退完成", axisIndex));
                //慢速到达端点
                AxisPosMove(axisIndex, pos, 10.0 / 15.0, true);
                Thread.Sleep(100);
                WaitMoved(axisIndex);
                //轴状态清零
                gts.mc.GT_ZeroPos(1, 8);

                Util.Notify(string.Format("轴{0}原点回归完成", axisIndex));
            }
            catch (Exception ex)
            {
                Util.Notify("控制卡原点回归异常" + ex.Message);

                CloseCom();
                isLink = false;
            }
        }
        public override void WaitMove2End(short axis)
        {
            short sRtn;
            int sts;
            uint clk;
            Thread.Sleep(1000);
            bool isOk = false;
            // 等待轴进入误差带
            do
            {
                sRtn = gts.mc.GT_GetSts(axis, out sts, 1, out clk);
                RunningError(string.Format("轴{0}状态查询", axis), sRtn);
                if (0x40 == (sts & 0x40) || 0x20 == (sts & 0x20))//电机到端点
                {
                    isOk = true;
                }
                Thread.Sleep(100);//降低刷新速率
            } while (isOk == false);
        }
        public override void WaitMoved(short axis)
        {
            short sRtn;
            int sts;
            uint clk;
            bool isOk = false;
            // 等待轴进入误差带
            do
            {
                sRtn = gts.mc.GT_GetSts(axis, out sts, 1, out clk);
                RunningError(string.Format("轴{0}状态查询", axis), sRtn);

                if (0x800 == (sts & 0x800))//电机到位
                {
                    isOk = true;
                }
                Thread.Sleep(100);//降低刷新速率
            } while (isOk == false);
        }
    }
}
