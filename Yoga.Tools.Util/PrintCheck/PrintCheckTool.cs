using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Common;
using Yoga.ImageControl;
using Yoga.Tools.Matching;

namespace Yoga.Tools.PrintCheck
{
    [Serializable]
    /// <summary>
    /// 印刷检测助手
    /// </summary>
    public class PrintCheckTool : ToolBase, IToolEnable, IToolRun
    {

        #region 字段
        private static string toolType = "瑕疵检测";

        public const double AbsThresholdInit = 40;
        public const double VarThresholdInit = 2;

        public const double OpeningRadiusInit = 0;
        public const double LeastAreaInit = 0;
        protected double absThreshold = AbsThresholdInit;
        protected double varThreshold = VarThresholdInit;

        private double openingRadius = OpeningRadiusInit;
        private double leastArea = LeastAreaInit;
        private bool isSingleImageMode = false;

        /// <summary>
        /// 训练后的平均图像
        /// </summary>
        private HImage meanImage = new HImage();

        /// <summary>
        /// 训练后的可变轮廓图像
        /// </summary>
        private HImage varImage = new HImage();
        /// <summary>
        /// 印刷检查模型
        /// </summary>
        private HVariationModel variationModel = new HVariationModel();

        /// <summary>
        /// 模板图像上的要检查的区域region
        /// </summary>
        private HRegion modelRegion = new HRegion();
        /// <summary>
        /// 当前训练模板内部是否有图像数据
        /// </summary>
        private bool haveTrainData = false;

        [NonSerialized]
        private bool runningFinish;
        [NonSerialized]
        private HRegion errRegion;
        [NonSerialized]
        /// <summary>
        /// 模板匹配助手
        /// </summary>
        private IMatching matchingTool;

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
                return toolType + "\r\n版本:20180421\r\n说明:";
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
        /// 检查灰度误差值 默认±10
        /// </summary>
        public double AbsThreshold
        {
            get
            {
                return absThreshold;
            }

            set
            {

                try
                {
                    absThreshold = value;
                    if (IsSingleImageMode && VarImage != null)
                    {
                        variationModel.PrepareDirectVariationModel(ImageRefIn, VarImage, absThreshold, varThreshold);
                    }
                    else
                    {
                        variationModel.PrepareVariationModel(absThreshold, varThreshold);
                    }
                }
                catch (Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    Util.Notify("模板数据已清除,无法调整此参数");
                }
            }
        }
        /// <summary>
        /// 可变区域权重比例,默认为x2
        /// </summary>
        public double VarThreshold
        {
            get
            {
                return varThreshold;
            }

            set
            {
                try
                {
                    varThreshold = value;
                    if (IsSingleImageMode && VarImage != null)
                    {
                        variationModel.PrepareDirectVariationModel(ImageRefIn, VarImage, absThreshold, varThreshold);
                    }
                    else
                    {
                        variationModel.PrepareVariationModel(absThreshold, varThreshold);
                    }
                }
                catch (Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    Util.Notify("模板数据已清除,无法调整此参数");
                }
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
        /// 模板匹配助手
        /// </summary>
        public IMatching MatchingTool
        {
            get
            {
                if (matchingTool == null)
                {
                    matchingTool = Mat2DManger.MatchingTool;
                }
                return matchingTool;
            }

            set
            {
                matchingTool = value;
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
        /// <summary>
        /// 训练后的平均图像
        /// </summary>
        public HImage MeanImage
        {
            get
            {
                return meanImage;
            }
        }
        /// <summary>
        /// 训练后的可变轮廓图像
        /// </summary>
        public HImage VarImage
        {
            get
            {
                return varImage;
            }
        }
        /// <summary>
        /// 印刷检查模型
        /// </summary>
        public HVariationModel VariationModel
        {
            get
            {
                return variationModel;
            }
        } 

        public HImage TrainImage
        {
            get
            {
                return trainImage;
            }
        }

        public bool IsSingleImageMode
        {
            get
            {
                return isSingleImageMode;
            }

            set
            {
                isSingleImageMode = value;
            }
        }
        #endregion

        /// <summary>
        /// 印刷检查助手事件
        /// </summary>
        [field: NonSerialized()]
        public event EventHandler<PrintCheckEventArgs> PrintCheckEvent;
        /// <summary>
        /// 引发硬刷检查助手事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TriggerPrintCheckEvent(object sender, PrintCheckEventArgs e)
        {
            if (PrintCheckEvent != null)
            {
                PrintCheckEvent(sender, e);
            }
        }
        /// <summary>
        /// 印刷检测构造
        /// </summary>
        public PrintCheckTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            ExceptionText = "";

        }
        /// <summary>
        /// 单张图训练
        /// </summary>
        public void TrainVariationModelSigleImage()
        {
            if (matchingTool == null)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(PrintCheckMessage.ErrNoMatchingTool));
                return;
            }
            bool isTrain = false;
            //每次训练都重新准备训练模型

            int width, heigth;
            ImageRefIn.GetImageSize(out width, out heigth);

            ClearTrainedImage();
            variationModel = new HVariationModel();
            variationModel.CreateVariationModel(width, heigth, "byte", "direct");
            if (modelRegion == null || modelRegion.IsInitialized() == false)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(PrintCheckMessage.ErrNoModelRegion));
                return;
            }
            trainImage = ImageRefIn;
            meanImage = TrainImage.ReduceDomain(ModelRegion);
            varImage = meanImage.SobelAmp("sum_abs", 5);
            VariationModel.PrepareDirectVariationModel(meanImage, varImage, absThreshold, varThreshold);


            if (isTrain == false)
            {
                isTrain = true;
            }
            haveTrainData = true;
            IsSingleImageMode = true;
            if (isTrain)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                        PrintCheckMessage.UpdateTrainResult));
            }
            else
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                       PrintCheckMessage.ErrTrain));
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (variationModel != null)
            {
                variationModel.Dispose();
                variationModel = null;
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 训练检查区域
        /// </summary>
        public async void TrainVariationModel()
        {
            if (matchingTool == null)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(PrintCheckMessage.ErrNoMatchingTool));
                return;
            }
            bool isTrain = false;
            //每次训练都重新准备训练模型

            int width, heigth;
            ImageRefIn.GetImageSize(out width, out heigth);

            ClearTrainedImage();
            variationModel = new HVariationModel();
            variationModel.CreateVariationModel(width, heigth, "byte", "standard");
            if (modelRegion == null || modelRegion.IsInitialized() == false)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(PrintCheckMessage.ErrNoModelRegion));
                return;
            }
            await Task.Run(() =>
            {
                foreach (HImage trainImage in trainImageDic.Values)
                {
                    if (trainImage == null || trainImage.IsInitialized() == false)
                    {
                        //TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                        //    PrintCheckMessage.ErrTrainImage));
                        continue;//取消当前一次循环
                    }
                    ToolBase toolTmp = MatchingTool as ToolBase;
                    toolTmp.Run(trainImage);
                    if (toolTmp.IsOk == false)
                    {
                        //TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                        //   PrintCheckMessage.ErrFindModel));
                        continue;
                    }
                    HHomMat2D mat2d = MatchingTool.TestimageToRefImage(true);
                    if (mat2d == null)
                    {
                        continue;
                    }
                    HImage trainImageAffine = trainImage.AffineTransImage(mat2d, "constant", "false");


                    HImage trainimaageReduce = trainImageAffine.ReduceDomain(ModelRegion);
                    trainImageAffine.Dispose();
                    if (trainimaageReduce == null || trainimaageReduce.IsInitialized() == false)
                    {
                        continue;
                    }
                    variationModel.TrainVariationModel(trainimaageReduce);
                    //DebugImage = trainImageAffine;
                    //TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                    //       PrintCheckMessage.UpdateTrainResult));

                    if (isTrain == false)
                    {
                        isTrain = true;
                        IsSingleImageMode = false;
                    }
                }
            });
            if (isTrain)
            {
                HImage tmpImage = new HImage();
                tmpImage.Dispose();
                meanImage = variationModel.GetVariationModel(out tmpImage);
                varImage = tmpImage.ReduceDomain(ModelRegion);
                meanImage = meanImage.ReduceDomain(ModelRegion);

                variationModel.PrepareVariationModel(absThreshold, varThreshold);

                haveTrainData = true;
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                        PrintCheckMessage.UpdateTrainResult));
            }
            else
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                       PrintCheckMessage.ErrTrain));
            }

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
                ExceptionText = "图像文件添加失败";
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(PrintCheckMessage.ErrReadingImage));
                return false;
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
        public void CompareVariationModel(HImage testImage, out HRegion errRegion, out bool runningFinish)
        {
            errRegion = null;
            runningFinish = false;

            if (matchingTool == null)
            {
                matchingTool = Mat2DManger.MatchingTool;
            }
            if (matchingTool == null)
            {
                ExceptionText = "定位工具未选择";
                return;
            }
            HHomMat2D mat2d = matchingTool.TestimageToRefImage(true);
            if (mat2d == null)
            {
                TriggerPrintCheckEvent(this, new PrintCheckEventArgs(
                     PrintCheckMessage.ErrFindModel));
                ExceptionText = "映射矩阵建立失败";
                return;
            }
            HImage testImageAffine = testImage.AffineTransImage(mat2d, "constant", "false");
            if (modelRegion == null)
            {
                ExceptionText = "模板区域未建立";
                return;
            }
            HImage testImageReduce = testImageAffine.ReduceDomain(modelRegion);
            testImageAffine.Dispose();
            errRegion = VariationModel.CompareExtVariationModel(testImageReduce, "absolute");
            testImageReduce.Dispose();
            if (errRegion != null && errRegion.IsInitialized())
            {
                HHomMat2D mat = matchingTool.RefImageToTestImage(true);

                HRegion regionTmp;

                regionTmp = errRegion.AffineTransRegion(mat, "nearest_neighbor");

                errRegion.Dispose();
                errRegion = regionTmp;

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


            runningFinish = true;
            return;
        }

        public void SetTrainImage(string fileKey)
        {
            trainImage = trainImageDic[fileKey];
            TriggerPrintCheckEvent(this,
                new PrintCheckEventArgs(PrintCheckMessage.UpdateTrainView));
        }
        /// <summary>
        /// 清除训练过程中产生的数据
        /// </summary>
        public void ClearTrainedImage()
        {
            ////清除训练模型内的图形,节约内存
            if (VariationModel != null && VariationModel.IsInitialized())
            {
                try
                {
                    VariationModel.ClearTrainDataVariationModel();

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
            if (settingUnit == null)
            {
                settingUnit = new PrintCheckToolUnit(this);
            }
            return settingUnit;
        }

        protected override void RunAct()
        {
            base.RunAct();

            this.Result = "NG";
            CompareVariationModel(ImageTestIn, out errRegion, out runningFinish);
            if (runningFinish)
            {
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
        }
        [NonSerialized]
        HRegion modelRegionAffine;
        public override void ShowResult(HWndCtrl viewCtrl)
        {
            if (Enable == false)
            {
                return;
            }
            if (matchingTool == null)
            {
                return;
            }
            if (RuningFinish == false)
            {
                return;
            }
            HHomMat2D mat = MatchingTool.RefImageToTestImage(true);

            if (ModelRegion != null && mat != null)
            {
                if (modelRegionAffine != null && modelRegionAffine.IsInitialized())
                {
                    modelRegionAffine.Dispose();
                }
                modelRegionAffine = ModelRegion.AffineTransRegion(mat, "nearest_neighbor");

                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(modelRegionAffine);
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

            if (modelRegion != null && modelRegion.IsInitialized() == false)
            {
                modelRegion = null;
                //ExceptionText = "检查区域未设置";
                //return false;
            }
            if (meanImage != null && meanImage.IsInitialized() == false)
            {
                meanImage = null;
            }
            if (varImage != null && varImage.IsInitialized() == false)
            {
                varImage = null;
            }
            if (variationModel != null && variationModel.IsInitialized() == false)
            {
                variationModel = null;
                //ExceptionText = "模板未训练";
                //return false;
            }
            base.SerializeCheck();
            using (System.IO.Stream objectStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
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
            PrintCheckEvent = null;
        }
        public override void ClearTrainData()
        {
            base.ClearTrainData();
            ClearTrainedImage();
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
