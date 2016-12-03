namespace ForInstall
{
    partial class ForInstall
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ShuMin = new System.Windows.Forms.Label();
            this.BTN_Install = new System.Windows.Forms.Button();
            this.Notice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ShuMin
            // 
            this.ShuMin.AutoSize = true;
            this.ShuMin.Location = new System.Drawing.Point(12, 55);
            this.ShuMin.Name = "ShuMin";
            this.ShuMin.Size = new System.Drawing.Size(41, 12);
            this.ShuMin.TabIndex = 0;
            this.ShuMin.Text = "说明：";
            // 
            // BTN_Install
            // 
            this.BTN_Install.Location = new System.Drawing.Point(120, 15);
            this.BTN_Install.Name = "BTN_Install";
            this.BTN_Install.Size = new System.Drawing.Size(75, 25);
            this.BTN_Install.TabIndex = 1;
            this.BTN_Install.Text = "安装";
            this.BTN_Install.UseVisualStyleBackColor = true;
            this.BTN_Install.Click += new System.EventHandler(this.BTN_Install_Click);
            // 
            // Notice
            // 
            this.Notice.AutoSize = true;
            this.Notice.Location = new System.Drawing.Point(73, 55);
            this.Notice.Name = "Notice";
            this.Notice.Size = new System.Drawing.Size(125, 12);
            this.Notice.TabIndex = 2;
            this.Notice.Text = "请点击安装来安装程序";
            // 
            // ForInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 92);
            this.Controls.Add(this.Notice);
            this.Controls.Add(this.BTN_Install);
            this.Controls.Add(this.ShuMin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ForInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "键盘控制安装程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ShuMin;
        private System.Windows.Forms.Button BTN_Install;
        private System.Windows.Forms.Label Notice;
    }
}

