using System;

namespace MedicalSys.Framework
{
    /// <summary>
    /// Enum LogLevel
    /// </summary>
    [Flags]
    public enum LogLevel
    {
        /// <summary>
        /// The INFO
        /// </summary>
        INFO,
        /// <summary>
        /// The WARN
        /// </summary>
        WARN,
        /// <summary>
        /// The ERROR
        /// </summary>
        ERROR,
        /// <summary>
        /// The FATAL
        /// </summary>
        FATAL,
        /// <summary>
        /// The ALL
        /// </summary>
        ALL
    }
}
