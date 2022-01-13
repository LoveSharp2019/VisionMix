using System;
using System.Text;

namespace Yoga.Common.VBAEngine
{
    /// <summary>
    /// vba引擎共用方法
    /// </summary>
    [Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute()]
    public class GlobalObject
    {
        static VbaFunction h = null;
        /// <summary>
        /// 全局的 HMeasureSYS 对象
        /// </summary>
        public static VbaFunction H
        {
            get
            {
                return h;
            }
        }
    }
}
