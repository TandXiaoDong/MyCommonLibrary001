using System;
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
using Microsoft.Office.Interop.Excel;
using CommonUtils.FileHelper;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace WindowsFormTelerik.GridViewExportData
{
    public class ExcelHelper:IDisposable
    {
        //private static string fileName = null; //文件名
        private static IWorkbook workbook = null;
        private static FileStream fs = null;
        private bool disposed;
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

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }

        public static void ExportExcel1(System.Data.DataTable dt)
        {
            if (null == dt || dt.Rows.Count <= 0)
                return;
            var desPath = FileSelect.SaveAs("*.*|*.xls", "C:\\");
            if (desPath == "")
                return;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;
            app.UserControl = true;

            Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            //worksheet.Name = "sheet1"; // 修改sheet名称
            //Microsoft.Office.Interop.Excel.Range range;
            //long totalCount = dt.Rows.Count;
            //long rowRead = 0;
            //float percent = 0; // 导出进度

            // 保存列名，Excel的索引从1开始
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                // 标题设置
                //range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                //range.Interior.ColorIndex = 15; // 背景色
                //range.Font.Bold = true; // 粗体
            }
            // 保存数据
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                //rowRead++;
                //percent = ((float)(100 * rowRead)) / totalCount; // 当前进度
            }

            // 合计：
            //worksheet.Cells[DT.Rows.Count + 2, 1] = "合计";
            //worksheet.Cells[DT.Rows.Count + 2, 4] = DT.Compute("sum(Quantity)", ""); // DataTable筛选条件
            //worksheet.Cells[DT.Rows.Count + 2, 6] = DT.Compute("sum(TotalAmt)", "");

            //调整Excel的样式。
            //Microsoft.Office.Interop.Excel.Range rg = worksheet.Cells.get_Range("A3", worksheet.Cells[rowCount + 2, 32]);
            //rg.Borders.LineStyle = 1; //单元格加边框
            //worksheet.Columns.AutoFit(); //自动调整列宽

            //隐藏某一行
            //选中部分单元格，把选中的单元格所在的行的Hidden属性设为true
            //worksheet.get_Range(app.Cells[2, 1], app.Cells[2, 32]).EntireRow.Hidden = true;

            //删除某一行
            // worksheet.get_Range(app.Cells[2, 1], app.Cells[2, 32]).EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);

            workbook.SaveAs(desPath);

            // 释放资源
            workbook.Close();
            workbook = null;
            app.Quit();
            app = null;
        }

        private void CreateStringWriter(System.Data.DataTable dt, ref StringWriter sw)
        {
            string sheetName = "sheetName";

            sw.WriteLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<head>");
            sw.WriteLine("<!--[if gte mso 9]>");
            sw.WriteLine("<xml>");
            sw.WriteLine(" <x:ExcelWorkbook>");
            sw.WriteLine(" <x:ExcelWorksheets>");
            sw.WriteLine(" <x:ExcelWorksheet>");
            sw.WriteLine(" <x:Name>" + sheetName + "</x:Name>");
            sw.WriteLine(" <x:WorksheetOptions>");
            sw.WriteLine(" <x:Print>");
            sw.WriteLine(" <x:ValidPrinterInfo />");
            sw.WriteLine(" </x:Print>");
            sw.WriteLine(" </x:WorksheetOptions>");
            sw.WriteLine(" </x:ExcelWorksheet>");
            sw.WriteLine(" </x:ExcelWorksheets>");
            sw.WriteLine("</x:ExcelWorkbook>");
            sw.WriteLine("</xml>");
            sw.WriteLine("<![endif]-->");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine("<table>");
            sw.WriteLine(" <tr>");
            string[] columnArr = new string[dt.Columns.Count];
            int i = 0;
            foreach (DataColumn columns in dt.Columns)
            {

                sw.WriteLine(" <td>" + columns.ColumnName + "</td>");
                columnArr[i] = columns.ColumnName;
                i++;
            }
            sw.WriteLine(" </tr>");

            foreach (DataRow dr in dt.Rows)
            {
                sw.WriteLine(" <tr>");
                foreach (string columnName in columnArr)
                {
                    sw.WriteLine(" <td style='vnd.ms-excel.numberformat:@'>" + dr[columnName] + "</td>");
                }
                sw.WriteLine(" </tr>");
            }
            sw.WriteLine("</table>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
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
