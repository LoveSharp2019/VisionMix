/*
参考halcon示例:find_text_dongle.hdev
    
当前正邦软件ocr参数有 
1.字符宽度 
2.字符高度
3.笔画宽度
4.最小片段
5.行最大数量
6.字符数量
7.基线公差
8.黑色背景
9.存在标点符号
10.全大写
11.联通片段
12.去除背景横线

*/


using System;
using HalconDotNet;
using Yoga.ImageControl;
using Yoga.Common;
/// <summary>
/// ocr工具
/// </summary>
namespace Yoga.Tools.OcrHand
{
    [Serializable]
    public class OcrHandTool : ToolBase, IToolRun, IToolEnable, IOCRTool
    {
        #region 字段

        /// <summary>
        /// ocr字库文件路径
        /// </summary>
        string ocrClassifierPath;
        private static string toolType = "OCR分割识别";

        /// <summary>
        /// ocr分类器
        /// </summary>
        [NonSerialized]
        HOCRMlp ocrMpl = new HOCRMlp();
        //[NonSerialized]
        //HOCRCnn ocrCnn = new HOCRCnn();
        [NonSerialized]
        Classifier classifier;
        /// <summary>
        /// 查找到的文字结果
        /// </summary>
        [NonSerialized]
        private HRegion textRegion;
        [NonSerialized]
        private string resultText = "";
        [NonSerialized]
        HHomMat2D mat2dMatching;
        double dynThreadVal = 15;
        double closeCircleRadius = 1;
        double openCircleRadius = 1;
        double minCharArea = 10;
        double maxCharArea = 99999;



        private int maxCharHeight = 100;
        /// <summary>
        /// 文字极性 默认为白底黑字
        /// </summary>
        private string polarity = "dark_on_light";
        /// <summary>
        /// 最小文字高度 
        /// </summary>
        private int minCharHeight = 5;

        private int minCharWidth = 5;

        private int maxCharWidth = 150;

        private int partitionCharWidth = 15;
        /// <summary>
        /// 正则表达式
        /// </summary>
        string expression = "([A-Z]{2}[0-9]{6}/[0-9]{4})";
        /// <summary>
        /// 模板图像上的要查找的的区域region
        /// </summary>
        private HRegion modelRegion = new HRegion();

        public HRegion ModelRegion
        {
            get { return modelRegion; }
            set { modelRegion = value; }
        }
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
                return toolType + "\r\n版本:20180421\r\n说明:使用手动分割字符方式提取文字,在自动分割不理想状况下使用此工具";
            }
        }
        #endregion

        public string Polarity
        {
            get
            {
                return polarity;
            }

            set
            {
                polarity = value;
            }
        }



        public string ResultText
        {
            get
            {
                return resultText;
            }
        }

        public string OcrClassPath
        {
            get
            {
                return ocrClassifierPath;
            }

            set
            {
                ocrClassifierPath = value;
            }
        }


        public int MinCharHeight
        {
            get
            {
                return minCharHeight;
            }

            set
            {
                minCharHeight = value;
            }
        }


        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Expression
        {
            get
            {
                return expression;
            }

            set
            {
                expression = value;
            }
        }


        public int MaxCharHeight
        {
            get
            {
                return maxCharHeight;
            }

            set
            {
                maxCharHeight = value;
            }
        }



        public int MinCharWidth
        {
            get
            {
                return minCharWidth;
            }

            set
            {
                minCharWidth = value;
            }
        }

        public int MaxCharWidth
        {
            get
            {
                return maxCharWidth;
            }

            set
            {
                maxCharWidth = value;
            }
        }

        public double DynThreadVal
        {
            get
            {
                return dynThreadVal;
            }

            set
            {
                dynThreadVal = value;
            }
        }

        public double CloseCircleRadius
        {
            get
            {
                return closeCircleRadius;
            }

            set
            {
                closeCircleRadius = value;
            }
        }

        public double OpenCircleRadius
        {
            get
            {
                return openCircleRadius;
            }

            set
            {
                openCircleRadius = value;
            }
        }

        public double MinCharArea
        {
            get
            {
                return minCharArea;
            }

            set
            {
                minCharArea = value;
            }
        }

        public double MaxCharArea
        {
            get
            {
                return maxCharArea;
            }

            set
            {
                maxCharArea = value;
            }
        }

        public int PartitionCharWidth
        {
            get
            {
                return partitionCharWidth;
            }

            set
            {
                partitionCharWidth = value;
            }
        }






        #endregion
        public OcrHandTool(int settingIndex)
        {
            base.settingIndex = settingIndex;

            Min = 0.6;
        }
        public void CreateOCRTool()
        {
            if (ocrClassifierPath == null || ocrClassifierPath.Length < 1 || System.IO.File.Exists(ocrClassifierPath) == false)
            {
                throw new Exception("字库文件加载异常");
            }
            if (ocrClassifierPath.EndsWith("omc"))
            {
                classifier = Classifier.MPL;
            }
            else if (ocrClassifierPath.EndsWith("occ"))
            {
                classifier = Classifier.CNN;
            }
            else
            {
                throw new Exception("字库文件加载异常");
            }
            isCreateTool = false;
            switch (classifier)
            {
                case Classifier.MPL:
                    if (ocrMpl != null)
                    {

                        ocrMpl.Dispose();
                    }
                    ocrMpl = new HOCRMlp();
                    ocrMpl.ReadOcrClassMlp(ocrClassifierPath);
                    isCreateTool = true;
                    break;
                case Classifier.CNN:
                    //if (ocrCnn != null)
                    //{

                    //    ocrCnn.Dispose();
                    //}
                    //ocrCnn = new HOCRCnn();
                    //ocrCnn.ReadOcrClassCnn(ocrClassifierPath);
                    isCreateTool = true;
                    break;
                default:
                    break;
            }

        }
        public void FindText(bool useMatching)
        {
            //if (testImage.CountChannels() != 1)
            //{
            //    throw new Exception("图像不为单通道图像");
            //}
            HImage findImage = null;

            resultText = "";
            if (textRegion!=null&&textRegion.IsInitialized())
            {
                textRegion.Dispose();
            }
            textRegion = null;
            //GenSearchRegion();
            mat2dMatching = null;
            if (ModelRegion == null || ModelRegion.IsInitialized() == false)
            {
                return;
            }
            else
            {

                if (RegionAffine!=null&& RegionAffine.IsInitialized())
                {
                    RegionAffine.Dispose();

                }
                RegionAffine = null;
                if (useMatching && ImageTestIn != ImageRefIn)
                {
                    IMatching matchingTool = Mat2DManger.MatchingTool;
                    if (matchingTool == null)
                    {
                        Util.Notify(string.Format("工具{0}匹配工具不存在!", Name));
                        return;
                    }
                    //测试图像转换为模板图像位置
                    mat2dMatching = matchingTool.TestimageToRefImage(true);
                    if (mat2dMatching==null)
                    {
                        return;
                    }
                    findImage = ImageTestIn.AffineTransImage(mat2dMatching, "constant", "false");
                    //矩阵变换回测试图像位置 为显示做准备
                    if (matchingTool != null && mat2dMatching != null && ModelRegion != null)
                    {
                        mat2dMatching = matchingTool.RefImageToTestImage(true);
                    }
                    else
                    {
                        return;
                    }
                }
                else //if (ImageTestIn == ImageRefIn)
                {
                    findImage = ImageTestIn.Clone();
                }
                if (findImage == null)
                {
                    return;
                }

            }

            HTuple ocrType = "";
            HTuple ocrhandle=null;
            switch (classifier)
            {
                case Classifier.MPL:
                    ocrhandle = ocrMpl.Handle;
                    ocrType = "mpl";
                    break;
                case Classifier.CNN:
                    //ocrhandle = ocrCnn.Handle;
                    ocrType = "cnn";
                    break;
                default:
                    break;
            }
            double m_scare ;

            string m_result;

            Wrapper.OcrParam param = new Wrapper.OcrParam();

            param.polarity = polarity;
            param.ocrType = ocrType;
            param.expression =expression.Trim();

            param.thresholdValue =(int) dynThreadVal;
            param.closeCircleRadius = closeCircleRadius;
            param.openCircleRadius = openCircleRadius;
            param.minCharArea =(int) minCharArea;
            param.maxCharArea = (int)maxCharArea;
            param.minCharWidth = minCharWidth;
            param.maxCharWidth = maxCharWidth;
            param.minCharHeight = minCharHeight;
            param.maxCharHeight = maxCharHeight;
            param.partitionCharWidth = partitionCharWidth;

            Wrapper.Fun.MyFindText(findImage, ModelRegion, out textRegion,  ocrhandle, param,
               out m_scare, out m_result);

            //Wrapper.Fun.MyFindText(findImage, ModelRegion, out textRegion, polarity, ocrhandle, ocrType,
            //   new HTuple( expression.Trim()), (int)dynThreadVal, closeCircleRadius, openCircleRadius,
            //    (int)minCharArea, (int)maxCharArea, minCharWidth, maxCharWidth, minCharHeight, charHeight, charWidth,
            //    out m_scare, out m_result);

            //结果分数
            Result = m_scare.ToString("f2");
            if (m_result.Length > 0)
            {
                resultText = m_result;
            }       
            Message = string.Format("识别结果{0}", resultText);
            findImage.Dispose();
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            return new OcrHandToolUnit(this);
        }

        public override string GetToolType()
        {
            return toolType;
        }
        protected override void Dispose(bool disposing)
        {
            if (ocrMpl != null)
            {
                ocrMpl.Dispose();
                ocrMpl = null;
            }

            //if (ocrCnn != null)
            //{
            //    ocrCnn.Dispose();
            //    ocrCnn = null;
            //}
            base.Dispose(disposing);
        }
        public override void LoadTool()
        {
            base.LoadTool();
            if (isCreateTool)
            {
                CreateOCRTool();
            }         
        }
        protected override void RunAct()
        {
            base.RunAct();
            FindText(Mat2DManger.UseMat2D);
            if (resultText != null && resultText.Length > 0)
            {
                double score = double.Parse(Result);
                if (score > Min)
                {
                    this.IsOk = true;
                    this.isRealOk = true;
                }

            }
        }

        public override void SerializeCheck()
        { 

            if (modelRegion != null && modelRegion.IsInitialized() == false)
            {
                modelRegion = null;
            }
            base.SerializeCheck();
            using (System.IO.Stream objectStream = new System.IO.MemoryStream())
            {
                System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(objectStream, this);
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
            if (ModelRegion != null&& ModelRegion.IsInitialized())
            {
                if (mat2dMatching != null)
                {
                    RegionAffine = ModelRegion.AffineTransRegion(mat2dMatching, "nearest_neighbor");
                }
                else
                {
                    RegionAffine = ModelRegion.Clone();
                }
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(RegionAffine);
            }
            if (textRegion != null&& textRegion.IsInitialized())
            {
                if (mat2dMatching != null)
                {
                    HRegion tmp= textRegion.AffineTransRegion(mat2dMatching, "nearest_neighbor");
                    textRegion.Dispose();
                    textRegion = tmp;
                }

                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(textRegion);

                HTuple r1, c1, l1, l2, phi,num;
                HOperatorSet.SmallestRectangle2(textRegion, out r1, out c1, out phi, out l1, out l2);
                //HOperatorSet.SmallestRectangle1(textHObject, out r1, out c1, out l1, out l2);
                HOperatorSet.CountObj(textRegion, out num);
                for (int i = 0; i < num; i++)
                {
                    if (resultText.Length < i)
                    {
                        break;
                    }
                    string ss = resultText.Substring(i, 1);
                    int rr =(int) (r1[i].D + 3);
                    int cc = (int)(c1[i].D - 3);
                    int size = (int)(Math.Abs(l2[0].D)*2);
                    viewCtrl.AddText(ss,rr ,cc , size, "blue");
                }

            }
        }

        public string GetOCRResultText()
        {
            return resultText;
        }

        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            string dat = ErrDataFlag;
            if (resultText != "")
            {
                dat = string.Format("{0}", resultText);
            }
            else
            {
                dat = NgDataFlag;
            }
            return dat;
        }
    }
}
