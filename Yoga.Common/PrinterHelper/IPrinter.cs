using System.Drawing;

namespace Yoga.Common.PrinterHelper
{
    public interface IPrinter
    {
        void PrintText(
            string content,
            FontSize fontSize = FontSize.Normal,
            StringAlignment stringAlignment = StringAlignment.Near,
            float width = 1,
            float offset = 0
            );

        void PrintImage(
            Image image,
            StringAlignment stringAlignment = StringAlignment.Near);

        void PrintLine(FontSize fontSize = FontSize.Normal);

        void NewRow();

        /// <summary>
        /// invoke this method when you ready to print document
        /// </summary>
        void Finish();
    }
}
