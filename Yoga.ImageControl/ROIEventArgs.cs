using System;

namespace Yoga.ImageControl
{
    /// <summary>
    /// 显示引发事件
    /// </summary>
    public class ViewEventArgs : EventArgs
    {
        public readonly ViewMessage ViewMessage;
        public ViewEventArgs(ViewMessage viewMessage)
        {
            this.ViewMessage = viewMessage;
        }
    }
}
