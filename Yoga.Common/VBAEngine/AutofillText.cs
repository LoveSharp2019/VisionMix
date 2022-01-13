using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.VBAEngine
{
    public class AutofillText
    {

        public List<string> MethodNameList { get; private set; }
        public AutofillText( List<string> methodNameList)
        {
            this.MethodNameList = methodNameList;
        }
        public override string ToString()
        {
            if (MethodNameList==null|| MethodNameList.Count<1)
            {
                return string.Empty;
            }
            StringBuilder str = new StringBuilder();
            MethodNameList.Sort();
            foreach (var item in MethodNameList)
            {
                str.Append(item);
                str.Append(" ");
            }
            return str.ToString().Trim();
        }
    }
}
