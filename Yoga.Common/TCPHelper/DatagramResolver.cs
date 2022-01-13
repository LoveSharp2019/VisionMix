using System;
using System.Collections;

namespace Yoga.Common.TCPHelper
{
    /// <summary> 
    /// 数据报文分析器,通过分析接收到的原始数据,得到完整的数据报文. 
    /// 继承该类可以实现自己的报文解析方法. 
    /// 当前没有设定报文的解析方法
    /// </summary> 
    public class DatagramResolver
    {
        /// <summary> 
        /// 报文结束标记 
        /// </summary> 
        private string endTag;

        /// <summary> 
        /// 返回结束标记 
        /// </summary> 
        public string EndTag
        {
            get
            {
                return endTag;
            }
        }

        /// <summary> 
        /// 受保护的默认构造函数,提供给继承类使用 
        /// </summary> 
        protected DatagramResolver()
        {

        }

        /// <summary> 
        /// 构造函数 
        /// </summary> 
        /// <param name="endTag">报文结束标记</param> 
        public DatagramResolver(string endTag)
        {
            if (endTag == null)
            {
                throw (new ArgumentNullException("结束标记不能为null"));
            }

            if (endTag == "")
            {
                throw (new ArgumentException("结束标记符号不能为空字符串"));
            }

            this.endTag = endTag;
        }

        /// <summary> 
        /// 解析报文 
        /// </summary> 
        /// <param name="rawDatagram">原始数据,返回未使用的报文片断, 
        /// 该片断会保存在Session的Datagram对象中</param> 
        /// <returns>报文数组,原始数据可能包含多个报文</returns> 
        public virtual string[] Resolve(ref string rawDatagram)
        {
            ArrayList datagrams = new ArrayList();

            //末尾标记位置索引 
            int tagIndex = -1;

            while (true)
            {
                tagIndex = rawDatagram.IndexOf(endTag, tagIndex + 1);

                if (tagIndex == -1)
                {
                    break;
                }
                else
                {
                    //按照末尾标记把字符串分为左右两个部分 
                    string newDatagram = rawDatagram.Substring(
                        0, tagIndex + endTag.Length);

                    datagrams.Add(newDatagram);

                    if (tagIndex + endTag.Length >= rawDatagram.Length)
                    {
                        rawDatagram = "";

                        break;
                    }

                    rawDatagram = rawDatagram.Substring(tagIndex + endTag.Length,
                        rawDatagram.Length - newDatagram.Length);

                    //从开始位置开始查找 
                    tagIndex = 0;
                }
            }

            string[] results = new string[datagrams.Count];

            datagrams.CopyTo(results);

            return results;
        }

    }
}
