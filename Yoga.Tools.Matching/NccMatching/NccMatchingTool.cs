

using System;
using HalconDotNet;

using Yoga.Tools.Matching;
using Yoga.ImageControl;
using System.Collections.Generic;
using Yoga.Common;

/// <summary>
/// ncc
/// </summary>
namespace Yoga.Tools.NccMatching
{
  public  enum Level
    {
        Lowest = 0,
        Lower,
        Low,
        Normal,
        High,
        Higher,
        Highest
    }
    [Serializable]
    public class NccMatchingTool : ToolBase, IToolRun, IToolEnable, IMatching
    {
      
        #region 字段

        private static string toolType = "灰度匹配";


        private Level accuracy = Level.Normal;
        private double angleExtent = 60;
        // private double angleStep = 0.0175;
        private int numLevels = 4;
        private bool isAutoNumLevels = true;

        private double minScore = 0.5;
        private int numMatches = 1;
        /// <summary>
        /// 创建的模板的参考坐标系
        /// </summary>
        private HTuple refCoordinates;

       
        /// <summary>
        /// 模板图像上的要查找的的区域region
        /// </summary>
        private HRegion modelRegion = new HRegion();

        
        /// <summary>
        /// 查找区域的最大宽度
        /// </summary>
        private double maxLength = 0;
        [NonSerialized]
        private HNCCModel nccModel = new HNCCModel();
        [NonSerialized]
        HTuple resultPos = new HTuple();
        [NonSerialized]
        private HRegion regionShow;
        [NonSerialized]
        private HXLDCont xldCrossShow;
        public HRegion ModelRegion
        {
            get { return modelRegion; }
            set { modelRegion = value; }
        }
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



        public double AngleExtent
        {
            get
            {
                return angleExtent;
            }

            set
            {
                angleExtent = value;
            }
        }

        public int NumLevels
        {
            get
            {
                return numLevels;
            }

            set
            {
                numLevels = value;
            }
        }

        public bool IsAutoNumLevels
        {
            get
            {
                return isAutoNumLevels;
            }

            set
            {
                isAutoNumLevels = value;
                //CreateNccMatchingTool(isAutoNumLevels);
            }
        }

        public double MinScore
        {
            get
            {
                return minScore;
            }

            set
            {
                minScore = value;
            }
        }

        public int NumMatches
        {
            get
            {
                return numMatches;
            }

            set
            {
                numMatches = value;
            }
        }

        public Level Accuracy
        {
            get
            {
                return accuracy;
            }

            set
            {
                accuracy = value;
            }
        }


        #endregion
        public NccMatchingTool(int settingIndex)
        {
            base.settingIndex = settingIndex;

            Min = 0.5;
        }
        public void CreateNccMatchingTool(bool isDetermineParams)
        {
            if (ImageRefIn == null || ImageRefIn.IsInitialized() == false)
            {
                throw new Exception("模板图形未确定");
            }
            if (modelRegion == null || modelRegion.IsInitialized() == false)
            {
                throw new Exception("模板区域未确定");
            }
            HImage reduceImg;
            HTuple angel = new HTuple(angleExtent).TupleRad();
            int chanal = ImageRefIn.CountChannels().I;

            if (chanal == 3)
            {
                HImage imgTmp = ImageRefIn.Rgb1ToGray();
                reduceImg = imgTmp.ReduceDomain(modelRegion);
                imgTmp.Dispose();
            }
            else
            {
                reduceImg = ImageRefIn.ReduceDomain(modelRegion);
            }

            int pyrLevelTmp;
            double angelTmp;

            CalculateNccModelParams(out pyrLevelTmp, out angelTmp);

            if (isAutoNumLevels & isDetermineParams)
            {
                
                //    HTuple name, value;

                //    HOperatorSet.DetermineNccModelParams(reduceImg, "auto", -angel, 2 * angel,
                //"use_polarity", "num_levels", out name, out value);
                NumLevels = pyrLevelTmp;// value[0].I;
                //angleStep = value[1].D;
            }
            if (nccModel != null)
            {
                nccModel.Dispose();
            }
            isCreateTool = false;
            nccModel = new HNCCModel();
            nccModel.CreateNccModel(reduceImg, NumLevels, -angel, 2 * angel, angelTmp, "use_polarity");
            double row, col;
            modelRegion.AreaCenter(out row, out col);
            refCoordinates = new HTuple(row, col, 0);
            //Common.Util.Notify(string.Format("模板r={0:f2}c={1:f2}",row, col));
            reduceImg.Dispose();
            isCreateTool = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (nccModel!=null)
            {
                nccModel.Dispose();
                nccModel = null;
            }
            base.Dispose(disposing);
        }
        public override void LoadTool()
        {
            base.LoadTool();
            if (isCreateTool)
            {
                CreateNccMatchingTool(false);
            }            
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            return new NccMatchingToolUnit(this);
        }

        public override string GetToolType()
        {
            return toolType;
        }
        private void CalculateNccModelParams(out int pyrLevel, out double angleStep)
        {
            HTuple hvRow1, hvColumn1, hvRow2, hvColumn2;

            modelRegion.SmallestRectangle1(out hvRow1, out hvColumn1, out hvRow2, out hvColumn2);

            // calculate pyramid levels
            const int minPyrLevels = 4;
            const int minPixInTopPyramid = 8;
            const int minAreaInTopPyramid = 32;

            int minWH = 0, maxWH = 0;
            if (hvRow2.I - hvRow1.I > hvColumn2.I - hvColumn1.I)
            {
                minWH = hvColumn2.I - hvColumn1.I;
                maxWH = hvRow2.I - hvRow1.I;
            }
            else
            {
                minWH = hvRow2.I - hvRow1.I;
                maxWH = hvColumn2.I - hvColumn1.I;
            }
            maxLength = maxWH;
            pyrLevel = 1;
            while (true)
            {
                minWH /= 2;
                maxWH /= 2;
                if (minWH < minPixInTopPyramid)
                {
                    if (minWH * maxWH > minAreaInTopPyramid)
                    {
                        ++pyrLevel;
                    }
                    break;
                }
                ++pyrLevel;
            }
            pyrLevel = Math.Max(pyrLevel, minPyrLevels);

            // @FixMe: angleStep should be associated with template width.
            // calculate angleStep. degree: [0.5, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5].
            const double maxAngleStep = 0.06125; // the range of angleStep is [0, pi / 16] in Halcon.
            const double minAngleStep = 0.00875;
            const int maxAngleLevel = 6;
            angleStep = (maxAngleStep - minAngleStep) * (maxAngleLevel - (int)Accuracy) / maxAngleLevel + minAngleStep;
        }
        protected override void RunAct()
        {
            base.RunAct();
            HImage reduceImage;
            //if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            //{
            //    GenSearchRegion();
            //}
            if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            {
                reduceImage = ImageTestIn.Clone();
            }
            else
            {
                reduceImage = ImageTestIn.ReduceDomain(SearchRegion);
            }

            int chanal = reduceImage.CountChannels().I;
            if (chanal == 3)
            {
                HImage imgTmp = reduceImage.Rgb1ToGray();
                reduceImage.Dispose();
                reduceImage = imgTmp;
            }

            HTuple row, col, angel, score;
            HTuple angelCreate = new HTuple(angleExtent).TupleRad();
            reduceImage.FindNccModel(nccModel, -angelCreate, 2 * angelCreate, MinScore, NumMatches, 0.5, "true",
            0, out row, out col, out angel, out score);
            reduceImage.Dispose();

            if (score.Length > 0)
            {
                base.Result = score.D.ToString("f2");

                resultPos = new HTuple(row, col, angel);

                //Common.Util.Notify(string.Format("测试r={0:f2}c={1:f2}", row.D, col.D));
            }
            else
            {
                base.Result = "0";
            }
            if (double.Parse(base.Result) > base.Min)
            {
                base.IsOk = true;
                base.isRealOk = true;
            }
            else
            {
                base.IsOk = false;
            }
        }
        public override void SerializeCheck()
        {
            if (modelRegion != null && modelRegion.IsInitialized() == false)
            {
                modelRegion = null;
            }
            if (nccModel != null && nccModel.IsInitialized() == false)
            {
                nccModel = null;
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
            viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            if (SearchRegion != null && SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(SearchRegion);
            }
            if (modelRegion != null
                && modelRegion.IsInitialized()
                && resultPos != null)
            {
                if (regionShow != null && regionShow.IsInitialized())
                {
                    regionShow.Dispose();
                }
                HHomMat2D mat2d = RefImageToTestImage(true);
                regionShow = modelRegion.AffineTransRegion(mat2d, "nearest_neighbor");
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(regionShow);

                double length = 6.0;
                if (maxLength > 0)
                {
                    length = maxLength / 4.0;
                }
                if (xldCrossShow != null && xldCrossShow.IsInitialized())
                {
                    xldCrossShow.Dispose();
                }
                xldCrossShow = new HXLDCont();
                xldCrossShow.GenCrossContourXld(resultPos[0].D,resultPos[1].D,length,resultPos[2].D);
                viewCtrl.AddIconicVar(xldCrossShow);
            }
        }

        public HHomMat2D TestimageToRefImage(bool useAngle)
        {
            HTuple mat = TestToRefMat2D(useAngle);
            HHomMat2D mat2d = null;
            if (mat != null)
            {
                mat2d = new HHomMat2D(mat);
            }
            return mat2d;
        }

        public HTuple TestToRefMat2D(bool useAngle)
        {
            if (refCoordinates == null || resultPos == null)
            {
                return null;
            }

            HTuple mat2d = null;
            double angle = 0;
            if (useAngle)
            {
                angle = resultPos[2];
            }
            HOperatorSet.VectorAngleToRigid(resultPos[0], resultPos[1], angle,
                refCoordinates[0], refCoordinates[1], 0.0, out mat2d);
            return mat2d;
        }

        public HHomMat2D RefImageToTestImage(bool useAngle)
        {

            if (refCoordinates == null || resultPos == null)
            {
                return null;
            }
            HHomMat2D mat2d = new HHomMat2D();
            double angle = 0;
            if (useAngle)
            {
                angle = resultPos[2];
            }
            mat2d.VectorAngleToRigid(refCoordinates[0], refCoordinates[1], 0.0,
                resultPos[0], resultPos[1], angle);

            return mat2d;
        }

        public HTuple GetRefPoint()
        {
            return refCoordinates;
        }

        public HTuple GetMatchingPoint()
        {
            return resultPos;
        }
        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            string dat = ErrDataFlag;
            if (this.IsOk)
            {
                if (IsCalibOut==false)
                {
                    dat = string.Format("{0:f2},{1:f2},{2:f2}",  resultPos[1].D, resultPos[0].D, resultPos[2].D);
                }
                else
                {
                    HTuple rx, ry;
                    if (CalibTool==null)
                    {
                        Util.Notify(string.Format("{0}输出异常,无标定设置数据", Name));
                        dat = NgDataFlag;
                        this.IsOk = false;
                    }
                    else
                    {
                        CalibTool.ImagePointsToWorldPlane(resultPos[1].D, resultPos[0].D, out rx, out ry);
                        dat = string.Format("{0:f2},{1:f2},{2:f2}", rx.D, ry.D, resultPos[2].D);
                    }
                }
            }
            else
            {
                dat = NgDataFlag;
            }
            return dat;
        }
    }
}
