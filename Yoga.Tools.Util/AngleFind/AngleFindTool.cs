using System;
using System.Linq;
using HalconDotNet;
using Yoga.ImageControl;
using System.IO;
using Yoga.Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Yoga.Wrapper;

namespace Yoga.Tools.AngleFind
{
    [Serializable]
    public class AngleFindTool : ToolBase//, IToolEnable, IToolRun
    {
        #region 字段

        private static string toolType = "角度查找";

        private HRegion circleRegion;
        private HRegion angleRegion;
        [NonSerialized]
        private double angleResult;

        [NonSerialized]
        HImage ho_PolarTransModelImage;

        #endregion

        #region 属性
        #region 供反射调用属性
        public static string ToolType
        {
            get
            {
                return toolType;
            }
        }
        public static string ToolExplanation
        {
            get
            {
                return toolType + "\r\n版本:20180425\r\n说明:圆心图形的角度查找";
            }
        }
        /// <summary>
        /// 圆形区域,初步使用外部roi创建的方式
        /// </summary>
        public HRegion CircleRegion
        {
            get
            {
                if (circleRegion == null)
                {
                    circleRegion = new HRegion();
                }
                return circleRegion;
            }

            set
            {
                circleRegion = value;
            }
        }

        public HRegion AngleRegion
        {
            get
            {
                if (angleRegion == null)
                {
                    angleRegion = new HRegion();
                }
                return angleRegion;
            }

            set
            {
                angleRegion = value;
            }
        }
        #endregion
        #endregion

        public AngleFindTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new AngleFindToolUnit(this);
            }

            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }

        protected override void RunAct()
        {
            base.RunAct();

            DisposeHobject(ho_PolarTransModelImage);
            ho_PolarTransModelImage = null;
            //HImage testImageCrop;
            if (Mat2DManger.UseMat2D && ImageTestIn != ImageRefIn)
            {
                IMatching matchingTool = Mat2DManger.MatchingTool;
                if (matchingTool == null)
                {
                    matchingTool = Mat2DManger.MatchingTool;
                }
                if (matchingTool == null)
                {
                    Util.Notify(string.Format("工具{0}匹配工具不存在!", Name));
                    return;
                }
                HHomMat2D mat2d;
                mat2d = matchingTool.RefImageToTestImage(true);
                if (mat2d == null)
                {
                    ExceptionText = "映射矩阵建立失败";
                    return;
                }
                if (CircleRegion == null)
                {
                    ExceptionText = "模板区域未建立";
                    return;
                }
                RegionAffine = CircleRegion.AffineTransRegion(mat2d, "nearest_neighbor");
            }
            else
            {
                if (CircleRegion == null)
                {
                    ExceptionText = "模板区域未建立";
                    return;
                }


                RegionAffine = CircleRegion.Clone();
            }
            bool isFind = FindAngle();

            if (isFind == false)
            {
                this.Result = "Err";
            }
            else
            {
                this.Result = $"{angleResult:f2}";
                this.IsOk = true;
                this.isRealOk = true;
            }
        }

        private bool FindAngle()
        {
            if (CircleRegion.IsInitialized() == false || AngleRegion.IsInitialized() == false)
            {
                return false;
            }

            //Tolerance: Half height of the search region in the
            //           polar transformed image for step 2
            HTuple hv_Tolerance = 2.5;

            HTuple hv_RowFittedCircle;
            HTuple hv_ColumnFittedCircle;
            HTuple hv_Radius;
            if (ImageTestIn == ImageRefIn)
            {
                CircleRegion.SmallestCircle(out hv_RowFittedCircle, out hv_ColumnFittedCircle, out hv_Radius);

                HTuple hv_RadiusInner;
                HTuple hv_RadiusOuter;
                HImage imageReduceRef;
                imageReduceRef = ImageTestIn.ReduceDomain(AngleRegion);

                HTuple hv_RowAsymEdgesRegion;
                HTuple hv_ColumnAsymEdgesRegion;
                HRegion AngleRegionRef;
                GetRegionPos(imageReduceRef, out hv_RowAsymEdgesRegion, out hv_ColumnAsymEdgesRegion, out AngleRegionRef);
                //计算极坐标变换的参数
                //不规则区域到拟合的圆心的距离 确定环形区域的内径和外径
                HOperatorSet.DistancePr(AngleRegion, hv_RowFittedCircle, hv_ColumnFittedCircle,
                    out hv_RadiusInner, out hv_RadiusOuter);

                HTuple hv_RefAngle;
                //计算初始角度 既是模板角度
                HOperatorSet.AngleLx(hv_RowFittedCircle, hv_ColumnFittedCircle, hv_RowAsymEdgesRegion,
                    hv_ColumnAsymEdgesRegion, out hv_RefAngle);
                //极坐标宽度 及高度计算
                HTuple hv_PolarWidth = ((hv_RadiusOuter * ((new HTuple(360)).TupleRad()))).TupleRound();
                HTuple hv_PolarHeight = ((hv_RadiusOuter - hv_RadiusInner)).TupleRound();

                HRegion ho_PolarTransAsymEdgesRegion;
                //特征区域转换为极坐标
                ho_PolarTransAsymEdgesRegion = AngleRegionRef.PolarTransRegion(hv_RowFittedCircle, hv_ColumnFittedCircle, hv_RefAngle + ((new HTuple(180)).TupleRad()
                    ), hv_RefAngle + ((new HTuple(540)).TupleRad()), hv_RadiusInner, hv_RadiusOuter,
                    hv_PolarWidth, hv_PolarHeight, "bilinear");

                HTuple hv_Row1;
                HTuple hv_Column1;
                HTuple hv_Row2;
                HTuple hv_Column2;
                //极坐标区域最小矩形
                HOperatorSet.SmallestRectangle1(ho_PolarTransAsymEdgesRegion, out hv_Row1, out hv_Column1,
                    out hv_Row2, out hv_Column2);
                HTuple hv_RegionWidth = (hv_Column2 - hv_Column1) + 1;
                //Extend the polar transform by the width of the transformed model region
                //to make sure, that the asymmetric part is completely visible even
                //if it is near the border of the transformed image.

                //将极坐标变换扩展到变换模型区域的宽度，以确保不对称部分即使在变换图像的边界附近也是完全可见的。
                HTuple hv_AngleOverlap = (hv_RegionWidth * ((new HTuple(360)).TupleRad())) / hv_PolarWidth;
                HTuple hv_PolarStartAngle = hv_RefAngle - (hv_AngleOverlap / 2);
                HTuple hv_PolarEndAngle = (hv_RefAngle + ((new HTuple(360)).TupleRad())) + (hv_AngleOverlap / 2);
                HTuple hv_PolarAngleRangeExtended = hv_PolarEndAngle - hv_PolarStartAngle;
                HTuple hv_PolarWidthExtended = ((hv_RadiusOuter * hv_PolarAngleRangeExtended)).TupleRound();

                
                ho_PolarTransModelImage = ImageTestIn.PolarTransImageExt(hv_RowFittedCircle,
             hv_ColumnFittedCircle, hv_PolarStartAngle, hv_PolarEndAngle, hv_RadiusInner,
             hv_RadiusOuter, hv_PolarWidthExtended, hv_PolarHeight, "bilinear");

                HRegion ho_PolarTransModelRegion;
                ho_PolarTransModelRegion = AngleRegionRef.PolarTransRegion(hv_RowFittedCircle, hv_ColumnFittedCircle, hv_PolarStartAngle, hv_PolarStartAngle + hv_AngleOverlap,
                    hv_RadiusInner, hv_RadiusOuter, hv_RegionWidth, hv_PolarHeight, "bilinear");

                HTuple hv_Area1;
                HTuple hv_RowRefPolar;
                HTuple hv_ColumnRefPolar;
                HOperatorSet.AreaCenter(ho_PolarTransModelRegion, out hv_Area1, out hv_RowRefPolar,
                    out hv_ColumnRefPolar);

                //以上为模板准备阶段



            }

            return true;
            //  HOperatorSet.PolarTransImageExt(testImage, out ho_PolarTransImage, hv_RowFittedCircle,
            //hv_ColumnFittedCircle, hv_PolarStartAngle, hv_PolarEndAngle, hv_RadiusInner,
            //hv_RadiusOuter, hv_PolarWidthExtended, hv_PolarHeight, "bilinear");

            //  ho_SearchImageReduced.Dispose();
            //  HOperatorSet.Rectangle1Domain(ho_PolarTransImage, out ho_SearchImageReduced,
            //      hv_RowRefPolar - hv_Tolerance, 0, hv_RowRefPolar + hv_Tolerance, hv_PolarWidthExtended - 1);

            //  HImage ho_SearchImageReduced;
            //  ho_SearchImageReduced = ho_PolarTransModelImage.Rectangle1Domain(hv_RowRefPolar - hv_Tolerance, 0, hv_RowRefPolar + hv_Tolerance, hv_PolarWidthExtended - 1);

            //HTuple hv_RowPolar, hv_ColumnPolar;
            //HRegion ho_SelectedRegion;
            //GetRegionPos(ho_SearchImageReduced, out hv_RowPolar, out hv_ColumnPolar, out ho_SelectedRegion);

            //HTuple hv_FinalAngle = ((hv_ColumnPolar - hv_ColumnRefPolar) / (hv_PolarWidthExtended - 1)) * hv_PolarAngleRangeExtended;

            //HTuple hv_FinalAngleDeg = hv_FinalAngle.TupleDeg();

            //HRegion ho_XYTransContour;

            //GetRefImage().GetImageSize(out hv_ImageWidth, out hv_ImageHeight);
            //ho_XYTransContour = ho_SelectedRegion.PolarTransRegionInv(hv_RowFittedCircle, hv_ColumnFittedCircle, hv_PolarStartAngle, hv_PolarEndAngle,
            //    hv_RadiusInner, hv_RadiusOuter, hv_PolarWidthExtended, hv_PolarHeight,
            //    hv_ImageWidth, hv_ImageHeight, "nearest_neighbor");

        }

        private static void GetRegionPos(HImage ho_SearchImageReduced, out HTuple hv_RowPolar, out HTuple hv_ColumnPolar, out HRegion ho_SelectedRegion)
        {
            HRegion ho_Region;
            HTuple hv_UsedThreshold;
            ho_Region = ho_SearchImageReduced.BinaryThreshold("max_separability",
                "light", out hv_UsedThreshold);
            HRegion ho_RegionFillUp;
            ho_RegionFillUp = ho_Region.FillUp();
            ho_Region.Dispose();
            HRegion ho_ConnectedRegions;
            ho_ConnectedRegions = ho_RegionFillUp.Connection();
            ho_RegionFillUp.Dispose();
            ho_SelectedRegion = ho_ConnectedRegions.SelectShapeStd("max_area", 70);
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegion.AreaCenter(out hv_RowPolar,
                out hv_ColumnPolar);
        }

        public override void SerializeCheck()
        {

            if (circleRegion != null && circleRegion.IsInitialized() == false)
            {
                circleRegion = null;
            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }

        public override void ShowResult(HWndCtrl viewCtrl)
        {
            if (Enable == false)
            {
                return;
            }
            if (RuningFinish == false)
            {
                return;
            }

            if (ho_PolarTransModelImage!=null&& ho_PolarTransModelImage.IsInitialized())
            {
                viewCtrl.AddIconicVar(ho_PolarTransModelImage);
            }
            if (RegionAffine != null && SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(RegionAffine);
            }
        }
        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            {
                return ToolBase.ErrDataFlag;
            }
        }
    }
}
