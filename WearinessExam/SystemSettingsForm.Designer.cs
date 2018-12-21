namespace WearinessExam
{
    partial class SystemSettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbInstalledPrinters = new System.Windows.Forms.ComboBox();
            this.btnProperty = new MedicalSys.MSCommon.ImageButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radList = new System.Windows.Forms.RadioButton();
            this.radReportAndList = new System.Windows.Forms.RadioButton();
            this.btnOK = new MedicalSys.MSCommon.ImageButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.lblStatus);
            this.groupBox1.Controls.Add(this.cmbInstalledPrinters);
            this.groupBox1.Controls.Add(this.btnProperty);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.groupBox1.Location = new System.Drawing.Point(32, 123);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(659, 284);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(128, 240);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 20);
            this.lblDescription.TabIndex = 47;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblLocation.Location = new System.Drawing.Point(128, 194);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(0, 20);
            this.lblLocation.TabIndex = 46;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(128, 147);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(0, 20);
            this.lblType.TabIndex = 45;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(128, 101);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 44;
            // 
            // cmbInstalledPrinters
            // 
            this.cmbInstalledPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInstalledPrinters.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbInstalledPrinters.FormattingEnabled = true;
            this.cmbInstalledPrinters.Location = new System.Drawing.Point(130, 47);
            this.cmbInstalledPrinters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbInstalledPrinters.Name = "cmbInstalledPrinters";
            this.cmbInstalledPrinters.Size = new System.Drawing.Size(399, 32);
            this.cmbInstalledPrinters.TabIndex = 43;
            this.cmbInstalledPrinters.SelectedIndexChanged += new System.EventHandler(this.cmbInstalledPrinters_SelectedIndexChanged);
            // 
            // btnProperty
            // 
            this.btnProperty.BackColor = System.Drawing.Color.Transparent;
            this.btnProperty.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnProperty.ButtonText = "属 性";
            this.btnProperty.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnProperty.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnProperty.Location = new System.Drawing.Point(545, 43);
            this.btnProperty.MouseClickImage = null;
            this.btnProperty.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new System.Drawing.Size(102, 39);
            this.btnProperty.TabIndex = 42;
            this.btnProperty.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnProperty_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label5.Location = new System.Drawing.Point(53, 236);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 24);
            this.label5.TabIndex = 39;
            this.label5.Text = "备注";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label4.Location = new System.Drawing.Point(53, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 24);
            this.label4.TabIndex = 38;
            this.label4.Text = "位置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label3.Location = new System.Drawing.Point(53, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 37;
            this.label3.Text = "类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label1.Location = new System.Drawing.Point(53, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "状态";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.label2.Location = new System.Drawing.Point(53, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 24);
            this.label2.TabIndex = 35;
            this.label2.Text = "名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radList);
            this.groupBox2.Controls.Add(this.radReportAndList);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(116)))), ((int)(((byte)(116)))));
            this.groupBox2.Location = new System.Drawing.Point(32, 428);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(659, 150);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "默认打印范围";
            // 
            // radList
            // 
            this.radList.AutoSize = true;
            this.radList.Location = new System.Drawing.Point(57, 93);
            this.radList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radList.Name = "radList";
            this.radList.Size = new System.Drawing.Size(103, 28);
            this.radList.TabIndex = 34;
            this.radList.TabStop = true;
            this.radList.Text = "仅报告";
            this.radList.UseVisualStyleBackColor = true;
            // 
            // radReportAndList
            // 
            this.radReportAndList.AutoSize = true;
            this.radReportAndList.Location = new System.Drawing.Point(57, 45);
            this.radReportAndList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radReportAndList.Name = "radReportAndList";
            this.radReportAndList.Size = new System.Drawing.Size(153, 28);
            this.radReportAndList.TabIndex = 33;
            this.radReportAndList.TabStop = true;
            this.radReportAndList.Text = "报告及列表";
            this.radReportAndList.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.ButtonImage = global::WearinessExam.Properties.Resources.btn_login;
            this.btnOK.ButtonText = "确 定";
            this.btnOK.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOK.Location = new System.Drawing.Point(233, 595);
            this.btnOK.MouseClickImage = null;
            this.btnOK.MouseOverImage = global::WearinessExam.Properties.Resources.btn_login2;
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(132, 39);
            this.btnOK.TabIndex = 43;
            this.btnOK.Click += new MedicalSys.MSCommon.ImageButton.ClickEventHandler(this.btnOK_Click);
            // 
            // SystemSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(721, 654);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SystemSettingsForm";
            this.Title = "系统设置";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MedicalSys.MSCommon.ImageButton btnProperty;
        private System.Windows.Forms.ComboBox cmbInstalledPrinters;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radList;
        private System.Windows.Forms.RadioButton radReportAndList;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblStatus;
        private MedicalSys.MSCommon.ImageButton btnOK;
    }
}
