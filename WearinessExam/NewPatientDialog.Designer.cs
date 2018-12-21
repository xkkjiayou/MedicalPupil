namespace WearinessExam
{
    partial class NewPatientDialog
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
            this.lblId = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtMen = new System.Windows.Forms.RadioButton();
            this.rbtWomen = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpBirth = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.btnOk = new MedicalSys.MSCommon.ImageButton();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.lblId.Location = new System.Drawing.Point(118, 158);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(45, 30);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtID.Location = new System.Drawing.Point(191, 154);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(252, 38);
            this.txtID.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(520, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(616, 154);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(281, 38);
            this.txtName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(88, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "性别";
            // 
            // rbtMen
            // 
            this.rbtMen.AutoSize = true;
            this.rbtMen.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.rbtMen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.rbtMen.Location = new System.Drawing.Point(215, 241);
            this.rbtMen.Name = "rbtMen";
            this.rbtMen.Size = new System.Drawing.Size(65, 34);
            this.rbtMen.TabIndex = 6;
            this.rbtMen.TabStop = true;
            this.rbtMen.Text = "男";
            this.rbtMen.UseVisualStyleBackColor = true;
            // 
            // rbtWomen
            // 
            this.rbtWomen.AutoSize = true;
            this.rbtWomen.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.rbtWomen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.rbtWomen.Location = new System.Drawing.Point(324, 241);
            this.rbtWomen.Name = "rbtWomen";
            this.rbtWomen.Size = new System.Drawing.Size(65, 34);
            this.rbtWomen.TabIndex = 7;
            this.rbtWomen.TabStop = true;
            this.rbtWomen.Text = "女";
            this.rbtWomen.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label3.Location = new System.Drawing.Point(458, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "出生日期";
            // 
            // dtpBirth
            // 
            this.dtpBirth.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.dtpBirth.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.dtpBirth.Location = new System.Drawing.Point(616, 237);
            this.dtpBirth.Name = "dtpBirth";
            this.dtpBirth.Size = new System.Drawing.Size(281, 42);
            this.dtpBirth.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label4.Location = new System.Drawing.Point(88, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "单位";
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnit.Location = new System.Drawing.Point(191, 325);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(706, 38);
            this.txtUnit.TabIndex = 11;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnOk.ButtonText = "确 定";
            this.btnOk.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOk.Location = new System.Drawing.Point(397, 402);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.MouseClickImage = null;
            this.btnOk.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(237, 68);
            this.btnOk.TabIndex = 12;
            this.btnOk.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnOk_Click);
            // 
            // NewPatientDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1004, 514);
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
            this.Name = "NewPatientDialog";
            this.Title = "新建受测员";
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

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtMen;
        private System.Windows.Forms.RadioButton rbtWomen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpBirth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUnit;
        private MedicalSys.MSCommon.ImageButton btnOk;
    }
}
