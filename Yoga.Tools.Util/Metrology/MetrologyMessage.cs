namespace Yoga.Tools.Metrology
{
    /// <summary>
    /// 二维测量与ui交互用枚举
    /// </summary>
    public enum MetrologyMessage
    {
        /// <summary>
        /// 更新测量实例常量.
        /// </summary>
        EventUpdateMetrology,
        /// <summary>
        /// 从测量实例数组中移除实例常量
        /// </summary>
        EventUpdateRemoveObject,
        /// <summary>
        /// 更新测量结果常量.
        /// </summary>
        EventUpdateResultXLD,
        /// <summary>
        /// 读取文件错误异常的常量标识.
        ///</summary>
        ErrReadingFile,
    }
}
