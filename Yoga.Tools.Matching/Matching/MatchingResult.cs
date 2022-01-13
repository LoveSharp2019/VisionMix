using HalconDotNet;

namespace Yoga.Tools.Matching
{
    /// <summary>
    /// 模板查找结果
    /// </summary>
    public class MatchingResult
    {
        /// <summary>
        /// 原始模板图像的轮廓,坐标为图像左上中心位置
        /// </summary>
		private HXLDCont contour;
        /// <summary>
        /// 查找到的所有模板图形轮廓
        /// </summary>
		private HXLDCont contResults;

        /// <summary>
        /// 查找到的模板的行坐标
        /// </summary>
		public HTuple Row;
        /// <summary>
        ///查找到的模板的列坐标
        /// </summary>
		public HTuple Col;

        /// <summary>
        /// 模板旋转角度
        /// </summary>
		public HTuple Angle;
        /// <summary>
        /// 行缩放
        /// </summary>
		public HTuple ScaleRow;
        /// <summary>
        /// 列缩放
        /// </summary>
		public HTuple ScaleCol;
        /// <summary>
        /// 结果分数
        /// </summary>
		public HTuple Score;
        /// <summary>
        /// 查找时间
        /// </summary>
		public double TimeFound;
        /// <summary>
        /// 查找到的模板个数
        /// </summary>
		public int Count;
        /// <summary>
        /// 2D均匀变换矩阵，可用于将数据从模型转换为测试图像。
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

        /// <summary>构造函数</summary>
        public MatchingResult()
        {
            Hmat = new HHomMat2D();
            contResults = new HXLDCont();
        }


        /// <summary>
        /// 获取结果图形
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
                //变换矩阵设置
                mat1.VectorAngleToRigid(0, 0, 0, Row[i].D, Col[i].D, Angle[i].D);
                mat1=mat1.HomMat2dScale(ScaleRow[i].D, ScaleCol[i].D, Row[i].D, Col[i].D);
                //图像偏移
                rContours = mat1.AffineTransContourXld(Contour);
                //获取模板集合
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
