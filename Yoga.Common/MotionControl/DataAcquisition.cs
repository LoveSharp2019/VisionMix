using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.MotionControl
{
    /// <summary>
    /// 数据采集基类
    /// </summary>
  public  abstract class DataAcquisition : IDisposable
    {
        bool _disposed;
        public string ReceiveData { get; set; }
        protected string receiveDataTmp;
        public void Dispose()
        {
            if (_disposed) return;
            Close();
            _disposed = true;
        }

        /// <summary>
        /// 接口打开
        /// </summary>
        /// <returns>是否打开成功</returns>
        public abstract bool Open();
        /// <summary>
        /// 写入数据到通信口
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>是否写入成功</returns>
        public abstract bool Write(string data);
        public abstract void Close();
    }
}
