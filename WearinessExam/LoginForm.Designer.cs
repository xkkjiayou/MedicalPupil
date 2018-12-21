namespace WearinessExam
{
    partial class LoginForm
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new MedicalSys.MSCommon.ImageButton();
            this.btnLogin = new MedicalSys.MSCommon.ImageButton();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(515, 595);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(13, 14);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox2.Location = new System.Drawing.Point(559, 520);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(297, 35);
            this.textBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox1.Location = new System.Drawing.Point(559, 440);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(297, 35);
            this.textBox1.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnExit.ButtonText = "退出|Exit";
            this.btnExit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Location = new System.Drawing.Point(698, 631);
            this.btnExit.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnExit.MouseClickImage = null;
            this.btnExit.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(162, 52);
            this.btnExit.TabIndex = 2;
            this.btnExit.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnLogin.ButtonText = "登录|Login";
            this.btnLogin.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.Location = new System.Drawing.Point(515, 631);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnLogin.MouseClickImage = null;
            this.btnLogin.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(162, 52);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnLogin_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WearinessExam.Properties.Resources.bg_login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoginForm";
            this.Text = "瞳孔分析仪软件系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MedicalSys.MSCommon.ImageButton btnLogin;
        private MedicalSys.MSCommon.ImageButton btnExit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}