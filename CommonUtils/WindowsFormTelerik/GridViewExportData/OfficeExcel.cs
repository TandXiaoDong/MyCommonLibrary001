using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using System.Data.OleDb;
using ExcelLibrary;
using ExcelDataReader;
using ExcelDataReader.Core;
using ExcelDataReader.Exceptions;
using ExcelDataReader.Log;
//using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Collections;
using Aspose.Words.Loading;
using Aspose.Words.Properties;
using Aspose.Words.BuildingBlocks;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using Aspose.Words.Fonts;
using Aspose.Words.Layout;
using Aspose.Words.Lists;
using Aspose.Words.MailMerging;
using Aspose.Words.Markup;
using Aspose.Words.Math;
using Aspose.Words.Pdf2Word;
using Aspose.Words.Rendering;
using Aspose.Words.Replacing;
using Aspose.Words.Reporting;
using Aspose.Words.Saving;
using Aspose.Words.Settings;
using Aspose.Words.Shaping;
using Aspose.Words.Tables;
using Aspose.Words.Themes;
using Aspose.Words.WebExtensions;
using Aspose.Cells;


namespace WindowsFormTelerik.GridViewExportData
{
    public class OfficeExcel
    {
        public static void SaveAsExcel(string filePath)
        {
            DataTable dt = new DataTable();
            //Aspose.Cells.License li = new Aspose.Cells.License();
            //li.SetLicense("Aspose.Cells.lic");
            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(filePath);
            Worksheet ws = wk.Worksheets[0];

            dt = ws.Cells.ExportDataTable(0, 0, ws.Cells.Rows.Count, ws.Cells.Columns.Count);
        }
    }
}
