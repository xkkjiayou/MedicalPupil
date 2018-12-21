using System;
using MedicalSys.DataAccessor;

namespace WearinessExam.DO
{
    /// <summary>
    /// 基础值
    /// </summary>
    public class BaseValue : IDataObject
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
        /// Gets or sets the patient_key.
        /// </summary>
        /// <value>The patient_key.</value>
        public int Patient_key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the update date time.
        /// </summary>
        /// <value>The update date time.</value>
        public DateTime UPDATE_DATE_TIME
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the USE r_ KEY.
        /// </summary>
        /// <value>The USE r_ KEY.</value>
        public int USER_KEY
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
        /// Gets or sets the PID.
        /// </summary>
        /// <value>The PID.</value>
        public double PID
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
        /// Gets or sets the PCV.
        /// </summary>
        /// <value>The PCV.</value>
        public double PCV
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
        /// Gets or sets the PCR.
        /// </summary>
        /// <value>The PCR.</value>
        public double PCR
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCT.
        /// </summary>
        /// <value>The PCT.</value>
        public double PCT
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCA.
        /// </summary>
        /// <value>The PCA.</value>
        public double PCA
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the PCD.
        /// </summary>
        /// <value>The PCD.</value>
        public double PCD
        {
            get;
            set;
        }



    }
}
