using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MedicalSys.DataAccessor
{
    class DatabaseHelper
    {
        private const string SQL_SYS_DATETIME = "select getdate();";
        /// <summary>
        /// Execute the scalar.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>value of m_DbDataReader</returns>
        public static object ExecuteScalar(string sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand getCmd = db.GetSqlStringCommand(sql);
            object result = db.ExecuteScalar(getCmd);

            return result;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns>result of sql execute</returns>
        public static object ExecuteNonQuery(string sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand getCmd = db.GetSqlStringCommand(sql);
            object result = db.ExecuteNonQuery(getCmd);

            return result;
        }


        /// <summary>
        /// Gets the sys date time.
        /// </summary>
        /// <returns>System.DateTime</returns>
        public static System.DateTime GetSysDateTime()
        {
            try
            {
                System.DateTime dt = System.DateTime.Parse(ExecuteScalar(SQL_SYS_DATETIME).ToString());
                return dt;
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }
    }
}
