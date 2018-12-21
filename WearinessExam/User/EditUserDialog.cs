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
    public partial class EditUserDialog : MedicalSys.MSCommon.BaseForm
    {
        private UserDAO userDao = new UserDAO();
        private bool m_SaveStatus = false;
        private string m_password = string.Empty;

        public EditUserDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets or sets the edited data.
        /// </summary>
        /// <value>The edited data.</value>
        private User EditedData
        {
            get;
            set;
        }

        /// <summary>
        /// Initials the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Initial(User user)
        {
            EditedData = user;
            txtAccout.Text = EditedData.LoginID;
            txtName.Text = EditedData.Name;
            //txtUserID.Text = EditedData.ID;
            chkAdmin.Checked = EditedData.Level == 1;
            if (LoginInfoManager.CurrentUser.Level > 1)
            {
                chkAdmin.Enabled = false;
            }
            m_password = user.Password;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            User user = GetData();
            if (IsModified(user))
            {
                if (VerifyInput() && VerifyPassword())
                {
                    //更新数据
                    bool success = DataAccessProxy.Execute(() => { userDao.Update(user); }, this);
                    if (success)
                    {
                        if (m_password == txtConfirmPWD.Text)
                        {
                            MessageBox.Show(this, CommonMessage.M0019, string.Empty,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        //如果是当前用户，更新LoginInfoManager.CurrentUser
                        if (user.LoginID == LoginInfoManager.CurrentUser.LoginID)
                        {
                            LoginInfoManager.CurrentUser = user;
                        }
                        m_SaveStatus = true;
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, CommonMessage.M0019, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
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
                primaryKey = EditedData.primaryKey,
                LoginID = txtAccout.Text.ToLower(),
                Name = txtName.Text.Trim(),
                //ID = txtUserID.Text.Trim().ToLower(),
                Password = string.IsNullOrEmpty(txtNewPWD.Text) ? EditedData.Password : MD5CrypHelper.GetMd5Hash(txtNewPWD.Text),
                Level = chkAdmin.Checked ? 1 : 2
            };

            return user;
        }

        /// <summary>
        /// Determines whether the specified user is modified.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the specified user is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified(User user)
        {
            return (user.Level != EditedData.Level) || (user.Name != EditedData.Name) || (user.ID != EditedData.ID)
                || !string.IsNullOrEmpty(txtNewPWD.Text) || !string.IsNullOrEmpty(txtConfirmPWD.Text);

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

            bool result = (string.IsNullOrEmpty(txtNewPWD.Text) &&
                    string.IsNullOrEmpty(txtConfirmPWD.Text)) || (!string.IsNullOrEmpty(txtNewPWD.Text) &&
                          !string.IsNullOrEmpty(txtConfirmPWD.Text) &&
                           string.Equals(txtNewPWD.Text, txtConfirmPWD.Text));

            if (!result)
            {
                MessageBox.Show(this, CommonMessage.M0013, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPWD.Focus();
            }
            return result;
        }


        /// <summary>
        /// Handles the FormClosing event of the EditUserDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs" /> instance containing the event data.</param>
        private void EditUserDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SaveStatus)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (IsModified(GetData()) && MessageBox.Show(this, CommonMessage.M0006, string.Empty,
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
    }
}
