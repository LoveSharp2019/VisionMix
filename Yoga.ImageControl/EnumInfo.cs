using System;

namespace Yoga.ImageControl
{
    [Serializable]
    public enum ROIType
    {
        /// <summary>
        /// 直线
        /// </summary>
        Line = 10,
        /// <summary>
        /// 圆
        /// </summary>
        Circle,
        /// <summary>
        /// 圆弧
        /// </summary>
        CircleArc,
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle1,
        /// <summary>
        /// 带角度矩形
        /// </summary>
        Rectangle2
    }
    /// <summary>
    /// ROI运算
    /// </summary>
    public enum ROIOperation
    {
        /// <summary>
        /// ROI求和模式
        /// </summary>
        Positive = 21,
        /// <summary>
        /// ROI求差模式
        /// </summary>
        Negative,
        /// <summary>
        /// ROI模式为无
        /// </summary>
        None,
    }
    public enum ViewMessage
    {
        /// <summary>Constant describing an update of the model region</summary>
        UpdateROI = 50,

        ChangedROISign,

        /// <summary>Constant describing an update of the model region</summary>
        MovingROI,
        DeletedActROI,
        DelectedAllROIs,

        ActivatedROI,

        MouseMove,
        CreatedROI,
        /// <summary>
        /// Constant describes delegate message to signal new image
        /// </summary>
        UpdateImage,
        /// <summary>
        /// Constant describes delegate message to signal error
        /// when reading an image from file
        /// </summary>
        ErrReadingImage,
        /// <summary> 
        /// Constant describes delegate message to signal error
        /// when defining a graphical context
        /// </summary>
        ErrDefiningGC
    }
    public enum ShowMode
    {
        /// <summary>
        /// 包含ROI显示
        /// </summary>
        IncludeROI = 1,
        /// <summary>
        /// 不包含ROI显示
        /// </summary>
        ExcludeROI
    }
    public enum ResultShow
    {
        原图,
        处理后
    }
}
