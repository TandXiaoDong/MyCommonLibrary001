namespace WindowsFormTelerik.CommonUI
{
    partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.radPanorama1 = new Telerik.WinControls.UI.RadPanorama();
            this.lable_companyCopyRight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lable_version = new System.Windows.Forms.Label();
            this.lable_Title = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).BeginInit();
            this.radPanorama1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lable_Title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanorama1
            // 
            this.radPanorama1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radPanorama1.Controls.Add(this.lable_companyCopyRight);
            this.radPanorama1.Controls.Add(this.label2);
            this.radPanorama1.Controls.Add(this.lable_version);
            this.radPanorama1.Controls.Add(this.lable_Title);
            this.radPanorama1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanorama1.Location = new System.Drawing.Point(0, 0);
            this.radPanorama1.Name = "radPanorama1";
            this.radPanorama1.Size = new System.Drawing.Size(492, 324);
            this.radPanorama1.TabIndex = 0;
            // 
            // lable_companyCopyRight
            // 
            this.lable_companyCopyRight.AutoSize = true;
            this.lable_companyCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lable_companyCopyRight.Location = new System.Drawing.Point(28, 302);
            this.lable_companyCopyRight.Name = "lable_companyCopyRight";
            this.lable_companyCopyRight.Size = new System.Drawing.Size(290, 13);
            this.lable_companyCopyRight.TabIndex = 3;
            this.lable_companyCopyRight.Text = "Copyright (c)  2019  丰柯电子科技（上海）有限公司";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(3, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // lable_version
            // 
            this.lable_version.AutoSize = true;
            this.lable_version.BackColor = System.Drawing.Color.Transparent;
            this.lable_version.Location = new System.Drawing.Point(340, 98);
            this.lable_version.Name = "lable_version";
            this.lable_version.Size = new System.Drawing.Size(54, 13);
            this.lable_version.TabIndex = 1;
            this.lable_version.Text = "v 1.0.2.20";
            // 
            // lable_Title
            // 
            this.lable_Title.BackColor = System.Drawing.Color.Transparent;
            this.lable_Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable_Title.Location = new System.Drawing.Point(122, 90);
            this.lable_Title.Name = "lable_Title";
            this.lable_Title.Size = new System.Drawing.Size(212, 25);
            this.lable_Title.TabIndex = 0;
            this.lable_Title.Text = "万通智控产线追溯MES系统";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 324);
            this.Controls.Add(this.radPanorama1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WelcomeForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "";
            ((System.ComponentModel.ISupportInitialize)(this.radPanorama1)).EndInit();
            this.radPanorama1.ResumeLayout(false);
            this.radPanorama1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lable_Title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanorama radPanorama1;
        private Telerik.WinControls.UI.RadLabel lable_Title;
        private System.Windows.Forms.Label lable_version;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lable_companyCopyRight;
    }
}
