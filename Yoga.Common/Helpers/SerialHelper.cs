using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoga.Common.Basic;

namespace Yoga.Common.Helpers
{
    public class PortDataReciveEventArgs : EventArgs
    {
        public PortDataReciveEventArgs(string data)
        {
            this.data = data;
        }

        private string data;

        public string Data
        {
            get { return data; }
        }
    }

    public class SerialHelper
    {
        private SerialPort com = new SerialPort();
        bool isLink = false;
        public event EventHandler<PortDataReciveEventArgs> PortDataReciveEvent;


        public bool IsLink
        {
            get
            {
                return isLink;
            }

            set
            {
                isLink = value;
            }
        }

        public SerialPort Com
        {
            get
            {
                if (com==null)
                {
                    com = new SerialPort();
                }
                return com;
            }

            set
            {
                com = value;
            }
        }

        /// <summary>
        /// 串口初始化
        /// </summary>
        public void InitSerial(CommunicationParam rs232Param)
        {
            try
            {
                if (Com.IsOpen)
                {
                    Close();
                    IsLink = false;
                }

                Com.PortName = rs232Param.ComName;
                Com.BaudRate = Convert.ToInt32(rs232Param.BaudRate);
                Com.Parity = (Parity)Convert.ToInt32(rs232Param.Parity);
                Com.DataBits = Convert.ToInt32(rs232Param.DataBits);
                Com.StopBits = (StopBits)Convert.ToInt32(rs232Param.StopBits);
                //com.NewLine = "\r\n";
                //com.NewLine = "\r";
                Com.DataReceived += new SerialDataReceivedEventHandler(this.OnDataReceived);

                Com.Open();
                IsLink = true;
               
            }
            catch (Exception ex)
            {
                IsLink = false;
                throw new ApplicationException("外部通信串口打开失败," + ex.Message);
            }
        }
        /// <summary>
        /// 写入数据到串口
        /// </summary>
        /// <param name="str">待写入字符串-无校验</param>
        /// <returns></returns>
        public void WriteDataToSerial(string str)
        {
            try
            {
                if (Com.IsOpen == false)
                {
                    throw new ApplicationException("串口未打开");
                }
                Com.Write(str);
            }
            catch
            {
                IsLink = false;
                throw new ApplicationException("串口设置异常");
            }
        }
        //private object portLockObj = new object();
        /// <summary>
        /// 串口接收到数据引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("串口反馈线程编号为:" + System.Threading.Thread.CurrentThread.ManagedThreadId);
            string strdata = "";
            strdata = Com.ReadExisting();

            if (PortDataReciveEvent != null && strdata.Length>0)
            {
                PortDataReciveEvent(this, new PortDataReciveEventArgs(strdata));
            }
        }
        public void Close()
        {
            if (Com != null && Com.IsOpen)
                Com.Close();
            Com = null;
            IsLink = false;
        }
    }
}
