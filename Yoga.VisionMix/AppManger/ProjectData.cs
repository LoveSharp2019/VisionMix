using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoga.Camera;
using Yoga.Tools;
using Yoga.Common;
using Yoga.Common.FileAct;
using System.IO;
using Yoga.Common.Basic;

namespace Yoga.VisionMix.AppManger
{
    [Serializable]
    public class ProjectData: ISerializeCheck
    {

        private static ProjectData instance;
        private Dictionary<int, List<Tools.ToolBase>> toolsDic;
        private Dictionary<int, Camera.CameraPram> cameraPramDic;

        private CommunicationParam communicationParam; 

        public Dictionary<int, List<ToolBase>> ToolsDic
        {
            get
            {
                return toolsDic;
            }

             set
            {
                toolsDic = value;
            }
        }

        public Dictionary<int, CameraPram> CameraPramDic
        {
            get
            {
                return cameraPramDic;
            }

            set
            {
                cameraPramDic = value;
            }
        }

        public CommunicationParam CommunicationParam
        {
            get
            {
                if (communicationParam==null)
                {
                    communicationParam = new CommunicationParam();
                }
                return communicationParam;
            }

            set
            {
                communicationParam = value;
            }
        }

        public static ProjectData Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new ProjectData();
                }
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public  CameraPram GetCameraPram(int key)
        {
            if (cameraPramDic == null)
            {
                cameraPramDic = new Dictionary<int, CameraPram>();
            }
            if (cameraPramDic.ContainsKey(key) == false)
            {
                CameraPram camera1 = new CameraPram();
                cameraPramDic.Add(key, camera1);
            }
            return cameraPramDic[key];
        }

        /// <summary>
        /// 保存工程数据到文件
        /// </summary>
        /// <param name="path"></param>
        public  void SaveProject(string path)
        {
            bool canSave = true;
            //读取数据
            this.ToolsDic = ToolsFactory.ToolsDic;

            this.SerializeCheck();
            foreach (var item in ToolsDic.Values)
            {
                foreach (var item1 in item)
                {
                    item1.ClearTrainData();
                    item1.SerializeCheck();
                    if (SerializationFile.TrySerialize(item1) == false)
                    {
                        canSave = false;
                        throw new Exception(item1.Name + "数据异常");
                    }
                }
            }

            if (canSave)
            {
                SerializationFile.SerializeObject(path, this);
            }
        }
        public  void ReadProject(string path)
        {
            foreach (var item in ToolsFactory.ToolsDic.Values)
            {
                foreach (var item1 in item)
                {
                    item1.ClearTestData();
                }
            }
            string pathWant = "";
            if (path.EndsWith(".prj"))
            {
                pathWant = path;
            }
            else
            {
                pathWant = Environment.CurrentDirectory + "\\project\\" + path + ".prj";
            }
            ProjectData project = SerializationFile.DeserializeObject(
                pathWant) as ProjectData;
            //Dictionary<int, BindingList<ToolBase>> tools = SerializationFile.DeserializeObject(
            //    path) as Dictionary<int, BindingList<ToolBase>>;
            if (project == null)
            {

                Util.Notify("工程数据加载失败");
            }
            else
            {
                ProjectData.Instance = project;
                ToolsFactory.ToolsDic = project.ToolsDic;
                Common.Util.Notify("工程数据加载成功");
            }

        }

        public void SerializeCheck()
        {
            return;
        }
    }
}
