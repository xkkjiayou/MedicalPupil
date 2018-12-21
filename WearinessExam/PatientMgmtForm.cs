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
    public partial class PatientMgmtForm : MedicalSys.MSCommon.BaseForm
    {
        /// <summary>
        /// The column select all
        /// </summary>
        private const string COLUMN_SELECTALL = "";
        /// <summary>
        /// The column name
        /// </summary>
        private const string COLUMN_NAME = "姓名";
        /// <summary>
        /// The column id
        /// </summary>
        private const string COLUMN_ID = "ID";
        /// <summary>
        /// The column unit
        /// </summary>
        private const string COLUMN_UNIT = "单位";
        /// <summary>
        /// The column age
        /// </summary>
        private const string COLUMN_AGE = "年龄";

        /// <summary>
        /// The name column's property name
        /// </summary>
        private const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// The id column's property name
        /// </summary>
        private const string PROPERTYNAME_ID = "ID";
        /// <summary>
        /// The unit column's property name
        /// </summary>
        private const string PROPERTYNAME_UNIT = "Unit";
        /// <summary>
        /// The age column's property name
        /// </summary>
        private const string PROPERTYNAME_AGE = "Age";

        /// <summary>
        /// The min age
        /// </summary>
        private const int MINAGE = 0;
        /// <summary>
        /// The max age
        /// </summary>
        private const int MAXAGE = 200;

        /// <summary>
        /// The patient list
        /// </summary>
        private List<Patient> m_PatientList = null;
        /// <summary>
        /// The patient DAO
        /// </summary>
        private PatientDAO m_PatientDao = new PatientDAO();
        /// <summary>
        /// The user DAO
        /// </summary>
        private UserDAO m_UserDao = new UserDAO();
        /// <summary>
        /// The header cell
        /// </summary>
        DataGridViewCheckBoxHeaderCell m_HeaderCell;

        public PatientMgmtForm()
        {
            InitializeComponent();
        }

        public PatientMgmtForm(bool selectedMode)
        {
            InitializeComponent();

            if (selectedMode)
            {
                this.btnSelect.Visible = true;
                this.btnSelect.Enabled = false;
            }
        }


        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void Init()
        {
            // 初始化受测员List
            InitGridView();
            // 初始化数据
            InitData();
            // 初始化时删除按钮是不可用的状态
            this.btnDelete.Enabled = false;
            this.btnSelect.Enabled = false;
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
        /// Inits the grid view.
        /// </summary>
        private void InitGridView()
        {
            dgvPatientList.AutoGenerateColumns = false;
            dgvPatientList.CellClick += new DataGridViewCellEventHandler(dgvPatientList_CellClick);

            // Set the DataGridView control's border.
            dgvPatientList.BorderStyle = BorderStyle.Fixed3D;

            // Initialize and add a check box column.
            DataGridViewColumn column = new DataGridViewCheckBoxColumn();

            m_HeaderCell = new DataGridViewCheckBoxHeaderCell();
            m_HeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(HeaderCell_OnCheckBoxClicked);
            column.HeaderCell = m_HeaderCell;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.DataPropertyName = string.Empty;
            column.Name = COLUMN_SELECTALL;
            column.DisplayIndex = 1;
            column.Width = 109;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvPatientList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = PROPERTYNAME_NAME;
            column.Name = COLUMN_NAME;
            column.ReadOnly = true;
            column.Width = 180;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvPatientList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = PROPERTYNAME_ID;
            column.Name = COLUMN_ID;
            column.ReadOnly = true;
            column.Width = 180;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvPatientList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = PROPERTYNAME_UNIT;
            column.Name = COLUMN_UNIT;
            column.ReadOnly = true;
            column.Width = 240;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            dgvPatientList.Columns.Add(column);

            // Initialize and add a text box column.
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = PROPERTYNAME_AGE;
            column.Name = COLUMN_AGE;
            column.ReadOnly = true;
            column.Width = 170;
            column.SortMode = DataGridViewColumnSortMode.Automatic;    
            dgvPatientList.Columns.Add(column);

        }

        /// <summary>
        /// Handles the CellClick event of the dgvPatientList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        void dgvPatientList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != 0)
            {
                DataGridViewRow row = dgvPatientList.Rows[e.RowIndex];
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
        /// Inits the data.
        /// </summary>
        private void InitData()
        {
            // 从DB取得受测员信息
            bool success = DataAccessProxy.Execute<Patient>(() => { return m_PatientDao.GetAll(); }, this, out m_PatientList);
            if (success)
            {
                BindData(m_PatientList);
            }
            btnDelete.Enabled = false;
            btnSelect.Enabled = false;
            txtName.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtAgeStart.Text = string.Empty;
            txtAgeEnd.Text = string.Empty;
            SelectedPatient = new List<Patient>();
        }

        /// <summary>
        /// Binding the source.
        /// </summary>
        /// <param name="patientList">The patient list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private BindingSource BindData(List<Patient> patientList)
        {
            //创建可以排序的BindingList
            SortableBindingList<Patient> bindingList = new SortableBindingList<Patient>(patientList);
            BindingSource bindingSource = new BindingSource();
            bindingSource = new BindingSource();
            bindingSource.DataSource = bindingList;
            dgvPatientList.DataSource = bindingSource;

            //设置按照PatientID列升序排序
            dgvPatientList.Sort(dgvPatientList.Columns[COLUMN_ID], ListSortDirection.Ascending);
            dgvPatientList.Refresh();
            return bindingSource;
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAgeStart.Text) && !string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                if (Convert.ToInt32(txtAgeStart.Text.Trim()) > Convert.ToInt32(txtAgeEnd.Text.Trim()))
                {
                    MessageBox.Show(this, CommonMessage.M0012, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (m_PatientList != null)
            {
                List<Patient> patientList = m_PatientList.FindAll(FindPredicate);

                BindingSource bindingSource = BindData(patientList);

                if (bindingSource.Count == 0)
                {
                    MessageBox.Show(this, CommonMessage.M0007, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Finds the predicate.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool FindPredicate(Patient patient)
        {
            bool result = true;
            // 按姓名查询
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (!patient.Name.ToLower().Contains(txtName.Text.ToLower().Trim()))
                {
                    return false;
                }
                result = true;
            }

            // 按单位查询
            if (!string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                if (!patient.Unit.ToLower().Contains(txtUnit.Text.ToLower().Trim()))
                {
                    return false;
                }
                result = true;
            }

            // 按年龄查询
            int iAge = 0;
            if (!string.IsNullOrEmpty(patient.Age))
            {
                iAge = Convert.ToInt32(patient.Age);
            }
            // 开始年龄不输入的时候，把开始年龄默认为0
            if (string.IsNullOrWhiteSpace(txtAgeStart.Text) && !string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                if (!(MINAGE.CompareTo(iAge) <= 0 && Convert.ToInt32(txtAgeEnd.Text.Trim()).CompareTo(iAge) >= 0))
                {
                    return false;
                }
                result = true;
            }
            // 终了年龄不输入的时候,把终了年龄默认为200
            if (!string.IsNullOrWhiteSpace(txtAgeStart.Text) && string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                if (!(Convert.ToInt32(txtAgeStart.Text.Trim()).CompareTo(iAge) <= 0 && MAXAGE.CompareTo(iAge) >= 0))
                {
                    return false;
                }
                result = true;
            }
            // 开始年龄和终了同时输入
            if (!string.IsNullOrWhiteSpace(txtAgeStart.Text) && !string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                if (!(Convert.ToInt32(txtAgeStart.Text.Trim()).CompareTo(iAge) <= 0 && Convert.ToInt32(txtAgeEnd.Text.Trim()).CompareTo(iAge) >= 0))
                {
                    return false;
                }
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewPatientDialog dialog = new NewPatientDialog();
            dialog.Init();
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                InitData();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(CommonMessage.M0008, string.Empty, MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvPatientList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[COLUMN_SELECTALL].Value))
                    {
                        Patient patient = row.DataBoundItem as Patient;
                        
                        if (patient != null)
                        {
                            // 删除受测员
                            DataAccessProxy.Execute(() => { m_PatientDao.Delete(patient); }, this);
                        }
                    }
                }
                InitData();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnEidt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnEidt_Click(object sender, EventArgs e)
        {
            //获得当前选中用户
            Patient patient = dgvPatientList.CurrentRow.DataBoundItem as Patient;
            if (patient != null)
            {
                EditPatientDialog editPatientDialog = new EditPatientDialog();
                editPatientDialog.Init(patient);
                if (editPatientDialog.ShowDialog(this) == DialogResult.OK)
                {
                    InitData();
                }
            }
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the dgvPatientList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs" /> instance containing the event data.</param>
        private void dgvPatientList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// Headers the cell_ on check box clicked.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void HeaderCell_OnCheckBoxClicked(bool state)
        {
            //选择或取消全选 Checkbox,设置所有行状态
            foreach (DataGridViewRow row in dgvPatientList.Rows)
            {
                row.Cells[COLUMN_SELECTALL].Value = state;
            }
            dgvPatientList.RefreshEdit();
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvPatientList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs" /> instance containing the event data.</param>
        private void dgvPatientList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //编辑全选列
            if (e.ColumnIndex == 0)
            {
                bool deleteStatus = GetDeleteButtonStatus();
                btnDelete.Enabled = deleteStatus;
                btnSelect.Enabled = deleteStatus;
            }
        }

        /// <summary>
        /// Gets the delete button status.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool GetDeleteButtonStatus()
        {
            bool deleteStatus = false;
            int selectCount = 0;
            foreach (DataGridViewRow row in dgvPatientList.Rows)
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
            m_HeaderCell.Checked = selectCount == dgvPatientList.Rows.Count;
            return deleteStatus;
        }

        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the dgvPatientList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void dgvPatientList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            dgvPatientList.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        /// Handles the Sorted event of the UserList DataGridView.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void dgvUserList_Sorted(object sender, EventArgs e)
        {
            m_HeaderCell.Checked = false;
            btnDelete.Enabled = false;
            btnSelect.Enabled = false;
        }

        /// <summary>
        /// Gets the selected patient.
        /// </summary>
        /// <value>The selected patient.</value>
        public IList<Patient> SelectedPatient { get; private set; }

        /// <summary>
        /// Handles the Click event of the SelectPatient button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectedPatient.Clear();

            foreach (DataGridViewRow row in dgvPatientList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        SelectedPatient.Add(row.DataBoundItem as Patient);
                    }
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

    }
}
