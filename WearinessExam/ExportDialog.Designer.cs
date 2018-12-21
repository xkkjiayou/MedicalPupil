namespace WearinessExam
{
    partial class ExportDialog
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
            this.label7 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new MedicalSys.MSCommon.ImageButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbOrignalData = new System.Windows.Forms.CheckBox();
            this.chbExamData = new System.Windows.Forms.CheckBox();
            this.chbBaseValue = new System.Windows.Forms.CheckBox();
            this.chbPatientInfo = new System.Windows.Forms.CheckBox();
            this.btnOK = new MedicalSys.MSCommon.ImageButton();
            this.btnCancel = new MedicalSys.MSCommon.ImageButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label7.Location = new System.Drawing.Point(38, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 27);
            this.label7.TabIndex = 33;
            this.label7.Text = "文件名";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPath.Location = new System.Drawing.Point(179, 34);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(411, 38);
            this.txtPath.TabIndex = 34;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectPath.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnSelectPath.ButtonText = "选 择";
            this.btnSelectPath.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectPath.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelectPath.Location = new System.Drawing.Point(630, 28);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectPath.MouseClickImage = null;
            this.btnSelectPath.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(150, 51);
            this.btnSelectPath.TabIndex = 35;
            this.btnSelectPath.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnSelectPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbOrignalData);
            this.groupBox1.Controls.Add(this.chbExamData);
            this.groupBox1.Controls.Add(this.chbBaseValue);
            this.groupBox1.Controls.Add(this.chbPatientInfo);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.groupBox1.Location = new System.Drawing.Point(30, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 150);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据类型";
            // 
            // chbOrignalData
            // 
            this.chbOrignalData.AutoSize = true;
            this.chbOrignalData.Location = new System.Drawing.Point(588, 75);
            this.chbOrignalData.Name = "chbOrignalData";
            this.chbOrignalData.Size = new System.Drawing.Size(146, 31);
            this.chbOrignalData.TabIndex = 3;
            this.chbOrignalData.Text = "原始数据";
            this.chbOrignalData.UseVisualStyleBackColor = true;
            // 
            // chbExamData
            // 
            this.chbExamData.AutoSize = true;
            this.chbExamData.Location = new System.Drawing.Point(405, 75);
            this.chbExamData.Name = "chbExamData";
            this.chbExamData.Size = new System.Drawing.Size(146, 31);
            this.chbExamData.TabIndex = 2;
            this.chbExamData.Text = "检测数据";
            this.chbExamData.UseVisualStyleBackColor = true;
            // 
            // chbBaseValue
            // 
            this.chbBaseValue.AutoSize = true;
            this.chbBaseValue.Location = new System.Drawing.Point(201, 75);
            this.chbBaseValue.Name = "chbBaseValue";
            this.chbBaseValue.Size = new System.Drawing.Size(174, 31);
            this.chbBaseValue.TabIndex = 1;
            this.chbBaseValue.Text = "基础值数据";
            this.chbBaseValue.UseVisualStyleBackColor = true;
            // 
            // chbPatientInfo
            // 
            this.chbPatientInfo.AutoSize = true;
            this.chbPatientInfo.Checked = true;
            this.chbPatientInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbPatientInfo.Location = new System.Drawing.Point(26, 75);
            this.chbPatientInfo.Name = "chbPatientInfo";
            this.chbPatientInfo.Size = new System.Drawing.Size(146, 31);
            this.chbPatientInfo.TabIndex = 0;
            this.chbPatientInfo.Text = "个人信息";
            this.chbPatientInfo.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnOK.ButtonText = "确 定";
            this.btnOK.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOK.Location = new System.Drawing.Point(241, 266);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.MouseClickImage = null;
            this.btnOK.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 51);
            this.btnOK.TabIndex = 37;
            this.btnOK.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnCancel.ButtonText = "取 消";
            this.btnCancel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(456, 266);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.MouseClickImage = null;
            this.btnCancel.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 51);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnCancel_Click);
            // 
            // ExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 339);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ExportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据导出";
            this.Load += new System.EventHandler(this.ExportDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPath;
        private MedicalSys.MSCommon.ImageButton btnSelectPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbOrignalData;
        private System.Windows.Forms.CheckBox chbExamData;
        private System.Windows.Forms.CheckBox chbBaseValue;
        private System.Windows.Forms.CheckBox chbPatientInfo;
        private MedicalSys.MSCommon.ImageButton btnOK;
        private MedicalSys.MSCommon.ImageButton btnCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}