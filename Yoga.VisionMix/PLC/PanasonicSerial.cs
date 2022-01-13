using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yoga.Common.Basic;

namespace Yoga.VisionMix.PLC
{
    public class PanasonicSerial
    {
        private SerialPort com = new SerialPort();

        public  int[] DTValue = null;
        private bool isReadOK = false;

        private object portLockObj = new object();


        private static PanasonicSerial instance;
        public static PanasonicSerial Instance
        {
            get
            {
                if(instance==null)
                {
                    instance = new PanasonicSerial();
                }
                return instance;
            }
        }
        public bool IsReadOK
        {
            get
            {
                return isReadOK;
            }

            set
            {
                isReadOK = value;
            }
        }

        /// <summary>
        /// 对指令及数据的字符串进行最终FCS运算，并添加“*CR”
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        private string Command(string Str)                        //对指令及数据的字符串进行最终FCS运算，并添加“*CR”
        {
            char[] chararraytemp = Str.ToCharArray();
            int fcs = 0;
            foreach (char chartemp in chararraytemp)
            {
                fcs = fcs ^ Convert.ToInt16(chartemp);           //对字符进行ASC转换，并进行异或运算
            }
            string fcstohex = String.Format("{0:X2}", (uint)System.Convert.ToUInt32(fcs));
            string result = Str + fcstohex + "*" + Convert.ToChar(13);
            return result;
        }

        /// <summary>
        /// 串口初始化
        /// </summary>
        public void InitSerial(CommunicationParam rs232Param)
        {
            try
            {
                if (com.IsOpen)
                {
                    Close();
                }

                com.PortName = rs232Param.ComName ;
                com.BaudRate = Convert.ToInt32(rs232Param.BaudRate);
                com.Parity = (Parity)Convert.ToInt32(rs232Param.Parity);
                com.DataBits = Convert.ToInt32(rs232Param.DataBits);
                com.StopBits = (StopBits)Convert.ToInt32(rs232Param.StopBits); 
                //com.NewLine = "\r\n";
                com.NewLine = "\r";
                isReadOK = false;
                com.DataReceived += new SerialDataReceivedEventHandler(this.OnDataReceived);

                com.Open();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("串口打开失败," +ex.Message);
            }
        }
        /// <summary>
        /// 写入数据到串口
        /// </summary>
        /// <param name="str">待写入字符串-无校验</param>
        /// <returns></returns>
        public bool WriteDataToSerial(string str)
        {
            bool writeFlag = false;
            try
            {
                if (!com.IsOpen)
                {
                    InitSerial(AppManger.ProjectData.Instance.CommunicationParam);
                    //com.Open();
                }
                isReadOK = false;
                com.Write(str);
                writeFlag = true;
            }
            catch
            {
                throw new ApplicationException("串口设置异常");
            }
            return writeFlag;
        }
       

        /// <summary>
        /// 串口接收到数据引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (portLockObj)
            {
                int i = 0;
                string readText = "";
                Thread.Sleep(1);
                readText = com.ReadLine();
                if (readText.Length<6)
                {
                    return;
                }
                string sHead = readText.Substring(0, 4);//"%01$;
                string sComm = readText.Substring(4, 2);//"RC/WC/RD/WD
                string sValues = "";
                string tValue = "";
                switch (sHead)
                {
                    case "%01$": //读写成功
                        {
                            switch (sComm)
                            {
                                case "RD"://读取WD,转换为十进制
                                    {
                                        sValues = readText.Substring(6, readText.Length - 8);
                                        DTValue = new int[sValues.Length / 4];
                                        for (i = 0; i < (sValues.Length / 4); i++)
                                        {
                                            tValue = sValues.Substring(i * 4, 4);
                                            DTValue[i] = Convert.ToInt32(tValue.Substring(2, 2) + tValue.Substring(0, 2), 16) ;
                                        }
                                        isReadOK = true;
                                        break;
                                    }
                                case "WD":
                                    {
                                        isReadOK = true;
                                       
                                        //写入DT成功,没返回值!
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case "%01!": //读写失败
                        {
                            //返回错误
                            break;
                        }
                }
            }
        }
        public void Close()
        {
            if (com.IsOpen)
                com.Close();
            com.DataReceived -= new SerialDataReceivedEventHandler(this.OnDataReceived);
        }
        public void DiscardInBuffer()
        {
            if (!com.IsOpen)
                com.Open();
            com.DiscardInBuffer();
        }
        /// <summary>
        /// writeDT(string DTaddr1,string DTaddr2, int value)
        /// DTaddr1:寄存器起始地址
        /// DTaddr2:寄存器结束地址
        /// value=数值;
        /// 如:WriteDT("D1","D4",arrValue); short[] arrValue={100,233,220,4355};
        /// </summary>
        public void WriteDT(string DTaddr1, string DTaddr2, short[] value)
        {
            //发送:%01%WDD00001000030500071500095D[CR]
            //-------------地址转换---------------------
            string sReg = DTaddr1.Substring(0, 1); //获取"D"
            string sA1 = DTaddr1.Substring(1, DTaddr1.Length - 1); //DT1中的1
            string sA2 = DTaddr2.Substring(1, DTaddr2.Length - 1); //DT4中的4
            int iLen = Convert.ToInt16(sA2) - Convert.ToInt16(sA1) + 1; //4-1
            sA1 = sA1.PadLeft(5).Replace(" ", "0");
            sA2 = sA2.PadLeft(5).Replace(" ", "0");
            string sAddr = sReg + sA1 + sA2; //地址:D0000100004
            //-------------数值转换---------------------
            string sValues = "";
            string sValue = "";
            for (int i = 0; i < iLen; i++)
            {
                sValue = value[i].ToString();
                sValue = Convert.ToInt32(sValue).ToString("X4");
                sValue = sValue.Substring(2, 2) + sValue.Substring(0, 2);
                sValues += sValue;
            }
            //------------------------------------------
            string outStr = "";
            outStr = "%01#WD" + sAddr + sValues;
            outStr = outStr + bcc(outStr) + "\r";
            WriteDataToSerial(outStr);
            return;
        }
        /// <summary>
        /// 本命令只是发送，取值，取值在OnDataReceived里
        /// readDT(string DTaddr1, string DTaddr2)
        /// DTaddr1:寄存器起始地址
        /// DTaddr2:寄存器结束地址
        /// 如:readDT("D1","D4");
        /// </summary>
        /// <param name="chkString"></param>
        /// <returns></returns>
        public void ReadDT(string DTaddr1, string DTaddr2)
        {            //-------------地址转换---------------------
            string sReg = DTaddr1.Substring(0, 1); //获取"D"
            string sA1 = DTaddr1.Substring(1, DTaddr1.Length - 1); //DT1中的1
            string sA2 = DTaddr2.Substring(1, DTaddr2.Length - 1); //DT4中的4
            int iLen = Convert.ToInt16(sA2) - Convert.ToInt16(sA1) + 1; //4-1
            sA1 = sA1.PadLeft(5).Replace(" ", "0");
            sA2 = sA2.PadLeft(5).Replace(" ", "0");
            string sAddr = sReg + sA1 + sA2; //地址:D0000100004
            //------------------------------------------
            string outStr = "";
            outStr = "%01#RD" + sAddr;
            outStr = outStr + bcc(outStr) + "\r";
            WriteDataToSerial(outStr);
            return;
        }
        /// <summary>
        /// writeDT(string DTaddr1,string DTaddr2, int value)
        /// DTaddr1:寄存器起始地址
        /// DTaddr2:寄存器结束地址
        /// value=数值;
        /// 如:WriteDT("D1","D4",arrValue); int[] arrValue={100,233,220,4355};
        /// </summary>
        public void WriteDT(string DTaddr1, string DTaddr2, int[] value)
        {
            //发送:%01%WDD00001000030500071500095D[CR]
            //-------------地址转换---------------------
            string sReg = DTaddr1.Substring(0, 1); //获取"D"
            string sA1 = DTaddr1.Substring(1, DTaddr1.Length - 1); //DT1中的1
            string sA2 = DTaddr2.Substring(1, DTaddr2.Length - 1); //DT4中的4
            int iLen = Convert.ToInt16(sA2) - Convert.ToInt16(sA1) + 1; //4-1
            sA1 = sA1.PadLeft(5).Replace(" ", "0");
            sA2 = sA2.PadLeft(5).Replace(" ", "0");
            string sAddr = sReg + sA1 + sA2; //地址:D0000100004
            //-------------数值转换---------------------
            string sValues = "";
            string sValue = "";
            for (int i = 0; i < iLen/2; i++)
            {
                sValue = value[i].ToString();
                sValue = Convert.ToInt32(sValue).ToString("X8");
                string sValue1 = sValue.Substring(6, 2) + sValue.Substring(4, 2)+
                    sValue.Substring(2, 2) + sValue.Substring(0, 2)
                   ;
                sValues += sValue1;
            }
            //------------------------------------------
            string outStr = "";
            outStr = "%01#WD" + sAddr + sValues;
            outStr = outStr + bcc(outStr) + "\r";
            WriteDataToSerial(outStr);
            ReceiveFinsResponse();

        }
        public string ReceiveFinsResponse()
        {
            int timeout = 0;            // 超时计数器

            while (true)
            {
                if (IsReadOK == true)
                {
                    Common.Util.Notify("plc数据写入成功");
                    return "";
                }
                // 超时确认
                if (++timeout > 10)
                {

                    Common.Util.Notify("PLC : 接收数据: 超时");
                    throw new Exception("PLC: 接收数据: 超时");
                }

                Thread.Sleep(100);
            }
        }
        private string bcc(string chkString)
        {
            int chkSum = 0;
            string chkSums = "";
            int k;
            for (k = 0; k < chkString.Length; k++)
            {
                chkSum = chkSum ^ Asc(chkString.Substring(k, 1));
            }
            chkSums = Convert.ToString(chkSum, 16);
            return chkSums.Substring(chkSums.Length - 2, 2).ToUpper();
        }

        private int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }
        }
    }
}
