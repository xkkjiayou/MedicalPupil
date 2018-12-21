namespace MedicalSys.MSCommon
{
    partial class BaseForm
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
            this.titlepanel = new System.Windows.Forms.Panel();
            this.titlelabel = new System.Windows.Forms.Label();
            this.button1 = new MedicalSys.MSCommon.ImageButton();
            this.titlepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titlepanel
            // 
            this.titlepanel.BackgroundImage = global::MSCommon.Properties.Resources.title;
            this.titlepanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.titlepanel.Controls.Add(this.titlelabel);
            this.titlepanel.Controls.Add(this.button1);
            this.titlepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlepanel.Location = new System.Drawing.Point(0, 0);
            this.titlepanel.Margin = new System.Windows.Forms.Padding(0);
            this.titlepanel.Name = "titlepanel";
            this.titlepanel.Size = new System.Drawing.Size(600, 63);
            this.titlepanel.TabIndex = 0;
            this.titlepanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Titlepanel_MouseDown);
            this.titlepanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Titlepanel_MouseMove);
            this.titlepanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Titlepanel_MouseUp);
            // 
            // titlelabel
            // 
            this.titlelabel.AutoSize = true;
            this.titlelabel.BackColor = System.Drawing.Color.Transparent;
            this.titlelabel.Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold);
            this.titlelabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.titlelabel.Location = new System.Drawing.Point(15, 20);
            this.titlelabel.Name = "titlelabel";
            this.titlelabel.Size = new System.Drawing.Size(0, 30);
            this.titlelabel.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.ButtonImage = global::MSCommon.Properties.Resources.close_default;
            this.button1.ButtonText = "";
            this.button1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(552, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.MouseClickImage = null;
            this.button1.MouseOverImage = global::MSCommon.Properties.Resources.close_click;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 0;
            this.button1.Tag = "close";
            this.button1.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.button1_Click);
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::MSCommon.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 480);
            this.ControlBox = false;
            this.Controls.Add(this.titlepanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.titlepanel.ResumeLayout(false);
            this.titlepanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel titlepanel;
        private ImageButton button1;
        private System.Windows.Forms.Label titlelabel;
    }
}

