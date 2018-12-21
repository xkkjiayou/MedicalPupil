using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MedicalSys.DataAccessor;
using MedicalSys.MSCommon;
using WearinessExam.DAO;
using WearinessExam.DO;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class ExamDataCenter
    /// </summary>
    public class ExamDataCenter
    {
        private static LightSetting lightSetting = new LightSetting();

        public static LightSetting LightSetting
        {
            get { return lightSetting; }
            set { lightSetting = value; }
        }

        /// <summary>
        /// The m_ exam DAO
        /// </summary>
        private static ExamInfoDAO m_ExamDao = new ExamInfoDAO();
        /// <summary>
        /// Gets or sets the exam window.
        /// </summary>
        /// <value>The exam window.</value>
        public static WearinessDetectForm ExamWindow
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the current patient.
        /// </summary>
        /// <value>The current patient.</value>
        public static Patient CurrentPatient
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the exam data.
        /// </summary>
        /// <value>The exam data.</value>
        public static ExamInfo ExamData
        {
            get;
            set;
        }
        /// <summary>
        /// The m_ exam list
        /// </summary>
        private static Dictionary<int, ExamInfo> m_ExamList = new Dictionary<int, ExamInfo>();
        /// <summary>
        /// Gets the exam list.
        /// </summary>
        /// <value>The exam list.</value>
        public static Dictionary<int, ExamInfo> ExamList
        {
            get
            {
                return m_ExamList;
            }
        }

        /// <summary>
        /// The CFF1 value
        /// </summary>
        public static List<double> CFF1Value = new List<double>();


        /// <summary>
        /// The CFF2 value
        /// </summary>
        public static List<double> CFF2Value = new List<double>();
        
        /// <summary>
        /// Gets or sets the pupil exam data.
        /// </summary>
        /// <value>The pupil exam data.</value>
        public static PupilExamData PupilExamData = new PupilExamData();

        /// <summary>
        /// Selects the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public static void SelectPatient(Patient patient)
        {
            CurrentPatient = patient;
            CFF1Value.Clear();
            CFF2Value.Clear();
            //ScanExamData = new ScanExamData();
            //SVValue.Clear();
            //PupilExamData = new PupilExamData();
            //PCLValue.Clear();

            if (m_ExamList.ContainsKey(patient.primaryKey))
            {
                bool newExamFlag = MessageBox.Show("该受测员的检测记录已存在，是否开始新的检测？", string.Empty,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                if (newExamFlag)
                {
                    m_ExamList.Remove(patient.primaryKey);
                    ExamData = new ExamInfo();
                    return;
                }

                ExamData = m_ExamList[patient.primaryKey];

                if (!string.IsNullOrEmpty(ExamData.DATA_FILE_NAME) && DataFileHelper.CheckFileExist(ExamData.DATA_FILE_NAME))
                {
                    DataFileHelper dataFileHelper = new DataFileHelper(ExamData.DATA_FILE_NAME);

                    if (ExamData.CFF1Status)
                    {
                        // 读取CFF1曲线数据
                        CFF1Value.AddRange(dataFileHelper.GetCffLow2HighSerials());
                    }
                    if (ExamData.CFF2Status)
                    {
                        // 读取CFF2曲线数据
                        CFF2Value.AddRange(dataFileHelper.GetCffHigh2LowSerials());
                    }

                }
            }
            else
            {
                ExamData = new ExamInfo();
            }

        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            CurrentPatient = null;
            ExamData = null;
            CFF1Value.Clear();
            CFF2Value.Clear();
            //ScanExamData = new ScanExamData();
            //SVValue.Clear();
            //PupilExamData = new PupilExamData();
            //PCLValue.Clear();
        }

        /// <summary>
        /// Updates the exam.
        /// </summary>
        public static void UpdateExam()
        {

            if (ExamData.primaryKey == 0)
            {
                ExamData.Patient_key = CurrentPatient.primaryKey;
                ExamData.USER_KEY = LoginInfoManager.CurrentUser.primaryKey;
                ExamData.EXAM_DATE_TIME = DateTime.Now;
                m_ExamDao.Insert(ExamData);
                ExamList.Add(ExamData.Patient_key, ExamData);
            }
            else
            {
                m_ExamDao.Update(ExamData);
            }
        }
        /// <summary>
        /// Deletes the exam.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        public static void DeleteExam(int patientKey)
        {
            if (ExamList.ContainsKey(patientKey))
            {
                ExamList.Remove(patientKey);
            }
        }


    }

    public struct LightSetting
    {
        /// <summary>
        /// 亮点亮黑比
        /// </summary>
        public int Liangheibi;
        /// <summary>
        /// 亮点光亮度
        /// </summary>
        public int LDLiangdu;
        /// <summary>
        /// 背景光亮度
        /// </summary>
        public int BJLiangdu;
        /// <summary>
        /// 亮点颜色
        /// </summary>
        public int colorLight;

        /// <summary>
        /// 刺激光源
        /// </summary>
        public int StimulateColor;
        /// <summary>
        /// 刺激时长(ms)
        /// </summary>
        public int StimulateTime;
        /// <summary>
        /// 强度(%)
        /// </summary>
        public int StimulateStrength;

    }
}
