using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Yoga.Common.TCPHelper
{
    public delegate void SocketErrorEvent(object sender, NetEventArgs e, SocketError ErrorCode);
    /// <summary> 
    /// 提供Tcp网络连接服务的客户端类 
    /// 
    /// 原理: 
    /// 1.使用异步Socket通讯与服务器按照一定的通讯格式通讯,请注意与服务器的通 
    /// 讯格式一定要一致,否则可能造成服务器程序崩溃,整个问题没有克服,怎么从byte[] 
    /// 判断它的编码格式 
    /// 2.支持带标记的数据报文格式的识别,以完成大数据报文的传输和适应恶劣的网 
    /// 络环境. 
    /// </summary> 
    public class TCPClientHelper
    {
        #region 字段

        /// <summary> 
        /// 客户端与服务器之间的会话类 
        /// </summary> 
        private Session _session;

        /// <summary> 
        /// 客户端是否已经连接服务器 
        /// </summary> 
        private bool _isConnected = false;

        /// <summary> 
        /// 接收数据缓冲区大小64K 
        /// </summary> 
        public const int DefaultBufferSize = 4 * 1024 * 1024;

        /// <summary> 
        /// 报文解析器 
        /// </summary> 
        private DatagramResolver _resolver;

        /// <summary> 
        /// 接收数据缓冲区 
        /// </summary> 
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];
        /// <summary>
        /// 客户端收到的数据
        /// </summary>
        private string _receivedData;
        #endregion

        #region 事件定义

        //需要订阅事件才能收到事件的通知，如果订阅者退出，必须取消订阅 

        /// <summary> 
        /// 已经连接服务器事件 
        /// </summary> 
        public event NetEvent ConnectedServer;

        /// <summary> 
        /// 错误事件 
        /// </summary> 
        public event SocketErrorEvent ErrorEvent;
        /// <summary> 
        /// 发送数据报文完成事件 
        /// </summary> 
        public event NetEvent DataSended;

        /// <summary> 
        /// 接收到数据报文事件 
        /// </summary> 
        public event NetEvent ReceivedDatagram;

        /// <summary> 
        /// 连接断开事件 
        /// </summary> 
        public event NetEvent DisConnectedServer;
        #endregion

        #region 属性
        /// <summary> 
        /// 返回客户端与服务器之间的会话对象 
        /// </summary> 
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary> 
        /// 返回客户端与服务器之间的连接状态 
        /// </summary> 
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        /// <summary> 
        /// 数据报文分析器 
        /// </summary> 
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary> 
        /// 编码解码器 
        /// </summary> 
        private Coder _coder;
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }
        public string ReceivedData
        {
            get
            {
                return _receivedData;
            }
        }
        #endregion

        #region 构造函数

        /// <summary> 
        /// 默认构造函数,使用默认的编码格式 
        /// </summary> 
        public TCPClientHelper()
        {
            _coder = new Coder(Coder.EncodingMothord.Default);
        }
        #endregion
        #region 公有方法
        /// <summary> 
        /// 连接服务器 
        /// </summary> 
        /// <param name="ip">服务器IP地址</param> 
        /// <param name="port">服务器端口</param> 
        public virtual void Connect(string ip, int port)
        {
            if (IsConnected)
            {
                //重新连接 
                Debug.Assert(_session != null);
                Close();
            }
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);

        }

        /// <summary> 
        /// 发送数据报文 
        /// </summary> 
        /// <param name="datagram"></param> 
        public virtual void SendText(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("没有连接服务器，不能发送数据"));
            }
            try
            {
                //获得报文的编码字节 
                SocketError ErrorCode = new SocketError();
                byte[] data = System.Text.Encoding.Default.GetBytes(datagram);
                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, out ErrorCode, new AsyncCallback(SendDataEnd), _session.ClientSocket);
                if (ErrorCode != SocketError.Success)
                {
                    //产生发送错误事件
                    this.ErrorSocket(ErrorCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void SendBytes(byte[] data)
        {
            if (data.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("没有连接服务器，不能发送数据"));
            }
            try
            {
                _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
                    new AsyncCallback(SendDataEnd), _session.ClientSocket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 
        /// 关闭连接 
        /// </summary> 
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }
            _session.Close();
            _session = null;
            _isConnected = false;
        }

        #endregion

        #region 受保护方法
        /// <summary> 
        /// 数据发送错误处理函数 
        /// </summary> 
        /// <param name="iar"></param> 
        /// <summary> 
        protected virtual void ErrorSocket(SocketError ErrorCode)
        {
            if (ErrorEvent != null)
            {
                ErrorEvent(this, new NetEventArgs(_session), ErrorCode);
            }
        }
        /// 数据发送完成处理函数 
        /// </summary> 
        /// <param name="iar"></param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            Debug.Assert(sent != 0);
            if (DataSended != null)
            {
                DataSended(this, new NetEventArgs(_session));
            }
        }

        /// <summary> 
        /// 建立Tcp连接后处理过程 
        /// </summary> 
        /// <param name="iar">异步Socket</param> 
        protected virtual void Connected(IAsyncResult iar)
        {
            try
            {
                Socket socket = (Socket)iar.AsyncState;
                socket.EndConnect(iar);
                //创建新的会话 
                _session = new Session(socket);
                _isConnected = true;
                //触发连接建立事件 
                if (ConnectedServer != null)
                {
                    ConnectedServer(this, new NetEventArgs(_session));
                }
                //建立连接后应该立即接收数据 
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
                    DefaultBufferSize, SocketFlags.None,
                    new AsyncCallback(RecvData), socket);
            }
            catch (SocketException ex)
            {
                //产生发送错误事件 
                this.ErrorSocket((SocketError)ex.ErrorCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 
        /// 数据接收处理函数 
        /// </summary> 
        /// <param name="iar">异步Socket</param> 
        protected virtual void RecvData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            try
            {
                int recv = remote.EndReceive(iar);
                //正常的退出 
                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                    return;
                }
                _receivedData = _coder.GetEncodingString(_recvDataBuffer, 0, recv);

                //通过事件发布收到的报文 
                if (ReceivedDatagram != null)
                {
                    ICloneable copySession = (ICloneable)_session;
                    Session clientSession = (Session)copySession.Clone();
                    clientSession.Datagram = new byte[recv];
                    Array.Copy(_recvDataBuffer, 0, clientSession.Datagram, 0, recv);
                    ReceivedDatagram(this, new NetEventArgs(clientSession));
                }//end of if(ReceivedDatagram != null) 

                //继续接收数据 
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                    new AsyncCallback(RecvData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                //客户端退出 
                if (10054 == ex.ErrorCode)
                {
                    //服务器强制的关闭连接，强制退出 
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                else
                {
                    //产生发送错误事件 
                    this.ErrorSocket((SocketError)ex.ErrorCode);
                }
            }
            catch (ObjectDisposedException ex)
            {
                //这里的实现不够优雅 
                //当调用CloseSession()时,会结束数据接收,但是数据接收 
                //处理中会调用int recv = client.EndReceive(iar); 
                //就访问了CloseSession()已经处置的对象 
                //我想这样的实现方法也是无伤大雅的. 
                if (ex != null)
                {
                    ex = null;
                    //DoNothing; 
                }
            }
        }
        #endregion
    }
}
