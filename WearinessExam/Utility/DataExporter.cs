using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MedicalSys.Framework.Utility;
using MedicalSys.MSCommon;

namespace WearinessExam.Utility
{
    /// <summary>
    /// Class DataExporter
    /// </summary>
    public class DataExporter : IBackGroundWorkerObject
    {
        /// <summary>
        /// The backgound workder
        /// </summary>
        private BackgroundWorker m_BackgoundWorkder;

        /// <summary>
        /// The data table list
        /// </summary>
        private List<DataTable> m_DataTableList;

        /// <summary>
        /// The file path
        /// </summary>
        private string m_FilePath;

        /// <summary>
        /// The Processing Message.
        /// </summary>
        private const string PROCCESSING_MESSAGE = "正在导出数据，请等待 ......";

        /// <summary>
        /// The completed message
        /// </summary>
        private string m_CompletedMessage = "数据导出完成！";

        /// <summary>
        /// Initializes a new instance of the <see cref="DataExporter"/> class.
        /// </summary>
        /// <param name="dataTableList">The data table list.</param>
        /// <param name="filePath">The file path.</param>
        public DataExporter(List<DataTable> dataTableList,string filePath)
        {
            m_DataTableList = dataTableList;
            m_FilePath = filePath;
        }

        /// <summary>
        /// Does the export work.
        /// </summary>
        public void DoWork()
        {
            bool result = ExcelHelper.ToExcelSheet(m_DataTableList, m_FilePath, ConstMessage.TEMPLATE_NAME);
            if (!result)
            {
                m_CompletedMessage = "数据导出发生错误!";
            }
        }
        /// <summary>
        /// Gets or sets the background worker.
        /// </summary>
        /// <value>The background worker.</value>
        public BackgroundWorker BackgroundWorker
        {
            get
            {
                return m_BackgoundWorkder;
            }
            set
            { m_BackgoundWorkder = value; }
        }

        /// <summary>
        /// Gets the proccessing message.
        /// </summary>
        /// <value>The proccessing message.</value>
        public string ProccessingMessage
        {
            get { return PROCCESSING_MESSAGE; }
        }

        /// <summary>
        /// Gets the completed message.
        /// </summary>
        /// <value>The completed message.</value>
        public string CompletedMessage
        {
            get { return m_CompletedMessage; }
        }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get;
            set;
        }
    }
}
