using HalconDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Yoga.Camera;
using Yoga.Common;
using Yoga.ImageControl;

namespace Yoga.Tools.CreateImage
{
    [Serializable]
    public class CreateImageTool : ToolBase, IToolRun, ICreateImage
    {
        private static string toolType = "图像采集";

        private int cameraIndex = 1;


        private int offLineImageIndex = 0;

        private string offLineImagePath;



        private HTuple imageFiles;

        private HImage refImage;

        private bool offLineMode;

        [NonSerialized]
        private double startTime;

        private bool allReadFinish = false;
        [NonSerialized]
        private CameraBase camera;
        //[NonSerialized]
        bool isExtTrigger = false;


        public bool AllReadFinish
        {
            get
            {
                return allReadFinish;
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

        public int CameraIndex
        {
            get
            {
                return cameraIndex;
            }

            set
            {
                cameraIndex = value;
            }
        }

        public bool OffLineMode
        {
            get
            {
                return offLineMode;
            }

            set
            {
                offLineMode = value;
            }
        }

        public string OffLineImagePath
        {
            get
            {
                return offLineImagePath;
            }

            set
            {
                offLineImagePath = value;
                RefushImagePathList();

            }
        }
        public override void LoadTool()
        {
            base.LoadTool();
            try
            {
                RefushImagePathList();
            }
            catch (Exception) { }
        }
        public void RefushImagePathList()
        {
            offLineImageIndex = 0;
            if (offLineImagePath!=null)
            {
                HOperatorSet.ListFiles(offLineImagePath,
                (new HTuple("files")).TupleConcat("follow_links"), out imageFiles);
                HOperatorSet.TupleRegexpSelect(imageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                    "ignore_case"), out imageFiles);
            }
            
        }

        public CameraBase Camera
        {
            get
            {
                if (camera == null)
                {
                    if (CameraManger.CameraDic.ContainsKey(cameraIndex))
                    {
                        camera = CameraManger.CameraDic[cameraIndex];
                    }
                }

                return camera;
            }
        }

        public double StartTime
        {
            get
            {
                return startTime;
            }

            private set
            {
                startTime = value;
            }
        }


        public override HImage GetImageResultRefOut()
        {
            return refImage;
        }
        public HImage RefImage
        {
            get
            {
                return refImage;
            }

            set
            {
                refImage = value;
            }
        }

        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new CreateImageUnit(this);
            }

            return settingUnit;
        }

        public override string GetToolType()
        {
            return toolType;
        }
        public CreateImageTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
        }


        public void SettExtTriggerData(ImageEventArgs imageData)
        {
            isExtTrigger = true;
            //DisposeHobject(ImageTestOut);
            ImageTestOut = imageData.CameraImage;
            StartTime = imageData.StartTime.D;
        }
        public void SetExtTriggerDataOff()
        {
            isExtTrigger = false;
        }
        protected override void RunAct()
        {
            try
            {

                if (isExtTrigger == false)
                {
                    StartTime = runStartTime.D;
                    //由于这里是图像的源头,可以直接清除图像
                    DisposeHobject(ImageTestOut);
                    if (OffLineMode == true)
                    {
                        ImageTestOut = new HImage();

                        if (imageFiles == null || imageFiles.TupleLength() < 1)
                        {
                            Util.Notify(string.Format("工具{0}无离线数据", this.Name));
                            return;
                        }
                        //Image Acquisition 01: Code generated by Image Acquisition 01
                      
                        ImageTestOut.ReadImage(imageFiles.TupleSelect(offLineImageIndex));
                        Result = "图像采集完成";
                        offLineImageIndex++;

                        if (offLineImageIndex > (imageFiles.TupleLength() - 1))
                        {
                            offLineImageIndex = 0;
                            allReadFinish = true;
                        }
                        else
                        {
                            allReadFinish = false;
                        }
                    }
                    else
                    {
                        if (Camera != null)
                        {
                            ImageTestOut = Camera.GrabImage(1000);
                        }
                        else
                        {
                            Util.Notify(string.Format("工具{0}无相机连接", this.Name));
                        }
                    }
                }
                else
                {
                    //外触发,收到图像时间就是图像处理的开始时间
                    runStartTime = StartTime;
                }
                if (ImageTestOut != null && ImageTestOut.IsInitialized())
                {
                    IsOk = true;
                    isRealOk = true;
                    Result = "OK";
                }
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                string message = "";
                if (OffLineMode)
                {
                    message = "离线图像路径异常,请检查设置";
                }
                else
                {
                    message = "相机图像采集异常,请检查设置";
                }
                Util.Notify(string.Format("工具{0}运行异常:{1}", Name, message));
            }
        }

        public override void SerializeCheck()
        {
            if (refImage != null && refImage.IsInitialized() == false)
            {
                refImage = null;
            }
            base.SerializeCheck();
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
            }
        }

        public void ShowImage(HWndCtrl viewCtrl)
        {
            if (ImageTestOut != null && ImageTestOut.IsInitialized())
            {
                viewCtrl.AddIconicVar(ImageTestOut);
            }
        }
        public override void ShowResult(HWndCtrl viewCtrl)
        {
            //if (Enable == false)
            //{
            //    return;
            //}
            //viewCtrl.ClearList();
            //if (RuningFinish == false)
            //{
            //    return;
            //}
            //if (testImage != null && testImage.IsInitialized())
            //{
            //    viewCtrl.AddIconicVar(testImage);
            //}
        }
        public override string GetSendResult()
        {
            if (IsOutputResults == false)
            {
                return string.Empty;
            }
            return "";
        }
    }
}
