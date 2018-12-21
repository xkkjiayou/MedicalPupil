namespace WearinessExam
{
    partial class EditPatientDialog
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
            this.btnOk = new MedicalSys.MSCommon.ImageButton();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtWomen = new System.Windows.Forms.RadioButton();
            this.rbtMen = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnOk.ButtonText = "确 定";
            this.btnOk.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOk.Location = new System.Drawing.Point(290, 325);
            this.btnOk.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnOk.MouseClickImage = null;
            this.btnOk.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(178, 54);
            this.btnOk.TabIndex = 24;
            this.btnOk.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnOk_Click);
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnit.Location = new System.Drawing.Point(136, 263);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(2);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(530, 32);
            this.txtUnit.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label4.Location = new System.Drawing.Point(58, 266);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "单位";
            // 
            // dtpBirth
            // 
            this.dtpBirth.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.dtpBirth.CalendarTitleForeColor = System.Drawing.SystemColors.GrayText;
            this.dtpBirth.Font = new System.Drawing.Font("宋体", 18F);
            this.dtpBirth.Location = new System.Drawing.Point(454, 193);
            this.dtpBirth.Margin = new System.Windows.Forms.Padding(2);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(212, 35);
            this.dtpBirth.TabIndex = 21;
            this.dtpBirth.Validating += new System.ComponentModel.CancelEventHandler(this.dtpBirth_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label3.Location = new System.Drawing.Point(336, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "出生日期";
            // 
            // rbtWomen
            // 
            this.rbtWomen.AutoSize = true;
            this.rbtWomen.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.rbtWomen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.rbtWomen.Location = new System.Drawing.Point(236, 196);
            this.rbtWomen.Margin = new System.Windows.Forms.Padding(2);
            this.rbtWomen.Name = "rbtWomen";
            this.rbtWomen.Size = new System.Drawing.Size(53, 28);
            this.rbtWomen.TabIndex = 19;
            this.rbtWomen.TabStop = true;
            this.rbtWomen.Text = "女";
            this.rbtWomen.UseVisualStyleBackColor = true;
            // 
            // rbtMen
            // 
            this.rbtMen.AutoSize = true;
            this.rbtMen.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.rbtMen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.rbtMen.Location = new System.Drawing.Point(154, 196);
            this.rbtMen.Margin = new System.Windows.Forms.Padding(2);
            this.rbtMen.Name = "rbtMen";
            this.rbtMen.Size = new System.Drawing.Size(53, 28);
            this.rbtMen.TabIndex = 18;
            this.rbtMen.TabStop = true;
            this.rbtMen.Text = "男";
            this.rbtMen.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(58, 198);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "性别";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(454, 126);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(212, 32);
            this.txtName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(382, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "姓名";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtID.Location = new System.Drawing.Point(136, 126);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(190, 32);
            this.txtID.TabIndex = 14;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.lblId.Location = new System.Drawing.Point(81, 130);
            this.lblId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(36, 24);
            this.lblId.TabIndex = 13;
            this.lblId.Text = "ID";
            // 
            // EditPatientDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(737, 411);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpBirth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rbtWomen);
            this.Controls.Add(this.rbtMen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblId);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EditPatientDialog";
            this.Title = "编辑受测员信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditPatientDialog_FormClosing);
            this.Controls.SetChildIndex(this.lblId, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.rbtMen, 0);
            this.Controls.SetChildIndex(this.rbtWomen, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.dtpBirth, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtUnit, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalSys.MSCommon.ImageButton btnOk;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtWomen;
        private System.Windows.Forms.RadioButton rbtMen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblId;
    }
}
