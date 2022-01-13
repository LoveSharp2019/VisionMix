using HalconDotNet;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Yoga.Common.Basic;
namespace Yoga.Common
{
    /// <summary>
    /// 日志及log记录等杂项
    /// </summary>
    public static class Util
    {
        public static event EventHandler<MessageEventArgs> MessageEvent;

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);

            log.Error("Error", ex);
        }

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
        /// <summary>消息通知</summary>
        /// <param name="message">信息</param>
        public static void Notify(string message)
        {
            if (MessageEvent != null)
            {
                MessageEvent(null, new MessageEventArgs(message));
            }
        }
        public static void Notify(Basic.Level level, string message)
        {
            if (MessageEvent != null)
            {
                MessageEvent(null, new MessageEventArgs(level,message));
            }
        }
        /// <summary>
        /// 异步执行一个方法,并阻塞当前线程指定时间
        /// </summary>
        /// <param name="action">要执行的方法</param>
        /// <param name="timeoutMilliseconds">等待的ms时间</param>
        /// <returns>超时返回false,未超时返回true</returns>
        public static bool CallWithTimeout(Action action, int timeoutMilliseconds)
        {
            Thread threadToKill = null;
            Action wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                action();
            };

            IAsyncResult result = wrappedAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
                return true;
            }
            else
            {
                threadToKill.Abort();
            }
            return false;
        }

        /// <summary>
        /// 判断字符串中是否包含中文
        /// </summary>
        /// <param name="str">需要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        [Obsolete("该方法已移至StringHelper",false)]
        /// <summary>
        /// utf8文字用gb2312格式显示时候乱码,需要转换为gb2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Gb2312Correct(string text)
        {
            if (HasChinese(text)==false)
            {
                return text;
            }
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(utf8, gb2312, gb);
            //返回转换后的字符   
            return gb2312.GetString(gb);
        }
        /// <summary>  
        /// 去掉字符串末尾位置中指定的字符串  
        /// </summary>  
        /// <param name="s">源串</param>  
        /// <param name="searchStr">查找的串</param>  
        public static string TrimEndString(this string s, string searchStr)
        {
            var result = s;
            if (string.IsNullOrEmpty(result))
            {
                return result;
            }
            if (s.Length < searchStr.Length)
            {
                return result;
            }
            if (s.IndexOf(searchStr, s.Length - searchStr.Length, searchStr.Length, StringComparison.Ordinal) > -1)
            {
                result = s.Substring(0, s.Length - searchStr.Length);
            }
            return result;
        }
        /// <summary>  
        /// 去掉字符串起始位置(开头)中指定的字符串  
        /// </summary>  
        /// <param name="s">源串</param>  
        /// <param name="searchStr">查找的串</param>  
        /// <returns></returns>  
        public static string TrimStartString(this string s, string searchStr)
        {
            var result = s;
            if (string.IsNullOrEmpty(result))
            {
                return result;
            }
            if (s.Length < searchStr.Length)
            {
                return result;
            }
            if (s.IndexOf(searchStr, 0, searchStr.Length, StringComparison.Ordinal) > -1)
            {
                result = s.Substring(searchStr.Length);
            }
            return result;
        }
        [DllImport("Kernel32.dll")]
        internal static extern void CopyMemory(int dest, int source, int size);
        public static void HObject2Bpp8(HObject image, out Bitmap res)
        {
            HTuple hpoint, type, width, height;

            const int Alpha = 255;
            int[] ptr = new int[2];
            HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

            res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            ColorPalette pal = res.Palette;
            for (int i = 0; i <= 255; i++)
            {
                pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);
            }
            res.Palette = pal;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            ptr[0] = bitmapData.Scan0.ToInt32();
            ptr[1] = hpoint.I;
            if (width % 4 == 0)
                CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
            else
            {
                for (int i = 0; i < height - 1; i++)
                {
                    ptr[1] += width;
                    CopyMemory(ptr[0], ptr[1], width * PixelSize);
                    ptr[0] += bitmapData.Stride;
                }
            }
            res.UnlockBits(bitmapData);

        }
    }
}
