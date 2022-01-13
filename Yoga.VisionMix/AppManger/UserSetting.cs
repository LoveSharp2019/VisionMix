using System;
using System.Collections.Generic;
using Yoga.Common;
using Yoga.Common.Basic;
using Yoga.Common.FileAct;

namespace Yoga.VisionMix
{
    [Serializable]
    /// <summary>
    /// 用户自定义数据类
    /// </summary>
    class UserSetting
    {
        public static UserSetting Instance = new UserSetting();

        private static readonly string userDataPath = Environment.CurrentDirectory + "\\user.dat";
        private string projectPath = Environment.CurrentDirectory + "\\project\\产品1.prj";
        private readonly string projectPathInit = Environment.CurrentDirectory + "\\project\\产品1.prj";

        private string softKey;
       // private Dictionary<int, CameraPram> cameraPramDic;

        private CommunicationParam ioRs232Param;
        public CommunicationParam IoRs232Param
        {
            get
            {
                if (ioRs232Param == null)
                {
                    ioRs232Param = new CommunicationParam();
                }
                return ioRs232Param;
            }

            set
            {
                ioRs232Param = value;
            }
        }

      
        public string ProjectPath
        {
            get
            {
                if (projectPath == null)
                {
                    projectPath = projectPathInit;
                }
                return projectPath;
            }

            set
            {
                projectPath = value;
            }
        }

        public static string UserDataPath
        {
            get
            {
                return userDataPath;
            }
        }

        public string SoftKey
        {
            get
            {
                return softKey;
            }

            set
            {
                softKey = value;
            }
        }

        

        public void ReadSetting()
        {

            UserSetting setting = SerializationFile.DeserializeObject(
                UserDataPath) as UserSetting;
            if (setting == null)
            {
                setting = new UserSetting();
            }
            Instance = setting;
        }
        public void SaveSetting()
        {
            if (projectPath == projectPathInit)
            {
                projectPath = null;
            }
            if (SerializationFile.TrySerialize(Instance))
            {
                try
                {
                    SerializationFile.SerializeObject(userDataPath, Instance);
                }
                catch(Exception ex)
                {
                    Util.WriteLog(this.GetType(), ex);
                    Util.Notify(Level.Err,"用户设置保存失败");
                }
            }
        }
    }
}
