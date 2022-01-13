using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Tools
{
    public struct ToolReflection
    {
        public Assembly Assembly;
        public Type Type;
        public ToolReflection(Assembly assembly, Type type)
        {
            this.Assembly = assembly;
            this.Type = type;
        }
    }
}
