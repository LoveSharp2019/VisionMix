
using HalconDotNet;
using System.Collections.Generic;
using Yoga.ImageControl;
using System;
using Yoga.Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

namespace Yoga.Tools.Metrology
{
    /// <summary>
    /// 与UI交互的委托
    /// </summary>
    /// <param name="value"></param>
    public delegate void MetrologyDelegate(MetrologyMessage value);
    /// <summary>
    /// 测量方法
    /// </summary>
    public enum MetrologyMethod
    {
        直线到直线,
        直线到圆弧,
        圆弧到圆弧,
        圆弧半径,
        圆心获取,
        直线拟合,
        直线角度,
        腰孔长,
        矩形中心
    }

    [Serializable]
    /// <summary>
    /// 二维测量控制类
    /// </summary>
    public class MetrologyTool : ToolBase, IToolEnable, IToolRun
    {
        #region 字段及属性
        private static string toolType = "几何测量";

        MetrologyMethod metrologyMethod = MetrologyMethod.直线到直线;

        /// <summary>
        /// 选择的ROI的序号
        /// .</summary>
        public int ActivedRoiIndex;
        /// <summary>二维测量参数集合
        /// </summary>
        public List<MetrologyParam> ParamList = new List<MetrologyParam>();

        bool isHandleMethod = false;
        private double scale = 1.0;
        private double offset = 0;
        /// <summary>用于测量的图片对象宽度.
        /// </summary>
        public int Width;
        /// <summary>用于测量的图片对象高度.
        /// </summary>
        public int Height;

        /// <summary> 是否标定的标志.
        /// </summary>
        public bool IsCalibValid { get; private set; }

        /// <summary>是否转换到世界坐标系标志
        /// </summary>
        public bool IsToWorldCoord { set; get; }

        /// <summary>
        /// 相机参数.
        /// </summary>
        private HTuple camParameter;

        /// <summary>
        /// 相机位姿参数.
        /// </summary>
        private HTuple camPose;

        /// <summary>
        /// 参考点
        /// </summary>
        private HTuple refSystem = new HTuple(0).TupleConcat(0).TupleConcat(0);
        /// <summary>
        /// 转换到世界坐标系的单位.
        /// </summary>
        public string Unit { set; get; }

        /// <summary>
        /// halcon二维测量句柄.
        /// </summary>
        HMetrologyModel handle;


        [NonSerialized]
        /// <summary>二维测量委托
        /// </summary>
        public MetrologyDelegate NotifyMetrologyObserver;
        [NonSerialized]
        HImage trangtransformImg;
        [NonSerialized]
        HRegion regionMeasure;
        [NonSerialized]
        HImage trangtransformImgShow;
        [NonSerialized]
        HXLDCont xldRect2;
        [NonSerialized]
        HTuple resultData;
        [NonSerialized]
        /// <summary>
        /// 测量结果的XLD图形.
        /// </summary>
        HXLDCont resultXLD = new HXLDCont();
        [NonSerialized]
        /// <summary>
        /// 测量区域XLd
        /// </summary>
        HXLDCont measureXld = new HXLDCont();
        [NonSerialized]
        /// <summary>
        /// 测量结果(像素坐标)
        /// </summary>
        List<MetrologyResult> resultDataList = new List<MetrologyResult>();
        [NonSerialized]
        /// <summary>
        /// 测量结果(世界坐标),未标定就是像素坐标
        /// </summary>
        List<MetrologyResult> resultDataWorldList = new List<MetrologyResult>();
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


        public void SetMethodByHandle(MetrologyMethod method)
        {
            metrologyMethod = method;
            isHandleMethod = true;
        }
        public MetrologyMethod MetrologyMethod
        {
            get
            {
                return metrologyMethod;
            }

            set
            {
                metrologyMethod = value;
            }
        }

        #endregion
        /// <summary>
        /// 二维测量构造函数
        /// </summary>
        public MetrologyTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            //异常信息清空
            ExceptionText = "";
            //ROI控制类指定
            //roiController = cRoi;
            //ROIList = cRoi.GetROIList();
            //标定参数初始化设置为无
            IsCalibValid = false;
            //默认设置为无选中ROI的序号-1
            ActivedRoiIndex = -1;
        }


        #region 参数设置

        /// <summary>设置测量矩形长度1
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetMeasureLength1(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].Length1 = val;
            CreateMetrologyModel();
        }
        /// <summary>高斯平滑参数设置
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetSigma(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].Sigma = val;
            CreateMetrologyModel();
        }
        /// <summary>
        /// 明暗变化设置
        /// </summary>
        /// <param name="val"></param>
        public void SetTransition(string val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].Transition = val;
            CreateMetrologyModel();
        }

        public void SetMeasureSelect(string val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].MeasureSelect = val;
            CreateMetrologyModel();
        }
        //最小分数设置
        public void SetMinScore(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].MinScore = val;
            CreateMetrologyModel();
        }
        /// <summary>
        /// 设置测量矩形长度2
        /// </summary>
        /// <param name="val"></param>
        public void SetMeasureLength2(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].Length2 = val;
            CreateMetrologyModel();
        }

        public void SetNumMeasures(int val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].NumMeasures = val;
            CreateMetrologyModel();
        }
        /// <summary>设置最小边缘灰度值幅度
        /// 
        /// </summary>
        /// <param name="val"></param>
        public void SetMinEdgeAmpl(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].Threshold = val;
            CreateMetrologyModel();
        }
        public void SetImageScale(double val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].SacleImagePretreatment.Scale = val;
            CreateMetrologyModel();
        }
        public void SetImageOffset(int val)
        {
            if (ActivedRoiIndex == -1)
                return;
            ParamList[ActivedRoiIndex].SacleImagePretreatment.Offset = val;
            CreateMetrologyModel();
        }
        /// <summary>
        /// 设置测量参考系
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="angle"></param>
        public void SetRefSystem(double row, double col, double angle)
        {
            SetRefSystem(new HTuple(row).TupleConcat(col).TupleConcat(angle));

        }
        #endregion

        #region 参数获取

        /// <summary>获取测量结果(世界坐标)</summary>
        public List<MetrologyResult> ResultDataWorldList
        {
            get
            {

                if (resultDataWorldList == null)
                {
                    resultDataWorldList = new List<MetrologyResult>();
                }
                return resultDataWorldList;
            }
        }
        /// <summary>获取测量结果(像素坐标)</summary>
        public List<MetrologyResult> ResultDataList
        {
            get
            {
                if (resultDataList == null)
                {
                    resultDataList = new List<MetrologyResult>();
                }
                return resultDataList;
            }

        }

        /// <summary>
        /// 获取边缘测量结果图形
        /// </summary>
        public HXLDCont ResultsXLD
        {
            get
            {
                if (resultXLD == null)
                {
                    resultXLD = new HXLDCont();
                }
                return resultXLD;
            }
            protected set
            {
                resultXLD = value;
            }
        }
        /// <summary>
        /// 获取测量区域xld
        /// </summary>
        public HXLDCont MeasureXLD
        {
            get
            {
                if (measureXld == null)
                {
                    measureXld = new HXLDCont();
                }
                return measureXld;
            }

            protected set
            {
                measureXld = value;
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

        public double Offset
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
        /// <summary>
        /// 是否已经手动指定测量方法
        /// </summary>
        public bool IsHandleMethod
        {
            get
            {
                return isHandleMethod;
            }
        }

        public HTuple ResultData
        {
            get
            {
                return resultData;
            }
        }


        /// <summary>组合测量结果数据显示在结果表中
        /// </summary>
        /// <returns>测量结果信息集合</returns>
        public List<HTuple> GetMeasureTableData()
        {
            MetrologyResult resultData;
            List<HTuple> table = new List<HTuple>();
            //如果无测量对象或者激活的rio无就返回一个空的集合
            if (ROIList == null || ROIList.Count == 0 || ActivedRoiIndex == -1)
                return table;
            //获取当前激活的ROI的测量结果数据

            if (ResultDataWorldList.Count > ActivedRoiIndex)
            {
                resultData = ResultDataWorldList[ActivedRoiIndex];
                HTuple data = resultData.GetData();
                if (data == null)
                {
                    return null;
                }
                if (ROIList[ActivedRoiIndex].ROIType == ROIType.Line)
                {

                    table.Add(data[0]);
                    table.Add(data[1]);

                    table.Add(data[2]);
                    table.Add(data[3]);
                }
                else if ((ROIList[ActivedRoiIndex]).ROIType == ROIType.CircleArc)
                {
                    table.Add(data[0]);
                    table.Add(data[1]);

                    table.Add(data[2]);
                }
            }
            return table;
        }

        /// <summary> 撰写要显示在结果表中的测量结果类型集。 类型由用户确定，哪些项显示在GUI前端中的依据相应标志
        /// </summary>
        /// <returns>测量数据表头集合</returns>
        public List<string> GetMeasureResultComposition()
        {
            List<string> composition = new List<string>();
            if (ActivedRoiIndex == -1)
            {
                return null;
            }
            //边缘对方式测量
            if ((ROIList[ActivedRoiIndex]).ROIType == ROIType.Line)
            {
                composition.Add("起点行");
                composition.Add("起点列");
                composition.Add("终点行");
                composition.Add("终点列");
            }
            else if ((ROIList[ActivedRoiIndex]).ROIType == ROIType.CircleArc)
            {
                composition.Add("圆心行");
                composition.Add("圆心列");
                composition.Add("半径");
            }

            return composition;
        }
        /// <summary>
        /// 获取测量对象ROI中一点对应的灰度值集合(如直线为中点对应宽度切线线段)
        /// </summary>
        /// <returns></returns>
        public double[] GetMeasureProjection()
        {
            if (ActivedRoiIndex == -1)
                return null;
            else
            {

                ROI roitmp = ROIList[ActivedRoiIndex];
                if (roitmp.ROIType == ROIType.Line)
                {
                    HTuple grayVal = new HTuple();
                    HTuple linePoint = roitmp.GetModelData();
                    using (HRegion region = new HRegion())
                    {
                        region.GenRegionLine(linePoint[0].D, linePoint[1].D, linePoint[2].D, linePoint[3].D);
                        region.GrayProjections(ImageRefIn, "rectangle", out grayVal);
                        return grayVal.ToDArr();
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        /// <summary>依据文件名称加载相机参数数据</summary>
        /// <param name="file"> *.cal 文件路径</param>
        public void SetCamParFile(string file)
        {
            ExceptionText = "";
            HTuple camParameter = new HTuple();
            //读取文件
            try
            {
                camParameter = HMisc.ReadTuple(file);
            }
            catch (HOperatorException ex)
            {
                //标定标志清空
                IsCalibValid = false;
                //异常信息获取并抛出
                ExceptionText = ex.Message;
                throw (ex);
            }
            SetCamParameter(camParameter);
        }
        /// <summary>
        /// 设置相机参数并更新测量
        /// </summary>
        /// <param name="camParameter"></param>
        public void SetCamParameter(HTuple camParameter)
        {
            this.camParameter = camParameter;
            //相机参数及位姿参数均有效则表示标定有效
            if (this.camParameter == null || !(this.camParameter.Length > 0) || camPose == null || !(camPose.Length > 0))
                IsCalibValid = false;
            else
                IsCalibValid = true;
            //标定有效就触发更新测量结果
            if (IsCalibValid)
                UpdateExecute();
        }
        /// <summary>
        /// 依据htuple设置相机位置参数并更新测量
        /// </summary>
        /// <param name="camPose"></param>
        public void SetCamPose(HTuple camPose)
        {
            this.camPose = camPose;
            //相机参数及位姿参数均有效则表示标定有效
            if (camParameter == null || !(camParameter.Length > 0) || this.camPose == null || !(this.camPose.Length > 0))
                IsCalibValid = false;
            else
                IsCalibValid = true;

            //标定有效就触发更新测量结果
            if (IsCalibValid)
                UpdateExecute();
        }
        /// <summary>依据文件路径加载相机位姿参数数据</summary>
        /// <param name="file">*.dat文件路径</param>
        public void SetCamPoseFile(string file)
        {
            ExceptionText = "";
            HTuple camPose = new HTuple();
            //读取位姿文件
            try
            {
                HOperatorSet.ReadPose(new HTuple(file), out camPose);
            }
            catch (HOperatorException ex)
            {
                IsCalibValid = false;
                ExceptionText = ex.Message;
                throw (ex);
            }
            SetCamPose(camPose);
        }

        /// <summary>
        /// 在测量对象中添加新的测量参数
        /// </summary>
        public void AddMeasureObject()
        {
            MetrologyParam parm = new MetrologyParam();
            parm.Init();
            ParamList.Add(parm);

            //触发更新测量实例
            if (NotifyMetrologyObserver != null)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateMetrology);
            }
        }
        /// <summary>更新测量结果。
        /// </summary>
        public void UpdateExecute()
        {
            //更新测量结果
            UpdateResults(ImageRefIn, refSystem);
            if (NotifyMetrologyObserver != null)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateMetrology);
            }
        }
        /// <summary>清除测量句柄集合并释放所有用过的资源
        /// </summary>
        public void RemoveAllMeasureObjects()
        {
            ClearMeasurement();
            ActivedRoiIndex = -1;
            if (NotifyMetrologyObserver != null)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateRemoveObject);
            }
        }

        /// <summary>移除当前要删除的ROI对象所创建的测量对象
        /// </summary>
        public void RemoveMeasureObjectActIdx(int delROIIdx)
        {
            //获取序号
            //int delROIIdx = roiController.getDelROIIdx();
            //删除对象
            RemoveMeasureObjectIdx(delROIIdx);
            //更新显示
            if (NotifyMetrologyObserver != null)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateRemoveObject);
            }
        }

        /// <summary>依据序号清除测量对象并释放资源
        /// </summary>
        private void RemoveMeasureObjectIdx(int DelIdx)
        {
            ParamList.RemoveAt(DelIdx);
            //重新创建测量模型
            CreateMetrologyModel();
            ActivedRoiIndex = -1;
        }

        /// <summary>修改当前选择的ROI序号
        /// </summary>
        public void ChangeActiveROI(int activeROIIndex)
        {
            ActivedRoiIndex = activeROIIndex;
        }
        /// <summary>
        /// 创建一个测量对象,依据ROI对象及控制参数
        /// </summary>
        public void CreateMetrologyModel()
        {
            //System.Diagnostics.Debug.WriteLine("创建二维测量");
            //释放当前测量资源
            if (handle != null)
            {
                handle.Dispose();
            }

            //对测量助手的异常信息清除
            ExceptionText = "";
            //生成测量模型
            handle = new HMetrologyModel();
            //设置测量长宽
            handle.SetMetrologyModelImageSize(Width, Height);


            try
            {
                int count = ParamList.Count;
                for (int i = 0; i < count; i++)
                {
                    //获取ROI
                    ROI roiTmp = ROIList[i];
                    //获取roi坐标信息
                    HTuple mROICoord = roiTmp.GetModelData();
                    switch (roiTmp.ROIType)
                    {
                        case ROIType.Line:
                            ParamList[i].Index = handle.AddMetrologyObjectLineMeasure(
                                mROICoord[0],
                                mROICoord[1],//起点
                                mROICoord[2],
                                mROICoord[3],//终点
                                ParamList[i].Length1,
                                ParamList[i].Length2,
                               ParamList[i].Sigma,
                               ParamList[i].Threshold,
                                new HTuple(),//参数名称
                                new HTuple());//参数值
                            break;
                        case ROIType.CircleArc:
                            ParamList[i].Index = handle.AddMetrologyObjectCircleMeasure(
                                mROICoord[0], mROICoord[1],//坐标
                                mROICoord[2],//半径
                                ParamList[i].Length1,
                                ParamList[i].Length2,
                                ParamList[i].Sigma,
                                ParamList[i].Threshold,
                                 (new HTuple("start_phi")).TupleConcat("end_phi"),
                                 (new HTuple(mROICoord[3].D)).TupleConcat(mROICoord[3].D + mROICoord[4].D));
                            break;
                    }


                    //将其他设置参数应用到测量中
                    ParamList[i].ApplyMetrologyParam(handle);
                }

            }
            catch (HOperatorException ex)
            {
                //图像变量释放
                ResultsXLD.Dispose();
                //mMeasureRegion.Dispose();
                MeasureXLD.Dispose();
                ExceptionText = ex.Message;
                Util.WriteLog(this.GetType(), ex);
                //清除测量结果数据
                ClearResultData();
                return;
            }
            ////更新测量结果及结果对象图形
            //UpdateResults(ImageData, refSystem);
            ////更新测量region图形
            //UpdateMeasureXLD();
            if (ImageRefIn == null || ImageRefIn.IsInitialized() == false)
            {
                return;
            }
            if (Mat2DManger.UseMat2D)
            {
                IMatching matchingTool = Mat2DManger.MatchingTool;
                refSystem = matchingTool.GetRefPoint();
            }
            else
            {
                refSystem = new HTuple(0, 0, 0);
            }
            if (refSystem != null)
            {
                //设置参考点
                handle.SetMetrologyModelParam("reference_system", refSystem);
            }

            RunRef();
            if (NotifyMetrologyObserver != null && ParamList.Count == ROIList.Count)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateMetrology);
            }
        }
        /// <summary>
		///更新测量区域xld
		/// </summary>
		public void UpdateMeasureXLD()
        {
            MeasureXLD.Dispose();
            MeasureXLD.GenEmptyObj();
            HTuple row, col;
            HXLDCont cross;

            cross = new HXLDCont();
            if (handle == null)
            {
                return;
            }
            //获取测量出来的点的集合及测量框
            MeasureXLD = handle.GetMetrologyObjectMeasures("all",
                "all",
                out row,
                out col);
            cross.GenCrossContourXld(row, col, new HTuple(6.0), new HTuple(0.785398));
            MeasureXLD = MeasureXLD.ConcatObj(cross);
            cross.Dispose();
        }
        /// <summary>释放测量对象所用的资源.</summary>
        void ClearMeasurement()
        {
            if (handle != null)
            {
                //测量句柄释放
                handle.Dispose();
                //测量结果图形对象释放
                ResultsXLD.Dispose();
                //mMeasureRegion.Dispose();
                MeasureXLD.Dispose();

                //测量结果清除
                ResultDataList.Clear();
                ResultDataWorldList.Clear();
            }
        }
        public void SetRefSystem(HTuple refSystem)
        {
            this.refSystem = refSystem;
            CreateMetrologyModel();

        }

        /// <summary>
        /// 二维测量结果更新。
        /// </summary>
        /// <param name="image">测量的图片</param>
        /// <param name="refSystem">参考系统信息</param>
        public void UpdateResults(HImage image, HTuple refSystem)
        {
            if (handle == null)
                return;

            ExceptionText = "";

            try
            {

                handle.AlignMetrologyModel((HTuple)refSystem[0], refSystem[1], refSystem[2]);
                Debug.WriteLine("图像坐标" + refSystem[0].D + "_" + refSystem[1].D);
                #region 预处理
                if (trangtransformImg != null && trangtransformImg.IsInitialized())
                {
                    trangtransformImg.Dispose();

                }
                trangtransformImg = image.Clone();

                if (regionMeasure != null && regionMeasure.IsInitialized())
                {
                    regionMeasure.Dispose();
                }
                regionMeasure = new HRegion();
                regionMeasure.GenEmptyRegion();
                int count = ParamList.Count;
                HHomMat2D mat2dMatching = null;
                if (Mat2DManger.UseMat2D && image != ImageRefIn)
                {
                    mat2dMatching = Mat2DManger.MatchingTool.RefImageToTestImage(true);
                }
                for (int i = 0; i < count; i++)
                {
                    //  获取ROI
                    ROI roiTmp = ROIList[i];
                    // 获取roi坐标信息
                    HTuple mROICoord = roiTmp.GetModelData();

                    HRegion regionTmp = new HRegion();

                    #region 生成roi区域
                    switch (roiTmp.ROIType)
                    {
                        case ROIType.Line:
                            HTuple Distance;
                            HOperatorSet.DistancePp(mROICoord[0],
                                mROICoord[1],//起点
                                mROICoord[2],
                                mROICoord[3],//终点
                                out Distance
                                );
                            HTuple r1 = (mROICoord[0] + mROICoord[2]) / 2.0;
                            HTuple c1 = (mROICoord[1] + mROICoord[3]) / 2.0;
                            HTuple angle;
                            HOperatorSet.AngleLx(mROICoord[0],
                                mROICoord[1],//起点
                                mROICoord[2],
                                mROICoord[3],//终点
                                out angle);

                            regionTmp.GenRectangle2(r1.D, c1.D, angle.D, Distance.D / 2.0, 2 + ParamList[i].Length1.D);

                            break;

                        case ROIType.CircleArc:
                            HRegion region1 = new HRegion();

                            double startAngle, endAngle;
                            startAngle = mROICoord[3].D;
                            if (startAngle < 0)
                            {
                                startAngle = startAngle + Math.PI;
                            }
                            else if (startAngle > 2 * Math.PI)
                            {
                                startAngle = startAngle % (2 * Math.PI);
                            }
                            endAngle = mROICoord[3].D + mROICoord[4].D;
                            if (endAngle < 0)
                            {
                                endAngle = endAngle + Math.PI;
                            }
                            else if (endAngle > 2 * Math.PI)
                            {
                                endAngle = endAngle % (2 * Math.PI);
                            }
                            //double tmp;
                            //if (startAngle > endAngle)
                            //{
                            //    tmp = startAngle;
                            //    startAngle = endAngle;
                            //    endAngle = tmp;
                            //}

                            region1.GenCircleSector(mROICoord[0].D, mROICoord[1].D,//坐标
                                mROICoord[2].D + ParamList[i].Length1.D + 2,//半径
                                startAngle,
                                endAngle);
                            HRegion region2 = new HRegion();

                            region2.GenCircleSector(mROICoord[0].D, mROICoord[1].D,//坐标
                                mROICoord[2].D - ParamList[i].Length1.D - 2,//半径
                               startAngle,
                               endAngle);
                            regionTmp = region1.Difference(region2);
                            region1.Dispose();
                            region2.Dispose();
                            break;
                    }
                    #endregion
                    if (Mat2DManger.UseMat2D && image != ImageRefIn)
                    {
                        HRegion regionTmp1 = regionTmp.AffineTransRegion(mat2dMatching, "nearest_neighbor");
                        regionTmp.Dispose();
                        regionTmp = regionTmp1;
                    }
                    regionMeasure = regionTmp.Union2(regionMeasure);

                    HImage imageReduceTmp = image.ReduceDomain(regionTmp);
                    HImage endImg = ParamList[i].SacleImagePretreatment.Run(imageReduceTmp);
                    trangtransformImg = endImg.PaintGray(trangtransformImg);
                    regionTmp.Dispose();
                    imageReduceTmp.Dispose();
                    endImg.Dispose();
                }
                #endregion 
                //参数更新到图片
                handle.ApplyMetrologyModel(trangtransformImg);
                if (trangtransformImgShow != null && trangtransformImgShow.IsInitialized())
                {
                    trangtransformImgShow.Dispose();
                }
                trangtransformImgShow = trangtransformImg.ReduceDomain(regionMeasure);
                int num = ParamList.Count;
                //清除结果数据
                ResultDataWorldList.Clear();
                ResultDataList.Clear();

                //添加结果数据
                for (int i = 0; i < num; i++)
                {
                    ROI roiTmp = ROIList[i];
                    HTuple resultData = handle.GetMetrologyObjectResult(
                        ParamList[i].Index,//测量序号
                        "all",
                        "result_type", "all_param");
                    if (roiTmp.ROIType == ROIType.Line)
                    {
                        MetrologyResultLine resultLine = new MetrologyResultLine();
                        MetrologyResultLine resultLineWorld = new MetrologyResultLine();
                        if (resultData.Length == 4)
                        {
                            resultLine.row1 = resultData[0];
                            resultLine.col1 = resultData[1];

                            resultLine.row2 = resultData[2];
                            resultLine.col2 = resultData[3];
                            //如果标定有效
                            if (IsCalibValid && IsToWorldCoord)
                            {
                                //第一点转化世界坐标
                                Rectify(resultLine.row1, resultLine.col1, out resultLineWorld.row1, out resultLineWorld.col1);
                                //第二点转化世界坐标
                                Rectify(resultLine.row2, resultLine.col2, out resultLineWorld.row2, out resultLineWorld.col2);
                            }
                            else
                            {
                                resultLineWorld = new MetrologyResultLine(resultLine);
                            }
                        }

                        ResultDataList.Add(resultLine);
                        ResultDataWorldList.Add(resultLineWorld);
                    }
                    else if (roiTmp.ROIType == ROIType.CircleArc)
                    {
                        MetrologyResultCircularArc resurltMetrologyTmp = new MetrologyResultCircularArc();
                        MetrologyResultCircularArc resultMetrologyTmpWorld = new MetrologyResultCircularArc();
                        if (resultData.Length == 3)
                        {
                            resurltMetrologyTmp.midR = resultData[0];
                            resurltMetrologyTmp.midC = resultData[1];
                            resurltMetrologyTmp.radius = resultData[2];
                            resurltMetrologyTmp.startPhi = roiTmp.GetModelData()[3];
                            resurltMetrologyTmp.extentPhi = roiTmp.GetModelData()[4];
                            //如果标定有效
                            if (IsCalibValid && IsToWorldCoord)
                            {
                                //圆信息转化为世界坐标系
                                Rectify(resurltMetrologyTmp.midR, resurltMetrologyTmp.midC, resurltMetrologyTmp.radius,
                                    out resultMetrologyTmpWorld.midR, out resultMetrologyTmpWorld.midC, out resultMetrologyTmpWorld.radius);
                                resultMetrologyTmpWorld.startPhi = resurltMetrologyTmp.startPhi;
                                resultMetrologyTmpWorld.extentPhi = resurltMetrologyTmp.extentPhi;
                            }
                            else
                            {
                                resultMetrologyTmpWorld = new MetrologyResultCircularArc(resurltMetrologyTmp);
                            }
                        }

                        ResultDataList.Add(resurltMetrologyTmp);
                        ResultDataWorldList.Add(resultMetrologyTmpWorld);
                    }
                }
            }
            catch (HOperatorException ex)
            {
                ResultsXLD.Dispose();
                ExceptionText = ex.Message;
                ResultDataWorldList.Clear();
                Util.WriteLog(this.GetType(), ex);
                return;
            }
            //更新测量region图形
            UpdateMeasureXLD();
            //更新测量到的对象图形
            UpdateXLD();

        }
        /// <summary>
        /// 如果校准数据可用且有效，则校正测量结果坐标，否则保持不变
        /// </summary>
        public void Rectify(HTuple row, HTuple col, out HTuple rowRect, out HTuple colRect)
        {
            double unitScale = 0.0;

            if (IsCalibValid)
            {
                switch (Unit)
                {
                    case "um":
                        unitScale = 0.000001;
                        break;
                    case "mm":
                        unitScale = 0.001;
                        break;
                    case "cm":
                        unitScale = 0.01;
                        break;
                    case "m":
                        unitScale = 1.0;
                        break;
                    default:
                        break;
                }

                HOperatorSet.ImagePointsToWorldPlane(camParameter,
                                                     camPose,
                                                     row, col, new HTuple(unitScale),
                                                     out colRect, out rowRect);
            }
            else
            {
                rowRect = row;
                colRect = col;
            }

        }

        /// <summary>
        /// 如果校准数据可用且有效，则校正测量结果坐标，否则保持不变
        /// </summary>
        public void Rectify(HTuple row, HTuple col, HTuple radius, out HTuple rowRect, out HTuple colRect, out HTuple radiusRect)
        {
            double unitScale = 0.0;

            if (IsCalibValid)
            {
                switch (Unit)
                {
                    case "um":
                        unitScale = 0.000001;
                        break;
                    case "mm":
                        unitScale = 0.001;
                        break;
                    case "cm":
                        unitScale = 0.01;
                        break;
                    case "m":
                        unitScale = 1.0;
                        break;
                    default:
                        break;
                }

                HOperatorSet.ImagePointsToWorldPlane(camParameter,
                                                     camPose,
                                                     row, col, new HTuple(unitScale),
                                                     out colRect, out rowRect);
            }
            else
            {
                rowRect = row;
                colRect = col;
            }
            radiusRect = radius;
        }
        /// <summary>
        /// 更新测量结果生成的测量图形(直线/圆)
        /// </summary>
        public void UpdateXLD()
        {
            if (handle == null && ((int)handle.Handle < 0))
                return;

            ExceptionText = "";
            ResultsXLD.Dispose();
            ResultsXLD.GenEmptyObj();

            try
            {
                int num = ParamList.Count;
                //清除结果数据
                //mResultWorldList.Clear();
                //添加结果数据
                for (int i = 0; i < num; i++)
                {
                    ROI roiTmp = ROIList[i];
                    if (roiTmp.ROIType == ROIType.Line)
                    {
                        MetrologyResultLine result1 = (MetrologyResultLine)ResultDataList[i];
                        HXLDCont val = result1.GetXLD();
                        if (val != null)
                            ResultsXLD = ResultsXLD.ConcatObj(val);
                    }
                    else if (roiTmp.ROIType == ROIType.CircleArc)
                    {
                        MetrologyResultCircularArc result1 = (MetrologyResultCircularArc)ResultDataList[i];
                        HXLDCont val = result1.GetXLD();
                        if (val != null)
                            ResultsXLD = ResultsXLD.ConcatObj(val);
                    }
                }
            }
            catch (HOperatorException ex)
            {
                ExceptionText = ex.Message;
                Util.WriteLog(this.GetType(), ex);
            }
            //委托更新
            if (NotifyMetrologyObserver != null)
            {
                NotifyMetrologyObserver(MetrologyMessage.EventUpdateResultXLD);
            }
        }

        /// <summary>清除测量结果</summary>
        public void ClearResultData()
        {
            ResultDataWorldList.Clear();
        }
        public override string GetToolType()
        {
            return toolType;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new MetrologyToolUnit(this);
            }
            return settingUnit;
        }

        private double ResultConversion(double dat)
        {
            double result = 0;
            result = dat * scale + offset;
            return result;
        }

        protected override void RunAct()
        {
            base.RunAct();
            Result = "0";

            DisposeHobject(measureXld);
            DisposeHobject(resultXLD);
            DisposeHobject(xldRect2);
            resultXLD = null;
            measureXld = null;
            xldRect2 = null;
            IsOk = false;
            if (Enable == false)
            {
                IsOk = true;
                isRealOk = false;
                return;
            }
            if (Mat2DManger.UseMat2D && ImageTestIn != ImageRefIn)
            {
                IMatching matchingTool = Mat2DManger.MatchingTool;
                HTuple resultPos = matchingTool.GetMatchingPoint();
                if (matchingTool != null && resultPos != null)
                {
                    //HTuple refPos = matchingTool.RefCoordinates;
                    //if (referenceSystemSetted == false)
                    //{
                    //    System.Diagnostics.Debug.WriteLine("设置参考坐标");
                    //    handle.SetMetrologyModelParam("reference_system", refPos);
                    //    referenceSystemSetted = true;
                    //}


                    //更新测量结果及结果对象图形
                    UpdateResults(ImageTestIn, resultPos);
                    //更新测量region图形
                    //UpdateMeasureXLD();
                }
                else
                {
                    return;
                }
            }
            else
            {
                //更新测量结果及结果对象图形
                UpdateResults(ImageTestIn, refSystem);
            }

            if (ResultDataWorldList.Count > 0)
            {
                switch (metrologyMethod)
                {
                    case MetrologyMethod.直线到直线:
                        //找到两条直线并且只有两条直线的roi
                        if (ResultDataWorldList.Count == 2 &&
                            ROIList.Count == 2 &&
                            ROIList[0].ROIType == ROIType.Line &&
                            ROIList[1].ROIType == ROIType.Line)
                        {
                            //直线0
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            //直线1
                            HTuple data1 = ResultDataWorldList[1].GetData();
                            if (data0 != null && data1 != null)
                            {
                                //直线1点0到直线2距离
                                HTuple distance1, distance2;
                                HOperatorSet.DistancePl(data0[0], data0[1], data1[0], data1[1], data1[2], data1[3], out distance1);
                                HOperatorSet.DistancePl(data0[2], data0[3], data1[0], data1[1], data1[2], data1[3], out distance2);
                                double result = (distance1 + distance2) / 2.0;
                                Result = ResultConversion(result).ToString("F4");
                                resultData = result;
                            }

                        }
                        break;
                    case MetrologyMethod.直线到圆弧:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 2 &&
                           ROIList.Count == 2)
                        {
                            int indexLine = ROIList.FindIndex(x => x.ROIType == ROIType.Line);
                            int indexCircleArc = ROIList.FindIndex(x => x.ROIType == ROIType.CircleArc);
                            if (indexLine > -1 && indexCircleArc > -1)
                            {
                                HTuple dataLine = ResultDataWorldList[indexLine].GetData();
                                HTuple dataCirecleArc = ResultDataWorldList[indexCircleArc].GetData();
                                if (dataLine != null && dataCirecleArc != null)
                                {
                                    HTuple distance;

                                    HOperatorSet.DistancePl(dataCirecleArc[0], dataCirecleArc[1],
                                        dataLine[0], dataLine[1], dataLine[2], dataLine[3],
                                        out distance);
                                    Result = ResultConversion(distance.D).ToString("F2");
                                    resultData = distance;
                                }

                            }
                        }
                        break;
                    case MetrologyMethod.圆弧到圆弧:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 2 &&
                           ROIList.Count == 2 &&
                             ROIList[0].ROIType == ROIType.CircleArc &&
                            ROIList[1].ROIType == ROIType.CircleArc)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            HTuple data1 = ResultDataWorldList[1].GetData();
                            if (data0 != null && data1 != null)
                            {
                                HTuple distance;
                                HOperatorSet.DistancePp(data0[0], data0[1], data1[0], data1[1],
                                    out distance);
                                Result = ResultConversion(distance.D).ToString("F2");

                                resultData = distance;
                            }

                        }
                        break;
                    case MetrologyMethod.圆弧半径:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 1 &&
                           ROIList.Count == 1 &&
                           ROIList[0].ROIType == ROIType.CircleArc)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            if (data0 != null)
                            {
                                Result = ResultConversion(data0[2].D).ToString("F2");
                                resultData = data0[2].D;
                            }
                        }
                        break;
                    case MetrologyMethod.圆心获取:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 1 &&
                           ROIList.Count == 1 &&
                           ROIList[0].ROIType == ROIType.CircleArc)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            if (data0 != null)
                            {
                                Result = string.Format("圆心坐标 行{0:f2},列{1:f2}", data0[0].D, data0[1].D);
                                resultData = new HTuple(data0[0].D, data0[1].D);
                            }
                        }
                        break;
                    case MetrologyMethod.直线拟合:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 1 &&
                           ROIList.Count == 1 &&
                           ROIList[0].ROIType == ROIType.Line)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            if (data0 != null)
                            {
                                Result = string.Format("r1{0:f2},c1{1:f2},r2{2:f2},c2{3:f2}", data0[0].D, data0[1].D, data0[2].D, data0[3].D);

                                resultData = new HTuple(data0[0].D, data0[1].D, data0[2].D, data0[3].D);
                            }
                        }
                        break;
                    case MetrologyMethod.直线角度:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 1 &&
                           ROIList.Count == 1 &&
                           ROIList[0].ROIType == ROIType.Line)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            if (data0 != null)
                            {
                                HTuple angle;
                                //角度值   -180 到180 deg
                                HOperatorSet.AngleLx(data0[0].D, data0[1].D, data0[2].D, data0[3].D, out angle);
                                Result = angle.TupleDeg().D.ToString("F2");
                                resultData = new HTuple(angle);
                            }
                        }
                        break;
                    case MetrologyMethod.矩形中心:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 4 &&
                           ROIList.Count == 4 &&
                           ROIList[0].ROIType == ROIType.Line &&
                           ROIList[1].ROIType == ROIType.Line &&
                           ROIList[2].ROIType == ROIType.Line &&
                           ROIList[3].ROIType == ROIType.Line)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            HTuple data1 = ResultDataWorldList[1].GetData();
                            HTuple data2 = ResultDataWorldList[2].GetData();
                            HTuple data3 = ResultDataWorldList[3].GetData();
                            if (data0 != null && data1 != null && data2 != null && data3 != null)
                            {
                                PointT pt1, pt2, pt3, pt4, ptResult;

                                HTuple isOverlapping1, isOverlapping2, isOverlapping3, isOverlapping4;
                                //0-1
                                HOperatorSet.IntersectionLines(data0[0].D, data0[1].D, data0[2].D, data0[3].D,
                                    data1[0].D, data1[1].D, data1[2].D, data1[3].D,
                                    out pt1.Y, out pt1.X, out isOverlapping1);
                                //1-2
                                HOperatorSet.IntersectionLines(data2[0].D, data2[1].D, data2[2].D, data2[3].D,
                                    data1[0].D, data1[1].D, data1[2].D, data1[3].D,
                                    out pt2.Y, out pt2.X, out isOverlapping2);

                                //3-2
                                HOperatorSet.IntersectionLines(data2[0].D, data2[1].D, data2[2].D, data2[3].D,
                                    data3[0].D, data3[1].D, data3[2].D, data3[3].D,
                                    out pt3.Y, out pt3.X, out isOverlapping3);
                                //0-3
                                HOperatorSet.IntersectionLines(data0[0].D, data0[1].D, data0[2].D, data0[3].D,
                                    data3[0].D, data3[1].D, data3[2].D, data3[3].D,
                                    out pt4.Y, out pt4.X, out isOverlapping4);
                                //交点获取成功
                                if (isOverlapping1 != 0 || isOverlapping2 != 0 ||
                                    isOverlapping3 != 0 || isOverlapping4 != 0)
                                {
                                    throw new Exception("直线交点获取失败");
                                }

                                HTuple isOverlapping5;
                                HOperatorSet.IntersectionLines(pt1.Y, pt1.X, pt3.Y, pt3.X,
                                pt2.Y, pt2.X, pt4.Y, pt4.X,
                                out ptResult.Y, out ptResult.X, out isOverlapping5);

                                //交点获取成功
                                if (isOverlapping5 != 0)
                                {
                                    throw new Exception("中心交点获取失败");
                                }

                                xldRect2 = new HXLDCont();

                                HTuple rr1 = new HTuple(pt1.Y, pt2.Y, pt3.Y, pt4.Y, pt1.Y, pt3.Y, pt2.Y, pt4.Y);
                                HTuple cc1 = new HTuple(pt1.X, pt2.X, pt3.X, pt4.X, pt1.X, pt3.X, pt2.X, pt4.X);
                                xldRect2.GenContourPolygonXld(rr1, cc1);
                                Result = string.Format("矩形中心 行{0:f2},列{1:f2}", ptResult.Y.D, ptResult.X.D);
                                resultData = new HTuple(ptResult.Y, ptResult.X);
                            }
                        }
                        break;
                    case MetrologyMethod.腰孔长:
                        //判断roi及测量结果
                        if (ResultDataWorldList.Count == 2 &&
                           ROIList.Count == 2 &&
                           ROIList[0].ROIType == ROIType.CircleArc &&
                           ROIList[1].ROIType == ROIType.CircleArc)
                        {
                            HTuple data0 = ResultDataWorldList[0].GetData();
                            HTuple data1 = ResultDataWorldList[1].GetData();
                            if (data0 != null && data1 != null)
                            {
                                HTuple dd1;
                                HOperatorSet.DistancePp(data0[0], data0[1], data1[0], data1[1], out dd1);
                                HTuple dd2 = dd1 + data0[2] + data1[2].D;
                                Result = dd2.D.ToString("f3");
                                resultData = new HTuple(dd2.D);
                                //Result = string.Format("R1 x{0:F2},y{1:F2}r{2:F2},R2 x{3:F2},y{4:F2}r{5:F2}",
                                //    data0[0].D, data0[1].D, data0[2].D, data1[0].D, data1[1].D, data1[2].D);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (metrologyMethod == MetrologyMethod.直线拟合 ||
                metrologyMethod == MetrologyMethod.直线角度 ||
                metrologyMethod == MetrologyMethod.圆心获取 ||
                metrologyMethod == MetrologyMethod.矩形中心 ||
                metrologyMethod == MetrologyMethod.腰孔长)
            {
                if (Result != "0")  //角度0度为0.00
                {
                    IsOk = true;
                    isRealOk = true;
                }
            }
            else
            {
                double resultD = double.Parse(Result);
                if (resultD < Min || resultD > Max)
                {
                    IsOk = false;
                }
                else
                {
                    IsOk = true;
                    isRealOk = true;
                }
            }
        }
        /// <summary>
        /// 计算两条直线的交点
        /// </summary>
        /// <param name="lineFirstStar">L1的点1坐标</param>
        /// <param name="lineFirstEnd">L1的点2坐标</param>
        /// <param name="lineSecondStar">L2的点1坐标</param>
        /// <param name="lineSecondEnd">L2的点2坐标</param>
        /// <returns></returns>
        public static PointT GetIntersection(PointT lineFirstStar, PointT lineFirstEnd, PointT lineSecondStar, PointT lineSecondEnd)
        {
            /*
             * L1，L2都存在斜率的情况：
             * 直线方程L1: ( y - y1 ) / ( y2 - y1 ) = ( x - x1 ) / ( x2 - x1 ) 
             * => y = [ ( y2 - y1 ) / ( x2 - x1 ) ]( x - x1 ) + y1
             * 令 a = ( y2 - y1 ) / ( x2 - x1 )
             * 有 y = a * x - a * x1 + y1   .........1
             * 直线方程L2: ( y - y3 ) / ( y4 - y3 ) = ( x - x3 ) / ( x4 - x3 )
             * 令 b = ( y4 - y3 ) / ( x4 - x3 )
             * 有 y = b * x - b * x3 + y3 ..........2
             * 
             * 如果 a = b，则两直线平等，否则， 联解方程 1,2，得:
             * x = ( a * x1 - b * x3 - y1 + y3 ) / ( a - b )
             * y = a * x - a * x1 + y1
             * 
             * L1存在斜率, L2平行Y轴的情况：
             * x = x3
             * y = a * x3 - a * x1 + y1
             * 
             * L1 平行Y轴，L2存在斜率的情况：
             * x = x1
             * y = b * x - b * x3 + y3
             * 
             * L1与L2都平行Y轴的情况：
             * 如果 x1 = x3，那么L1与L2重合，否则平等
             * 
            */
            double a = 0, b = 0;
            int state = 0;
            if (lineFirstStar.X != lineFirstEnd.X)
            {
                a = (lineFirstEnd.Y - lineFirstStar.Y) / (lineFirstEnd.X - lineFirstStar.X);
                state |= 1;
            }
            if (lineSecondStar.X != lineSecondEnd.X)
            {
                b = (lineSecondEnd.Y - lineSecondStar.Y) / (lineSecondEnd.X - lineSecondStar.X);
                state |= 2;
            }
            switch (state)
            {
                case 0: //L1与L2都平行Y轴
                    {
                        if (lineFirstStar.X == lineSecondStar.X)
                        {
                            throw new Exception("两条直线互相重合，且平行于Y轴，无法计算交点。");
                        }
                        else
                        {
                            throw new Exception("两条直线互相平行，且平行于Y轴，无法计算交点。");
                        }
                    }
                case 1: //L1存在斜率, L2平行Y轴
                    {
                        double x = lineSecondStar.X;
                        double y = (lineFirstStar.X - x) * (-a) + lineFirstStar.Y;
                        return new PointT(x, y);
                    }
                case 2: //L1 平行Y轴，L2存在斜率
                    {
                        double x = lineFirstStar.X;
                        //网上有相似代码的，这一处是错误的。你可以对比case 1 的逻辑 进行分析
                        //源code:lineSecondStar * x + lineSecondStar * lineSecondStar.X + p3.Y;
                        double y = (lineSecondStar.X - x) * (-b) + lineSecondStar.Y;
                        return new PointT(x, y);
                    }
                case 3: //L1，L2都存在斜率
                    {
                        if (a == b)
                        {
                            throw new Exception("两条直线平行或重合，无法计算交点。");
                        }
                        double x = (a * lineFirstStar.X - b * lineSecondStar.X - lineFirstStar.Y + lineSecondStar.Y) / (a - b);
                        double y = a * x - a * lineFirstStar.X + lineFirstStar.Y;
                        return new PointT(x, y);
                    }
            }
            throw new Exception("不可能发生的情况");
        }

        public override void ShowResult(HWndCtrl ViewCtrl)
        {
            if (Enable == false)
            {
                return;
            }
            if (RuningFinish == false)
            {
                return;
            }
            if (ROIList.Count > 0)
            {
                if (trangtransformImgShow != null && trangtransformImgShow.IsInitialized())
                {
                    ViewCtrl.AddIconicVar(trangtransformImgShow);
                }
                if (MeasureXLD != null && MeasureXLD.IsInitialized())
                {
                    ViewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                    ViewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                    ViewCtrl.AddIconicVar(MeasureXLD);
                }
                if (ResultsXLD.IsInitialized())
                {
                    ViewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                    ViewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                    ViewCtrl.AddIconicVar(ResultsXLD);
                }
                if (xldRect2 != null && xldRect2.IsInitialized())
                {
                    ViewCtrl.AddIconicVar(xldRect2);
                }
            }
        }
        public override void ClearTestData()
        {
            base.ClearTestData();
            NotifyMetrologyObserver = null;
        }
        public override void SerializeCheck()
        {

            if (handle != null && handle.IsInitialized() == false)
            {
                handle = null;
            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (handle != null)
            {
                handle.Dispose();
                handle = null;
            }
            base.Dispose(disposing);
        }
        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            string dat = ErrDataFlag;
            if (this.IsOk && resultData != null && resultData.Length > 0)
            {
                string datTmp = "";
                for (int i = 0; i < resultData.Length; i++)
                {
                    datTmp += string.Format(",{0:f2}",
                        resultData[i].D);
                }
                dat = string.Format("{0}{1}", resultData.Length, datTmp);
            }
            else
            {
                dat = NgDataFlag;
            }
            return dat;
        }
    }
}
