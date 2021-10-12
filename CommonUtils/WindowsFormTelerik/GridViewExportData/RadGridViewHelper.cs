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
        private void ListView1_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
            //e.CellElement.BackColor = ColorTranslator.FromHtml("#D3D7DE");
            e.CellElement.BorderBottomShadowColor = ColorTranslator.FromHtml("#D3D7DE");//列头底部线
            e.CellElement.BorderRightShadowColor = ColorTranslator.FromHtml("#D3D7DE");
        }

        public static DataTable ConvertGridViewToDataTable(RadGridView gridView, bool IsIncludeFirstCol)
        {
            DataTable dt = new DataTable();

            foreach (var col in gridView.Columns)
            {
                if (!IsIncludeFirstCol)//不包含第一列
                {
                    if (col.Index != 0)
                    {
                        dt.Columns.Add(col.Name);
                    }
                }
                else
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
                        if (IsIncludeFirstCol)
                        {
                            dr[i] = dataRow.Cells[i].Value;
                        }
                        else
                        {
                            dr[i] = dataRow.Cells[i + 1].Value;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
