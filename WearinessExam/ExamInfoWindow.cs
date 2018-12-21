using MedicalSys.Framework;
using MedicalSys.MSCommon;
using MedicalSys.MSCommon.UIControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using WearinessExam.DAO;
using WearinessExam.DO;
using WearinessExam.Examination;
using WearinessExam.Utility;
using System.Drawing;

namespace WearinessExam
{
    public partial class ExamInfoWindow : MedicalSys.MSCommon.BaseForm
    {

        #region 变量
        /// <summary>
        /// The SQL and text.
        /// </summary>
        private const string SQL_AND = " AND ";

        /// <summary>
        /// The exam DAO
        /// </summary>
        private ExamInfoDAO m_ExamDao = new ExamInfoDAO();

        /// <summary>
        /// The exam data
        /// </summary>
        private DataTable m_ExamData = null;

        /// <summary>
        /// The Column width.
        /// </summary>
        private const int COLUMN_WIDTH = 120;

        enum ExamDataType
        {
            CFF1,
            CFF2
        }

        private const string EXPORT_COLUMN_CFF1 = "临界闪光融合频率—低到高（Hz）";
        private const string EXPORT_COLUMN_CFF2 = "临界闪光融合频率-高到低（Hz）";
        #endregion 变量

        public ExamInfoWindow()
        {
            InitializeComponent();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.AutoScroll = true;
            this.Width = 1280;
            this.Height = 780;

            //txtName.TextChanged += new EventHandler(textChanged);
            //chbSex.TextChanged += new EventHandler(textChanged);
            //txtUnit.TextChanged += new EventHandler(textChanged);
            //cmbScenario.TextChanged += new EventHandler(textChanged);
            //dtpStartTime.ValueChanged += new EventHandler(textChanged);
            //dtpEndTime.ValueChanged += new EventHandler(textChanged);
            txtAgeStart.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            txtAgeEnd.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            dgvExamData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExamData.OnCellSelected += SetButtonsStatus;
            dgvExamData.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dgvExamData_CellMouseDoubleClick);
            Initial();
            InitGridView();
            GetTop100ExamData();
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

        //void textChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtName.Text) &&
        //        string.IsNullOrEmpty(chbSex.Text) &&
        //        string.IsNullOrEmpty(txtUnit.Text) &&
        //        string.IsNullOrEmpty(cmbScenario.Text) &&
        //        dtpStartTime.Value == null &&
        //        dtpEndTime.Value == null &&
        //        string.IsNullOrEmpty(txtAgeStart.Text) &&
        //        string.IsNullOrEmpty(txtAgeEnd.Text))
        //    {
        //        this.btnInquire.Enabled = false;
        //    }
        //    else
        //    {
        //        this.btnInquire.Enabled = true;
        //    }
        //}

        #region 私有函数
        /// <summary>
        /// Initials the controls.
        /// </summary>
        private void Initial()
        {
            //初始设置基础值按钮，导出按钮，复制按钮，报告按钮，删除按钮为不可用
            btnBaseValue.Enabled = false;
            btnExport.Enabled = false;
            btnCopy.Enabled = false;
            btnReport.Enabled = false;
            btnDelete.Enabled = false;
            //起始和结束时间为空
            dtpStartTime.Text = string.Empty;
            dtpEndTime.Text = string.Empty;
            chbSex.DropDownStyle = ComboBoxStyle.DropDownList;

            // 初始化检查情景
            var strScenario = IniSettingConfig.GetInstance().ExamScenario;
            PopulateScenarioCombo(strScenario);
        }

        /// <summary>
        /// Initials the data grid view.
        /// </summary>
        private void InitGridView()
        {
            dgvExamData.AutoGenerateColumns = true;

            DataTable data = null;
            //初始化DataGridView
            bool success = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamData("1=2"); }, this, out data);
            if (success)
            {
                BindData(data);
            }
        }

        /// <summary>
        /// Sets the buttons status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void SetButtonsStatus(bool status)
        {
            //GridList选中或取消时，设置按钮状态
            btnBaseValue.Enabled = status;
            btnExport.Enabled = status;
            btnCopy.Enabled = status;
            btnReport.Enabled = status;
            btnDelete.Enabled = status;
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="data">The data table.</param>
        /// <returns>BindingSource.</returns>
        private BindingSource BindData(DataTable data)
        {
            m_ExamData = data;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = data;
            dgvExamData.DataSource = bindingSource;

            SetColumnStatus();

            lblRecordCount.Text = string.Format("共 {0} 条", data.Rows.Count);
            //设置按照PatientID列升序排序
            //dgvExamData.Sort(dgvExamData.Columns[ConstMessage.COLUMN_ID], ListSortDirection.Ascending);
            dgvExamData.Refresh();
            return bindingSource;
        }

        /// <summary>
        /// Sets the column status.
        /// </summary>
        private void SetColumnStatus()
        {
            foreach (DataGridViewColumn column in dgvExamData.Columns)
            {
                column.Visible = false;

                if (column.Name != DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL)
                {
                    column.ReadOnly = true;
                }
                else
                {
                    column.Width = 55;
                    column.Visible = true;
                }
                //PATIENT_KEY  EXAM_KEY不显示
                //if (column.Name == ConstMessage.COLUMN_PATIENT_KEY || column.Name == ConstMessage.COLUMN_EXAM_KEY ||
                //    column.Name == ConstMessage.COLUMN_ID)
                //{
                //    column.Visible = false;
                //}
                //设置EXAM_DATETIME，EXAM_SCENARIO和BIRTHDAY宽度
                if (column.Name == ConstMessage.COLUMN_EXAM_SCENARIO ||
                    column.Name == ConstMessage.COLUMN_BIRTHDAY)
                {
                    column.Width = COLUMN_WIDTH;
                    column.Visible = true;
                }
                if (column.Name == ConstMessage.COLUMN_EXAM_PARAMS)
                {
                    column.Width = 200;
                    column.Visible = true;
                }
                if (column.Name == ConstMessage.COLUMN_EXAM_DATETIME)
                {
                    column.Width = 180;
                    column.Visible = true;
                }
                if (column.Name == ConstMessage.COLUMN_NAME || column.Name == ConstMessage.COLUMN_ID 
                    || column.Name == ConstMessage.COLUMN_SEX || column.Name == ConstMessage.COLUMN_UNIT)
                {
                    column.Visible = true;
                }
                if (column.Name == "CFF" || column.Name == "PID" || column.Name == "PID2" || column.Name == "PCV" || 
                    column.Name == "PCV2" || column.Name == "PCL" || column.Name == "PCL2" || column.Name == "PCR" || column.Name == "PCR2")
                {
                    column.Width = 70;
                    column.Visible = true;
                }

            }
        }

        /// <summary>
        /// Subtracts the year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="year">The year.</param>
        /// <returns>DateTime.</returns>
        private DateTime SubtractYear(DateTime datetime, int year)
        {
            int rusultYear = datetime.Year - year;
            return new DateTime(rusultYear, datetime.Month, datetime.Day);
        }

        /// <summary>
        /// Gets the exam data selected.
        /// </summary>
        /// <returns>List{BaseValue}.</returns>
        private List<BaseValue> GetExamDataSelected()
        {
            List<BaseValue> baseValueList = new List<BaseValue>();
            //遍历所有行,获得被选中项目
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                //判断是否被选中
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {
                    BaseValue examData = new BaseValue();
                    //获得每个检测项数据
                    examData.CFF = Convert.ToDouble(row.Cells[ConstMessage.COLUMN_CFF].Value);
                    baseValueList.Add(examData);
                }
            }

            return baseValueList;

        }

        private static void AddExamData(DataTable originalDT, double[] examData, ExamDataType dataType)
        {
            if (examData == null)
            {
                return;
            }
            for (int i = 0; i < examData.Length; i++)
            {
                if (originalDT.Rows.Count < i + 1)
                {
                    DataRow row = originalDT.NewRow();
                    originalDT.Rows.Add(row);
                }
                //临界闪光融合频率—低到高（Hz）
                if (dataType == ExamDataType.CFF1)
                {
                    originalDT.Rows[i][EXPORT_COLUMN_CFF1] = examData[i];
                }
                //临界闪光融合频率-高到低（Hz）
                else if (dataType == ExamDataType.CFF2)
                {
                    originalDT.Rows[i][EXPORT_COLUMN_CFF2] = examData[i];
                }
            }
        }

        /// <summary>
        /// Sets the default base value.
        /// </summary>
        /// <param name="dt">The data table.</param>
        private void SetDefaultBaseValue(DataTable dt)
        {
            BaseValue baseVaule = BaseValueHelper.GetDefaultBaseValue();
            //把默认的基础值存入DataTable
            foreach (DataRow row in dt.Rows)
            {
                if (row[ConstMessage.COLUMN_CFF] == DBNull.Value)
                {
                    row[ConstMessage.COLUMN_CFF] = baseVaule.CFF;
                }
            }
        }

        /// <summary>
        /// Gets the exam key selected.
        /// </summary>
        /// <returns>List{System.Int32}.</returns>
        private Dictionary<int, int> GetExamIndexSelected()
        {
            Dictionary<int, int> examIndexList = new Dictionary<int, int>();

            //遍历所有记录，获得选中数据的ExamKey
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {
                    examIndexList.Add(Convert.ToInt32(row.Cells[ConstMessage.COLUMN_EXAM_KEY].Value), row.Index);
                }
            }
            return examIndexList;
        }


        /// <summary>
        /// Checks the exam data for same patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool CheckExamDataForSamePatient(out int patientKey)
        {
            patientKey = 0;
            bool result = false;
            //遍历列表中所有记录
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                //对被选中的记录处理
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {

                    if (patientKey == 0)
                    {
                        //获得第一条记录的PatientKey
                        result = true;
                        patientKey = Convert.ToInt32(row.Cells[ConstMessage.COLUMN_PATIENT_KEY].Value);
                    }
                    else
                    {
                        //判断PatientKey是否和最初的值一致
                        if (Convert.ToInt32(row.Cells[ConstMessage.COLUMN_PATIENT_KEY].Value) != patientKey)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// Gets the exam key selected.
        /// </summary>
        /// <returns>List{System.Int32}.</returns>
        private List<int> GetExamKeySelected()
        {
            List<int> examKeyList = new List<int>();

            //遍历所有记录，获得选中数据的ExamKey
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {
                    examKeyList.Add(Convert.ToInt32(row.Cells[ConstMessage.COLUMN_EXAM_KEY].Value));
                }
            }
            return examKeyList;
        }

        /// <summary>
        /// Gets the patient key selected.
        /// </summary>
        /// <returns>List{System.Int32}.</returns>
        private List<int> GetPatientKeySelected()
        {
            List<int> patientKeyList = new List<int>();

            //获取选中的PatientKey列表
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {
                    int patientKey = Convert.ToInt32(row.Cells[ConstMessage.COLUMN_PATIENT_KEY].Value);
                    //判断PatientKey是否重复，重复的就不添加到List了
                    if (!patientKeyList.Exists((a) => { return a == patientKey; }))
                    {
                        patientKeyList.Add(Convert.ToInt32(row.Cells[ConstMessage.COLUMN_PATIENT_KEY].Value));
                    }
                }
            }
            return patientKeyList;
        }


        /// <summary>
        /// 向ComboBox添加检测情景
        /// </summary>
        private void PopulateScenarioCombo(string defaultScenario)
        {
            string fileName = Path.Combine(AssemblyHelper.GetExecuteAssemblyPath(), "DefaultScenario.xml");
            XDocument xmlDoc = XDocument.Load(fileName);
            var s = from c in xmlDoc.Elements("Scenario").Elements() select c.Value;
            var scenarioList = s.ToList();
            if (!scenarioList.Contains(defaultScenario))
            {
                scenarioList.Add(defaultScenario);
            }
            this.cmbScenario.DataSource = scenarioList;
            this.cmbScenario.SelectedIndex = -1;
            this.cmbScenario.Text = string.Empty;
        }
        #endregion 私有函数

        #region 事件
        /// <summary>
        /// Handles the Click event of the Close button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the Report button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnReport_Click(object sender, System.EventArgs e)
        {
            ReportWindow reportWindow = new ReportWindow();
            //克隆一个DataTable
            DataTable dtReport = m_ExamData.Clone();
            //获得选中的Exam_Key
            List<int> examList = GetExamKeySelected();
            foreach (DataRow row in m_ExamData.Rows)
            {
                if (examList.Contains(Convert.ToInt32(row[ConstMessage.COLUMN_EXAM_KEY])))
                {
                    dtReport.Rows.Add(row.ItemArray);
                }
            }
            dtReport.AcceptChanges();

            reportWindow.AddedList = dtReport;
            reportWindow.Owner = this;
            reportWindow.ShowDialog();
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the dgvExamData control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        void dgvExamData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //ReportWindow reportWindow = new ReportWindow();
            ////克隆一个DataTable
            //DataTable dtReport = m_ExamData.Clone();
            ////获得选中的Exam_Key
            //int examKey = Convert.ToInt32(dgvExamData.CurrentRow.Cells[ConstMessage.COLUMN_EXAM_KEY].Value);
            //foreach (DataRow row in m_ExamData.Rows)
            //{
            //    if (row.RowState != DataRowState.Deleted && examKey == Convert.ToInt32(row[ConstMessage.COLUMN_EXAM_KEY]))
            //    {
            //        dtReport.Rows.Add(row.ItemArray);
            //    }
            //}
            //dtReport.AcceptChanges();

            //reportWindow.AddedList = dtReport;
            //reportWindow.Owner = this;
            //reportWindow.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the Inquire button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnInquire_Click(object sender, System.EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtAgeStart.Text) && !string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                if (Convert.ToInt32(txtAgeStart.Text.Trim()) > Convert.ToInt32(txtAgeEnd.Text.Trim()))
                {
                    MessageBox.Show(this, ConstMessage.MESSAGE_M1201, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(dtpStartTime.Text) && !string.IsNullOrWhiteSpace(dtpEndTime.Text))
            {
                if (Convert.ToDateTime(dtpStartTime.Value) > Convert.ToDateTime(dtpEndTime.Value))
                {
                    MessageBox.Show(this, ConstMessage.MESSAGE_M1202, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string strSqlCondation = string.Empty;
            //根据输入构造查询条件

            //判断姓名是否为空，如果不为空创建PATIENT_NAME查询条件
            if (!string.IsNullOrWhiteSpace(txtName.Text))
            {
                strSqlCondation += "PATIENT_NAME like '%" + txtName.Text.Trim() + "%' " + SQL_AND;
            }
            //判断性别是否为空，如果不为空创建SEX查询条件
            if (!string.IsNullOrWhiteSpace(chbSex.Text))
            {
                strSqlCondation += "SEX =" + chbSex.SelectedIndex.ToString() + " " + SQL_AND;
            }
            //判断单位是否为空，如果不为空创建UNIT查询条件
            if (!string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                strSqlCondation += "UNIT like '%" + txtUnit.Text.Trim() + "%' " + SQL_AND;
            }
            //判断出生日期 起始是否为空，如果不为空创建Birth查询条件
            if (!string.IsNullOrWhiteSpace(txtAgeStart.Text))
            {
                strSqlCondation += "FLOOR(datediff(dayofyear,[BIRTH],getdate())/365.25)>=" + "'" + Convert.ToInt32(txtAgeStart.Text) + "' " + SQL_AND;
            }
            //判断出生日期 结束是否为空，如果不为空创建Birth查询条件
            if (!string.IsNullOrWhiteSpace(txtAgeEnd.Text))
            {
                strSqlCondation += "FLOOR(datediff(dayofyear,[BIRTH],getdate())/365.25)<=" + "'" + Convert.ToInt32(txtAgeEnd.Text) + "' " + SQL_AND;
            }
            //判断场景是否为空，如果不为空创建EXAM_SCENARIO查询条件
            if (!string.IsNullOrWhiteSpace(cmbScenario.Text))
            {
                strSqlCondation += "EXAM_SCENARIO like '%" + cmbScenario.Text.Trim() + "%' " + SQL_AND;
            }
            //判断检查时间 起始是否为空，如果不为空创建检查时间查询条件
            if (!string.IsNullOrWhiteSpace(dtpStartTime.Text))
            {
                strSqlCondation += "convert(varchar(8),EXAM_DATE_TIME,112) >='" + Convert.ToDateTime(dtpStartTime.Value).ToString("yyyyMMdd") + "' " + SQL_AND;
            }
            //判断检查时间 结束是否为空，如果不为空创建检查时间查询条件
            if (!string.IsNullOrWhiteSpace(dtpEndTime.Text))
            {
                strSqlCondation += "convert(varchar(8),EXAM_DATE_TIME,112) <='" + Convert.ToDateTime(dtpEndTime.Value).ToString("yyyyMMdd") + "' " + SQL_AND;
            }

            if (!string.IsNullOrEmpty(strSqlCondation))
            {
                strSqlCondation = strSqlCondation.Substring(0, strSqlCondation.Length - SQL_AND.Length);
                DataTable data = null;
                //读取检查数据
                bool success = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamData(strSqlCondation); }, this, out data);
                if (success)
                {
                    BindData(data);
                    //if (data.Rows.Count == 0)
                    //{
                    //    MessageBox.Show(this, ConstMessage.MESSAGE_M1101, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                    //设置Button
                    SetButtonsStatus(false);

                    this.dgvExamData.UnselectedHeaderCell();
                }
            }
            else
            {
                MessageBox.Show("请输入查询条件！");

            }
        }

        private void GetTop100ExamData()
        {
            DataTable data = null;
            //读取检查数据
            bool success = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetTop100ExamData(); }, this, out data);
            if (success)
            {
                BindData(data);
                //if (data.Rows.Count == 0)
                //{
                //    MessageBox.Show(this, ConstMessage.MESSAGE_M1101, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                //设置Button
                SetButtonsStatus(false);
            }
        }

        //private double GetExamDataForBaseValue(string indicatorName, string patientKey)
        //{
        //    double value = 0d;
        //    DataTable data = null;
        //    //读取检查数据
        //    bool success = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamDataForBaseValue(indicatorName, patientKey); }, this, out data);
        //    if (success)
        //    {
        //        if (data.Rows.Count != 0)
        //        {
        //            value = (double)data.Compute("avg("+ indicatorName + ")", null);
        //        }
        //    }

        //    return value;
        //}




        /// <summary>
        /// Handles the Click event of the BaseValue button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBaseValue_Click(object sender, System.EventArgs e)
        {
            int patientKey;
            //判断是否选择了同一个Patient的记录
            if (CheckExamDataForSamePatient(out patientKey))
            {
                BaseValueDialog baseValueDialog = new BaseValueDialog(patientKey);
                baseValueDialog.Owner = this;
                baseValueDialog.BaseValueList = GetExamDataSelected();
                baseValueDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show(this, ConstMessage.MESSAGE_M1105, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the Delete button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, ConstMessage.MESSAGE_M1102, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                //获得选中的Exam_key列表
                //List<int> examKeyList = GetExamKeySelected();
                Dictionary<int, int> examIndexList = GetExamIndexSelected();
                if (examIndexList.Count > 0)
                {

                    //bool hasNotDeleted = false;

                    //List<int> onExamKeyList = new List<int>();

                    //foreach (int i in ExamDataCenter.ExamList.Keys)
                    //{
                    //    onExamKeyList.Add(ExamDataCenter.ExamList[i].primaryKey);
                    //}

                    //逐条删除这些记录
                    foreach (int key in examIndexList.Keys)
                    {

                        // 检测列表中的检测的信息不能删除，防止更新检测结果失败。
                        //if (onExamKeyList.Contains(key))
                        //{
                        //    hasNotDeleted = true;
                        //    return;
                        //}

                        bool success = DataAccessProxy.Execute(() => { m_ExamDao.Delete(key); }, this);
                        if (!success)
                        {
                            return;
                        }

                        int count = m_ExamData.Rows.Count;
                        //从列表中删除这条记录
                        for (int i = count - 1; i >= 0; i--)
                        {
                            if (Convert.ToInt32(m_ExamData.Rows[i][ConstMessage.COLUMN_EXAM_KEY]) == key)
                            {
                                m_ExamData.Rows[i].Delete();
                                break;
                            }
                        }
                        //dgvExamData.Rows.RemoveAt(examIndexList[key]);
                        dgvExamData.Refresh();
                    }

                    //if (hasNotDeleted)
                    //{
                    //    MessageBox.Show("部分选中条目仍在检测列表中，删除将导致继续检测时更新记录失败，请先将检测列表中对应的选中条目删除。", string.Empty, MessageBoxButtons.OK);
                    //}

                }
                SetButtonsStatus(false);
            }
        }


        /// <summary>
        /// Handles the Click event of the Import button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Excel Worksheets|*.xls";
            //选择导入文件
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Cursor cursor = Cursor.Current;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    PatientDataImporter importer = new PatientDataImporter(openFileDialog.FileName, this);
                    BackGroundWorkerDialog dialog = new BackGroundWorkerDialog();
                    dialog.BackGroundWorkerObject = importer;
                    //启动导入
                    dialog.ShowDialog(this);


                    //if (!string.IsNullOrEmpty(importer.ErrorMessage))
                    //{
                    //    MessageBox.Show(this, importer.ErrorMessage, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                finally
                {
                    Cursor.Current = cursor;
                }
            }

        }

        /// <summary>
        /// Handles the Click event of the Copy button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            var newline = System.Environment.NewLine;
            var tab = "\t";
            var clipboard_string = new StringBuilder();

            //获取选中的Patient
            foreach (DataGridViewRow row in dgvExamData.Rows)
            {
                if (Convert.ToBoolean(row.Cells[DataGridViewWithCheckBoxColumn.COLUMN_SELECTALL].Value))
                {
                    // Copy单元格中的数据，包括检测值
                    int count = dgvExamData.ColumnCount;

                    for (int i = 1; i < count; i++)
                    {
                        if (row.Cells[i].Visible)
                        {
                            if (i == (count - 1))
                            {
                                clipboard_string.Append(row.Cells[i].Value + newline);
                            }
                            else
                            {
                                clipboard_string.Append(row.Cells[i].Value + tab);
                            }
                        }
                    }
                }
            }

            Clipboard.SetText(clipboard_string.ToString());
        }

        /// <summary>
        /// Handles the Click event of the Export button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportDialog exportDialog = new ExportDialog();

            List<int> examKeyList = GetExamKeySelected();

            List<int> patientKeyList = GetPatientKeySelected();

            //设置多选属性
            exportDialog.MultiSelected = examKeyList.Count > 1;

            //打开导出设置Dialog
            if (exportDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            ExportDataDAO exportDataDao = new ExportDataDAO();

            List<DataTable> dataTableList = new List<DataTable>();
            //当选择基础值数据类型，读取基础值。
            if ((exportDialog.SelectDataType & ExportDataType.BaseValueData) == ExportDataType.BaseValueData)
            {
                DataTable datatable = null;
                bool success = DataAccessProxy.Execute<DataTable>(() => { return exportDataDao.GetBaseValue(patientKeyList); }, this, out datatable);
                if (!success)
                {
                    return;
                }

                DataRow[] drs = datatable.Select("CFF is null");
                foreach (DataRow myRow in drs)
                {
                    myRow["CFF"] = BaseValueHelper.GetDefaultBaseValue().CFF.ToString();
                }
                datatable.TableName = ConstMessage.BASEVALUE_SHEET_NAME;
                dataTableList.Add(datatable);
            }
            //当选择检查值数据类型，读取检查值。
            if ((exportDialog.SelectDataType & ExportDataType.ExamData) == ExportDataType.ExamData)
            {
                DataTable datatable = null;
                bool success = DataAccessProxy.Execute<DataTable>(() => { return exportDataDao.GetExamData(examKeyList); }, this, out datatable);
                if (!success)
                {
                    return;
                }

                datatable.TableName = ConstMessage.EXAM_DATA_SHEET_NAME;
                dataTableList.Add(datatable);
            }

            //当选择原始数据类型，读取原始数据。
            if ((exportDialog.SelectDataType & ExportDataType.OriginalData) == ExportDataType.OriginalData)
            {

                int exam_Key = examKeyList[0];
                ExamInfo exam = null;
                bool success = DataAccessProxy.Execute<ExamInfo>(() => { return m_ExamDao.GetExamData(exam_Key); }, this, out exam);
                if (!success)
                {
                    return;
                }
                DataTable patientDT = null;
                success = DataAccessProxy.Execute<DataTable>(() => { return exportDataDao.GetPatient(exam.Patient_key); }, this, out patientDT);
                if (!success)
                {
                    return;
                }

                patientDT.TableName = ConstMessage.PATIENTDATA_SHEET_NAME;
                dataTableList.Add(patientDT);

                DataTable originalDT = new DataTable();
                originalDT.Columns.Add(EXPORT_COLUMN_CFF1);
                originalDT.Columns.Add(EXPORT_COLUMN_CFF2);


                // 从文件读出曲线数据
                double[] cff1 = null;
                double[] cff2 = null;

                if (DataFileHelper.CheckFileExist(exam.DATA_FILE_NAME))
                {
                    DataFileHelper dataFileHelper = new DataFileHelper(exam.DATA_FILE_NAME);
                    // CFF数据
                    cff1 = dataFileHelper.GetCffLow2HighSerials();
                    cff2 = dataFileHelper.GetCffHigh2LowSerials();
                }

                AddExamData(originalDT, cff1, ExamDataType.CFF1);
                AddExamData(originalDT, cff2, ExamDataType.CFF2);

                originalDT.TableName = ConstMessage.EXAM_ORIGINAL_DATA_SHEET_NAME;
                dataTableList.Add(originalDT);
            }

            DataExporter importer = new DataExporter(dataTableList, exportDialog.SelectDirectory);

            BackGroundWorkerDialog dialog = new BackGroundWorkerDialog();
            dialog.BackGroundWorkerObject = importer;
            //启动导出 
            dialog.ShowDialog(this);
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            // 清空查询条件
            this.txtAgeStart.Text = string.Empty;
            this.txtAgeEnd.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.cmbScenario.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
            this.chbSex.ResetText();
            this.dtpEndTime.Value = null;
            this.dtpStartTime.Value = null;

            GetTop100ExamData();
        }

        private void btnClearQueryCondition_Click(object sender, EventArgs e)
        {
            // 清空查询条件
            this.txtAgeStart.Text = string.Empty;
            this.txtAgeEnd.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.cmbScenario.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
            this.chbSex.ResetText();
            this.dtpEndTime.Value = null;
            this.dtpStartTime.Value = null;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;
            }
        }
        #endregion 事件

    }
}
