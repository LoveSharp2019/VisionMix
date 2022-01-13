using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Tools
{
    public interface ICalibration
    {
        void ImagePointsToWorldPlane(HTuple px, HTuple py, out HTuple rx, out HTuple ry);
    }
    public interface IOCRTool
    {
        string GetOCRResultText();
    }
    public interface IToolEnable
    {
    }
    public interface IToolRun
    {
    }
    public interface ICreateImage
    {
    }
    public interface IMatching
    {
        /// <summary>
        /// 测试图像2d映射到模板图像,此处需要查找模板成功
        /// </summary>
        /// <returns></returns>
        HHomMat2D TestimageToRefImage(bool useAngle);
        HTuple TestToRefMat2D(bool useAngle);
        /// <summary>
        /// 生成原始图像到测试图像的变换矩阵,此处需要查找模板成功
        /// </summary>
        /// <returns></returns>
        HHomMat2D RefImageToTestImage(bool useAngle);
        HTuple GetRefPoint();
        HTuple GetMatchingPoint();
    }
}
