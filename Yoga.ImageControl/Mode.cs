using System;
using System.Collections;
using HalconDotNet;



namespace Yoga.ImageControl
{
    /// <summary>
    /// halcon��������������
    /// </summary>
    public struct Mode
	{

		/// <summary>
        /// ��ʾ��ɫģʽ
		/// </summary>        
		public const string COLOR	 = "Color";

		/// <summary>
        /// ��ʾ��ɫ���� (see dev_set_colored)
		/// </summary>
		public const string COLORED	 = "Colored";

		/// <summary>
        /// ��ʾ�߿�(see set_line_width)
		/// </summary>
		public const string LINEWIDTH = "LineWidth";

		/// <summary>
        /// ���ģʽ(see set_draw)
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
		/// ������ʾ��ʽ,ʵ��/���ߵ�
		/// </summary>
		public const string LINESTYLE = "LineStyle";
	}//end of class
}//end of namespace
