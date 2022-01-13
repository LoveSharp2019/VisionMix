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
    /// ģ��ƥ�����
    /// </summary>
    public class MatchingParam : ICloneable
    {
        #region ����ģ�����
        /// <summary>
        /// �������ȼ�
        /// </summary>
        public int PyramidLevel = 4;
        /// <summary>
        /// �Աȶ�
        /// </summary>
        public int Contrast = 30;
        /// <summary>
        /// ��С���ű���
        /// </summary>
        public double MinScale = 1.0;
        /// <summary>
        ///������ű���
        /// </summary>
        public double MaxScale = 1.0;
        /// <summary>
        /// ���Ų���
        /// </summary>
        public double ScaleStep = 10.0 / 1000.0;
        /// <summary>
        /// ��ʼ�Ƕ�
        /// </summary>
        public double StartingAngle = (0) * Math.PI / 180.0;
        /// <summary>
        /// �Ƕȷ�Χ
        /// </summary>
        public double AngleExtent = (360) * Math.PI / 180.0;
        /// <summary>
        /// �ǶȲ���
        /// </summary>
        public double AngleStep = 1.0 * Math.PI / 180.0;
        /// <summary>
        /// ��С�Աȶ�
        /// </summary>
        public int MinContrast = 10;
        /// <summary>
        /// ������ʽ
        /// </summary>
        public string Metric = "use_polarity";
        /// <summary>
        ///���Ż���ʽ
        /// </summary>
        public string Optimization = "none";
        #endregion

        #region ����ģ�����
        /// <summary>
        /// ��С����
        /// </summary>
        public double MinScore = 0.5;
        /// <summary>
        /// ƥ�����
        /// </summary>
		public int NumMatches = 1;
        /// <summary>
        /// ̰���㷨
        /// </summary>
		public double Greediness = 0.75;
        /// <summary>
        /// ����ص�
        /// </summary>
		public double MaxOverlap = 0.5;
        /// <summary>
        /// ������ģʽ
        /// </summary>
		public string Subpixel = "least_squares";
        /// <summary>
        ///������������,ȷ���ٶ�
        /// </summary>
		public int LastPyramidLevel = 1;
        #endregion

        #region �ٶ��Ż�����
        /// <summary>
        /// ʶ����ģʽ 0Ϊ>=   | 1Ϊ=
        /// </summary>
        public int RecogRateOpt = 0; /* opt=0 => '=' und opt=1 => '>='*/
        /// <summary>
        /// ģ��ʶ����
        /// </summary>
		public int RecogRate = 100;
        /// <summary>
        ///ʶ��ģʽ
        /// </summary>
		public string RecogSpeedMode = MatchingParam.RECOGM_MANUALSELECT;
        /// <summary>
        /// �ֶ�����Ҫ�ҵ���ģ��������
        /// </summary>
		public int RecogManualSel = 1;
        #endregion

        // ---------------------- inspect vals -----------------------

        /// <summary>
        /// ��NumMatchesƥ�����ֵ���
        /// </summary>
        public int InspectMaxNoMatch = 1;
        /// <summary>
        /// �Զ�ȷ����������
        /// </summary>
        public List<string> ParamAutoList = new List<string>();

        /// <summary>
        /// �������NumLevels���Զ�ģʽ
        /// </summary>
        public const string AUTO_NUM_LEVEL = "num_levels";
        /// <summary>
        /// ��������Աȶȵ��Զ�ģʽ
        /// </summary>
        public const string AUTO_CONTRAST = "contrast";
        /// <summary>
        ///���������������
        /// </summary>
        public const string AUTO_SCALE_STEP = "scale_step";
        /// <summary>
        /// ����ǶȲ���
        /// </summary>
        public const string AUTO_ANGLE_STEP = "angle_step";
        /// <summary>
        /// ��С�Աȶ�
        /// </summary>
        public const string AUTO_MIN_CONTRAST = "min_contrast";
        /// <summary>
        /// ���Ż���ʽ
        /// </summary>
        public const string AUTO_OPTIMIZATION = "optimization";

        /// <summary>
        /// ��ʼ�Ƕ�
        /// AngleStart.
        /// </summary>
        public const string BUTTON_ANGLE_START = "angle_start";
        /// <summary>
        /// ���Ƕ�
        /// </summary>
        public const string BUTTON_ANGLE_EXTENT = "angle_extent";
        /// <summary>
        /// ��С����
        /// </summary>
        public const string BUTTON_SCALE_MIN = "scale_min";
        /// <summary>
        /// ������
        /// </summary>
        public const string BUTTON_SCALE_MAX = "scale_max";
        /// <summary>
        /// ������ʽ����
        /// </summary>
        public const string BUTTON_METRIC = "metric";
        /// <summary>
        /// ��С����
        /// </summary>
        public const string BUTTON_MINSCORE = "min_score";
        /// <summary>
        /// ̰���̶�
        /// </summary>
        public const string BUTTON_GREEDINESS = "greediness";

        public const string BUTTON_NUM_MATCHES = "numMatches";

        public const string BUTTON_MAX_OVERLAP = "maxOverlap";

        public const string BUTTON_SUB_PIXEL = "subPixel";
        /// <summary>
        /// ����Ҫ����ʵ�����������û�ָ����ģ����
        /// </summary>
        public const string RECOGM_MANUALSELECT = "RecognFindSpecifiedNumber";
        /// <summary>
        ///�������ٲ��Ҹ���
        /// </summary>
        public const string RECOGM_ATLEASTONE = "RecognAtLeast";
        /// <summary>
        /// �����Ҹ���
        /// </summary>
        public const string RECOGM_MAXNUMBER = "RecognFindMaximum";

        /// <summary>
        /// ���������޸�
        /// </summary>
        public const string RANGE_SCALE_STEP = "RangeScaleStep";
        /// <summary>
        /// �ǶȲ����޸�
        /// </summary>
        public const string RANGE_ANGLE_STEP = "RangeAngleStep";

        /// <summary>
        ///������Ϣ
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
        /// ��ȸ��Ʋ���
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
        /// �������ȼ�����
        /// </summary>
        /// <param name="val"></param>
        public void SetNumLevel(double val)
        {
            PyramidLevel = (int)val;
            if (ParamAutoList.Contains(MatchingParam.AUTO_NUM_LEVEL))
                ParamAutoList.Remove(MatchingParam.AUTO_NUM_LEVEL);
        }

        /// <summary>
        /// �Աȶ�����
        /// </summary>
        /// <param name="val"></param>
        public void SetContrast(int val)
        {
            Contrast = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_CONTRAST))
                ParamAutoList.Remove(MatchingParam.AUTO_CONTRAST);
        }

        /// <summary>
        /// ������С���ű���
        /// </summary>
        /// <param name="val"></param>
        public void SetMinScale(double val)
        {
            MinScale = val;
        }

        /// <summary>
        /// ����������ű���
        /// </summary>
        /// <param name="val"></param>
        public void SetMaxScale(double val)
        {
            MaxScale = val;
        }

        /// <summary>
        /// ���ñ�������
        /// </summary>
        /// <param name="val"></param>
        public void SetScaleStep(double val)
        {
            ScaleStep = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_SCALE_STEP))
                ParamAutoList.Remove(MatchingParam.AUTO_SCALE_STEP);

        }

        /// <summary>
        /// ���ýǶȲ���
        /// </summary>
        /// <param name="val"></param>
        public void SetAngleStep(double val)
        {
            AngleStep = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_ANGLE_STEP))
                ParamAutoList.Remove(MatchingParam.AUTO_ANGLE_STEP);
        }

        /// <summary>
        /// ������С�Աȶ�
        /// </summary>
        /// <param name="val"></param>
        public void SetMinContrast(int val)
        {
            MinContrast = val;

            if (ParamAutoList.Contains(MatchingParam.AUTO_MIN_CONTRAST))
                ParamAutoList.Remove(MatchingParam.AUTO_MIN_CONTRAST);
        }

        /// <summary>
        /// �������Ż���ʽ
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
        /// ������ʼ�Ƕ�ֵ
        /// </summary>
        /// <param name="val"></param>
        public void SetStartingAngle(double val)
        {
            StartingAngle = val;
        }

        /// <summary>
        /// ���ýǶȷ�Χ
        /// </summary>
        /// <param name="val"></param>
        public void SetAngleExtent(double val)
        {
            AngleExtent = val;
        }

        /// <summary>
        /// ���ö�����ʽ
        /// </summary>
        /// <param name="val"></param>
        public void SetMetric(string val)
        {
            Metric = val;
        }

        /// <summary>
        /// ������С����
        /// </summary>
        public void SetMinScore(double val)
        {
            MinScore = val;
        }

        /// <summary>
        /// ����ģ����Ҹ���
        /// </summary>
        public void SetNumMatches(int val)
        {
            NumMatches = val;
        }


        /// <summary>
        /// ����̰���㷨
        /// </summary>
        public void SetGreediness(double val)
        {
            Greediness = val;
        }

        /// <summary>
        /// ��������ص�
        /// </summary>
        public void SetMaxOverlap(double val)
        {
            MaxOverlap = val;
        }

        /// <summary>
        ///���������ز���ģʽ
        /// </summary>
        public void SetSubPixel(string val)
        {
            Subpixel = val;
        }

        /// <summary>
        /// ����������������
        /// </summary>
        public void SetLastPyramLevel(int val)
        {
            LastPyramidLevel = val;
        }

        /*******************************************************************/
        /*******************************************************************/
        /*******************************************************************/

        /// <summary>
        /// ����ʶ����ģʽ
        /// </summary>
        public void SetRecogRateOption(int val)
        {
            RecogRateOpt = val;
        }

        /// <summary>
        /// ����ģ��ʶ����
        /// </summary>
        public void SetRecogitionRate(int val)
        {
            RecogRate = val;
        }

        /// <summary>
        /// �����ٶ��Ż�ģʽ
        /// </summary>
        public void SetRecogSpeedMode(string val)
        {
            RecogSpeedMode = val;
        }

        /// <summary>
        /// ����Ҫ���ҵ�ģ����Ŀ
        /// </summary>
        public void SetRecogManualSelection(int val)
        {
            RecogManualSel = val;
        }

        /// <summary>
        /// ����ƥ�����
        /// </summary>
        public void SetInspectMaxNoMatchValue(int val)
        {
            InspectMaxNoMatch = val;
        }


        /*******************************************************************/
        /*******************************************************************/


        /// <summary>
        /// ����mode�ж����Ƿ����Զ���������������
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
        /// �Ƿ����Զ���������
        /// </summary>
        public bool IsOnAuto()
        {
            if (ParamAutoList.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �����������Ϊ�Զ�����
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
        /// ���Զ����ò����б����Ƴ��������
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
        /// ��ȡ�Զ����õĲ����б�
        /// </summary>
        /// <returns>
        /// �Զ����ò����ַ�������
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
