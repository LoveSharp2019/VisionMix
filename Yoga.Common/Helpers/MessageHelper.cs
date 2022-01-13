using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Common.Helpers
{
    /// <summary>
    /// Message封装
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// 显示一般的提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        public static DialogResult ShowTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 显示询问用户信息，并显示错误标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 显示询问用户信息，并显示警告标志
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
        }
    }
}
