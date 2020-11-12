using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.IO;
using System.Windows.Forms;
using System.Windows;
using System.Data;
using WindowsFormTelerik.FileHelper;

namespace WindowsFormTelerik.GridViewExportData
{
    public class GridViewExport
    {
        private static bool IsSavedColumns;
        public enum ExportFormat
        {
            EXCEL,
            HTML,
            PDF,
            CSV
        }

        #region 私有函数
        private static void RunExportToExcelML(string fileName, RadGridView radGridView)
        {
            ExportToExcelML excelExporter = new ExportToExcelML(radGridView);
            excelExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //excelExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //excelExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            excelExporter.HiddenColumnOption = HiddenOption.DoNotExport;
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                excelExporter.RunExport(fileName);

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
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                MessageBox.Show(ex.Message,"ERR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void RunExportToCSV(string fileName, RadGridView radGridView)
        {
            ExportToCSV csvExporter = new ExportToCSV(radGridView);
            csvExporter.CSVCellFormatting += csvExporter_CSVCellFormatting;
            csvExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //csvExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            csvExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                csvExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to CSV", MessageBoxButtons.YesNo, RadMessageIcon.Question);
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
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void csvExporter_CSVCellFormatting(object sender, Telerik.WinControls.UI.Export.CSV.CSVCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.CSVCellElement.Value = FormatDate(e.CSVCellElement.Value);
            }
        }

        private static string FormatDate(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.ToString("d MMM yyyy");
            }

            return value.ToString();
        }

        private static void RunExportToHTML(string fileName, RadGridView radGridView)
        {
            ExportToHTML htmlExporter = new ExportToHTML(radGridView);
            htmlExporter.HTMLCellFormatting += htmlExporter_HTMLCellFormatting;

            htmlExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //htmlExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //htmlExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            htmlExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                htmlExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to HTML", MessageBoxButtons.YesNo, RadMessageIcon.Question);
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
            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void htmlExporter_HTMLCellFormatting(object sender, Telerik.WinControls.UI.Export.HTML.HTMLCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.HTMLCellElement.Value = FormatDate(e.HTMLCellElement.Value);
            }
        }

        private static void RunExportToPDF(string fileName, RadGridView radGridView)
        {
            ExportToPDF pdfExporter = new ExportToPDF(radGridView);
            pdfExporter.PdfExportSettings.Title = "My PDF Title";
            pdfExporter.PdfExportSettings.PageWidth = 297;
            pdfExporter.PdfExportSettings.PageHeight = 210;
            pdfExporter.FitToPageWidth = true;
            pdfExporter.HTMLCellFormatting += pdfExporter_HTMLCellFormatting;

            pdfExporter.SummariesExportOption = SummariesOption.ExportAll;

            //set export settings
            //pdfExporter.ExportVisualSettings = this.radCheckBoxExportVisual.IsChecked;
            //pdfExporter.ExportHierarchy = this.radCheckBoxExportHierarchy.IsChecked;
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport;

            try
            {
                //this.Cursor = Cursors.WaitCursor;

                pdfExporter.RunExport(fileName);

                RadMessageBox.SetThemeName(radGridView.ThemeName);
                DialogResult dr = RadMessageBox.Show("The data in the grid was exported successfully. Do you want to open the file?",
                    "Export to PDF", MessageBoxButtons.YesNo, RadMessageIcon.Question);
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

            }
            catch (IOException ex)
            {
                RadMessageBox.SetThemeName(radGridView.ThemeName);
                //RadMessageBox.Show(this, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error);
                MessageBox.Show(ex.Message, "ERR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Cursor = Cursors.Default;
            }
        }

        private static void pdfExporter_HTMLCellFormatting(object sender, Telerik.WinControls.UI.Export.HTML.HTMLCellFormattingEventArgs e)
        {
            if (e.GridCellInfo.ColumnInfo is GridViewDateTimeColumn)
            {
                e.HTMLCellElement.Value = FormatDate(e.HTMLCellElement.Value);
            }
        }
        #endregion

        #region 调用

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="selectIndex"></param>
        /// <param name="radGridView"></param>
        public static void ExportGridViewData(ExportFormat exportFormat, RadGridView radGridView)
        {
            if (radGridView.RowCount < 1)
            {
                MessageBox.Show("没有可以导出的数据！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            var filter = "Excel (*.xls)|*.xls";
            if (exportFormat == ExportFormat.EXCEL)
            {
                filter = "Excel (*.xls)|*.xls";
                var path = FileSelect.SaveAs(filter, "C:\\");
                if (path == "")
                    return;
                GridViewExport.RunExportToExcelML(path, radGridView);
            }
            else if (exportFormat == ExportFormat.HTML)
            {
                filter = "Html File (*.htm)|*.htm";
                var path = FileSelect.SaveAs(filter, "C:\\");
                if (path == "")
                    return;
                GridViewExport.RunExportToHTML(path, radGridView);
            }
            else if (exportFormat == ExportFormat.PDF)
            {
                filter = "PDF file (*.pdf)|*.pdf";
                var path = FileSelect.SaveAs(filter, "C:\\");
                if (path == "")
                    return;
                GridViewExport.RunExportToPDF(path, radGridView);
            }
            else if (exportFormat == ExportFormat.CSV)
            {
                filter = "CSV file (*.csv)|*.csv";
                var path = FileSelect.SaveAs(filter, "C:\\");
                if (path == "")
                    return;
                GridViewExport.RunExportToCSV(path, radGridView);
            }
        }

        public static bool ImportToCSV(DataTable dt, string path)
        {
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("没有可以导出的数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return  false;
            }

            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.Default);
                string head = "";
                //拼接列头
                for (int cNum = 0; cNum < dt.Columns.Count; cNum++)
                {
                    head += dt.Columns[cNum].ColumnName + ",";
                }
                //csv文件写入列头
                sw.WriteLine(head);
                string data = "";
                //csv写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string data2 = string.Empty;
                    //拼接行数据
                    for (int cNum1 = 0; cNum1 < dt.Columns.Count; cNum1++)
                    {
                        data2 = data2 + "\"" + dt.Rows[i][dt.Columns[cNum1].ColumnName].ToString() + "\",";
                    }
                    bool flag = data != data2;
                    if (flag)
                    {
                        sw.WriteLine(data2);
                    }
                    data = data2;
                    return true;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("导出csv失败！" + ex.Message);
                return false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                sw = null;
                fs = null;
            }
            return true;
        }

        public static void ImportToCSV(List<string> channelData, string path)
        {
            if (channelData.Count == 0)
                return;
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (!Directory.Exists(fileInfo.Directory.FullName))
                {
                    Directory.CreateDirectory(fileInfo.Directory.FullName);
                }
                if (!File.Exists(path))
                    IsSavedColumns = false;
                fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.Default);
                //写入列头
                if (!IsSavedColumns)
                {
                    var columns = "dateTime,setVal,";
                    for (int i = 1; i <= 8; i++)
                    {
                        if (i < 8)
                        {
                            columns += $"ch{i}_tps1,ch{i}_tps2,ch{i}_current(mA),ch{i}_voltage(mV),";
                        }
                        else
                        {
                            columns += $"ch{i}_tps1,ch{i}_tps2,ch{i}_current(mA),ch{i}_voltage(mV)";
                        }
                    }
                    sw.WriteLine(columns);
                    IsSavedColumns = true;
                }
                //csv写入数据
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < channelData.Count; j++)
                {
                    if (j < channelData.Count - 1)
                    {
                        sb.Append(channelData[j] + ",");
                    }
                    else
                    {
                        sb.Append(channelData[j]);
                    }
                }
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                //Logh("导出csv失败！" + ex.Message);
                return;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                sw = null;
                fs = null;
            }
        }
        #endregion
    }
}
