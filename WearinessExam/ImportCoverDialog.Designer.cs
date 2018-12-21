namespace WearinessExam
{
    partial class ImportCoverDialog
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
            this.chbAllSame = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblMessage);
            this.splitContainer1.Panel1.Controls.Add(this.chbAllSame);
            this.splitContainer1.Size = new System.Drawing.Size(463, 249);
            this.splitContainer1.SplitterDistance = 163;
            // 
            // chbAllSame
            // 
            this.chbAllSame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chbAllSame.AutoSize = true;
            this.chbAllSame.Font = new System.Drawing.Font("SimSun", 10F);
            this.chbAllSame.Location = new System.Drawing.Point(62, 90);
            this.chbAllSame.Name = "chbAllSame";
            this.chbAllSame.Size = new System.Drawing.Size(166, 18);
            this.chbAllSame.TabIndex = 0;
            this.chbAllSame.Text = "对所有数据做相同处理";
            this.chbAllSame.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblMessage.Location = new System.Drawing.Point(51, 53);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(352, 16);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "该受测员的基础值数据已经设置，确定要覆盖吗?";
            // 
            // ImportCoverDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(463, 249);
            this.Name = "ImportCoverDialog";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chbAllSame;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
