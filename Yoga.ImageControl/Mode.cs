using System;
using System.Collections;
using HalconDotNet;



namespace Yoga.ImageControl
{
    /// <summary>
    /// halcon窗体属性设置类
    /// </summary>
    public struct Mode
	{

		/// <summary>
        /// 显示颜色模式
		/// </summary>        
		public const string COLOR	 = "Color";

		/// <summary>
        /// 显示颜色种类 (see dev_set_colored)
		/// </summary>
		public const string COLORED	 = "Colored";

		/// <summary>
        /// 显示线宽(see set_line_width)
		/// </summary>
		public const string LINEWIDTH = "LineWidth";

		/// <summary>
        /// 填充模式(see set_draw)
		/// </summary>
		public const string DRAWMODE  = "DrawMode";

		/// <summary>
		/// Graphical mode for the drawing shape (see set_shape)
		/// </summary>
		public const string SHAPE     = "Shape";

		/// <summary>
		/// Graphical mode for the LUT (lookup table) (see set_lut)
		/// </summary>
		public const string LUT       = "Lut";

		/// <summary>
		/// Graphical mode for the painting (see set_paint)
		/// </summary>
		public const string PAINT     = "Paint";

		/// <summary>
		/// 线条显示方式,实线/虚线等
		/// </summary>
		public const string LINESTYLE = "LineStyle";
	}//end of class
}//end of namespace
