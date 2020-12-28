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
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using WindowsFormTelerik.FileHelper;
using WindowsFormTelerik.Logger;

namespace WindowsFormTelerik.GridViewExportData
{
    public class ExcelHelper2
    {
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumnHeader, string fileName)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            System.Data.DataTable data = new System.Data.DataTable();
            FileStream fs = null;
            int startRow = 0;

            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                CloseStream(fs);
                MessageBox.Show("文件打开失败！请检查文件是否被占用，关闭已打开文件后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return data;
            }

            try
            {
                if (Path.GetExtension(fileName) == ".xlsx")
                {
                    workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(fs);//2007
                }
                else if (Path.GetExtension(fileName) == ".xls")
                {
                    fs.Position = 0;
                    workbook = new NPOI.HSSF.UserModel.HSSFWorkbook(fs);//2003
                }
                if (workbook == null)
                {
                    CloseStream(fs);
                    MessageBox.Show($"打开文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return data;
                }
            }
            catch (Exception ex)
            {
                CloseStream(fs);
                MessageBox.Show($"{ex.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Aspose2Data(fileName);
                return data;
            }
            if (!string.IsNullOrEmpty(sheetName))
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
            if (sheet == null)
            {
                MessageBox.Show($"未查询到Sheet表格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CloseStream(fs);
                return data;
            }
            IRow firstRow = sheet.GetRow(0);
            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
            try
            {
                if (isFirstRowColumnHeader)
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
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        DataColumn column = new DataColumn("column" + (i + 1));
                        data.Columns.Add(column);
                    }
                    startRow = sheet.FirstRowNum;
                }

                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    bool IsStrNull = false;
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                        {
                            if (row.GetCell(j).ToString().Trim() != "")
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                                IsStrNull = true;
                            }
                        }
                    }
                    if (IsStrNull)
                    {
                        data.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                CloseStream(fs);
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return data;
            }
            CloseStream(fs);
            return data;
        }

        public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten, string fileName, bool IsShowExcel)
        {
            FileStream fs = null;
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            IWorkbook workbook = null;

            if (null == data)
                return 0;
            if (data.Rows.Count <= 0)
                return 0;
            if (fileName == "")
            {
                fileName = FileSelect.SaveAs("Microsoft Excel files(*.xls)|*.xls", "C:\\");
            }
            if (fileName == "")
                return 0;

            try
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建文件失败！请检查文件是否被占用，关闭已打开文件后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            try
            {
                if (Path.GetExtension(fileName) == ".xlsx") // 2007版本
                    workbook = new XSSFWorkbook();
                else if (Path.GetExtension(fileName) == ".xls") // 2003版本
                {
                    fs.Position = 0;
                    workbook = new HSSFWorkbook();
                }
            }
            catch (Exception ex)
            {
                var exMessage = "";
                if (ex.Message == "" && ex.InnerException != null)
                    exMessage = ex.InnerException.Message;
                MessageBox.Show(exMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            if (workbook != null)
            {
                if (sheetName.Trim() == "")
                    sheetName = "Sheet1";
                sheet = workbook.CreateSheet(sheetName);
            }
            else
            {
                MessageBox.Show("创建Sheet失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (isColumnWritten) //写入DataTable的列名
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
            try
            {
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
                if (IsShowExcel)
                {
                    DialogResult dr = MessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                        "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (File.Exists(fileName))
                        {
                            System.Diagnostics.Process.Start(fileName);
                        }
                    }
                }
                CloseStream(fs);
                return count;
            }
            catch (Exception ex)
            {
                CloseStream(fs);
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
        }

        public static DataTable Aspose2Data(string filePath)
        {
            //Aspose.Cells.License li = new Aspose.Cells.License();
            //li.SetLicense("Aspose.Cells.lic");
            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(filePath);
            Aspose.Cells.Worksheet ws = wk.Worksheets[0];
            return ws.Cells.ExportDataTable(1, 0, ws.Cells.Rows.Count, ws.Cells.Columns.Count);
        }

        private static void CloseStream(FileStream fs)
        {
            if (fs != null)
                fs.Close();
        }
    }
}
