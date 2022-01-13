using HalconDotNet;
using System;

namespace Yoga.Tools
{
    /// <summary>
    /// 坐标点数据
    /// </summary>
    [Serializable]
    public struct PointD
    {
        public double X;
        public double Y;
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    [Serializable]
    public struct PointT
    {
        public HTuple X;
        public HTuple Y;
        public PointT(HTuple x, HTuple y)
        {
            X = x;
            Y = y;
        }
    }
}
