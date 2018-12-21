using System;
using log4net;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Class Log4netLoggerAdapter
    /// </summary>
    public class Log4netLoggerAdapter : IMSLogger
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4netLoggerAdapter"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public Log4netLoggerAdapter(ILog log)
        {
            this.logger = log;
        }

        #region IFXthis.logger Members

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            System.Diagnostics.Debug.WriteLine(message);

            if (this.logger.IsDebugEnabled)
                this.logger.Debug(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Debug(object message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(message + Environment.NewLine + exception.StackTrace);
            if (this.logger.IsDebugEnabled)
                this.logger.Debug(message, exception);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void DebugFormat(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, args));

            if (this.logger.IsDebugEnabled)
                this.logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void DebugFormat(string format, object arg0)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, arg0));

            if (this.logger.IsDebugEnabled)
                this.logger.DebugFormat(format, arg0);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void DebugFormat(string format, object arg0, object arg1)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, arg0, arg1));

            if (this.logger.IsDebugEnabled)
                this.logger.DebugFormat(format, arg0, arg1);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            System.Diagnostics.Debug.WriteLine(string.Format(format, arg0, arg1, arg2));

            if (this.logger.IsDebugEnabled)
                this.logger.DebugFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.Info(message);
        }

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Info(object message, Exception exception)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.Info(message, exception);
        }

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void InfoFormat(string format, params object[] args)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void InfoFormat(string format, object arg0)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.InfoFormat(format, arg0);
        }

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void InfoFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.InfoFormat(format, arg0, arg1);
        }

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsInfoEnabled)
                this.logger.InfoFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(object message)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.Warn(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warn(object message, Exception exception)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.Warn(message, exception);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void WarnFormat(string format, params object[] args)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.WarnFormat(format, args);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void WarnFormat(string format, object arg0)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.WarnFormat(format, arg0);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void WarnFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.WarnFormat(format, arg0, arg1);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsWarnEnabled)
                this.logger.WarnFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.Error(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.Error(message, exception);
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.ErrorFormat(format, args);
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void ErrorFormat(string format, object arg0)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.ErrorFormat(format, arg0);
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void ErrorFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.ErrorFormat(format, arg0, arg1);
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsErrorEnabled)
                this.logger.ErrorFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(object message)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.Fatal(message);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.Fatal(message, exception);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void FatalFormat(string format, params object[] args)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.FatalFormat(format, args);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void FatalFormat(string format, object arg0)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.FatalFormat(format, arg0);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void FatalFormat(string format, object arg0, object arg1)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.FatalFormat(format, arg0, arg1);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            if (this.logger.IsFatalEnabled)
                this.logger.FatalFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// Alls the specified format.
        /// </summary>
        /// <param name="message">The message.</param>
        public void All(string message)
        {
            logger.Fatal(message);
        }

        #endregion
    }
}
