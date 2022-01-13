using System;
using HalconDotNet;
using Yoga.ImageControl;
using Yoga.Tools.Matching;
using System.Collections.Generic;
using Yoga.Common;

namespace Yoga.Tools.ExtractColor
{
    [Serializable]
    public class ExtractColorTool : ToolBase, IToolEnable, IToolRun
    {
        #region 字段

        private static string toolType = "颜色识别";

        private ColorRange colorRange = new ColorRange();

        private HRegion showRegion;
        /// <summary>
        /// 鼠标画出的要创建的roi模板对象
        /// </summary>
        private HRegion modelRegion;


        private List<PointD> colorPointList;

        [NonSerialized]
        private HObject selectRegion;
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
                return toolType + "\r\n版本:20180421\r\n说明:";
            }
        }
        #endregion

        public ColorRange ColorRange
        {
            get
            {
                return colorRange;
            }

            set
            {
                colorRange = value;
            }
        }

        public HRegion ModelRegion
        {
            get
            {
                return modelRegion;
            }

            set
            {
                modelRegion = value;
            }
        }


        #endregion

        public ExtractColorTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }

        public void AddColorPoint(double x,double y)
        {
            PointD point = new PointD(x, y);
            if (colorPointList==null)
            {
                colorPointList=new List<PointD>();
            }
            colorPointList.Add(point);
        }

        public void DelLeastPoint()
        {
            if (colorPointList==null||colorPointList.Count<1)
            {
                return;
            }
            colorPointList.RemoveAt(colorPointList.Count-1);
        }

        public void ResetColorPoint()
        {
            if (colorPointList == null)
            {
                colorPointList = new List<PointD>();
            }
            colorPointList.Clear();
        }
        /// <summary>
        /// 依据模板点来创建点集region
        /// </summary>
        public void GenModelRegion()
        {
            
            if (modelRegion!=null&&modelRegion.IsInitialized())
            {
                modelRegion.Dispose();
               
            }
            modelRegion = new HRegion();
            modelRegion.GenEmptyObj();

            if (colorPointList == null || colorPointList.Count < 1)
            {
                return;
            }

            foreach (var item in colorPointList)
            {
                HRegion regionTmp = new HRegion();
                //regionTmp.GenRegionPoints(item.Y,item.X);
                regionTmp.GenCircle(item.Y, item.X,2);
                HRegion regionAdd= modelRegion.Union2(regionTmp);
                modelRegion.Dispose();

                regionTmp.Dispose();
                modelRegion = regionAdd;
            }
        }
        protected HObject GetExtractColorRegion(bool useMatching)
        {
            HObject regionSelect = null;
            HImage findImage;

            //if (selectRegion == null)
            //{
            //    GenSearchRegion();
            //}
            if (ImageTestIn == null)
            {
                return null;
            }
            if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            {
                findImage = ImageTestIn.CopyImage();
            }
            else
            {
                HRegion regionAffine = null;
                if (useMatching && ImageTestIn != ImageRefIn)
                {
                    IMatching matchingTool = Mat2DManger.MatchingTool;
                    if (matchingTool == null)
                    {
                        Util.Notify(string.Format("工具{0}匹配工具不存在!", Name));
                        return null;
                    }
                    HHomMat2D mat2d = matchingTool.RefImageToTestImage(true);
                    if (matchingTool != null && mat2d !=null&& SearchRegion.IsInitialized())
                    {                        
                        regionAffine = SearchRegion.AffineTransRegion(mat2d, "nearest_neighbor");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    regionAffine = SearchRegion.Clone();
                }
                findImage = ImageTestIn.ReduceDomain(regionAffine);
                regionAffine.Dispose();
            }
            int ch = ImageTestIn.CountChannels();
            if (ch != 3)
            {
                return null;
            }

            HObject imageR = new HObject(), imageG = new HObject(), imageB = new HObject(),
                imageH = new HObject(), imageS = new HObject(), imageV = new HObject();
            //表示训练模式,会依据当前图像来训练到合适的参数
            if (ImageTestIn == ImageRefIn)
            {              
                GenModelRegion();
                if (modelRegion == null || modelRegion.IsInitialized() == false)
                {
                    return null;
                }
                if (modelRegion.Area<1)
                {
                    return null;
                }
                HImage imageRedu = ImageTestIn.ReduceDomain(ModelRegion);

                HOperatorSet.Decompose3(imageRedu, out imageR, out imageG, out imageB);
                HOperatorSet.TransFromRgb(imageR, imageG, imageB,
                    out imageH, out imageS, out imageV, "hsv");

                imageR.Dispose();
                imageB.Dispose();
                imageG.Dispose();
                HTuple range;
                HOperatorSet.MinMaxGray(modelRegion, imageH, 0, out colorRange.Ch1.Min, out colorRange.Ch1.Max, out range);
                HOperatorSet.MinMaxGray(modelRegion, imageS, 0, out colorRange.Ch2.Min, out colorRange.Ch2.Max, out range);
                HOperatorSet.MinMaxGray(modelRegion, imageV, 0, out colorRange.Ch3.Min, out colorRange.Ch3.Max, out range);
                imageRedu.Dispose();

                imageH.Dispose();
                imageS.Dispose();
                imageV.Dispose();
            }
           // else
            {
                HOperatorSet.Decompose3(findImage, out imageR, out imageG, out imageB);
                HOperatorSet.TransFromRgb(imageR, imageG, imageB,
                   out imageH, out imageS, out imageV, "hsv");

                imageR.Dispose();
                imageB.Dispose();
                imageG.Dispose();

                HObject regionH, regionS, regionV, regionTmp;
                HOperatorSet.Threshold(imageH, out regionH, colorRange.Ch1.Min, colorRange.Ch1.Max);
                HOperatorSet.Threshold(imageS, out regionS, colorRange.Ch2.Min, colorRange.Ch2.Max);
                HOperatorSet.Threshold(imageV, out regionV, colorRange.Ch3.Min, colorRange.Ch3.Max);
                HOperatorSet.Intersection(regionH, regionS, out regionTmp);

                HOperatorSet.Intersection(regionTmp, regionV, out regionSelect);
                regionH.Dispose();
                regionS.Dispose();
                regionV.Dispose();

                regionTmp.Dispose();

                

                imageH.Dispose();
                imageS.Dispose();
                imageV.Dispose();
            }
            findImage.Dispose();
            return regionSelect;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new ExtractColorToolUnit(this);
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
            if (selectRegion != null && selectRegion.IsInitialized())
            {
                selectRegion.Dispose();
            }
            selectRegion = GetExtractColorRegion(Mat2DManger.UseMat2D);

            if (selectRegion != null && selectRegion.IsInitialized())
            {
                HTuple area, row, col;
                HOperatorSet.AreaCenter(selectRegion, out area, out row, out col);
                this.Result = area.D.ToString("F2");
            }
            else
            {
                this.Result = "0";
            }
            double areaD = double.Parse(this.Result);
            if (areaD > this.Min && areaD < this.Max)
            {
                this.IsOk = true;
                this.isRealOk = true;
            }
        }
        public override void SerializeCheck()
        {
            if (modelRegion != null && modelRegion.IsInitialized() == false)
            {
                modelRegion = null;
            }
            base.SerializeCheck();
            using (System.IO.Stream objectStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
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
            if (SearchRegion != null && SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                if (Mat2DManger.UseMat2D&& ImageTestIn != ImageRefIn)
                {
                    if (showRegion != null && showRegion.IsInitialized())
                    {
                        showRegion.Dispose();
                    }
                    HHomMat2D mat = Mat2DManger.MatchingTool.RefImageToTestImage(true);
                    if (mat!=null)
                    {
                        showRegion = SearchRegion.AffineTransRegion(mat, "nearest_neighbor");
                        viewCtrl.AddIconicVar(showRegion);
                    }
                   
                }
                else
                {
                    viewCtrl.AddIconicVar(SearchRegion);
                }
            }
            if (selectRegion != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "fill");
                viewCtrl.AddIconicVar(selectRegion);
                viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            }
        }

        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            return "";
        }
    }
    [Serializable]
    public struct ColorRange
    {
        public GrayRange Ch1;
        public GrayRange Ch2;
        public GrayRange Ch3;
    }
    [Serializable]
    public struct GrayRange
    {
        public HTuple Max;
        public HTuple Min;
    }
}
