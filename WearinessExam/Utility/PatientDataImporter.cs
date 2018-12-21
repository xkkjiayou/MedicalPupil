using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MedicalSys.DataAccessor;
using MedicalSys.Framework;
using MedicalSys.Framework.Utility;
using MedicalSys.MSCommon;
using WearinessExam.DAO;
using WearinessExam.DO;

namespace WearinessExam.Utility
{
    /// <summary>
    /// Class PatientDataImporter
    /// </summary>
    public class PatientDataImporter : IBackGroundWorkerObject
    {
        /// <summary>
        /// The Importer error message.
        /// </summary>
        private string MESSAGE_IMPORTER_ERROR = "数据导入发生错误！";

        /// <summary>
        /// The processing message.
        /// </summary>
        private const string MESSAGE_PROCCESSING = "正在导入数据，请等待 ......";

        /// <summary>
        /// The completed message
        /// </summary>
        private string m_CompletedMessage = "数据导入完成！";

        /// <summary>
        /// The column list
        /// </summary>
        private string[] ColumnList = new string[] { "ID", "姓名", "性别", "出生日期", "单位", "检测情景", "检测参数", "CFF", "检测时间", "医生" };
        /// <summary>
        /// The logger
        /// </summary>
        private IMSLogger m_Logger = LogFactory.GetLogger();

        /// <summary>
        /// The data table
        /// </summary>
        private DataTable m_DataTable;

        /// <summary>
        /// The patient DAO
        /// </summary>
        private PatientDAO patientDao = new PatientDAO();

        /// <summary>
        /// The base value DAO
        /// </summary>
        private BaseValueDAO baseValueDao = new BaseValueDAO();

        /// <summary>
        /// The default base vaule
        /// </summary>
        private BaseValue m_DefaultBaseVaule = null;

        /// <summary>
        /// The all same handle
        /// </summary>
        private bool m_AllSameHandle = false;

        /// <summary>
        /// The cover data
        /// </summary>
        private bool m_CoverData = false;

        /// <summary>
        /// The file name
        /// </summary>
        private string m_FileName;

        /// <summary>
        /// The backgound workder
        /// </summary>
        private BackgroundWorker m_BackgoundWorkder;

        /// <summary>
        /// Delegate ShowMessageBoxDelegate
        /// </summary>
        /// <param name="form">The form.</param>
        delegate void ShowMessageBoxDelegate(Form form);

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDataImporter"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="callWindow">The call window.</param>
        public PatientDataImporter(string fileName, Form callWindow)
        {
            m_FileName = fileName;
            CallWindow = callWindow;
        }

        /// <summary>
        /// Does the import work.
        /// </summary>
        public void DoWork()
        {
            ErrorMessage = string.Empty;
            Import();
        }

        /// <summary>
        /// Gets or sets the background worker.
        /// </summary>
        /// <value>The background worker.</value>
        public BackgroundWorker BackgroundWorker
        {
            get
            {
                return m_BackgoundWorkder;
            }
            set
            { m_BackgoundWorkder = value; }
        }


        /// <summary>
        /// Gets the proccessing message.
        /// </summary>
        /// <value>The proccessing message.</value>
        public string ProccessingMessage
        {
            get { return MESSAGE_PROCCESSING; }
        }

        /// <summary>
        /// Gets the completed message.
        /// </summary>
        /// <value>The completed message.</value>
        public string CompletedMessage
        {
            get { return m_CompletedMessage; }
        }

        /// <summary>
        /// Imports this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool Import()
        {
            string message;
            m_DataTable = ExcelHelper.GetDataFromExcel(m_FileName, true, ConstMessage.BASEVALUE_SHEET_NAME, out  message, 1);
            m_DefaultBaseVaule = BaseValueHelper.GetDefaultBaseValue();
            if (!string.IsNullOrEmpty(message))
            {
                ErrorMessage = message;
                m_CompletedMessage = MESSAGE_IMPORTER_ERROR;
                return false;
            }

            SaveDataTable(out message);
            if (!string.IsNullOrEmpty(message))
            {
                ErrorMessage = message;
                m_CompletedMessage = MESSAGE_IMPORTER_ERROR;
                return false;
            }

            return true;
        }
        /// <summary>
        /// Gets or sets the call window.
        /// </summary>
        /// <value>The call window.</value>
        public Form CallWindow
        {
            get;
            set;
        }

        /// <summary>
        /// Saves the data table.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        private void SaveDataTable(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                //检查DataTable各列
                if (CheckColumn(m_DataTable))
                {
                    int count = 0;
                    //遍历DataTable各行数据
                    foreach (DataRow dr in m_DataTable.Rows)
                    {
                        count++;
                        Patient newData = GetPatient(dr);
                        //判断ID和姓名是否为空
                        if (!string.IsNullOrEmpty(newData.ID) && !string.IsNullOrEmpty(newData.Name))
                        {
                            //根据ID获得Patient
                            List<Patient> patientList = patientDao.GetPatient(dr[ConstMessage.COLUMN_ID].ToString().Trim());

                            //判断Patient是否存在
                            if (patientList.Count > 0)
                            {
                                Patient origialData = patientList[0];

                                newData.ID = origialData.ID;
                                newData.primaryKey = origialData.primaryKey;

                                //比较Patient数据是否相等，如果不相等更新DB
                                PatientComparer comparer = new PatientComparer();
                                if (comparer.Compare(origialData, newData) != 0)
                                {
                                    patientDao.Update(newData);
                                }
                            }
                            else
                            {
                                //如果Patient不存在， 插入一条新的Patient记录
                                patientDao.Insert(newData);
                                newData = patientDao.GetPatient(newData.ID)[0];
                            }
                            //倒入基础值
                            ImportBaseValue(dr, newData);
                        }
                        else
                        {
                            //ID或者姓名为空，记录log
                            m_Logger.WarnFormat(ConstMessage.MESSAGE_PATIENTDATA_ERROR, newData.ID, newData.Name);
                        }
                        m_BackgoundWorkder.ReportProgress(count / m_DataTable.Rows.Count * 100);
                    }
                }
                else
                {
                    errorMessage = ConstMessage.MESSAGE_M1103;
                    m_Logger.Error(ConstMessage.MESSAGE_M1103);
                }
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex.Message);
                ShowMessageBoxDelegate d = delegate(Form window) { DataAccessProxy.ShowMessageBox(window); };
                this.CallWindow.Invoke(d, this.CallWindow);
            }
        }

        /// <summary>
        /// Imports the base value.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="patient">The patient.</param>
        private void ImportBaseValue(DataRow dr, Patient patient)
        {
            BaseValue newBaseValue = GetBaseValue(dr);
            //检查基础值是否满足要求
            if (CheckBaseValue(newBaseValue))
            {
                //获得这个Patient的基础值
                BaseValue oldValue = baseValueDao.GetBaseValue(patient.primaryKey);
                newBaseValue.Patient_key = patient.primaryKey;
                newBaseValue.UPDATE_DATE_TIME = DateTime.Now;
                newBaseValue.USER_KEY = LoginInfoManager.CurrentUser.primaryKey;
                BaseValueComparer baseValueComparer = new BaseValueComparer();

                //判断基础值是否存在
                if (oldValue != null)
                {
                    //比较基础值是否相同
                    if (baseValueComparer.Compare(oldValue, newBaseValue) != 0)
                    {
                        bool modified = false;
                        //原基础值和系统默认值是否相同
                        if (baseValueComparer.Compare(oldValue, m_DefaultBaseVaule) != 0)
                        {
                            modified = true;
                            if (!m_AllSameHandle)
                            {
                                //弹出窗口判断是否覆盖
                                ImportCoverDialog importDialog = new ImportCoverDialog();
                                ShowMessageBoxDelegate d = delegate(Form window) { importDialog.ShowDialog(window); };
                                this.CallWindow.Invoke(d, this.CallWindow);

                                m_AllSameHandle = importDialog.AllSameHandleChecked;
                                m_CoverData = importDialog.CoverDataChecked;
                            }
                        }
                        //数据未修改或者要覆盖的需要更新操作
                        if (!modified || m_CoverData)
                        {
                            newBaseValue.primaryKey = oldValue.primaryKey;
                            baseValueDao.Update(newBaseValue);
                        }
                    }
                }
                else
                {
                    //如果不存在，插入基础值
                    baseValueDao.Insert(newBaseValue);
                }
            }
            else
            {
                //基础值不完整，记录log
                m_Logger.WarnFormat(ConstMessage.MESSAGE_BASEVALUE_ERROR, patient.ID);
            }
        }

        /// <summary>
        /// Checks the column.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool CheckColumn(DataTable dt)
        {
            bool exist = true;
            //遍历检查表格各列是否存在
            foreach (string column in ColumnList)
            {
                if (!dt.Columns.Contains(column))
                {
                    exist = false;
                    break;
                }
            }
            return exist;
        }

        /// <summary>
        /// Checks the base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <returns><c>true</c> if the base value equls zero, <c>false</c> otherwise</returns>
        private static bool CheckBaseValue(BaseValue baseValue)
        {
            //判断各基础值是否为0值
            return (baseValue.CFF != 0);
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <returns>Patient.</returns>
        private Patient GetPatient(DataRow dataRow)
        {
            Patient patient = new Patient();

            //获得人员姓名
            patient.Name = dataRow[ConstMessage.COLUMN_NAME].ToString().Trim();

            //获得人员ID
            patient.ID = dataRow[ConstMessage.COLUMN_ID].ToString().Trim();

            //获得人员性别
            if (dataRow[ConstMessage.COLUMN_SEX].ToString() == "男")
            {
                patient.Sex = 1;
            }
            else if (dataRow[ConstMessage.COLUMN_SEX].ToString() == "女")
            {
                patient.Sex = 2;
            }
            else
            {
                patient.Sex = 0;
            }

            //获得人员单位
            patient.Unit = dataRow[ConstMessage.COLUMN_UNIT].ToString().Trim();

            //获得人员出生日期
            double OLEAutoDate;
            if (double.TryParse(dataRow[ConstMessage.COLUMN_BIRTHDAY].ToString(), out OLEAutoDate))
            {
                if (OLEAutoDate != 0)
                {
                    patient.Birth = DateTime.FromOADate(OLEAutoDate);
                }
            }

            return patient;
        }

        /// <summary>
        /// Gets the base value.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <returns>BaseValue.</returns>
        private BaseValue GetBaseValue(DataRow dataRow)
        {
            BaseValue baseValue = new BaseValue();
            double value;
            //获得CFF
            if (double.TryParse(dataRow[ConstMessage.COLUMN_CFF].ToString(), out value))
            {
                baseValue.CFF = value;
            }

            return baseValue;
        }

    }
}
