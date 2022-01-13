using System;
using System.Linq;
using HalconDotNet;
using Yoga.ImageControl;
using Yoga.Tools.Matching;
using Yoga.Common;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Yoga.Tools.PretreatImage
{
    [Serializable]
    public class SobelAmpImage
    {
        bool use = false;
        string filterType="sum_abs";
        int size = 3;

        public bool Use
        {
            get
            {
                return use;
            }

            set
            {
                use = value;
            }
        }

        public string FilterType
        {
            get
            {
                return filterType;
            }

            set
            {
                filterType = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }
        public HImage Run(HImage img)
        {

            return img.SobelAmp(FilterType, Size);

        }
    }
    /// <summary>
    /// 中值滤波
    /// </summary>
    [Serializable]
    public class MedianImage
    {
        bool use = false;
        int radius = 1;

        public bool Use
        {
            get
            {
                return use;
            }

            set
            {
                use = value;
            }
        }

        public int Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }
        public HImage Run(HImage img)
        {
          
            return img.MedianImage("circle", Radius, "mirrored");
            
        }
    }
    [Serializable]
    public class EmphasizeImage
    {
        bool use = false;
        int maskWidth = 7;
        int maskHeight = 7;
        double factor = 1.0;

        public bool Use
        {
            get
            {
                return use;
            }

            set
            {
                use = value;
            }
        }

        public int MaskWidth
        {
            get
            {
                return maskWidth;
            }

            set
            {
                maskWidth = value;
            }
        }

        public int MaskHeight
        {
            get
            {
                return maskHeight;
            }

            set
            {
                maskHeight = value;
            }
        }

        public double Factor
        {
            get
            {
                return factor;
            }

            set
            {
                factor = value;
            }
        }

        public HImage Run(HImage img)
        {
            return img.Emphasize(MaskWidth,MaskHeight,Factor);
        }
    }
    [Serializable]
    public class ScaleImage
    {
        bool use = false;
        int offset = 0;
        double scale = 1;

        public bool Use
        {
            get
            {
                return use;
            }

            set
            {
                use = value;
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

        public double Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }
        public HImage Run(HImage img)
        {
            double offsetTmp = Scale * Offset;
            return img.ScaleImage(Scale, offsetTmp);
        }
    }
    [Serializable]
    public class PretreatImageTool : ToolBase, IToolEnable, IToolRun, ICreateImage
    {
        #region 字段

        private static string toolType = "图像预处理";
        SobelAmpImage sobelAmpImagePram;
        ScaleImage scaleImagePram;
        EmphasizeImage emphasizeImagePram;
        MedianImage medianImagePram;
        HImage imageRefOut;

        bool isShowResultImage = false;
        public int changeCount, uiCount;
        [NonSerialized]
        SettingThread settingThread;
        [NonSerialized]
        HRegion regionAffine = null;
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
                return toolType + "\r\n版本:20180426\r\n说明:";
            }
        }

        public HImage ImageRefOut
        {
            get
            {
                return imageRefOut;
            }

            set
            {
                imageRefOut = value;
            }
        }

        public ScaleImage ScaleImagePram
        {
            get
            {
                if (scaleImagePram==null)
                {
                    scaleImagePram = new ScaleImage();
                }
                return scaleImagePram;
            }

            set
            {
                scaleImagePram = value;
            }
        }

        public EmphasizeImage EmphasizeImagePram
        {
            get
            {
                if (emphasizeImagePram==null)
                {
                    emphasizeImagePram = new EmphasizeImage();
                }
                return emphasizeImagePram;
            }

            set
            {
                emphasizeImagePram = value;
            }
        }

        public MedianImage MedianImagePram
        {
            get
            {
                if (medianImagePram==null)
                {
                    medianImagePram = new MedianImage();
                }
                return medianImagePram;
            }

            set
            {
                medianImagePram = value;
            }
        }

        public SettingThread SettingThread
        {
            get
            {
                if (settingThread==null)
                {
                    settingThread = new SettingThread(this);
                }
                return settingThread;
            }

            set
            {
                settingThread = value;
            }
        }

        public SobelAmpImage SobelAmpImagePram
        {
            get
            {
                if (sobelAmpImagePram==null)
                {
                    sobelAmpImagePram = new SobelAmpImage();
                }
                return sobelAmpImagePram;
            }

            set
            {
                sobelAmpImagePram = value;
            }
        }

        public bool IsShowResultImage
        {
            get
            {
                return isShowResultImage;
            }

            set
            {
                isShowResultImage = value;
            }
        }


        #endregion







        #endregion

        public PretreatImageTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new PretreatImageToolUnit(this);
            }
            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }

        protected override void RunAct()
        {

            DisposeHobject(ImageTestOut);         
            //如果图像整个区变换 就缩小一个像素(为了显示时候不会将原图覆盖)
            HImage imagePretreatTmp=null;
            HRegion scaleRegion, scaleRegionErosion;
            
            scaleRegion = ImageTestIn.GetDomain();
            scaleRegionErosion = scaleRegion.ErosionRectangle1(2, 2);
            DisposeHobject(scaleRegion);

            DisposeHobject(regionAffine);

            if ( SearchRegion.IsInitialized()==false)
            {
                imagePretreatTmp = ImageTestIn.ReduceDomain(scaleRegionErosion);
            }
            else
            {
                if (Mat2DManger.UseMat2D && ImageTestIn != ImageRefIn)
                {
                    IMatching matchingTool = Mat2DManger.MatchingTool;
                    if (matchingTool==null)
                    {
                        Util.Notify(string.Format("工具{0}匹配工具不存在!",Name));
                        return;
                    }
                    HHomMat2D mat2d = matchingTool.RefImageToTestImage(true);
                    if (matchingTool != null && mat2d != null && SearchRegion != null)
                    {

                        regionAffine = SearchRegion.AffineTransRegion(mat2d, "nearest_neighbor");
                    }
                    else
                    {
                        return ;
                    }
                }
                else
                {
                    regionAffine = SearchRegion.Clone();
                }
                HRegion regionEnd = scaleRegionErosion.Intersection(regionAffine);

                imagePretreatTmp = ImageTestIn.ReduceDomain(regionEnd);
                DisposeHobject(regionEnd);
            }
            
            DisposeHobject(scaleRegionErosion);
            //中值滤波
            HImage imageTmpMedianImage=null;
            //changeCount++;
            //Util.Notify(string.Format("中值滤波半径{0}计数{1}", MedianImagePram.Radius, changeCount));
            if (MedianImagePram.Use)
            {
                
                imageTmpMedianImage = MedianImagePram.Run(imagePretreatTmp);
            }
            else
            {
                imageTmpMedianImage = imagePretreatTmp;
            }

           
            HImage imageTmpEmphasize=null;
            //图像锐化
            if (EmphasizeImagePram.Use)
            {
                imageTmpEmphasize = EmphasizeImagePram.Run(imageTmpMedianImage);
                DisposeHobject(imageTmpMedianImage);
            }
            else
            {
                imageTmpEmphasize = imageTmpMedianImage;
            }
            HImage imageTmpScale = null;
            //比例增强
            if (ScaleImagePram.Use)
            {
                imageTmpScale = ScaleImagePram.Run(imageTmpEmphasize);
                DisposeHobject(imageTmpEmphasize);
            }
            else
            {
                imageTmpScale = imageTmpEmphasize;
            }

            HImage imageTmpSobelAmp = null;
            if (SobelAmpImagePram.Use)
            {
                imageTmpSobelAmp = SobelAmpImagePram.Run(imageTmpScale);
                DisposeHobject(imageTmpScale);
            }
            else
            {
                imageTmpSobelAmp = imageTmpScale;
            }
            ImageTestOut = imageTmpSobelAmp;

            if (ImageTestIn == ImageRefIn)
            {
                DisposeHobject(imageRefOut);
                imageRefOut = ImageTestOut.Clone();
            }

            if (ImageTestOut != null && ImageTestOut.IsInitialized())
            {
                IsOk = true;
                isRealOk = true;
                Result = "OK";
            }
        }
        public override HImage GetImageResultRefOut()
        {
            return imageRefOut;
        }
        public override void SerializeCheck()
        {
            if (imageRefOut != null && imageRefOut.IsInitialized() == false)
            {
                imageRefOut = null;
            }
            base.SerializeCheck();
            using (System.IO.Stream objectStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }

        public void ShowPretreatImage(HWndCtrl viewCtrl)
        {
            if (ImageTestOut != null)
            {
                viewCtrl.AddIconicVar(ImageTestOut);
            }
        }
        public override void ShowResult(HWndCtrl viewCtrl)
        {
            if (RuningFinish == false)
            {
                return;
            }

            if (ImageTestOut != null&&IsShowResultImage)
            {
                viewCtrl.AddIconicVar(ImageTestOut);
            }
        }

        public override string GetSendResult()
        {
            return string.Empty;
        }
    }
}
