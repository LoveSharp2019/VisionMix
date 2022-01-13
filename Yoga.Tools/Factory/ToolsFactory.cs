using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Yoga.Common;

namespace Yoga.Tools
{
    public static class ToolsFactory
    {
        private static Dictionary<int, List<ToolBase>> toolsDic;


        private static List<ToolReflection> allToolsType;
        public static readonly string PlugInsDir = System.IO.Path.Combine(System.Environment.CurrentDirectory, "PlugIns\\");

        static ToolsFactory()
        {
            GetToolList(1);
        }
        public static Dictionary<int, List<ToolBase>> ToolsDic
        {
            get
            {
                if (toolsDic == null)
                {
                    toolsDic = new Dictionary<int, List<ToolBase>>();
                }
                return toolsDic;
            }
            set
            {
                toolsDic = value;
                foreach (var item in toolsDic.Values)
                {
                    foreach (var item1 in item)
                    {
                        item1.LoadTool();
                    }
                }
            }
        }
        /// <summary>
        /// 获取工具组
        /// </summary>
        /// <param name="settingKey"></param>
        /// <returns></returns>
        public static List<ToolBase> GetToolList(int settingKey)
        {
            if (ToolsDic.ContainsKey(settingKey) == false)
            {
                List<ToolBase> toolNew = new List<ToolBase>();
                ToolsDic.Add(settingKey, toolNew);
                CreateTool(settingKey, new ToolReflection(Assembly.GetExecutingAssembly(), typeof(CreateImage.CreateImageTool)));
            }
            return ToolsDic[settingKey];
        }
        private static T CreateInstance<T>(ToolReflection toolReflection, object[] parameters)
        {
            try
            {
                object ect = toolReflection.Assembly.CreateInstance(toolReflection.Type.FullName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);//加载程序集，创建程序集里面的 命名空间.类型名 实例                
                return (T)ect;//类型转换并返回
            }
            catch (Exception ex)
            {
                Util.WriteLog(typeof(ToolsFactory), ex);
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
        public static void CreateTool(int settingKey, ToolReflection toolReflection)
        {
            object[] parameters = new object[1];
            parameters[0] = settingKey;
            ToolBase tool = CreateInstance<ToolBase>(toolReflection, parameters);
            if (tool == null)
            {
                throw new Exception("工具创建失败");
            }
            int index = -1;
            int name = 1;

            GetToolList(settingKey);
            string nameTmp = string.Format("C{0}{1:00}", settingKey, name);
            index = GetToolList(settingKey).FindIndex(
               x => x.Name == nameTmp);
            while (index != -1)
            {
                name++;
                nameTmp = string.Format("C{0}{1:00}", settingKey, name);
                index = GetToolList(settingKey).FindIndex(
               x => x.Name == nameTmp);
            }
            tool.SetToolName(nameTmp);
            if (tool != null)
            {
                GetToolList(settingKey).Add(tool);
            }
        }
        public static void DeleteToolDic(int cameraIndex)
        {
            if (ToolsDic.ContainsKey(cameraIndex))
            {
                foreach (var item in ToolsDic[cameraIndex])
                {
                    item.Dispose();
                }
            }
            ToolsDic.Remove(cameraIndex);
        }
        public static void DeleteTool(int cameraIndex, int index)
        {
            List<ToolBase> selectTools = GetToolList(cameraIndex);
            if (index < 0 || index > selectTools.Count - 1)
            {
                return;
            }
            //新添加 手动释放工具对象
            selectTools[index].Dispose();
            selectTools.RemoveAt(index);

        }
        public static List<ToolReflection> AllToolsType
        {
            get
            {
                if (allToolsType == null)
                {
                    allToolsType = new List<ToolReflection>();
                    List<Assembly> lstAssembly = new List<Assembly>();
                    //添加当前目录
                    Assembly assem = Assembly.GetExecutingAssembly();
                    lstAssembly.Add(assem);
                    //添加插件目录
                    if (Directory.Exists(PlugInsDir) == false)//判断是否存在
                    {
                        Directory.CreateDirectory(PlugInsDir);//创建新路径
                    }
                    foreach (var dllFile in Directory.GetFiles(PlugInsDir))
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(dllFile);
                            if (!fi.Name.EndsWith(".dll")) continue;
                            if (!fi.Name.Contains("Yoga")) continue;
                            Assembly assemPlugIn = Assembly.LoadFile(fi.FullName);

                            lstAssembly.Add(assemPlugIn);
                        }
                        catch (Exception)
                        {

                        }

                    }

                    foreach (Assembly item in lstAssembly)
                    {
                        //Type[] tt = item.GetTypes();
                        try
                        {
                            foreach (Type tChild in item.GetTypes())
                            {
                                if (tChild.BaseType == (typeof(ToolBase)) && typeof(IToolEnable).IsAssignableFrom(tChild))
                                {
                                    allToolsType.Add(new ToolReflection(item, tChild));
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                            ;
                        }
                        
                    }
                }
                return allToolsType;
            }
        }

        public static List<string> GetAllMatchingTools(int settingIndex)
        {
            List<string> toolNamelist = new List<string>();
            foreach (var item in ToolsDic[settingIndex])
            {
                if (typeof(IMatching).IsAssignableFrom(item.GetType()))
                {
                    toolNamelist.Add(item.Name);
                }
            }
            return toolNamelist;
        }

        public static ToolBase GetTool(string toolName)
        {
            try
            {
                int settingKey = int.Parse(toolName.Substring(1,1));
                List<ToolBase> toolList = GetToolList(settingKey);

                ToolBase tool = toolList.Find(x=>x.Name== toolName.ToUpper());
                return tool;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

    }
}
