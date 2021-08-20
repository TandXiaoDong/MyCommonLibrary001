using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormTelerik.ControlCommon
{
    class Wf_ListView:ListView
    {
        private ListView listView1;

        /// <summary>
        /// 双缓冲防止刷新时闪烁
        /// </summary>
        public Wf_ListView()
        {
            this.OwnerDraw = true;
            SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        #region listview test
        private void InitListView()
        {
            //this.listView1.ViewType = ListViewType.DetailsView;
            //this.listView1.FullRowSelect = true;
            //this.listView1.ShowGridLines = true;

            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;

            this.listView1.Columns.Add("Monitor ID");
            this.listView1.Columns.Add("Test ID");
            this.listView1.Columns.Add("Unit ID");
            this.listView1.Columns.Add("测试值");
            this.listView1.Columns.Add("最小值");
            this.listView1.Columns.Add("最大值");
            this.listView1.Columns.Add("单位");
            this.listView1.Columns.Add("注释");

            this.listView1.Columns[0].Width = this.listView1.Width / 10;
            this.listView1.Columns[1].Width = this.listView1.Width / 10;
            this.listView1.Columns[2].Width = this.listView1.Width / 10;
            this.listView1.Columns[3].Width = this.listView1.Width / 10;
            this.listView1.Columns[4].Width = this.listView1.Width / 10;
            this.listView1.Columns[5].Width = this.listView1.Width / 10;
            this.listView1.Columns[6].Width = this.listView1.Width / 10;
            this.listView1.Columns[7].Width = this.listView1.Width / 2;
        }

        private void LoadMode6Data(DataTable data)
        {
            if (this.listView1.Items.Count == data.Rows.Count)//替换数据
            {
                this.listView1.BeginUpdate();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    for (int j = 0; j < this.listView1.Columns.Count; j++)
                    {
                        this.listView1.Items[i].SubItems[j].Text = data.Rows[i][j].ToString();
                    }
                }
                this.listView1.EndUpdate();
            }
            else
            {
                this.listView1.Items.Clear();
                this.listView1.BeginUpdate();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem(data.Rows[i][0].ToString());
                    for (int j = 1; j < data.Columns.Count; j++)
                    {
                        lvi.SubItems.Add(data.Rows[i][j].ToString());
                    }
                    this.listView1.Items.Add(lvi);
                }
                this.listView1.EndUpdate();
            }
        }
        #endregion
    }
}
