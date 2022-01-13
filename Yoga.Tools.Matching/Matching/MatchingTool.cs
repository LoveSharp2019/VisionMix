using System;
using HalconDotNet;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Yoga.ImageControl;
using Yoga.Common;
using System.Collections.Generic;
using System.Diagnostics;

namespace Yoga.Tools.Matching
{
    public delegate void MatchingDelegate(MatchingStatus value);
    public delegate void AutoParamDelegate(string value);
    [Serializable]
    /// <summary>
    ///模板匹配助手类
    /// </summary>
    public class MatchingTool : ToolBase, IMatching, IToolEnable,IToolRun
    {

        #region 字段
        
        /// <summary>
        /// 模板匹配参数
        /// </summary>
        public MatchingParam ParameterSet;

        /// <summary>
        /// 模板句柄ID
        /// </summary>
        private HShapeModel shapeModel;

        private HRegion modelRegion;
        /// <summary>
        /// 创建的模板的参考坐标系
        /// </summary>
        private HTuple refCoordinates;
        private double scaleW = 1.0;
        private double scaleH = 1.0;


        private int currentImgLevel = 1;    //mNumLevel
        private int maxPyramidLevel = 6;

        private string toolImageSourceKey = "";

        protected static string toolType = "轮廓模板匹配";

        [NonSerialized]
        private HRegion modelRegionShow;

        [NonSerialized]
        /// <summary>
        /// 模板匹配结果
        /// </summary>
        private MatchingResult matchingResult;
        /// <summary>
        /// 模板匹配结果图形
        /// </summary>
        [NonSerialized]
        HXLDCont detectionContour;
        #region 变量范围字段
        private int contrastLowB;
        private int contrastUpB;
        private double scaleStepLowB;
        private double scaleStepUpB;
        private double angleStepLowB;
        private double angleStepUpB;
        private int pyramLevLowB;
        private int pyramLevUpB;
        private int minContrastLowB;
        private int minContrastUpB;
        #endregion
        [NonSerialized]
        private HImage PyramidImages;
        [NonSerialized]
        private HRegion PyramidROIs;

        [NonSerialized]
        private HHomMat2D homSc2D;

        [NonSerialized]
        /// <summary> 
        /// 依据ROI裁剪后的图像
        /// </summary>
        private HImage ROIImage;

        // 控制标志
        [NonSerialized]
        private bool findAlways;
        [NonSerialized]
        private bool createNewModelID;

        //flags to control the inspection and recognition process
        [NonSerialized]
        public bool onExternalModelID;
        [NonSerialized]
        public bool onTimer = false;

        [NonSerialized]
        /// <summary>
        /// 图像变量更新委托
        /// </summary>
        public MatchingDelegate NotifyIconObserver;
        [NonSerialized]
        /// <summary>
        /// 参数更新委托
        /// </summary>
		public AutoParamDelegate NotifyParamObserver;
        #endregion
        #region 属性

  
        /// <summary>
        /// 匹配用模板图像
        /// </summary>
      

        public  HTuple RefCoordinates
        {
            get
            {
                return refCoordinates;
            }
        }
        public string ToolImageSourceKey
        {
            get
            {
                return toolImageSourceKey;
            }

            set
            {
                toolImageSourceKey = value;
            }
        }
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
        #endregion
        /// <summary>
        /// 构造函数,并参数初始化
        /// </summary>
        public MatchingTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            ParameterSet = new MatchingParam();
            homSc2D = new HHomMat2D();
            matchingResult = new MatchingResult();

            contrastLowB = 0;
            contrastUpB = 255;
            scaleStepLowB = 0.0;
            scaleStepUpB = 19.0 / 1000.0;
            angleStepLowB = 0.0;
            angleStepUpB = 112.0 / 10.0 * Math.PI / 180.0;
            pyramLevLowB = 1;
            pyramLevUpB = 6;
            minContrastLowB = 0;
            minContrastUpB = 30;

            findAlways = false;
            createNewModelID = true;
            shapeModel = new HShapeModel();
            onExternalModelID = false;
            Min = 0.6;
        }

        public MatchingParam GetMatchingParam()
        {
            return this.ParameterSet;
        }
        /********************************************************************/
        /*					methods for test image      					*/
        /********************************************************************/





        /// <summary>
        /// 获取当前测试图像
        /// </summary>
        public HImage GetCurrTestImage()
        {
            return imageTestIn;
        }


        /******************************************************************/
        /*							  auto options						  */
        /******************************************************************/

        /// <summary>
        /// 添加自动设置的属性
        /// </summary>
        public void SetAuto(string mode)
        {
            if (ParameterSet.SetAuto(mode))
                DetermineShapeParameter();
        }

        /// <summary>
        /// 移除自动设置的属性
        /// </summary>
        public bool RemoveAuto(string mode)
        {
            return ParameterSet.RemoveAuto(mode);
        }


        /************************  set matching values ********************/
        /******************************************************************/
        #region 匹配参数设置

        /// <summary>
        /// 对比度设置
        /// </summary>
        public void SetContrast(int val)
        {
            ParameterSet.SetContrast(val);
            minContrastUpB = val;

            InspectShapeModel();

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        ///设置缩放步长
        /// </summary>
        public void SetScaleStep(double val)
        {
            ParameterSet.SetScaleStep(val);

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        /// 设置角度步长
        /// </summary>
        public void SetAngleStep(double val)
        {
            ParameterSet.SetAngleStep(val);

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        /// 设置金字塔等级
        /// </summary>
        public void SetPyramidLevel(double val)
        {
            ParameterSet.SetNumLevel(val);

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        /// 设置最优化方式
        /// </summary>
       	public void SetOptimization(string val)
        {
            ParameterSet.SetOptimization(val);

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        /// 设置最小对比度
        /// </summary>
        public void SetMinContrast(int val)
        {
            ParameterSet.SetMinContrast(val);

            if (ParameterSet.IsOnAuto())
                DetermineShapeParameter();

            createNewModelID = true;
        }

        /// <summary>
        /// 设置度量方式
        /// </summary>
        public void SetMetric(string val)
        {           
            ParameterSet.SetMetric(val);
            createNewModelID = true;
        }

        /// <summary>
        /// 设置最小缩放
        /// </summary>
        public void SetMinScale(double val)
        {
            ParameterSet.SetMinScale(val);
            createNewModelID = true;
        }

        /// <summary>
        /// 设置最大缩放
        /// </summary>
        public void SetMaxScale(double val)
        {
            ParameterSet.SetMaxScale(val);
            createNewModelID = true;
        }

        /// <summary>
        /// 设置起始角度
        /// </summary>
        public void SetStartingAngle(double val)
        {
            ParameterSet.SetStartingAngle(val);
            createNewModelID = true;
        }

        /// <summary>
        /// 设置角度范围
        /// </summary>
        public void SetAngleExtent(double val)
        {
            ParameterSet.SetAngleExtent(val);
            createNewModelID = true;
        }

        /// <summary>
        /// 设置查找最小分数,如果一直查找勾选,就查找模板
        /// </summary>
        public void SetMinScore(double val)
        {
            ParameterSet.SetMinScore(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        /// 设置查找个数,如果一直查找勾选,就查找模板
        /// </summary>
        public void SetNumMatches(int val)
        {
            ParameterSet.SetNumMatches(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        /// 设置贪婪程度,如果一直查找勾选,就查找模板
        /// </summary>
        public void SetGreediness(double val)
        {
            ParameterSet.SetGreediness(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        ///设置最大重叠 ,如果椅子查找勾选,就查找模板
        /// </summary>
        public void SetMaxOverlap(double val)
        {
            ParameterSet.SetMaxOverlap(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        /// 设置亚像素查找模式,如果椅子查找勾选,就查找模板
        /// </summary>
        public void SetSubPixel(string val)
        {
            ParameterSet.SetSubPixel(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        /// S设置最大金字塔级别,如果椅子查找勾选,就查找模板
        /// </summary>
        public void SetLastPyramLevel(int val)
        {
            ParameterSet.SetLastPyramLevel(val);

            if (findAlways)
                DetectShapeModel();
        }

        /// <summary>
        /// 设置一直查找模式
        /// </summary>
        public void SetFindAlways(bool flag)
        {
            findAlways = flag;

            if (findAlways && ImageTestIn != null)
                DetectShapeModel();
        }
        #endregion

        /// <summary>
        /// 如果定义测试图像，则触发模板检测
        /// </summary>
        public bool ApplyFindModel()
        {
            bool success = false;
            if (ImageTestIn != null)
            {
                success = DetectShapeModel();
                
            }
            else if (NotifyIconObserver != null)
            {
                NotifyIconObserver(MatchingStatus.ErrNoTestImage);
            }

            return success;
        }
        /// <summary>
        /// 测试图像2d映射到模板图像,此处需要查找模板成功
        /// </summary>
        /// <returns></returns>
        public HHomMat2D TestimageToRefImage(bool useAngle)
        {
            HTuple mat = TestToRefMat2D(useAngle);
            HHomMat2D mat2d = null;
            if (mat!=null)
            {
                mat2d = new HHomMat2D(mat);
            }
            return mat2d;
        }

        public HTuple TestToRefMat2D(bool useAngle)
        {
            if (matchingResult == null || matchingResult.Count != 1)
            {
                return null;
            }

            HTuple mat2d = null;
            double angle = 0;
            if (useAngle)
            {
                angle = matchingResult.Angle.D;
            }
            HOperatorSet.VectorAngleToRigid(matchingResult.Row.D, matchingResult.Col.D, angle,
               refCoordinates[0], refCoordinates[1], 0, out mat2d);
            return mat2d;
        }
        /// <summary>
        /// 生成原始图像到测试图像的变换矩阵
        /// </summary>
        /// <returns></returns>
        public HHomMat2D RefImageToTestImage(bool useAngle)
        {
            HHomMat2D mat2d = new HHomMat2D();
            if (matchingResult==null|| matchingResult.Count<1)
            {
                return null;
            }
            double angle = 0;
            if (useAngle)
            {
                angle = matchingResult.Angle.D;
            }
            mat2d.VectorAngleToRigid(refCoordinates[0], refCoordinates[1], 0,
                matchingResult.Row.D, matchingResult.Col.D, angle);

            return mat2d;
        }

        /// <summary>
        ///查找结果
        /// </summary>
        public MatchingResult MatchingResults
        {
            get
            {
                if (matchingResult==null)
                {
                    matchingResult = new MatchingResult();
                }
                return matchingResult;
            }
        }

        public HXLDCont DetectionContour
        {
            get
            {
                return detectionContour;
            }
            set
            {
                detectionContour = value;
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

                PyramidROIs = null;
                createNewModelID = true;

                if (modelRegion == null)
                {
                    ROIImage = null;
                    return;
                }
                //图像裁剪
                ROIImage = ImageRefIn.ReduceDomain(modelRegion);

                DetermineStepRanges();

                if (ParameterSet.IsOnAuto())
                    DetermineShapeParameter();

                InspectShapeModel();

            }
        }



        /********************  optimize recognition speed *****************/
        /******************************************************************/

        /// <summary>
        /// 设置识别率模式
        /// </summary>
        public void SetRecogRateOption(int idx)
        {
            ParameterSet.SetRecogRateOption(idx);
        }
        /// <summary>
        /// 设置识别率
        /// </summary>
        public void SetRecogitionRate(int val)
        {
            ParameterSet.SetRecogitionRate(val);
        }

        /// <summary>
        /// 设置速度优化模式
        /// </summary>
        public void SetRecogSpeedMode(string val)
        {
            ParameterSet.SetRecogSpeedMode(val);
        }

        /// <summary>
        /// 设置要查找的模板数目
        /// </summary>
        public void SetRecogManualSelection(int val)
        {
            ParameterSet.SetRecogManualSelection(val);
        }


        /********************************************************************/
        /*                        getter methods                            */
        /********************************************************************/

        /// <summary>
        /// 获取当前图像级别的模板图形
        /// </summary>
        public HXLDCont GetModelContour()
        {
            if (PyramidROIs == null)
                return null;
            if (homSc2D == null)
            {
                homSc2D = new HHomMat2D();
            }
            homSc2D.HomMat2dIdentity();
            homSc2D = homSc2D.HomMat2dScaleLocal(scaleW, scaleH);

            return ((PyramidROIs.SelectObj(currentImgLevel)).
                                           GenContourRegionXld("center")).
                                           AffineTransContourXld(homSc2D);
        }

        /// <summary>
        /// 获取由加载的基于形状的模型文件（.shm）提供的模型图像
        /// </summary>
        public HXLDCont GetLoadedModelContour()
        {
            HTuple row1, col1, row2, col2, row, col;
            HHomMat2D homMat2D = new HHomMat2D();

            try
            {
                if (matchingResult.Contour==null)
                {
                    return null;
                }
                matchingResult.Contour.SmallestRectangle1Xld(out row1, out col1, out row2, out col2);
                row2 = row1.TupleMin();
                col2 = col1.TupleMin();
                row = row2.TupleFloor() - 5;
                col = col2.TupleFloor() - 5;
                homMat2D.HomMat2dIdentity();
                homMat2D = homMat2D.HomMat2dTranslate(-row, -col);

                return homMat2D.AffineTransContourXld(matchingResult.Contour);
            }
            catch (HOperatorException ex)
            {
                ExceptionText = ex.Message;
                Util.WriteLog(this.GetType(), ex);
                if (NotifyIconObserver != null)
                {
                    NotifyIconObserver(MatchingStatus.ErrReadShapeModel);
                }
                return null;
            }
        }

        /// <summary>
        /// 获取当前图像级别的模型区域。
        /// </summary>
        public HRegion GetModelRegion()
        {
            if (PyramidROIs == null)
                return null;

            HRegion reg = PyramidROIs.SelectObj(currentImgLevel);
            return reg.ZoomRegion(scaleW, scaleH);
        }
        /// <summary>
        /// 模板查找重置
        /// </summary>
        public void Reset()
        {
            modelRegion = null;
            currentImgLevel = 1;
            PyramidROIs = null;
            PyramidImages = null;
            ROIImage = null;
            scaleH = 1;
            scaleW = 1;
            createNewModelID = true;
            if (matchingResult != null)
            {
                matchingResult.Reset();
            }


            SearchRegion = null;
            DetectionContour = null;

            System.GC.Collect();
            if (NotifyIconObserver != null)
            {
                NotifyIconObserver(MatchingStatus.UpdateXLD);
            }
        }



        public void InitMatchingResult()
        {
            if (shapeModel == null)
            {
                return;
            }
            if (matchingResult == null)
            {
                matchingResult = new MatchingResult();
            }
            try
            {
                matchingResult.Contour = shapeModel.GetShapeModelContours(1);
            }
            catch { }
        }
        /// <summary>
        ///创建查找模板
        /// </summary>
        public bool CreateShapeModel()
        {
            if (ROIImage == null)
            {
                if (!onTimer)
                {
                    if (NotifyIconObserver != null)
                    {
                        NotifyIconObserver(MatchingStatus.ErrNoModelDefined);
                    }
                }
                return false;
            }

            try
            {
                shapeModel = ROIImage.CreateScaledShapeModel(ParameterSet.PyramidLevel,
                                                        ParameterSet.StartingAngle,
                                                        ParameterSet.AngleExtent,
                                                        ParameterSet.AngleStep,
                                                        ParameterSet.MinScale,
                                                        ParameterSet.MaxScale,
                                                        ParameterSet.ScaleStep,
                                                        ParameterSet.Optimization,
                                                        ParameterSet.Metric,
                                                        ParameterSet.Contrast,
                                                        ParameterSet.MinContrast);
                double row, col;
                modelRegion.AreaCenter(out row, out col);
                refCoordinates = new HTuple(row, col, 0);
            }
            catch (HOperatorException ex)
            {
                Util.WriteLog(this.GetType(), ex);
                if (!onTimer)
                {
                    ExceptionText = ex.Message;
                    if (NotifyParamObserver != null)
                    {
                        NotifyParamObserver(MatchingParam.H_ERR_MESSAGE);
                    }
                }
                return false;
            }
            if (matchingResult == null)
            {
                matchingResult = new MatchingResult();
            }
            matchingResult.Contour = shapeModel.GetShapeModelContours(1);

            createNewModelID = false;
            return true;
        }
        /// <summary>
        /// 查找模板
        /// </summary>
        /// <param name="testImage"></param>
        /// <returns></returns>
        public bool DetectShapeModel(HImage testImage)
        {
            HTuple levels, rtmp;

            rtmp = new HTuple();
            double t2, t1;
            if (createNewModelID && !onExternalModelID)
            {
                if (!CreateShapeModel())
                {
                    return false;
                }
            }
            try
            {
                levels = new HTuple(new int[] {ParameterSet.PyramidLevel,
                                                ParameterSet.LastPyramidLevel});
                if (matchingResult == null)
                {
                    matchingResult = new MatchingResult();
                    matchingResult.Contour = shapeModel.GetShapeModelContours(1);
                }
                t1 = HSystem.CountSeconds();
                testImage.FindScaledShapeModel(shapeModel,
                                            ParameterSet.StartingAngle,
                                            ParameterSet.AngleExtent,
                                            ParameterSet.MinScale,
                                            ParameterSet.MaxScale,
                                            ParameterSet.MinScore,
                                            ParameterSet.NumMatches,
                                            ParameterSet.MaxOverlap,
                                            new HTuple(ParameterSet.Subpixel),
                                            levels,
                                            ParameterSet.Greediness,
                                            out matchingResult.Row,
                                            out matchingResult.Col,
                                            out matchingResult.Angle,
                                            out matchingResult.ScaleRow,
                                            out matchingResult.Score);
                t2 = HSystem.CountSeconds();
                matchingResult.TimeFound = 1000.0 * (t2 - t1);
                matchingResult.Count = matchingResult.Row.Length;
                matchingResult.ScaleCol = matchingResult.ScaleRow;
            }
            catch (HOperatorException ex)
            {
                Util.WriteLog(this.GetType(), ex);
                if (!onTimer)
                {
                    ExceptionText = ex.Message;
                    matchingResult.Count = 0;
                    if (NotifyParamObserver != null)
                    {
                        NotifyParamObserver(MatchingParam.H_ERR_MESSAGE);
                    }

                }
            }
            return true;
        }
        /// <summary>
        /// 在测试图像中查找模板。 如果模型尚未创建或需要创建
        /// （由于用户对GUI组件所作的更改），则首先创建模型。
        /// </summary>
        public bool DetectShapeModel()
        {

            if (ImageTestIn == null)
                return false;

            HImage reduceImage;
            //if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            //{
            //    GenSearchRegion();
            //}
            if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            {
                if (NotifyIconObserver != null)
                {
                    NotifyIconObserver(MatchingStatus.ErrNoSearchRegion);
                }

                reduceImage = ImageTestIn.Clone();
            }
            else
            {
                reduceImage = ImageTestIn.ReduceDomain(SearchRegion);
            }
            if (DetectShapeModel(reduceImage) == false)
            {
                reduceImage.Dispose();
                return false;
            }
            reduceImage.Dispose();
            RuningFinish = true;
            //查找结果更新
            if (NotifyIconObserver != null)
            {
                NotifyIconObserver(MatchingStatus.UpdateDetectiongResult);
            }

            return true;
        }


        /// <summary>
        /// 创建模板图形
        /// </summary>
        public void InspectShapeModel()
        {
            HRegion tmpReg;
            HImage tmpImg;

            if (ROIImage == null)
            {
                if (NotifyIconObserver != null)
                {
                    NotifyIconObserver(MatchingStatus.ErrNoModelDefined);
                }
                return;
            }

            PyramidImages = ImageRefIn.InspectShapeModel(out tmpReg,
                                                      maxPyramidLevel,
                                                      ParameterSet.Contrast);
            tmpImg = ROIImage.InspectShapeModel(out PyramidROIs,
                                               maxPyramidLevel,
                                               ParameterSet.Contrast);
            if (NotifyIconObserver != null)
            {
                NotifyIconObserver(MatchingStatus.UpdateXLD);
                NotifyIconObserver(MatchingStatus.UpdatePyramid);
            }
        }

        /// <summary>
        /// 对裁剪后的图像做步长确定
        /// </summary>
        public void DetermineStepRanges()
        {
            double vald = 0.0;
            HTuple paramValue = new HTuple();
            HTuple paramList = new HTuple();
            string[] paramRange = { "scale_step", "angle_step" };

            if (ROIImage == null)
            {
                if (NotifyIconObserver != null)
                {
                    NotifyIconObserver(MatchingStatus.ErrNoModelDefined);
                }
                return;
            }

            try
            {
                //参数定义
                paramList = ROIImage.DetermineShapeModelParams(ParameterSet.PyramidLevel,
                                                            ParameterSet.StartingAngle,
                                                            ParameterSet.AngleExtent,
                                                            ParameterSet.MinScale,
                                                            ParameterSet.MaxScale,
                                                            ParameterSet.Optimization,
                                                            ParameterSet.Metric,
                                                            ParameterSet.Contrast,
                                                            ParameterSet.MinContrast,
                                                            paramRange,
                                                            out paramValue);
            }
            catch (HOperatorException ex)
            {
                ExceptionText = ex.Message;
                Util.WriteLog(this.GetType(), ex);
                if (NotifyParamObserver != null)
                {
                    NotifyParamObserver(MatchingParam.H_ERR_MESSAGE);
                }
                return;
            }

            for (int i = 0; i < paramList.Length; i++)
            {
                switch (paramList[i].S)
                {
                    case MatchingParam.AUTO_ANGLE_STEP:
                        vald = paramValue[i].D;

                        angleStepUpB = vald * 5.0;
                        angleStepLowB = vald / 5.0;
                        ParameterSet.AngleStep = vald;
                        if (NotifyParamObserver != null)
                        {
                            NotifyParamObserver(MatchingParam.RANGE_ANGLE_STEP);
                        }
                        break;
                    case MatchingParam.AUTO_SCALE_STEP:
                        vald = paramValue[i].D;

                        scaleStepUpB = vald * 5.0;
                        scaleStepLowB = vald / 5.0;
                        ParameterSet.ScaleStep = vald;
                        if (NotifyParamObserver != null)
                        {
                            NotifyParamObserver(MatchingParam.RANGE_SCALE_STEP);
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 对裁剪后的图像确定形状模型的参数
        /// </summary>
        public void DetermineShapeParameter()
        {
            double vald;
            int vali, count;
            HTuple paramValue = new HTuple();
            HTuple paramList = new HTuple();

            if (ROIImage == null)
            {
                if (NotifyIconObserver != null)
                {
                    NotifyIconObserver(MatchingStatus.ErrNoModelDefined);
                }
                return;
            }

            try
            {
                paramList = ROIImage.DetermineShapeModelParams(ParameterSet.PyramidLevel,
                                                             ParameterSet.StartingAngle,
                                                             ParameterSet.AngleExtent,
                                                             ParameterSet.MinScale,
                                                             ParameterSet.MaxScale,
                                                             ParameterSet.Optimization,
                                                             ParameterSet.Metric,
                                                             ParameterSet.Contrast,
                                                             ParameterSet.MinContrast,
                                                             ParameterSet.GetAutoParList(),
                                                             out paramValue);
            }
            catch (HOperatorException ex)
            {
                ExceptionText = ex.Message;
                Util.WriteLog(this.GetType(), ex);
                if (NotifyParamObserver != null)
                {
                    NotifyParamObserver(MatchingParam.H_ERR_MESSAGE);
                }
                return;
            }

            count = paramList.Length;

            for (int i = 0; i < count; i++)
            {
                switch (paramList[i].S)
                {
                    case MatchingParam.AUTO_ANGLE_STEP:
                        vald = paramValue[i].D;

                        if (vald > angleStepUpB)
                            vald = angleStepUpB;
                        else if (vald < angleStepLowB)
                            vald = angleStepLowB;

                        ParameterSet.AngleStep = vald;
                        break;
                    case MatchingParam.AUTO_CONTRAST:
                        vali = paramValue[i].I;

                        if (vali > contrastUpB)
                            vali = contrastUpB;
                        else if (vali < contrastLowB)
                            vali = contrastLowB;

                        minContrastUpB = vali;
                        ParameterSet.Contrast = vali;

                        InspectShapeModel();
                        break;
                    case MatchingParam.AUTO_MIN_CONTRAST:
                        vali = paramValue[i].I;

                        if (vali > minContrastUpB)
                            vali = minContrastUpB;
                        else if (vali < minContrastLowB)
                            vali = minContrastLowB;

                        ParameterSet.MinContrast = vali;
                        break;
                    case MatchingParam.AUTO_NUM_LEVEL:
                        vali = paramValue[i].I;

                        if (vali > pyramLevUpB)
                            vali = pyramLevUpB;
                        else if (vali < pyramLevLowB)
                            vali = pyramLevLowB;

                        ParameterSet.PyramidLevel = vali;
                        break;
                    case MatchingParam.AUTO_OPTIMIZATION:
                        ParameterSet.Optimization = paramValue[i].S;
                        break;
                    case MatchingParam.AUTO_SCALE_STEP:
                        vald = paramValue[i].D;

                        if (vald > scaleStepUpB)
                            vald = scaleStepUpB;
                        else if (vald < scaleStepLowB)
                            vald = scaleStepLowB;

                        ParameterSet.ScaleStep = vald;
                        break;
                    default:
                        break;
                }
                if (NotifyParamObserver != null)
                {
                    NotifyParamObserver(paramList[i].S);
                }

            }

            if (count != 0)
                createNewModelID = true;
        }

        /// <summary>
        /// 获取角度/比例步长的范围
        /// </summary>
        public int[] GetStepRange(string param)
        {
            int[] range = new int[2];

            switch (param)
            {
                case MatchingParam.RANGE_ANGLE_STEP:
                    range[0] = (int)(angleStepLowB * 10.0 * 180.0 / Math.PI); //low
                    range[1] = (int)(angleStepUpB * 10.0 * 180.0 / Math.PI);  //up
                    break;
                case MatchingParam.RANGE_SCALE_STEP:
                    range[0] = (int)(scaleStepLowB * 1000.0); //low
                    range[1] = (int)(scaleStepUpB * 1000.0);  //up
                    break;
                default:
                    break;
            }
            return range;
        }

        public void ResetMatchingResult()
        {
            if (matchingResult!=null)
            {
                matchingResult.Reset();
            }
        }

        protected override void RunAct()
        {
            base.RunAct();
            InitMatchingResult();
            matchingResult.Reset();
            DetectShapeModel();
            if (matchingResult.Score != null && matchingResult.Score.Length > 0)
            {
                base.Result = matchingResult.Score.D.ToString("f2");
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
        public void ShowMatchingResult(HWndCtrl viewCtrl)
        {
            if (matchingResult != null)
            {
                DetectionContour = matchingResult.GetDetectionResults();
            }

            if (detectionContour != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(detectionContour);
            }
            if (SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(SearchRegion);
            }
        }
        public void ShowTrainResult(HWndCtrl viewCtrl)
        {
            if (ModelRegion != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(ModelRegion);
            }
            if (SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(SearchRegion);
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
            if (viewCtrl != null)
            {
                if (MatchingResults.Count>0)
                {
                    HHomMat2D mat = RefImageToTestImage(true);
                    DisposeHobject(modelRegionShow);
                    bool xx = modelRegion.IsInitialized();
                    modelRegionShow = ModelRegion.AffineTransRegion(mat, "nearest_neighbor");
                    viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                    viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                    viewCtrl.AddIconicVar(modelRegionShow);
                }
                ShowMatchingResult(viewCtrl);
            }
        }
        public override void SerializeCheck()
        {

            if (createNewModelID && !onExternalModelID)
            {
                CreateShapeModel(); 
            }

            if (shapeModel != null && shapeModel.IsInitialized() == false)
            {
                shapeModel = null;
            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit==null)
            {
                settingUnit = new MatchingToolUnit(this);
            }
            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }
        public override void ClearTestData()
        {
            base.ClearTestData();
            NotifyIconObserver = null;
            NotifyParamObserver = null;
        }
        public override void Close()
        {
            try
            {
                base.Close();
                //DisposeHobject(trainingImage);
                ParameterSet = null;
                if (shapeModel != null && shapeModel.IsInitialized())
                {
                    shapeModel.Dispose();
                }
                refCoordinates = null;

                DisposeHobject(modelRegion);
                DisposeHobject(ROIImage);
                NotifyIconObserver = null;
                NotifyParamObserver = null;
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("匹配工具删除异常");
            }

        }

        public HTuple GetRefPoint()
        {
            return refCoordinates;
        }

        public HTuple GetMatchingPoint()
        {
            MatchingResult result =matchingResult;
            if (result.Count==0)
            {
                return null;
            }
            HTuple resultPos = new HTuple(result.Row, result.Col, result.Angle);
            return resultPos;
        }

        public HImage GetTrainingImage()
        {
            return ImageRefIn;
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
                string pos = "";
                if (IsCalibOut==false)
                {
                    for (int i = 0; i < MatchingResults.Score.Length; i++)
                    {
                        pos += string.Format(",{0:f2},{1:f2},{2:f2},{3:f2}",
                            MatchingResults.Col[i].D,
                            MatchingResults.Row[i].D,
                            MatchingResults.Angle[i].D,
                            MatchingResults.Score[i].D);
                    }
                    dat = string.Format("{0}{1}", MatchingResults.Score.Length, pos);
                }
               else
                {
                    if (CalibTool==null)
                    {
                        Util.Notify(string.Format("{0}输出异常,无标定设置数据",Name));
                        this.IsOk = false;
                        dat = NgDataFlag;
                    }
                    else
                    {
                        HTuple rx, ry;
                        CalibTool.ImagePointsToWorldPlane(MatchingResults.Col, MatchingResults.Row, out rx, out ry);
                        for (int i = 0; i < MatchingResults.Score.Length; i++)
                        {
                            pos += string.Format(",{0:f2},{1:f2},{2:f2},{3:f2}",
                                rx[i].D,
                                ry[i].D,
                                MatchingResults.Angle[i].D,
                                MatchingResults.Score[i].D);
                        }
                        dat = string.Format("{0}{1}", MatchingResults.Score.Length, pos);
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

