using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormTelerik.GridViewExportData;

namespace TestFunc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("col1");
            dt.Columns.Add("col2");
            dt.Columns.Add("col3");

            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "col1_" + i;
                dr[1] = "col2_" + i;
                dr[2] = "col3_" + i;
                dt.Rows.Add(dr);
            }
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "testDemo001.xls";
            
            ExcelHelper2.DataTableToExcel(dt, "data", false, fileName, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "testDemo001.xls";
            DataTable dt = ExcelHelper2.ExcelToDataTable("", true, fileName);
            DataRow[] dataRows = dt.Select("col1 = '10' and col2 = '11'");
            MessageBox.Show(dataRows.Length + "");
        }
    }
}
