namespace WindowsFormTelerik.CommonUI
{
    partial class LeftMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.office2010BlackTheme1 = new Telerik.WinControls.Themes.Office2010BlackTheme();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radListView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 567);
            this.panel1.TabIndex = 0;
            // 
            // radListView1
            // 
            this.radListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radListView1.Location = new System.Drawing.Point(0, 0);
            this.radListView1.Name = "radListView1";
            this.radListView1.Size = new System.Drawing.Size(249, 567);
            this.radListView1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LeftMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 567);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "LeftMenu";
            this.TabText = "菜单";
            this.Text = "菜单";
            this.Load += new System.EventHandler(this.LeftMenu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadListView radListView1;
        private System.Windows.Forms.ImageList imageList1;
        private Telerik.WinControls.Themes.Office2010BlackTheme office2010BlackTheme1;
    }
}