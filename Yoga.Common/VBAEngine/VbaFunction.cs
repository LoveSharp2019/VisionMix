using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Common.VBAEngine
{
    public class VbaFunction
    {
        /// <summary>
        /// 信息提示框
        /// </summary>
        /// <param name="s">提示信息</param>
        public static void Show(string s)
        {
            MessageBox.Show(s);
        }
    }
}
