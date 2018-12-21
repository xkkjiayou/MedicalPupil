using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class WearinessExamConfig
    /// </summary>
    public class WearinessExamConfig
    {
        /// <summary>
        /// The WearinessExamConfig instance.
        /// </summary>
        private static WearinessExamConfig m_Instance = new WearinessExamConfig();

        /// <summary>
        /// The INI Configuration
        /// </summary>
        private IniConfiguration m_INIConfiguration;

        /// <summary>
        /// The ini file path
        /// </summary>
        private string m_IniFilePath = string.Empty;

        /// <summary>
        /// The ini file name
        /// </summary>
        private const string FILE_NAME = "WearinessExam.ini";
        /// <summary>
        /// The Printer Section
        /// </summary>
        private const string PRINTER_SECTION = "Printer";
        /// <summary>
        /// The Other Section
        /// </summary>
        private const string OTHER_SECTION = "Other";
        /// <summary>
        /// The BaseValue Section
        /// </summary>
        private const string BASEVALUE_SECTION = "DefaultBaseValue";
        /// <summary>
        /// The Deviation Scope
        /// </summary>
        private const string DEVIATION_SCOPE = "DeviationScope";

        /// <summary>
        /// Prevents a default instance of the <see cref="WearinessExamConfig"/> class from being created.
        /// </summary>
        private WearinessExamConfig()
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
        /// <returns>WearinessExamConfig.</returns>
        public static WearinessExamConfig GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new WearinessExamConfig();
            }

            return m_Instance;
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
        /// Gets or sets the auto run.
        /// </summary>
        /// <value>auto run.</value>
        public string AutoRun
        {
            get
            {
                return m_INIConfiguration.GetValue(OTHER_SECTION, "AutoRun", "0");
            }
            set
            {
                if (value != "0" && value != "1")
                {
                    value = "0";
                }
                m_INIConfiguration.WriteValue(OTHER_SECTION, "AutoRun", value);
            }
        }

        /// <summary>
        /// Gets or sets the scenario.
        /// </summary>
        /// <value>Scenario.</value>
        public string Scenario
        {
            get
            {
                return m_INIConfiguration.GetValue(OTHER_SECTION, "Scenario", String.Empty);
            }
            set
            {
                m_INIConfiguration.WriteValue(OTHER_SECTION, "Scenario", value);
            }
        }

        /// <summary>
        /// 系统自动保护时间(分钟)
        /// </summary>
        /// <value>The auto protect minute.</value>
        public int AutoProtectMinute
        {
            get
            {
                int result;
                Int32.TryParse(m_INIConfiguration.GetValue(OTHER_SECTION, "AutoProtectMinute", "30"), out result);
                return result;
            }
        }

        /// <summary>
        /// 扫视速度错误波峰判断阈值
        /// </summary>
        /// <value>The SV threshold value.</value>
        public double SvThresholdValue
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(OTHER_SECTION, "SvThresholdValue", "150"), out result);
                return result;
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
                string result = m_INIConfiguration.GetValue(OTHER_SECTION, "SelectedEyeData", "L");

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
                double.TryParse(m_INIConfiguration.GetValue(OTHER_SECTION, "PclThresholdValue", "0"), out result);
                return result;
            }
        }

        /// <summary>
        /// 眼扫视求取潜伏期值的阈值
        /// </summary>
        /// <value>The SV threshold value.</value>
        public double SlThresholdValue
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(OTHER_SECTION, "SlThresholdValue", "0"), out result);
                return result;
            }
        }

        #region 默认基础值

        /// <summary>
        /// Gets or sets the AWI.
        /// </summary>
        /// <value>AWI.</value>
        public double AWI
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "AWI", "22.1"), out result);
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
        /// Gets or sets the PCL.
        /// </summary>
        /// <value>PCL</value>
        public double PCL
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "PCL", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the PCR.
        /// </summary>
        /// <value>PCR.</value>
        public double PCR
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "PCR", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the PCV.
        /// </summary>
        /// <value>PCV.</value>
        public double PCV
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "PCV", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the PID.
        /// </summary>
        /// <value>PID.</value>
        public double PID
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "PID", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the SL.
        /// </summary>
        /// <value>SL.</value>
        public double SL
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "SL", "22.1"), out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the SV.
        /// </summary>
        /// <value>SV.</value>
        public double SV
        {
            get
            {
                double result;
                double.TryParse(m_INIConfiguration.GetValue(BASEVALUE_SECTION, "SV", "22.1"), out result);
                return result;
            }
        }

        #endregion 默认基础值

        #region 偏差值范围

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
        /// Gets the SL dev max.
        /// </summary>
        /// <value>The SL dev max.</value>
        public string SL_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "SL_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the SL dev min.
        /// </summary>
        /// <value>The SL dev min.</value>
        public string SL_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "SL_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the SV dev max.
        /// </summary>
        /// <value>The SV dev max.</value>
        public string SV_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "SV_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the SV dev min.
        /// </summary>
        /// <value>The SV dev min.</value>
        public string SV_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "SV_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the PID dev max.
        /// </summary>
        /// <value>The PID dev max.</value>
        public string PID_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PID_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the PID dev min.
        /// </summary>
        /// <value>The PID dev min.</value>
        public string PID_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PID_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the PCV dev max.
        /// </summary>
        /// <value>The PCV dev max.</value>
        public string PCV_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCV_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the PCV dev min.
        /// </summary>
        /// <value>The PCV dev min.</value>
        public string PCV_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCV_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the PCL dev max.
        /// </summary>
        /// <value>The PCL dev max.</value>
        public string PCL_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCL_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the PCL dev min.
        /// </summary>
        /// <value>The PCL dev min.</value>
        public string PCL_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCL_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the PCR max.
        /// </summary>
        /// <value>The PCR dev max.</value>
        public string PCR_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCR_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the PCR Dev min.
        /// </summary>
        /// <value>The PCR dev min.</value>
        public string PCR_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "PCR_Dev_Min", "-0.2");
            }
        }

        /// <summary>
        /// Gets the AWI dev max.
        /// </summary>
        /// <value>The AWI dev max.</value>
        public string AWI_Dev_Max
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "AWI_Dev_Max", "0.2");
            }
        }

        /// <summary>
        /// Gets the AWI dev min.
        /// </summary>
        /// <value>The AW dev min.</value>
        public string AWI_Dev_Min
        {
            get
            {
                return m_INIConfiguration.GetValue(DEVIATION_SCOPE, "AWI_Dev_Min", "-0.2");
            }
        }

        #endregion 偏差值范围


    }
}
