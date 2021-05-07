using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using System.Drawing;

namespace WindowsFormTelerik.ControlCommon
{
    public class RadGridViewProperties
    {
        public enum GridViewRecordEnum
        {
            Normal,
            Qualification,
            Disqualification,
            Conduction,
            UnConduction,
            OpenCircuit,
            CurrentRow
        }
        public static void SetRadGridViewProperty(RadGridView gridView, bool allowAddNewRow, bool IsReadOnly, int columnCount)
        {
            gridView.EnableGrouping = false;
            gridView.AllowDrop = true;
            gridView.AllowRowReorder = true;
            //显示新行
            gridView.AddNewRowPosition = SystemRowPosition.Bottom;
            gridView.ShowRowHeaderColumn = true;
            gridView.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            //radGridView.AutoSizeRows = true;
            //gridView.MasterTemplate.BestFitColumns();
            //gridView.ColumnChooserSortOrder = RadSortOrder.Ascending;
            //dgv.AllowRowHeaderContextMenu = false;
            gridView.ShowGroupPanel = false;
            gridView.MasterTemplate.EnableGrouping = false;
            gridView.MasterTemplate.AllowAddNewRow = allowAddNewRow;
            gridView.EnableHotTracking = true;
            gridView.MasterTemplate.SelectLastAddedRow = false;
            gridView.ReadOnly = IsReadOnly;
            //radRadioDataReader.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            //this.radGridView1.CurrentRow = this.radGridView1.Rows[0];//设置某行为当前行
            //radGridView.Rows[0].Cells[0].Style.ForeColor;
            //gridView.AutoScroll = true;
            ConditionalFormattingObject obj = new ConditionalFormattingObject("myCondition", ConditionTypes.NotEqual, "", "", true);
            //obj.CellBackColor = Color.DeepSkyBlue;
            //obj.CellForeColor = Color.Red;
            obj.TextAlignment = ContentAlignment.MiddleCenter;
            if (columnCount > 0)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    gridView.Columns[i].ConditionalFormattingObjectList.Add(obj);
                }
            }
        }

        public static void SetGridViewRowStyle(RadGridView gridView, int colIndex, string content)
        {
            foreach (var rowInfo in gridView.Rows)
            {
                if (rowInfo.Cells[colIndex].Value.ToString() == content)
                {
                    ConditionalFormattingObject obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, content, "", true);
                    obj.CellBackColor = Color.LawnGreen;
                    obj.TextAlignment = ContentAlignment.MiddleCenter;
                    for (int i = 0; i < gridView.ColumnCount; i++)
                    {
                        gridView.Columns[i].ConditionalFormattingObjectList.Add(obj);//多线程中使用时，要释放已添加的对象，否则会导致内存增加问题
                    }
                }
            }
        }

        public static void SetRadGridViewStyle(RadGridView gridView, int columnCount, GridViewRecordEnum viewRecordEnum)
        {
            ConditionalFormattingObject obj = null;
            if (viewRecordEnum == GridViewRecordEnum.Normal)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "", "", true);
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.Qualification)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "合格", "", true);
                obj.CellBackColor = Color.LawnGreen;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.Disqualification)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "不合格", "", true);
                obj.CellBackColor = Color.Red;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.Conduction)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "导通", "", true);
                obj.CellBackColor = Color.LawnGreen;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.UnConduction)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "不导通", "", true);
                obj.CellBackColor = Color.Red;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.OpenCircuit)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Equal, "开路", "", true);
                obj.CellBackColor = Color.OrangeRed;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else if (viewRecordEnum == GridViewRecordEnum.CurrentRow)
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.NotEqual, "", "", true);
                obj.CellBackColor = Color.Red;
                obj.TextAlignment = ContentAlignment.MiddleCenter;
                for (int i = 0; i < gridView.ColumnCount; i++)
                {
                    gridView.Columns[i].ConditionalFormattingObjectList.Add(obj);
                }
            }
            gridView.Columns[columnCount].ConditionalFormattingObjectList.Add(obj);
        }

        public static void SetRadGridViewStyle(RadGridView gridView, int setType, int colIndex, double threshold, double curVal)
        {
            ConditionalFormattingObject obj = null;
            if (setType == 1)//正常颜色
            {
                obj = new ConditionalFormattingObject("myCondition", ConditionTypes.None, "", "", true);
                obj.TextAlignment = ContentAlignment.MiddleCenter;
            }
            else//错误提示
            {
                if (threshold > curVal)
                {
                    obj = new ConditionalFormattingObject("myCondition", ConditionTypes.Less, $"{threshold}", "", true);
                    obj.CellBackColor = Color.Red;
                    obj.TextAlignment = ContentAlignment.MiddleCenter;
                }
                else
                {
                    obj = new ConditionalFormattingObject("myCondition", ConditionTypes.None, "", "", true);
                    obj.TextAlignment = ContentAlignment.MiddleCenter;
                }
            }
            gridView.Columns[colIndex].ConditionalFormattingObjectList.Add(obj);
        }

        public static void ClearGridView(RadGridView radGridView, System.Data.DataTable data)
        {
            if (radGridView.RowCount < 1)
                return;
            for (int i = radGridView.RowCount - 1; i >= 0; i--)
            {
                radGridView.Rows[i].Delete();
            }
            if (data != null)
            {
                data.Rows.Clear();
                radGridView.DataSource = data;
            }
        }

        public static bool IsSelectRow(RadGridView radGrid)
        {
            if (radGrid.CurrentRow == null)
                return false;
            if (radGrid.CurrentRow.Index < 0)
                return false;
            return true;
        }
    }
}
