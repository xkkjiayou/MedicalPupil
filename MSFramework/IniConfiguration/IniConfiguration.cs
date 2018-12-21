using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class IniConfiguration
    /// </summary>
    internal class IniConfiguration
    {

        /// <summary>
        /// The DEFAULT VERSION
        /// </summary>
        public readonly static string DEFAULT_VERSION = string.Empty;

        /// <summary>
        /// The DEFAULT SECTION NAME
        /// </summary>
        public const string DEFAULT_SECTION_NAME = "default";

        /// <summary>
        /// Initializes a new instance of theINIConfiguration class.
        /// </summary>
        /// <param name="iniFilePath">The ini file path.</param>
        private IniConfiguration(string iniFilePath)
        {
            FilePath = iniFilePath;
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets the ini configuration instance.
        /// </summary>
        /// <param name="iniFilePath">The ini file path.</param>
        /// <returns>configuration</returns>
        public static IniConfiguration GetIniConfigurationInstance(string iniFilePath)
        {
            IniConfiguration configuration = new IniConfiguration(iniFilePath);

            return configuration;
        }


        /// <summary>
        /// Gets the private profile string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="def">The def.</param>
        /// <param name="retVal">The ret val.</param>
        /// <param name="size">The size.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>value by int</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);

        /// <summary>
        /// Write the private profile string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="val">The write string.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The return value.</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, 
            string key, string val, string filePath);

        /// <summary>
        /// read ini value.
        /// </summary>
        /// <param name="Section">The section.</param>
        /// <param name="Key">The key.</param>
        /// <param name="iniFilePath">The ini file path.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>value by string</returns>
        private string ReadINIValue(string Section, string Key, string iniFilePath, string defaultValue)
        {
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, defaultValue,temp,
                                            255, iniFilePath);
            return temp.ToString();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>iniValue</returns>
        public string GetValue(string section, string key, string defaultValue)
        {
            string iniValue = ReadINIValue(section, key, FilePath, defaultValue);

            if (string.IsNullOrEmpty(iniValue))
            {
                return defaultValue;
            }

            return iniValue;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>value</returns>
        public string GetValue(string section, string key)
        {
            return GetValue(section, key, DEFAULT_VERSION);

        }

        /// <summary>
        /// Write the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The writed value.</param>
        public void WriteValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, this.FilePath);
        }
    }
}
