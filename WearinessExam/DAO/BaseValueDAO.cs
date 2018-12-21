using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using MedicalSys.DataAccessor;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WearinessExam.DO;

namespace WearinessExam.DAO
{
    /// <summary>
    /// Class BaseValueDAO
    /// </summary>
    public class BaseValueDAO : IDAO<BaseValue>
    {
        /// <summary>
        /// The Column VALUE_KEY
        /// </summary>
        private const string COLUMN_VALUE_KEY = "VALUE_KEY";
        /// <summary>
        /// The Column PATIENT_KEY
        /// </summary>
        private const string COLUMN_PATIENT_KEY = "PATIENT_KEY";
        /// <summary>
        /// The Column UPDATE_DATE_TIME
        /// </summary>
        private const string COLUMN_UPDATE_DATE_TIME = "UPDATE_DATE_TIME";
        /// <summary>
        /// The Column USER_KEY
        /// </summary>
        private const string COLUMN_USER_KEY = "USER_KEY";
        /// <summary>
        /// The Column CFF
        /// </summary>
        private const string COLUMN_CFF = "CFF";

        /// <summary>
        /// The Column PID
        /// </summary>
        private const string COLUMN_PID = "PID";
        /// <summary>
        /// The Column PMD
        /// </summary>
        private const string COLUMN_PMD = "PMD";
        /// <summary>
        /// The Column PCV
        /// </summary>
        private const string COLUMN_PCV = "PCV";
        /// <summary>
        /// The Column PCL
        /// </summary>
        private const string COLUMN_PCL = "PCL";
        /// <summary>
        /// The Column PCR
        /// </summary>
        private const string COLUMN_PCR = "PCR";
        /// <summary>
        /// The Column PCT
        /// </summary>
        private const string COLUMN_PCT = "PCT";
        /// <summary>
        /// The Column PCA
        /// </summary>
        private const string COLUMN_PCA = "PCA";
        /// <summary>
        /// The Column PCD
        /// </summary>
        private const string COLUMN_PCD = "PCD";
        


        /// <summary>
        /// The SQL Update CFF
        /// </summary>
        private const string SQL_UPDATE_CFF = @"UPDATE M_WEARINESS_BASE_VALUE
                                           SET [PATIENT_KEY] = @PATIENT_KEY
                                              ,[UPDATE_DATE_TIME] = @UPDATE_DATE_TIME
                                              ,[USER_KEY] = @USER_KEY
                                              ,[CFF] = @CFF
                                         WHERE VALUE_KEY = @VALUE_KEY";

        /// <summary>
        /// The SQL Update PID
        /// </summary>
        private const string SQL_UPDATE_PID = @"UPDATE M_WEARINESS_BASE_VALUE
                                           SET [PATIENT_KEY] = @PATIENT_KEY
                                              ,[UPDATE_DATE_TIME] = @UPDATE_DATE_TIME
                                              ,[USER_KEY] = @USER_KEY
                                              ,[PID] = @PID
                                              ,[PMD] = @PMD
                                              ,[PCV] = @PCV
                                              ,[PCL] = @PCL
                                              ,[PCR] = @PCR
                                              ,[PCT] = @PCT
                                              ,[PCA] = @PCA
                                              ,[PCD] = @PCD
                                         WHERE VALUE_KEY = @VALUE_KEY";


        /// <summary>
        /// The SQL Insert CFF
        /// </summary>
        private const string SQL_INSERT = @"INSERT INTO [zhmsdb].[dbo].[M_WEARINESS_BASE_VALUE]
                                       ([PATIENT_KEY],[UPDATE_DATE_TIME],[USER_KEY],[CFF],[PID],[PMD],
                                        [PCV],[PCL],[PCR],[PCT],[PCA],[PCD])
                                 VALUES (@PATIENT_KEY ,@UPDATE_DATE_TIME,@USER_KEY,@CFF,@PID,@PMD,
                                        @PCV,@PCL,@PCR,@PCT,@PCA,@PCD)";


        /// <summary>
        /// The SQL Select
        /// </summary>
        private const string SQL_SELECT = @"SELECT [VALUE_KEY] ,[PATIENT_KEY],[UPDATE_DATE_TIME],[USER_KEY],
                                      [CFF],[PID],[PMD],[PCV],[PCL],[PCR],[PCT],[PCA],[PCD]
                                      FROM M_WEARINESS_BASE_VALUE 
                                      where [PATIENT_KEY] = ";


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>System.Collections.Generic.List{BaseValue}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Collections.Generic.List<BaseValue> GetAll()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the specified base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        public void Update(BaseValue baseValue)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPDATE_CFF);
            db.AddInParameter(dbCommand, COLUMN_VALUE_KEY, DbType.Int32, baseValue.primaryKey);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, baseValue.Patient_key);
            db.AddInParameter(dbCommand, COLUMN_UPDATE_DATE_TIME, DbType.DateTime2, baseValue.UPDATE_DATE_TIME);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, baseValue.USER_KEY);
            db.AddInParameter(dbCommand, COLUMN_CFF, DbType.Decimal, baseValue.CFF);

            // Execute the Command, update the result.
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Updates the specified base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        public void UpdatePID(BaseValue baseValue)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPDATE_PID);
            db.AddInParameter(dbCommand, COLUMN_VALUE_KEY, DbType.Int32, baseValue.primaryKey);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, baseValue.Patient_key);
            db.AddInParameter(dbCommand, COLUMN_UPDATE_DATE_TIME, DbType.DateTime2, baseValue.UPDATE_DATE_TIME);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, baseValue.USER_KEY);
            //db.AddInParameter(dbCommand, COLUMN_CFF, DbType.Decimal, baseValue.CFF);
            db.AddInParameter(dbCommand, COLUMN_PID, DbType.Decimal, baseValue.PID);
            db.AddInParameter(dbCommand, COLUMN_PMD, DbType.Decimal, baseValue.PMD);
            db.AddInParameter(dbCommand, COLUMN_PCV, DbType.Decimal, baseValue.PCV);
            db.AddInParameter(dbCommand, COLUMN_PCL, DbType.Decimal, baseValue.PCL);
            db.AddInParameter(dbCommand, COLUMN_PCR, DbType.Decimal, baseValue.PCR);
            db.AddInParameter(dbCommand, COLUMN_PCT, DbType.Decimal, baseValue.PCT);
            db.AddInParameter(dbCommand, COLUMN_PCA, DbType.Decimal, baseValue.PCA);
            db.AddInParameter(dbCommand, COLUMN_PCD, DbType.Decimal, baseValue.PCD);

            // Execute the Command, update the result.
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Deletes the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(BaseValue dataObject)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Inserts the specified base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        public void Insert(BaseValue baseValue)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_INSERT);
            db.AddInParameter(dbCommand, COLUMN_VALUE_KEY, DbType.Int32, baseValue.primaryKey);
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, baseValue.Patient_key);
            db.AddInParameter(dbCommand, COLUMN_UPDATE_DATE_TIME, DbType.DateTime2, baseValue.UPDATE_DATE_TIME);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, baseValue.USER_KEY);
            db.AddInParameter(dbCommand, COLUMN_CFF, DbType.Decimal, baseValue.CFF);
            db.AddInParameter(dbCommand, COLUMN_PID, DbType.Decimal, baseValue.PID);
            db.AddInParameter(dbCommand, COLUMN_PMD, DbType.Decimal, baseValue.PMD);
            db.AddInParameter(dbCommand, COLUMN_PCV, DbType.Decimal, baseValue.PCV);
            db.AddInParameter(dbCommand, COLUMN_PCL, DbType.Decimal, baseValue.PCL);
            db.AddInParameter(dbCommand, COLUMN_PCR, DbType.Decimal, baseValue.PCR);
            db.AddInParameter(dbCommand, COLUMN_PCT, DbType.Decimal, baseValue.PCT);
            db.AddInParameter(dbCommand, COLUMN_PCA, DbType.Decimal, baseValue.PCA);
            db.AddInParameter(dbCommand, COLUMN_PCD, DbType.Decimal, baseValue.PCD);

            // Execute the Command, writing the result.
            db.ExecuteNonQuery(dbCommand);
        }


        /// <summary>
        /// Gets the base value.
        /// </summary>
        /// <param name="PatientKey">The patient key.</param>
        /// <returns>BaseValue.</returns>
        public BaseValue GetBaseValue(int PatientKey)
        {
            Database db = DatabaseFactory.CreateDatabase();
            IRowMapper<BaseValue> rowMapper = CreateRowMapper();
            string sql = SQL_SELECT + PatientKey.ToString();
            // 根据PatientKey查询受测员的基础值
            List<BaseValue> list = db.ExecuteSqlStringAccessor(sql, rowMapper).ToList();
            BaseValue value = null;
            if (list.Count > 0)
            {
                value = list[0];
            }
            return value;
        }


        /// <summary>
        /// Creates the row mapper.
        /// </summary>
        /// <returns>IRowMapper{BaseValue}.</returns>
        private IRowMapper<BaseValue> CreateRowMapper()
        {
            IRowMapper<BaseValue> rowMapper = MapBuilder<BaseValue>.MapAllProperties()
                .Map(b => b.primaryKey).ToColumn(COLUMN_VALUE_KEY)
                .Map(b => b.Patient_key).ToColumn(COLUMN_PATIENT_KEY)
                .Map(b => b.UPDATE_DATE_TIME).ToColumn(COLUMN_UPDATE_DATE_TIME)
                .Map(b => b.USER_KEY).ToColumn(COLUMN_USER_KEY)
                .Map(b => b.CFF).ToColumn(COLUMN_CFF)
                .Map(b => b.PID).ToColumn(COLUMN_PID)
                .Map(b => b.PMD).ToColumn(COLUMN_PMD)
                .Map(b => b.PCV).ToColumn(COLUMN_PCV)
                .Map(b => b.PCL).ToColumn(COLUMN_PCL)
                .Map(b => b.PID).ToColumn(COLUMN_PID)
                .Map(b => b.PMD).ToColumn(COLUMN_PMD)
                .Map(b => b.PCV).ToColumn(COLUMN_PCV)
                .Map(b => b.PCL).ToColumn(COLUMN_PCL).Build();

            return rowMapper;
        }
    }
}
