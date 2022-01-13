﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yoga.Common;

namespace Yoga.Camera
{
   public class GigeCamera : CameraBase
    {
        private Stopwatch stopWatch = new Stopwatch();
        HFramegrabber framegrabber;
        Thread runThread;
        AutoResetEvent threadRunSignal = new AutoResetEvent(false);
        public override double GainCur
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                ; ;
            }
        }

        public override long ShuterCur
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                ;
            }
        }
        public GigeCamera(HFramegrabber framegrabber, int index)
        {
            this.framegrabber = framegrabber;
            this.cameraIndex = index;
        }
        public override void Close()
        {
            try
            {
                IsLink = false;


                // Reset the stopwatch.
                stopWatch.Reset();
                framegrabber.Dispose();

            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("相机关闭异常");
            }
        }

        public override void ContinuousShot()
        {
            if (framegrabber == null || framegrabber.IsInitialized() == false)
            {
                return;
            }
            try
            {
                Command = Command.Video;

                IsContinuousShot = true;
                threadRunSignal.Set();
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("相机连续采集开始异常");
            }
        }
        public override HImage GrabImage(int delayMs)
        {
            if (framegrabber == null || framegrabber.IsInitialized() == false)
            {
                Util.Notify("图像采集设备打开异常");
                return null;
            }
            GetImage();
            return hPylonImage;
        }
        private void GetImage()
        {
            try
            {
                hPylonImage = null;
                HOperatorSet.CountSeconds(out startTime);
                hPylonImage = framegrabber.GrabImage();

            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify(string.Format("图像采集发生异常,相机{0}采集失败", cameraIndex));
            }
        }

        public override void ContinuousShotStop()
        {
            try
            {
                // Set an enum parameter.
                if (framegrabber == null)
                {
                    return;
                }

                IsContinuousShot = false;
                IsExtTrigger = false;
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("相机连续采集停止异常");
            }
        }
        /// <summary>
        /// 图像采集线程对应方法
        /// </summary>
        public void Run()
        {
            while (IsLink)
            {

                threadRunSignal.WaitOne();

                Util.Notify("开始连续采集图像");
                if (IsLink)
                {
                    while (IsContinuousShot)
                    {
                        GetImage();
                        if (hPylonImage != null && hPylonImage.IsInitialized())
                        {
                            TrigerImageEvent();
                        }
                    }

                }
            }
        }
        public override void OneShot(Command command)
        {
            try
            {
                if (IsContinuousShot || IsExtTrigger)
                {
                    ContinuousShotStop();
                }
                Command = command;
                // Execute the software trigger. Wait up to 1000 ms until the camera is ready for trigger.
                threadRunSignal.Set();
            }
            catch
            {
                IsLink = false;
                Util.Notify("相机软触发异常");
            }
        }

        public override bool Open()
        {
            try
            {


                //ContinuousShotStop();//设置为软触发模式


                // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed
                //stopWatch.Reset();

                GetCameraSettingData();
                //usb相机第一次采集图像缓慢,采集一张图像不使用来提速
                GetImage();

                IsLink = true;
                runThread = new Thread(new ThreadStart(Run));
                runThread.IsBackground = true;
                runThread.Start();
                //}
                //else
                //{
                //    Util.Notify("无相机连接");
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("相机打开出现异常");

                throw ex;
            }
            return true;
        }

        public override void Output()
        {
            throw new NotImplementedException();
        }

        public override void SetExtTrigger()
        {
            throw new NotImplementedException();
        }

        protected override void GetCameraSettingData()
        {
            try
            {
                //long max, min, cur;

                HTuple fGainRange = 0;
                fGainRange=framegrabber.GetFramegrabberParam("Gain_range");
                gainMin = fGainRange[0];
                gainMax = fGainRange[1];
                gainCur = fGainRange[3];
                gainUnit = "db";

                shuterUnit = "us";

                HTuple exposureRange = framegrabber.GetFramegrabberParam("ExposureTimeAbs_range");

                shuterMin =(long) exposureRange[0].D;
                shuterMax = (long)exposureRange[1].D;
                shuterCur = (long)exposureRange[3].D;


                HTuple fTriggerDelayRange= framegrabber.GetFramegrabberParam("TriggerDelayAbs_range");
                triggerDelayAbsMin = fTriggerDelayRange[0];
                triggerDelayAbsMax = fTriggerDelayRange[1];
                triggerDelayAbs = fTriggerDelayRange[3];

                lineDebouncerTimeAbsMin = 0;
                lineDebouncerTimeAbsMax = 5000;
                lineDebouncerTimeAbs = 0;

            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("相机设置信息获取异常");
            }
        }
    }
}
