using System;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Forms;

namespace Yoga.Common.VBAEngine
{
    [Serializable]
    /// <summary>
    /// 基于VB.NET的脚本引擎
    /// </summary>
    /// <remarks>
    /// 本对象能动态的设置和编译VB.NET模块代码，并动态的执行其中定义的方法，本对象适用于微软.NET框架1.1和2.0.
    /// 编制 袁永福
    /// </remarks>
    /// 
    public class XVBAEngine : System.IDisposable
    {

        int startCodeLineCount = 0;
        /// <summary>
        /// 初始化对象
        /// </summary>
        public XVBAEngine()
        {
            Init();
        }
        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="SourceCode">脚本代码</param>
        public XVBAEngine(string SourceCode)
        {
            Init();
            this.ScriptText = SourceCode;
        }

        /// <summary>
        /// 初始化脚本引擎
        /// </summary>
        private void Init()
        {
            this.mySourceImports.Add("System");
            this.mySourceImports.Add("Microsoft.VisualBasic");

            this.myVBCompilerImports.Add("Microsoft.VisualBasic");
            this.myVBCompilerImports.Add("System.Collections.Generic");
            this.myVBCompilerImports.Add("System.Drawing");
            this.myVBCompilerImports.Add("System.Linq");
            this.myVBCompilerImports.Add("HalconDotNet");

            this.myCompilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.Data.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.Xml.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");
            this.myCompilerParameters.ReferencedAssemblies.Add("halcondotnet.dll");
        }
        /// <summary>
        /// 脚本编译是否成功
        /// </summary>
        private bool bolDebuged = false;

        /// <summary>
        /// 脚本代码改变标记
        /// </summary>
        private bool bolScriptModified = true;

        /// <summary>
        /// 原始的VBA脚本文本
        /// </summary>
        private string strScriptText = string.Empty;

        /// <summary>
        /// 是否调试成功
        /// </summary>
        public bool bDebuged
        {
            get { return bolDebuged; }
        }
        /// <summary>
        /// 原始的VBA脚本文本
        /// </summary>
        public string ScriptText
        {
            get
            {
                return strScriptText;
            }
            set
            {
                if (strScriptText != value)
                {
                    bolScriptModified = true;
                    strScriptText = value;
                }
            }
        }

        private bool bolEnabled = true;
        /// <summary>
        /// 对象是否可用
        /// </summary>
        public bool Enabled
        {
            get
            {
                return bolEnabled;
            }
            set
            {
                bolEnabled = value;
            }
        }

        private bool bolOutputDebug = true;
        /// <summary>
        /// 脚本在运行过程中可否输出调试信息
        /// </summary>
        public bool OutputDebug
        {
            get
            {
                return bolOutputDebug;
            }
            set
            {
                bolOutputDebug = value;
            }
        }
        /// <summary>
        /// VB.NET编译器参数
        /// </summary>
        private CompilerParameters myCompilerParameters = new CompilerParameters();
        /// <summary>
        /// 引用的名称列表
        /// </summary>
        public StringCollection ReferencedAssemblies
        {
            get
            {
                return myCompilerParameters.ReferencedAssemblies;
            }
        }

        /// <summary>
        /// 添加指定的类型所在的引用
        /// </summary>
        /// <param name="SourceType">对象类型</param>
        public void AddReferenceAssemblyByType(Type SourceType)
        {
            if (SourceType == null)
            {
                throw new ArgumentNullException("SourceType");
            }
            System.Uri uri = new Uri(SourceType.Assembly.CodeBase);
            string path = null;
            if (uri.Scheme == System.Uri.UriSchemeFile)
            {
                path = uri.LocalPath;
            }
            else
            {
                path = uri.AbsoluteUri;
            }
            if (this.myCompilerParameters.ReferencedAssemblies.Contains(path) == false)
            {
                this.myCompilerParameters.ReferencedAssemblies.Add(path);
            }
        }

        /// <summary>
        /// 源代码中使用的名称空间导入
        /// </summary>
        private StringCollection mySourceImports = new StringCollection();
        /// <summary>
        /// 源代码中使用的名称空间导入
        /// </summary>
        public StringCollection SourceImports
        {
            get
            {
                return mySourceImports;
            }
        }

        /// <summary>
        /// VB编译器使用的名称空间导入
        /// </summary>
        private StringCollection myVBCompilerImports = new StringCollection();
        /// <summary>
        /// VB编译器使用的名称空间导入
        /// </summary>
        public StringCollection VBCompilerImports
        {
            get
            {
                return myVBCompilerImports;
            }
        }

        /// <summary>
        /// 编译脚本生成的程序集
        /// </summary>
        private System.Reflection.Assembly myAssembly = null;
        /// <summary>
        /// 所有缓存的程序集
        /// </summary>
        private static Hashtable myAssemblies = new Hashtable();


        private string strCompilerOutput = null;
        /// <summary>
        /// 编译器输出文本
        /// </summary>
        public string CompilerOutput
        {
            get
            {
                return strCompilerOutput;
            }
        }

        private int intScriptVersion = 0;
        /// <summary>
        /// 脚本版本
        /// </summary>
        public int ScriptVersion
        {
            get
            {
                this.CheckReady();
                return intScriptVersion;
            }
        }

        /// <summary>
        /// 所有脚本方法的信息列表
        /// </summary>
        private ArrayList myScriptMethods = new ArrayList();
        /// <summary>
        /// 脚本方法信息
        /// </summary>
        private class ScriptMethodInfo
        {
            /// <summary>
            /// 模块名称
            /// </summary>
            public string ModuleName = null;
            /// <summary>
            /// 方法名称
            /// </summary>
            public string MethodName = null;
            /// <summary>
            /// 方法对象
            /// </summary>
            public System.Reflection.MethodInfo MethodObject = null;
            /// <summary>
            /// 方法返回值
            /// </summary>
            public System.Type ReturnType = null;
            /// <summary>
            /// 指向该方法的委托
            /// </summary>
            public System.Delegate MethodDelegate = null;
        }

        /// <summary>
        /// 检查脚本引擎状态
        /// </summary>
        /// <returns>脚本引擎是否可用</returns>
        public bool CheckReady()
        {
            if (bolClosedFlag)
                return false;
            if (this.bolEnabled == false)
            {
                return false;
            }
            if (bolScriptModified == false)
            {
                return myAssembly != null;
            }
            return Compile();
        }

        /// <summary>
        /// 清空脚本
        /// </summary>
        public void Clear()
        {
            myScriptMethods.Clear();
        }
        /// <summary>
        /// 编译脚本
        /// </summary>
        /// <returns>编译是否成功</returns>
        public bool Compile()
        {
            if (bolClosedFlag)
                return false;
            intScriptVersion++;
            bolDebuged = false;
            myScriptMethods.Clear();
            myAssembly = null;
            bolScriptModified = false;

            // 生成编译用的完整的VB源代码
            string ModuleName = "mdlXVBAScriptEngine";
            string nsName = "NameSpaceXVBAScriptEngien";
            System.Text.StringBuilder mySource = new System.Text.StringBuilder();
            mySource.Append("Option Strict Off");
            foreach (string import in this.mySourceImports)
            {
                mySource.Append("\r\nImports " + import);
            }
            mySource.Append("\r\nNamespace " + nsName);
            mySource.Append("\r\nModule " + ModuleName);
            mySource.Append("\r\n");

            mySource.Append("sub Cal() \r\ntry \r\n");

            string datTmp = mySource.ToString();
            startCodeLineCount = datTmp.Split('\r').Length;


            mySource.Append(this.strScriptText);

            mySource.Append("\r\nCatch ex As Exception \r\nh.show(ex.tostring) \r\nEnd Try \r\nend sub");
            mySource.Append("\r\nEnd Module");
            mySource.Append("\r\nEnd Namespace");
            string strRuntimeSource = mySource.ToString();

            // 检查程序集缓存区
            myAssembly = (System.Reflection.Assembly)myAssemblies[strRuntimeSource];
            if (myAssembly == null)
            {
                // 设置编译参数
                this.myCompilerParameters.GenerateExecutable = false;
                this.myCompilerParameters.GenerateInMemory = true;
                this.myCompilerParameters.IncludeDebugInformation = true;
                if (this.myVBCompilerImports.Count > 0)
                {
                    // 添加 imports 指令
                    System.Text.StringBuilder opt = new System.Text.StringBuilder();
                    foreach (string import in this.myVBCompilerImports)
                    {
                        if (opt.Length > 0)
                        {
                            opt.Append(",");
                        }
                        opt.Append(import.Trim());
                    }
                    opt.Insert(0, " /imports:");
                    for (int iCount = 0; iCount < this.myVBCompilerImports.Count; iCount++)
                    {
                        this.myCompilerParameters.CompilerOptions = opt.ToString();
                    }
                }//if

                if (this.bolOutputDebug)
                {
                    // 输出调试信息
                    System.Diagnostics.Debug.WriteLine(" Compile VBA.NET script \r\n" + strRuntimeSource);
                    foreach (string dll in this.myCompilerParameters.ReferencedAssemblies)
                    {
                        System.Diagnostics.Debug.WriteLine("Reference:" + dll);
                    }
                }

                // 对VB.NET代码进行编译
                Microsoft.VisualBasic.VBCodeProvider provider = new Microsoft.VisualBasic.VBCodeProvider();
#if DOTNET11
    // 这段代码用于微软.NET1.1
    ICodeCompiler compiler = provider.CreateCompiler();
    CompilerResults result = compiler.CompileAssemblyFromSource(
        this.myCompilerParameters, 
        strRuntimeSource );
#else
                // 这段代码用于微软.NET2.0或更高版本
                CompilerResults result = provider.CompileAssemblyFromSource(
                    this.myCompilerParameters,
                    strRuntimeSource);
#endif
                // 获得编译器控制台输出文本
                System.Text.StringBuilder myOutput = new System.Text.StringBuilder();
                foreach (string line in result.Output)
                {
                    myOutput.Append("\r\n" + line);
                }
                this.strCompilerOutput = myOutput.ToString();

                if (this.bolOutputDebug)
                {
                    // 输出编译结果
                    if (this.strCompilerOutput.Length > 0)
                    {
                        //System.Diagnostics.Debug.WriteLine("VBAScript Compile result" + strCompilerOutput);
                        //////////////////////
                        //string strErrorMsg = "检测出 " + result.Errors.Count.ToString() + " 个错误:";
                        string strErrorMsg = string.Empty;
                        int count = 0;
                        for (int x = 0; x < result.Errors.Count; x++)
                        {
                            if (result.Errors[x].IsWarning)
                                continue;
                            strErrorMsg = strErrorMsg + Environment.NewLine +
                                        "第 " + (result.Errors[x].Line - (startCodeLineCount-1)).ToString() + " 行:  " +
                                         result.Errors[x].ErrorText;
                            count++;
                        }
                        strErrorMsg = "检测出 " + count.ToString() + " 个错误:" + strErrorMsg;

                        strCompilerOutput = strErrorMsg;
                        ///////////////////////
                    }
                }

                provider.Dispose();

                if (result.Errors.HasErrors == false)
                {
                    // 若没有发生编译错误则获得编译所得的程序集
                    this.myAssembly = result.CompiledAssembly;
                }
                if (myAssembly != null)
                {
                    // 将程序集缓存到程序集缓存区中
                    myAssemblies[strRuntimeSource] = myAssembly;
                }
            }

            if (this.myAssembly != null)
            {
                // 检索脚本中定义的类型
                Type ModuleType = myAssembly.GetType(nsName + "." + ModuleName);
                if (ModuleType != null)
                {
                    System.Reflection.MethodInfo[] ms = ModuleType.GetMethods(
                        System.Reflection.BindingFlags.Public
                        | System.Reflection.BindingFlags.NonPublic
                        | System.Reflection.BindingFlags.Static);
                    foreach (System.Reflection.MethodInfo m in ms)
                    {
                        // 遍历类型中所有的静态方法
                        // 对每个方法创建一个脚本方法信息对象并添加到脚本方法列表中。
                        ScriptMethodInfo info = new ScriptMethodInfo();
                        info.MethodName = m.Name;
                        info.MethodObject = m;
                        info.ModuleName = ModuleType.Name;
                        info.ReturnType = m.ReturnType;
                        this.myScriptMethods.Add(info);
                        if (this.bolOutputDebug)
                        {
                            // 输出调试信息
                            System.Diagnostics.Debug.WriteLine("Get vbs method \"" + m.Name + "\"");
                        }
                    }//foreach
                    bolDebuged = true;
                }//if
            }//if
            return bolDebuged;
        }

        private bool bolClosedFlag = false;
        /// <summary>
        /// 关闭对象
        /// </summary>
        public void Close()
        {
            intScriptVersion++;
            bolClosedFlag = true;
            this.myScriptMethods.Clear();
            this.myAssembly = null;
            if (this.bolOutputDebug)
            {
                System.Diagnostics.Debug.WriteLine("XVBAEngine closed");
            }
        }

        /// <summary>
        /// 获得脚本中所有的方法的名称组成的数组
        /// </summary>
        public string[] ScriptMethodNames
        {
            get
            {
                if (CheckReady() == false)
                {
                    return null;
                }
                ArrayList names = new ArrayList();
                foreach (ScriptMethodInfo info in this.myScriptMethods)
                {
                    names.Add(info.MethodName);
                }
                return (string[])names.ToArray(typeof(string));
            }
        }

        /// <summary>
        /// 判断是否存在指定名称的脚本方法
        /// </summary>
        /// <param name="MethodName">方法名称</param>
        /// <returns>是否存在指定的方法</returns>
        public bool HasMethod(string MethodName)
        {
            if (CheckReady() == false)
            {
                return false;
            }
            if (this.myScriptMethods.Count > 0)
            {
                foreach (ScriptMethodInfo info in this.myScriptMethods)
                {
                    if (string.Compare(info.MethodName, MethodName, true) == 0)
                    {
                        return true;
                    }//if
                }//foreach
            }//if
            return false;
        }

        /// <summary>
        /// 安全的简单的执行脚本方法
        /// </summary>
        /// <param name="MethodName">方法名称</param>
        public void ExecuteSub(string MethodName)
        {
            Execute(MethodName, null, false);
        }
        /// <summary>
        /// 执行脚本方法
        /// </summary>
        /// <param name="MethodName">方法名称</param>
        /// <param name="Parameters">参数</param>
        /// <param name="ThrowException">若发生错误是否触发异常</param>
        /// <returns>执行结果</returns>
        public bool Execute(string MethodName, object[] Parameters, bool ThrowException)
        {
            // 检查脚本引擎状态
            if (CheckReady() == false)
            {
                return false;
            }
            // 若发生错误则不抛出异常，安静的退出
            // 检查参数
            if (MethodName == null)
            {
                return false;
            }
            MethodName = MethodName.Trim();
            if (MethodName.Length == 0)
            {
                return false;
            }
            if (this.myScriptMethods.Count > 0)
            {
                foreach (ScriptMethodInfo info in this.myScriptMethods)
                {
                    // 遍历所有的脚本方法信息，不区分大小写的找到指定名称的脚本方法
                    if (string.Compare(info.MethodName, MethodName, true) == 0)
                    {
                        try
                        {
                            // 执行脚本方法
                            object result = info.MethodObject.Invoke(null, Parameters);
                            // 返回脚本方法返回值
                            //return result;
                        }
                        catch (Exception ext)
                        {
                            // 若发生错误则输出调试信息
                            //System.Console.WriteLine("VBA:" + MethodName + ":" + ext.Message);
                            return false;
                        }
                    }//if
                }//foreach
                return true;
            }//if
            return false;
        }//public object Execute( string MethodName , object[] Parameters , bool ThrowException )

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

    }//public class XVBAScriptEngine
}