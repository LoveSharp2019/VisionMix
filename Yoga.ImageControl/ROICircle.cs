using System;
using HalconDotNet;

namespace Yoga.ImageControl
{
    [Serializable]
	/// <summary>
	/// Բ��region
	/// </summary>
	public class ROICircle : ROI
	{

		private double radius;     //�뾶
		private double row1, col1;  // Բ�ϵĵ�
        private double midR, midC;  // Բ��


        public ROICircle()
		{
            //���Ƶ�2��
			NumHandles = 2; // one at corner of circle + midpoint

            //Ĭ�Ͽ��Ƶ�ΪԲ��
			activeHandleIdx = 1;

            this.ROIType = ROIType.Circle;
		}
        /// <summary>�����������λ������һ��Բ��region</summary>
        public override void CreateROI(double midX, double midY)
		{
            //Բ��Ϊ������ڵ�
			midR = midY;
			midC = midX;


            int width = GetHandleWidth();
            radius = width*10.0;
            //Բ�ϵĵ�
			row1 = midR;
			col1 = midC + radius;
		}

		/// <summary>����Բ�������ڴ�����</summary>
		/// <param name="window">HALCON ����</param>
		public override void Draw(HalconDotNet.HWindow window)
		{
            //��ʾԲ
			window.DispCircle(midR, midC, radius);

            int width = GetHandleWidth();
            //��ʾ�������ƿ�
			window.DispRectangle2(row1, col1, 0, width, width);
			window.DispRectangle2(midR, midC, 0, width, width);
            
        }

        /// <summary>
        /// ��������������Ƶ�ľ���
        /// </summary>
        /// <param name="x">���x</param>
        /// <param name="y">���y</param>
        /// <returns>����ֵ</returns>
		public override double DistToClosestHandle(double x, double y)
		{
			double max = 10000;
			double [] val = new double[NumHandles];

			val[0] = HMisc.DistancePp(y, x, row1, col1); // border handle 
			val[1] = HMisc.DistancePp(y, x, midR, midC); // midpoint 

			for (int i=0; i < NumHandles; i++)
			{
				if (val[i] < max)
				{
					max = val[i];
					activeHandleIdx = i;
				}
			}// end of for 
			return val[activeHandleIdx];
		}

		/// <summary> 
		///�ڴ�������ʾ����Ŀ���ͼ��
		/// </summary>
		public override void DisplayActive(HalconDotNet.HWindow window)
		{
            int width = GetHandleWidth();
			switch (activeHandleIdx)
			{
				case 0://Բ�ϵĵ�
					window.DispRectangle2(row1, col1, 0, width, width);
					break;
				case 1://Բ��
					window.DispRectangle2(midR, midC, 0, width, width);
					break;
			}
		}

		/// <summary>���ص�ǰ��regionͼ������</summary>
		public override HRegion GetRegion()
		{
			HRegion region = new HRegion();
			region.GenCircle(midR, midC, radius);
			return region;
		}
        /// <summary>
        /// ���ص���Բ�ĵľ���
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
		public override double GetDistanceFromStartPoint(double row, double col)
		{
			double sRow = midR; // assumption: we have an angle starting at 0.0
			double sCol = midC + 1 * radius;

			double angle = HMisc.AngleLl(midR, midC, sRow, sCol, midR, midC, row, col);

			if (angle < 0)
				angle += 2 * Math.PI;

			return (radius * angle);
		}

		/// <summary>
		/// ��ȡԲ��region�Ĳ�������
		/// </summary> 
		public override HTuple GetModelData()
		{
			return new HTuple(new double[] { midR, midC, radius });
		}

		/// <summary> 
		/// ���ݿ��Ƶ㼰��꽻���޸�region
		/// </summary>
		public override void MoveByHandle(double newX, double newY)
		{
			HTuple distance;
			double shiftX,shiftY;

			switch (activeHandleIdx)
			{
				case 0: // �����Բ�ϵ�һ��ʱ

					row1 = newY;
					col1 = newX;
					HOperatorSet.DistancePp(new HTuple(row1), new HTuple(col1),
											new HTuple(midR), new HTuple(midC),
											out distance);

					radius = distance[0].D;
					break;
				case 1: // �����Բ����ʱ

					shiftY = midR - newY;
					shiftX = midC - newX;

					midR = newY;
					midC = newX;

					row1 -= shiftY;
					col1 -= shiftX;
					break;
			}
		}
	}//end of class
}//end of namespace
