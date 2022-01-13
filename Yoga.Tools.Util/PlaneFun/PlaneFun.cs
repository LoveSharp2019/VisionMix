using System.Runtime.InteropServices;

namespace Yoga.Tools.PlaneFun
{
    public class PlaneFun
    {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PlaneInfo
        {
            public double A;
            public double B;
            public double C;
            public double D;
        };

        /***************************************************************************************
        ///// // Ax + By + Cz + D=0
        ////A    1
        ////B   2
        ////C  - 1
        ////D   -4

        ////平面拟合测试结果正确
        //FitPlaneFun.PlaneInfo p1 = new FitPlaneFun.PlaneInfo();
        //double[] x = new double[4];
        //double[] y = new double[4];
        //double[] z = new double[4];

        //x[0] = 1;
        //    x[1] = 2;
        //    x[2] = 3;
        //    x[3] = 4;

        //    y[0] = 2;
        //    y[1] = 3;
        //    y[2] = 4;
        //    y[3] = 6;

        //    z[0] = 1;
        //    z[1] = 4;
        //    z[2] = 7;
        //    z[3] = 12;
        //    bool f = FitPlaneFun.FitPlane(4, x, y, z, ref p1);
        ***********************************************************************************************/


        /// <summary>
        /// 平面拟合
        /// </summary>
        /// <param name="nPntSum">点位个数</param>
        /// <param name="px">x坐标</param>
        /// <param name="py">y坐标</param>
        /// <param name="pz">z坐标</param>
        /// <param name="planeInfo">拟合结果</param>
        /// <returns></returns>

        [DllImport("Yoga.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern bool FitPlane(int nPntSum, double[] px, double[] py, double[] pz, ref PlaneInfo planeInfo);
        [DllImport("Yoga.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern double DisPntToPlane(double dx, double dy, double dz, ref PlaneInfo planeInfo);
        /// <summary>
        /// 平面度计算 既是求所有点到平面的最大距离
        /// </summary>
        /// <param name="nPntSum">点个数</param>
        /// <param name="px">x坐标数组</param>
        /// <param name="py">y坐标数组</param>
        /// <param name="pz">z坐标数组</param>
        /// <param name="planeInfo">已知的平面结构体</param>
        /// <returns>点到平面的最大距离</returns>
        [DllImport("Yoga.Native.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern double Flatness(int nPntSum, double[] px, double[] py, double[] pz, ref PlaneInfo planeInfo);


    }
}
