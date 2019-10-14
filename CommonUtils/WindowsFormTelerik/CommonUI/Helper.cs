using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace WindowsFormTelerik.CommonUI
{
    public partial class Helper : Telerik.WinControls.UI.RadForm
    {
        public Helper()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void Helper_Load(object sender, EventArgs e)
        {
            linkLabel1.Text = "www.figkey.com";
            linkLabel1.LinkVisited = true;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", linkLabel1.Text);
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
