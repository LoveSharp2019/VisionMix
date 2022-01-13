//************************************************************************************
// Copyright (c) 2017117 JIGTECH All Rights Reserved.
//  CLR version:      14.0
// Company Name:      JIGTECH
//     Filename:      FrmLoad.cs
//      Version:      V1.0.0.0
//       Create:      longteng
//  Create time:      2017-11-7 17:6:39
//   Descrption:      窗体载入界面定义
//
//************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;
using Yoga.Common.Helpers;
using Yoga.Common.Basic;
/// <summary>
/// 窗体载入界面定义
/// </summary>
namespace Yoga.Common.UI
{
    public partial class FrmLoad : Form
    {
        /// <summary>
        /// 背景图像
        /// </summary>
        private Bitmap background;
        /// <summary>
        /// 运行参数
        /// </summary>
        private static class RunParametereter
        {
            private static string status = "";//状态提示信息
            private static int startNum = 0;//此次 进度条 开始数值
            private static int endNum = 0;//此次 进度条 结束数值
            private static int costTime = 10000;//此过程预计加载时间 单位为秒
            private static float currentNum = 0;//当前进度条显示数值

            public static string Status { get; set; }
            public static int StartNum { get; set; }
            public static int EndNum { get; set; }
            public static int CostTime { get; set; }
            public static float CurrentNum { get; set; }
        }
        /// <summary>
        /// 背景图像所在路径
        /// </summary>
        private string filepath_background = "";
        public FrmLoad(string src)
        {
            TopMost = false;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();

            if (!GetBackgroundImageFilepath(src, out filepath_background))
            {
                System.Environment.Exit(0);
            }
        }

        private void FrmLoad_Load(object sender, EventArgs e)
        {
            SetPerPixelBitmapFilename(filepath_background);
        }

        private void SetPerPixelBitmapFilename(string fileName)
        {
            Bitmap newBitmap;

            try
            {
                newBitmap = Image.FromFile(fileName) as Bitmap;
                Graphics g = Graphics.FromImage(newBitmap);
                g.DrawString(Parameter.Name, GetFont(55), new SolidBrush(Color.White), new PointF(20, 25));
                g.DrawString(Parameter.Year, GetFont(40), new SolidBrush(Color.White), new PointF(25, 115));
                g.DrawString("Version:" + Parameter.Version, GetFont(13), new SolidBrush(Color.Black), new PointF(30, 285));
                g.DrawString(Parameter.CopyRight, GetFont(13), new SolidBrush(Color.Black), new PointF(30, 310));
                g.DrawString(Parameter.CompanyName, GetFont(20), new SolidBrush(Color.White), new PointF(20, 350));
                g.FillRectangle(new SolidBrush(Color.PaleGreen), new Rectangle(30, 230, 445, 30));
                g.Dispose();
                //设置图片透明度
                SetBitmap(newBitmap, (byte)255);
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(this, e.Message, "Error with bitmap.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Could not open image file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (background != null)
                background.Dispose();
            background = newBitmap;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
                return cp;
            }
        }

        /// <summary>
        /// 响应鼠标事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0084 /*WM_NCHITTEST*/)
            {
                m.Result = (IntPtr)2;	// HTCLIENT
                return;
            }
            base.WndProc(ref m);
        }

        #region 函数
        /// <summary>
        /// 获取随机目录
        /// </summary>
        /// <param name="src">图像所在目录</param>
        /// <param name="dst">结果图像路径</param>
        /// <returns>是否获取正确路径</returns>
        private bool GetBackgroundImageFilepath(string src, out string dst)
        {
            dst = "";
            if (src == "")
            {
                MessageHelper.ShowWarning("启动界面背景图片不能为空！");
                return false;
            }

            if (!System.IO.Directory.Exists(src))
            {
              MessageHelper.ShowWarning(src + "不存在！");
                return false;
            }

            try
            {
                //获取目录下面的png图像
                var files = System.IO.Directory.GetFiles(src, "*.png");

                //判断是否存在png图像
                if (files.Length <= 0)
                {
                    MessageHelper.ShowWarning(src + " 此目录中不存在png图像！");
                    return false;
                }

                //获取随机图像路径
                long tick = System.DateTime.Now.Ticks;

                Random random = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int i = random.Next();
                dst = files[random.Next() % files.Length];
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("载入界面获取随机目录：" + ex.ToString());
                return false;
            }
        }

        #region 与主界面数据互动
        public void UpdateProgress(string status, int startNum, int endNum, int costTime)
        {
            RunParametereter.Status = status;
            RunParametereter.StartNum = startNum;
            RunParametereter.EndNum = endNum;
            RunParametereter.CostTime = costTime;
            UpdateProgressBackground();
        }

        private void UpdateProgressBackground()
        {
            int frame = 0;
            if (RunParametereter.EndNum - RunParametereter.StartNum < 0)
            {
                int temp = RunParametereter.StartNum;
                RunParametereter.StartNum = RunParametereter.EndNum;
                RunParametereter.EndNum = temp;
            }
            if (RunParametereter.StartNum == RunParametereter.EndNum)
            {
                frame = 1;
            }
            else
            {
                frame = (int)(RunParametereter.CostTime / (RunParametereter.EndNum - RunParametereter.StartNum) * 0.8);
            }
            for (int i = RunParametereter.StartNum; i <= RunParametereter.EndNum; i++)
            {
                UpdateStatusToForm(RunParametereter.Status, i, frame);
            }
        }

        private void UpdateStatusToForm(string status, int currentNum, int waitTime)
        {
            Bitmap temp = background.Clone(new Rectangle(0, 0, background.Width, background.Height), background.PixelFormat);
            Graphics g = Graphics.FromImage(temp);
            g.DrawString(status, GetFont(18), new SolidBrush(Color.Black), new PointF(35, 195));
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(30, 230, 445 * currentNum / 100, 30));
            g.DrawString(currentNum.ToString("0'%'"), GetFont(15), new SolidBrush(Color.Black), new PointF(230, 235));
            g.Dispose();

            SetBitmap(temp);
            if (temp != null)
                temp.Dispose();

            System.Threading.Thread.Sleep(waitTime);
            Application.DoEvents();
        }
        #endregion

        #region 设置字体
        private Font GetFont(float size)
        {
            return new System.Drawing.Font("微软雅黑", size, GraphicsUnit.Pixel);
        }
        #endregion

        #region 设置背景图像
        private void SetBitmap(Bitmap bitmap)
        {
            SetBitmap(bitmap, 255);
        }
        private void SetBitmap(Bitmap bitmap, byte opacity)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

            // The idea of this is very simple,
            // 1. Create a compatible DC with screen;
            // 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
            // 3. Call the UpdateLayeredWindow.

            IntPtr screenDc = GDI.GetDC(IntPtr.Zero);
            IntPtr memDc = GDI.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
                oldBitmap = GDI.SelectObject(memDc, hBitmap);

                GDI.Size size = new GDI.Size(bitmap.Width, bitmap.Height);
                GDI.Point pointSource = new GDI.Point(0, 0);
                GDI.Point topPos = new GDI.Point(Left, Top);
                GDI.BLENDFUNCTION blend = new GDI.BLENDFUNCTION();
                blend.BlendOp = GDI.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = opacity;
                blend.AlphaFormat = GDI.AC_SRC_ALPHA;

                GDI.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, GDI.ULW_ALPHA);
            }
            finally
            {
                GDI.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    GDI.SelectObject(memDc, oldBitmap);
                    //Windows.DeleteObject(hBitmap);
                    // The documentation says that we have to use the Windows.DeleteObject... but since there is no such method I use the normal DeleteObject from GDI GDI and it's working fine without any resource leak.
                    GDI.DeleteObject(hBitmap);
                }
                GDI.DeleteDC(memDc);
            }
        }
        #endregion
        #endregion
    }

    public static class Parameter
    {
        public static string Name { get; set; }
        public static string Year { get; set; }
        public static string Version { get; set; }
        public static string CopyRight { get; set; }
        public static string CompanyName { get; set; }
        public static string Status { get; set; }
        public static int StatusNum { get; set; }
        public static string MachineName { get; set; }
    }
}
