using System;
using System.Windows.Forms;
using HalconDotNet;
using System.Collections.Generic;
using Yoga.Tools.Matching;
using System.Linq;
using Yoga.ImageControl;
using System.Reflection;

using System.Drawing;
using System.Text;
using Yoga.Common.VBAEngine;

namespace Yoga.Tools.VBA
{
    public partial class VabToolUnit : ToolsSettingUnit
    {
        private VbaTool tool;


        Common.VBAEngine.VbaEditBox vbaEditBox;

        public VabToolUnit(VbaTool tool)
        {
            InitializeComponent();
            locked = true;

            this.tool = tool;
            Init();
            HideMax();
            HideMin();

            InitScintilla();
            base.Init(tool.Name, tool);
            locked = false;
        }
        public override void ShowResult()
        {
        }
        private void Init()
        {

        }

        private void InitScintilla()
        {
            vbaEditBox = new Common.VBAEngine.VbaEditBox();
            groupBox1.Controls.Add(vbaEditBox);
            vbaEditBox.Dock = DockStyle.Fill;

            //vbaEditBox.SetKeyword("test");
        }
        private void VabToolUnit_Load(object sender, EventArgs e)
        {
            vbaEditBox.SetKeyword(getMethodsList());
            vbaEditBox.MethodNameDic = getMethodsDic();
            if (tool.Script != "")
                vbaEditBox.ScriptText = tool.Script;

        }


        public Dictionary<string, AutofillText> getMethodsDic()
        {
            Dictionary<string, AutofillText> dic = new Dictionary<string, AutofillText>();

            List<string> strList = new List<string>();
            //获取当前程序集的所有类

            foreach (Type t in Assembly.LoadFrom(Application.StartupPath + "/Yoga.Common.dll").GetTypes())
            {
                //只添加指定命名空间的类
                if (t.FullName.Contains("VbaFunction"))
                {
                    MethodInfo[] methods = t.GetMethods();
                    foreach (MethodInfo m in methods)
                    {
                        if (m.IsStatic)
                        {
                            if (!m.ToString().Contains("<"))
                            {
                                strList.Add(m.ToString().Split(' ')[1].Split('(')[0].Replace("set_", "").Replace("get_", ""));
                            }
                        }
                        
                    }
                    //strList.Add(t.Name);
                }
            }
            dic.Add("h", new AutofillText(strList));

            strList = new List<string>();
            foreach (Type t in Assembly.LoadFrom(Application.StartupPath + "/Yoga.Tools.dll").GetTypes())
            {
                //只添加指定命名空间的类
                if (t.FullName.Contains("ToolVbaFun"))
                {
                    MethodInfo[] methods = t.GetMethods();
                    foreach (MethodInfo m in methods)
                    {
                        if (m.IsStatic)
                        {
                            if (!m.ToString().Contains("<"))
                            {
                                strList.Add(m.ToString().Split(' ')[1].Split('(')[0].Replace("set_", "").Replace("get_", ""));

                            }
                        }                      
                    }
                    //strList.Add(t.Name);
                }
            }
            dic.Add("f", new AutofillText(strList));

            return dic;
        }
        public string getMethodsList()
        {
            List<string> strList = new List<string>();
            //获取当前程序集的所有类

            foreach (Type t in Assembly.LoadFrom(Application.StartupPath + "/Yoga.Common.dll").GetTypes())
            {
                //只添加指定命名空间的类
                if (t.FullName.Contains("VbaFunction"))
                {
                    MethodInfo[] methods = t.GetMethods();
                    foreach (MethodInfo m in methods)
                    {
                        if (!m.ToString().Contains("<"))
                            strList.Add("h." + m.ToString().Split(' ')[1].Split('(')[0].Replace("set_", "").Replace("get_", ""));
                    }
                    //strList.Add(t.Name);
                }
            }

            foreach (Type t in Assembly.LoadFrom(Application.StartupPath + "/Yoga.Tools.dll").GetTypes())
            {
                //只添加指定命名空间的类
                if (t.FullName.Contains("ToolVbaFun"))
                {
                    MethodInfo[] methods = t.GetMethods();
                    foreach (MethodInfo m in methods)
                    {
                        if (!m.ToString().Contains("<"))
                            strList.Add("f." + m.ToString().Split(' ')[1].Split('(')[0].Replace("set_", "").Replace("get_", ""));
                    }
                    //strList.Add(t.Name);
                }
            }

            StringBuilder str = new StringBuilder();

            foreach (var item in strList)
            {
                str.Append(item);
                str.Append(" ");
            }
            return str.ToString().Trim();
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
            //myVBAEngine.Clear();//清空所有脚本方法值
            txt_ErrorInfo.Text = "开始编译...";
            txt_ErrorInfo.Refresh();
            //StringBuilder str = new StringBuilder(VbaTool.vbaStartStr);
            //str.Append(vbaEditBox.ScEdit.Text.Trim());
            //str.Append(VbaTool.vbaEndStr);
            tool.VBAEngine.ScriptText = vbaEditBox.ScriptText.Trim();


            tool.Script = tool.VBAEngine.ScriptText;

            if (tool.VBAEngine.Compile() == false)
            {
                //MessageBox.Show(this, "编译错误:" + myVBAEngine.CompilerOutput, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_ErrorInfo.Text = "编译错误:" + tool.VBAEngine.CompilerOutput;
                //int index = myVBAEngine.CompilerOutput.IndexOf("保留所有权利。");
                //txt_ErrorInfo.Text = "编译错误:" + myVBAEngine.CompilerOutput.Substring(index + 17, myVBAEngine.CompilerOutput.Length - index -17);
                txt_ErrorInfo.ForeColor = Color.Red;
            }
            else
            {
                //MessageBox.Show(this, "编译成功", "系统提示");
                txt_ErrorInfo.Text = "编译成功:";
                txt_ErrorInfo.ForeColor = Color.Black;


            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                txt_ErrorInfo.Text = "开始运行...";
                txt_ErrorInfo.Refresh();
                // 调用脚本执行指定名称的脚本方法
                tool.VBAEngine.Execute("Cal", null, true);
                txt_ErrorInfo.Text = "运行完成";
            }
            catch (Exception ext)
            {
                txt_ErrorInfo.Text = "执行脚本错误:" + ext.Message;
                txt_ErrorInfo.ForeColor = Color.Red;
            }
        }
    }
}
