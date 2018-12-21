using MedicalSys.DataAccessor;

namespace MedicalSys.MSCommon
{
    /// <summary>
    /// Class LoginInfoManager
    /// </summary>
    public class LoginInfoManager
    {
        /// <summary>
        /// The current user
        /// </summary>
        public static User m_currentUser = new User();

        /// <summary>
        /// The check flag in login form
        /// </summary>
        public static bool m_checked = false;

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>The current user.</value>
        public static User CurrentUser
        {
            get
            {
                return m_currentUser;
            }

            set
            {
                m_currentUser = value;
            }
        }

        /// <summary>
        /// Gets or sets the check flag
        /// </summary>
        /// <value>check flag</value>
        public static bool IsChecked
        {
            get
            {
                return m_checked;
            }

            set
            {
                m_checked = value;
            }
        }

        public static bool LogoutStatus
        {
            get;
            set;

        }

        public static bool SelfCheckStatus
        {
            get;
            set;
        }

        public static bool AutoProtectStatus
        {
            get;
            set;
        }
    }
}
