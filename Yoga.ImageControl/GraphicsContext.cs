using System;
using System.Collections;
using HalconDotNet;



namespace Yoga.ImageControl
{
    /// <summary>
    /// halcon��������������
    /// </summary>
    public class GraphicsContext
	{

		/// <summary>
        /// ��ʾ��ɫģʽ
		/// </summary>        
		public const string GC_COLOR	 = "Color";

		/// <summary>
        /// ��ʾ��ɫ���� (see dev_set_colored)
		/// </summary>
		public const string GC_COLORED	 = "Colored";

		/// <summary>
        /// ��ʾ�߿�(see set_line_width)
		/// </summary>
		public const string GC_LINEWIDTH = "LineWidth";

		/// <summary>
        /// ���ģʽ(see set_draw)
		/// </summary>
		public const string GC_DRAWMODE  = "DrawMode";

		/// <summary>
		/// Graphical mode for the drawing shape (see set_shape)
		/// </summary>
		public const string GC_SHAPE     = "Shape";

		/// <summary>
		/// Graphical mode for the LUT (lookup table) (see set_lut)
		/// </summary>
		public const string GC_LUT       = "Lut";

		/// <summary>
		/// Graphical mode for the painting (see set_paint)
		/// </summary>
		public const string GC_PAINT     = "Paint";

		/// <summary>
		/// ������ʾ��ʽ,ʵ��/���ߵ�
		/// </summary>
		public const string GC_LINESTYLE = "LineStyle";

		/// <summary> 
		/// Hashlist containing entries for graphical modes (defined by GC_*), 
		/// which is then linked to some HALCON object to describe its 
		/// graphical context.
		/// </summary>
		private Hashtable		graphicalSettings;

		/// <summary> 
		/// ͼ�񴰿ڵ���ʾ��ǰ��ʾ����
		/// </summary>
		public	Hashtable		stateOfSettings;

		private IEnumerator iterator;

        /// <summary> 
        /// Creates a graphical context with no initial 
        /// graphical modes 
        /// </summary> 
        public GraphicsContext()
		{
			graphicalSettings = new Hashtable(10, 0.2f);
			stateOfSettings = new Hashtable(10, 0.2f);
		}


		/// <summary> 
		/// Creates an instance of the graphical context with 
		/// the modes defined in the hashtable 'settings' 
		/// </summary>
		/// <param name="settings"> 
		/// List of modes, which describes the graphical context 
		/// </param>
		public GraphicsContext(Hashtable settings)
		{
			graphicalSettings = settings;
			stateOfSettings = new Hashtable(10, 0.2f);
		}

		/// <summary>��ͼ����ʾ����Ӧ�õ�����</summary>
		/// <param name="window">������</param>
		/// <param name="cContext">���õ������б�</param>
		public void ApplyContext(HWindow window, Hashtable cContext)
		{
			string key  = "";
			string valS = "";
			int    valI = -1;
			HTuple valH = null;
            //��ȡ������ʾö��
			iterator = cContext.Keys.GetEnumerator();

			try
			{
				while (iterator.MoveNext())
				{
                    //��ǰԪ��
					key = (string)iterator.Current;
                    //��ǰ����������������ͬ��������ǰһ��ѭ��
					if (stateOfSettings.Contains(key) &&
						stateOfSettings[key] == cContext[key])
						continue;

					switch (key)
					{
						case GC_COLOR://���ò�ɫ����-�����ǰ����ɫ���Ծ��Ƴ�����
							valS = (string)cContext[key];
							window.SetColor(valS);
							if (stateOfSettings.Contains(GC_COLORED))
								stateOfSettings.Remove(GC_COLORED);

							break;
						case GC_COLORED:
							valI = (int)cContext[key];
							window.SetColored(valI);

							if (stateOfSettings.Contains(GC_COLOR))
								stateOfSettings.Remove(GC_COLOR);

							break;
						case GC_DRAWMODE:
							valS = (string)cContext[key];
							window.SetDraw(valS);
							break;
						case GC_LINEWIDTH:
							valI = (int)cContext[key];
							window.SetLineWidth(valI);
							break;
						case GC_LUT:
							valS = (string)cContext[key];
							window.SetLut(valS);
							break;
						case GC_PAINT:
							valS = (string)cContext[key];
							window.SetPaint(valS);
							break;
						case GC_SHAPE:
							valS = (string)cContext[key];
							window.SetShape(valS);
							break;
						case GC_LINESTYLE:
							valH = (HTuple)cContext[key];
							window.SetLineStyle(valH);
							break;
						default:
							break;
					}

                    //����������ɾͽ���ǰ��������ӵ� ���嵱ǰ��ʾ�����б�
					if (valI != -1)
					{
						if (stateOfSettings.Contains(key))
							stateOfSettings[key] = valI;
						else
							stateOfSettings.Add(key, valI);

						valI = -1;
					}
					else if (valS != "")
					{
						if (stateOfSettings.Contains(key))
							stateOfSettings[key] = valI;
						else
							stateOfSettings.Add(key, valI);

						valS = "";
					}
					else if (valH != null)
					{
						if (stateOfSettings.Contains(key))
							stateOfSettings[key] = valI;
						else
							stateOfSettings.Add(key, valI);

						valH = null;
					}
				}//while
			}
			catch (HOperatorException ex)
			{
                Common.Util.WriteLog(this.GetType(), ex);

				return;
			}
		}
        /// <summary>
        /// ������ʾ��ɫ,���ú��ɫ�������ñ��Ƴ�
        /// </summary>
        /// <param name="val">��ɫ,��"blue", "green" ��</param>
        public void SetColorAttribute(string val)
		{
			if (graphicalSettings.ContainsKey(GC_COLORED))
				graphicalSettings.Remove(GC_COLORED);

			addValue(GC_COLOR, val);
		}

		/// <summary>���ò�ɫ����,���������ú�ɫ���ñ��Ƴ�</summary>
		/// <param name="val"> 
		/// ��ɫ���� �� "colored3" / "colored6" /"colored12" 
		/// </param>
		public void SetColoredAttribute(int val)
		{
			if (graphicalSettings.ContainsKey(GC_COLOR))
				graphicalSettings.Remove(GC_COLOR);

			addValue(GC_COLORED, val);
		}

		/// <summary>����region���ģʽ</summary>
		/// <param name="val"> 
		///  "margin" ���� "fill" 
		/// </param>
		public void SetDrawModeAttribute(string val)
		{
			addValue(GC_DRAWMODE, val);
		}

		/// <summary>�����߿�</summary>
		/// <param name="val"> 
		/// �߿�,��ΧΪ 1 to 50 
		/// </param>
		public void SetLineWidthAttribute(int val)
		{
			addValue(GC_LINEWIDTH, val);
		}

		/// <summary>Sets a value for the graphical mode GC_LUT</summary>
		/// <param name="val"> 
		/// One of the possible modes of look up tables. For 
		/// further information on particular setups, please refer to the
		/// Reference Manual entry of the operator set_lut.
		/// </param>
		public void SetLutAttribute(string val)
		{
			addValue(GC_LUT, val);
		}


		/// <summary>Sets a value for the graphical mode GC_PAINT</summary>
		/// <param name="val"> 
		/// One of the possible paint modes. For further 
		/// information on particular setups, please refer refer to the
		/// Reference Manual entry of the operator set_paint.
		/// </param>
		public void SetPaintAttribute(string val)
		{
			addValue(GC_PAINT, val);
		}


		/// <summary>Sets a value for the graphical mode GC_SHAPE</summary>
		/// <param name="val">
		/// One of the possible shape modes. For further 
		/// information on particular setups, please refer refer to the
		/// Reference Manual entry of the operator set_shape.
		/// </param>
		public void SetShapeAttribute(string val)
		{
			addValue(GC_SHAPE, val);
		}

		/// <summary>Sets a value for the graphical mode GC_LINESTYLE</summary>
		/// <param name="val"> 
		/// A line style mode, which works 
		/// identical to the input for the HDevelop operator 
		/// 'set_line_style'. For particular information on this 
		/// topic, please refer to the Reference Manual entry of the operator
		/// set_line_style.
		/// </param>
		public void SetLineStyleAttribute(HTuple val)
		{
			addValue(GC_LINESTYLE, val);
		}

		/// <summary> 
		/// Adds a value to the hashlist 'graphicalSettings' for the 
		/// graphical mode described by the parameter 'key' 
		/// </summary>
		/// <param name="key"> 
		/// A graphical mode defined by the constant GC_* 
		/// </param>
		/// <param name="val"> 
		/// Defines the value as an int for this graphical
		/// mode 'key' 
		/// </param>
		private void addValue(string key, int val)
		{
			if (graphicalSettings.ContainsKey(key))
				graphicalSettings[key] = val;
			else
				graphicalSettings.Add(key, val);
		}

		/// <summary>
		/// Adds a value to the hashlist 'graphicalSettings' for the 
		/// graphical mode, described by the parameter 'key'
		/// </summary>
		/// <param name="key"> 
		/// A graphical mode defined by the constant GC_* 
		/// </param>
		/// <param name="val"> 
		/// Defines the value as a string for this 
		/// graphical mode 'key' 
		/// </param>
		private void addValue(string key, string val)
		{
			if (graphicalSettings.ContainsKey(key))
				graphicalSettings[key] = val;
			else
				graphicalSettings.Add(key, val);
		}


		/// <summary> 
		/// Adds a value to the hashlist 'graphicalSettings' for the 
		/// graphical mode, described by the parameter 'key'
		/// </summary>
		/// <param name="key">
		/// A graphical mode defined by the constant GC_* 
		/// </param>
		/// <param name="val"> 
		/// Defines the value as a HTuple for this 
		/// graphical mode 'key' 
		/// </param>
		private void addValue(string key, HTuple val)
		{
			if (graphicalSettings.ContainsKey(key))
				graphicalSettings[key] = val;
			else
				graphicalSettings.Add(key, val);
		}

		/// <summary> 
		/// �����������
		/// </summary>
		public void Clear()
		{
			graphicalSettings.Clear();
		}


		/// <summary> 
		/// Returns an exact clone of this graphicsContext instance 
		/// </summary>
		public GraphicsContext Copy()
		{
			return new GraphicsContext((Hashtable)this.graphicalSettings.Clone());
		}


		/// <summary> 
		/// If the hashtable contains the key, the corresponding 
		/// hashtable value is returned 
		/// </summary>
		/// <param name="key"> 
		/// One of the graphical keys starting with GC_* 
		/// </param>
		public object GetGraphicsAttribute(string key)
		{
			if (graphicalSettings.ContainsKey(key))
				return graphicalSettings[key];

			return null;
		}

		/// <summary> 
		/// Returns a copy of the hashtable that carries the 
		/// entries for the current graphical context 
		/// </summary>
		/// <returns> current graphical context </returns>
		public Hashtable CopyContextList()
		{
			return (Hashtable)graphicalSettings.Clone();
		}
	}//end of class
}//end of namespace
