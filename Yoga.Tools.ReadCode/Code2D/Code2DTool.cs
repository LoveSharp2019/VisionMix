using System;
using System.Linq;
using HalconDotNet;
using Yoga.ImageControl;
using Yoga.Common;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Yoga.Common.Helpers;
using Yoga.Tools.CreateImage;

namespace Yoga.Tools.Code2D
{
    public enum FindMode
    {
        [EnumDescription("standard_recognition")]
        标准,
        [EnumDescription("enhanced_recognition")]
        增强,
        [EnumDescription("maximum_recognition")]
        最强
    }
    [Serializable]
    public class Code2DTool : ToolBase, IToolEnable, IToolRun
    {
        #region 字段

        private Dictionary<string, HDataCode2D> dataCode2DDic;



        private string codeType = string.Empty;
        private static string toolType = "二维码识别";

        private int startIndex = 0;
        private int length = 30;
        private bool useTextCompare = false;
        private bool useZxing = false;

        HTuple wandCodeType = "auto";

        FindMode findMode;


        [NonSerialized]
        private HDataCode2D dataCode2d;

        [NonSerialized]
        string barCodeMessage;
        [NonSerialized]
        HObject dataCodexld;
        [NonSerialized]
        HRegion regionAffine = null;

        #endregion
        #region 属性
        /// <summary>
        /// 条码类型
        /// </summary>
        public string CodeType
        {
            get
            {
                return codeType;
            }

            protected set
            {
                codeType = value;
            }
        }
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
        public int StartIndex
        {
            get
            {
                return startIndex;
            }

            set
            {
                startIndex = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public string BarCodeMessage
        {
            get
            {
                return barCodeMessage;
            }
        }
        /// <summary>
        /// 是否同标准文字做对比
        /// </summary>
        public bool UseTextCompare
        {
            get
            {
                return useTextCompare;
            }

            set
            {
                useTextCompare = value;
            }
        }

        public HTuple WandCodeType
        {
            get
            {
                if (wandCodeType == null)
                {
                    wandCodeType = "auto";
                }
                return wandCodeType;
            }

            set
            {
                wandCodeType = value;
            }
        }

        public bool UseZxing
        {
            get
            {
                return useZxing;
            }

            set
            {
                useZxing = value;
            }
        }

        public FindMode FindMode
        {
            get
            {
                return findMode;
            }

            set
            {
                findMode = value;
            }
        }

        public Dictionary<string, HDataCode2D> DataCode2DDic
        {
            get
            {
                if (dataCode2DDic == null)
                {
                    dataCode2DDic = new Dictionary<string, HDataCode2D>();
                }
                return dataCode2DDic;
            }
        }

        public HDataCode2D DataCode2d
        {
            get
            {
                if (dataCode2d == null)
                {
                    dataCode2d = new HDataCode2D();
                }
                return dataCode2d;
            }
            set
            {
                dataCode2d = value;
            }
        }

        #endregion

        public Code2DTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }
        public bool UpdateRegionAffine(bool useMatching)
        {
            if (regionAffine != null && regionAffine.IsInitialized())
            {
                regionAffine.Dispose();
            }
            regionAffine = null;
            if (SearchRegion != null && SearchRegion.IsInitialized())
            {
                if (useMatching && ImageTestIn != ImageRefIn)
                {
                    IMatching matchingTool = Mat2DManger.MatchingTool;
                    if (matchingTool == null)
                    {
                        Util.Notify(string.Format("工具{0}匹配工具不存在!", Name));
                        return false;
                    }
                    HHomMat2D mat2d = matchingTool.RefImageToTestImage(true);
                    if (matchingTool != null && mat2d != null && SearchRegion.IsInitialized())
                    {

                        regionAffine = SearchRegion.AffineTransRegion(mat2d, "nearest_neighbor");
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    regionAffine = SearchRegion.Clone();
                }

            }
            return true;
        }
        private string DecodeDataCode(System.Drawing.Bitmap barcodeBitmap)
        {
            ZXing.BarcodeReader reader = new ZXing.BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            reader.Options.TryHarder = true;
            reader.Options.PureBarcode = false;
            reader.AutoRotate = true;

            //IList<ZXing.BarcodeFormat> PossibleFormats=new List<ZXing.BarcodeFormat>() ;
            //PossibleFormats.Add(ZXing.BarcodeFormat.DATA_MATRIX);
            //PossibleFormats.Add(ZXing.BarcodeFormat.QR_CODE);
            //reader.Options.PossibleFormats = PossibleFormats;
            var result = reader.Decode(barcodeBitmap);
            return (result == null) ? null : result.Text;
        }

        public void DelModel(string key)
        {
            if (DataCode2DDic.ContainsKey(key))
            {
                DataCode2DDic[key].Dispose();
                DataCode2DDic.Remove(key);
            }
        }

        public void DelAllModel()
        {
            foreach (var item in DataCode2DDic.Values)
            {
                item.Dispose();
            }
            DataCode2DDic.Clear();
        }


        public string TrainDataCodeMode()
        {
            DisposeHobject(dataCodexld);

            if (dataCode2d != null && dataCode2d.IsInitialized())
            {
                dataCode2d.Dispose();
            }
            dataCode2d = null;
            string codeResult = null;
            dataCodexld = null;

            if (UpdateRegionAffine(Mat2DManger.UseMat2D) == false)
            {
                return null;
            }
            HTuple toolHandle;
            HTuple default_parameters = EnumHelper.GetDescription(FindMode);
            Wrapper.Fun.MyCreateCode2D(ImageTestIn, regionAffine, WandCodeType, default_parameters, out dataCodexld,
                out toolHandle, out codeType, out codeResult);
            if (toolHandle.Length > 0)//训练成果
            {
                
                dataCode2d = new HDataCode2D(toolHandle.IP);
                //中文乱码纠正
                codeResult = StringHelper.Gb2312Correct(codeResult);
            }
            else
            {
                codeResult = "";
                CodeType = "";
            }

            return codeResult;
        }
        protected string FindDataCode()
        {
            string codeResult = null;
            dataCodexld = null;

            if (UpdateRegionAffine(Mat2DManger.UseMat2D) == false)
            {
                return null;
            }
            foreach (var item in DataCode2DDic)
            {
                string resultTmp = null;
                Wrapper.Fun.MyFindCode2D(ImageTestIn, regionAffine, out dataCodexld,
                item.Value.Handle, out resultTmp);

                if (resultTmp != null && resultTmp.Length > 0)
                {
                    codeResult = resultTmp;
                    Util.Notify($"{Name}使用模板 {item.Key} 找到二维码");
                    break;// 跳出循环
                }
            }

            if (codeResult == null || codeResult.Length < 1)
            {
                if (UseZxing)
                {
                    int channel = ImageTestIn.CountChannels();
                    if (channel == 1)
                    {
                        System.Drawing.Bitmap bitmap;
                        Util.HObject2Bpp8(ImageTestIn, out bitmap);
                        string zxingStr = DecodeDataCode(bitmap);
                        bitmap.Dispose();
                        if (zxingStr != null && zxingStr.Length > 0)
                        {
                            codeResult = zxingStr;
                        }
                        else
                        {
                            codeResult = "";
                        }
                    }
                }
                else
                {
                    codeResult = "";
                }
            }
            else
            {
                //中文乱码纠正
                codeResult = StringHelper.Gb2312Correct(codeResult);
            }
            return codeResult;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new Code2DToolUnit(this);
            }
            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }
        public override void RunRef()
        {
            RunStart();
            //如果没有测试图像就用模板图像作为训练对象
            if (ImageTestIn==null)
            {
                ImageTestIn = ImageRefIn;
            }
            if ((ImageTestIn == null || ImageTestIn.IsInitialized() == false))
            {
                return;
            }
            barCodeMessage = TrainDataCodeMode();

            ResultCodeConversion();

            RunEnd();
        }
        protected override void RunAct()
        {
            base.RunAct();
            DisposeHobject(dataCodexld);

            barCodeMessage = FindDataCode();
            ResultCodeConversion();

            if (useTextCompare)
            {
                if (this.Result == this.Target)
                {
                    this.IsOk = true;
                    this.isRealOk = true;
                }
            }
            else
            {
                if (this.Result != "" && this.Result != null)
                {
                    this.IsOk = true;
                    this.isRealOk = true;
                }
            }
        }

        private void ResultCodeConversion()
        {
            if (barCodeMessage != null && barCodeMessage.Length > 0)
            {
                if (UseTextCompare)
                {
                    //文字识别可能出现异常,此处直接屏蔽
                    try
                    {
                        if (barCodeMessage.Length < startIndex + length)
                        {
                            this.Result = barCodeMessage.Substring(startIndex, barCodeMessage.Length - startIndex);
                        }
                        else
                        {
                            this.Result = barCodeMessage.Substring(startIndex, length);
                        }
                    }
                    catch (Exception)
                    {
                        this.Result = "";
                    }
                }
                else
                {
                    this.Result = barCodeMessage;
                }


            }
            else
            {
                this.Result = "";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (dataCode2d != null)
            {
                dataCode2d.Dispose();
                dataCode2d = null;
            }
            foreach (var item in DataCode2DDic.Values)
            {
                item.Dispose();
            }
            DataCode2DDic.Clear();
            base.Dispose(disposing);
        }
        public override void SerializeCheck()
        {

            if (dataCode2d != null && dataCode2d.IsInitialized() == false)
            {
                dataCode2d = null;
            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
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
            if (SearchRegion.IsInitialized() && regionAffine != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(regionAffine);
            }
            if (dataCodexld != null)
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 1);
                viewCtrl.AddIconicVar(dataCodexld);
                if (barCodeMessage != null && barCodeMessage != ""&& barCodeMessage.Length>0)
                {
                    HObject regionTmp = new HObject();
                    HOperatorSet.GenRegionContourXld(dataCodexld, out regionTmp, "filled");
                    HTuple area, row, col, width;
                    HOperatorSet.AreaCenter(regionTmp, out area, out row, out col);
                    if (area != null && area.Length == 1 && area.D > 0.0)
                    {
                        HOperatorSet.RegionFeatures(regionTmp, "width", out width);
                        regionTmp.Dispose();
                        viewCtrl.AddText(barCodeMessage, (int)(row.D), (int)(col.D - width.D / 2), 20, "blue");
                    }

                }


            }
        }

        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            if (RuningFinish && BarCodeMessage.Length > 0)
            {
                return BarCodeMessage;
            }
            else
            {
                return ToolBase.ErrDataFlag;
            }
        }
    }
}
