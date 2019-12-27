using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using WindowsFormTelerik.RegisterBind;

namespace WindowsFormsApp
{
    public partial class TryUse : RadForm
    {
        public TryUse()
        {
            InitializeComponent();
        }

        public static void Test()
        {
            int res = SoftRegister.InitRegedit();
            if (res == 0)
            {
                //Application.Run(new Form1());
                MessageBox.Show("软件尚已注册，可以试用软件！");
            }
            else if (res == 1)
            {
                MessageBox.Show("软件尚未注册，请注册软件！");
            }
            else if (res == 2)
            {
                MessageBox.Show("注册机器与本机不一致,请联系管理员！");
            }
            else if (res == 3)
            {
                MessageBox.Show("软件试用已到期！");
            }
            else
            {
                MessageBox.Show("软件运行出错，请重新启动！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
        }
    }
}
