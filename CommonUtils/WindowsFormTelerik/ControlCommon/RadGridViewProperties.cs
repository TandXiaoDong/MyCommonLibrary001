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
            Disqualification
        }
        public static void SetRadGridViewProperty(RadGridView gridView, bool allowAddNewRow,bool IsReadOnly,int columnCount)
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

        public static void SetRadGridViewStyle(RadGridView gridView,int columnCount,GridViewRecordEnum viewRecordEnum)
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
            if (columnCount > 0)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    gridView.Columns[i].ConditionalFormattingObjectList.Add(obj);
                }
            }
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
    }
}
