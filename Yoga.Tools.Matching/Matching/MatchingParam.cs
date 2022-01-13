using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using Yoga.Common;

namespace Yoga.Tools.Matching
{
    [Serializable]
    /// <summary>
    /// 模板匹配参数
    /// </summary>
    public class MatchingParam : ICloneable
    {
        #region 创建模板参数
        /// <summary>
        /// 金字塔等级
        /// </summary>
        public int PyramidLevel = 4;
        /// <summary>
        /// 对比度
        /// </summary>
        public int Contrast = 30;
        /// <summary>
        /// 最小缩放倍率
        /// </summary>
        public double MinScale = 1.0;
        /// <summary>
        ///最大缩放倍率
        /// </summary>
        public double MaxScale = 1.0;
        /// <summary>
        /// 缩放步长
        /// </summary>
        public double ScaleStep = 10.0 / 1000.0;
        /// <summary>
        /// 开始角度
        /// </summary>
        public double StartingAngle = (0) * Math.PI / 180.0;
        /// <summary>
        /// 角度范围
        /// </summary>
        public double AngleExtent = (360) * Math.PI / 180.0;
        /// <summary>
        /// 角度步长
        /// </summary>
        public double AngleStep = 1.0 * Math.PI / 180.0;
        /// <summary>
        /// 最小对比度
        /// </summary>
        public int MinContrast = 10;
        /// <summary>
        /// 度量方式
        /// </summary>
        public string Metric = "use_polarity";
        /// <summary>
        ///最优化方式
        /// </summary>
        public string Optimization = "none";
        #endregion

        #region 查找模板参数
        /// <summary>
        /// 最小分数
        /// </summary>
        public double MinScore = 0.5;
        /// <summary>
        /// 匹配个数
        /// </summary>
		public int NumMatches = 1;
        /// <summary>
        /// 贪心算法
        /// </summary>
		public double Greediness = 0.75;
        /// <summary>
        /// 最大重叠
        /// </summary>
		public double MaxOverlap = 0.5;
        /// <summary>
        /// 亚像素模式
        /// </summary>
		public string Subpixel = "least_squares";
        /// <summary>
        ///最大金字塔级别,确定速度
        /// </summary>
		public int LastPyramidLevel = 1;
        #endregion

        #region 速度优化参数
        /// <summary>
        /// 识别率模式 0为>=   | 1为=
        /// </summary>
        public int RecogRateOpt = 0; /* opt=0 => '=' und opt=1 => '>='*/
        /// <summary>
        /// 模板识别率
        /// </summary>
		public int RecogRate = 100;
        /// <summary>
        ///识别模式
        /// </summary>
		public string RecogSpeedMode = MatchingParam.RECOGM_MANUALSELECT;
        /// <summary>
        /// 手动设置要找到的模板数量。
        /// </summary>
		public int RecogManualSel = 1;
        #endregion

        // ---------------------- inspect vals -----------------------

        /// <summary>
        /// 与NumMatches匹配个数值相等
        /// </summary>
        public int InspectMaxNoMatch = 1;
        /// <summary>
        /// 自动确定参数集合
        /// </summary>
        public List<string> ParamAutoList = new List<string>();

        /// <summary>
        /// 定义参数NumLevels的自动模式
        /// </summary>
        public const string AUTO_NUM_LEVEL = "num_levels";
        /// <summary>
        /// 定义参数对比度的自动模式
        /// </summary>
        public const string AUTO_CONTRAST = "contrast";
        /// <summary>
        ///定义比例步长常量
        /// </summary>
        public const string AUTO_SCALE_STEP = "scale_step";
        /// <summary>
        /// 定义角度步长
        /// </summary>
        public const string AUTO_ANGLE_STEP = "angle_step";
        /// <summary>
        /// 最小对比度
        /// </summary>
        public const string AUTO_MIN_CONTRAST = "min_contrast";
        /// <summary>
        /// 最优化方式
        /// </summary>
        public const string AUTO_OPTIMIZATION = "optimization";

        /// <summary>
        /// 起始角度
        /// AngleStart.
        /// </summary>
        public const string BUTTON_ANGLE_START = "angle_start";
        /// <summary>
        /// 最大角度
        /// </summary>
        public const string BUTTON_ANGLE_EXTENT = "angle_extent";
        /// <summary>
        /// 最小比例
        /// </summary>
        public const string BUTTON_SCALE_MIN = "scale_min";
        /// <summary>
        /// 最大比例
        /// </summary>
        public const string BUTTON_SCALE_MAX = "scale_max";
        /// <summary>
        /// 度量方式常量
        /// </summary>
        public const string BUTTON_METRIC = "metric";
        /// <summary>
        /// 最小分数
        /// </summary>
        public const string BUTTON_MINSCORE = "min_score";
        /// <summary>
        /// 贪婪程度
        /// </summary>
        public const string BUTTON_GREEDINESS = "greediness";

        public const string BUTTON_NUM_MATCHES = "numMatches";

        public const string BUTTON_MAX_OVERLAP = "maxOverlap";

        public const string BUTTON_SUB_PIXEL = "subPixel";
        /// <summary>
        /// 定义要检测的实例数：查找用户指定的模型数
        /// </summary>
        public const string RECOGM_MANUALSELECT = "RecognFindSpecifiedNumber";
        /// <summary>
        ///定义最少查找个数
        /// </summary>
        public const string RECOGM_ATLEASTONE = "RecognAtLeast";
        /// <summary>
        /// 最大查找个数
        /// </summary>
        public const string RECOGM_MAXNUMBER = "RecognFindMaximum";

        /// <summary>
        /// 比例步长修改
        /// </summary>
        public const string RANGE_SCALE_STEP = "RangeScaleStep";
        /// <summary>
        /// 角度步长修改
        /// </summary>
        public const string RANGE_ANGLE_STEP = "RangeAngleStep";

        /// <summary>
        ///错误信息
        /// </summary>
        public const string H_ERR_MESSAGE = "Halcon Error";

        /// <summary>Constructor</summary>
        public MatchingParam()
        {
            SetAuto(AUTO_ANGLE_STEP);
            SetAuto(AUTO_CONTRAST);
            SetAuto(AUTO_MIN_CONTRAST);
            //SetAuto(AUTO_NUM_LEVEL);
            SetAuto(AUTO_OPTIMIZATION);
            SetAuto(AUTO_SCALE_STEP);
        }
        /// <summary>
        /// 深度复制参数
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream);
            }
        }
        /*******************************************************************/
        /*    Setter-methods for the set of values, that can be determined 
         *    automatically. If a parameter gets assigned a new value
         *    it can be only caused by user interaction, which means, the
         *    auto-modus for these particular parameters needs to be 
         *    canceled, to avoid further automatic adjustment              
		/*******************************************************************/

        /// <summary>
        /// 金字塔等级设置
        /// </summary>
        /// <param name="val"></param>
        public void SetNumLevel(double val)
        {
            PyramidLevel = (int)val;
            if (ParamAutoList.Contains(MatchingParam.AUTO_NUM_LEVEL))
                ParamAutoList.Remove(MatchingParam.AUTO_NUM_LEVEL);
        }

        /// <summary>
        /// 对比度设置
        /// </summary>
        /// <param name="val"></param>
        public void SetContrast(int val)
        {
            Contrast = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_CONTRAST))
                ParamAutoList.Remove(MatchingParam.AUTO_CONTRAST);
        }

        /// <summary>
        /// 设置最小缩放比例
        /// </summary>
        /// <param name="val"></param>
        public void SetMinScale(double val)
        {
            MinScale = val;
        }

        /// <summary>
        /// 设置最大缩放比例
        /// </summary>
        /// <param name="val"></param>
        public void SetMaxScale(double val)
        {
            MaxScale = val;
        }

        /// <summary>
        /// 设置比例步长
        /// </summary>
        /// <param name="val"></param>
        public void SetScaleStep(double val)
        {
            ScaleStep = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_SCALE_STEP))
                ParamAutoList.Remove(MatchingParam.AUTO_SCALE_STEP);

        }

        /// <summary>
        /// 设置角度步长
        /// </summary>
        /// <param name="val"></param>
        public void SetAngleStep(double val)
        {
            AngleStep = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_ANGLE_STEP))
                ParamAutoList.Remove(MatchingParam.AUTO_ANGLE_STEP);
        }

        /// <summary>
        /// 设置最小对比度
        /// </summary>
        /// <param name="val"></param>
        public void SetMinContrast(int val)
        {
            MinContrast = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_MIN_CONTRAST))
                ParamAutoList.Remove(MatchingParam.AUTO_MIN_CONTRAST);
        }

        /// <summary>
        /// 设置最优化方式
        /// </summary>
        /// <param name="val"></param>
        public void SetOptimization(string val)
        {
            Optimization = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_OPTIMIZATION))
                ParamAutoList.Remove(MatchingParam.AUTO_OPTIMIZATION);
        }


        /*******************************************************************/
        /*        Setter-methods for the other values                      */
        /*******************************************************************/

        /// <summary>
        /// 设置起始角度值
        /// </summary>
        /// <param name="val"></param>
        public void SetStartingAngle(double val)
        {
            StartingAngle = val;
        }

        /// <summary>
        /// 设置角度范围
        /// </summary>
        /// <param name="val"></param>
        public void SetAngleExtent(double val)
        {
            AngleExtent = val;
        }

        /// <summary>
        /// 设置度量方式
        /// </summary>
        /// <param name="val"></param>
        public void SetMetric(string val)
        {
            Metric = val;
        }

        /// <summary>
        /// 设置最小分数
        /// </summary>
        public void SetMinScore(double val)
        {
            MinScore = val;
        }

        /// <summary>
        /// 设置模板查找个数
        /// </summary>
        public void SetNumMatches(int val)
        {
            NumMatches = val;
        }


        /// <summary>
        /// 设置贪心算法
        /// </summary>
        public void SetGreediness(double val)
        {
            Greediness = val;
        }

        /// <summary>
        /// 设置最大重叠
        /// </summary>
        public void SetMaxOverlap(double val)
        {
            MaxOverlap = val;
        }

        /// <summary>
        ///设置亚像素查找模式
        /// </summary>
        public void SetSubPixel(string val)
        {
            Subpixel = val;
        }

        /// <summary>
        /// 设置最大金字塔级别
        /// </summary>
        public void SetLastPyramLevel(int val)
        {
            LastPyramidLevel = val;
        }

        /*******************************************************************/
        /*******************************************************************/
        /*******************************************************************/

        /// <summary>
        /// 设置识别率模式
        /// </summary>
        public void SetRecogRateOption(int val)
        {
            RecogRateOpt = val;
        }

        /// <summary>
        /// 设置模板识别率
        /// </summary>
        public void SetRecogitionRate(int val)
        {
            RecogRate = val;
        }

        /// <summary>
        /// 设置速度优化模式
        /// </summary>
        public void SetRecogSpeedMode(string val)
        {
            RecogSpeedMode = val;
        }

        /// <summary>
        /// 设置要查找的模板数目
        /// </summary>
        public void SetRecogManualSelection(int val)
        {
            RecogManualSel = val;
        }

        /// <summary>
        /// 设置匹配个数
        /// </summary>
        public void SetInspectMaxNoMatchValue(int val)
        {
            InspectMaxNoMatch = val;
        }


        /*******************************************************************/
        /*******************************************************************/


        /// <summary>
        /// 依据mode判断其是否在自动调整参数集合中
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool IsAuto(string mode)
        {
            bool isAuto = false;

            switch (mode)
            {
                case MatchingParam.AUTO_ANGLE_STEP:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_ANGLE_STEP);
                    break;
                case MatchingParam.AUTO_CONTRAST:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_CONTRAST);
                    break;
                case MatchingParam.AUTO_MIN_CONTRAST:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_MIN_CONTRAST);
                    break;
                case MatchingParam.AUTO_NUM_LEVEL:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_NUM_LEVEL);
                    break;
                case MatchingParam.AUTO_OPTIMIZATION:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_OPTIMIZATION);
                    break;
                case MatchingParam.AUTO_SCALE_STEP:
                    isAuto = ParamAutoList.Contains(MatchingParam.AUTO_SCALE_STEP);
                    break;
                default: break;
            }

            return isAuto;
        }

        /// <summary>
        /// 是否有自动调整参数
        /// </summary>
        public bool IsOnAuto()
        {
            if (ParamAutoList.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 设置输入参数为自动调整
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool SetAuto(string val)
        {
            string mode = "";

            switch (val)
            {
                case MatchingParam.AUTO_ANGLE_STEP:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_ANGLE_STEP))
                        mode = MatchingParam.AUTO_ANGLE_STEP;
                    break;
                case MatchingParam.AUTO_CONTRAST:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_CONTRAST))
                        mode = MatchingParam.AUTO_CONTRAST;
                    break;
                case MatchingParam.AUTO_MIN_CONTRAST:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_MIN_CONTRAST))
                        mode = MatchingParam.AUTO_MIN_CONTRAST;
                    break;
                case MatchingParam.AUTO_NUM_LEVEL:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_NUM_LEVEL))
                        mode = MatchingParam.AUTO_NUM_LEVEL;
                    break;
                case MatchingParam.AUTO_OPTIMIZATION:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_OPTIMIZATION))
                        mode = MatchingParam.AUTO_OPTIMIZATION;
                    break;
                case MatchingParam.AUTO_SCALE_STEP:
                    if (!ParamAutoList.Contains(MatchingParam.AUTO_SCALE_STEP))
                        mode = MatchingParam.AUTO_SCALE_STEP;
                    break;
                default: break;
            }

            if (mode == "")
                return false;

            ParamAutoList.Add(mode);
            return true;
        }

        /// <summary>
        /// 在自动设置参数列表中移除输入变量
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool RemoveAuto(string val)
        {
            int index = ParamAutoList.FindIndex(e => e == val);
            if (index == -1)
            {
                return false;
            }
            ParamAutoList.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 获取自动设置的参数列表
        /// </summary>
        /// <returns>
        /// 自动设置参数字符串数组
        /// </returns>
        public string[] GetAutoParList()
        {
            int count = ParamAutoList.Count;
            string[] paramList = new string[count];

            for (int i = 0; i < count; i++)
                paramList[i] = ParamAutoList[i];

            return paramList;
        }

    }
}
