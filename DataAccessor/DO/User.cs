
namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Class User
    /// </summary>
    public class User : IDataObject
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
        /// Gets or sets the login ID.
        /// </summary>
        /// <value>The login ID.</value>
        public string LoginID
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get;
            set;
        }
    }
}
