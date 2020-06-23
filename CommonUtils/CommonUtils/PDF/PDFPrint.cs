using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.License;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace CommonUtils.PDF
{
    public class PDFPrint
    {
        public static void UseVirtualPrinter()
        {
            //使用虚拟打印机（Microsoft XPS Document Writer）

            //加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile("Test.pdf");

            //选择Microsoft XPS Document Writer打印机
            doc.PrintDocument.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";

            //打印PDF文档到XPS格式
            doc.PrintDocument.PrinterSettings.PrintToFile = true;
            doc.PrintDocument.PrinterSettings.PrintFileName = "PrintToXps.xps";
            doc.PrintDocument.Print();
        }

        public static void FixPrinter()
        {
            //指定打印机及文档打印页码范围

            //加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile("Test.pdf");

            //设置打印对话框属性
            PrintDialog dialogPrint = new PrintDialog();
            dialogPrint.AllowPrintToFile = true;
            dialogPrint.AllowSomePages = true;
            dialogPrint.PrinterSettings.MinimumPage = 1;
            dialogPrint.PrinterSettings.MaximumPage = doc.Pages.Count;
            dialogPrint.PrinterSettings.FromPage = 1;
            dialogPrint.PrinterSettings.ToPage = doc.Pages.Count;

            if (dialogPrint.ShowDialog() == DialogResult.OK)
            {
                //指定打印机及打印页码范围
                doc.PrintFromPage = dialogPrint.PrinterSettings.FromPage;
                doc.PrintToPage = dialogPrint.PrinterSettings.ToPage;
                doc.PrinterName = dialogPrint.PrinterSettings.PrinterName;

                //打印文档
                PrintDocument printDoc = doc.PrintDocument;
                dialogPrint.Document = printDoc;
                printDoc.Print();
            }
        }

        public static void UseDefaultPrint()
        {
            //静默打印

            //加载PDF文档
            var doc = new PdfDocument();
            doc.LoadFromFile("Test.pdf");

            //静默打印PDF文档
            PrintDocument printDoc = doc.PrintDocument;
            printDoc.PrintController = new StandardPrintController();
            printDoc.Print();
        }

        public static void DoublePagePrint()
        {
            //双面打印

            //加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile("Test.pdf");

            //判断打印机是否支持双面打印
            bool isDuplex = doc.PrintDocument.PrinterSettings.CanDuplex;
            if (isDuplex)
            {
                //如果支持则设置双面打印模式，可选：Default/Simplex/Horizontal/Vertical
                doc.PrintDocument.PrinterSettings.Duplex = Duplex.Default;
                //打印文档
                doc.PrintDocument.Print();
            }
        }

        public static void UseDefaultPrinter(string pdfFile)
        {
            Task.Run(()=>
            { 
            //创建PdfDocument类的对象，并加载PDF文档
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(pdfFile);
            //指定打印机
            //doc.PrinterName = "HP LaserJet M1522 MFP Series PCL 6";
            //使用默认打印机打印文档所有页面
            doc.Print();
            });
        }
    }
}
