using System;
using System.Runtime.InteropServices;
using System.Threading;
using Yoga.Common;

namespace Yoga.VisionMix.PLC
{
    /// <summary>
    /// plc通信处理
    /// </summary>
    public class PanasonicPLCComm : Common.MotionControl.CommBase
    {
        protected static PanasonicPLCComm instance = null;

        private PanasonicSerial comChannel;
        public static PanasonicPLCComm Instance
        {
            get
            {
                if (instance == null)
                    instance = new PanasonicPLCComm();
                return instance;
            }
        }
        protected override void ProcDataDeal()
        {
            if (comChannel == null)
            {
                comChannel = new PanasonicSerial();
            }
            try
            {
                comChannel.InitSerial(AppManger.ProjectData.Instance.CommunicationParam);
            }
            catch (Exception ex)
            {
                if (comChannel != null)
                {
                    comChannel.Close();
                    comChannel = null;
                }
                StatusManger statusManger = StatusManger.Instance;
                statusManger.RuningStatus = RuningStatus.系统异常;
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("PLC : 初始化失败: " + ex.Message);
                if (comChannel == null)
                    return;
                try
                {
                    while (isLink)
                    {
                        #region 读plc数据流程
                        {

                            comChannel.ReadDT("D10", "D40");
                            // 待机
                            Thread.Sleep(10);

                            // 数据接收
                            comChannel.ReceiveFinsResponse();
                            if (comChannel.IsReadOK)
                            {
                                // 锁定
                                mutexShMem.WaitOne();

                                // 读取数据写入共享内存区域
                                int length = comChannel.DTValue.Length;
                                if (length < (DMI_SIZE))
                                {
                                    throw new ArgumentOutOfRangeException("收到数据长度异常");
                                }
                                for (int i = 0; i < DMI_SIZE; i++)
                                {
                                    // 共有存储在存储器  16位eg:abcd
                                    int ofst = DMI_OFST + i * Marshal.SizeOf(typeof(short));
                                    short value = (short)(comChannel.DTValue[i]);
                                    //将读取到的PLC数据写入到相应的内存区域
                                    Marshal.WriteInt16(mapAddrPtr, ofst, value);
                                }

                                // 解除
                                mutexShMem.ReleaseMutex();

                            }
                        }
                        #endregion
                        Thread.Sleep(8);
                        #region 写数据到plc
                        {

                            // 锁定
                            mutexShMem.WaitOne();

                            // 从共享存储器获取要写入plc的数据
                            short[] data = new short[DMO_SIZE];

                            for (int i = 0; i < data.GetLength(0); i++)
                            {
                                int ofst = DMO_OFST + i * Marshal.SizeOf(typeof(short));

                                data[i] = Marshal.ReadInt16(mapAddrPtr, ofst);
                            }

                            // 解除
                            mutexShMem.ReleaseMutex();

                            comChannel. WriteDT("D0", "D10", data);

                            // 待机
                            Thread.Sleep(1);

                            // 等待接收  --接收数据校验
                            comChannel.ReceiveFinsResponse();

                            #endregion
                        Thread.Sleep(8);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    Util.WriteLog(this.GetType(), ex1);
                    Util.Notify("串口通信异常");
                }
                finally
                {
                    if (comChannel != null)
                        comChannel.Close();
                }
            }

        }
      
        public override void CloseCom()
        {
            base.CloseCom();
            if (comChannel != null)
            {
                comChannel.Close();
            }
        }
    }
}
