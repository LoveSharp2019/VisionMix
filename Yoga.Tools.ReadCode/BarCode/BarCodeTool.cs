using System;
using System.Linq;
using HalconDotNet;
using Yoga.ImageControl;
using System.IO;
using Yoga.Common;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Yoga.Wrapper;

namespace Yoga.Tools.BarCode
{
    [Serializable]
    public class BarCodeTool : ToolBase, IToolEnable, IToolRun
    {
        #region 字段
        protected HBarCode barCodeTool = new HBarCode();
        private HTuple codeType = new HTuple();

        public const string CodeTypeErr = "CodeTypeErr";
        private static string toolType = "一维码识别";


        private int startIndex = 0;
        private int length = 30;

        private bool useTextCompare = false;
        HTuple wandCodeType = "auto";

        [NonSerialized]
        HTuple barCodeMessage;

        [NonSerialized]
        public HRegion barCodeRegion;

        public const string CodeTypeAuto = "auto";

        private HTuple decodedDataTypes;

        #endregion

        #region 属性
        /// <summary>
        /// 条码类型
        /// </summary>
        public HTuple CodeType
        {
            get
            {
                return codeType;
            }

            set
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
                return toolType+"\r\n版本:20180421\r\n说明:";
            }
        } 
        #endregion
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

        public HTuple BarCodeMessage
        {
            get
            {
                return barCodeMessage;
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
        #endregion

        public BarCodeTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            barCodeTool.CreateBarCodeModel(new HTuple(), new HTuple());
        }
        private string FindBarCode(bool useMatching, out HTuple decodedDataTypes, out HRegion barCodeRegion)
        {
            decodedDataTypes = CodeTypeErr;
            barCodeRegion = null;
            string codeResult = null;
            barCodeRegion = null;
            decodedDataTypes = CodeTypeErr;
            //GenSearchRegion();
            if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            {
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
                        return null;
                    }
                    HHomMat2D mat2d = matchingTool.RefImageToTestImage(true);
                    if (matchingTool != null && mat2d != null && SearchRegion.IsInitialized())
                    {
                        RegionAffine = SearchRegion.AffineTransRegion(mat2d, "nearest_neighbor");
                    }
                    else
                    {
                        return codeResult;
                    }
                }
                else
                {
                    RegionAffine = SearchRegion.Clone();
                }
                //SearchRegion.Dispose();
            }
            if (CodeType.Length < 1 || ImageTestIn == ImageRefIn)
            {
                CodeType = WandCodeType;
            }
            //HObject obj;
            string useType;
            Fun.MyFindBarCode(ImageTestIn, RegionAffine, out barCodeRegion,
                barCodeTool.Handle, CodeType, out useType, out codeResult);
            if (useType.Length > 0)
            {
                CodeType = useType;
            }
            return codeResult;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new BarCodeToolUnit(this);
            }

            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }

        protected override void RunAct()
        {
            base.RunAct();

            decodedDataTypes = CodeTypeErr;
            barCodeRegion = null;
            barCodeMessage = FindBarCode(Mat2DManger.UseMat2D, out decodedDataTypes, out barCodeRegion);
            if (barCodeMessage != null && barCodeMessage.Length > 0)
            {
                if (UseTextCompare)
                {
                    try
                    {
                        if (barCodeMessage.S.Length < startIndex + length)
                        {
                            this.Result = barCodeMessage.S.Substring(startIndex, barCodeMessage.S.Length - startIndex);
                        }
                        else
                        {
                            this.Result = barCodeMessage.S.Substring(startIndex, length);
                        }
                    }
                    catch (Exception)
                    {
                        this.Result = "";
                    }
                }
                else
                {
                    this.Result = barCodeMessage.S;
                }
            }
            else
            {
                this.Result = "";
            }
            if (UseTextCompare)
            {
                if (Result == this.Target)
                {
                    this.IsOk = true;
                    this.isRealOk = true;
                }
            }
            else
            {
                if (this.Result != "")
                {
                    this.IsOk = true;
                    this.isRealOk = true;
                }
            }

        }
        public override void SerializeCheck()
        {
            base.SerializeCheck();
            if (barCodeTool != null && barCodeTool.IsInitialized() == false)
            {
                barCodeTool = null;

            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (barCodeTool != null)
            {
                barCodeTool.Dispose();
                barCodeTool = null;
            }
            base.Dispose(disposing);
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
            if (barCodeMessage == null || barCodeMessage.Length < 1)
            {
                return;
            }
            if (RegionAffine != null && SearchRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "blue");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(RegionAffine);
            }
            if (barCodeRegion != null && barCodeRegion.IsInitialized())
            {
                viewCtrl.ChangeGraphicSettings(Mode.COLOR, "green");
                viewCtrl.ChangeGraphicSettings(Mode.LINEWIDTH, 2);
                viewCtrl.AddIconicVar(barCodeRegion);
                if (barCodeMessage != null && barCodeMessage.S != "")
                {
                    double row, col;
                    barCodeRegion.AreaCenter(out row, out col);
                    double width = barCodeRegion.RegionFeatures("width");
                    viewCtrl.AddText(barCodeMessage, (int)(row), (int)(col - width / 2), 20, "blue");
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
                return BarCodeMessage.S;
            }
            else
            {
                return ToolBase.ErrDataFlag;
            }
        }
    }
}
