using HalconDotNet;

namespace Yoga.Tools.Matching
{
    /// <summary>
    /// ģ����ҽ��
    /// </summary>
    public class MatchingResult
    {
        /// <summary>
        /// ԭʼģ��ͼ�������,����Ϊͼ����������λ��
        /// </summary>
		private HXLDCont contour;
        /// <summary>
        /// ���ҵ�������ģ��ͼ������
        /// </summary>
		private HXLDCont contResults;

        /// <summary>
        /// ���ҵ���ģ���������
        /// </summary>
		public HTuple Row;
        /// <summary>
        ///���ҵ���ģ���������
        /// </summary>
		public HTuple Col;

        /// <summary>
        /// ģ����ת�Ƕ�
        /// </summary>
		public HTuple Angle;
        /// <summary>
        /// ������
        /// </summary>
		public HTuple ScaleRow;
        /// <summary>
        /// ������
        /// </summary>
		public HTuple ScaleCol;
        /// <summary>
        /// �������
        /// </summary>
		public HTuple Score;
        /// <summary>
        /// ����ʱ��
        /// </summary>
		public double TimeFound;
        /// <summary>
        /// ���ҵ���ģ�����
        /// </summary>
		public int Count;
        /// <summary>
        /// 2D���ȱ任���󣬿����ڽ����ݴ�ģ��ת��Ϊ����ͼ��
        /// </summary>
		public HHomMat2D Hmat;

        private double maxLength = 0;

        public HXLDCont Contour
        {
            get
            {
                return contour;
            }

            set
            {
                contour = value;


                double row1;
                double col1;
                double phi;

                double l1, l2;
                HRegion region = new HRegion();
                HRegion regionUnion = new HRegion();
                region=contour.GenRegionContourXld("filled");
                regionUnion=region.Union1();
                regionUnion.SmallestRectangle2(out row1, out col1, out phi, out l1, out l2);
                region.Dispose();
                regionUnion.Dispose();


                //double row2;
                //double col2;
                //contour.SmallestRectangle1Xld(out row1, out col1, out row2, out col2);
                //l1 = row2 - row1;
                //l2 = col2 - col1;
                ////contour.SmallestRectangle2Xld(out row1, out col1, out phi, out l1, out l2);
                maxLength = (System.Math.Max(l1, l2)) * 2.0;
            }
        }

        /// <summary>���캯��</summary>
        public MatchingResult()
        {
            Hmat = new HHomMat2D();
            contResults = new HXLDCont();
        }


        /// <summary>
        /// ��ȡ���ͼ��
        /// </summary>
        /// <returns>Detected contour</returns>
		public HXLDCont GetDetectionResults()
        {
            HXLDCont rContours = new HXLDCont();
            if (Hmat == null)
            {
                Hmat = new HHomMat2D();
            }
            Hmat.HomMat2dIdentity();
            if (contResults != null && contResults.IsInitialized())
            {
                contResults.Dispose();
            }
            if (contResults == null)
            {
                contResults = new HXLDCont();
            }
            contResults.GenEmptyObj();

            for (int i = 0; i < Count; i++)
            {
                HHomMat2D mat1 = new HHomMat2D();
                //�任��������
                mat1.VectorAngleToRigid(0, 0, 0, Row[i].D, Col[i].D, Angle[i].D);
                mat1=mat1.HomMat2dScale(ScaleRow[i].D, ScaleCol[i].D, Row[i].D, Col[i].D);
                //ͼ��ƫ��
                rContours = mat1.AffineTransContourXld(Contour);
                //��ȡģ�弯��
                contResults = contResults.ConcatObj(rContours);
                rContours.Dispose();
                double length=6.0;
                if (maxLength>0)
                {
                    length = maxLength/4.0;
                }
                rContours.GenCrossContourXld(Row[i].D, Col[i].D, length, Angle[i].D);
                contResults = contResults.ConcatObj(rContours);
            }
            return contResults;
        }


        /// <summary>
        /// Resets the detection results and sets count to 0.
        /// </summary>
		public void Reset()
        {
            Count = 0;
        }

    }//end of class
}//end of namespace
