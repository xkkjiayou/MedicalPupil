using System;

namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Class Patient
    /// </summary>
    public class Patient : IDataObject
    {
        // age
        private string iAge;

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
        /// Gets or sets the sex.
        /// </summary>
        /// <value>The sex.</value>
        public int Sex
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the birth.
        /// </summary>
        /// <value>The birth.</value>
        public DateTime Birth
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>The unit.</value>
        public string Unit
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>The age.</value>
        public string Age
        {
            get { return iAge; }
            set
            {
                if (Convert.ToInt32(value) > 200)
                {
                    iAge = string.Empty;
                }
                else
                {
                    iAge = value;
                }
            }
        }
    }
}
