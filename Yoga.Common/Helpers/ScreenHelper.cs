using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Yoga.Common.Helpers
{
    public static class ScreenHelper
    {
        #region CaptureScreen
        /// <summary>
        /// CaptureScreen 截取全屏。
        /// </summary>  
        public static Bitmap CaptureScreen()
        {
            return ScreenHelper.CaptureScreen(new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
        }

        /// <summary>
        /// CaptureScreen 截取目标区域region内的屏幕。
        /// </summary>        
        public static Bitmap CaptureScreen(Rectangle region)
        {
            Bitmap image = new Bitmap(region.Width, region.Height);
            Graphics g = Graphics.FromImage(image);
            g.CopyFromScreen(region.Location, new Point(0, 0), region.Size);
            return image;
        }
        #endregion

        #region GetSupportedScreenResolutions
        /// <summary>
        /// GetSupportedDisplayModes 获取系统支持的屏幕分辨率。
        /// </summary>        
        public static List<ScreenResolution> GetSupportedScreenResolutions()
        {
            Dictionary<string, DisplayMode> dic = new Dictionary<string, DisplayMode>();
            foreach (DisplayMode mode in ScreenHelper.GetSupportedDisplayModes())
            {
                if (mode.dmPelsWidth >= mode.dmPelsHeight)
                {
                    string token = string.Format("{0}*{1}", mode.dmPelsWidth, mode.dmPelsHeight);
                    if (!dic.ContainsKey(token))
                    {
                        dic.Add(token, mode);
                    }
                    else
                    {
                        if (mode.dmBitsPerPel > dic[token].dmBitsPerPel)
                        {
                            dic[token] = mode;
                        }
                        else if (mode.dmBitsPerPel == dic[token].dmBitsPerPel)
                        {
                            if (mode.dmDisplayFrequency > dic[token].dmDisplayFrequency)
                            {
                                dic[token] = mode;
                            }
                        }
                    }
                }
            }

            List<ScreenResolution> list = new List<ScreenResolution>();
            foreach (DisplayMode mode in dic.Values)
            {
                list.Add(mode.GetScreenResolution());
            }
            return list;
        }

        /// <summary>
        /// GetSupportedDisplayModes 获取系统支持的所有的屏幕分辨率的详细信息。
        /// </summary>        
        public static List<DisplayMode> GetSupportedDisplayModes()
        {
            List<DisplayMode> list = new List<DisplayMode>();
            DisplayMode displayMode = new DisplayMode();
            for (int i = 0; ScreenHelper.EnumDisplaySettings(0, i, ref displayMode); i++)
            {
                list.Add(displayMode);
            }
            return list;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool EnumDisplaySettings(int lpszDeviceName, int iModeNum, ref DisplayMode lpDevMode);
        #endregion

        #region ChangeDisplayMode
        /// <summary>
        /// ChangeDisplayMode 更改系统的显示设置。
        /// </summary>    
        public static void ChangeDisplayMode(ScreenResolution screenResolution)
        {
            if (screenResolution.BitsPerPixels == 0 || screenResolution.DisplayFrequency == 0)
            {
                ScreenHelper.ChangeDisplayMode(screenResolution.Width, screenResolution.Height);
                return;
            }

            bool found = false;
            foreach (DisplayMode mode in ScreenHelper.GetSupportedDisplayModes())
            {
                if (mode.dmPelsWidth == screenResolution.Width && mode.dmPelsHeight == screenResolution.Height
                 && mode.dmDisplayFrequency == screenResolution.DisplayFrequency && mode.dmBitsPerPel == screenResolution.BitsPerPixels)
                {
                    found = true;
                    ScreenHelper.ChangeDisplayMode(mode);
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("ScreenResolution settings Not Supported");
            }
        }

        public static void ChangeDisplayMode(int width, int heigth)
        {
            DisplayMode? target = null;
            foreach (DisplayMode mode in ScreenHelper.GetSupportedDisplayModes())
            {
                if (mode.dmPelsWidth == width && mode.dmPelsHeight == heigth)
                {
                    if (target == null)
                    {
                        target = mode;
                    }
                    else
                    {
                        if (mode.dmDisplayFrequency > target.Value.dmDisplayFrequency || mode.dmBitsPerPel > target.Value.dmBitsPerPel)
                        {
                            target = mode;
                        }
                    }

                }
            }

            if (target == null)
            {
                throw new Exception("ScreenResolution settings Not Supported");
            }

            ScreenHelper.ChangeDisplayMode(target.Value);
        }

        public static void ChangeDisplayMode(DisplayMode displayMode)
        {
            ScreenHelper.ChangeDisplaySettings(ref displayMode, 0);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int ChangeDisplaySettings([In] ref DisplayMode lpDevMode, int dwFlags);
        #endregion
    }

    /// <summary>
    /// ScreenResolution 屏幕分辨率
    /// </summary>
    [Serializable]
    public class ScreenResolution
    {
        #region Ctor
        public ScreenResolution()
        {
            this.width = Screen.PrimaryScreen.Bounds.Width;
            this.height = Screen.PrimaryScreen.Bounds.Height;
            this.displayFrequency = 60;
            this.bitsPerPixels = 32;
        }

        public ScreenResolution(int _width, int _height, int _displayFrequency, int _bitsPerPixels)
        {
            this.width = _width;
            this.height = _height;
            this.displayFrequency = _displayFrequency;
            this.bitsPerPixels = _bitsPerPixels;
        }
        #endregion

        #region Width
        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        #endregion

        #region Height
        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        #endregion

        #region BitsPerPixels
        private int bitsPerPixels = 32;
        public int BitsPerPixels
        {
            get { return bitsPerPixels; }
            set { bitsPerPixels = value; }
        }
        #endregion

        #region DisplayFrequency
        private int displayFrequency;
        public int DisplayFrequency
        {
            get { return displayFrequency; }
            set { displayFrequency = value; }
        }
        #endregion

        public override string ToString()
        {
            return string.Format("{0,4}×{1,-4} {2,2}Bits {3,3}Hz", this.width, this.height, this.bitsPerPixels, this.displayFrequency);
        }
    }

    #region DisplayMode
    /// <summary>
    /// DisplayMode 显示模式。分辨率、刷新率等
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DisplayMode
    {
        public const int DM_BITSPERPEL = 0x040000;
        public const int DM_PELSWIDTH = 0x080000;
        public const int DM_PELSHEIGHT = 0x100000;
        public const int DM_DISPLAYFREQUENCY = 0x400000;
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;

        public int dmPositionX;
        public int dmPositionY;
        public DMDO dmDisplayOrientation;
        public int dmDisplayFixedOutput;

        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;

        public override string ToString()
        {
            return string.Format("{0,4}×{1,-4} {2,2}Bits {3,3}Hz  {4}", dmPelsWidth, dmPelsHeight, dmBitsPerPel, dmDisplayFrequency, dmDeviceName);
        }

        public ScreenResolution GetScreenResolution()
        {
            return new ScreenResolution(dmPelsWidth, dmPelsHeight, dmDisplayFrequency, dmBitsPerPel);
        }
    }

    public enum DMDO
    {
        DEFAULT = 0,
        D90 = 1,
        D180 = 2,
        D270 = 3
    }
    #endregion
}
