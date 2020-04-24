using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using System.Data;

namespace WindowsFormTelerik.GridViewExportData
{
    public class RadGridViewHelper
    {
        public static DataTable ConvertGridViewToDataTable(RadGridView gridView,int notIncludeFirstColIndex)
        {
            DataTable dt = new DataTable();
            if (gridView.ColumnCount < 1)
                return null;
            foreach (var col in gridView.Columns)
            {
                if (col.Index != notIncludeFirstColIndex)
                {
                    dt.Columns.Add(col.Name);
                }
            }
            if (gridView.RowCount < 1)
                return dt;
            foreach (var dataRow in gridView.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dataRow.Cells[i] == null)
                    {
                        dr[i] = "";
                    }
                    else
                    {
                        dr[i] = dataRow.Cells[i + 1].Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
