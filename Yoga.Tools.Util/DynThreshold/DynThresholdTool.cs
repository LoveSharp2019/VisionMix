using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoga.Common;
using Yoga.Common.Helpers;
using Yoga.ImageControl;
using Yoga.Tools.Matching;

namespace Yoga.Tools.DynThreshold
{
    public enum LightDark
    {
        [EnumDescription("not_equal")]
        不同灰度,
        [EnumDescription("equal")]
        相同灰度,
        [EnumDescription("dark")]
        暗色,
        [EnumDescription("light")]
        亮色
    }
    [Serializable]
    public class DynThresholdTool : ToolBase, IToolEnable, IToolRun
    {
        private static string toolType = "脏污检测";

        [NonSerialized]
        HRegion modelRegionAffine;
        private int maskWidth = 20;
        private int maskHeight = 20;
        private int offset = 5;
        private LightDark lightDark = DynThreshold.LightDark.不同灰度;

        private double openingRadius = 0;
        private double closingRadius = 0;
        private double leastArea = 0;
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
        public int MaskWidth
        {
            get
            {
                return maskWidth;
            }

            set
            {
                int dat = value;
                if (dat > 501)
                {
                    dat = 501;
                }
                if (dat < 1)
                {
                    dat = 1;
                }
                maskWidth = dat;
            }
        }

        /// <summary>
        /// 模板图像上的要检查的区域region
        /// </summary>
        private HRegion modelRegion = new HRegion();
        [NonSerialized]
        private HRegion errRegion;
        [NonSerialized]
        /// <summary>
        /// 模板匹配助手
        /// </summary>
        private IMatching matchingTool;
        public int MaskHeight
        {
            get
            {
                return maskHeight;
            }

            set
            {
                int dat = value;
                if (dat > 501)
                {
                    dat = 501;
                }
                if (dat < 1)
                {
                    dat = 1;
                }
                maskHeight = dat;
            }
        }

        public int Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public LightDark LightDark
        {
            get
            {
                return lightDark;
            }

            set
            {
                lightDark = value;
            }
        }

        public double OpeningRadius
        {
            get
            {
                if (openingRadius == 0)
                {
                    openingRadius = 0.5;
                }
                return openingRadius;
            }

            set
            {
                double dat = value;
                if (dat < 0.5)
                {
                    dat = 0.5;
                }
                openingRadius = dat;
            }
        }
        public double ClosingRadius
        {
            get
            {
                if (closingRadius == 0)
                {
                    closingRadius = 0.5;
                }
                return closingRadius;
            }

            set
            {
                double dat = value;
                if (dat < 0.5)
                {
                    dat = 0.5;
                }
                closingRadius = dat;
            }
        }
        /// <summary>
        /// 模板图像上的要检查的区域region
        /// </summary>
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

        public IMatching MatchingTool
        {
            get
            {
                return matchingTool;
            }

            set
            {
                matchingTool = value;
            }
        }

        public double LeastArea
        {
            get
            {
                return leastArea;
            }

            set
            {
                leastArea = value;
            }
        }

        public DynThresholdTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new DynThresholdToolUnit(this);
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
            if (modelRegionAffine != null && modelRegionAffine.IsInitialized())
            {
                modelRegionAffine.Dispose();

            }
            //HImage testImageCrop;
            if (Mat2DManger.UseMat2D && ImageTestIn != ImageRefIn)
            {
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
                if (modelRegion == null)
                {
                    ExceptionText = "模板区域未建立";
                    return;
                }
                modelRegionAffine = modelRegion.AffineTransRegion(mat2d, "nearest_neighbor");
            }
            else
            {
                if (modelRegion == null)
                {
                    ExceptionText = "模板区域未建立";
                    return;
                }


                modelRegionAffine = modelRegion.Clone();
                //testImageAffine.Dispose();
            }
            string lightDarkTmp = EnumHelper.GetDescription(LightDark);

            Wrapper.DynThresholdParam param = new Wrapper.DynThresholdParam();
            param.lightDark = lightDarkTmp;

            //String str = param->lightDark;
            //paramN.lightDark= marshal_as<std::string>(param->lightDark);
            param.offset = offset;
            param.maskWidth = maskWidth;
            param.maskHeight = maskHeight;
            param.closingRadius = closingRadius;
            param.openingRadius = openingRadius;
            param.leastArea =leastArea;
            Wrapper.Fun.MyFindDirty(ImageTestIn, modelRegionAffine, out errRegion, param);

            //Wrapper.Fun.MyFindDirty(ImageTestIn, modelRegionAffine, out errRegion, lightDarkTmp, offset, maskWidth, maskHeight, closingRadius, openingRadius, leastArea);


            if (errRegion != null && errRegion.IsInitialized() && errRegion.Area > 0)
            {
                HRegion errRegionList = errRegion.Connection();
                int errNumber = errRegionList.CountObj();
                errRegionList.Dispose();
                this.Result = errNumber.ToString();
            }
            else
            {
                this.Result = "OK";
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
            using (Stream objectStream = new MemoryStream())
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

            if (modelRegionAffine != null && modelRegionAffine.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(modelRegionAffine);
            }
            if (errRegion != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "fill");
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "red");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(errRegion);
                viewCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            }
        }

        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            return Result;
        }
    }
}
