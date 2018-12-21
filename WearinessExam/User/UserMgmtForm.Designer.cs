namespace WearinessExam
{
    partial class UserMgmtForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.btnNew = new MedicalSys.MSCommon.ImageButton();
            this.btnSearch = new MedicalSys.MSCommon.ImageButton();
            this.btnDelete = new MedicalSys.MSCommon.ImageButton();
            this.btnEdit = new MedicalSys.MSCommon.ImageButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(346, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(436, 156);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 38);
            this.txtName.TabIndex = 6;
            // 
            // dgvUserList
            // 
            this.dgvUserList.AllowUserToAddRows = false;
            this.dgvUserList.AllowUserToDeleteRows = false;
            this.dgvUserList.AllowUserToResizeColumns = false;
            this.dgvUserList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUserList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUserList.ColumnHeadersHeight = 35;
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUserList.Location = new System.Drawing.Point(50, 233);
            this.dgvUserList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvUserList.MultiSelect = false;
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvUserList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUserList.RowTemplate.Height = 30;
            this.dgvUserList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserList.Size = new System.Drawing.Size(1062, 417);
            this.dgvUserList.TabIndex = 8;
            this.dgvUserList.TabStop = false;
            this.dgvUserList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellClick);
            this.dgvUserList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUserList_CellMouseDoubleClick);
            this.dgvUserList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellValueChanged);
            this.dgvUserList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvUserList_CurrentCellDirtyStateChanged);
            this.dgvUserList.Sorted += new System.EventHandler(this.dgvUserList_Sorted);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnNew.ButtonText = "新 建";
            this.btnNew.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNew.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNew.Location = new System.Drawing.Point(906, 145);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MouseClickImage = null;
            this.btnNew.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(206, 61);
            this.btnNew.TabIndex = 14;
            this.btnNew.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnSearch.ButtonText = "查 询";
            this.btnSearch.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(671, 145);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.MouseClickImage = null;
            this.btnSearch.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(213, 61);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnDelete.ButtonText = "删 除";
            this.btnDelete.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(906, 670);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MouseClickImage = null;
            this.btnDelete.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(213, 61);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnEdit.ButtonText = "编 辑";
            this.btnEdit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEdit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEdit.Location = new System.Drawing.Point(678, 671);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.MouseClickImage = null;
            this.btnEdit.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(213, 61);
            this.btnEdit.TabIndex = 18;
            this.btnEdit.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnEdit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(45, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 30);
            this.label2.TabIndex = 20;
            this.label2.Text = "登录名";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAccount.Location = new System.Drawing.Point(157, 156);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(160, 38);
            this.txtAccount.TabIndex = 21;
            // 
            // UserMgmtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1171, 757);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvUserList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "UserMgmtForm";
            this.Title = "用户管理";
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgvUserList, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnNew, 0);
            this.Controls.SetChildIndex(this.btnEdit, 0);
            this.Controls.SetChildIndex(this.btnDelete, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtAccount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.DataGridView dgvUserList;
        private MedicalSys.MSCommon.ImageButton btnNew;
        private MedicalSys.MSCommon.ImageButton btnSearch;
        private MedicalSys.MSCommon.ImageButton btnDelete;
        private MedicalSys.MSCommon.ImageButton btnEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccount;
    }
}
