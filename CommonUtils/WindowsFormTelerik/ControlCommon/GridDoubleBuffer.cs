using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Telerik.WinControls.UI;

namespace WindowsFormTelerik.ControlCommon
{
    public static class  GridDoubleBuffer
    {
        /// <summary>
        /// 将给定的DataGridView设置双缓冲
        /// </summary>
        /// <param name="dgv">给定的DataGridView</param>
        /// <param name="b">设置为ture即打开双缓冲</param>
        public static void SetDoubleBuffered(this RadGridView dgv, bool b)
        {
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, b, null);
        }
    }
}
