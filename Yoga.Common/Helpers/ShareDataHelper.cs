using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Threading;

namespace Yoga.Common.Helpers
{
    /// <summary>
    /// 对结构体数据映射到内存并做线程安全访问
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShareDataHelper<T> where T : struct
    {
        private MemoryMappedFile file = null;
        private MemoryMappedViewAccessor accessor = null;
        private Mutex mutexShMem = null;

        private T data;
        /// <summary>
        /// 跨exe的线程安全结构体
        /// </summary>
        public T Data
        {
            get
            {
                mutexShMem.WaitOne();

                accessor.Read(0, out data);
                // 解除
                mutexShMem.ReleaseMutex();

                return data;
            }

            set
            {

                mutexShMem.WaitOne();
                data = value;
                accessor.Write(0, ref data);
                // 解除
                mutexShMem.ReleaseMutex();
            }
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="key"></param>
        public void CreateLink(string key)
        {

            int size = Marshal.SizeOf(typeof(T));
            file = MemoryMappedFile.CreateOrOpen(key, size);

            mutexShMem = new Mutex(false, key + "Lock");
            accessor = file.CreateViewAccessor();
        }

        public void Close()
        {
            if (accessor != null)
            {
                accessor.Dispose();
                accessor = null;
            }

            if (file != null)
            {
                file.Dispose();
                file = null;
            }

            if (mutexShMem != null)
            {
                mutexShMem.Close();
                mutexShMem = null;
            }
        }
    }
}
