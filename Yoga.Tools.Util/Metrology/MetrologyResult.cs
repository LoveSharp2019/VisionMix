using HalconDotNet;

namespace Yoga.Tools.Metrology
{
    public abstract class MetrologyResult
    {
        public abstract HXLDCont GetXLD();
        public abstract HTuple GetData();
    }
    public class MetrologyResultLine:MetrologyResult
    {
        public HTuple row1, col1;   // 直线起始点
        public HTuple row2, col2;   // 直线终点

        public MetrologyResultLine(double r1,double c1,double r2,double c2)
        {
            row1 = r1;
            col1 = c1;
            row2 = r2;
            col2 = c2;
        }
        /// <summary>
        /// 深度复制的构造函数
        /// </summary>
        /// <param name="result"></param>
        public MetrologyResultLine(MetrologyResultLine result)
			: this(result.row1, result.col1,
                  result.row2, result.col2)
		{
		}
        public MetrologyResultLine()
        {

        }
        public override HXLDCont GetXLD()
        {
            if (row1 == null || row2 == null || col1 == null || col2 == null)
            {
                return null;
            }
            HXLDCont edge = new HXLDCont();
            HTuple rows, cols;
            //行数组
            rows = new HTuple(new double[] { row1, row2 });
            //列数组
            cols = new HTuple(new double[] { col1, col2 });
            //生成直线图形
            edge.GenContourPolygonXld(rows, cols);
            //返回数据
            return edge;
        }
        public override HTuple GetData()
        {
            if (row1 == null || col1 == null || row2 == null || col2 == null)
            {
                return null;
            }
           return new HTuple(new double[] { row1, col1, row2, col2 });
        }
    }
    public class MetrologyResultCircularArc : MetrologyResult
    {
        public HTuple midR, midC;
        public HTuple radius;
        public HTuple startPhi, extentPhi; // -2*PI <= x <= 2*PI
        public MetrologyResultCircularArc(double _midR, double _midC, double _randius, double _startPhi,double _extentPhi)
        {
            midR = _midR;
            midC = _midC;
            radius = _randius;
            startPhi = _startPhi;
            extentPhi = _extentPhi;
        }
        /// <summary>
        /// 深度复制的构造函数
        /// </summary>
        /// <param name="result"></param>
        public MetrologyResultCircularArc(MetrologyResultCircularArc result)
            : this(result.midR, result.midC,
                  result.radius, result.startPhi,result.extentPhi)
        {
        }
        public MetrologyResultCircularArc()
        {

        }
        public override HXLDCont GetXLD()
        {
            if (midR == null || midC == null || radius == null || startPhi == null || extentPhi==null)
            {
                return null;
            }
            HXLDCont edge = new HXLDCont();
            //生成圆弧图形
            edge.GenCircleContourXld(midR.D, midC.D, radius.D, 0.0,
                                        6.28318, "positive", 1.0);
            //返回数据
            return edge;
        }
        public override HTuple GetData()
        {
            if(midR==null)
            {
                return null;
            }
            return new HTuple(new double[] { midR, midC, radius, startPhi,
                                        (startPhi + extentPhi) });
        }
    }
}
