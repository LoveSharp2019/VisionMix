using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Tools.Factory
{
    [Serializable]
   public class RunStatus
    {
        public int SettingIndex { get; private set; } = 1;
        public int CameraIndex { get; private set; } = 1;
        public int OKCount { get;  set; } = 0;
        public int NgCount { get;  set; } = 0;
        public double CylceTime { get; set; } = 0;
        
        public string FpsStr { get; set; } = string.Empty;
        public List<bool> RunStatusList = new List<bool>();

        public string ResultMessage { get; set; } = "";
        public RunStatus(int settingIndex,int cameraIndex)
        {
            SettingIndex = settingIndex;
            CameraIndex = cameraIndex;
        }
        public RunStatus DeepClone()
        {
            if (this==null)
            {
                return null;
            }
            return Common.FileAct.SerializationFile.Clone(this) as RunStatus;
        }
    }
}
