using System;
using System.Data;

namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Class DbDataReaderMapping
    /// </summary>
    public class DbDataReaderMapping
    {
        private IDataReader m_DbDataReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataReaderMapping"/> class.
        /// </summary>
        public DbDataReaderMapping()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDataReaderMapping"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public DbDataReaderMapping(IDataReader reader)
        {
            m_DbDataReader = reader;
        }

        #region IDbMapping Members

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>string of DbDataReader</returns>
        public string GetString(string columnName)
        {
            return m_DbDataReader[columnName].ToString();
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>string of DbDataReader</returns>
        public string GetStringForMapping(string columnName)
        {

            if (m_DbDataReader[columnName] == DBNull.Value)
            {
                return null;
            }
            return m_DbDataReader[columnName].ToString();
        }

        /// <summary>
        /// Gets the int.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>value of m_DbDataReader</returns>
        public int GetInt(string columnName)
        {
            string v = m_DbDataReader[columnName].ToString();
            int o = 0;
            return Int32.TryParse(v, out o) ? o : 0;
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>true or false</returns>
        public bool GetBool(string columnName)
        {
            bool blnIsBool = false;
            if (m_DbDataReader[columnName] != null
                && m_DbDataReader[columnName].ToString().Equals("1"))
            {
                blnIsBool = true;
            }

            return blnIsBool;
        }

        /// <summary>
        /// Gets the float.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>value of m_DbDataReader</returns>
        public float GetFloat(string columnName)
        {
            string v = m_DbDataReader[columnName].ToString();
            float o = 0;
            return float.TryParse(v, out o) ? o : 0;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>value of m_DbDataReader</returns>
        public DateTime GetDateTime(string columnName)
        {
            string v = m_DbDataReader[columnName].ToString();
            if (string.IsNullOrEmpty(v))
            {
                return new DateTime();
            }

            return DateTime.Parse(m_DbDataReader[columnName].ToString());
        }

        #endregion

        /// <summary>
        /// Sets the db data reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void SetDbDataReader(IDataReader reader)
        {
            m_DbDataReader = reader;
        }
    }
}
