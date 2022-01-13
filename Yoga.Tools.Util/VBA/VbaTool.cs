using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Common;
using Yoga.Common.VBAEngine;
using Yoga.ImageControl;
using Yoga.Tools.Matching;

namespace Yoga.Tools.VBA
{
    [Serializable]
    /// <summary>
    /// 印刷检测助手
    /// </summary>
    public class VbaTool : ToolBase, IToolEnable, IToolRun
    {

        #region 字段
        private static string toolType = "脚本工具";


       public const string vbaStartStr = "sub Cal() \r\ntry \r\n";
       public const string vbaEndStr = "\r\nCatch ex As Exception \r\nh.show(ex.tostring) \r\nEnd Try \r\nend sub";


        [NonSerialized]
        private XVBAEngine vbaEngine = null;

        private string _Script = string.Empty;

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
                return toolType + "\r\n版本:20180712\r\n说明:使用vb脚本访问各个工具";
            }
        }


        #endregion

        public XVBAEngine VBAEngine
        {
            get
            {
                if (vbaEngine==null )
                {
                    vbaEngine = new XVBAEngine();


                    vbaEngine.VBCompilerImports.Add("Yoga.Common.VBAEngine");
                    vbaEngine.VBCompilerImports.Add("Yoga.Tools.Factory");
                    vbaEngine.AddReferenceAssemblyByType(this.GetType());
                    //vbaEngine.AddReferenceAssemblyByType((new Measure.HMeasureSYS()).GetType());
                    vbaEngine.AddReferenceAssemblyByType(typeof(Yoga.Common.VBAEngine.GlobalObject));
                    vbaEngine.AddReferenceAssemblyByType(typeof(Yoga.Tools.Factory.ToolObject));
                    //vbaEngine.VBCompilerImports.Add("Measure.UserDefine");

                }
                return vbaEngine;
            }

            set
            {
                vbaEngine = value;
            }
        }

        public string Script
        {
            get
            {
               
                return _Script;
            }

            set
            {
                VBAEngine.ScriptText = value;
               _Script = value;
            }
        }
        #endregion

        /// <summary>
        /// 印刷检测构造
        /// </summary>
        public VbaTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            ExceptionText = "";

        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }





        public override string GetToolType()
        {
            return toolType;
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            return new VabToolUnit(this);
        }

        protected override void RunAct()
        {
            base.RunAct();

            this.Result = "NG";
            
            VBAEngine.Execute("Cal", null, true);
        }

        public override void LoadTool()
        {
            base.LoadTool();
            try
            {
                VBAEngine.ScriptText = Script;
                //此处不编译也可,但是第一次运行速度很慢,预计是编译影响
                VBAEngine.Compile();
            }
            catch (Exception)
            {
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

        }

        public override void SerializeCheck()
        {
            base.SerializeCheck();
        }
        public override void ClearTestData()
        {
            base.ClearTestData();
        }
        public override void ClearTrainData()
        {
            base.ClearTrainData();
            //ClearTrainedImage();
        }
        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            return Result;
        }
    }
}
