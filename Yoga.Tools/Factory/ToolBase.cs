using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.ImageControl;

namespace Yoga.Tools
{
    [Serializable]
    public abstract class ToolBase : IDisposable, ISerializeCheck
    {

        protected string imageSoureToolName;
        private int refImageKey=-1;
        private string tag = "";
        private string name = "";
        protected Mat2DManger mat2dManger;
        protected int settingIndex = 1;
        protected string note;
        private bool enable = true;
        private string target;
        private double min;
        private double max;

        private bool isOutputResults = false;
        public List<ROI> ROIList = new List<ROI>();
        HRegion searchRegion;

        private bool isCalibOut=false;

        protected bool isCreateTool = false;
        [NonSerialized]
        private ICalibration calibTool;
        [NonSerialized]
        private string result;
        [NonSerialized]
        private bool isOk = false;
        [NonSerialized]
        protected bool isRealOk = false;

        [NonSerialized]
        private bool runingFinish = false;
        [NonSerialized]
        protected HImage imageTestIn;
        [NonSerialized]
        /// <summary>
        /// 测试图像字典列表
        /// </summary>
        public Dictionary<string, HImage> TestImageDic;

        
        /// <summary>
        /// 用于搜索框及其他模板region对应于定位偏移后的region
        /// </summary>
        [NonSerialized]
        public HRegion RegionAffine;

        [NonSerialized]
        public string Message;
        [NonSerialized]
        /// <summary>
        /// 异常内容
        /// </summary>
        public string ExceptionText = "";
        [NonSerialized]
        protected double executionTime = 0;
        [NonSerialized]
        protected ToolsSettingUnit settingUnit;
        [NonSerialized]
        protected HTuple runStartTime;
        [NonSerialized]
        protected HTuple runEndTime;
        [NonSerialized]
        private ToolBase imageSoureTool;
        [NonSerialized]
        HImage imageTestOut;
        /// <summary>
        /// 图像源,用于子工具使用,流程中工具不调用
        /// </summary>
        [NonSerialized]
        HImage imageRefIn;

        
        public static string ErrDataFlag = "ERR";
        public static string OkDataFlag = "OK";
        public static string NgDataFlag = "NG";
        /// <summary>
        /// 来自图像源工具的图像
        /// </summary>
        public HImage ImageRefIn
        {
            get
            {
                if (IsSubTool)
                {
                    return imageRefIn;
                }
                if (ImageSoureTool != null)
                {
                    return ImageSoureTool.GetImageResultRefOut();
                }
                else
                {
                    throw new Exception("图像源序号异常");
                }
            }
            set
            {
                if (IsSubTool)
                {
                    imageRefIn = value;
                }
            }
        }

        public bool IsSubTool { get; set; }

        public bool IsDisposed { get; set; } = false;
        public HRegion SearchRegion
        {
            get
            {
                if (searchRegion==null)
                {
                    searchRegion = new HRegion();
                }
                return searchRegion;
            }
            set
            {
                searchRegion = value;
            }
        }
        public HImage ImageTestOut
        {
            get
            {
                return imageTestOut;
            }

            set
            {
                imageTestOut = value;
            }
        }
        public virtual string Name
        {
            get
            {
                return name;
            }
        }
        public virtual string Note
        {
            set
            {
                note = value;
            }
            get
            {
                if (note == null)
                {
                    note = GetToolType();
                }
                return note;
            }
        }
        /// <summary>
        /// 结果
        /// </summary>
        public virtual string Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
        [Browsable(false)]
        /// <summary>
        /// 是否使能
        /// </summary>
        public virtual bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
        [Browsable(false)]
        [Description("目标结果")]
        public virtual string Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }
        [Description("最小值")]
        public virtual double Min
        {
            get
            {
                return min;
            }

            set
            {
                min = value;
            }
        }
        //[Browsable(false)]
        public virtual double Max
        {
            get
            {
                return max;
            }

            set
            {
                max = value;
            }
        }

        public virtual bool IsOk
        {
            get
            {
                return isOk;
            }
            protected set
            {
                isOk = value;
            }
        }

        public virtual bool IsRealOk()
        {
            return isRealOk;
        }
        public abstract string GetToolType();

        public void SetToolName(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// 获取工具的结果模板图像,用于后续工具的调用,使用标记为工具为ICreateImage接口工具
        /// </summary>
        /// <returns></returns>
        public virtual HImage GetImageResultRefOut()
        {
            return null;
        }
        public abstract ToolsSettingUnit GetSettingUnit();
        public virtual void Run(HImage image)
        {
            RunStart();
            ImageTestIn = image;
            if ((ImageTestIn == null || ImageTestIn.IsInitialized() == false) && (this is CreateImage.CreateImageTool == false))
            {
                return;
            }
            RunAct();
            RunEnd();
        }
        public virtual void Run()
        {
            RunStart();
            LinkTestImage();
            if ((ImageTestIn == null || ImageTestIn.IsInitialized() == false)&&(this is CreateImage.CreateImageTool==false))
            {
                return;
            }
            RunAct();
            RunEnd();
        }

        public virtual void RunRef()
        {
            RunStart();
            ImageTestIn = ImageRefIn;
            if ((ImageTestIn == null || ImageTestIn.IsInitialized() == false) && (this is CreateImage.CreateImageTool == false))
            {
                return;
            }
            RunAct();
            RunEnd();
        }
        /// <summary>
        /// 运行准备 清除结果 时间统计
        /// </summary>
        protected virtual void RunStart()
        {
            HOperatorSet.CountSeconds(out runStartTime);
            ClearResult();
        }
        /// <summary>
        /// 图像处理实际类,需要添加异常捕捉防止程序崩溃
        /// </summary>
        protected virtual void RunAct()
        {

        }
        /// <summary>
        /// 运行完成 时间统计
        /// </summary>
        protected virtual void RunEnd()
        {
            HOperatorSet.CountSeconds(out runEndTime);
            double tt = (runEndTime.D - runStartTime.D) * 1000.0;
            RuningFinish = true;
            executionTime = tt;
        }
        public virtual void SetValue(string key,object value)
        {

        }
        public virtual object getValue(string key)
        {
            return null;
        }
        public abstract void ShowResult(HWndCtrl viewCtrl);
        public virtual void SerializeCheck()
        {
            if (searchRegion != null && searchRegion.IsInitialized() == false)
            {
                searchRegion = null;
            }
        }

        public virtual void MarkTool(bool flag)
        {
            
            this.IsOk = flag;
            this.isRealOk = flag;
        }
        /// <summary>
        /// 依据文件名添加测试图像
        /// </summary>
        public bool AddTestImages(string fileKey)
        {
            if (TestImageDic == null)
            {
                TestImageDic = new Dictionary<string, HImage>();
            }
            if (TestImageDic.ContainsKey(fileKey))
                return false;

            try
            {
                HImage image = new HImage(fileKey);
                TestImageDic.Add(fileKey, image);
            }
            catch (HOperatorException ex)
            {
                Util.WriteLog(this.GetType(), ex);
                return false;
            }
            return true;
        }
        [Browsable(false)]
        public double ExecutionTime
        {
            get
            {
                return executionTime;
            }
        }
        /// <summary>
        /// 依据文件名获取测试图像字典中的图像
        /// </summary>
        public HImage GetImageTest(string fileName)
        {
            if ((ImageTestIn == null) && (TestImageDic.ContainsKey(fileName)))
                ImageTestIn = TestImageDic[fileName];

            return imageTestIn;
        }
        /// <summary>
        /// 链接测试图像
        /// </summary>
        public virtual void LinkTestImage()
        {
            //图像采集工具不需要链接测试图像源工具
            if (this is CreateImage.CreateImageTool)
            {
                return;
            }
            if (ImageSoureTool != null)
            {
                ImageTestIn = ImageSoureTool.ImageTestOut;
            }
            else
            {
                throw new Exception("图像源序号异常");
            }
        }
        [Browsable(false)]
        public HImage ImageTestIn
        {
            get
            {
                return imageTestIn;
            }
            set
            {
                this.imageTestIn = value;
            }
        }

        /// <summary>
        /// 在图像字典中依据文件名移除测试图片文件
        /// </summary>
        public void RemoveTestImage(string fileName)
        {
            if (TestImageDic.ContainsKey(fileName))
                TestImageDic.Remove(fileName);

            if (TestImageDic.Count == 0)
                ImageTestIn = null;
        }
        /// <summary>
        /// 测试图像字典数据清除
        /// </summary>
        public void RemoveTestImage()
        {
            TestImageDic.Clear();
            ImageTestIn = null;
        }
        public void SetTestImage(string fileKey)
        {
            ImageTestIn = TestImageDic[fileKey];
        }

        public void GenSearchRegion()
        {
            if (SearchRegion == null || SearchRegion.IsInitialized() == false)
            {
                if (ROIList != null)
                {
                    ROI roiSearcch = ROIList.Find(x => x.OperatorFlag == ROIOperation.None);
                    if (roiSearcch != null)
                    {
                        SearchRegion = roiSearcch.GetRegion();
                    }
                }
            }
        }
        /// <summary>
        /// 清除训练过程中产生的资源,例 文字检查训练模型中添加的图像
        /// </summary>
        public virtual void ClearTrainData()
        {

        }
        /// <summary>
        /// 清除运行结果标志
        /// </summary>
        public virtual void ClearResult()
        {
            isOk = false;
            isRealOk = false;
            RuningFinish = false;
            result = "";
            Message = "";
        }
        /// <summary>
        /// 清除测试图像及窗体资源
        /// </summary>
        public virtual void ClearTestData()
        {
            if (TestImageDic != null)
            {
                foreach (var item in TestImageDic)
                {
                    item.Value.Dispose();
                }
            }

            TestImageDic = null;
            if (imageTestIn != null)
            {
                imageTestIn = null;
            }
            if (settingUnit != null)
            {
                settingUnit.Dispose();
            }
            if (mat2dManger != null)
            {
                mat2dManger.SelectMatchingToolObserver = null;
            }
            settingUnit = null;
            result = "";
            isOk = false;
        }
        /// <summary>
        /// 删除工具(子类中实现),并清除测试图像资源
        /// </summary>
        public virtual void Close()
        {
            ClearTestData();
        }
        /// <summary>
        /// 工具需要加载的数据
        /// </summary>
        public virtual void LoadTool()
        {
            LinkTestImage();
        }
        public abstract string GetSendResult();
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //执行基本的清理代码
                IsDisposed = true;
            }
        }

        ~ToolBase()
        {
            this.Dispose(false);
        }

        public static void DisposeHobject(HObject obj)
        {
            if (obj != null && obj.IsInitialized())
            {
                obj.Dispose();
            }

        }
        [Browsable(false)]
        public int SettingIndex
        {
            get
            {
                return settingIndex;
            }
        }
        [Browsable(false)]
        public Mat2DManger Mat2DManger
        {
            get
            {
                if (mat2dManger == null)
                {
                    mat2dManger = new Mat2DManger(this);
                }
                return mat2dManger;
            }
        }
        [Browsable(false)]
        public bool RuningFinish
        {
            get
            {
                return runingFinish;
            }

            set
            {
                runingFinish = value;
            }
        }
        [Browsable(false)]
        public string Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
            }
        }

        public bool IsOutputResults
        {
            get
            {
                return isOutputResults;
            }

            set
            {
                isOutputResults = value;
            }
        }

        public string ImageSoureToolName
        {
            get
            {
                if (imageSoureToolName==null|| imageSoureToolName==string.Empty)
                {
                    imageSoureToolName = ToolsFactory.GetToolList(settingIndex)[0].Name;
                }
                return imageSoureToolName;
            }

            set
            {
                imageSoureToolName = value;
                imageSoureTool = null;
                LinkTestImage();
            }
        }

        public bool IsCalibOut
        {
            get
            {
                return isCalibOut;
            }

            set
            {
                isCalibOut = value;
            }
        }

        public ICalibration CalibTool
        {
            get
            {
                if (calibTool==null|| ((ToolBase)calibTool).IsDisposed)
                {
                    calibTool = ToolsFactory.GetToolList(settingIndex).Find(x => x is ICalibration) as ICalibration;
                }
                return calibTool;
            }
        }

        public ToolBase ImageSoureTool
        {
            get
            {
                if (imageSoureTool == null || imageSoureTool.IsDisposed)
                {
                    List<ToolBase> toolsList = ToolsFactory.GetToolList(settingIndex);
                    imageSoureTool = toolsList.Find(x => x.name == this.ImageSoureToolName);
                }
                return imageSoureTool;
            }

            set
            {
                imageSoureTool = value;
            }
        }
    }


}
