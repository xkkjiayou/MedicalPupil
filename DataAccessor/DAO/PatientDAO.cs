using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Class PatientDAO
    /// </summary>
    public class PatientDAO:IDAO<Patient>
    {
        
        private const string COLUMN_PATIENT_KEY = "PATIENT_KEY";
        private const string COLUMN_PATIENT_ID = "PATIENT_ID";
        private const string COLUMN_PATIENT_NAME = "PATIENT_NAME";
        private const string COLUMN_SEX = "SEX";
        private const string COLUMN_BIRTH = "BIRTH";
        private const string COLUMN_AGE = "AGE";
        private const string COLUMN_UNIT = "UNIT";
        private const string SQL_SELECTALL = "select [PATIENT_KEY],[PATIENT_ID],[PATIENT_NAME],[SEX],[BIRTH],[UNIT],FLOOR(datediff(dayofyear,[BIRTH],getdate())/365.25) as AGE from M_Patient";
        private const string SQL_UPDATE = "update [M_Patient] Set [PATIENT_ID] =@PATIENT_ID,[PATIENT_NAME] =@PATIENT_NAME, [SEX]=@SEX,[BIRTH] =@BIRTH,[UNIT] =@UNIT from M_Patient Where [PATIENT_KEY] = @PATIENT_KEY";
        private const string SQL_DELETE = "Delete from [M_Patient] Where  [PATIENT_KEY] = @PATIENT_KEY";
        private const string SQL_INSERT = "Insert into [M_Patient] ([PATIENT_ID] ,[PATIENT_NAME],[SEX],[BIRTH],[UNIT]) Values (@PATIENT_ID,@PATIENT_NAME,@SEX,@BIRTH,@UNIT)";
        private const string SQL_SELECT_PATIENT = "select [PATIENT_KEY],[PATIENT_ID],[PATIENT_NAME],[SEX],[BIRTH],[UNIT],FLOOR(datediff(dayofyear,[BIRTH],getdate())/365.25) as AGE from M_Patient Where [PATIENT_ID] = @PATIENT_ID";
        private const string SQL_SELECT_PATIENTID = "select count(*) from M_Patient where [PATIENT_ID] = @PATIENT_ID";
        private const string SQL_SELECT_PATIENTKEY = "select [PATIENT_KEY],[PATIENT_ID],[PATIENT_NAME],[SEX],[BIRTH],[UNIT],FLOOR(datediff(dayofyear,[BIRTH],getdate())/365.25) as AGE from M_Patient Where [PATIENT_KEY] = @PATIENT_KEY";

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List{Patient}.</returns>
        public List<Patient> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();
            IRowMapper<Patient> rowMapper = CreateRowMapper();
            List<Patient> userList = db.ExecuteSqlStringAccessor(SQL_SELECTALL, rowMapper).ToList();
            return userList;
        }

        /// <summary>
        /// Creates the row mapper.
        /// </summary>
        /// <returns>IRowMapper{Patient}.</returns>
        private IRowMapper<Patient> CreateRowMapper()
        {
            IRowMapper<Patient> rowMapper = MapBuilder<Patient>.MapAllProperties()
                .Map(b => b.Name).ToColumn(COLUMN_PATIENT_NAME)
                .Map(b => b.ID).ToColumn(COLUMN_PATIENT_ID)
                .Map(b => b.Sex).ToColumn(COLUMN_SEX)
                .Map(b => b.Birth).ToColumn(COLUMN_BIRTH)
                .Map(b => b.Age).ToColumn(COLUMN_AGE)
                .Map(b => b.Unit).ToColumn(COLUMN_UNIT)
                .Map(b => b.primaryKey).ToColumn(COLUMN_PATIENT_KEY).Build();
            return rowMapper;
        }

        /// <summary>
        /// Updates the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void Update(Patient patient)
        { 
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPDATE);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_NAME, DbType.String, patient.Name);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_ID, DbType.String, patient.ID);
            db.AddInParameter(dbCommand, COLUMN_SEX, DbType.String, patient.Sex);
            if (DateTime.MinValue == patient.Birth)
            {
                db.AddInParameter(dbCommand, COLUMN_BIRTH, DbType.DateTime2, null);
            }
            else
            {
                db.AddInParameter(dbCommand, COLUMN_BIRTH, DbType.DateTime2, patient.Birth);
            }
            db.AddInParameter(dbCommand, COLUMN_UNIT, DbType.String, patient.Unit);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, patient.primaryKey);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Deletes the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void Delete(Patient patient)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_DELETE);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, patient.primaryKey);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Inserts the specified patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void Insert(Patient patient)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_INSERT);

            db.AddInParameter(dbCommand, COLUMN_PATIENT_NAME, DbType.String, patient.Name);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_ID, DbType.String, patient.ID);
            db.AddInParameter(dbCommand, COLUMN_SEX, DbType.Int32, patient.Sex);
            if (DateTime.MinValue == patient.Birth)
            {
                db.AddInParameter(dbCommand, COLUMN_BIRTH, DbType.DateTime2, null);
            }
            else
            {
                db.AddInParameter(dbCommand, COLUMN_BIRTH, DbType.DateTime2, patient.Birth);
            }
            db.AddInParameter(dbCommand, COLUMN_UNIT, DbType.String, patient.Unit);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Determines whether [has exist ID] [the specified patient ID].
        /// </summary>
        /// <param name="patientID">The patient ID.</param>
        /// <returns><c>true</c> if [has exist ID] [the specified patient ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistID(string patientID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_PATIENTID);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_ID, DbType.String, patientID);

            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="patientID">The patient ID.</param>
        /// <returns>List{Patient}.</returns>
        public List<Patient> GetPatient(string patientID)
        {
            List<Patient> patientList = new List<Patient>();
            Patient patient = new Patient();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_PATIENT);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_ID, DbType.String, patientID);

            DataSet patientDataSet = db.ExecuteDataSet(dbCommand);
            DataTable dataTable = new DataTable();

            dataTable = patientDataSet.Tables[0];
            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                patient.primaryKey = (Int32)dataTable.Rows[0][COLUMN_PATIENT_KEY];
                patient.Name = dataTable.Rows[0][COLUMN_PATIENT_NAME].ToString();
                patient.ID = dataTable.Rows[0][COLUMN_PATIENT_ID].ToString();
                patient.Sex = (Int32)dataTable.Rows[0][COLUMN_SEX];
                patient.Unit = dataTable.Rows[0][COLUMN_UNIT].ToString();
                if (!(dataTable.Rows[0][COLUMN_BIRTH] is DBNull))
                {
                    patient.Birth = (DateTime)dataTable.Rows[0][COLUMN_BIRTH];
                }
                if (!(dataTable.Rows[0][COLUMN_AGE] is DBNull))
                {
                    patient.Age = dataTable.Rows[0][COLUMN_AGE].ToString();
                }
                patientList.Add(patient);
            }
            return patientList;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="patientID">The patient ID.</param>
        /// <returns>List{Patient}.</returns>
        public List<Patient> GetPatient(int patientKey)
        {
            List<Patient> patientList = new List<Patient>();
            Patient patient = new Patient();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_PATIENTKEY);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, patientKey);

            DataSet patientDataSet = db.ExecuteDataSet(dbCommand);
            DataTable dataTable = new DataTable();

            dataTable = patientDataSet.Tables[0];
            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                patient.primaryKey = (Int32)dataTable.Rows[0][COLUMN_PATIENT_KEY];
                patient.Name = dataTable.Rows[0][COLUMN_PATIENT_NAME].ToString();
                patient.ID = dataTable.Rows[0][COLUMN_PATIENT_ID].ToString();
                patient.Sex = (Int32)dataTable.Rows[0][COLUMN_SEX];
                patient.Unit = dataTable.Rows[0][COLUMN_UNIT].ToString();
                if (!(dataTable.Rows[0][COLUMN_BIRTH] is DBNull))
                {
                    patient.Birth = (DateTime)dataTable.Rows[0][COLUMN_BIRTH];
                }
                if (!(dataTable.Rows[0][COLUMN_AGE] is DBNull))
                {
                    patient.Age = dataTable.Rows[0][COLUMN_AGE].ToString();
                }
                patientList.Add(patient);
            }
            return patientList;
        }
    }
}
