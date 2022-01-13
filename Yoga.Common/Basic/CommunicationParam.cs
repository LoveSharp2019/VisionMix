using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Yoga.Common.Helpers;

namespace Yoga.Common.Basic
{
    /// <summary>
    /// 通信模式
    /// </summary>
    public enum InterlockMode
    {
        [EnumDescription("串口")]
        串口,
        [EnumDescription("TCP服务器")]
        TCP服务器,
        [EnumDescription("TCP客户端")]
        TCP客户端,
        [EnumDescription("UDP")]
        UDP,
        [EnumDescription("相机IO")]
        相机IO
    }

    [Serializable]
    public class CommunicationParam
    {
        public string ComName = "COM1";
        public string BaudRate = "9600";
        public string Parity = "0";
        public string DataBits = "8";
        public string StopBits = "1";

        public int[] PlcData = new int[5];

        public bool Use = false;

        public string TcpIP = IPAddress.Any.ToString();
        public string TcpPort = "5000";
        public InterlockMode InterlockMode = InterlockMode.串口;

        public bool IsExtTrigger = false;
    }
}
