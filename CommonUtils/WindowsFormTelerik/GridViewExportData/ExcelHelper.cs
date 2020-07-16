﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.OleDb;
using CommonUtils.FileHelper;
using CommonUtils.Logger;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace WindowsFormTelerik.GridViewExportData
{
    public class ExcelHelper
    {
        //private static string fileName = null; //文件名
        private static IWorkbook workbook = null;
        private static FileStream fs = null;

        public enum ExcelTypeEnum
        {
            excelXls,
            excelXlsx
        }

        private static string GetFileName()
        {
            var fileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.ShowHelp = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
            return fileName;
        }

        /// <summary>
        /// 获取Sheets
        /// </summary>
        /// <returns></returns>
        private List<string> GetSheets(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook(fs);
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook(fs);

            List<string> sheets = new List<string>();

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);

                sheets.Add(sheet.SheetName);
            }

            return sheets;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static int DataTableToExcel(RadGridView radGridView, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            var data = RadGridViewHelper.ConvertGridViewToDataTable(radGridView, 0);
            if (null == data || data.Rows.Count <= 0)
                return 0;
            var fileName = FileSelect.SaveAs("Microsoft Excel files(*.xls)|*.xls", "C:\\");
            if (fileName == "")
                return 0;
            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to Excel", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        RadMessageBox.Show(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
                if (fs != null)
                    fs.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            if (null == data || data.Rows.Count <= 0)
                return 0;
            var fileName = FileSelect.SaveAs("Microsoft Excel files(*.xls)|*.xls", "C:\\");
            if (fileName == "")
                return 0;
            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                DialogResult dr = MessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                    catch (Exception ex)
                    {
                        string message = String.Format("The file cannot be opened on your system.\nError message: {0}", ex.Message);
                        MessageBox.Show(message, "Open File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (fs != null)
                    fs.Close();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static System.Data.DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn,string fileName)
        {
            ISheet sheet = null;
            System.Data.DataTable data = new System.Data.DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                if (fs != null)
                    fs.Close();
                return data;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("Exception: " + ex.Message);
                return null;
            }
        }

        public static DataSet ExcelToDS(string filePath, ExcelTypeEnum excelType)
        {
            DataSet ds = new DataSet();
            if (filePath.Length <= 0)
            {
                return ds;
            }
            var strConn = "";
            var tableName = "";
            //此连接可以操作.xls与.xlsx文件 (支持Excel2003 和 Excel2007 的连接字符串)  
            //"IMEX=1 "如果列中的数据类型不一致，使用"IMEX=1"可必免数据类型冲突。

            if (excelType == ExcelTypeEnum.excelXls)
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            else if (excelType == ExcelTypeEnum.excelXlsx)
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filePath + "; Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";
            }
            //strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + Server.MapPath("ExcelFiles/Mydata2007.xlsx") + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            var strExcel = "";
            OleDbDataAdapter myCommand = null;

            System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                tableName = schemaTable.Rows[i][2].ToString().Trim();
                if (!tableName.Contains("FilterDatabase") && tableName.Substring(tableName.Length - 1, 1) != "_")
                {
                    ds.Tables.Add(tableName);
                    strExcel = string.Format("select * from [{0}]", tableName);
                    myCommand = new OleDbDataAdapter(strExcel, strConn);
                    myCommand.Fill(ds, tableName);
                }
            }
            conn.Close();
            return ds;
        }
    }
}
