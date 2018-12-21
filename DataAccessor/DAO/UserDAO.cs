using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Data accessor object for user
    /// </summary>
    public class UserDAO : IDAO<User>
    {

        private const string COLUMN_USER_NAME = "USER_NAME";
        private const string COLUMN_LOGIN_ID = "LOGIN_ID";
        private const string COLUMN_USER_ID = "USER_ID";
        private const string COLUMN_USER_KEY = "USER_KEY";
        private const string COLUMN_USER_LEVEL = "USER_LEVEL";
        private const string COLUMN_PASSWORD = "PASSWORD";

        private const string SQL_SELECTALL = "select [USER_KEY],[LOGIN_ID],[USER_NAME],[USER_ID],[USER_LEVEL],[PASSWORD] from M_User";
        private const string SQL_SELECTALL_DOCTORADMIN = "select [USER_KEY],[LOGIN_ID],[USER_NAME],[USER_ID],[USER_LEVEL],[PASSWORD] from M_User where [USER_LEVEL] in (1,2)";
        private const string SQL_UPDATE = "update [M_User] Set [LOGIN_ID] =@LOGIN_ID ,[USER_NAME] =@USER_NAME,[USER_ID] =@USER_ID, [USER_LEVEL]=@USER_LEVEL,[PASSWORD] =@PASSWORD from M_User Where  [USER_KEY] = @USER_KEY";
        private const string SQL_DELETE = "Delete from [M_User] Where  [USER_KEY] = @USER_KEY";
        private const string SQL_INSERT = "Insert into [M_User] ([LOGIN_ID] ,[USER_NAME],[USER_ID],[USER_LEVEL],[PASSWORD]) Values (@LOGIN_ID,@USER_NAME,@USER_ID, @USER_LEVEL ,@PASSWORD  )";
        private const string SQL_SELECT_USERID = "select count(*) from M_User where [USER_ID] = @USER_ID And [USER_LEVEL] in (1,2)";
        private const string SQL_SELECT_LOGINID = "select count(*) from M_User where [LOGIN_ID] = @LOGIN_ID";
        private const string SQL_SELECT_USERID_PRIAMRY = "select count(*) from M_User where [USER_ID] = @USER_ID And [USER_KEY] <> @USER_KEY And [USER_LEVEL] in (1,2)";
        private const string SQL_SELECT_LOGINID_PRIAMRY = "select count(*) from M_User where [LOGIN_ID] = @LOGIN_ID And [USER_KEY] <> @USER_KEY";
        private const string SQL_SELECT_PATIENT = "select [USER_KEY],[LOGIN_ID],[USER_NAME],[USER_ID],[USER_LEVEL],[PASSWORD] from M_User where [USER_ID] = @USER_ID And [USER_LEVEL] = 3";
        private const string SQL_SELECT_PATIENT_ID = "select count(*) from M_User where [USER_ID] = @USER_ID And [USER_LEVEL] = 3";
        private const string SQL_DELETE_PATIENT = "Delete from [M_User] Where  [USER_ID] = @USER_ID And [USER_LEVEL] = 3";


        /// <summary>
        /// Get all user data
        /// </summary>
        /// <returns>List{User}.</returns>
        public List<User> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();
            //DbCommand dbCommand = db.GetSqlStringCommand(sql);
            IRowMapper<User> rowMapper = CreateRowMapper();

            List<User> userList = db.ExecuteSqlStringAccessor(SQL_SELECTALL, rowMapper).ToList();
            return userList;
        }

        /// <summary>
        /// Get all user data
        /// </summary>
        /// <returns>List{User}.</returns>
        public List<User> GetAllDoctorAndAdmin()
        {
            Database db = DatabaseFactory.CreateDatabase();
            //DbCommand dbCommand = db.GetSqlStringCommand(sql);
            IRowMapper<User> rowMapper = CreateRowMapper();

            List<User> userList = db.ExecuteSqlStringAccessor(SQL_SELECTALL_DOCTORADMIN, rowMapper).ToList();
            return userList;
        }

        /// <summary>
        /// Get patient data
        /// </summary>
        /// <returns>List{User}.</returns>
        public List<User> GetPatient(string userID)
        {
            List<User> userList = new List<User>();
            User user = new User();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_PATIENT);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, userID);

            DataSet userDataSet = db.ExecuteDataSet(dbCommand);
            DataTable dataTable = new DataTable();

            dataTable = userDataSet.Tables[0];
            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                user.primaryKey = (Int32)dataTable.Rows[0][COLUMN_USER_KEY];
                user.LoginID = dataTable.Rows[0][COLUMN_LOGIN_ID].ToString();
                user.Name = dataTable.Rows[0][COLUMN_USER_NAME].ToString();
                user.ID = dataTable.Rows[0][COLUMN_USER_ID].ToString();
                user.Level = (Int32)dataTable.Rows[0][COLUMN_USER_LEVEL];
                user.Password = dataTable.Rows[0][COLUMN_PASSWORD].ToString();
                userList.Add(user);
            }
            return userList;
        }

        /// <summary>
        /// Creates the row mapper.
        /// </summary>
        /// <returns>IRowMapper{User}.</returns>
        private IRowMapper<User> CreateRowMapper()
        {
            IRowMapper<User> rowMapper = MapBuilder<User>.MapAllProperties()
                .Map(b => b.Name).ToColumn(COLUMN_USER_NAME)
                .Map(b => b.LoginID).ToColumn(COLUMN_LOGIN_ID)
                .Map(b => b.ID).ToColumn(COLUMN_USER_ID)
                .Map(b => b.Level).ToColumn(COLUMN_USER_LEVEL)
                .Map(b => b.Password).ToColumn(COLUMN_PASSWORD)
                .Map(b => b.primaryKey).ToColumn(COLUMN_USER_KEY).Build();
            return rowMapper;
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Update(User user)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_UPDATE);
            db.AddInParameter(dbCommand, COLUMN_LOGIN_ID, DbType.String, user.LoginID);
            db.AddInParameter(dbCommand, COLUMN_USER_NAME, DbType.String, user.Name);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, user.ID);
            db.AddInParameter(dbCommand, COLUMN_USER_LEVEL, DbType.Int32, user.Level);
            db.AddInParameter(dbCommand, COLUMN_PASSWORD, DbType.String, user.Password);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, user.primaryKey);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Delete(User user)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_DELETE);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, user.primaryKey);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void DeletePatient(User user)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_DELETE_PATIENT);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, user.ID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Inserts the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Insert(User user)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_INSERT);

            db.AddInParameter(dbCommand, COLUMN_LOGIN_ID, DbType.String, user.LoginID);
            db.AddInParameter(dbCommand, COLUMN_USER_NAME, DbType.String, user.Name);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, user.ID);
            db.AddInParameter(dbCommand, COLUMN_USER_LEVEL, DbType.Int32, user.Level);
            db.AddInParameter(dbCommand, COLUMN_PASSWORD, DbType.String, user.Password);
            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Determines whether [has exist user ID] [the specified user ID].
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns><c>true</c> if [has exist user ID] [the specified user ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistUserID(string userID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_USERID);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, userID);

            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }

        /// <summary>
        /// Determines whether [has exist login ID] [the specified login ID].
        /// </summary>
        /// <param name="loginID">The login ID.</param>
        /// <returns><c>true</c> if [has exist login ID] [the specified login ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistLoginID(string loginID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_LOGINID);
            db.AddInParameter(dbCommand, COLUMN_LOGIN_ID, DbType.String, loginID);

            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }

        /// <summary>
        /// Determines whether [has exist user ID] [the specified user ID].
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="primaryKey">The primary key.</param>
        /// <returns><c>true</c> if [has exist user ID] [the specified user ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistUserID(string userID, int primaryKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_USERID_PRIAMRY);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, userID);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, primaryKey);
            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }

        /// <summary>
        /// Determines whether [has exist login ID] [the specified login ID].
        /// </summary>
        /// <param name="loginID">The login ID.</param>
        /// <param name="primaryKey">The primary key.</param>
        /// <returns><c>true</c> if [has exist login ID] [the specified login ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistLoginID(string loginID, int primaryKey)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_LOGINID_PRIAMRY);
            db.AddInParameter(dbCommand, COLUMN_LOGIN_ID, DbType.String, loginID);
            db.AddInParameter(dbCommand, COLUMN_USER_KEY, DbType.Int32, primaryKey);
            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }

        /// <summary>
        /// Determines whether [has exist user ID] [the specified user ID].
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <returns><c>true</c> if [has exist user ID] [the specified user ID]; otherwise, <c>false</c>.</returns>
        public bool HasExistPatientID(string userID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand(SQL_SELECT_PATIENT_ID);
            db.AddInParameter(dbCommand, COLUMN_USER_ID, DbType.String, userID);

            object result = db.ExecuteScalar(dbCommand);
            bool hasExist = false;
            if (Convert.ToInt32(result) > 0)
            {
                hasExist = true;
            }

            return hasExist;
        }


    }
}
