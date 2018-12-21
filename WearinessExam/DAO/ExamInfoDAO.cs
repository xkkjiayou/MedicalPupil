using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WearinessExam.DO;

namespace WearinessExam.DAO
{
    /// <summary>
    /// Class ExamInfoDAO
    /// </summary>
    public class ExamInfoDAO
    {
        /// <summary>
        /// The Column EXAM_KEY
        /// </summary>
        private const string COLUMN_EXAM_KEY = "EXAM_KEY";
        /// <summary>
        /// The Column PATIENT_KEY
        /// </summary>
        private const string COLUMN_PATIENT_KEY = "PATIENT_KEY";
        /// <summary>
        /// The Column EXAM_DATE_TIME
        /// </summary>
        private const string COLUMN_EXAM_DATE_TIME = "EXAM_DATE_TIME";
        /// <summary>
        /// The Column EXAM_SCENARIO
        /// </summary>
        private const string COLUMN_EXAM_SCENARIO = "EXAM_SCENARIO";
        /// <summary>
        /// The Column EXAM_SCENARIO
        /// </summary>
        private const string COLUMN_EXAM_PARAMS = "EXAM_PARAMS";
        /// <summary>
        /// The Column USER_KEY
        /// </summary>
        private const string COLUMN_USER_KEY = "USER_KEY";
        /// <summary>
        /// The Column CFF1_FLG
        /// </summary>
        private const string COLUMN_CFF1_FLG = "CFF1_FLG";
        /// <summary>
        /// The Column CFF2_FLG
        /// </summary>
        private const string COLUMN_CFF2_FLG = "CFF2_FLG";
        /// <summary>
        /// The Column PCL_FLG
        /// </summary>
        private const string COLUMN_PCL_FLG = "PCL_FLG";
        /// <summary>
        /// The Column CFF1
        /// </summary>
        private const string COLUMN_CFF1 = "CFF1";
        /// <summary>
        /// The Column CFF2
        /// </summary>
        private const string COLUMN_CFF2 = "CFF2";
        /// <summary>
        /// The Column CFF
        /// </summary>
        private const string COLUMN_CFF = "CFF";
        /// <summary>
        /// The Column PID
        /// </summary>
        private const string COLUMN_PID = "PID";
        /// <summary>
        /// The Column PID2
        /// </summary>
        private const string COLUMN_PID2 = "PID2";
        /// <summary>
        /// The Column PCV
        /// </summary>
        private const string COLUMN_PCV = "PCV";
        /// <summary>
        /// The Column PCV2
        /// </summary>
        private const string COLUMN_PCV2 = "PCV2";
        /// <summary>
        /// The Column PCL
        /// </summary>
        private const string COLUMN_PCL = "PCL";
        /// <summary>
        /// The Column PCL2
        /// </summary>
        private const string COLUMN_PCL2 = "PCL2";
        /// <summary>
        /// The Column PCR
        /// </summary>
        private const string COLUMN_PCR = "PCR";
        /// <summary>
        /// The Column PCR2
        /// </summary>
        private const string COLUMN_PCR2 = "PCR2";
        /// <summary>
        /// The Column PCA
        /// </summary>
        private const string COLUMN_PCA = "PCA";
        /// <summary>
        /// The Column PCA2
        /// </summary>
        private const string COLUMN_PCA2 = "PCA2";
        /// <summary>
        /// The Column PCD
        /// </summary>
        private const string COLUMN_PCD = "PCD";
        /// <summary>
        /// The Column PCD2
        /// </summary>
        private const string COLUMN_PCD2 = "PCD2";
        /// <summary>
        /// The Column PCT
        /// </summary>
        private const string COLUMN_PCT = "PCT";
        /// <summary>
        /// The Column PCT2
        /// </summary>
        private const string COLUMN_PCT2 = "PCT2";
        /// <summary>
        /// The Column PMD
        /// </summary>
        private const string COLUMN_PMD = "PMD";
        /// <summary>
        /// The Column PCT2
        /// </summary>
        private const string COLUMN_PMD2 = "PMD2";
        /// <summary>
        /// The Column DATA_FILE_NAME
        /// </summary>
        private const string COLUMN_DATA_FILE_NAME = "DATA_FILE_NAME";
        /// <summary>
        /// The Column REPORT_SARMARY
        /// </summary>
        private const string COLUMN_REPORT_SARMARY = "REPORT_SARMARY";
        /// <summary>
        /// The Column REPORT_COMMENT
        /// </summary>
        private const string COLUMN_REPORT_COMMENT = "REPORT_COMMENT";
        /// <summary>
        /// The Column REPORT_CREATE_DATETIME
        /// </summary>
        private const string COLUMN_REPORT_CREATE_DATETIME = "REPORT_CREATE_DATETIME";

        /// <summary>
        /// The SQL select all exam
        /// </summary>
        private const string SELECT_ALL_EXAM = @"select  E.EXAM_KEY,P.PATIENT_KEY, P.PATIENT_ID as ID,P.PATIENT_NAME as 姓名, 性别 = case SEX when 1 Then '男' when 2 then '女' end,p.BIRTH as 出生日期, 
		p.UNIT as 单位, E.EXAM_SCENARIO as 检测情景,E.EXAM_PARAMS as 检测参数,E.EXAM_DATE_TIME as 检测时间,U.USER_NAME as 医生,
        E.CFF,E.CFF1,E.CFF2,E.PCV,E.PCV2,E.PCL,E.PCL2,E.PCR,E.PCR2,E.PMD,E.PMD2,E.PCT,E.PCT2,E.PCD,E.PCD2,E.PCA,E.PCA2,E.PID,E.PID2
		from dbo.M_PATIENT as P inner join M_WEARINESS_EXAM as E 
		on P.PATIENT_KEY = E.PATIENT_KEY left join M_USER as U 
		on E.USER_KEY = U.USER_KEY";

        /// <summary>
        /// The SQL select top 100 exam
        /// </summary>
        private const string SELECT_TOP100_EXAM = @"select top 100 E.EXAM_KEY,P.PATIENT_KEY, P.PATIENT_ID as ID,P.PATIENT_NAME as 姓名, 性别 = case SEX when 1 Then '男' when 2 then '女' end,p.BIRTH as 出生日期, 
        p.UNIT as 单位, E.EXAM_SCENARIO as 检测情景,E.EXAM_PARAMS as 检测参数,E.EXAM_DATE_TIME as 检测时间,U.USER_NAME as 医生,
        E.CFF,E.CFF1,E.CFF2,E.PCV,E.PCV2,E.PCL,E.PCL2,E.PCR,E.PCR2,E.PMD,E.PMD2,E.PCT,E.PCT2,E.PCD,E.PCD2,E.PCA,E.PCA2,E.PID,E.PID2
        from dbo.M_PATIENT as P inner join M_WEARINESS_EXAM as E 
        on P.PATIENT_KEY = E.PATIENT_KEY left join M_USER as U 
        on E.USER_KEY = U.USER_KEY order by E.EXAM_KEY desc";


        private const string SELECT_FOR_BASE_VALUE = @"select top 30 EXAM_KEY,PATIENT_KEY,EXAM_DATE_TIME,CFF,PCV,PCL,PCR,PMD,PCT,PCD,PCA,PID
           from M_WEARINESS_EXAM where {0}>0 and PATIENT_KEY ={1} order by EXAM_DATE_TIME desc";


        /// <summary>
        /// The SQL order by exam key desc
        /// </summary>
        private const string ORDER_BY_EXAM_KEY = " order by E.EXAM_KEY desc";

        /// <summary>
        /// The SQL select exam by key
        /// </summary>
        private const string SELECT_EXAM_BY_KEY = @"select [EXAM_KEY], [PATIENT_KEY] ,[EXAM_DATE_TIME],[EXAM_SCENARIO],[EXAM_PARAMS],[USER_KEY],[CFF1_FLG],[CFF2_FLG]
           ,[CFF1],[CFF2],[CFF],[DATA_FILE_NAME] from M_WEARINESS_EXAM where  [EXAM_KEY] = ";

        /// <summary>
        /// The SQL DELETE
        /// </summary>
        private const string SQL_DELETE = "Delete from [M_WEARINESS_EXAM] Where  [EXAM_KEY] = @EXAM_KEY";


        /// <summary>
        /// The SQL select exam report
        /// </summary>
        private const string SQL_SELECT_EXAM_REPORT = @"SELECT [EXAM_KEY]
        ,[PATIENT_KEY],[EXAM_DATE_TIME],[EXAM_SCENARIO],[EXAM_PARAMS],[USER_KEY]
        ,[CFF1_FLG],[CFF2_FLG],[CFF1],[CFF2],[CFF]
        ,[DATA_FILE_NAME],[REPORT_SARMARY],[REPORT_COMMENT],[REPORT_CREATE_DATETIME]
          FROM [dbo].[M_WEARINESS_EXAM]
        WHERE [EXAM_KEY]=";

        /// <summary>
        /// The SQL update report sarmary
        /// </summary>
        private const string SQL_UPDATE_REPORT_SARMARY = @"UPDATE [dbo].[M_WEARINESS_EXAM]
        SET [REPORT_SARMARY] = @REPORT_SARMARY
           ,[REPORT_COMMENT] = @REPORT_COMMENT
           ,[REPORT_CREATE_DATETIME]=@REPORT_CREATE_DATETIME
        WHERE [EXAM_KEY] = @EXAM_KEY";

        /// <summary>
        /// The SQL Insert exam data
        /// </summary>
        private const string SQL_INSERT_EXAM_DATA = @"INSERT INTO [M_WEARINESS_EXAM]
           ([PATIENT_KEY] ,[EXAM_DATE_TIME],[EXAM_SCENARIO],[EXAM_PARAMS],[USER_KEY],[CFF1_FLG],[CFF2_FLG],[PCL_FLG],
            [CFF1],[CFF2],[CFF],[PCA],[PCA2],[PCD],[PCD2],[PID],[PID2],[PMD],[PMD2],[PCV],[PCV2],[PCL],[PCL2],[PCR],[PCR2],[PCT],[PCT2],[DATA_FILE_NAME])
           VALUES (@PATIENT_KEY  ,@EXAM_DATE_TIME ,@EXAM_SCENARIO ,@EXAM_PARAMS ,@USER_KEY ,@CFF1_FLG,@CFF2_FLG,@PCL_FLG,
            @CFF1,@CFF2,@CFF,@PCA,@PCA2,@PCD,@PCD2,@PID,@PID2,@PMD,@PMD2,@PCV,@PCV2,@PCL,@PCL2,@PCR,@PCR2,@PCT,@PCT2,@DATA_FILE_NAME) Select @@IDENTITY";

        /// <summary>
        /// The SQL update exam data
        /// </summary>
        private const string SQL_UPATE_EXAM_DATA = @"UPDATE M_WEARINESS_EXAM SET 
          [PATIENT_KEY] = @PATIENT_KEY,[EXAM_DATE_TIME]=@EXAM_DATE_TIME,[EXAM_SCENARIO]=@EXAM_SCENARIO,
            [EXAM_PARAMS]=@EXAM_PARAMS,[USER_KEY]=@USER_KEY,[CFF1_FLG]=@CFF1_FLG ,[CFF2_FLG]=@CFF2_FLG,[PCL_FLG]=@PCL_FLG,
            [CFF1]=@CFF1,[CFF2]=@CFF2,[CFF]=@CFF,[PID]=@PID,[PID2]=@PID2,[PCV]=@PCV,[PCV2]=@PCV2,[PCL]=@PCL,[PCL2]=@PCL2,
            [PCA]=@PCA,[PCA2]=@PCA2,[PCD]=@PCD,[PCD2]=@PCD2,[PCR]=@PCR,[PCR2]=@PCR2,[PCT]=@PCT,[PCT2]=@PCT2,[PMD]=@PMD,[PMD2]=@PMD2,
            [DATA_FILE_NAME]= @DATA_FILE_NAME Where EXAM_KEY = @EXAM_KEY";

        /// <summary>
        /// Gets the exam data.
        /// </summary>
        /// <param name="sqlCondation">The SQL condation.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetExamData(string sqlCondation)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command object.
            string sqlSelect = sqlCondation == null ? SELECT_ALL_EXAM : SELECT_ALL_EXAM + " Where " + sqlCondation + ORDER_BY_EXAM_KEY;
            DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect);

            // Execute the Command.
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }

        /// <summary>
        /// Gets the top of 100 exam data.
        /// </summary>
        /// <param name="sqlCondation">The SQL condation.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetTop100ExamData()
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command object.
            DbCommand dbCommand = db.GetSqlStringCommand(SELECT_TOP100_EXAM);

            // Execute the Command.
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }

        /// <summary>
        /// Gets the top of 30 exam data.
        /// </summary>
        /// <param name="sqlCondation">The SQL condation.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetExamDataForBaseValue(string indicatorName, int patientKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command object.
            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(SELECT_FOR_BASE_VALUE, indicatorName, patientKey));

            // Execute the Command.
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }


        /// <summary>
        /// Gets the exam report data.
        /// </summary>
        /// <param name="examKey">The exam key.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetExamReportData(int examKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command object.
            string sqlSelect = SQL_SELECT_EXAM_REPORT + examKey.ToString();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect);

            // Execute the Command.
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }

        /// <summary>
        /// Deletes the specified exam key.
        /// </summary>
        /// <param name="examKey">The exam key.</param>
        public void Delete(int examKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_DELETE);
            db.AddInParameter(dbCommand, COLUMN_EXAM_KEY, DbType.Int32, examKey);

            // Execute the Command, delete the result.
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Inserts the specified exam info.
        /// </summary>
        /// <param name="examInfo">The exam info.</param>
        public void Insert(ExamInfo examInfo)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_INSERT_EXAM_DATA);
            BindData(examInfo, db, dbCommand);

            // Execute the Command, insert the exam.
            examInfo.primaryKey = Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        /// <summary>
        /// Updates the specified exam info.
        /// </summary>
        /// <param name="examInfo">The exam info.</param>
        public void Update(ExamInfo examInfo)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPATE_EXAM_DATA);
            db.AddInParameter(dbCommand, COLUMN_EXAM_KEY, DbType.Int32, examInfo.primaryKey);
            BindData(examInfo, db, dbCommand);

            // Execute the Command, update the exam.
            db.ExecuteNonQuery(dbCommand);
        }

        public ExamInfo GetExamData(int examKey)
        {
            
            Database db = DatabaseFactory.CreateDatabase();
            IRowMapper<ExamInfo> rowMapper = CreateRowMapper();
            string sql = SELECT_EXAM_BY_KEY + examKey.ToString();
            // 根据examKey查询检查信息
            List<ExamInfo> list = new List<ExamInfo>(db.ExecuteSqlStringAccessor(sql, rowMapper));
            ExamInfo value = null;
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
        private IRowMapper<ExamInfo> CreateRowMapper()
        {
            IRowMapper<ExamInfo> rowMapper = MapBuilder<ExamInfo>.MapAllProperties()
                .Map(b => b.primaryKey).ToColumn(COLUMN_EXAM_KEY)
                .Map(b => b.Patient_key).ToColumn(COLUMN_PATIENT_KEY)
                .Map(b => b.EXAM_DATE_TIME).ToColumn(COLUMN_EXAM_DATE_TIME)
                .Map(b => b.USER_KEY).ToColumn(COLUMN_USER_KEY)
                .Map(b => b.EXAM_SCENARIO).ToColumn(COLUMN_EXAM_SCENARIO)
                .Map(b => b.EXAM_PARAMS).ToColumn(COLUMN_EXAM_PARAMS)
                .Map(b => b.CFF1Status).ToColumn(COLUMN_CFF1_FLG)
                .Map(b => b.CFF2Status).ToColumn(COLUMN_CFF2_FLG)
                .Map(b => b.PCLStatus).ToColumn(COLUMN_PCL_FLG)
                .Map(b => b.CFF1).ToColumn(COLUMN_CFF1)
                .Map(b => b.CFF2).ToColumn(COLUMN_CFF2)
                .Map(b => b.CFF).ToColumn(COLUMN_CFF)
                .Map(b => b.PID).ToColumn(COLUMN_PID)
                .Map(b => b.PID2).ToColumn(COLUMN_PID2)
                .Map(b => b.PMD).ToColumn(COLUMN_PMD)
                .Map(b => b.PMD2).ToColumn(COLUMN_PMD2)
                .Map(b => b.PCA).ToColumn(COLUMN_PCA)
                .Map(b => b.PCA2).ToColumn(COLUMN_PCA2)
                .Map(b => b.PCD).ToColumn(COLUMN_PCD)
                .Map(b => b.PCD2).ToColumn(COLUMN_PCD2)
                .Map(b => b.PCV).ToColumn(COLUMN_PCV)
                .Map(b => b.PCV2).ToColumn(COLUMN_PCV2)
                .Map(b => b.PCL).ToColumn(COLUMN_PCL)
                .Map(b => b.PCL2).ToColumn(COLUMN_PCL2)
                .Map(b => b.PCR).ToColumn(COLUMN_PCR)
                .Map(b => b.PCR2).ToColumn(COLUMN_PCR2)
                .Map(b => b.PCT).ToColumn(COLUMN_PCT)
                .Map(b => b.PCT2).ToColumn(COLUMN_PCT2)
                .Map(b => b.DATA_FILE_NAME).ToColumn(COLUMN_DATA_FILE_NAME).Build();

            return rowMapper;
        }

        /// <summary>
        /// Updates the report summary.
        /// </summary>
        /// <param name="summary">The summary.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="examKey">The exam key.</param>
        public void UpdateReportSummary(string summary, string comment, int examKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            // Create the Command and Parameter objects.
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPDATE_REPORT_SARMARY);
            db.AddInParameter(dbCommand, COLUMN_REPORT_SARMARY, DbType.String, summary);
            db.AddInParameter(dbCommand, COLUMN_REPORT_COMMENT, DbType.String, comment);
            db.AddInParameter(dbCommand, COLUMN_REPORT_CREATE_DATETIME, DbType.DateTime2, DateTime.Now);
            db.AddInParameter(dbCommand, COLUMN_EXAM_KEY, DbType.Int32, examKey);

            // Execute the Command, update the summary.
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="examInfo">The exam info.</param>
        /// <param name="db">The db.</param>
        /// <param name="dbCommand">The db command.</param>
        private void BindData(ExamInfo examInfo, Database db, DbCommand dbCommand)
        {
            // Add the Parameter objects.
            db.AddInParameter(dbCommand, COLUMN_PATIENT_KEY, DbType.Int32, examInfo.Patient_key);
            db.AddInParameter(dbCommand, COLUMN_EXAM_DATE_TIME, DbType.DateTime2, examInfo.EXAM_DATE_TIME);
            db.AddInParameter(dbCommand, COLUMN_EXAM_SCENARIO, DbType.String, examInfo.EXAM_SCENARIO);
            db.AddInParameter(dbCommand, COLUMN_EXAM_PARAMS, DbType.String, examInfo.EXAM_PARAMS);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, examInfo.USER_KEY);
            db.AddInParameter(dbCommand, COLUMN_CFF1_FLG, DbType.Boolean, examInfo.CFF1Status);
            db.AddInParameter(dbCommand, COLUMN_CFF2_FLG, DbType.Boolean, examInfo.CFF2Status);
            db.AddInParameter(dbCommand, COLUMN_PCL_FLG, DbType.Boolean, examInfo.PCLStatus);
            db.AddInParameter(dbCommand, COLUMN_CFF1, DbType.Double, examInfo.CFF1);
            db.AddInParameter(dbCommand, COLUMN_CFF2, DbType.Double, examInfo.CFF2);
            db.AddInParameter(dbCommand, COLUMN_CFF, DbType.Double, examInfo.CFF);
            db.AddInParameter(dbCommand, COLUMN_PCA, DbType.Double, examInfo.PCA);
            db.AddInParameter(dbCommand, COLUMN_PCA2, DbType.Double, examInfo.PCA2);
            db.AddInParameter(dbCommand, COLUMN_PCD, DbType.Double, examInfo.PCD);
            db.AddInParameter(dbCommand, COLUMN_PCD2, DbType.Double, examInfo.PCD2);
            db.AddInParameter(dbCommand, COLUMN_PCT, DbType.Double, examInfo.PCT);
            db.AddInParameter(dbCommand, COLUMN_PCT2, DbType.Double, examInfo.PCT2);
            db.AddInParameter(dbCommand, COLUMN_PID, DbType.Double, examInfo.PID);
            db.AddInParameter(dbCommand, COLUMN_PID2, DbType.Double, examInfo.PID2);
            db.AddInParameter(dbCommand, COLUMN_PMD, DbType.Double, examInfo.PMD);
            db.AddInParameter(dbCommand, COLUMN_PMD2, DbType.Double, examInfo.PMD2);
            db.AddInParameter(dbCommand, COLUMN_PCV, DbType.Double, examInfo.PCV);
            db.AddInParameter(dbCommand, COLUMN_PCV2, DbType.Double, examInfo.PCV2);
            db.AddInParameter(dbCommand, COLUMN_PCL, DbType.Double, examInfo.PCL);
            db.AddInParameter(dbCommand, COLUMN_PCL2, DbType.Double, examInfo.PCL2);
            db.AddInParameter(dbCommand, COLUMN_PCR, DbType.Double, examInfo.PCR);
            db.AddInParameter(dbCommand, COLUMN_PCR2, DbType.Double, examInfo.PCR2);
            db.AddInParameter(dbCommand, COLUMN_DATA_FILE_NAME, DbType.String, examInfo.DATA_FILE_NAME);
        }
    }
}