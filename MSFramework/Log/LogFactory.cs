using System;
using System.Collections;
using System.IO;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class LogFactory
    /// </summary>
    public class LogFactory
    {
        /// <summary>
        /// The m_ logger
        /// </summary>
        private static readonly IMSLogger m_Logger = LogFactory.GetLogger("LogFactory");


        /// <summary>
        /// Initializes static members of the <see cref="LogFactory"/> class.
        /// </summary>
        static LogFactory()
        {
#if DEBUG
            RollingFileAppender rfAppender = new RollingFileAppender();
            rfAppender.Name = "RootRollingFileAppender";
            rfAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            rfAppender.AppendToFile = true;
            rfAppender.MaxSizeRollBackups = 10;
            rfAppender.StaticLogFileName = true;
            rfAppender.File = string.Format("{0}_debug.log", AssemblyHelper.GetStartAssemblyName());
            rfAppender.MaxFileSize = 2 * 1000 * 1000;

            PatternLayout patternLayout = new PatternLayout("%date [%thread] %-5level %logger - %message%newline");
            patternLayout.Header = "[Header] " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff") + Environment.NewLine;
            patternLayout.Footer = "[Footer]" + Environment.NewLine;

            rfAppender.Layout = patternLayout;
            rfAppender.ActivateOptions();

            ConsoleAppender cAppender = new ConsoleAppender();
            cAppender.Name = "RootConsoleAppender";
            cAppender.Layout = new PatternLayout("%date{HH:mm:ss,ffff} %-5level - %message%newline");

            //ColoredConsoleAppender ccAppender = new ColoredConsoleAppender();
            //ccAppender.Name = "RootColoredConsoleAppender";
            //ccAppender.Layout = new PatternLayout("%date{HH:mm:ss,ffff} %-5level - %message%newline");

            Logger logger = ((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root;
            logger.AddAppender(rfAppender);
            logger.AddAppender(cAppender);
            logger.Level = Level.Debug;
            logger.Repository.Configured = true;
#endif

        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>IMSLogger</returns>
        public static IMSLogger GetLogger(Type type)
        {
            ILog log = LogManager.GetLogger(type);

            return new Log4netLoggerAdapter(log);
        }


        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>IMSLogger</returns>
        public static IMSLogger GetLogger()
        {
            ILog log = LogManager.GetLogger(AssemblyHelper.GetStartAssemblyName());

            return new Log4netLoggerAdapter(log);
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="loggername">The loggername.</param>
        /// <returns>IMSLogger</returns>
        public static IMSLogger GetLogger(string loggername)
        {
            ILog log = LogManager.GetLogger(loggername);

            return new Log4netLoggerAdapter(log);
        }


        /// <summary>
        /// Configs the logger with default pattern("%message%newline").
        /// </summary>
        /// <param name="logFileFullName">Full name of the log file.</param>
        public static void ConfigLogger(string logFileFullName)
        {
            ConfigLogger(AssemblyHelper.GetStartAssemblyName(), logFileFullName);
        }


        /// <summary>
        /// Configs the logger with default pattern("%message%newline").
        /// </summary>
        /// <param name="loggername">The loggername.</param>
        /// <param name="logFileFullName">Full name of the log file.</param>
        public static void ConfigLogger(string loggername, string logFileFullName)
        {
            ConfigLogger(loggername, logFileFullName, "%date [%thread] %-5level %logger - %message%newline");
        }

        /// <summary>
        /// Configs the logger.
        /// </summary>
        /// <param name="loggername">The loggername.</param>
        /// <param name="logFileFullName">Full name of the log file.</param>
        /// <param name="pattern">The pattern.</param>
        public static void ConfigLogger(string loggername, string logFileFullName, string pattern)
        {
            try
            {
                string filePath = GetFilePath(logFileFullName);

                MSRollingFileAppender rfAppender = null;

                // use same file path appender, so use filepath as key
                if (m_LogAppenders.ContainsKey(filePath))
                {
                    rfAppender = (MSRollingFileAppender)m_LogAppenders[filePath];
                    //Change roll file size
                    rfAppender.MaxFileSize = 2 * 1000 * 1000;// 1000 * IniLogConfig.GetInstance().MAXIMUM_FILE_SIZE;
                }
                else
                {
                    m_Logger.DebugFormat("Configure log [{0}], path is [{1}]", loggername, filePath);
                    rfAppender = GetRfAppender(filePath, pattern);
                    m_LogAppenders.Add(filePath, rfAppender);
                }


                Logger logger = (Logger)LogManager.GetLogger(loggername).Logger;
                SetLoggerLevel(logger);

                //Set logger appender
                bool flag = true;

                IAppender[] appenders = new IAppender[logger.Appenders.Count];

                int m = 0;
                foreach (IAppender appender in logger.Appenders)
                {
                    appenders[m] = appender;
                    m++;
                }

                for (int i = 0; i < appenders.Length; i++)
                {
                    IAppender appender = appenders[i];

                    MSRollingFileAppender a1 = appender as MSRollingFileAppender;

                    if (a1 != null)
                    {
                        flag = false;

                        if (a1.File != filePath)
                        {
                            if (a1.File != null && m_LogAppenders.ContainsKey(a1.File))
                            {
                                m_LogAppenders.Remove(a1.File);
                                a1.Close();
                            }
                            m_Logger.Debug("Remove appender. " + a1.File);
                            logger.RemoveAppender(a1);
                            m_Logger.Debug("Add appender. " + rfAppender.File);
                            logger.AddAppender(rfAppender);
                        }
                    }
                }

                if (flag)
                {
                    m_Logger.Debug("Add appender. " + rfAppender.File);
                    logger.AddAppender(rfAppender);
                }

                logger.Repository.Configured = true;
            }
            catch (Exception ex)
            {
                try
                {
                    StringBuilder buffer = new StringBuilder();
                    buffer.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffff"));
                    buffer.Append(ExceptionHelper.GetExceptionDetailInformation(ex));
                    File.WriteAllText("_LogError.log", buffer.ToString());
                }
                catch
                {

                }

            }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="logFileFullName">Full name of the log file.</param>
        /// <returns>System.String.</returns>
        private static string GetFilePath(string logFileFullName)
        {
            string filePath;
            if (Path.IsPathRooted(logFileFullName))
            {
                filePath = logFileFullName;
            }
            else
            {
                string logPath = MSAppContext.MSAppRootPath + logFileFullName;
                filePath = Path.GetFullPath(logPath);
            }
            return filePath;
        }

        /// <summary>
        /// Sets the logger level.
        /// </summary>
        /// <param name="logger">The logger.</param>
        private static void SetLoggerLevel(Logger logger)
        {
            switch (IniSettingConfig.GetInstance().LOG_LEVEL)
            {
                case LogLevel.FATAL:
                    logger.Level = Level.Fatal;
                    break;
                case LogLevel.ERROR:
                    logger.Level = Level.Error;
                    break;
                case LogLevel.WARN:
                    logger.Level = Level.Warn;
                    break;
                case LogLevel.INFO:
                    logger.Level = Level.Info;
                    break;
                default:
                    logger.Level = Level.Error;
                    break;
            }
        }

        /// <summary>
        /// Gets the rf appender.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>RollingFileAppender.</returns>
        private static MSRollingFileAppender GetRfAppender(string filePath, string pattern)
        {
            MSRollingFileAppender rfAppender = new MSRollingFileAppender();
            rfAppender.CountDirection = 1;
            rfAppender.Name = Path.GetFileName(filePath) + "_RollingFileAppender";
            rfAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            rfAppender.DatePattern = "_yyyy-MM-dd";
            rfAppender.AppendToFile = true;
            rfAppender.MaxSizeRollBackups = 10000000;
            rfAppender.StaticLogFileName = true;
            rfAppender.File = filePath;
            rfAppender.Encoding = Encoding.UTF8;
            rfAppender.MaxFileSize =  2 * 1000 * 1000;//1024 * IniLogConfig.GetInstance().MAXIMUM_FILE_SIZE;
            PatternLayout patternLayout = new PatternLayout(pattern);
            patternLayout.ConversionPattern = "%d [%t] %-5p %c [%x] - %m%n%n";
            rfAppender.Layout = patternLayout;

            rfAppender.ActivateOptions();
            return rfAppender;
        }

        /// <summary>
        /// The m_ log appenders
        /// </summary>
        private static Hashtable m_LogAppenders = Hashtable.Synchronized(new Hashtable());
    }
}
