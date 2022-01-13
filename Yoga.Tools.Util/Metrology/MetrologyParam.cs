using HalconDotNet;
using System;
using System.Diagnostics;
using Yoga.Common;

namespace Yoga.Tools.Metrology
{
    [Serializable]
    public class MetrologyParam
    {
        //变量默认值
        public const double INIT_LENGTH1 = 30;
        public const double INIT_LENGTH2 = 4;
        public const double INIT_THRESHOLD = 30.0;
        public const double INIT_SIGMA = 1.0;
        public const double INIT_MIN_SCORE = 0.7;
        public const double INIT_NUM_INSTANCES = 1;

        public const int INIT_NUM_MEASURE = 50;

        public const string TRANSITION_UNIFORM = "uniform";
        public const string TRANSITION_NEGATIVE = "negative";
        public const string TRANSITION_POSTTIVE = "positive";

        public const string SELECT_ALL = "all";
        public const string SELECT_FIRST = "first";
        public const string SELECT_LAST = "last";


        private int numInstances;
        private string transition;

        private string measureSelect;
        private double minScore;
        private double length1;
        private double length2;
        private double sigma;
        private double threshold;
        private int index;
        private int numMeasures;

        private SacleImagePretreatment sacleImagePretreatment;
        /// <summary>最小分数 min_score
        /// </summary>
        public double MinScore
        {
            get
            {
                return minScore;
            }

            set
            {
                if (value > 0 && value < 1)
                {
                    minScore = value;
                }
            }
        }
        /// <summary> 边缘求取方式(暗到明或者明到暗).measure_transition
        /// </summary>
        public string Transition
        {
            get
            {
                return transition;
            }

            set
            {
                transition = value;
            }
        }
        /// <summary>查找对象的个数 num_instances
        /// </summary>
        public int NumInstances
        {
            get
            {
                return numInstances;
            }

            set
            {
                numInstances = value;
            }
        }
        /// <summary>测量区域的一半长度垂直于边界。
        /// </summary>
        public HTuple Length1
        {
            get
            {
                return length1;
            }

            set
            {
                length1 = value;
            }
        }
        /// <summary>与边界相切的测量区域的一半长度。
        /// </summary>
        public HTuple Length2
        {
            get
            {
                return length2;
            }

            set
            {
                length2 = value;
            }
        }
        /// <summary>用于平滑的高斯函数的Sigma值
        /// </summary>
        public HTuple Sigma
        {
            get
            {
                return sigma;
            }

            set
            {
                sigma = value;
            }
        }
        /// <summary>最小边缘灰度值幅度
        /// </summary>
        public HTuple Threshold
        {
            get
            {
                return threshold;
            }

            set
            {
                threshold = value;
            }
        }
        /// <summary>应用到测量模板上的参数的序号
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }

        public int NumMeasures
        {
            get
            {
                return numMeasures;
            }

            set
            {
                numMeasures = value;
            }
        }

        public SacleImagePretreatment SacleImagePretreatment
        {
            get
            {
                if (sacleImagePretreatment == null)
                {
                    sacleImagePretreatment = new SacleImagePretreatment();
                }
                return sacleImagePretreatment;
            }

            set
            {
                sacleImagePretreatment = value;
            }
        }

        public string MeasureSelect
        {
            get
            {
                return measureSelect;
            }

            set
            {
                measureSelect = value;
            }
        }

        /// <summary>
        /// 参数初始化设定
        /// </summary>
        public void Init()
        {
            this.MinScore = INIT_MIN_SCORE;
            this.Transition = TRANSITION_NEGATIVE;
            this.MeasureSelect = SELECT_FIRST;
            this.NumInstances = 1;
            this.Length1 = INIT_LENGTH1;
            this.Length2 = INIT_LENGTH2;
            this.Threshold = INIT_THRESHOLD;
            this.Sigma = INIT_SIGMA;
            this.numMeasures = INIT_NUM_MEASURE;
            this.sacleImagePretreatment = new SacleImagePretreatment();
        }
        /// <summary>将参数设置在测量句柄中
        /// 
        /// </summary>
        /// <param name="modelHandle"></param>
        public void ApplyMetrologyParam(HMetrologyModel handle)
        {
            handle.SetMetrologyObjectParam(Index, "measure_transition", Transition);
            handle.SetMetrologyObjectParam(Index, "measure_select", MeasureSelect);//新增
            handle.SetMetrologyObjectParam(Index, "min_score", MinScore);
            handle.SetMetrologyObjectParam(Index, "num_instances", NumInstances);
            handle.SetMetrologyObjectParam(Index, "num_measures", NumMeasures);
        }
    }
}
