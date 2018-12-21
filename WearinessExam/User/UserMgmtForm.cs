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
    public partial class UserMgmtForm : MedicalSys.MSCommon.BaseForm
    {
        private const string COLUMN_SELECTALL = "";
        private const string COLUMN_LOGIN_ID = "登录名";
        private const string COLUMN_NAME = "姓名";
        private const string COLUMN_ID = "管理员";
        private const string PROPERTYNAME_LOGIN_ID = "LoginID";
        private const string PROPERTYNAME_NAME = "Name";
        private const string PROPERTYNAME_ID = "USER_LEVEL";

        private List<User> m_UserList = null;
        private UserDAO m_UserDao = new UserDAO();

        DataGridViewCheckBoxHeaderCell m_HeaderCell;

        public UserMgmtForm()
        {
            InitializeComponent();
            btnDelete.Enabled = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }

        /// <summary>
        /// Initials this instance.
        /// </summary>
        public void Initial()
        {
            InitialGridView();
            InitialData();
        }

        /// <summary>
        /// Initials the grid view.
        /// </summary>
        private void InitialGridView()
        {
            dgvUserList.AutoGenerateColumns = false;
            dgvUserList.CellClick += new DataGridViewCellEventHandler(dgvUserList_CellClick);

            // Set the DataGridView control's border.
            dgvUserList.BorderStyle = BorderStyle.Fixed3D;

            // Initialize and add a check box column.
            DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            m_HeaderCell = new DataGridViewCheckBoxHeaderCell();
            m_HeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(HeaderCell_OnCheckBoxClicked);
            column.HeaderCell = m_HeaderCell;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.DataPropertyName = string.Empty;
            column.Name = COLUMN_SELECTALL;
            column.DisplayIndex = 1;
            column.Width = 130;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvUserList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DataPropertyName = PROPERTYNAME_LOGIN_ID;
            column.Name = COLUMN_LOGIN_ID;
            column.ReadOnly = true;
            column.Width = 333;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvUserList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DataPropertyName = PROPERTYNAME_NAME;
            column.Name = COLUMN_NAME;
            column.ReadOnly = true;
            column.Width = 333;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvUserList.Columns.Add(column);

            // Initialize and add a text box column.
            //column = new DataGridViewTextBoxColumn();
            //column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //column.DataPropertyName = PROPERTYNAME_ID;
            //column.Name = COLUMN_ID;
            //column.ReadOnly = true;
            //column.SortMode = DataGridViewColumnSortMode.Automatic;
            //column.Width = 220;
            //dgvUserList.Columns.Add(column);
        }

        /// <summary>
        /// Handles the CellClick event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        void dgvUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != 0)
            {
                DataGridViewRow row = dgvUserList.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    bool state = (bool)row.Cells[0].Value;
                    row.Cells[COLUMN_SELECTALL].Value = !state;
                }
                else
                {
                    row.Cells[COLUMN_SELECTALL].Value = true;
                }
            }
        }

        /// <summary>
        /// Initials the data.
        /// </summary>
        private void InitialData()
        {
            //从DB获得医生和管理员数据
            bool success = DataAccessProxy.Execute<User>(() => { return m_UserDao.GetAllDoctorAndAdmin(); }, this, out m_UserList);
            if (success)
            {
                BindData(m_UserList);
            }
        }
        /// <summary>
        /// Initials the UI.
        /// </summary>
        private void InitialUI()
        {
            txtAccount.Text = string.Empty;
            //txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="userList">The user list.</param>
        /// <returns>BindingSource.</returns>
        private BindingSource BindData(List<User> userList)
        {
            //创建可以排序的BindingList
            SortableBindingList<User> bindingList = new SortableBindingList<User>(userList);
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = bindingList;
            dgvUserList.DataSource = bindingSource;

            //设置按照LoginID列升序排序
            dgvUserList.Sort(dgvUserList.Columns[COLUMN_LOGIN_ID], ListSortDirection.Ascending);
            dgvUserList.Refresh();
            return bindingSource;
        }


        #region Button event handlers
        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (m_UserList != null)
            {
                //执行查询条件,返回查询结果
                List<User> userList = m_UserList.FindAll(FindPredicate);
                BindingSource bindingSource = BindData(userList);
                m_HeaderCell.Checked = false;
                btnDelete.Enabled = false;
                //当没有满足条件的数据,弹出消息框
                if (bindingSource.Count == 0)
                {
                    MessageBox.Show(this, CommonMessage.M0003, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Finds the predicate.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if the user match, <c>false</c> otherwise</returns>
        private bool FindPredicate(User user)
        {
            bool result = true;
            //检查登录名
            if (!string.IsNullOrWhiteSpace(txtAccount.Text))
            {
                if (!user.LoginID.ToLower().Contains(txtAccount.Text.ToLower().Trim()))
                {
                    return false;
                }
                result = true;
            }
            //检查用户名
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (!user.Name.ToLower().Contains(txtName.Text.ToLower().Trim()))
                {
                    return false;
                }
                result = true;
            }
            //检查用户ID
            //if (!string.IsNullOrWhiteSpace(txtID.Text))
            //{
            //    if (!user.ID.ToLower().Contains(txtID.Text.ToLower().Trim()))
            //    {
            //        return false;
            //    }
            //    result = true;
            //}
            return result;
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(CommonMessage.M0010, string.Empty, MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                List<User> userList = new List<User>();
                foreach (DataGridViewRow row in dgvUserList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[COLUMN_SELECTALL].Value))
                    {
                        User user = row.DataBoundItem as User;
                        if (user != null)
                        {
                            //判断删除账号是否是当前登陆账号
                            if (LoginInfoManager.CurrentUser.LoginID == user.LoginID)
                            {
                                MessageBox.Show(this, CommonMessage.M0011, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            userList.Add(user);
                        }
                    }
                }
                if (userList.Count > 0)
                {
                    foreach (User user in userList)
                    {
                        //删除账号
                        bool success = DataAccessProxy.Execute(() => { m_UserDao.Delete(user); }, this);
                        if (!success)
                        {
                            return;
                        }
                    }
                    InitialData();
                    InitialUI();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewUserDialog dialog = new NewUserDialog();
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                InitialData();
                InitialUI();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //获得当前选中用户
            if (dgvUserList.CurrentRow != null)
            {
                User user = dgvUserList.CurrentRow.DataBoundItem as User;
                if (user != null)
                {
                    EditUserDialog editUserDialog = new EditUserDialog();
                    editUserDialog.Initial(user);
                    if (editUserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        //当返回OK，更新列表数据
                        InitialData();
                        InitialUI();
                    }
                }
            }
        }

        #endregion Button event handlers

        #region DataGridView event handlers
        /// <summary>
        /// Headers the cell_ on check box clicked.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void HeaderCell_OnCheckBoxClicked(bool state)
        {
            //选择或取消全选 Checkbox,设置所有行状态
            foreach (DataGridViewRow row in dgvUserList.Rows)
            {
                row.Cells[COLUMN_SELECTALL].Value = state;
            }
            dgvUserList.RefreshEdit();
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void dgvUserList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //获得当前选中账号
            if (dgvUserList.CurrentRow != null && e.RowIndex == dgvUserList.CurrentRow.Index)
            {
                User user = dgvUserList.CurrentRow.DataBoundItem as User;

                if (user != null)
                {
                    EditUserDialog editUserDialog = new EditUserDialog();
                    editUserDialog.Initial(user);
                    if (editUserDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        InitialData();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvUserList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //编辑全选列
            if (e.ColumnIndex == 0)
            {
                bool deleteStatus = GetDeleteButtonStatus();
                btnDelete.Enabled = deleteStatus;
            }
        }

        /// <summary>
        /// Gets the delete button status.
        /// </summary>
        /// <returns><c>true</c> if one or more item is checked, <c>false</c> otherwise</returns>
        private bool GetDeleteButtonStatus()
        {
            bool deleteStatus = false;
            int selectCount = 0;
            foreach (DataGridViewRow row in dgvUserList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        deleteStatus = true;
                        selectCount++;
                    }
                }
            }
            m_HeaderCell.Checked = selectCount == dgvUserList.Rows.Count;
            return deleteStatus;
        }

        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvUserList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgvUserList.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        #endregion DataGridView event handlers

        /// <summary>
        /// Handles the Sorted event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvUserList_Sorted(object sender, EventArgs e)
        {
            //当排序时去掉全选checkbox
            m_HeaderCell.Checked = false;
            btnDelete.Enabled = false;
        }
    }
}
