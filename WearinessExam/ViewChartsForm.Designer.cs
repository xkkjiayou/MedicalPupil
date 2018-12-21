namespace WearinessExam
{
    partial class ViewChartsForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdbCFF = new System.Windows.Forms.RadioButton();
            this.rdbPID = new System.Windows.Forms.RadioButton();
            this.rdbPMD = new System.Windows.Forms.RadioButton();
            this.rdbPCV = new System.Windows.Forms.RadioButton();
            this.rdbPCL = new System.Windows.Forms.RadioButton();
            this.rdbPCT = new System.Windows.Forms.RadioButton();
            this.rdbPCR = new System.Windows.Forms.RadioButton();
            this.rdbPCA = new System.Windows.Forms.RadioButton();
            this.rdbPCD = new System.Windows.Forms.RadioButton();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbPCD);
            this.panel1.Controls.Add(this.rdbPCA);
            this.panel1.Controls.Add(this.rdbPCR);
            this.panel1.Controls.Add(this.rdbPCT);
            this.panel1.Controls.Add(this.rdbPCL);
            this.panel1.Controls.Add(this.rdbPCV);
            this.panel1.Controls.Add(this.rdbPMD);
            this.panel1.Controls.Add(this.rdbPID);
            this.panel1.Controls.Add(this.rdbCFF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 447);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 73);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.zedGraphControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 384);
            this.panel2.TabIndex = 2;
            // 
            // rdbCFF
            // 
            this.rdbCFF.AutoSize = true;
            this.rdbCFF.Location = new System.Drawing.Point(54, 30);
            this.rdbCFF.Name = "rdbCFF";
            this.rdbCFF.Size = new System.Drawing.Size(41, 16);
            this.rdbCFF.TabIndex = 0;
            this.rdbCFF.TabStop = true;
            this.rdbCFF.Text = "CFF";
            this.rdbCFF.UseVisualStyleBackColor = true;
            this.rdbCFF.CheckedChanged += new System.EventHandler(this.rdbCFF_CheckedChanged);
            // 
            // rdbPID
            // 
            this.rdbPID.AutoSize = true;
            this.rdbPID.Location = new System.Drawing.Point(128, 30);
            this.rdbPID.Name = "rdbPID";
            this.rdbPID.Size = new System.Drawing.Size(41, 16);
            this.rdbPID.TabIndex = 1;
            this.rdbPID.TabStop = true;
            this.rdbPID.Text = "PID";
            this.rdbPID.UseVisualStyleBackColor = true;
            this.rdbPID.CheckedChanged += new System.EventHandler(this.rdbPID_CheckedChanged);
            // 
            // rdbPMD
            // 
            this.rdbPMD.AutoSize = true;
            this.rdbPMD.Location = new System.Drawing.Point(202, 30);
            this.rdbPMD.Name = "rdbPMD";
            this.rdbPMD.Size = new System.Drawing.Size(41, 16);
            this.rdbPMD.TabIndex = 2;
            this.rdbPMD.TabStop = true;
            this.rdbPMD.Text = "PMD";
            this.rdbPMD.UseVisualStyleBackColor = true;
            this.rdbPMD.CheckedChanged += new System.EventHandler(this.rdbPMD_CheckedChanged);
            // 
            // rdbPCV
            // 
            this.rdbPCV.AutoSize = true;
            this.rdbPCV.Location = new System.Drawing.Point(276, 30);
            this.rdbPCV.Name = "rdbPCV";
            this.rdbPCV.Size = new System.Drawing.Size(41, 16);
            this.rdbPCV.TabIndex = 3;
            this.rdbPCV.TabStop = true;
            this.rdbPCV.Text = "PCV";
            this.rdbPCV.UseVisualStyleBackColor = true;
            this.rdbPCV.CheckedChanged += new System.EventHandler(this.rdbPCV_CheckedChanged);
            // 
            // rdbPCL
            // 
            this.rdbPCL.AutoSize = true;
            this.rdbPCL.Location = new System.Drawing.Point(350, 30);
            this.rdbPCL.Name = "rdbPCL";
            this.rdbPCL.Size = new System.Drawing.Size(41, 16);
            this.rdbPCL.TabIndex = 4;
            this.rdbPCL.TabStop = true;
            this.rdbPCL.Text = "PCL";
            this.rdbPCL.UseVisualStyleBackColor = true;
            this.rdbPCL.CheckedChanged += new System.EventHandler(this.rdbPCL_CheckedChanged);
            // 
            // rdbPCT
            // 
            this.rdbPCT.AutoSize = true;
            this.rdbPCT.Location = new System.Drawing.Point(424, 30);
            this.rdbPCT.Name = "rdbPCT";
            this.rdbPCT.Size = new System.Drawing.Size(41, 16);
            this.rdbPCT.TabIndex = 5;
            this.rdbPCT.TabStop = true;
            this.rdbPCT.Text = "PCT";
            this.rdbPCT.UseVisualStyleBackColor = true;
            this.rdbPCT.CheckedChanged += new System.EventHandler(this.rdbPCT_CheckedChanged);
            // 
            // rdbPCR
            // 
            this.rdbPCR.AutoSize = true;
            this.rdbPCR.Location = new System.Drawing.Point(498, 30);
            this.rdbPCR.Name = "rdbPCR";
            this.rdbPCR.Size = new System.Drawing.Size(41, 16);
            this.rdbPCR.TabIndex = 6;
            this.rdbPCR.TabStop = true;
            this.rdbPCR.Text = "PCR";
            this.rdbPCR.UseVisualStyleBackColor = true;
            this.rdbPCR.CheckedChanged += new System.EventHandler(this.rdbPCR_CheckedChanged);
            // 
            // rdbPCA
            // 
            this.rdbPCA.AutoSize = true;
            this.rdbPCA.Location = new System.Drawing.Point(572, 30);
            this.rdbPCA.Name = "rdbPCA";
            this.rdbPCA.Size = new System.Drawing.Size(41, 16);
            this.rdbPCA.TabIndex = 7;
            this.rdbPCA.TabStop = true;
            this.rdbPCA.Text = "PCA";
            this.rdbPCA.UseVisualStyleBackColor = true;
            this.rdbPCA.CheckedChanged += new System.EventHandler(this.rdbPCA_CheckedChanged);
            // 
            // rdbPCD
            // 
            this.rdbPCD.AutoSize = true;
            this.rdbPCD.Location = new System.Drawing.Point(646, 30);
            this.rdbPCD.Name = "rdbPCD";
            this.rdbPCD.Size = new System.Drawing.Size(41, 16);
            this.rdbPCD.TabIndex = 8;
            this.rdbPCD.TabStop = true;
            this.rdbPCD.Text = "PCD";
            this.rdbPCD.UseVisualStyleBackColor = true;
            this.rdbPCD.CheckedChanged += new System.EventHandler(this.rdbPCD_CheckedChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(760, 384);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // ViewChartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(760, 520);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ViewChartsForm";
            this.Title = "查看曲线";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbPCD;
        private System.Windows.Forms.RadioButton rdbPCA;
        private System.Windows.Forms.RadioButton rdbPCR;
        private System.Windows.Forms.RadioButton rdbPCT;
        private System.Windows.Forms.RadioButton rdbPCL;
        private System.Windows.Forms.RadioButton rdbPCV;
        private System.Windows.Forms.RadioButton rdbPMD;
        private System.Windows.Forms.RadioButton rdbPID;
        private System.Windows.Forms.RadioButton rdbCFF;
        private System.Windows.Forms.Panel panel2;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }
}
