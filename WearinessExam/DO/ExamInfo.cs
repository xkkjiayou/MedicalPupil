using System;

namespace WearinessExam.DO
{
    /// <summary>
    /// Class ExamInfo
    /// </summary>
    public class ExamInfo
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        /// <value>The primary key.</value>
        public int primaryKey
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        public int Patient_key
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the exam date time.
        /// </summary>
        /// <value>The exam date time.</value>
        public DateTime EXAM_DATE_TIME
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the exam scenario.
        /// </summary>
        /// <value>The exam scenario.</value>
        public string EXAM_SCENARIO
        {
            get;
            set;
        }

        public string EXAM_PARAMS { get; set; }


        /// <summary>
        /// Gets or sets the user key.
        /// </summary>
        /// <value>The user key.</value>
        public int USER_KEY
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [CFF1 status].
        /// </summary>
        /// <value><c>true</c> if [CFF1 status]; otherwise, <c>false</c>.</value>
        public bool CFF1Status
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [CFF2 status].
        /// </summary>
        /// <value><c>true</c> if [CFF2 status]; otherwise, <c>false</c>.</value>
        public bool CFF2Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [PCL status].
        /// </summary>
        /// <value><c>true</c> if [PCL status]; otherwise, <c>false</c>.</value>
        public bool PCLStatus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the CFF.
        /// </summary>
        /// <value>The CFF.</value>
        public double CFF
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the CFF1.
        /// </summary>
        /// <value>The CFF1.</value>
        public double CFF1
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the CFF2.
        /// </summary>
        /// <value>The CFF2.</value>
        public double CFF2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCV.
        /// </summary>
        /// <value>The PCV.</value>
        public double PCV
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCV2.
        /// </summary>
        /// <value>The PCV2.</value>
        public double PCV2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCL.
        /// </summary>
        /// <value>The PCL.</value>
        public double PCL
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCL2.
        /// </summary>
        /// <value>The PCL.</value>
        public double PCL2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCR.
        /// </summary>
        /// <value>The PCR.</value>
        public double PCR
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCR2.
        /// </summary>
        /// <value>The PCR.</value>
        public double PCR2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PMD.
        /// </summary>
        /// <value>The PMD.</value>
        public double PMD
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PMD2.
        /// </summary>
        /// <value>The PMD.</value>
        public double PMD2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PID.
        /// </summary>
        /// <value>The PID.</value>
        public double PID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PID2.
        /// </summary>
        /// <value>The PID2.</value>
        public double PID2
        {
            get;
            set;
        }

        public double PCT
        {
            get;
            set;
        }

        public double PCT2
        {
            get;
            set;
        }

        public double PCA
        {
            get;
            set;
        }

        public double PCA2
        {
            get;
            set;
        }

        public double PCD
        {
            get;
            set;
        }

        public double PCD2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data file name.
        /// </summary>
        /// <value>The data file name.</value>
        public string DATA_FILE_NAME
        {
            get;
            set;
        }

    }
}
