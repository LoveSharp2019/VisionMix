using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yoga.Camera;
using Yoga.Common.Basic;
using Yoga.Tools.Factory;

namespace Yoga.VisionMix.Scheduling
{
    public class UpdateUI
    {
        Units.AutoUnit ui;
        RunStatus runStatusDat;

        int runCount;
        private int inShow = 0;
        public UpdateUI(Units.AutoUnit autoUnit)
        {
            this.ui = autoUnit;
        }


        public void ShowResultData(RunStatus runStatus)
        {
            DataRow dr = ui.DtResultShow.Rows[runStatus.CameraIndex - 1];
            DateTime dt = DateTime.Now;
            string timeNow = dt.ToString("HH:mm:ss:fff_yyyy_MM_dd");
            int ok = runStatus.OKCount;
            int ng = runStatus.NgCount;

            dr["时间"] = timeNow;

            dr["正常计数"] = ok.ToString();
            dr["失败计数"] = ng.ToString();

            dr["节拍时间"] = runStatus.CylceTime.ToString("f2");
            if (ok + ng > 10)
            {
                dr["异常率"] = (ng / (double)(ok + ng) * 100.0).ToString("F2") + "%";
            }
            else
            {
                dr["异常率"] = "0%";
            }
        }

        //int x = 0;
        public async void DoWork()
        {
            while (Interlocked.Exchange(ref runCount, 0) > 0)
            {
                //if (RunCount != 1)
                //{
                //    x++;
                //    Common.Util.Notify(Level.Err, string.Format("{1}运行计数{0}", RunCount,x));
                //}
                //ResteCount();
                //HTuple start, end;
                //HOperatorSet.CountSeconds(out start);
                try
                {
                    ShowResultData(runStatusDat);
                    ui.toolShowUnit1.ShowStatus(runStatusDat);
                    int cameraIndex = runStatusDat.CameraIndex;

                    string showMessage = string.Empty;
                    if (CameraManger.CameraDic.ContainsKey(cameraIndex))
                    {
                        string cameraInfo = CameraManger.CameraDic[cameraIndex].GetCameraAcqInfo();
                        showMessage = string.Format(
                                   "相机{0} {1} ", cameraIndex, cameraInfo);

                    }
                    showMessage += string.Format("处理{0}", runStatusDat.FpsStr);
                    ui.CameraShowUnitDic[cameraIndex].HWndUnit.CameraMessage = showMessage;
                }
                catch (Exception)
                {

                }
                //HOperatorSet.CountSeconds(out end);
                //double time = (end - start) * 1000;
                //Common.Util.Notify($"界面ui刷新用时{time:f2}ms");
                await Task.Delay(100);
            }
            Interlocked.Exchange(ref inShow, 0);
            if (Interlocked.Exchange(ref runCount, 0) > 0)
            {
                TriggerShow();
            }
        }
        private void TriggerShow()
        {
            if (Interlocked.CompareExchange(ref inShow, 1, 0) == 0)
            {
                //启动刷新
                if (ui.InvokeRequired)
                {
                    ui.BeginInvoke(new Action(DoWork));
                }
                else
                {
                    DoWork();
                }

            }
        }
        public void Update(RunStatus runStatus)
        {

            runStatusDat = runStatus;
            //AddCount();

            Interlocked.Increment(ref runCount);
            TriggerShow();

        }
    }
}
