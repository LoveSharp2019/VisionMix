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

enum Classifier
{
    MPL,
    CNN
}
/// <summary>
/// ocr工具
/// </summary>
namespace Yoga.Tools.OCR
{
    [Serializable]
    public class OCRTool : ToolBase, IToolRun, IToolEnable,IOCRTool
    {
        #region 字段

        /// <summary>
        /// ocr字库文件路径
        /// </summary>
        string ocrClassifierPath;
        private static string toolType = "OCR识别";

        /// <summary>
        /// 文本读取,不支持序列化
        /// </summary>
        [NonSerialized]
        HTextModel textModel = new HTextModel();
        /// <summary>
        /// ocr分类器
        /// </summary>
        [NonSerialized]
        HOCRMlp ocrMpl = new HOCRMlp();
        //HOCRCnn ocrCnn = new HOCRCnn();
        [NonSerialized]
        Classifier classifier;
        /// <summary>
        /// 查找到的文字结果
        /// </summary>
        [NonSerialized]
        private HObject textHObject;
        [NonSerialized]
        private string resultText = "";

        HHomMat2D mat2dMatching;
        HHomMat2D mat2dRotate;
        /// <summary>
        /// 最小对比度,默认值15
        /// </summary>
        private double minContrast = 15;

        private int charHeight = 50;
        /// <summary>
        /// 文字极性 默认为both
        /// </summary>
        private string polarity = "dark_on_light";
        /// <summary>
        /// 最小文字高度 
        /// </summary>
        private int minCharHeight =10;

        private int minCharWidth = 5;

        private int maxCharWidth = 15;
        private int minStrokeWidth = 1;
        private int maxStrokeWidth = 4;
        private string textLineSeparators = "";
        private string textLineStructure = "";
        private bool isDotPrint = false;
        private bool isAddFragments = false;
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
                return toolType + "\r\n版本:20180421\r\n说明:";
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
        /// <summary>
        /// 文本分隔符
        /// </summary>
        public string TextLineSeparators
        {
            get
            {
                return textLineSeparators;
            }

            set
            {
                textLineSeparators = value;
            }
        }
        /// <summary>
        /// 文本结构
        /// </summary>
        public string TextLineStructure
        {
            get
            {
                return textLineStructure;
            }

            set
            {
                textLineStructure = value;
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

        public double MinContrast
        {
            get
            {
                return minContrast;
            }

            set
            {
                double dat = value;
                if (dat < 0)
                {
                    minContrast = 0;
                }
                else if (dat > 255)
                {
                    minContrast = 255;
                }
                else
                {
                    minContrast = value;
                }
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

        public bool IsDotPrint
        {
            get
            {
                return isDotPrint;
            }

            set
            {
                isDotPrint = value;
            }
        }

        public int CharHeight
        {
            get
            {
                return charHeight;
            }

            set
            {
                charHeight = value;
            }
        }

        public bool IsAddFragments
        {
            get
            {
                return isAddFragments;
            }

            set
            {
                isAddFragments = value;
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

        public int MinStrokeWidth
        {
            get
            {
                return minStrokeWidth;
            }

            set
            {
                minStrokeWidth = value;
            }
        }

        public int MaxStrokeWidth
        {
            get
            {
                return maxStrokeWidth;
            }

            set
            {
                maxStrokeWidth = value;
            }
        }


        #endregion
        public OCRTool(int settingIndex)
        {
            base.settingIndex = settingIndex;

            Min = 0.6;
        }
        public void CreateOCRTool()
        {
            if (ocrClassifierPath == null || ocrClassifierPath.Length < 1||System.IO.File.Exists(ocrClassifierPath)==false)
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
            switch (classifier)
            {
                case Classifier.MPL:
                    if (ocrMpl != null)
                    {

                        ocrMpl.Dispose();
                    }
                    ocrMpl = new HOCRMlp();
                    ocrMpl.ReadOcrClassMlp(ocrClassifierPath);

                    if (textModel != null)
                    {
                        textModel.Dispose();
                    }
                    textModel = new HTextModel();
                    //字符库选择后使用
                    textModel.CreateTextModelReader("auto", ocrMpl);
                    break;
                case Classifier.CNN:
                    //if (ocrCnn != null)
                    //{

                    //    ocrCnn.Dispose();
                    //}
                    //ocrCnn = new HOCRCnn();
                    //ocrCnn.ReadOcrClassCnn(ocrClassifierPath);

                    if (textModel != null)
                    {
                        textModel.Dispose();
                    }
                    textModel = new HTextModel();
                    //字符库选择后使用
                    //textModel.CreateTextModelReader("auto", ocrCnn);
                    break;
                default:
                    break;
            }
            //最小对比度
            textModel.SetTextModelParam("min_contrast", minContrast);
            //字符极性
            textModel.SetTextModelParam("polarity", polarity);

           
            //丢弃边界
            textModel.SetTextModelParam("eliminate_border_blobs", "true");

            //最小高度
            textModel.SetTextModelParam("min_char_height", minCharHeight);
            //最大高度-文字方向时候也使用
            textModel.SetTextModelParam("max_char_height", charHeight);

            //最小宽度
            textModel.SetTextModelParam("min_char_width", minCharWidth);
            //最大宽度
            textModel.SetTextModelParam("max_char_width", maxCharWidth);

            //最小笔画
            textModel.SetTextModelParam("min_stroke_width", minStrokeWidth);
            //最大笔画
            textModel.SetTextModelParam("max_stroke_width", maxStrokeWidth);
            //是否识别i
            string addFragments = "false";
            if (isAddFragments)
            {
                addFragments = "true";
            }
            
            textModel.SetTextModelParam("add_fragments", addFragments);

            //分隔符
            textModel.SetTextModelParam("text_line_separators", textLineSeparators.Trim());
            //文本结构
            textModel.SetTextModelParam("text_line_structure", textLineStructure.Trim());
            
            if (IsDotPrint)
            {
                textModel.SetTextModelParam("dot_print", "true");
            }
            else
            {
                textModel.SetTextModelParam("dot_print", "false");
            }
            //不返回标点符号
            textModel.SetTextModelParam("return_punctuation", "false");
            //不返回分隔符
            textModel.SetTextModelParam("return_separators", "false");

        }
        public void FindText(bool useMatching)
        {
            //if (testImage.CountChannels() != 1)
            //{
            //    throw new Exception("图像不为单通道图像");
            //}
            HImage findImage;
            resultText = "";
            textHObject = null;
            //GenSearchRegion();
            mat2dMatching = null;
            mat2dRotate = null;
            if (ModelRegion == null || ModelRegion.IsInitialized() == false)
            {
                findImage = ImageTestIn.CopyImage();
                RegionAffine = null;
            }
            else
            {
                if (useMatching && ImageTestIn != ImageRefIn)
                {
                    IMatching matchingTool = Mat2DManger.MatchingTool;
                    if (matchingTool == null)
                    {
                        Util.Notify(string.Format("工具{0}匹配工具不存在!", Name));
                        return;
                    }
                    mat2dMatching = matchingTool.RefImageToTestImage(true);

                    if (matchingTool != null && mat2dMatching !=null && ModelRegion != null)
                    {
                        
                        RegionAffine = ModelRegion.AffineTransRegion(mat2dMatching, "nearest_neighbor");

                        mat2dMatching = matchingTool.TestimageToRefImage(true);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    RegionAffine = ModelRegion.Clone();
                }
                findImage = ImageTestIn.Clone();
                HTuple angle;
                if (findImage.CountChannels()==3)
                {
                    HImage tmp = findImage.Rgb1ToGray();
                    findImage.Dispose();
                    findImage = tmp;
                }
                HImage angleImg = null;
                if (Polarity == "light_on_dark")
                {
                    angleImg = findImage.InvertImage();
                }
                else
                {
                    angleImg = findImage.Clone();
                }
                HOperatorSet.TextLineOrientation(RegionAffine, angleImg, CharHeight, (new HTuple(-30)).TupleRad()
            , (new HTuple(30)).TupleRad(), out angle);

                angleImg.Dispose();

                mat2dRotate = new HHomMat2D();
                HTuple t = angle.TupleDeg();
                Common.Util.Notify(string.Format("{0}文字角度{1:f2}°",Name,t.D));
                mat2dRotate.VectorAngleToRigid(0, 0, 0, 0, 0, -angle);



                RegionAffine = RegionAffine.AffineTransRegion(mat2dRotate, "nearest_neighbor");

                findImage = findImage.AffineTransImage(mat2dRotate, "constant", "false");
                findImage = findImage.ReduceDomain(RegionAffine);

                mat2dRotate.VectorAngleToRigid(0, 0, -angle, 0, 0, 0);

            }
           
            HTextResult textResult = textModel.FindText(findImage);
            //判断查找到的文字的行数来判断是否找到文字
            HTuple numLines = textResult.GetTextResult("num_lines");
            if (textHObject != null && textHObject.IsInitialized())
            {
                textHObject.Dispose();
            }
            if (numLines == null || numLines < 1)
            {
                return;
            }
            //使用正则表达式来筛选文字
            textHObject = textResult.GetTextObject("all_lines");

           HTuple r1= textResult.GetTextResult("class");
            HTuple confidence=null;
            //查找到的字符串
            HTuple resultClass;
            HTuple word=null, score=null;
            //  HImage imgtt = findImage.InvertImage();


            HImage ocrImg = null;
            if (Polarity == "light_on_dark")
            {
                ocrImg = findImage.InvertImage();
            }
            else
            {
                ocrImg = findImage.Clone();
            }

            switch (classifier)
            {
                case Classifier.MPL:
                    HOperatorSet.DoOcrWordMlp(textHObject, ocrImg, ocrMpl, expression.Trim(),
           3, 5, out resultClass, out confidence, out word, out score);
                    break;
                case Classifier.CNN:
           //         HOperatorSet.DoOcrWordCnn(textHObject, ocrImg, ocrCnn,expression.Trim(),
           //3, 5, out resultClass, out confidence, out word, out score);
                    break;
                default:
                    break;
            }
            ocrImg.Dispose();
            //imgtt.Dispose();
            //取信心与正则表达式的最小值来作为最后识别结果的分值
            HTuple scoreTmp = new HTuple(confidence, score);

            //结果分数
            Result = scoreTmp.TupleMin().D.ToString("f2");
            if (word.Length > 0)
            {
                resultText = word;
            }
            Message = string.Format("识别结果{0}", resultText);
            findImage.Dispose();
            textResult.Dispose();
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            return new OCRToolUnit(this);
        }

        public override string GetToolType()
        {
            return toolType;
        }
        protected override void RunAct()
        {
            base.RunAct();
            if (textModel == null)
            {
                textModel = new HTextModel();
                CreateOCRTool();
            }

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
            if (modelRegion != null && modelRegion.IsInitialized()==false)
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
            if (RegionAffine != null)
            {
                if (mat2dRotate != null)
                {
                    RegionAffine = RegionAffine.AffineTransRegion(mat2dRotate, "nearest_neighbor");
                }
                //if (mat2dMatching != null)
                //{
                //    RegionAffine = RegionAffine.AffineTransRegion(mat2dMatching, "nearest_neighbor");
                //}
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(RegionAffine);
            }
            if (textHObject != null)
            {
                if (mat2dRotate != null)
                {
                    HOperatorSet.AffineTransRegion(textHObject, out textHObject, mat2dRotate, "nearest_neighbor");
                }

                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(textHObject);

                HTuple r1, c1, r2, c2, num;
                HOperatorSet.SmallestRectangle1(textHObject, out r1, out c1, out r2, out c2);
                HOperatorSet.CountObj(textHObject, out num);
                for (int i = 0; i < num; i++)
                {
                    if (resultText.Length < i)
                    {
                        break;
                    }
                    viewCtrl.AddText(resultText.Substring(i, 1), r2[i] + 3, c1[i] - 3, 10, "blue");
                }

            }
        }
        protected override void Dispose(bool disposing)
        {
            if (ocrMpl!=null)
            {
                ocrMpl.Dispose();
                ocrMpl = null;
            }

            //if (ocrCnn != null)
            //{
            //    ocrCnn.Dispose();
            //    ocrCnn = null;
            //}
            if (textModel != null)
            {
                textModel.Dispose();
                textModel = null;
            }
            base.Dispose(disposing);
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
            if (resultText!="")
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
