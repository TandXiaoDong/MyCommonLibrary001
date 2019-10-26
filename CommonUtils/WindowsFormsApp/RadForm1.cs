using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using WeifenLuo.WinFormsUI.Docking;
using WindowsFormTelerik.CommonUI;

namespace WindowsFormsApp
{
    public partial class RadForm1 : Telerik.WinControls.UI.RadForm
    {
        public RadForm1()
        {
            InitializeComponent();
        }

        private void RadForm1_Load(object sender, EventArgs e)
        {
            DockPanel dockPanel = new DockPanel();
            dockPanel.Dock = DockStyle.Fill;
            this.Controls.Add(dockPanel);

            //this.IsMdiContainer = true;
            LeftMenu authorithManager = new LeftMenu(dockPanel);
            authorithManager.Show(dockPanel, DockState.DockLeft);
            authorithManager.BackColor = Color.Black;
        }
    }
}
