namespace MedicalSys.MSCommon
{
    partial class ImageButton
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
            this.lblTemp = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTemp
            // 
            this.lblTemp.BackColor = System.Drawing.Color.Transparent;
            this.lblTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemp.Location = new System.Drawing.Point(0, 0);
            this.lblTemp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(171, 50);
            this.lblTemp.TabIndex = 1;
            this.lblTemp.Text = "请输入显示的文本";
            this.lblTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTemp.TextChanged += new System.EventHandler(this.lblTemp_TextChanged);
            this.lblTemp.Click += new System.EventHandler(this.lblTemp_Click);
            this.lblTemp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTemp_MouseDown);
            this.lblTemp.MouseEnter += new System.EventHandler(this.lblTemp_MouseEnter);
            this.lblTemp.MouseLeave += new System.EventHandler(this.lblTemp_MouseLeave);
            this.lblTemp.MouseHover += new System.EventHandler(this.lblTemp_MouseHover);
            this.lblTemp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblTemp_MouseUp);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Margin = new System.Windows.Forms.Padding(4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(171, 50);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.BackgroundImageChanged += new System.EventHandler(this.pbImage_BackgroundImageChanged);
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            this.pbImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseDown);
            this.pbImage.MouseEnter += new System.EventHandler(this.pbImage_MouseEnter);
            this.pbImage.MouseLeave += new System.EventHandler(this.pbImage_MouseLeave);
            this.pbImage.MouseHover += new System.EventHandler(this.pbImage_MouseHover);
            this.pbImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseUp);
            // 
            // ImageButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.pbImage);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImageButton";
            this.Size = new System.Drawing.Size(171, 50);
            this.Load += new System.EventHandler(this.ImageButton_Load);
            this.MouseEnter += new System.EventHandler(this.ImageButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ImageButton_MouseLeave);
            this.Resize += new System.EventHandler(this.ImageButton_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblTemp;

    }
}
