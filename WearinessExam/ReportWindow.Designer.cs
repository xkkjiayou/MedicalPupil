namespace WearinessExam
{
    partial class ReportWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewerMain = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkPrintList = new System.Windows.Forms.CheckBox();
            this.btnSave = new MedicalSys.MSCommon.ImageButton();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.txtConclusion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.btnExportPdf = new MedicalSys.MSCommon.ImageButton();
            this.btnPrint = new MedicalSys.MSCommon.ImageButton();
            this.dgvExamList = new System.Windows.Forms.DataGridView();
            this.reportViewerExamList = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DsExamDataListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.columnExamKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnExamTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsExamDataListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 118);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvExamList);
            this.splitContainer1.Size = new System.Drawing.Size(1359, 741);
            this.splitContainer1.SplitterDistance = 957;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reportViewerMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(957, 501);
            this.panel2.TabIndex = 1;
            // 
            // reportViewerMain
            // 
            this.reportViewerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerMain.DocumentMapWidth = 33;
            this.reportViewerMain.Location = new System.Drawing.Point(0, 0);
            this.reportViewerMain.Margin = new System.Windows.Forms.Padding(4);
            this.reportViewerMain.Name = "reportViewerMain";
            this.reportViewerMain.ShowBackButton = false;
            this.reportViewerMain.ShowDocumentMapButton = false;
            this.reportViewerMain.ShowExportButton = false;
            this.reportViewerMain.ShowFindControls = false;
            this.reportViewerMain.ShowPageNavigationControls = false;
            this.reportViewerMain.ShowPrintButton = false;
            this.reportViewerMain.ShowPromptAreaButton = false;
            this.reportViewerMain.ShowRefreshButton = false;
            this.reportViewerMain.ShowStopButton = false;
            this.reportViewerMain.ShowZoomControl = false;
            this.reportViewerMain.Size = new System.Drawing.Size(957, 501);
            this.reportViewerMain.TabIndex = 5;
            this.reportViewerMain.Load += new System.EventHandler(this.reportViewerMain_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPrintList);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.txtConclusion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblId);
            this.panel1.Controls.Add(this.btnExportPdf);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 501);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(957, 240);
            this.panel1.TabIndex = 0;
            // 
            // chkPrintList
            // 
            this.chkPrintList.AutoSize = true;
            this.chkPrintList.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold);
            this.chkPrintList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.chkPrintList.Location = new System.Drawing.Point(66, 181);
            this.chkPrintList.Name = "chkPrintList";
            this.chkPrintList.Size = new System.Drawing.Size(78, 26);
            this.chkPrintList.TabIndex = 19;
            this.chkPrintList.Text = "列表";
            this.chkPrintList.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnSave.ButtonText = "保 存";
            this.btnSave.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(799, 93);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MouseClickImage = null;
            this.btnSave.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(141, 52);
            this.btnSave.TabIndex = 18;
            this.btnSave.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnSave_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.txtMemo.Location = new System.Drawing.Point(423, 57);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(361, 88);
            this.txtMemo.TabIndex = 17;
            // 
            // txtConclusion
            // 
            this.txtConclusion.Font = new System.Drawing.Font("宋体", 12F);
            this.txtConclusion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.txtConclusion.Location = new System.Drawing.Point(28, 57);
            this.txtConclusion.Multiline = true;
            this.txtConclusion.Name = "txtConclusion";
            this.txtConclusion.Size = new System.Drawing.Size(361, 88);
            this.txtConclusion.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(419, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 22);
            this.label1.TabIndex = 15;
            this.label1.Text = "备注";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.lblId.Location = new System.Drawing.Point(24, 22);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(102, 22);
            this.lblId.TabIndex = 14;
            this.lblId.Text = "检测结论";
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.BackColor = System.Drawing.Color.Transparent;
            this.btnExportPdf.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnExportPdf.ButtonText = "导出PDF";
            this.btnExportPdf.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnExportPdf.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExportPdf.Location = new System.Drawing.Point(423, 165);
            this.btnExportPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportPdf.MouseClickImage = null;
            this.btnExportPdf.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(173, 52);
            this.btnExportPdf.TabIndex = 3;
            this.btnExportPdf.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnExportPdf_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnPrint.ButtonText = "打 印";
            this.btnPrint.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrint.Location = new System.Drawing.Point(216, 165);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.MouseClickImage = null;
            this.btnPrint.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(173, 52);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnPrint_Click);
            // 
            // dgvExamList
            // 
            this.dgvExamList.AllowUserToAddRows = false;
            this.dgvExamList.AllowUserToDeleteRows = false;
            this.dgvExamList.AllowUserToResizeColumns = false;
            this.dgvExamList.AllowUserToResizeRows = false;
            this.dgvExamList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExamList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExamList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExamList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnExamKey,
            this.columnId,
            this.columnName,
            this.columnExamTime});
            this.dgvExamList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExamList.Location = new System.Drawing.Point(0, 0);
            this.dgvExamList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvExamList.MultiSelect = false;
            this.dgvExamList.Name = "dgvExamList";
            this.dgvExamList.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvExamList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExamList.RowTemplate.Height = 23;
            this.dgvExamList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExamList.Size = new System.Drawing.Size(398, 741);
            this.dgvExamList.TabIndex = 4;
            this.dgvExamList.TabStop = false;
            // 
            // reportViewerExamList
            // 
            this.reportViewerExamList.DocumentMapWidth = 73;
            this.reportViewerExamList.Location = new System.Drawing.Point(227, 16);
            this.reportViewerExamList.Margin = new System.Windows.Forms.Padding(4);
            this.reportViewerExamList.Name = "reportViewerExamList";
            this.reportViewerExamList.Size = new System.Drawing.Size(45, 27);
            this.reportViewerExamList.TabIndex = 3;
            this.reportViewerExamList.Visible = false;
            // 
            // DsExamDataListBindingSource
            // 
            this.DsExamDataListBindingSource.DataMember = "EXAM";
            this.DsExamDataListBindingSource.DataSource = typeof(WearinessExam.Report.DsExamDataList);
            // 
            // columnExamKey
            // 
            this.columnExamKey.DataPropertyName = "ExamKey";
            this.columnExamKey.HeaderText = "ExamKey";
            this.columnExamKey.Name = "columnExamKey";
            this.columnExamKey.ReadOnly = true;
            this.columnExamKey.Visible = false;
            this.columnExamKey.Width = 5;
            // 
            // columnId
            // 
            this.columnId.DataPropertyName = "PatientId";
            this.columnId.HeaderText = "ID";
            this.columnId.Name = "columnId";
            this.columnId.ReadOnly = true;
            this.columnId.Width = 75;
            // 
            // columnName
            // 
            this.columnName.DataPropertyName = "PatientName";
            this.columnName.HeaderText = "姓名";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            this.columnName.Width = 95;
            // 
            // columnExamTime
            // 
            this.columnExamTime.DataPropertyName = "ExamTime";
            this.columnExamTime.HeaderText = "检测时间";
            this.columnExamTime.Name = "columnExamTime";
            this.columnExamTime.ReadOnly = true;
            this.columnExamTime.Width = 200;
            // 
            // ReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1359, 859);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportWindow";
            this.Title = "检测报告";
            this.Load += new System.EventHandler(this.ReportWindow_Load);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsExamDataListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerMain;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerExamList;
        private System.Windows.Forms.DataGridView dgvExamList;
        private MedicalSys.MSCommon.ImageButton btnExportPdf;
        private MedicalSys.MSCommon.ImageButton btnPrint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblId;
        private MedicalSys.MSCommon.ImageButton btnSave;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TextBox txtConclusion;
        private System.Windows.Forms.CheckBox chkPrintList;
        private System.Windows.Forms.BindingSource DsExamDataListBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExamKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnExamTime;
    }
}
