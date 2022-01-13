using System;
using HalconDotNet;
using System.Diagnostics;

namespace Yoga.ImageControl
{

    [Serializable]
    /// <summary>
    /// ROI���͵Ļ���
    /// </summary>    
    public class ROI
    {
        /// <summary>
        /// Ҫ��ʾroi��ͼ����
        /// </summary>
        private int imageWidth;
        /// <summary>
        /// ͼ���ϵĿ��ƿ����
        /// </summary>
		protected int NumHandles;
        /// <summary>
        /// ͼ���ϵļ�����ƿ����
        /// </summary>
		protected int activeHandleIdx;
        /// <summary>
        /// ROI����
        /// </summary>
        public ROIType ROIType { get; protected set; }
        /// <summary>
        /// Flag to define the ROI to be 'positive' or 'negative'.
        /// </summary>
        protected ROIOperation operatorFlag;

        protected string info;
        public ROIOperation OperatorFlag
        {
            get
            {
                return operatorFlag;
            }
            set
            {
                operatorFlag = value;

                switch (operatorFlag)
                {
                    case ROIOperation.Positive:
                        FlagLineStyle = posOperation;
                        break;
                    case ROIOperation.Negative:
                        FlagLineStyle = negOperation;
                        break;
                    default:
                        FlagLineStyle = posOperation;
                        break;
                }
            }

        }
        /// <summary>ROI����ʾ��ʽ����</summary>
        public HTuple FlagLineStyle { get; set; }


        public double TxtScale { get; set; }
        public int ImageWidth
        {
            get
            {
                if (imageWidth == 0)
                {
                    imageWidth = 500;
                }
                return imageWidth;
            }

            set
            {
                imageWidth = value;
            }
        }

        /// <summary>
        /// "+"��ʽֱ��ֱ��
        /// </summary>
        protected HTuple posOperation = new HTuple();
        /// <summary>
        /// "-"��ʽ������
        /// </summary>
		protected HTuple negOperation = new HTuple(new int[] { 8, 8 });

        /// <summary>Constructor of abstract ROI class.</summary>
        public ROI() { }

        /// <summary>
        /// ����roi��Ϣ��������rio����ͼ��
        /// </summary>
        public virtual void ReCreateROI() { }
        /// <summary>�����λ�ô���ROI</summary>
        /// <param name="midX">
        /// ���������
        /// </param>
        /// <param name="midY">
        /// ���������
        /// </param>
        public virtual void CreateROI(double midX, double midY) { }

        /// <summary>Paints the ROI into the supplied window.</summary>
        /// <param name="window">HALCON window</param>
        public virtual void Draw(HalconDotNet.HWindow window) { }

        /// <summary>
        /// ������������ROI��������Ƶ�ľ���
        /// </summary>
        /// <param name="x">�������X</param>
        /// <param name="y">�������Y</param>
        /// <returns>�����ROI�Ŀ��ƿ���������ֵ</returns>
        public virtual double DistToClosestHandle(double x, double y)
        {
            return 0.0;
        }

        /// <summary> 
        /// Paints the active handle of the ROI object into the supplied window. 
        /// </summary>
        /// <param name="window">HALCON window</param>
        public virtual void DisplayActive(HalconDotNet.HWindow window) { }

        /// <summary> 
        /// Recalculates the shape of the ROI. Translation is 
        /// performed at the active handle of the ROI object 
        /// for the image coordinate (x,y).
        /// </summary>
        /// <param name="x">x (=column) coordinate</param>
        /// <param name="y">y (=row) coordinate</param>
        public virtual void MoveByHandle(double x, double y) { }

        /// <summary>��ȡroi������region</summary>
        public virtual HRegion GetRegion()
        {
            return null;
        }

        public virtual double GetDistanceFromStartPoint(double row, double col)
        {
            return 0.0;
        }
        /// <summary>
        /// ��ȡROI��������������Ϣ.
        /// </summary> 
        public virtual HTuple GetModelData()
        {
            return null;
        }

        /// <summary>Number of handles defined for the ROI.</summary>
        /// <returns>Number of handles</returns>
        public int GetNumHandles()
        {
            return NumHandles;
        }

        /// <summary>Gets the active handle of the ROI.</summary>
        /// <returns>Index of the active handle (from the handle list)</returns>
        public int GetActHandleIdx()
        {
            return activeHandleIdx;
        }

        public int GetHandleWidth()
        {
            //Debug.WriteLine("widht{0}", ImageWidth);
            int dat = ImageWidth / 80;
            if (dat < 3)
            {
                dat = 3;
            }
            return dat;
        }
    }//end of class
}//end of namespace
