using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoga.ImageControl;
using Yoga.Tools.Matching;
using Yoga.Tools.Metrology;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using Yoga.Tools.CreateImage;

namespace Yoga.Tools.Calib9Point
{
    [Serializable]
    public class CalibPos
    {
        private double px;
        private double py;
        private double rx;
        private double ry;

        public double Px
        {
            get
            {
                return px;
            }

            set
            {
                px = value;
            }
        }

        public double Py
        {
            get
            {
                return py;
            }

            set
            {
                py = value;
            }
        }

        public double Rx
        {
            get
            {
                return rx;
            }

            set
            {
                rx = value;
            }
        }

        public double Ry
        {
            get
            {
                return ry;
            }

            set
            {
                ry = value;
            }
        }
    }
    [Serializable]
    /// <summary>
    /// 九点标定工具
    /// </summary>
    public class Calib9PointTool : ToolBase, ICalibration, IToolEnable
    {
        #region 字段
        private static string toolType = "九点标定";
        private MatchingTool matchingTool;
        private MetrologyTool metrologyTool;
        [NonSerialized]
        private CreateImageTool createImageTool;
        /// <summary>
        /// 训练用图像
        /// </summary>
        public HImage TrainImage;
        double minScore = 0.6;

        HHomMat2D homMat2D;

        double errRowMax;
        double errColMax;

        BindingList<CalibPos> points;
        [NonSerialized]
        private HImage testImage;
        #endregion
        #region 属性
        public MatchingTool MatchingTool
        {
            get
            {
                if (matchingTool == null)
                {
                    matchingTool = new MatchingTool(base.settingIndex);
                    matchingTool.SetToolName(this.Name + "_" + matchingTool.GetToolType());
                    matchingTool.IsSubTool = true;
                }
                return matchingTool;
            }
        }
        public MetrologyTool MetrologyTool
        {
            get
            {
                if (metrologyTool == null)
                {
                    metrologyTool = new MetrologyTool(base.settingIndex);
                    metrologyTool.SetToolName(this.Name + "_" + metrologyTool.GetToolType());
                }
                return metrologyTool;
            }

        }
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
                return toolType + "\r\n版本:20180428\r\n说明:像素转化为坐标工具,此工具不参与实际的运行,若删除此工具请保存数据后重新打开";
            }
        }
        public double MinScore
        {
            get
            {
                return minScore;
            }

            set
            {
                minScore = value;
            }
        }

        public double ErrRowMax
        {
            get
            {
                return errRowMax;
            }

        }

        public double ErrColMax
        {
            get
            {
                return errColMax;
            }
        }

        public BindingList<CalibPos> Points
        {
            get
            {
                if (points == null)
                {
                    points = new BindingList<CalibPos>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            CalibPos pos = new CalibPos();
                            points.Add(pos);
                        }

                    }
                }
                return points;
            }

            set
            {
                points = value;
            }
        }

        public CreateImageTool CreateImageTool
        {
            get
            {
                if (createImageTool == null)
                {
                    createImageTool = ToolsFactory.GetToolList(settingIndex)[0] as CreateImageTool;
                }
                return createImageTool;
            }
        }

        public HImage TestImage
        {
            get
            {
                return testImage;
            }

            set
            {
                testImage = value;
            }
        }
        #endregion

        public Calib9PointTool(int settingIndex)
        {
            base.settingIndex = settingIndex;
            this.IsOk = true;
            this.IsSubTool = true;
        }
        public override ToolsSettingUnit GetSettingUnit()
        {
            if (settingUnit == null)
            {
                settingUnit = new Calib9PointToolUnit(this);
            }
            return settingUnit;
        }
        public override string GetToolType()
        {
            return toolType;
        }

        public override void SerializeCheck()
        {
            if (matchingTool != null)
            {
                matchingTool.SerializeCheck();
            }
            if (metrologyTool != null)
            {
                metrologyTool.SerializeCheck();
            }
            if (TrainImage != null && TrainImage.IsInitialized() == false)
            {
                TrainImage = null;
            }
        }

        public void GetImage()
        {
            CreateImageTool.Run();
            TestImage = CreateImageTool.ImageTestOut;
        }

        public string DoMatching()
        {
            if (TestImage == null || TestImage.IsInitialized() == false)
            {
                return "当前无图像数据";
            }
            MatchingTool.Run(TestImage);
            MatchingResult result = MatchingTool.MatchingResults;
            if (result.Count > 0)
            {
                return string.Format("定位结果:行{0:f2}列{1:f2}", result.Row.D, result.Col.D);
            }
            return "定位失败";
        }

        protected override void RunStart()
        {
        }

        protected override void RunEnd()
        {
        }
        public void DoCalibration()
        {

            homMat2D = new HHomMat2D();

            HTuple pxAll=new HTuple(), pyAll = new HTuple(), rxAll = new HTuple(), ryAll = new HTuple();
            for (int i = 0; i < Points.Count; i++)
            {
                pxAll[i] = Points[i].Px;
                pyAll[i] = Points[i].Py;
                rxAll[i] = Points[i].Rx;
                ryAll[i] = Points[i].Ry;
            }
            //九点的矩阵映射
            homMat2D.VectorToHomMat2d(pxAll, pyAll, rxAll, ryAll);

            HTuple checkRow, checkCol;
            //将九点直接验证结果
            checkCol = homMat2D.AffineTransPoint2d(pxAll, pyAll, out checkRow);
            //获取到行及列方向的误差
            HTuple errRow = (ryAll - checkRow).TupleAbs();
            HTuple errCol = (rxAll - checkCol).TupleAbs();
            errRowMax = errRow.TupleMax();
            errColMax = errCol.TupleMax();
        }

        public override void ShowResult(HWndCtrl viewCtrl)
        {
            if (RuningFinish == false)
            {
                return;
            }
            ;
        }


        public void ImagePointsToWorldPlane(HTuple px, HTuple py, out HTuple rx, out HTuple ry)
        {
            rx = null;
            ry = null;
            if (homMat2D == null)
            {
                throw new Exception("标定数据不存在");
            }
            rx = homMat2D.AffineTransPoint2d(px, py, out ry);
        }

        public override string GetSendResult()
        {
            return string.Empty;
        }
    }
}
