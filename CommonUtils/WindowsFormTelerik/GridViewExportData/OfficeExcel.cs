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
using Microsoft.Office.Interop.Excel;
using ExcelLibrary;
using ExcelDataReader;
using ExcelDataReader.Core;
using ExcelDataReader.Exceptions;
using ExcelDataReader.Log;

namespace WindowsFormTelerik.GridViewExportData
{
    public class OfficeExcel
    {
        public static void SaveAsExcel(string filePath)
        {
            //1：打开文件，得到文件stream
            var streamData = File.Open(filePath, FileMode.Open, FileAccess.Read);
            //2：得到文件reader（需要NuGet包ExcelDataReader）
            var readerData = ExcelReaderFactory.CreateOpenXmlReader(streamData);
    //3：通过reader得到数据（需要NuGet包ExcelDataReader.DataSet ）
            var result = readerData.AsDataSet();
            //4：得到ExcelFile文件的表Sheet
            var sheet = result.Tables["INCA"];

        }
    }
}
