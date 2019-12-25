using System.Data;
using System.IO;
using System.Windows.Forms;
using System;
using System.Text;

namespace CommonUtils.SCVFile
{
    /// <summary>
    /// CSV文件转换类
    /// </summary>
    public static class CsvHelper
    {
        /// <summary>
        /// 导出报表为Csv
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strFilePath">物理路径</param>
        /// <param name="tableheader">表头</param>
        /// <param name="columname">字段标题,逗号分隔</param>
        public static bool dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string strBufferLine = "";
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveListViewSCV(ListView lv, string strFilePath, string tableheader, string columName)
        {
            try
            {
                if (lv.Items.Count < 1)
                {
                    MessageBox.Show("没有可以导出的数据！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false ;
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "xls文件 | *.xls";
                saveDialog.FileName = strFilePath;
                saveDialog.ShowDialog();
                var strFileName = saveDialog.FileName;
                if (string.IsNullOrEmpty(strFileName))
                    return false;
                if (File.Exists(strFileName))
                    File.Delete(strFileName);
                string strBufferLine = "";
                if (String.IsNullOrEmpty(strFilePath))
                    return false;
                StreamWriter strmWriterObj = new StreamWriter(strFileName, false, Encoding.UTF8);
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columName);
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < lv.Columns.Count; j++)
                    {
                        //if(j == 0)
                        //    strBufferLine.Append(lv.Items[i].Text);
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += lv.Items[i].SubItems[j].Text;
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将Csv读入DataTable
        /// </summary>
        /// <param name="filePath">csv文件路径</param>
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>
        public static DataTable csv2dt(string filePath, int n, DataTable dt)
        {
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    System.Data.DataRow dr = dt.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
