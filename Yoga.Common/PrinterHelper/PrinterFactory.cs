using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoga.Common.PrinterHelper
{
    public static class PrinterFactory
    {
        /// <summary>
        /// 打印实例 此处需要添加纸张长度计算
        /// </summary>
        public static void  test()
        {
            var printer = PrinterFactory.GetPrinter("Adobe PDF", PaperWidth.Paper80mm);
            printer.NewRow();
            printer.NewRow();
            printer.PrintText("永辉超市", FontSize.Huge, StringAlignment.Center);
            printer.NewRow();
            printer.NewRow();
            printer.NewRow();
            printer.PrintText("操作员：张三");
            printer.PrintText(DateTime.Now.ToString("HH:mm:ss"), stringAlignment: StringAlignment.Far);
            printer.NewRow();
            printer.PrintLine();
            printer.NewRow();
            printer.PrintText("商品");
            printer.PrintText("单价", offset: 0.35f);
            printer.PrintText("数量", offset: 0.65f);
            printer.PrintText("总价", stringAlignment: StringAlignment.Far);
            printer.NewRow();
            printer.PrintLine();
            printer.NewRow();
            printer.PrintText("**长白山大萝卜,跳楼吐血大甩卖，不甜不要钱**", width: 0.35f);
            printer.PrintText("6.00", width: 0.2f, offset: 0.35f);
            printer.PrintText("2.00", width: 0.2f, offset: 0.65F);
            printer.PrintText("12.00", stringAlignment: StringAlignment.Far);
            printer.NewRow();
            printer.NewRow();
            printer.PrintText("大螃蟹", width: 0.35f);
            printer.PrintText("6.000000000001", width: 0.2f, offset: 0.35f);
            printer.PrintText("1", width: 0.2f, offset: 0.65F);
            printer.PrintText("6.000000000001", offset: 0.8f, width: 0.2f);
            printer.NewRow();
            printer.PrintLine();
            printer.NewRow();
            //var bitmap = new Bitmap("qr.png");
            //printer.PrintImage(bitmap, StringAlignment.Center);
            printer.NewRow();
            printer.PrintLine();
            printer.NewRow();
            printer.PrintText("感谢光临，欢迎下次再来！", stringAlignment: StringAlignment.Center);
            printer.NewRow();
            printer.Finish();
        }
        public static Printer GetPrinter(string printerName, double paperWidth, int? pagerHeight = null)
        {
            if (string.IsNullOrEmpty(printerName)) throw new ArgumentException(nameof(printerName));
            return new Printer(printerName, paperWidth, pagerHeight ?? 10000);
        }

        public static Printer GetPrinter(string printerName, PaperWidth paperWidth, int? pagerHeight = null)
        {
            switch (paperWidth)
            {
                case PaperWidth.Paper80mm:
                    //80打印纸扣去两边内距实际可打的宽度为72.1
                    return GetPrinter(printerName, 72.1, pagerHeight);
                case PaperWidth.Paper76mm:
                    //76打印纸扣去两边内距实际可打的宽度为63.5
                    return GetPrinter(printerName, 63.5, pagerHeight);
                case PaperWidth.Paper58mm:
                    //58打印纸扣去两边内距实际可打的宽度为48
                    return GetPrinter(printerName, 48, pagerHeight);
                default:
                    throw new ArgumentException(nameof(paperWidth));
            }

        }
    }
}
