using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Drawing;

namespace WindowsFormTelerik.ControlCommon
{
    public class FontRool
    {
        private delegate void FlushLabel(string text);
        private static int roll;
        private static int parentWidth;
        private static int lbxXPoint;
        private static int lbxYPoint;
        private static int lbxWidth;//要滚动的宽度
        private static int lbxInitWidth = 5;
        private static System.Timers.Timer timer;
        private static RadLabel radLabel;
        private static RoolDirection roolDirectionEnum;

        public enum RoolDirection
        {
            Left,
            Right
        }

        public static void FontAutoRool(RadLabel label, Panel panel, RoolDirection roolDirection)
        {
            radLabel = label;
            roolDirectionEnum = roolDirection;
            radLabel.Location = new Point(lbxInitWidth, label.Location.Y);
            parentWidth = panel.Width;
            lbxWidth = radLabel.Size.Width;
            lbxXPoint = radLabel.Location.X;
            lbxYPoint = radLabel.Location.Y;
            if (timer != null)
                timer.Stop();
            if (lbxWidth < parentWidth)
                return;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            timer.Interval = 30;
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (roolDirectionEnum == RoolDirection.Right)
            {
                radLabel.Location = new Point(lbxXPoint + roll, lbxYPoint);
                if (roll == lbxWidth)
                {
                    radLabel.Location = new Point(lbxInitWidth, lbxYPoint);
                    roll = 0;
                }
            }
            else if (roolDirectionEnum == RoolDirection.Left)
            {
                radLabel.Location = new Point(lbxXPoint - roll, lbxYPoint);
                if (roll == lbxWidth)
                {
                    radLabel.Location = new Point(lbxInitWidth, lbxYPoint);
                    roll = 0;
                }
            }
            roll++;
        }
    }
}
