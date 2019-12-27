namespace dgvPage
{
    partial class dgvPageControl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lable1 = new System.Windows.Forms.Label();
            this.txtPageCount = new System.Windows.Forms.TextBox();
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbFirst = new System.Windows.Forms.Button();
            this.lbForward = new System.Windows.Forms.Button();
            this.lbNext_ = new System.Windows.Forms.Button();
            this.lbLast = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(177, 15);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(41, 12);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "每页：";
            // 
            // txtPageCount
            // 
            this.txtPageCount.Location = new System.Drawing.Point(215, 12);
            this.txtPageCount.Name = "txtPageCount";
            this.txtPageCount.Size = new System.Drawing.Size(29, 21);
            this.txtPageCount.TabIndex = 1;
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Location = new System.Drawing.Point(16, 15);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(89, 12);
            this.lbInfo.TabIndex = 2;
            this.lbInfo.Text = "当前|总共：1|1";
            // 
            // lbFirst
            // 
            this.lbFirst.Location = new System.Drawing.Point(335, 10);
            this.lbFirst.Name = "lbFirst";
            this.lbFirst.Size = new System.Drawing.Size(31, 23);
            this.lbFirst.TabIndex = 3;
            this.lbFirst.Text = "|<<";
            this.lbFirst.UseVisualStyleBackColor = true;
            this.lbFirst.Click += new System.EventHandler(this.lbFirst_Click);
            // 
            // lbForward
            // 
            this.lbForward.Location = new System.Drawing.Point(387, 10);
            this.lbForward.Name = "lbForward";
            this.lbForward.Size = new System.Drawing.Size(31, 23);
            this.lbForward.TabIndex = 4;
            this.lbForward.Text = "<<";
            this.lbForward.UseVisualStyleBackColor = true;
            this.lbForward.Click += new System.EventHandler(this.lbForward_Click);
            // 
            // lbNext_
            // 
            this.lbNext_.Location = new System.Drawing.Point(424, 10);
            this.lbNext_.Name = "lbNext_";
            this.lbNext_.Size = new System.Drawing.Size(31, 23);
            this.lbNext_.TabIndex = 5;
            this.lbNext_.Text = ">>";
            this.lbNext_.UseVisualStyleBackColor = true;
            this.lbNext_.Click += new System.EventHandler(this.lbNext__Click);
            // 
            // lbLast
            // 
            this.lbLast.Location = new System.Drawing.Point(475, 10);
            this.lbLast.Name = "lbLast";
            this.lbLast.Size = new System.Drawing.Size(31, 23);
            this.lbLast.TabIndex = 6;
            this.lbLast.Text = ">>|";
            this.lbLast.UseVisualStyleBackColor = true;
            this.lbLast.Click += new System.EventHandler(this.lbLast_Click);
            // 
            // dgvPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLast);
            this.Controls.Add(this.lbNext_);
            this.Controls.Add(this.lbForward);
            this.Controls.Add(this.lbFirst);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.txtPageCount);
            this.Controls.Add(this.lable1);
            this.Name = "dgvPageControl";
            this.Size = new System.Drawing.Size(530, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.TextBox txtPageCount;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button lbFirst;
        private System.Windows.Forms.Button lbForward;
        private System.Windows.Forms.Button lbNext_;
        private System.Windows.Forms.Button lbLast;
    }
}
