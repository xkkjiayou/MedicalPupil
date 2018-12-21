using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class IniSettingConfig
    /// </summary>
    public class IniSettingConfig
    {
        /// <summary>
        /// The IniSettingConfig instance
        /// </summary>
        private static IniSettingConfig m_Instance = new IniSettingConfig();

        /// <summary>
        /// The INI configuration
        /// </summary>
        private IniConfiguration m_INIConfiguration;

        /// <summary>
        /// The ini file path
        /// </summary>
        private string m_IniFilePath = string.Empty;

        /// <summary>
        /// Default value of data_source
        /// </summary>
        private const string DEFAULT_DATA_SOURCE = "MedicalSys";
        /// <summary>
        /// Default value of user_id
        /// </summary>
        private const string DEFAULT_USER_ID = "sa";
        /// <summary>
        /// Default value of password
        /// </summary>
        private const string DEFAULT_PASSWORD = "Win2003@";

        /// <summary>
        /// log_level
        /// </summary>
        private const string DEFAULT_LOG_LEVEL = "INFO";

        /// <summary>
        /// The BaseValue Section
        /// </summary>
        private const string BASEVALUE_SECTION = "DefaultBaseValue";

        /// <summary>
        /// The Printer Section
        /// </summary>
        private const string PRINTER_SECTION = "Printer";

        /// <summary>
        /// The Deviation Scope
        /// </summary>
        private const string DEVIATION_SCOPE = "DeviationScope";

        /// <summary>
        /// The file name
        /// </summary>
        private const string FILE_NAME = "MSApp.ini";

        /// <summary>
        /// Prevents a default instance of the <see cref="IniSettingConfig"/> class from being created.
        /// </summary>
        private IniSettingConfig()
        {
            m_IniFilePath = Path.GetFullPath(Path.Combine(MSAppContext.MSAppRootPath, FILE_NAME));
            m_INIConfiguration = IniConfiguration.GetIniConfigurationInstance(IniFilePath);

        }


        /// <summary>
        /// Gets or sets the ini file path.
        /// </summary>
        /// <value>The ini file path.</value>
        public string IniFilePath
        {
            get { return m_IniFilePath; }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>IniSettingConfig.</returns>
        public static IniSettingConfig GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new IniSettingConfig();
            }

            return m_Instance;
        }

        /// <summary>
        /// Gets or sets the systemtype.
        /// </summary>
        /// <value>systemtype.</value>
        public string SystemType
        {
            get
            {
                return m_INIConfiguration.GetValue("System", "systemtype", String.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the HospitalName.
        /// </summary>
        /// <value>HospitalName</value>
        public string HospitalName
        {
            get
            {
                return m_INIConfiguration.GetValue("System", "hospitalname", String.Empty);
            }
        }
        /// <summary>
        /// Gets or sets the System Name.
        /// </summary>
        /// <value>SystemName.</value>
        public string SystemName
        {
            get
            {
                string systemType = m_INIConfiguration.GetValue("System", "systemtype", String.Empty);
                return m_INIConfiguration.GetValue(systemType, "systemname", String.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>version</value>
        public string Version
        {
            get
            {
                string systemType = m_INIConfiguration.GetValue("System", "systemtype", String.Empty);
                return m_INIConfiguration.GetValue(systemType, "version", String.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>imagepath</value>
        public string ImagePath
        {
            get
            {
                string systemType = m_INIConfiguration.GetValue("System", "systemtype", String.Empty);
                return m_INIConfiguration.GetValue(systemType, "BackgroudImage", String.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the About Image.
        /// </summary>
        /// <value>AboutImage.</value>
        public string AboutImage
        {
            get
            {
                return m_INIConfiguration.GetValue("System", "aboutimage", String.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the printer name.
        /// </summary>
        /// <value>printer name.</value>
        public string PrinterName
        {
            get
            {
                return m_INIConfiguration.GetValue(PRINTER_SECTION, "Name", String.Empty);
            }
            set
            {
                m_INIConfiguration.WriteValue(PRINTER_SECTION, "Name", value);
            }
        }

        /// <summary>
        /// Gets or sets the printer region.
        /// </summary>
        /// <value>printer region.</value>
        public string PrinterRegion
        {
            get
            {
                return m_INIConfiguration.GetValue(PRINTER_SECTION, "Region", "0");
            }
            set
            {
                if (value != "0" && value != "1")
                {
                    value = "0";
                }
                m_INIConfiguration.WriteValue(PRINTER_SECTION, "Region", value);
            }
        }

        /// <summary>
        /// 亮点颜色
        /// </summary>
        public int BrightPointColour
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "bright_point_colour", String.Empty));
            }
        }

        /// <summary>
        /// 亮黑比
        /// </summary>
        public int BrightBlackRatio
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "bright_black_ratio", String.Empty));
            }
        }

        /// <summary>
        /// 亮点强度
        /// </summary>
        public int BrightPointIntensity
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "bright_point_intensity", String.Empty));
            }
        }

        /// <summary>
        /// 背景光强度
        /// </summary>
        public int BackgroundIntensity
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "background_intensity", String.Empty));
            }
        }

        /// <summary>
        /// 检测情景
        /// </summary>
        public string ExamScenario
        {
            get
            {
                return m_INIConfiguration.GetValue("System", "Exam_Scenario", String.Empty);
            }
        }

        /// <summary>
        /// COM端口
        /// </summary>
        public string ComPortName
        {
            get
            {
                return m_INIConfiguration.GetValue("System", "ComPortName", String.Empty);
            }
        }

        /// <summary>
        /// 刺激光源颜色
        /// </summary>
        public int StimulateLightColour
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "stimulate_light_colour", String.Empty));
            }
        }

        /// <summary>
        /// 刺激时长
        /// </summary>
        public int StimulateTime
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "stimulate_time", String.Empty));
            }
        }

        /// <summary>
        /// 刺激强度
        /// </summary>
        public int StimulateStrength
        {
            get
            {
                return int.Parse(m_INIConfiguration.GetValue("System", "stimulate_strength", String.Empty));
            }
        }

        /// <summary>
        /// 计算或绘图时选取左眼或右眼数据
        /// </summary>
        /// <value>The SV threshold value.</value>
        public string SelectedEyeData
        {
            get
            {
                string result = m_INIConfiguration.GetValue("System", "SelectedEyeData", "L");

                if (result != "L" && result != "R")
                {
                    result = "L";
                }
                return result;
            }
        }

        /// <summary>
        /// 瞳孔对光反应求取潜伏期的阈值
        /// </summary>
        /// <value>The SV threshold value.</value>
        public double PclThresholdValue
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue("System", "PclThresholdValue", "0"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the CFF.
        /// </summary>
        /// <value>CFF.</value>
        public double CFF
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "CFF", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets the CFF dev max.
        /// </summary>
        /// <value>The CFF dev max.</value>
        public string CFF_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "CFF_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the CFF dev min.
        /// </summary>
        /// <value>The CFF dev min.</value>
        public string CFF_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "CFF_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Deals the database description.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="password">The password.</param>
        /// <returns>source</returns>
        private string DealDatabaseDescription(string dataSource, string userID, string password)
        {
            StringBuilder source = new StringBuilder();
            source.Append("Data Source = ").Append(dataSource).Append(";");
            source.Append("Initial Catalog= ").Append("zhmsdb").Append(";");
            source.Append("User ID = ").Append(userID).Append(";");
            source.Append("Password = ").Append(password).Append(
                ";MultipleActiveResultSets=True");
            return source.ToString();
        }

        /// <summary>
        /// Gets the database description.<br />
        /// db_description
        /// </summary>
        /// <value>The database description.</value>
        public string DatabaseDescription
        {
            get
            {
                string result = m_INIConfiguration.GetValue("Database", "db_description");
                string DataSource = m_INIConfiguration.GetValue("Database", "data_source", DEFAULT_DATA_SOURCE);
                string UserID = m_INIConfiguration.GetValue("Database", "user_id", DEFAULT_USER_ID);
                string Password = m_INIConfiguration.GetValue("Database", "password", DEFAULT_PASSWORD);
                return DealDatabaseDescription(DataSource, UserID, Password);
            }
        }

        /// <summary>
        /// Gets the LO g_ LEVEL.
        /// </summary>
        /// <value>The LO g_ LEVEL.</value>
        public LogLevel LOG_LEVEL
        {
            get
            {
                LogLevel logLevel = LogLevel.INFO;
                string result = string.Empty;
                try
                {
                    result = m_INIConfiguration.GetValue("Log", "log_level", DEFAULT_LOG_LEVEL);
                    if (!result.Trim().ToUpper().Equals("INFO") && !result.Trim().ToUpper().Equals("WARN")
                        && !result.Trim().ToUpper().Equals("ERROR") && !result.Trim().ToUpper().Equals("FATAL"))
                    {
                        result = DEFAULT_LOG_LEVEL;
                    }
                }
                catch
                {
                    result = DEFAULT_LOG_LEVEL;
                    if (result.Trim().ToUpper().Equals("INFO"))
                    {
                        logLevel = LogLevel.INFO;
                    }
                    if (result.Trim().ToUpper().Equals("WARN"))
                    {
                        logLevel = LogLevel.WARN;
                    }
                    if (result.Trim().ToUpper().Equals("ERROR"))
                    {
                        logLevel = LogLevel.ERROR;
                    }
                    if (result.Trim().ToUpper().Equals("FATAL"))
                    {
                        logLevel = LogLevel.FATAL;
                    }
                    return logLevel;
                }
                if (result.Trim().ToUpper().Equals("INFO"))
                {
                    logLevel = LogLevel.INFO;
                }
                if (result.Trim().ToUpper().Equals("WARN"))
                {
                    logLevel = LogLevel.WARN;
                }
                if (result.Trim().ToUpper().Equals("ERROR"))
                {
                    logLevel = LogLevel.ERROR;
                }
                if (result.Trim().ToUpper().Equals("FATAL"))
                {
                    logLevel = LogLevel.FATAL;
                }
                return logLevel;
            }
        }
    }
}
