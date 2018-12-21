namespace WearinessExam
{
    partial class ChartsWindow
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new MedicalSys.MSCommon.ImageButton();
            this.btnLogin = new MedicalSys.MSCommon.ImageButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCFF = new System.Windows.Forms.TabPage();
            this.cgCFF = new MedicalSys.MSCommon.CurveGraphics();
            this.tabPR = new System.Windows.Forms.TabPage();
            this.cgPCL = new MedicalSys.MSCommon.CurveGraphics();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCFF.SuspendLayout();
            this.tabPR.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 434);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 80);
            this.panel1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnExit.ButtonText = "关 闭";
            this.btnExit.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Location = new System.Drawing.Point(326, 14);
            this.btnExit.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnExit.MouseClickImage = null;
            this.btnExit.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(179, 49);
            this.btnExit.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.ButtonImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnLogin.ButtonText = "另存为图片";
            this.btnLogin.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.Location = new System.Drawing.Point(110, 14);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnLogin.MouseClickImage = null;
            this.btnLogin.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(179, 49);
            this.btnLogin.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 371);
            this.panel2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCFF);
            this.tabControl1.Controls.Add(this.tabPR);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(631, 371);
            this.tabControl1.TabIndex = 5;
            // 
            // tabCFF
            // 
            this.tabCFF.Controls.Add(this.cgCFF);
            this.tabCFF.Location = new System.Drawing.Point(4, 26);
            this.tabCFF.Name = "tabCFF";
            this.tabCFF.Padding = new System.Windows.Forms.Padding(3);
            this.tabCFF.Size = new System.Drawing.Size(623, 341);
            this.tabCFF.TabIndex = 0;
            this.tabCFF.Text = "闪光融合频率曲线";
            this.tabCFF.UseVisualStyleBackColor = true;
            // 
            // cgCFF
            // 
            this.cgCFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgCFF.IsEnableHZoom = false;
            this.cgCFF.IsEnableVZoom = false;
            this.cgCFF.IsShowContextMenu = false;
            this.cgCFF.IsShowGrid = false;
            this.cgCFF.IsShowPointValues = true;
            this.cgCFF.Location = new System.Drawing.Point(3, 3);
            this.cgCFF.Margin = new System.Windows.Forms.Padding(7);
            this.cgCFF.Name = "cgCFF";
            this.cgCFF.ScrollGrace = 0D;
            this.cgCFF.ScrollMaxX = 0D;
            this.cgCFF.ScrollMaxY = 0D;
            this.cgCFF.ScrollMaxY2 = 0D;
            this.cgCFF.ScrollMinX = 0D;
            this.cgCFF.ScrollMinY = 0D;
            this.cgCFF.ScrollMinY2 = 0D;
            this.cgCFF.Size = new System.Drawing.Size(617, 335);
            this.cgCFF.TabIndex = 1;
            // 
            // tabPR
            // 
            this.tabPR.Controls.Add(this.cgPCL);
            this.tabPR.Location = new System.Drawing.Point(4, 26);
            this.tabPR.Name = "tabPR";
            this.tabPR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPR.Size = new System.Drawing.Size(623, 341);
            this.tabPR.TabIndex = 2;
            this.tabPR.Text = "瞳孔对光反应曲线";
            this.tabPR.UseVisualStyleBackColor = true;
            // 
            // cgPCL
            // 
            this.cgPCL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cgPCL.IsEnableHZoom = false;
            this.cgPCL.IsEnableVZoom = false;
            this.cgPCL.IsShowContextMenu = false;
            this.cgPCL.IsShowGrid = false;
            this.cgPCL.IsShowPointValues = true;
            this.cgPCL.Location = new System.Drawing.Point(3, 3);
            this.cgPCL.Margin = new System.Windows.Forms.Padding(5);
            this.cgPCL.Name = "cgPCL";
            this.cgPCL.ScrollGrace = 0D;
            this.cgPCL.ScrollMaxX = 0D;
            this.cgPCL.ScrollMaxY = 0D;
            this.cgPCL.ScrollMaxY2 = 0D;
            this.cgPCL.ScrollMinX = 0D;
            this.cgPCL.ScrollMinY = 0D;
            this.cgPCL.ScrollMinY2 = 0D;
            this.cgPCL.Size = new System.Drawing.Size(617, 335);
            this.cgPCL.TabIndex = 1;
            // 
            // ChartsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(631, 514);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChartsWindow";
            this.Title = "闪光融合频率曲线";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabCFF.ResumeLayout(false);
            this.tabPR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MedicalSys.MSCommon.ImageButton btnLogin;
        private MedicalSys.MSCommon.ImageButton btnExit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCFF;
        private MedicalSys.MSCommon.CurveGraphics cgCFF;
        private System.Windows.Forms.TabPage tabPR;
        private MedicalSys.MSCommon.CurveGraphics cgPCL;
    }
}
