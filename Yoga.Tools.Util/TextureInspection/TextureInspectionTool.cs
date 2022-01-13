using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Common;
using Yoga.ImageControl;
using Yoga.Tools.Matching;

namespace Yoga.Tools.TextureInspection
{
    [Serializable]
    /// <summary>
    /// 印刷检测助手
    /// </summary>
    public class TextureInspectionTool : ToolBase, IToolEnable, IToolRun
    {

        #region 字段
        private static string toolType = "纹理检测";

        public const double AbsThresholdInit = 40;
        public const double VarThresholdInit = 2;

        public const double OpeningRadiusInit = 10;
        public const double LeastAreaInit = 0;
        protected double absThreshold = AbsThresholdInit;
        protected double varThreshold = VarThresholdInit;

        private double openingRadius = OpeningRadiusInit;
        private double leastArea = LeastAreaInit;


        /// <summary>
        /// 印刷检查模型
        /// </summary>
        private HTextureInspectionModel textureInspectionModel = new HTextureInspectionModel();


        /// <summary>
        /// 当前训练模板内部是否有图像数据
        /// </summary>
        private bool haveTrainData = false;

        [NonSerialized]
        private HRegion errRegion;
        [NonSerialized]
        private HRegion errRegionFind;

        [NonSerialized]
        /// <summary>
        /// 训练图像字典列表
        /// </summary>
        private Dictionary<string, HImage> trainImageDic;
        /// <summary>
        /// 训练中图像,不序列化
        /// </summary>
        [NonSerialized]
        private HImage trainImage = new HImage();

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
                return toolType + "\r\n版本:20180524\r\n说明:";
            }
        }
        #endregion
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
        /// <summary>
        /// 当前训练模板内部是否有图像数据
        /// </summary>
        public bool HaveTrainData
        {
            get
            {
                return haveTrainData;
            }
        }
        /// <summary>
        /// 检查完成后的过滤杂质开运算圆半径
        /// </summary>
        public double OpeningRadius
        {
            get
            {
                return openingRadius;
            }

            set
            {
                openingRadius = value;
            }
        }

        /// <summary>
        /// 印刷检查模型
        /// </summary>
        public HTextureInspectionModel TextureInspectionModel
        {
            get
            {
                if (textureInspectionModel == null)
                {
                    textureInspectionModel = new HTextureInspectionModel();
                }
                return textureInspectionModel;
            }
        }

        public HImage TrainImage
        {
            get
            {
                return trainImage;
            }
        }

        #endregion

        /// <summary>
        /// 印刷检测构造
        /// </summary>
        public TextureInspectionTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            ExceptionText = "";

        }

        /// <summary>
        /// 训练检查区域
        /// </summary>
        public  void TrainTextureInspectionModel()
        {

            bool isTrain = false;
            //每次训练都重新准备训练模型

            int width, heigth;
            ImageRefIn.GetImageSize(out width, out heigth);

            ClearTrainedImage();
            textureInspectionModel = new HTextureInspectionModel();
            textureInspectionModel.CreateTextureInspectionModel("basic");

            foreach (HImage trainImage in trainImageDic.Values)
            {
                if (trainImage == null || trainImage.IsInitialized() == false)
                {
                    continue;//取消当前一次循环
                }

                textureInspectionModel.AddTextureInspectionModelImage(trainImage);
            }
            //此处应该添加纹理训练参数
            textureInspectionModel.SetTextureInspectionModelParam("patch_size", 5);
            //textureInspectionModel.SetTextureInspectionModelParam(new HTuple("levels"), new HTuple(2, 3, 4, 5));


            textureInspectionModel.TrainTextureInspectionModel();
            textureInspectionModel.SerializeTextureInspectionModel();
            if (isTrain == false)
            {
                isTrain = true;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (textureInspectionModel != null)
            {
                textureInspectionModel.Dispose();
                textureInspectionModel = null;
            }
            base.Dispose(disposing);
        }
        public bool AddTrainImages(string fileKey)
        {
            if (trainImageDic == null)
            {
                trainImageDic = new Dictionary<string, HImage>();
            }
            if (trainImageDic.ContainsKey(fileKey))
                return false;

            try
            {
                HImage image = new HImage(fileKey);
                trainImageDic.Add(fileKey, image);
            }
            catch (HOperatorException)
            {
                throw new Exception("图像添加失败,请检查路径");
            }
            return true;
        }

        public void RemoveTrainImage()
        {
            trainImageDic.Clear();
            trainImage = null;
        }

        public HImage GetTrainImage(string fileName)
        {
            if ((trainImage == null) && (trainImageDic.ContainsKey(fileName)))
                trainImage = trainImageDic[fileName];

            return trainImage;
        }

        public void RemoveTrainImage(string fileName)
        {
            if (trainImageDic.ContainsKey(fileName))
                trainImageDic.Remove(fileName);

            if (trainImageDic.Count == 0)
                trainImage = null;
        }
        /// <summary>
        /// 检查测试图像
        /// </summary>
        /// <param name="testImage"></param>
        /// <returns></returns>
        public void ApplyTextureInspectionModel(HImage testImage)
        {
            DisposeHobject(errRegion);
            DisposeHobject(errRegionFind);
            errRegion = null;
            errRegionFind = null;

            //HTextureInspectionResult textureInspectionResult;
            HTuple hv_TextureInspectionResultID;
            HObject err;
            HOperatorSet.ApplyTextureInspectionModel(testImage, out err,
            textureInspectionModel.Handle, out hv_TextureInspectionResultID);
           
            //errRegionFind = textureInspectionModel.ApplyTextureInspectionModel(testImage, out textureInspectionResult);
            if (err != null && err.IsInitialized())
            {
                errRegionFind = new HRegion(err);
                errRegion = errRegionFind.Clone();
                HRegion regionTmp;

                if (openingRadius >= 1)
                {
                    regionTmp = errRegion.OpeningCircle(openingRadius);
                    errRegion.Dispose();
                    errRegion = regionTmp;
                }
                if (leastArea >= 1)
                {
                    regionTmp = errRegion.Connection();
                    errRegion.Dispose();
                    errRegion = regionTmp;

                    regionTmp = errRegion.SelectShape("area", "and", leastArea, 99999.0);
                    errRegion.Dispose();
                    errRegion = regionTmp;
                    regionTmp = errRegion.Union1();
                    errRegion.Dispose();
                    errRegion = regionTmp;
                }
            }
        }

        public void SetTrainImage(string fileKey)
        {
            trainImage = trainImageDic[fileKey];
        }
        /// <summary>
        /// 清除训练过程中产生的数据
        /// </summary>
        public void ClearTrainedImage()
        {
            ////清除训练模型内的图形,节约内存
            if (textureInspectionModel != null && textureInspectionModel.IsInitialized())
            {
                try
                {
                    textureInspectionModel.Dispose();

                }
                catch (Exception)
                {


                }
                haveTrainData = false;
            }
        }
        public void Reset()
        {
            //ROIModel = null;
            //currentImgLevel = 1;
            //PyramidROIs = null;
            //PyramidImages = null;
            //ROIImage = null;
            //scaleH = 1;
            //scaleW = 1;
            //createNewModelID = true;

            //tResult.reset();
            //NotifyIconObserver(MatchingStatus.UpdateXLD);
        }




        public override string GetToolType()
        {
            return toolType;
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            return new TextureInspectionToolUnit(this);
        }

        protected override void RunAct()
        {
            base.RunAct();

            this.Result = "NG";
            ApplyTextureInspectionModel(ImageTestIn);

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
                MarkTool(true);
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
            if (errRegionFind != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "yellow");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(errRegionFind);
            }
            if (errRegion != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "red");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(errRegion);
            }
        }

        public override void SerializeCheck()
        {
            if (textureInspectionModel != null && textureInspectionModel.IsInitialized() == false)
            {
                textureInspectionModel = null;
            }
            base.SerializeCheck();
        }
        private void ClearTrainImageData()
        {
            if (trainImageDic != null)
            {
                foreach (var item in trainImageDic)
                {
                    item.Value.Dispose();
                }
            }

            trainImageDic = null;
            if (trainImage != null)
            {
                trainImage = null;
            }
        }
        public override void ClearTestData()
        {
            base.ClearTestData();
            ClearTrainImageData();
        }
        public override void ClearTrainData()
        {
            base.ClearTrainData();
            //ClearTrainedImage();
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
