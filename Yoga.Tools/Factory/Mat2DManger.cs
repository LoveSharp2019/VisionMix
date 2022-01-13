using System;
using System.Collections.Generic;
using System.Linq;

namespace Yoga.Tools
{
    public delegate void SelectMatchingToolDelegate(IMatching  matchingTool);
    [Serializable]
    public class Mat2DManger
    {
        private string mat2DToolName;
        private bool useMat2D = false;
        [NonSerialized]
        IMatching matchingTool;
        [NonSerialized]
        private ToolBase tool;
        [NonSerialized]
        private List<string> mat2dTools;
        private bool useMat2DAlways = false;

        private int settingIndex;
        private string toolName;
        [NonSerialized]
        public SelectMatchingToolDelegate SelectMatchingToolObserver;
        #region 属性

        private bool canSelectTool = true;
        public string Mat2DToolName
        {
            get
            {
                return mat2DToolName;
            }

            set
            {
                mat2DToolName = value;
            }
        }

        public bool UseMat2D
        {
            get
            {
                return useMat2D;
            }

            set
            {
                useMat2D = value;
            }
        }
        /// <summary>
        /// 当前工具所使用的定位工具
        /// </summary>
        public IMatching MatchingTool
        {
            get
            {
                if (matchingTool == null)
                {
                    matchingTool = ToolsFactory.GetToolList(settingIndex).Find(
                  x => x.Name == mat2DToolName) as IMatching;
                }
                return matchingTool;
            }

            set
            {
                matchingTool = value;
                Mat2DToolName = ((ToolBase)matchingTool).Name;
                CanSelectTool = false;
            }
        }

        public List<string> Mat2dTools
        {
            get
            {
                if (mat2dTools == null)
                {
                    mat2dTools = ToolsFactory.GetAllMatchingTools(settingIndex);
                }
                return mat2dTools;
            }

        }
        /// <summary>
        /// 一直使用匹配定位
        /// </summary>
        public bool UseMat2DAlways
        {
            get
            {
                return useMat2DAlways;
            }

            set
            {
                if (value == true)
                {
                    useMat2D = true;
                }
                useMat2DAlways = value;
            }
        }

        public int SettingIndex
        {
            get
            {
                return settingIndex;
            }
        }

        public bool CanSelectTool
        {
            get
            {
                return canSelectTool;
            }

            set
            {
                canSelectTool = value;
            }
        }
        #endregion
        public Mat2DManger(ToolBase tool)
        {
            this.tool = tool;
            this.settingIndex = tool.SettingIndex;
            this.toolName = tool.Name;
        }
    }

}