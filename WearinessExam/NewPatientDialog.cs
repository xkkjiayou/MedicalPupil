using MedicalSys.DataAccessor;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WearinessExam
{
    public partial class NewPatientDialog : MedicalSys.MSCommon.BaseForm
    {
        /// <summary>
        /// The patient DAO
        /// </summary>
        private PatientDAO m_PatientDao = new PatientDAO();
        /// <summary>
        /// The user DAO
        /// </summary>
        private UserDAO m_UserDao = new UserDAO();
        /// <summary>
        /// The save status
        /// </summary>
        private bool m_SaveStatus = false;

        public NewPatientDialog()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void Init()
        {
            rbtMen.Checked = true;

        }

        
        /// <summary>
        /// Gets the patient info.
        /// </summary>
        /// <param name="patientID">The patient ID.</param>
        /// <returns>Patient.</returns>
        private Patient GetPatientInfo(string patientID)
        {
            // 根据PatientID，到Patient表里获取Patient信息
            Patient patient = new Patient();
            List<Patient> patientList = null;
            bool success = true;
            success = DataAccessProxy.Execute<Patient>(() => { return m_PatientDao.GetAll(); }, this, out patientList);
            if (success)
            {
                for (int i = 0; i < patientList.Count; i++)
                {
                    if (patientID.Equals(patientList[i].ID))
                    {
                        patient = patientList[i];
                        break;
                    }
                }
            }
            return patient;
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (VerifyInput() && IsModified() && VerifyPatientID())
            {
                bool patientSuccess = false;

                // 反馈式放松系统及以外的系统都需要登录Patient信息.
                Patient patient = GetPatientData();
                patientSuccess = DataAccessProxy.Execute(() => { m_PatientDao.Insert(patient); }, this);

                if (patientSuccess)
                {
                    m_SaveStatus = true;
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// Gets the patient data.
        /// </summary>
        /// <returns>Patient.</returns>
        private Patient GetPatientData()
        {
            int iSex = 1;
            if (rbtMen.Checked)
            {
                iSex = 1;
            }
            else
            {
                iSex = 2;
            }
            // 取得画面上的受测员信息
            Patient patient = new Patient()
            {
                Name = txtName.Text.Trim(),
                ID = txtID.Text.Trim(),
                Sex = iSex,
                Unit = txtUnit.Text.Trim(),
                Birth = Convert.ToDateTime(dtpBirth.Value),
            };
            return patient;
        }


        /// <summary>
        /// Determines whether this instance is modified.
        /// </summary>
        /// <returns><c>true</c> if this instance is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified()
        {
            bool result = false;

            result = !string.IsNullOrEmpty(txtID.Text)
                   || !string.IsNullOrEmpty(txtName.Text)
                   || !rbtMen.Checked
                   || !string.IsNullOrEmpty(txtUnit.Text)
                   || !string.IsNullOrEmpty(dtpBirth.Text);

            return result;
        }

        /// <summary>
        /// Verifies the input.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool VerifyInput()
        {
            bool result = false;

            result = !string.IsNullOrWhiteSpace(txtID.Text) &&
                     !string.IsNullOrWhiteSpace(txtName.Text) &&
                     !string.IsNullOrWhiteSpace(dtpBirth.Text);

            if (!result)
            {
                // 画面输入项目有任何一项为空时，弹出对话框。
                MessageBox.Show(this, CommonMessage.M0005, string.Empty,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
            }

            return result;
        }

        /// <summary>
        /// Verifies the user ID and login ID and patient ID.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool VerifyPatientID()
        {
            bool resultPatientID = false;
            bool patientSuccess = true;

            patientSuccess = DataAccessProxy.Execute(() => { return m_PatientDao.HasExistID(txtID.Text); }, this, out resultPatientID);

            if (patientSuccess && resultPatientID)
            {
                // UserID,LoginID,PatientID在DB为空时,弹出对话框.
                MessageBox.Show(this, CommonMessage.M0018, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return patientSuccess && !resultPatientID;
        }

        /// <summary>
        /// Handles the FormClosing event of the EditPatientDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void EditPatientDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SaveStatus)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (IsModified() && MessageBox.Show(this, CommonMessage.M0006, string.Empty,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Validating event of the dtpBirth control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpBirth_Validating(object sender, CancelEventArgs e)
        {
            // 出生日期已经输入时
            if (dtpBirth.Value != null)
            {
                DateTime dtBirthChange = Convert.ToDateTime(dtpBirth.Value);
                // 如果输入的出生日期比现在的时间大，弹出对话框
                if (dtBirthChange.CompareTo(System.DateTime.Now) > 0)
                {
                    MessageBox.Show(this, CommonMessage.M0017, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpBirth.Value = System.DateTime.Now;
                    e.Cancel = true;
                }
            }
        }

    }
}
