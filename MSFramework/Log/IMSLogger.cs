using System;
namespace MedicalSys.Framework
{
    public interface IMSLogger
    {
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(object message);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        void DebugFormat(string format, object arg0);

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void DebugFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void DebugFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(object message);

        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        void InfoFormat(string format, object arg0);

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void InfoFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Infoes the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void InfoFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(object message);

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        void WarnFormat(string format, params object[] args);

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        void WarnFormat(string format, object arg0);

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void WarnFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void WarnFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(object message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        void ErrorFormat(string format, object arg0);

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void ErrorFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void ErrorFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(object message);

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        void FatalFormat(string format, params object[] args);

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        void FatalFormat(string format, object arg0);

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        void FatalFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        void FatalFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Alls the specified format.
        /// </summary>
        /// <param name="message">The message.</param>
        void All(string message);
    }
}