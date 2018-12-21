namespace WearinessExam
{
    partial class EditUserDialog
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
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.txtConfirmPWD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNewPWD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnOk.ButtonText = "确 定";
            this.btnOk.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOk.Location = new System.Drawing.Point(363, 394);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.MouseClickImage = null;
            this.btnOk.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(237, 68);
            this.btnOk.TabIndex = 41;
            this.btnOk.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnOk_Click);
            // 
            // chkAdmin
            // 
            this.chkAdmin.AutoSize = true;
            this.chkAdmin.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.chkAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.chkAdmin.Location = new System.Drawing.Point(410, 330);
            this.chkAdmin.Name = "chkAdmin";
            this.chkAdmin.Size = new System.Drawing.Size(128, 34);
            this.chkAdmin.TabIndex = 40;
            this.chkAdmin.Text = "管理员";
            this.chkAdmin.UseVisualStyleBackColor = true;
            // 
            // txtConfirmPWD
            // 
            this.txtConfirmPWD.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConfirmPWD.Location = new System.Drawing.Point(639, 238);
            this.txtConfirmPWD.Name = "txtConfirmPWD";
            this.txtConfirmPWD.PasswordChar = '*';
            this.txtConfirmPWD.Size = new System.Drawing.Size(199, 38);
            this.txtConfirmPWD.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label4.Location = new System.Drawing.Point(485, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 30);
            this.label4.TabIndex = 38;
            this.label4.Text = "确认密码";
            // 
            // txtNewPWD
            // 
            this.txtNewPWD.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNewPWD.Location = new System.Drawing.Point(234, 238);
            this.txtNewPWD.Name = "txtNewPWD";
            this.txtNewPWD.PasswordChar = '*';
            this.txtNewPWD.Size = new System.Drawing.Size(199, 38);
            this.txtNewPWD.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label3.Location = new System.Drawing.Point(147, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 30);
            this.label3.TabIndex = 36;
            this.label3.Text = "密码";
            // 
            // txtAccout
            // 
            this.txtAccout.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAccout.Location = new System.Drawing.Point(234, 160);
            this.txtAccout.Name = "txtAccout";
            this.txtAccout.ReadOnly = true;
            this.txtAccout.Size = new System.Drawing.Size(199, 38);
            this.txtAccout.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(116, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 30);
            this.label2.TabIndex = 34;
            this.label2.Text = "登录名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(547, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 30);
            this.label1.TabIndex = 33;
            this.label1.Text = "姓名";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(639, 160);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(199, 38);
            this.txtName.TabIndex = 32;
            // 
            // EditUserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(956, 495);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkAdmin);
            this.Controls.Add(this.txtConfirmPWD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNewPWD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAccout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "EditUserDialog";
            this.Title = "编辑用户信息";
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtAccout, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtNewPWD, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtConfirmPWD, 0);
            this.Controls.SetChildIndex(this.chkAdmin, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MedicalSys.MSCommon.ImageButton btnOk;
        private System.Windows.Forms.CheckBox chkAdmin;
        private System.Windows.Forms.TextBox txtConfirmPWD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNewPWD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
    }
}
