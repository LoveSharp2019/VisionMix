using System;
using HalconDotNet;

namespace Yoga.ImageControl
{

    [Serializable]
    /// <summary>
    /// ֱ��ROI
    /// </summary>
    public class ROILine : ROI
    {
        /// <summary>
        /// ���������
        /// </summary>
        double startRow;
        /// <summary>
        /// ���������
        /// </summary>
        double startCol;
        /// <summary>
        /// �յ�������
        /// </summary>
        double endRow;
        /// <summary>
        /// �յ�������
        /// </summary>
        double endCol;
        /// <summary>
        /// �е�������
        /// </summary>
        double midRow;
        /// <summary>
        /// �е�������
        /// </summary>
        double midCol;

        [NonSerialized]
        /// <summary>
        /// ֱ������Ӽ�ͷ��ʾ
        /// </summary>
        private HXLDCont arrowHandleXLD;
        /// <summary>
        /// ֱ��ROI����
        /// </summary>
        public ROILine()
        {
            //ֱ���ϵĿ��ƿ����
            NumHandles = 3;        // �˵�2+�е�1=3
            //Ĭ�ϼ���Ŀ��Ƶ�Ϊ2--�е�
            activeHandleIdx = 2;
            //ֱ�߼�ͷ��ʼ��
            arrowHandleXLD = new HXLDCont();
            arrowHandleXLD.GenEmptyObj();

            this.ROIType = ROIType.Line;
        }
        /// <summary>
        /// ����������յ���Ϣ�ع�ROI
        /// </summary>
        /// <param name="roiDat">������Ϣ</param>
        private void CreateROI(HTuple roiDat)
        {
            if (roiDat.Length == 4)
            {
                startRow = roiDat[0].D;
                startCol = roiDat[1].D;
                endRow = roiDat[2].D;
                endCol = roiDat[3].D;
                midRow = (startRow + endRow) / 2.0;
                midCol = (startCol + endCol) / 2.0;
                updateArrowHandle();
            }
        }

        public override void ReCreateROI()
        {
            updateArrowHandle();
        }
        /// <summary>�����λ�ô���һ��ROI</summary>
        public override void CreateROI(double midX, double midY)
        {

            int width = GetHandleWidth();

            //���λ��Ϊֱ������
            midRow = midY;
            midCol = midX;

            startRow = midRow;
            startCol = midCol - width*5;
            endRow = midRow;
            endCol = midCol + width*5;

            updateArrowHandle();
        }

        /// <summary>����ͼ���ڴ�����</summary>
        public override void Draw(HalconDotNet.HWindow window)
        {
            //ֱ�߻���
            window.DispLine(startRow, startCol, endRow, endCol);

            int width = GetHandleWidth();
            //ֱ�����Ŀ�
            window.DispRectangle2(startRow, startCol, 0, width, width);
            //ֱ���յ�ļ�ͷ
            window.DispObj(arrowHandleXLD);  //window.DispRectangle2( row2, col2, 0, 5, 5);
            //ֱ���е�Ŀ�
            window.DispRectangle2(midRow, midCol, 0, width, width);

            //int r1, c1, r2, c2;
            //window.GetPart(out r1, out c1, out r2, out c2);
            //int width = r2 - r1;
            ////ֱ�����Ŀ�
            //window.DispRectangle2(startRow, startCol, 0, width / 50.0, width / 50.0);
            ////System.Diagnostics.Debug.WriteLine("{0}-{1}-{2}-{3}", r1, c1, r2, c2);
        }

        /// <summary>
        /// ������������ROI��������Ƶ�ľ���
        /// </summary>
        /// <param name="x">�������X</param>
        /// <param name="y">�������Y</param>
        /// <returns>�����ROI�Ŀ��ƿ���������ֵ</returns>
        public override double DistToClosestHandle(double x, double y)
        {

            double max = 10000;
            double[] val = new double[NumHandles];

            val[0] = HMisc.DistancePp(y, x, startRow, startCol); // upper left 
            val[1] = HMisc.DistancePp(y, x, endRow, endCol); // upper right 
            val[2] = HMisc.DistancePp(y, x, midRow, midCol); // midpoint 

            for (int i = 0; i < NumHandles; i++)
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
        /// �ڴ�������ʾ����Ŀ��Ƶ�
        /// </summary>
        public override void DisplayActive(HalconDotNet.HWindow window)
        {
            int width = GetHandleWidth();
            switch (activeHandleIdx)
            {
                case 0:
                    window.DispRectangle2(startRow, startCol, 0, width, width);
                    break;
                case 1:
                    window.DispObj(arrowHandleXLD); //window.DispRectangle2(row2, col2, 0, 5, 5);
                    break;
                case 2:
                    window.DispRectangle2(midRow, midCol, 0, width, width);
                    break;
            }
        }

        /// <summary>
        /// ��ȡregion
        /// </summary>
        /// <returns>regionͼ��</returns>
        public override HRegion GetRegion()
        {
            HRegion region = new HRegion();
            region.GenRegionLine(startRow, startCol, endRow, endCol);
            return region;
        }
        /// <summary>
        /// ��ȡ����㵽ͼ����ʼ��ľ���
        /// </summary>
        /// <param name="row">������ Y</param>
        /// <param name="col">������ X</param>
        /// <returns>����ֵ</returns>
        public override double GetDistanceFromStartPoint(double row, double col)
        {
            double distance = HMisc.DistancePp(row, col, startRow, startCol);
            return distance;
        }
        /// <summary>
        /// ��ȡregion��������Ϣ
        /// </summary> 
        public override HTuple GetModelData()
        {
            return new HTuple(new double[] { startRow, startCol, endRow, endCol });
        }

        /// <summary> 
        ///����������ƶ�region
        /// </summary>
        public override void MoveByHandle(double newX, double newY)
        {
            double lenR, lenC;
            //��ʼ��-�յ�-�е����ַ�ʽ�޸�ֱ��region
            switch (activeHandleIdx)
            {
                case 0: // first end point
                    startRow = newY;
                    startCol = newX;

                    midRow = (startRow + endRow) / 2;
                    midCol = (startCol + endCol) / 2;
                    break;
                case 1: // last end point
                    endRow = newY;
                    endCol = newX;

                    midRow = (startRow + endRow) / 2;
                    midCol = (startCol + endCol) / 2;
                    break;
                case 2: // midpoint 
                    lenR = startRow - midRow;
                    lenC = startCol - midCol;

                    midRow = newY;
                    midCol = newX;

                    startRow = midRow + lenR;
                    startCol = midCol + lenC;
                    endRow = midRow - lenR;
                    endCol = midCol - lenC;
                    break;
            }
            updateArrowHandle();
        }
        /// <summary> 
        /// �����ļ�ͷ��ʾ����
        /// </summary>
        private void updateArrowHandle()
        {
            double length, dr, dc, halfHW;
            double rrow1, ccol1, rowP1, colP1, rowP2, colP2;

            int width = GetHandleWidth();
            double headLength = width;
            double headWidth = width;

            if (arrowHandleXLD == null)
                arrowHandleXLD = new HXLDCont();
            arrowHandleXLD.Dispose();
            arrowHandleXLD.GenEmptyObj();

            //��ͷ��ʼ��Ϊֱ�߳��ȵ�0.8λ��
            rrow1 = startRow + (endRow - startRow) * 0.8;
            ccol1 = startCol + (endCol - startCol) * 0.8;
            //������ͷ��ʼ�㵽ֱ���յ�ľ���
            length = HMisc.DistancePp(rrow1, ccol1, endRow, endCol);
            //�������Ϊ0˵��ֱ�߳���Ϊ0
            if (length == 0)
                length = -1;

            dr = (endRow - rrow1) / length;
            dc = (endCol - ccol1) / length;

            halfHW = headWidth / 2.0;
            rowP1 = rrow1 + (length - headLength) * dr + halfHW * dc;
            rowP2 = rrow1 + (length - headLength) * dr - halfHW * dc;
            colP1 = ccol1 + (length - headLength) * dc - halfHW * dr;
            colP2 = ccol1 + (length - headLength) * dc + halfHW * dr;

            if (length == -1)
                arrowHandleXLD.GenContourPolygonXld(rrow1, ccol1);
            else
                arrowHandleXLD.GenContourPolygonXld(new HTuple(new double[] { rrow1, endRow, rowP1, endRow, rowP2, endRow }),
                                                    new HTuple(new double[] { ccol1, endCol, colP1, endCol, colP2, endCol }));
        }

    }
}
