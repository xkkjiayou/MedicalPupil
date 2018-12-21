using MedicalSys.DataAccessor;
using MedicalSys.Framework;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WearinessExam
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //初始化Log
            LogFactory.ConfigLogger(Path.Combine(Path.Combine(MSAppContext.MSAppRootPath, "Log"), "MSApp.log"));
            IMSLogger logger = LogFactory.GetLogger();

            //设置Application 中没有捕获异常的处理方法。
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            DAOConfiguration.InitialDatabaseConfiguration();
            Application.SetCompatibleTextRenderingDefault(false);

            //系统二重启动判断
            bool createdNew;
            System.Threading.Mutex instance = new System.Threading.Mutex(true, "瞳孔分析仪软件系统", out createdNew);

            if (createdNew)
            {
                logger.Info("=====================================================================================");
                logger.Info("=============================     应用程序启动      =================================");
                logger.Info("=====================================================================================");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LoginForm());
                //Application.Run(new WearinessDetectForm());
            }
            else
            {
                //系统二重启动，弹出错误信息
                MessageBox.Show(CommonMessage.M0001, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Error(CommonMessage.M0001);
                Application.Exit();
            }

            logger.Info("=============================     应用程序关闭      ==================================");
        }


        /// <summary>
        /// Handles the ThreadException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Threading.ThreadExceptionEventArgs"/> instance containing the event data.</param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {

                MessageBox.Show("不可恢复的系统异常，应用程序将退出！");
                IMSLogger logger = LogFactory.GetLogger();
                logger.Fatal(ExceptionHelper.GetExceptionDetailInformation(e.Exception));

            }
            finally
            {
                Application.Exit();

            }
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                MessageBox.Show("不可恢复的系统异常，应用程序将退出！");
                IMSLogger logger = LogFactory.GetLogger();
                logger.Fatal(ExceptionHelper.GetExceptionMessage(e.ExceptionObject as Exception));
            }
            finally
            {
                Application.Exit();

            }

        }

        static void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                // 退出系统时关闭所有设备。
                bool ret = WearinessExam.Examination.DeviceFacade.CloseDevices();
                if (!ret)
                {
                    IMSLogger logger = LogFactory.GetLogger();
                    logger.Error("退出系统,关闭设备失败!");
                }
            }
            finally
            {

            }
        }


    }
}
