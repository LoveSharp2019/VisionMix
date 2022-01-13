using System;
using System.Net;
using System.Net.Sockets;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.Common.Helpers;
using Yoga.Common.TCPHelper;

namespace Yoga.VisionMix.AppManger
{
    /// <summary>
    /// 外部通信接口类 初步计划包含串口 tcp
    /// </summary>
    public class AppInterlockHelper
    {

        private static AppInterlockHelper instance;


        public event EventHandler<PortDataReciveEventArgs> PortDataReciveEvent;

        /// <summary>接收数据后的整合零时变量</summary>
        private string messageStringRecevied;
        /// <summary>接收到外部发出的一整条信息</summary>
        private string receiveCode;
        bool isLink = false;

        CommunicationParam communicationParam;
        SerialHelper serialHelper = new SerialHelper();
        TCPClientHelper tcpClientHelper;
        TCPServerHelper tcpServerHelper;

        TCPClientHelper udpClientHelper;

        TCPServerHelper udpServerHelper;
        #region 属性

        public static AppInterlockHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppInterlockHelper();
                }
                return instance;
            }
        }

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

        public CommunicationParam CommunicationParam
        {
            get
            {
                return communicationParam;
            }

            set
            {
                communicationParam = value;
            }
        }

        public SerialHelper SerialHelper
        {
            get
            {
                return serialHelper;
            }
        }

        public TCPClientHelper TcpClientHelper
        {
            get
            {
                return tcpClientHelper;
            }
        }

        public TCPServerHelper TcpServerHelper
        {
            get
            {

                return tcpServerHelper;
            }
        }

        public TCPClientHelper UdpClientHelper
        {
            get
            {
                return udpClientHelper;
            }
        }

        public TCPServerHelper UdpServerHelper
        {
            get
            {
                return udpServerHelper;
            }
        }
        #endregion

        public void Close()
        {
            if (serialHelper != null)
            {
                serialHelper.PortDataReciveEvent -= SerialHelper_PortDataReciveEvent;
                serialHelper.Close();
                serialHelper = null;

            }
            if (tcpClientHelper != null)
            {
                tcpClientHelper.ConnectedServer -= SocketClientHelper_ConnectedServer;
                tcpClientHelper.ReceivedDatagram -= SocketClientHelper_ReceivedDatagram;
                tcpClientHelper.Close();
                tcpClientHelper = null;
            }
            if (tcpServerHelper != null)
            {
                tcpServerHelper.RecvData -= SocketServerHelper_RecvData;
                tcpServerHelper.ClientConn -= SocketServerHelper_ClientConn;
                
                tcpServerHelper.CloseAllClient();
                if (tcpServerHelper.IsRun)
                {
                    tcpServerHelper.Stop();
                }
                tcpServerHelper = null;
            }

            if (udpClientHelper != null)
            {
                udpClientHelper.ConnectedServer -= SocketClientHelper_ConnectedServer;
                udpClientHelper.ReceivedDatagram -= SocketClientHelper_ReceivedDatagram;
                udpClientHelper.Close();
                udpClientHelper = null;
            }
            if (udpServerHelper != null)
            {
                udpServerHelper.RecvData -= SocketServerHelper_RecvData;
                udpServerHelper.ClientConn -= SocketServerHelper_ClientConn;
                udpServerHelper.CloseAllClient();
                udpServerHelper = null;
            }

        }

        private void SocketServerHelper_ClientConn(object sender, NetEventArgs e)
        {
            Util.Notify(string.Format("客户端{0}上线", e.Client.ClientSocket.RemoteEndPoint));

        }

        public void Open()
        {
            IsLink = false;
            Close();
            IPAddress address = null;
            ushort port = 0;
            switch (CommunicationParam.InterlockMode)
            {
                case InterlockMode.串口:
                    serialHelper = new SerialHelper();
                    serialHelper.InitSerial(CommunicationParam);
                    serialHelper.PortDataReciveEvent += SerialHelper_PortDataReciveEvent;
                    if (serialHelper.IsLink)
                    {
                        Util.Notify(string.Format("串口{0}打开完成", CommunicationParam.ComName));
                    }
                    break;
                case InterlockMode.TCP服务器:
                    if (IPAddress.TryParse(communicationParam.TcpIP, out address) && ushort.TryParse(communicationParam.TcpPort, out port))
                    {
                        tcpServerHelper = new TCPServerHelper(address, port);
                        tcpServerHelper.Start();
                        tcpServerHelper.RecvData += SocketServerHelper_RecvData;
                        tcpServerHelper.ClientConn += SocketServerHelper_ClientConn;
                        tcpServerHelper.ClientClose += SocketServerHelper_ClientClose;
                        if (tcpServerHelper.IsRun == false)
                        {
                            Util.Notify("服务器打开失败,请检查设置");
                        }
                        else
                        {
                            Util.Notify(string.Format("服务器{0}端口{1}打开完成", tcpServerHelper.ServerIP, tcpServerHelper.Port));
                        }
                    }

                    break;
                case InterlockMode.TCP客户端:
                    if (IPAddress.TryParse(communicationParam.TcpIP, out address) && ushort.TryParse(communicationParam.TcpPort, out port))
                    {
                        tcpClientHelper = new TCPClientHelper();
                        tcpClientHelper.ConnectedServer += SocketClientHelper_ConnectedServer;
                        tcpClientHelper.Connect(address.ToString(), port);
                        
                        tcpClientHelper.ReceivedDatagram += SocketClientHelper_ReceivedDatagram;
                    }
                    break;
                case InterlockMode.UDP:

                    Util.Notify("UDP服务器未完成,请修改设置");
                    return;
                    //if (IPAddress.TryParse(communicationParam.TcpIP, out address) && ushort.TryParse(communicationParam.TcpPort, out port))
                    //{
                    //    udpServerHelper = new SocketServerHelper(address, port, ProtocolType.Udp);
                    //    udpServerHelper.Start();
                    //    udpServerHelper.RecvData += SocketServerHelper_RecvData;
                    //    udpServerHelper.ClientConn += SocketServerHelper_ClientConn;
                    //    udpServerHelper.ClientClose += SocketServerHelper_ClientClose;
                    //    if (udpServerHelper.IsRun == false)
                    //    {
                    //        Util.Notify("服务器打开失败,请检查设置");
                    //    }
                    //    else
                    //    {
                    //        Util.Notify(string.Format("服务器{0}端口{1}打开完成", udpServerHelper.ServerIP, udpServerHelper.Port));
                    //    }
                    //}

                    //break;
                case InterlockMode.相机IO:
                    Util.Notify("相机输出模式开启");
                    break;
                default:
                    break;
            }
            IsLink = true;
        }

        private void SocketClientHelper_ConnectedServer(object sender, NetEventArgs e)
        {
            Util.Notify(string.Format("连接到服务器{0}",e.Client.ClientSocket.RemoteEndPoint));

        }

        private void SocketServerHelper_ClientClose(object sender, NetEventArgs e)
        {
            Util.Notify(string.Format("客户端{0}下线", e.Client.ClientSocket.RemoteEndPoint));

        }

        private void SocketClientHelper_ReceivedDatagram(object sender, NetEventArgs e)
        {
            OnDataRecive(tcpClientHelper.ReceivedData);
        }

        private void SocketServerHelper_RecvData(object sender, NetEventArgs e)
        {
            OnDataRecive(tcpServerHelper.ReceivedData);
        }

        private void SerialHelper_PortDataReciveEvent(object sender, PortDataReciveEventArgs e)
        {
            OnDataRecive(e.Data);
        }


        private void OnDataRecive(string strData)
        {
            messageStringRecevied = messageStringRecevied + strData;
            int end = messageStringRecevied.IndexOf("#");//尾巴
            while (end > -1)
            {
                string dat = messageStringRecevied.Substring(0, end);
                int st = dat.IndexOf("*");//头
                if (st > -1 && st < dat.Length - 1)
                {
                    //截取收到的数据
                    receiveCode = dat.Substring(st + 1, dat.Length - st - 1);
                    if (PortDataReciveEvent != null && receiveCode != "")
                    {
                        PortDataReciveEvent(this, new PortDataReciveEventArgs(receiveCode));
                    }
                }
                if (end == messageStringRecevied.Length - 1)
                {
                    messageStringRecevied = "";
                }
                else if (end < messageStringRecevied.Length - 1)
                {
                    //截取end以后的数据重新接收数据
                    messageStringRecevied = messageStringRecevied.Substring(end + 1, messageStringRecevied.Length - end - 1);
                }

                end = messageStringRecevied.IndexOf("#");//尾巴
            }
        }
        public void WriteData(string str)
        {
            try
            {
                string data = AddCommand(str);
                switch (CommunicationParam.InterlockMode)
                {
                    case InterlockMode.串口:
                        SerialHelper.WriteDataToSerial(data);
                        break;
                    case InterlockMode.TCP服务器:
                        if (TcpServerHelper != null && TcpServerHelper.IsRun)
                        {
                            foreach (Session s in TcpServerHelper.SessionTable.Values)
                            {
                                TcpServerHelper.SendText(s, data);
                            }
                        }
                        break;
                    case InterlockMode.TCP客户端:
                        {
                            TcpClientHelper.SendText(data);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("数据发送异常");
            }
        }
        /// <summary>
        /// 对将要发送的字符串添加标志字符 aa=>*aa#
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string AddCommand(string str)                        //对指令及数据的字符串进行最终FCS运算，并添加“*CR”
        {
            string result = string.Format("*{0}#", str);
            return result;
        }
    }
}
