namespace Yoga.Tools.Matching
{
    public enum MatchingStatus
    {
        /// <summary>
        /// 更新图形常量
        /// </summary>
        UpdateXLD = 0,
        /// <summary>
        ///更新金字塔。
        /// </summary>
        UpdatePyramid,
        /// <summary>
        ///检测结果应该更新。
        /// </summary>
        UpdateDetectiongResult,
        /// <summary>
        /// 应该更新测试图像
        /// </summary>
        UpdateTestView,

        /// <summary>
        /// 文件扩展名错误
        /// </summary>
        ErrNoValidFile,
        /// <summary>
        /// 模板写入错误
        /// </summary>
        ErrWriteShapeModel,
        /// <summary>
        /// 模板读取错误
        /// </summary>
        ErrReadShapeModel,
        /// <summary>
        ///模板未定义异常
        /// </summary>
        ErrNoModelDefined,
        /// <summary>
        /// 无模板图像异常
        /// </summary>
        ErrNoImage,
        /// <summary> 
        /// 无测试图像异常
        /// </summary>
        ErrNoTestImage,
        /// <summary>
        /// 无搜索框
        /// </summary>
        ErrNoSearchRegion,
        /// <summary>
        /// 读取图像异常
        /// </summary>
        ErrReadingImage,
        /// <summary>
        ///更新优化状态
        /// </summary>
        UpdateRecogSattisticStatus,
        /// <summary>
        /// 更新上次优化结果数据
        /// </summary>
        UpdateRecogVals,
        /// <summary>
        /// 更新优化后的结果数据
        /// </summary>
        UpdateRecogOptimumVals,
        /// <summary>
        /// 更新错误信息,包含图像错误及模板错误等
        /// </summary>
        UpdateTestErr,
        /// <summary>
        /// Constant describing an error, which says that there is 
        /// no possible combination of matching parameters to obtain
        /// a detection result
        /// </summary>
        UpdateRecogErr,

        /// <summary>
        /// Constant describing a change in the statistics of
        /// the recognition rate
        /// </summary>
        UpdateInspRecograte,
        /// <summary>
        /// Constant describing a change in the statics of
        /// the average recognition results 
        /// </summary>
        UpdateInspStatistics,
        /// Constant describing the success of the optimization 
        /// process and triggering the adjustment of the GUI
        /// components to the optimal parameter setting
        /// </summary>
        RunSucccessful,
        /// <summary>
        /// Constant describing the failure of the optimization
        /// process and reseting the matching parameters to the 
        /// initial setup
        /// </summary>
        RunFailed
    }
}
