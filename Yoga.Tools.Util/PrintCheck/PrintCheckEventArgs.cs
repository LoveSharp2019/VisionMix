using System;

namespace Yoga.Tools.PrintCheck
{
    public class PrintCheckEventArgs: EventArgs
    {
        public readonly PrintCheckMessage PrintCheckMessage;
        public PrintCheckEventArgs(PrintCheckMessage printCheckMessage)
        {
            this.PrintCheckMessage = printCheckMessage;
        }
    }
}
