using System;
using System.Text;

namespace MedicalSys.Framework
{
    public class ExceptionHelper
    {
        /// <summary>
        /// Gets the exception detail information.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>ExceptionDetailInformation</returns>
        public static string GetExceptionDetailInformation(Exception ex)
        {
            StringBuilder buffer = new StringBuilder();
            GetExceptionDetailInformation(ex, buffer);
            return buffer.ToString();
        }

        /// <summary>
        /// Gets the exception detail information.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="buffer">The buffer.</param>
        private static void GetExceptionDetailInformation(Exception ex, StringBuilder buffer)
        {
            if (ex == null) return;

            buffer.Append(ex.GetType().FullName).Append(": ").Append(ex.Message).Append(Environment.NewLine);
            buffer.Append(ex.StackTrace).Append(Environment.NewLine);
            GetExceptionDetailInformation(ex.InnerException, buffer);
        }

        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        public static string GetExceptionMessage(Exception ex)
        {
            if (ex != null)
            {
                return string.IsNullOrEmpty(ex.Message) ? string.Empty : ex.Message;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
