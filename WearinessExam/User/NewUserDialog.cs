using MedicalSys.DataAccessor;
using MedicalSys.Framework;
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
    public partial class NewUserDialog : MedicalSys.MSCommon.BaseForm
    {

        private UserDAO userDao = new UserDAO();
        private bool m_SaveStatus = false;

        public NewUserDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (VerifyInput() && VerifyPassword() && VerifyUserIDAndAccount())
            {
                User user = GetData();
                bool success = DataAccessProxy.Execute(() => { userDao.Insert(user); }, this);
                if (success)
                {
                    m_SaveStatus = true;
                    this.DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>User.</returns>
        private User GetData()
        {
            User user = new User()
            {
                LoginID = txtAccout.Text.Trim().ToLower(),
                Name = txtName.Text.Trim(),
                //ID = txtUserID.Text.Trim().ToLower(),
                Password = MD5CrypHelper.GetMd5Hash(txtNewPWD.Text),
                Level = chkAdmin.Checked ? 1 : 2
            };
            return user;
        }

        /// <summary>
        /// Verifies the input.
        /// </summary>
        /// <returns><c>true</c> if input verification pass, <c>false</c> otherwise</returns>
        private bool VerifyInput()
        {
            bool result = true;

            TextBox emptyItem = null;
            if (string.IsNullOrWhiteSpace(txtAccout.Text))
            {
                emptyItem = txtAccout;
                result = false;
            }
            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                emptyItem = txtName;
                result = false;
            }
            //else if (string.IsNullOrWhiteSpace(txtUserID.Text))
            //{
            //    emptyItem = txtUserID;
            //    result = false;
            //}
            else if (string.IsNullOrEmpty(txtNewPWD.Text) && string.IsNullOrEmpty(txtConfirmPWD.Text))
            {
                emptyItem = txtNewPWD;
                result = false;
            }

            if (!result)
            {
                MessageBox.Show(this, CommonMessage.M0005, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                emptyItem.Focus();
            }
            return result;




        }

        private bool VerifyPassword()
        {
            bool result = string.Equals(txtNewPWD.Text, txtConfirmPWD.Text);

            if (!result)
            {
                MessageBox.Show(this, CommonMessage.M0013, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPWD.Focus();
            }
            return result;
        }


        /// <summary>
        /// Verifies the user ID and account.
        /// </summary>
        /// <returns><c>true</c> if UserID and account does not exist, <c>false</c> otherwise</returns>
        private bool VerifyUserIDAndAccount()
        {
            bool result = false;

            bool success = DataAccessProxy.Execute(() => { return userDao.HasExistLoginID(txtAccout.Text.Trim().ToLower()); }, this, out result);
            if (success && result)
            {
                if (result)
                {

                    MessageBox.Show(this, CommonMessage.M0014, string.Empty,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAccout.Focus();
                }

            }
            return success && !result;
        }


        /// <summary>
        /// Handles the FormClosing event of the NewUserDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void NewUserDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SaveStatus)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (IsModified() && MessageBox.Show(this, CommonMessage.M0006, string.Empty,
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
        /// Determines whether this instance is modified.
        /// </summary>
        /// <returns><c>true</c> if this instance is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified()
        {
            return !string.IsNullOrEmpty(txtAccout.Text) || !string.IsNullOrEmpty(txtName.Text) 
                || !string.IsNullOrEmpty(txtNewPWD.Text) || !string.IsNullOrEmpty(txtConfirmPWD.Text);
        }


    }
}
