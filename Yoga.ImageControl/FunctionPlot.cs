using System;
using HalconDotNet;
using System.Drawing;
using System.Windows.Forms;

namespace Yoga.ImageControl
{
    /// <summary>
    ///���ƻҶ�ͼ��
    /// </summary>
    public class FunctionPlot
    {
        // ��ͼ����
        private Graphics gPanel, backBuffer;

        // ��ͼ�趨
        private Pen pen, penCurve, penCursor;
        private SolidBrush brushCS, brushFuncPanel;
        private Font drawFont;
        private StringFormat format;
        private Bitmap functionMap;

        Control panel;

        // ��������޶� 
        private float panelWidth;
        private float panelHeight;
        private float margin;

        // ԭ��
        private float originX;
        private float originY;

        private PointF[] points;
        private HFunction1D func;

        // axis 
        private int axisAdaption;
        private float axisXLength;
        private float axisYLength;
        private float scaleX, scaleY;
        //��ʾģʽ
        public const int AXIS_RANGE_FIXED = 3;
        public const int AXIS_RANGE_INCREASING = 4;
        public const int AXIS_RANGE_ADAPTING = 5;

        int PreX, BorderRight, BorderTop;



        private string xName, yName;

        public string XName
        {
            get
            {
                return xName;
            }

            private set
            {
                xName = value;
            }
        }

        public string YName
        {
            get
            {
                return yName;
            }

            set
            {
                yName = value;
            }
        }

        /// <summary>
        ///���캯��
        /// </summary>
        /// <param name="panel">
        /// Ҫ��ʾ�Ҷ����ߵĿؼ�
        /// </param>
        /// <param name="useMouseHandle">
        /// �Ƿ���Ӧ����ƶ��¼�
        /// </param>
        public FunctionPlot(Control panel, bool useMouseHandle)
        {
            gPanel = panel.CreateGraphics();
            this.panel = panel;
            panelWidth = panel.Size.Width - 32;
            panelHeight = panel.Size.Height - 22;

            originX = 32;
            originY = panel.Size.Height - 22;
            margin = 5.0f;

            BorderRight = (int)(panelWidth + originX - margin);
            BorderTop = (int)panelHeight;

            PreX = 0;
            scaleX = scaleY = 0.0f;


            //default setting for axis scaling
            axisAdaption = AXIS_RANGE_ADAPTING;
            axisXLength = 10.0f;
            axisYLength = 10.0f;

            pen = new Pen(System.Drawing.Color.DarkGray, 1);
            penCurve = new Pen(System.Drawing.Color.Blue, 1);
            penCursor = new Pen(System.Drawing.Color.LightSteelBlue, 1);
            penCursor.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            brushCS = new SolidBrush(Color.Black);
            brushFuncPanel = new SolidBrush(Color.White);
            drawFont = new Font("Arial", 6);
            format = new StringFormat();
            format.Alignment = StringAlignment.Far;

            functionMap = new Bitmap(panel.Size.Width, panel.Size.Height);
            backBuffer = Graphics.FromImage(functionMap);


            XName = "X";
            YName = "Y";
            ResetPlot();

            panel.Paint += new System.Windows.Forms.PaintEventHandler(this.paint);

            if (useMouseHandle)
                panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
        }

        public void SetLabel(string x, string y)
        {
            XName = x;
            YName = y;
        }
        /// <summary>
        /// ���캯��,Ĭ�ϲ���Ӧ����ƶ�
        /// </summary>
        /// <param name="panel">
        /// ����ؼ�
        /// </param>
        public FunctionPlot(Control panel)
            : this(panel, false)
        {
        }


        /// <summary>
        ///�ı�����ԭ��
        /// </summary>
        /// <param name="x">
        ///�ؼ�����ϵ��x
        /// </param>
        /// <param name="y">
        ///�ؼ�����ϵ��y 
        /// </param>
        public void SetOrigin(int x, int y)
        {
            float tmpX;

            if (x < 1 || y < 1)
                return;

            tmpX = originX;
            originX = x;
            originY = y;

            panelWidth = panelWidth + tmpX - originX;
            panelHeight = originY;
            BorderRight = (int)(panelWidth + originX - margin);
            BorderTop = (int)panelHeight;
        }


        /// <summary>
        /// ����Y����������ģʽ
        /// </summary>
        /// <param name="mode">
        ///����ģʽ����
        /// </param>
        /// <param name="val">
        /// ��AXIS_RANGE_FIXEDģʽ������y�᷶Χ
        /// </param>
        public void SetAxisAdaption(int mode, float val)
        {
            switch (mode)
            {
                case AXIS_RANGE_FIXED:
                    axisAdaption = mode;
                    axisYLength = (val > 0) ? val : 255.0f;
                    break;
                default:
                    axisAdaption = mode;
                    break;
            }
        }
        /// <summary>
        /// ����Y����������ģʽ
        /// </summary>
        /// <param name="mode">����ģʽ����</param>
		public void SetAxisAdaption(int mode)
        {
            SetAxisAdaption(mode, -1.0f);
        }





        /// <summary>
        /// �����������ݻ�������
        /// </summary>
        /// <param name="tuple">��������</param>
        public void drawFunction(HTuple tuple)
        {
            HTuple val;
            int maxY, maxX;
            float stepOffset;

            if (tuple.Length == 0)
            {
                ResetPlot();
                return;
            }
            //����
            val = tuple.TupleSortIndex();
            //���ֵ��ȡ
            maxX = tuple.Length - 1;
            maxY = (int)tuple[val[val.Length - 1].I].D;

            axisXLength = maxX;

            switch (axisAdaption)
            {
                case AXIS_RANGE_ADAPTING:
                    axisYLength = maxY;
                    break;
                case AXIS_RANGE_INCREASING:
                    axisYLength = (maxY > axisYLength) ? maxY : axisYLength;
                    break;
            }
            //�������򱳾�
            backBuffer.Clear(System.Drawing.Color.WhiteSmoke);
            backBuffer.FillRectangle(brushFuncPanel, originX, 0, panelWidth, panelHeight);
            //���������ἰ��ǩ
            stepOffset = drawXYLabels();
            //�������ݻ��ƻҶ�����
            drawLineCurve(tuple, stepOffset);
            backBuffer.Flush();

            gPanel.DrawImageUnscaled(functionMap, 0, 0);
            gPanel.Flush();
        }


        /// <summary>
        /// �����ͼ����ֻ��ʾ����ϵ
        /// </summary>
        public void ResetPlot()
        {
            backBuffer.Clear(System.Drawing.Color.WhiteSmoke);
            backBuffer.FillRectangle(brushFuncPanel, originX, 0, panelWidth, panelHeight);
            func = null;
            drawXYLabels();
            backBuffer.Flush();
            Repaint();
        }


        /// <summary>
        /// �ػ�ͼ��
        /// </summary>
        private void Repaint()
        {
            gPanel.DrawImageUnscaled(functionMap, 0, 0);
            gPanel.Flush();
        }


        /// <summary>���ƺ�����</summary>
        private void drawLineCurve(HTuple tuple, float stepOffset)
        {
            int length;

            if (stepOffset > 1)
                points = scaleDispValue(tuple);
            else
                points = scaleDispBlockValue(tuple);

            length = points.Length;

            func = new HFunction1D(tuple);

            for (int i = 0; i < length - 1; i++)
                backBuffer.DrawLine(penCurve, points[i], points[i + 1]);

        }


        /// <summary>
        /// Scales the function to the dimension of the graphics object 
        /// (provided by the control). 
        /// </summary>
        /// <param name="tup">
        /// Function defined as a tuple of y-values
        /// </param>
        /// <returns>
        /// Array of PointF values, containing the scaled function data
        /// </returns>
        private PointF[] scaleDispValue(HTuple tup)
        {
            PointF[] pVals;
            float xMax, yMax, yV, x, y;
            int length;

            xMax = axisXLength;
            yMax = axisYLength;

            scaleX = (xMax != 0.0f) ? ((panelWidth - margin) / xMax) : 0.0f;
            scaleY = (yMax != 0.0f) ? ((panelHeight - margin) / yMax) : 0.0f;

            length = tup.Length;
            pVals = new PointF[length];

            for (int j = 0; j < length; j++)
            {
                yV = (float)tup[j].D;
                x = originX + j * scaleX;
                y = panelHeight - (yV * scaleY);
                pVals[j] = new PointF(x, y);
            }

            return pVals;
        }


        /// <summary>
        /// Scales the function to the dimension of the graphics object 
        /// (provided by the control). If the stepsize  for the x-axis is
        /// 1, the points are scaled in a block shape.
        /// </summary>
        /// <param name="tup">
        /// Function defined as a tuple of y-values 
        /// </param>
        /// <returns>
        /// Array of PointF values, containing the scaled function data
        /// </returns>
        private PointF[] scaleDispBlockValue(HTuple tup)
        {
            PointF[] pVals;
            float xMax, yMax, yV, x, y;
            int length, idx;

            xMax = axisXLength;
            yMax = axisYLength;

            scaleX = (xMax != 0.0f) ? ((panelWidth - margin) / xMax) : 0.0f;
            scaleY = (yMax != 0.0f) ? ((panelHeight - margin) / yMax) : 0.0f;

            length = tup.Length;
            pVals = new PointF[length * 2];

            y = 0;
            idx = 0;

            for (int j = 0; j < length; j++)
            {
                yV = (float)tup[j].D;
                x = originX + j * scaleX - (scaleX / 2.0f);
                y = panelHeight - (yV * scaleY);
                pVals[idx] = new PointF(x, y);
                idx++;

                x = originX + j * scaleX + (scaleX / 2.0f);
                pVals[idx] = new PointF(x, y);
                idx++;
            }

            //trim the end points of the curve
            idx--;
            x = originX + (length - 1) * scaleX;
            pVals[idx] = new PointF(x, y);

            idx = 0;
            yV = (float)tup[idx].D;
            x = originX;
            y = panelHeight - (yV * scaleY);
            pVals[idx] = new PointF(x, y);

            return pVals;
        }


        /// <summary>������ͱ�ǩ����</summary>
        /// <returns>x��Ĳ���</returns>
        private float drawXYLabels()
        {
            float pX, pY, length, XStart, YStart;
            float YCoord, XCoord, XEnd, YEnd, offset, offsetString, offsetStep;
            float scale = 0.0f;

            offsetString = 5;
            XStart = originX;
            YStart = originY;

            //prepare scale data for x-axis
            pX = axisXLength;
            if (pX != 0.0)
                scale = (panelWidth - margin) / pX;

            if (scale > 10.0)
                offset = 1.0f;
            else if (scale > 2)
                offset = 10.0f;
            else if (scale > 0.2)
                offset = 100.0f;
            else
                offset = 1000.0f;


            /***************   draw X-Axis   ***************/
            XCoord = 0.0f;
            YCoord = YStart;
            XEnd = (scale * pX);

            backBuffer.DrawLine(pen, XStart, YStart, XStart + panelWidth - margin, YStart);
            backBuffer.DrawLine(pen, XStart + XCoord, YCoord, XStart + XCoord, YCoord + 6);
            backBuffer.DrawString(0 + "", drawFont, brushCS, XStart + XCoord + 4, YCoord + 8, format);
            backBuffer.DrawLine(pen, XStart + XEnd, YCoord, XStart + XEnd, YCoord + 6);
            backBuffer.DrawString(((int)pX + ""), drawFont, brushCS, XStart + XEnd + 4, YCoord + 8, format);

            length = (int)(pX / offset);
            length = (offset == 10) ? length - 1 : length;
            for (int i = 1; i <= length; i++)
            {
                XCoord = offset * i * scale;

                if ((XEnd - XCoord) < 20)
                    continue;

                backBuffer.DrawLine(pen, XStart + XCoord, YCoord, XStart + XCoord, YCoord + 6);
                backBuffer.DrawString(((int)(i * offset) + ""), drawFont, brushCS, XStart + XCoord + 5, YCoord + 8, format);
            }

            offsetStep = offset;

            //prepare scale data for y-axis
            pY = axisYLength;
            if (pY != 0.0)
                scale = ((panelHeight - margin) / pY);

            if (scale > 10.0)
                offset = 1.0f;
            else if (scale > 2)
                offset = 10.0f;
            else if (scale > 0.8)
                offset = 50.0f;
            else if (scale > 0.12)
                offset = 100.0f;
            else
                offset = 1000.0f;

            /***************    draw Y-Axis   ***************/
            XCoord = XStart;
            YCoord = 5.0f;
            YEnd = YStart - (scale * pY);

            backBuffer.DrawLine(pen, XStart, YStart, XStart, YStart - (panelHeight - margin));
            backBuffer.DrawLine(pen, XCoord, YStart, XCoord - 10, YStart);
            backBuffer.DrawString(0 + "", drawFont, brushCS, XCoord - 12, YStart - offsetString, format);
            backBuffer.DrawLine(pen, XCoord, YEnd, XCoord - 10, YEnd);
            backBuffer.DrawString(pY + "", drawFont, brushCS, XCoord - 12, YEnd - offsetString, format);

            length = (int)(pY / offset);
            length = (offset == 10) ? length - 1 : length;
            for (int i = 1; i <= length; i++)
            {
                YCoord = (YStart - offset * i * scale);

                if ((YCoord - YEnd) < 10)
                    continue;
                //y������̫��Ͳ�����
                if (length > 10 && i % 5 != 0)
                {
                    continue;
                }
                backBuffer.DrawLine(pen, XCoord, YCoord, XCoord - 10, YCoord);
                backBuffer.DrawString(((int)(i * offset) + ""), drawFont, brushCS, XCoord - 12, YCoord - offsetString, format);
            }

            return offsetStep;
        }


        /// <summary>
        /// Action call for the Mouse-Move event. For the x-coordinate
        /// supplied by the MouseEvent, the unscaled x and y coordinates
        /// of the plotted function are determined and displayed 
        /// on the control.
        /// </summary>
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            int Xh, Xc;
            HTuple Ytup;
            float Yh, Yc;

            Xh = e.X;

            if (PreX == Xh || Xh < originX || Xh > BorderRight || func == null)
                return;

            PreX = Xh;

            Xc = (int)Math.Round((Xh - originX) / scaleX);
            Ytup = func.GetYValueFunct1d(new HTuple(Xc), "zero");

            Yc = (float)Ytup[0].D;
            Yh = panelHeight - (Yc * scaleY);

            BufferedGraphicsContext ctx = BufferedGraphicsManager.Current;
            BufferedGraphics bg = ctx.Allocate(gPanel, new Rectangle(new Point(0, 0), panel.Size));
            Graphics gOfbuff = bg.Graphics;
            Bitmap toDrawBeffor = new Bitmap(panel.Width, panel.Height);
            Graphics gOfToDrawBeffor = Graphics.FromImage(toDrawBeffor);
            // �Զ����ͼ
            gOfToDrawBeffor.DrawLine(Pens.Blue, 0, 0, 1000, 1000);

            gOfToDrawBeffor.DrawImageUnscaled(functionMap, 0, 0);
            gOfToDrawBeffor.DrawLine(penCursor, Xh, 0, Xh, BorderTop);
            gOfToDrawBeffor.DrawLine(penCursor, originX, Yh, BorderRight + margin, Yh);
            string xStr = string.Format("{0}={1}", XName, Xc);
            string yStr = string.Format("{0}={1}", YName, (int)Yc);
            gOfToDrawBeffor.DrawString(xStr, drawFont, brushCS, panelWidth - margin * 4, 10);
            gOfToDrawBeffor.DrawString(yStr, drawFont, brushCS, panelWidth - margin * 4, 20);

            //˫���浽panel��
            gOfbuff.Clear(panel.BackColor);
            gOfbuff.DrawImage(toDrawBeffor, 0, 0);
            bg.Render();
            toDrawBeffor.Dispose();
            gOfToDrawBeffor.Dispose();
            gOfbuff.Dispose();
            ctx.Dispose();
            bg.Dispose();
        }


        /// <summary>
        /// Action call for the Paint event of the control to trigger the
        /// repainting of the function plot. 
        /// </summary>
        private void paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Repaint();
        }

    }//end of class
}//end of namespace
