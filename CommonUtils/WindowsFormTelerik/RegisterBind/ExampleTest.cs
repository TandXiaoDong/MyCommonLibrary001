using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormTelerik.RegisterBind
{
    class ExampleTest
    {
        public static void Test()
        {
            int res = SoftRegister.InitRegedit();
            if (res == 0)
            {
                //Application.Run(new Form1());
            }
            else if (res == 1)
            {
                //MessageBox.Show("软件尚未注册，请注册软件！");
            }
            else if (res == 2)
            {
                //MessageBox.Show("注册机器与本机不一致,请联系管理员！");
            }
            else if (res == 3)
            {
                //MessageBox.Show("软件试用已到期！");
            }
            else
            {
                //MessageBox.Show("软件运行出错，请重新启动！");
            }
        }
    }
}
