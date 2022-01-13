using System;
using System.Text;

namespace Yoga.Common.TCPHelper
{
    /// <summary> 
    /// 通讯编码格式提供者,为通讯服务提供编码和解码服务 
    /// 可以在继承类中定制自己的编码方式如:数据加密传输等 
    /// </summary> 
    public class Coder
    {
        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
        }
        /// <summary> 
        /// 编码方式 
        /// </summary> 
        private EncodingMothord _encodingMothord;

        protected Coder()
        {

        }

        public Coder(EncodingMothord encodingMothord)
        {
            _encodingMothord = encodingMothord;
        }



        /// <summary> 
        /// 通讯数据解码 
        /// </summary> 
        /// <param name="dataBytes">需要解码的数据</param> 
        /// <returns>编码后的数据</returns> 
        public virtual string GetEncodingString(byte[] dataBytes, int start, int size)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(dataBytes, start, size);
                        //return Encoding.Unicode.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(dataBytes, start, size);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(dataBytes, start, size);
                    }
                default:
                    {
                        Util.WriteLog(this.GetType(), "未定义的编码格式");
                        throw (new Exception("未定义的编码格式"));
                    }
            }

        }
        /// <summary> 
        /// 数据编码 
        /// </summary> 
        /// <param name="datagram">需要编码的报文</param> 
        /// <returns>编码后的数据</returns> 
        public virtual byte[] GetTextBytes(string datagram)
        {
            byte[] rbyte = new byte[Encoding.UTF8.GetBytes(datagram).Length];
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        Encoding.Default.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.Unicode:
                    {
                        Encoding.Unicode.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.UTF8:
                    {
                        Encoding.UTF8.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                case EncodingMothord.ASCII:
                    {
                        Encoding.ASCII.GetBytes(datagram, 0, datagram.Length, rbyte, 0);
                        return rbyte;
                    }
                default:
                    {
                        Util.WriteLog(this.GetType(), "未定义的编码格式");
                        throw (new Exception("未定义的编码格式"));
                        
                    }
            }

        }
    }
}

