namespace WearinessExam
{
    partial class PatientMgmtForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtAgeStart = new System.Windows.Forms.TextBox();
            this.txtAgeEnd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch = new MedicalSys.MSCommon.ImageButton();
            this.btnNew = new MedicalSys.MSCommon.ImageButton();
            this.btnSelect = new MedicalSys.MSCommon.ImageButton();
            this.btnEdit = new MedicalSys.MSCommon.ImageButton();
            this.btnDelete = new MedicalSys.MSCommon.ImageButton();
            this.dgvPatientList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(124, 152);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 38);
            this.txtName.TabIndex = 1;
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnit.Location = new System.Drawing.Point(416, 152);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(284, 38);
            this.txtUnit.TabIndex = 2;
            // 
            // txtAgeStart
            // 
            this.txtAgeStart.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgeStart.Location = new System.Drawing.Point(849, 152);
            this.txtAgeStart.Name = "txtAgeStart";
            this.txtAgeStart.Size = new System.Drawing.Size(122, 38);
            this.txtAgeStart.TabIndex = 3;
            // 
            // txtAgeEnd
            // 
            this.txtAgeEnd.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgeEnd.Location = new System.Drawing.Point(1061, 152);
            this.txtAgeEnd.Name = "txtAgeEnd";
            this.txtAgeEnd.Size = new System.Drawing.Size(126, 38);
            this.txtAgeEnd.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(40, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(324, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label3.Location = new System.Drawing.Point(741, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "年龄";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label4.Location = new System.Drawing.Point(996, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 30);
            this.label4.TabIndex = 8;
            this.label4.Text = "—";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnSearch.ButtonText = "查 询";
            this.btnSearch.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(743, 217);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.MouseClickImage = null;
            this.btnSearch.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(213, 61);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnSearch_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnNew.ButtonText = "新 建";
            this.btnNew.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNew.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNew.Location = new System.Drawing.Point(998, 217);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MouseClickImage = null;
            this.btnNew.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(206, 61);
            this.btnNew.TabIndex = 12;
            this.btnNew.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnNew_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.Transparent;
            this.btnSelect.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnSelect.ButtonText = "选 择";
            this.btnSelect.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelect.Location = new System.Drawing.Point(54, 796);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.MouseClickImage = null;
            this.btnSelect.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(213, 61);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnSelect_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnEdit.ButtonText = "编 辑";
            this.btnEdit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEdit.Location = new System.Drawing.Point(746, 796);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.MouseClickImage = null;
            this.btnEdit.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(213, 61);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnEidt_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnDelete.ButtonText = "删 除";
            this.btnDelete.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(991, 796);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MouseClickImage = null;
            this.btnDelete.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(213, 61);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnDelete_Click);
            // 
            // dgvPatientList
            // 
            this.dgvPatientList.AllowUserToAddRows = false;
            this.dgvPatientList.AllowUserToDeleteRows = false;
            this.dgvPatientList.AllowUserToResizeColumns = false;
            this.dgvPatientList.AllowUserToResizeRows = false;
            this.dgvPatientList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatientList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatientList.ColumnHeadersHeight = 38;
            this.dgvPatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPatientList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPatientList.Location = new System.Drawing.Point(40, 305);
            this.dgvPatientList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPatientList.Name = "dgvPatientList";
            this.dgvPatientList.RowHeadersVisible = false;
            this.dgvPatientList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvPatientList.RowTemplate.Height = 32;
            this.dgvPatientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatientList.Size = new System.Drawing.Size(1182, 468);
            this.dgvPatientList.TabIndex = 18;
            this.dgvPatientList.TabStop = false;
            this.dgvPatientList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPatientList_CellMouseDoubleClick);
            this.dgvPatientList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatientList_CellValueChanged);
            this.dgvPatientList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvPatientList_CurrentCellDirtyStateChanged);
            this.dgvPatientList.Sorted += new System.EventHandler(this.dgvUserList_Sorted);
            // 
            // PatientMgmtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1265, 877);
            this.Controls.Add(this.dgvPatientList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAgeEnd);
            this.Controls.Add(this.txtAgeStart);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtName);
            this.Name = "PatientMgmtForm";
            this.Title = "受测员管理";
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtUnit, 0);
            this.Controls.SetChildIndex(this.txtAgeStart, 0);
            this.Controls.SetChildIndex(this.txtAgeEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnNew, 0);
            this.Controls.SetChildIndex(this.btnSelect, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.dgvPatientList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtAgeStart;
        private System.Windows.Forms.TextBox txtAgeEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MedicalSys.MSCommon.ImageButton btnSearch;
        private MedicalSys.MSCommon.ImageButton btnNew;
        private MedicalSys.MSCommon.ImageButton btnSelect;
        private MedicalSys.MSCommon.ImageButton btnEdit;
        private MedicalSys.MSCommon.ImageButton btnDelete;
        private System.Windows.Forms.DataGridView dgvPatientList;
    }
}
