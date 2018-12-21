using System.IO;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class MSAppContext
    /// </summary>
   public static  class MSAppContext
    {
        /// <summary>
        /// The m_ MS app root path
        /// </summary>
       static string m_MSAppRootPath;
       /// <summary>
       /// The logger
       /// </summary>
        private static IMSLogger logger = LogFactory.GetLogger(typeof(MSAppContext));

        /// <summary>
        /// Initializes static members of the <see cref="MSAppContext"/> class.
        /// </summary>
        static MSAppContext()
        {
            m_MSAppRootPath = Path.GetFullPath(AssemblyHelper.GetExecuteAssemblyPath()) + @"\";

            logger.DebugFormat("Set MSAppRootPath to [{0}]", m_MSAppRootPath);
        }

        /// <summary>
        /// Gets or sets the root path.
        /// </summary>
        /// <value>The  root path.</value>
        public static string MSAppRootPath
        {
            get
            {
                return m_MSAppRootPath; 
            }

            set
            {
                m_MSAppRootPath = Path.GetFullPath(value) + @"\"; 
            }
        }

    }
}
