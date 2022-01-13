using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Tools.Factory
{

    /// <summary>
    /// vba引擎共用方法
    /// </summary>
    [Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute()]
    public class ToolObject
    {
        static ToolVbaFun f = null;
        /// <summary>
        /// 全局的 HMeasureSYS 对象
        /// </summary>
        public static ToolVbaFun F
        {
            get
            {
                return f;
            }
        }
    }
    public class ToolVbaFun
    {
        public static void Show(string s)
        {
            MessageBox.Show(s);
        }

        public static bool GetToolStatus(string toolName)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                return tool.IsOk;
            }
            return false;
        }
        public static void SetValue(string toolName, string key, object value)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                return;
            }
            tool.SetValue(key, value);
        }

        public static object GetValue(string toolName, string key)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                return null;
            }
            return tool.getValue(key);
        }
        public static void MarkTool(string toolName, bool flag)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                tool.MarkTool(flag);
            }
        }
        public static void SetToolResult(string toolName, string dat)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                tool.Result = dat;
            }
        }

        public static string GetToolResult(string toolName)
        {
            ToolBase tool = ToolsFactory.GetTool(toolName);
            if (tool != null)
            {
                return tool.Result;
            }
            return "";
        }

    }
}
