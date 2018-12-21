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
    public partial class EditPatientDialog : MedicalSys.MSCommon.BaseForm
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

        public EditPatientDialog()
        {
            InitializeComponent();
        }


        // 根据登录者的ID从DB取得的受测员信息
        /// <summary>
        /// Gets or sets the edited patient data.
        /// </summary>
        /// <value>The edited patient data.</value>
        private Patient EditedPatientData
        {
            get;
            set;
        }


        /// <summary>
        /// Inits this instance.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void Init(Patient patient)
        {
            EditedPatientData = patient;


            txtID.Text = EditedPatientData.ID;
            txtName.Text = EditedPatientData.Name;
            if (EditedPatientData.Sex == 1)
            {
                rbtMen.Checked = true;
            }
            else
            {
                rbtWomen.Checked = true;
            }
            txtUnit.Text = EditedPatientData.Unit;
            if (EditedPatientData.Birth != DateTime.MinValue)
            {
                dtpBirth.Value = EditedPatientData.Birth;
            }
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
            success = DataAccessProxy.Execute<Patient>(() => { return m_PatientDao.GetPatient(patientID); }, this, out patientList);
            if (success && patientList != null && patientList.Count != 0)
            {
                patient = patientList[0];
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
            if (IsModified(GetPatientData()))
            {
                if (VerifyInput())
                {

                    bool patientSuccess = true;

                    // 反馈式放松系统及以外的系统都需要更新Patient信息.
                    Patient patient = GetPatientData();
                    // 根据主键PatientKey来更新Patient信息
                    patientSuccess = DataAccessProxy.Execute(() => { m_PatientDao.Update(patient); }, this);

                    if (patientSuccess)
                    {
                        m_SaveStatus = true;
                        Close();
                    }
                }
            }
            else
            {
                Close();
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
                primaryKey = EditedPatientData.primaryKey,
                ID = txtID.Text.Trim(),
                Name = txtName.Text.Trim(),
                Sex = iSex,
                Unit = txtUnit.Text.Trim(),
                Birth = Convert.ToDateTime(dtpBirth.Value)
            };
            return patient;
        }


        /// <summary>
        /// Determines whether the specified user is modified.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns><c>true</c> if the specified user is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified(Patient patient)
        {
            bool result = false;

            result = !patient.Name.Equals(EditedPatientData.Name)
               || !patient.Sex.Equals(EditedPatientData.Sex)
               || !patient.Unit.Equals(EditedPatientData.Unit)
               || !patient.Birth.Equals(EditedPatientData.Birth);


            return result;
        }

        /// <summary>
        /// Verifies the input.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool VerifyInput()
        {
            bool result = false;

            result = !string.IsNullOrWhiteSpace(txtName.Text) &&
                     !string.IsNullOrWhiteSpace(dtpBirth.Text);

            if (!result)
            {
                // 画面输入项目有任何一项为空时，弹出对话框。
                MessageBox.Show(this, CommonMessage.M0005, string.Empty,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
            }

            return result;
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
            else if (IsModified(GetPatientData()) && MessageBox.Show(this, CommonMessage.M0006, string.Empty,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
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
                    dtpBirth.Value = DateTime.Now;
                    e.Cancel = true;
                }
            }
        }


    }
}
