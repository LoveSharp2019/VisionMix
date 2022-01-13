using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.Basic
{
    /// <summary>
    /// 系统GDI定义
    /// </summary>
    public static class GDI
    {
        public enum Bool
        {
            False = 0,
            True
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public const Int32 ULW_COLORKEY = 0x00000001;
        public const Int32 ULW_ALPHA = 0x00000002;
        public const Int32 ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

        public static extern Bool DeleteObject(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;

            public MARGINS(int left, int right, int top, int bottom) { this.Left = left; this.Right = right; this.Top = top; this.Bottom = bottom; }
        }

#if win7 //界面磨砂特效
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        protected override void OnLoad(EventArgs e)
{
    //如果启用Aero
    if (DwmIsCompositionEnabled())
    {
        MARGINS m = new MARGINS();
        m.Right = -1; //设为负数,则全窗体透明
        DwmExtendFrameIntoClientArea(this.Handle, ref m); //开启全窗体透明效果
    }
    base.OnLoad(e);
}
 
protected override void OnPaintBackground(PaintEventArgs e)
{
    base.OnPaintBackground(e);
    if (DwmIsCompositionEnabled())
    {
        e.Graphics.Clear(Color.Black); //将窗体用黑色填充（Dwm 会把黑色视为透明区域）
    }
}
#endif

#if winXP // Compiling for Windows XP
#elif win2000 // Compiling for Windows 2000
#elif win7 // Compiling for Windows 7
#elif win8 // Compiling for Windows 8
#elif win10 // Compiling for Windows 10
#else // Unknown platform specified
#endif
    }
}
