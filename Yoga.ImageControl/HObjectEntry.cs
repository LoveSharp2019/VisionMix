using HalconDotNet;
using System.Collections;



namespace Yoga.ImageControl
{

    /// <summary>
    /// This class is an auxiliary class, which is used to 
    /// link a graphical context to an HALCON object. The graphical 
    /// context is described by a hashtable, which contains a list of
    /// graphical modes (e.g GC_COLOR, GC_LINEWIDTH and GC_PAINT) 
    /// and their corresponding values (e.g "blue", "4", "3D-plot"). These
    /// graphical states are applied to the window before displaying the
    /// object.
    /// </summary>
    public class HObjectEntry
	{
		/// <summary>HObject�ļ�ֵ����</summary>
		public Hashtable	gContext;

		/// <summary>halconͼ�ζ���</summary>
		public HObject		HObj;

        public HWndMessage Message;

		/// <summary>���캯��</summary>
		/// <param name="obj">
		/// ͼ�����. 
		/// </param>
		/// <param name="gc"> 
		/// Hashlist of graphical states that are applied before the object
		/// is displayed. 
		/// </param>
		public HObjectEntry(HObject obj, Hashtable gc)
		{
			gContext = gc;
			HObj = obj;
		}
        public HObjectEntry(HWndMessage message)
        {
            this.Message = message;
        }
		/// <summary>
		/// ���ʵ��
		/// </summary>
		public void Clear()
		{
			gContext.Clear();
			HObj.Dispose();
		}

	}
}
