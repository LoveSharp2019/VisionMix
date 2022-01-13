using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace Yoga.Common.MotionControl
{

   public abstract class CommBase : IDisposable
    {
        bool _disposed;
        /// <summary>
        /// 外部输入读取完成事件
        /// </summary>
        public event EventHandler<EventArgs> InputReadedEvent;
        public void TrigerInputReadedEvent()
        {
            if (InputReadedEvent != null)
            {
                InputReadedEvent(null, null);
            }
        }
        public void Dispose()
        {
            if (_disposed) return;
            CloseCom();
            _disposed = true;

        }
        #region WIN32 API
        /// <summary>
        /// 创建文件映射
        /// </summary>
        /// <param name="hFile">文件句柄</param>
        /// <param name="lpAttributes">安全</param>
        /// <param name="flProtect">保護</param>
        /// <param name="dwMaximumSizeHigh">サイズ 上位 WORD</param>
        /// <param name="dwMaximumSizeLow">サイズ 下位 WORD</param>
        /// <param name="lpName">对象名称</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFileMapping(
                                    UIntPtr hFile,                  // 
                                    IntPtr lpAttributes,            // 
                                    uint flProtect,                 // 
                                    uint dwMaximumSizeHigh,         // 
                                    uint dwMaximumSizeLow,          // 
                                    string lpName                   // 
                                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwDesiredAccess">访问模式</param>
        /// <param name="bInheritHandle">继承标志</param>
        /// <param name="lpName">对象名称</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenFileMapping(
                                    uint dwDesiredAccess,           // アクセスモード
                                    int bInheritHandle,             // 継承フラグ
                                    string lpName                   // オブジェクト名
                                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hFileMappingObject">文件映射对象句柄</param>
        /// <param name="dwDesiredAccess">访问模式</param>
        /// <param name="dwFileOffsetHigh"></param>
        /// <param name="dwFileOffsetLow"></param>
        /// <param name="dwNumberOfBytesToMap">字节在映射对象的数目</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile(
                                    IntPtr hFileMappingObject,
                                    uint dwDesiredAccess,
                                    uint dwFileOffsetHigh,
                                    uint dwFileOffsetLow,
                                    uint dwNumberOfBytesToMap
                                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpBaseAddress">起始地址</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int UnmapViewOfFile(
                                    IntPtr lpBaseAddress            // 開始アドレス
                                );
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hObject">句柄对象</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(
                                    IntPtr hObject                  // オブジェクトのハンドル
                                );
        /// <summary>
        /// 无效
        /// </summary>
        private const int INVALID_HANDLE_VALUE = -1;                // Invalid
        /// <summary>
        /// 读取和写入权限
        /// </summary>
        private const uint PAGE_READWRITE = 0x04;                   // 読み書きアクセス
        /// <summary>
        /// 读取和写入权限
        /// </summary>
        private const uint FILE_MAP_WRITE = 0x02;                   // 読み書きアクセス

        #endregion

        #region 共有变量
        /// <summary>
        /// 共享内存大小
        /// </summary>
        protected const int SHMEM_SIZE = 1024;            // 共有メモリサイズ
        /// <summary>
        /// DM输入（PLC）的大小
        /// </summary>
        protected const int DMI_SIZE = 40;                // DM 入力 (PLC) サイズ
        /// <summary>
        /// DM输出（PLC）的大小
        /// </summary>
        protected const int DMO_SIZE = 10;                // DM 出力 (PLC) サイズ
        /// <summary>
        /// 模拟量开始位置
        /// </summary>
        protected const int DMO_A_START = 10;
        /// <summary>
        /// 用户区域大小
        /// </summary>
        protected const int USR_SIZE = 1;                // ユーザエリア サイズ
        /// <summary>
        /// DM输入（PLC）地址偏移
        /// </summary>
        protected static readonly int DMI_OFST = 0;               // DM 入力 (PLC) アドレスオフセット
        /// <summary>
        /// DO输出（PLC）地址偏移
        /// </summary>
        protected static readonly int DMO_OFST = DMI_OFST +       // DO 出力 (PLC) アドレスオフセット
                                        DMI_SIZE * Marshal.SizeOf(typeof(short));
        /// <summary>
        /// 用户区地址偏移
        /// </summary>
        protected static readonly int USR_OFST = DMO_OFST +       // ユーザエリア アドレスオフセット
                                        DMO_SIZE * Marshal.SizeOf(typeof(short));
        /// <summary>
        /// 顺序布局结构
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = SHMEM_SIZE)]
        protected struct ShMem
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DMI_SIZE)]
            public short[] dmI;         // DM 入力 (PLC)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DMO_SIZE)]
            public short[] dmO;         // DM 出力 (PLC)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = USR_SIZE)]
            public int[] userArea;      // 用户区域
        }

        #endregion
        /// <summary>共享内存句柄</summary>
        protected IntPtr shMemPtr = IntPtr.Zero;

        /// <summary>共享内存地址</summary>
        protected IntPtr mapAddrPtr = IntPtr.Zero;

        /// <summary>共享内存</summary>
        protected ShMem shMem;

        /// <summary>共享内存名称</summary>
        protected readonly string SHMEM_NAME = "ShMemComIf";

        /// <summary>共享内存互斥</summary>
        protected Mutex mutexShMem = null;//一个共享资源每次只能被一个线程访问

        /// <summary>互斥名</summary>
        protected readonly string MUTEX_NAME = @"Global\ShMemMutex";

        /// <summary>PLC/控制卡的通信处理线程</summary>
        protected Thread procPLC = null;

        /// <summary>通信处理结束标志</summary>
        protected bool isLink = false;
        /// <summary>
        /// 当前通信是否连接标识
        /// </summary>
        public bool IsLink
        {
            get
            {
                return isLink;
            }
        }

        /// <summary>通信处理生成（发送端程序）</summary>
        public virtual bool CreateCom()
        {
            //Notify("外部通信开始");

            try
            {
                // 互斥
                mutexShMem = new Mutex(false, MUTEX_NAME);

                // 共享内存
                shMemPtr = CreateFileMapping(
                                (UIntPtr)0xFFFFFFFF,            // ファイルのハンドル
                                IntPtr.Zero,                    // セキュリティ
                                PAGE_READWRITE,                 // 保護
                                0,                              // サイズ 上位 WORD
                                SHMEM_SIZE,                     // サイズ 下位 WORD
                                SHMEM_NAME                      // オブジェクト名
                            );
                int test = shMemPtr.ToInt32();
                if (shMemPtr.ToInt32() == INVALID_HANDLE_VALUE)
                {
                    Util.WriteLog(this.GetType(), "CreateFileMapping : error");
                    throw new Exception("CreateFileMapping : error");
                }

                mapAddrPtr = MapViewOfFile(
                                shMemPtr,                       // ファイルマッピングオブジェクトのハンドル
                                FILE_MAP_WRITE,                 // アクセスモードWRITE,
                                0,                              // オフセット 上位 DWORD
                                0,                              // オフセット 下位 DWORD
                                0                               // マップ対象のバイト数
                            );

                if (mapAddrPtr.ToInt32() == 0)
                {
                    Util.WriteLog(this.GetType(), "MapViewOfFile : error");
                    throw new Exception("MapViewOfFile : error");
                }
                shMem = new ShMem();
                Marshal.StructureToPtr(shMem, mapAddrPtr, true);

                // 生成
                procPLC = new Thread(new ThreadStart(ProcDataDeal));
                procPLC.Name = "communication";
                procPLC.Start();
            }
            catch (Exception ex)
            {
                Util.Notify(ex.Message);
                Util.WriteLog(this.GetType(), "控制卡异常:" + ex.ToString());
                CloseCom();

                return false;           // NG
            }

            return true;            // OK
        }
        /// <summary>通信処理終了</summary>
        public virtual void CloseCom()
        {
            Util.Notify("外部通信停止");

            // 停止
            if (isLink)
                isLink = false;

            // PLC
            if (procPLC != null)
            {
                if (!procPLC.Join(1000))
                    procPLC.Abort();
                procPLC = null;
            }

            // 共有メモリ
            if (mapAddrPtr != IntPtr.Zero)
            {
                int ret = UnmapViewOfFile(mapAddrPtr);
                System.Diagnostics.Debug.Assert(ret != 0);
                mapAddrPtr = IntPtr.Zero;
            }

            if (shMemPtr != IntPtr.Zero)
            {
                int ret = CloseHandle(shMemPtr);
                System.Diagnostics.Debug.Assert(ret != 0);
                shMemPtr = IntPtr.Zero;
            }

            // ミューテックス
            if (mutexShMem != null)
            {
                mutexShMem.Close();
                mutexShMem = null;
            }
        }


        /// <summary>通信处理线程方法</summary>
        protected abstract void ProcDataDeal();

        /// <summary>
        /// 获取数据端口数据
        /// </summary>
        /// <param name="dataAcq"></param>
        /// <returns></returns>
        protected string ReceivePortResponse(DataAcquisition dataAcq)
        {
            int timeout = 0;            // 超时计数器

            while (true)
            {
                if (dataAcq.ReceiveData != null)
                {
                    if (dataAcq.ReceiveData.Length > 0)//接收到数据
                    {
                        string strTmp = "";
                        strTmp = dataAcq.ReceiveData;
                        dataAcq.ReceiveData = null;
                        return strTmp;
                    }
                }
                // 超时确认
                if (++timeout > 1000)
                {
                    Util.Notify("通信 : 接收超时");
                    return null;
                }

                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// DI (PLC) 取得
        /// </summary>
        /// <param name="ch">通道号</param>
        /// <returns>公共存储区对应的值</returns>
        public int GetDi(int ch)
        {
            if (isLink == false)
            {
                return 0;
            }
            int idx = ch / 16;          // 対象インデックス
            if (idx < 0 || DMI_SIZE <= idx)
                return 0;
            int bit = ch % 16;          // 対象ビット
            int ofst = DMI_OFST + idx * Marshal.SizeOf(typeof(short));

            // 锁定
            mutexShMem.WaitOne();

            short value = Marshal.ReadInt16(mapAddrPtr, ofst);

            // 解除
            mutexShMem.ReleaseMutex();

            return (value >> bit) & 0x01;
        }

        /// <summary>DI (PLC) 設定</summary>
        public void SetDi(int ch, int data)
        {
            int idx = ch / 16;          // 対象インデックス
            if (idx < 0 || DMI_SIZE <= idx)
                return;
            int bit = ch % 16;          // 対象ビット
            int ofst = DMI_OFST + idx * Marshal.SizeOf(typeof(short));

            // 锁定
            mutexShMem.WaitOne();

            short value = Marshal.ReadInt16(mapAddrPtr, ofst);

            if (data == 0)
                value = (short)(value & ~(0x01 << bit));
            else
                value = (short)((ushort)value | (0x01 << bit));

            Marshal.WriteInt16(mapAddrPtr, ofst, value);

            // 解除
            mutexShMem.ReleaseMutex();
        }

        /// <summary>DO (PLC) 取得</summary>
        public int GetDo(int ch)
        {
            if (isLink == false)
            {
                return 0;
            }
            int idx = ch / 16;          // 対象インデックス
            if (idx < 0 || DMO_SIZE <= idx)
                return 0;
            int bit = ch % 16;          // 対象ビット
            int ofst = DMO_OFST + idx * Marshal.SizeOf(typeof(short));

            // 锁定
            mutexShMem.WaitOne();

            short value = Marshal.ReadInt16(mapAddrPtr, ofst);

            // 解除
            mutexShMem.ReleaseMutex();

            return (value >> bit) & 0x01;
        }

        /// <summary>DO (PLC) 設定</summary>
        public virtual void SetDo(int ch, int data)
        {
            if (mutexShMem == null)
            {
                return;
            }
            int idx = ch / 16;          // 対象インデックス
            if (idx < 0 || DMO_SIZE <= idx)
                return;
            int bit = ch % 16;          // 対象ビット
            int ofst = DMO_OFST + idx * Marshal.SizeOf(typeof(short));

            // 锁定
            mutexShMem.WaitOne();

            short value = Marshal.ReadInt16(mapAddrPtr, ofst);

            if (data == 0)
                value = (short)(value & ~(0x01 << bit));
            else
                value = (short)((ushort)value | (0x01 << bit));

            Marshal.WriteInt16(mapAddrPtr, ofst, value);

            // 解除
            mutexShMem.ReleaseMutex();
        }

        /// <summary>AI (PLC) 取得</summary>
        public int GetAi(int ch)
        {
            if (isLink == false)
            {
                return 0;
            }
            if (ch < 0)
                return 0;
            if (isLink == false)
            {
                return 0;
            }
            // ch.变换 (AI : DMO_A_START int ～)
            ch += DMO_A_START;
            if (ch >= DMI_SIZE)
                return 0;

            int ofst = DMI_OFST + ch * Marshal.SizeOf(typeof(int));

            // 锁定
            mutexShMem.WaitOne();

            int value = Marshal.ReadInt32(mapAddrPtr, ofst);
            // 解除
            mutexShMem.ReleaseMutex();

            return value;
        }

        /// <summary>AI (PLC) 設定</summary>
        public void SetAi(int ch, int data)
        {
            if (ch < 0)
                return;

            ch += DMO_A_START;
            if (ch >= DMI_SIZE)
                return;

            int ofst = DMI_OFST + ch * Marshal.SizeOf(typeof(int));


            // 以下为写入 short16进制时候使用
            //if (data > short.MaxValue)
            //    data = short.MaxValue;
            //if (data < short.MinValue)
            //    data = short.MinValue;

            // 锁定
            mutexShMem.WaitOne();

            Marshal.WriteInt32(mapAddrPtr, ofst, data);
            // 解除
            mutexShMem.ReleaseMutex();
        }

        /// <summary>AO (PLC) 取得</summary>
        public int GetAo(int ch)
        {
            if (ch < 0)
                return 0;
            if (isLink == false)
            {
                return 0;
            }
            // ch.変換 (AO : 5word ～)
            ch += DMO_A_START;
            if (ch >= DMO_SIZE)
                return 0;

            int ofst = DMO_OFST + ch * Marshal.SizeOf(typeof(short));

            // 锁定
            mutexShMem.WaitOne();

            short value = Marshal.ReadInt16(mapAddrPtr, ofst);

            // 解除
            mutexShMem.ReleaseMutex();

            return value;
        }

        /// <summary>AO (PLC) 設定</summary>
        public void SetAo(int ch, int data)
        {
            if (ch < 0)
                return;

            // ch.変換 (AO : 5word ～)
            ch += DMO_A_START;
            if (ch >= DMO_SIZE)
                return;

            int ofst = DMO_OFST + ch * Marshal.SizeOf(typeof(short));

            if (data > short.MaxValue)
                data = short.MaxValue;
            if (data < short.MinValue)
                data = short.MinValue;

            // 锁定
            mutexShMem.WaitOne();

            short value = (short)data;

            Marshal.WriteInt16(mapAddrPtr, ofst, value);

            // 解除
            mutexShMem.ReleaseMutex();
        }

        /// <summary>
        /// 動作モード設定
        /// </summary>
        /// <param name="value">設定する動作モード</param>
        public void SetRunMode(int value)
        {
            int ofst = USR_OFST;

            // 锁定
            mutexShMem.WaitOne();

            Marshal.WriteInt32(mapAddrPtr, ofst, value);

            // 解除
            mutexShMem.ReleaseMutex();
        }

        /// <summary>
        /// 操作模式取得
        /// </summary>
        /// <returns>设置操作模式</returns>
        public int GetRunMode()
        {
            int ofst = USR_OFST;
            // 锁定
            mutexShMem.WaitOne();

            int value = Marshal.ReadInt32(mapAddrPtr, ofst);

            // 解除
            mutexShMem.ReleaseMutex();

            return value;
        }


    }
}
