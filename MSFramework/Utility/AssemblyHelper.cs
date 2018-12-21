using System.IO;
using System.Reflection;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class AssemblyHelper
    /// </summary>
    public class AssemblyHelper
    {
        static IMSLogger logger = LogFactory.GetLogger("AssemblyHelper");

        /// <summary>
        /// Gets the execute assembly path.
        /// </summary>
        /// <returns>DirectoryName</returns>
        public static string GetExecuteAssemblyPath()
        {
            string executeAssemblyPath = Assembly.GetExecutingAssembly().Location;
            logger.DebugFormat("Execute assembly path is [{0}]", executeAssemblyPath);
            logger.DebugFormat("Execute assembly folder is [{0}]", Path.GetDirectoryName(executeAssemblyPath));
            return Path.GetDirectoryName(executeAssemblyPath);
        }

        /// <summary>
        /// Gets the start name of the assembly.
        /// </summary>
        /// <returns>System.String.</returns>
        internal static string GetStartAssemblyName()
        {
            string versionString = "unknownWin32Assembly";

            try
            {
                versionString = Assembly.GetEntryAssembly().ManifestModule.Name;
            }
            catch
            {
            }

            return versionString;
        }
    }
}
