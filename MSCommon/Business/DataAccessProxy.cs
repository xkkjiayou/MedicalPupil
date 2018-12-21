using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MedicalSys.Framework;

namespace MedicalSys.MSCommon
{
    /// <summary>
    /// Class DataAccessProxy
    /// </summary>
    public class DataAccessProxy
    {
        private static IMSLogger m_logger = LogFactory.GetLogger();

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="fun">The function.</param>
        /// <param name="form">The form.</param>
        /// <returns><c>true</c> if succeed</returns>
        public static bool Execute(Action fun, Form form)
        {
            bool result = true;
            try
            {
                fun();
            }
            catch (Exception ex)
            {
                m_logger.Fatal(ExceptionHelper.GetExceptionMessage(ex));
                ShowMessageBox(form);

                result = false;
            }
            return result;
        }


        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="fun">The function.</param>
        /// <param name="form">The form.</param>
        /// <param name="funResult">The function result.</param>
        /// <returns><c>true</c> if succeed</returns>
        public static bool Execute(Func<bool> fun, Form form, out Boolean funResult)
        {
            bool result = true;
            funResult = false;
            try
            {
                funResult = fun();
            }
            catch (Exception ex)
            {
                m_logger.Fatal(ExceptionHelper.GetExceptionMessage(ex));
                ShowMessageBox(form);

                result = false;
            }
            return result;
        }


        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="fun">The function.</param>
        /// <param name="form">The form.</param>
        /// <param name="funResult">The function result.</param>
        /// <returns><c>true</c> if succeed</returns>
        public static bool Execute<T>(Func<T> fun, Form form, out T funResult)
        {
            bool result = true;
            funResult = default(T);
            try
            {
                funResult = fun();
            }
            catch (Exception ex)
            {
                m_logger.Fatal(ExceptionHelper.GetExceptionMessage(ex));
                ShowMessageBox(form);

                result = false;
            }
            return result;
        }

        /// <summary>
        /// Executes the specified function.
        /// </summary>
        /// <param name="fun">The function.</param>
        /// <param name="form">The form.</param>
        /// <param name="funResult">The function result.</param>
        /// <returns><c>true</c> if succeed</returns>
        public static bool Execute<T>(Func<List<T>> fun, Form form, out List<T> funResult)
        {
            bool result = true;
            funResult = null;
            try
            {
                funResult = fun();
            }
            catch (Exception ex)
            {
                m_logger.Fatal(ExceptionHelper.GetExceptionMessage(ex));
                ShowMessageBox(form);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="form">The form.</param>
        public static void ShowMessageBox(Form form)
        {
            MessageBox.Show(form, CommonMessage.M0009, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
