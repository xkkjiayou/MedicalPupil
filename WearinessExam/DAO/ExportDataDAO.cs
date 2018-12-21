using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace WearinessExam.DAO
{
    /// <summary>
    /// Class ExportDataDAO
    /// </summary>
    public class ExportDataDAO
    {
        /// <summary>
        /// The SQL Select Base Value
        /// </summary>
        private const string SELECT_BASEVALUE_SQL = @"SELECT     PATIENT_ID  as ID, PATIENT_NAME as 姓名, 
                                    性别 = case SEX when 1 Then '男' when 2 then '女' end, BIRTH 出生日期, UNIT 单位, 
                                    UPDATE_DATE_TIME as 更新时间 , USER_NAME as 医生, CFF From V_Export_BaseVaule";

        /// <summary>
        /// The SQL Select Patient
        /// </summary>
        private const string SELECT_PATIENT_SQL = @"SELECT     PATIENT_ID  as ID, PATIENT_NAME as 姓名, 
                                    性别 = case SEX when 1 Then '男' when 2 then '女' end, BIRTH 出生日期, UNIT 单位
                                     From M_PATIENT";

        /// <summary>
        /// The SQL Select Exam Data
        /// </summary>
        private const string SELECT_EXAMDATA_SQL = @"SELECT [PATIENT_ID] as ID,[PATIENT_NAME] as 姓名,
            性别 = case SEX when 1 Then '男' when 2 then '女' end,[BIRTH] 出生日期,[UNIT] 单位,[EXAM_DATE_TIME] as 检测时间,
            [EXAM_SCENARIO] as 检测情景,[USER_NAME] as 医生,[CFF1],[CFF2],[CFF]
            FROM [V_Export_ExamData]";


        /// <summary>
        /// Gets the base value.
        /// </summary>
        /// <param name="patientKeyList">The patient key list.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetBaseValue(List<int> patientKeyList)
        {
            // List为空结束查询，返回null
            if (patientKeyList.Count == 0)
            {
                return null;
            }

            // 构造Patient_Key的查询条件
            StringBuilder sbCondition = new StringBuilder();
            sbCondition.Append(" in (");
            foreach (int key in patientKeyList)
            {
                sbCondition.Append(key);
                sbCondition.Append(",");
            }
            sbCondition.Remove(sbCondition.Length - 1, 1);
            sbCondition.Append(")");

            string sqlSelect = SELECT_BASEVALUE_SQL + " where PATIENT_KEY" + sbCondition.ToString();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect);

            // 执行Command，返回基础值信息
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetPatient(int patientKey)
        {
            string sqlSelect = SELECT_PATIENT_SQL + " where PATIENT_KEY =" + patientKey.ToString();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect);

            // 执行Command，返回Patient信息
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }


        /// <summary>
        /// Gets the exam data.
        /// </summary>
        /// <param name="examKeyList">The exam key list.</param>
        /// <returns>DataTable.</returns>
        public DataTable GetExamData(List<int> examKeyList)
        {
            // List为空结束查询，返回null
            if (examKeyList.Count == 0)
            {
                return null;
            }

            // 构造Exam_Key查询条件
            StringBuilder sbCondition = new StringBuilder();
            sbCondition.Append(" in (");
            foreach (int key in examKeyList)
            {
                sbCondition.Append(key);
                sbCondition.Append(",");
            }
            sbCondition.Remove(sbCondition.Length - 1, 1);
            sbCondition.Append(")");

            string sqlSelect = SELECT_EXAMDATA_SQL + " where EXAM_KEY " + sbCondition.ToString();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect);

            // 执行Command，返回检查信息
            DataSet dataSet = db.ExecuteDataSet(dbCommand);
            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }
    }
}
