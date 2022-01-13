using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Yoga.Camera;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.ImageControl;
using Yoga.Tools;
using Yoga.Tools.CreateImage;
using Yoga.Tools.Factory;
using Yoga.VisionMix.AppManger;

namespace Yoga.VisionMix.Scheduling
{
    public class RunTheadData
    {
        Units.AutoUnit autoUnit;

        ImageEventArgs imageData;

        Thread CameraRunThread;
        bool isRun = true;
        bool isTestMode = false;
        bool isExtTrigger = false;
        double startExTime = 0;

        protected Fps fps = new Fps();
        private Stopwatch stopWatch = new Stopwatch();

        /// <summary>
        /// 控制线程运行标志
        /// </summary>
        AutoResetEvent threadRunSignal = new AutoResetEvent(false);

        int settingIndex = 1;
        int cameraIndex = 1;
        AutoResetEvent imageRunFinishSignal = new AutoResetEvent(true);

        AutoResetEvent imageRunFinishSignalFlow = new AutoResetEvent(true);
        /// <summary>
        /// 指示运行结果是否存在ng
        /// </summary>
        private bool runningResultFlag = false;

        Units.CameraShow cameraShowUnit;

        int ok = 0, ng = 0;

        ToolBase toolErr;
        private object lockObj = new object();
        const string toolDelimiter = "_";
        public RunTheadData(Units.AutoUnit autoUnit, int cameraIndex)
        {
            this.autoUnit = autoUnit;
            this.cameraIndex = cameraIndex;
            cameraShowUnit = autoUnit.CameraShowUnitDic[cameraIndex];
            CameraRunThread = new Thread(new ThreadStart(Run));
            CameraRunThread.IsBackground = true;
            CameraRunThread.Start();

            stopWatch.Reset();
        }
        public void ResetCount()
        {
            ok = 0;
            ng = 0;

            fps.Reset();
        }
        /// <summary>
        /// 当前图像处理完成标志信号
        /// </summary>
        public AutoResetEvent ImageRunFinishSignal
        {
            get
            {
                return imageRunFinishSignal;
            }

            protected set
            {
                imageRunFinishSignal = value;
            }
        }

        public ImageEventArgs ImageData
        {
            get
            {
                lock (lockObj)
                {
                    return imageData;
                }

            }

            set
            {
                lock (lockObj)
                {
                    imageData = value;
                }
            }
        }

        public void TrigerRun(ImageEventArgs imageData, int waitTime, bool isShowTimeOut)
        {
           //判断是否在时间间隔内 200ms
            bool isDelay = (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 200);
            if (isDelay)
            {
                stopWatch.Restart();
            }
            //如果不用显示超时且不再时间间隔内就不触发等待,间隔节拍
            if (isShowTimeOut == false&& isDelay==false)
            {
                return;
            }
            if (imageRunFinishSignalFlow.WaitOne(waitTime))
            {
                //autoUnit.Invoke(new Action(() =>
                //{
                settingIndex = imageData.CameraIndex;
                //HTuple tt;
                //HOperatorSet.CountSeconds(out tt);
                startExTime = imageData.StartTime.D;
                //startExTime = tt.D;

                //    double timeWait = (tt.D-imageData.StartTime.D ) * 1000.0;
                //    Util.Notify($"相机{cameraIndex}图像等待运行0用时{timeWait:f2}ms" );

                //清理图像内存
                if (this.ImageData != null)
                {
                    this.ImageData.Dispose();
                }
                this.ImageData = imageData;
                this.isTestMode = false;
                isExtTrigger = true;

                //HTuple start;
                //HOperatorSet.CountSeconds(out start);
                //double timeWait = (start - startExTime) * 1000.0;
                //Util.Notify(string.Format("相机{0}图像等待运行0用时{1:f2}ms", cameraIndex, timeWait));
                //RunAllTool();
                threadRunSignal.Set();

                //}));

            }
            else
            {
                imageData.Dispose();
                if (isShowTimeOut)
                {
                    string message = string.Format("工具组{0}图像处理超时发生,请检查程序运行节拍", settingIndex);
                    Util.Notify(Level.Err, message);
                }
                else
                {
                    //Thread.Sleep(50);
                }
                //if (autoUnit.InvokeRequired)
                //{

                //}
            }
        }
        public void TrigerRun(int settingKey, bool isTestMode)
        {

            if (imageRunFinishSignalFlow.WaitOne(1000))
            {
                settingIndex = settingKey;
                this.isTestMode = isTestMode;
                isExtTrigger = false;

                threadRunSignal.Set();
            }
            else
            {
                string message = string.Format("工具组{0}图像处理超时发生,请检查程序运行节拍", settingIndex);
                Util.Notify(Level.Err, message);
            }
        }
        public void Stop()
        {
            isRun = false;
            stopWatch.Reset();
            threadRunSignal.Set();
        }
        public void Run()
        {
            while (isRun)
            {
                threadRunSignal.WaitOne();


                if (isRun)
                {
                    RunAllTool();

                }
            }

        }

        public void ShowResult(HWndCtrl hWndCtrl, List<ToolBase> runToolList, bool runningResultFlag)
        {
            if (runningResultFlag == false)
            {
                hWndCtrl.ShowNg();

                if (IniStatus.Instance.SaveNgImage == 1)
                {

                    DateTime dt = DateTime.Now;
                    string timeNow = dt.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                    string dayNow = dt.ToString("yyyy_MM_dd");
                    string project = Path.GetFileNameWithoutExtension
                        (UserSetting.Instance.ProjectPath);

                    string NGImagePath = "D:\\data\\" + project +
                             "\\NgImage\\" + "\\工具组" + settingIndex + "\\";
                    SaveImage(NGImagePath + timeNow + ".png", runToolList[0].ImageTestOut);
                }

            }
            else
            {
                hWndCtrl.ShowOK();
            }
            HTuple showStart1;
            HOperatorSet.CountSeconds(out showStart1);
            hWndCtrl.ChangeGraphicSettings(Mode.LINESTYLE, new HTuple());
            hWndCtrl.ClearList();
            //hWndCtrl.ChangeGraphicSettings(Mode.DRAWMODE, "margin");
            hWndCtrl.AddIconicVar(runToolList[0].ImageTestOut);
            foreach (ToolBase item in runToolList)
            {
                try
                {
                    item.ShowResult(hWndCtrl);
                    if (item.Message != null && item.Message.Length > 0)
                    {
                        Util.Notify(string.Format("工具{0}_{1}", item.Name, item.Message));
                    }
                }
                catch (Exception ex)
                {
                    Util.Notify(string.Format("工具{0}显示异常{1}", item.Name, ex.Message));
                    Util.WriteLog(this.GetType(), ex);
                }
            }


            //HTuple showEnd1;
            //HOperatorSet.CountSeconds(out showEnd1);
            //double timeShow1 = (showEnd1 - showStart1) * 1000.0;
            //Util.Notify(string.Format("工具组{0}准备显示图像用时{1:f2}ms", settingIndex, timeShow1));

            hWndCtrl.Repaint();

            //HTuple showEnd2;
            //HOperatorSet.CountSeconds(out showEnd2);

            //double timeShow2 = (showEnd2 - showStart1) * 1000.0;
            //Util.Notify(string.Format("工具组{0}刷新图像用时{1:f2}ms", settingIndex, timeShow2));
        }
        int fpsCount = 0;
        public void RunAllTool()
        {
            HTuple start = null, end = null;
            //Util.Notify("运行线程id:"+Thread.CurrentThread.ManagedThreadId.ToString());


            //帧率统计增加
            fps.IncreaseFrameNum();

            fpsCount++;
            if (fpsCount > 10 )
            {
                fps.UpdateFps();
                fpsCount = 0;
            }
            
            HOperatorSet.CountSeconds(out start);

            if (isExtTrigger)
            {
                double timeWait = (start - startExTime) * 1000.0;
                Util.Notify(string.Format("相机{0}图像等待运行用时{1:f2}ms", cameraIndex, timeWait));
                start = startExTime;
            }

            RunStatus runStatus = new RunStatus(settingIndex, cameraIndex);

            runStatus.FpsStr = string.Format("FPS:{0:F1}|帧:{1}|",
                                              fps.GetFps(), fps.GetTotalFrameCount()); ;
            List<ToolBase> runToolList = ToolsFactory.GetToolList(settingIndex);

            Tools.CreateImage.CreateImageTool createImageTool = runToolList[0] as Tools.CreateImage.CreateImageTool;
            HWndCtrl hWndCtrl = cameraShowUnit.HWndUnit.HWndCtrl;
            try
            {
                HTuple toolStart = new HTuple(), toolEnd = new HTuple();
                #region 1 运行初始化
                StatusManger statusManger = StatusManger.Instance;
                //statusManger.RuningStatus = RuningStatus.图像检测中;
                runningResultFlag = false;
                toolErr = null;


                #endregion
                #region 2 运行所有工具
                HOperatorSet.CountSeconds(out toolStart);
                //外部触发处理
                if (isExtTrigger)
                {
                    createImageTool.SettExtTriggerData(ImageData);
                }
                else
                {
                    createImageTool.SetExtTriggerDataOff();
                }
                StringBuilder MyStringBuilder = new StringBuilder();

                string yy = MyStringBuilder.ToString();

                string datSend = "";

                foreach (var item in runToolList)
                {
                    if (item is IToolRun)
                    {
                        try
                        {
                            item.Run();

                            string result = item.IsOk ? "OK" : "NG";
                            Util.Notify(string.Format("{0}_{1} T={2:f2}ms,结果:{3}", item.Name, result, item.ExecutionTime, item.Result));
                            MyStringBuilder.Append(string.Format("{0}_{1}_T={2:f2}ms\r\n", item.Name, result, item.ExecutionTime));

                            if (item.IsOutputResults)
                            {
                                string dat = item.GetSendResult();
                                if (dat != string.Empty)
                                {
                                    datSend += dat;
                                    datSend += toolDelimiter;
                                }

                            }
                            runStatus.RunStatusList.Add(item.IsOk);
                        }
                        catch (Exception ex)
                        {
                            Util.WriteLog(this.GetType(), ex);
                            Util.Notify(Level.Err, string.Format("工具{0}运行出现异常{1}", item.Name, ex.Message));
                            runStatus.RunStatusList.Add(false);
                        }
                    }
                    else
                    {
                        runStatus.RunStatusList.Add(true);
                    }
                }
                runStatus.ResultMessage = MyStringBuilder.ToString();
                #endregion
                //时间统计
                HOperatorSet.CountSeconds(out toolEnd);
                double toolTime = (toolEnd - toolStart) * 1000.0;
                Util.Notify(string.Format("工具组{0}图像处理完成用时{1:f2}ms", settingIndex, toolTime));
                #region 3 查找是否存在运行错误的工具
                toolErr = runToolList.Find(
                            x => x.IsOk == false && x is IToolRun);

                if (toolErr == null && ToolsFactory.ToolsDic.Count > 0)
                {
                    runningResultFlag = true;
                    ok++;
                }
                else
                {
                    runningResultFlag = false;
                    ng++;


                    Util.Notify(string.Format("工具{0}图像处理检测到异常", toolErr.Name));
                }

                if (runningResultFlag == true)
                {
                    datSend = Util.TrimEndString(datSend, toolDelimiter);
                    datSend = Util.TrimStartString(datSend, toolDelimiter);

                }
                else
                {
                    datSend = "NG";
                }

                if (isTestMode == false)
                {

                    //SerialHelper.Instance.WriteCommdToSerial(datSend);
                    //非相机输出模式下就直接输出文本信息
                    if (AppInterlockHelper.Instance.CommunicationParam.InterlockMode != Common.Basic.InterlockMode.相机IO)
                    {
                        Util.Notify(string.Format("发送结果:{0}", datSend));
                        AppInterlockHelper.Instance.WriteData(datSend);
                    }
                    else
                    {
                        if (StatusManger.Instance.IsInterlocking && CameraManger.CameraDic.ContainsKey(settingIndex) && runningResultFlag == false)
                        {
                            CameraManger.CameraDic[settingIndex].Output();
                        }
                        ////发现异常就输出ng信号
                        //if (runningResultFlag == false)
                        //{
                        //    CameraManger.CameraDic[cameraIndex].Output();
                        //}
                    }
                }
                else
                {
                    if (AppInterlockHelper.Instance.CommunicationParam.InterlockMode != InterlockMode.相机IO)
                    {
                        Util.Notify(string.Format("测试结果:{0}", datSend));
                    }
                    else
                    {
                        if (runningResultFlag == false)
                        {
                            Util.Notify(string.Format("测试结果:{0}", "NG"));
                        }
                        else
                        {
                            Util.Notify(string.Format("测试结果:{0}", "OK"));
                        }
                    }
                }
                #endregion
                #region 4 显示所有的图形
                //HTuple showStart;
                //HOperatorSet.CountSeconds(out showStart);
                autoUnit.Invoke(new Action<HWndCtrl, List<ToolBase>, bool>((h, l, f) =>
              {

                  ShowResult(h, l, f);

              }), hWndCtrl, runToolList, runningResultFlag);

                //hWndCtrl.Repaint();

                //HTuple showEnd;
                //HOperatorSet.CountSeconds(out showEnd);

                //double timeShow2 = (showEnd - showEnd1) * 1000.0;
                //Util.Notify(string.Format("工具组{0}刷新图像用时{1:f2}ms", settingIndex, timeShow2));

                //double timeShow = (showEnd - showStart) * 1000.0;
                //Util.Notify(string.Format("工具组{0}显示图像用时{1:f2}ms", settingIndex, timeShow));
                #endregion

                HTuple end1;
                HOperatorSet.CountSeconds(out end1);
                double time1 = (end1 - toolEnd) * 1000.0;
                Util.Notify(string.Format("工具组{0}分析显示用时{1:f2}ms", settingIndex, time1));
                //string path = string.Format("D:\\data\\{0}\\运行数据\\{0}_相机{1}_{2}.csv", project, imageData.CameraIndex, dayNow);
                //int index = imageData.CameraIndex - 1;
                ////保存数据用时15ms左右 取消
                //SaveResult(index, path);  
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify(string.Format("图像处理异常"));
            }
            finally
            {
                HOperatorSet.CountSeconds(out end);
                double runTime = 0;
                runTime = (end - start) * 1000.0;

                runStatus.OKCount = ok;
                runStatus.NgCount = ng;
                runStatus.CylceTime = runTime;
                RunStatus runStatusTmp = runStatus;
                autoUnit.RefreshUI(runStatusTmp);


                //指示可以来图像处理
                Util.Notify(string.Format("---工具组{0}运行完成,用时{1:f2}ms", settingIndex, runTime));

                imageRunFinishSignalFlow.Set();
                //离线模式
                if (isTestMode&&
                    createImageTool.OffLineMode == true&&
                    createImageTool.AllReadFinish==false)
                {
                    Task.Delay(500);
                    TrigerRun(this.settingIndex, true);
                }
            }
        }


        private void SaveImage(string files, HImage ngImage)
        {

            if (ngImage == null || ngImage.IsInitialized() == false)
            {
                Util.WriteLog(this.GetType(), "异常图像数据丢失");
                Util.Notify("异常图像数据丢失");
                return;
            }
            HImage imgSave = ngImage.CopyImage();
            Task.Run(() =>
            {
                try
                {
                    FileInfo fi = new FileInfo(files);
                    if (!fi.Directory.Exists)
                    {
                        fi.Directory.Create();
                    }

                    Common.FileAct.FileManger.DeleteOverflowFile(Path.GetDirectoryName(files), IniStatus.Instance.NgImageCount);
                    imgSave.WriteImage("png", 0, files);
                    imgSave.Dispose();
                }
                catch (Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    Util.Notify(string.Format("相机{0}异常图像保存异常", settingIndex));
                }
            });

        }


    }
}
