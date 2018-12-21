using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MedicalSys.Framework;
using MedicalSys.MSCommon;
using Microsoft.Reporting.WinForms;
using WearinessExam.DAO;
using WearinessExam.DO;
using WearinessExam.Examination;
using WearinessExam.Report;
using WearinessExam.Report.zhmsdbDataSetTableAdapters;
using WearinessExam.Utility;

namespace WearinessExam
{
    public partial class ReportWindow : MedicalSys.MSCommon.BaseForm
    {
        #region 变量
        /// <summary>
        /// Logger
        /// </summary>
        private IMSLogger m_Logger = LogFactory.GetLogger();

        /// <summary>
        /// ExamInfoDAO
        /// </summary>
        private ExamInfoDAO m_ExamInfoDAO = new ExamInfoDAO();

        /// <summary>
        /// 基础值
        /// </summary>
        private BaseValue DefaultBaseValue { get; set; }

        /// <summary>
        /// CFF低到高数据
        /// </summary>
        private double[] m_CffLow2HighData = null;

        /// <summary>
        /// CFF高到低数据
        /// </summary>
        private double[] m_CffHigh2LowData = null;

        /// <summary>
        /// 瞳孔直径数据(左眼)
        /// </summary>
        private double[] m_PdLeftData = null;

        /// <summary>
        /// 瞳孔直径数据(右眼)
        /// </summary>
        private double[] m_PdRightData = null;
        #endregion 变量

        public ReportWindow()
        {
            InitializeComponent();

            Rectangle rect = System.Windows.Forms.Screen.GetBounds(this);
            this.Height = rect.Height;
            this.Width = 1280;
            this.Load += new EventHandler(ReportWindow_Load);
            this.dgvExamList.SelectionChanged += new EventHandler(dgvExamList_SelectionChanged);
        }


        #region 事件
        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportWindow_Load(object sender, EventArgs e)
        {
            InitExamList();

            FillExamListReport();
        }

        /// <summary>
        /// 打印按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            m_streams.Clear();
            Warning[] warningsMain;

            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>EMF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>2cm</MarginTop>" +
                "  <MarginLeft>1cm</MarginLeft>" +
                "  <MarginRight>1cm</MarginRight>" +
                "  <MarginBottom>2cm</MarginBottom>" +
                "</DeviceInfo>";

            //将报表的内容按照deviceInfo指定的格式输出到CreateStream函数提供的Stream中。
            LocalReport report = reportViewerMain.LocalReport;
            report.Render("Image", deviceInfo, CreateStream, out warningsMain);
            Print();

            // 打印检查结果列表
            if (chkPrintList.Checked)
            {
                deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>EMF</OutputFormat>" +
                "  <PageWidth>21cm</PageWidth>" +
                "  <PageHeight>29.7cm</PageHeight>" +
                "  <MarginTop>2cm</MarginTop>" +
                "  <MarginLeft>2cm</MarginLeft>" +
                "  <MarginRight>2cm</MarginRight>" +
                "  <MarginBottom>2cm</MarginBottom>" +
                "</DeviceInfo>";

                m_streams.Clear();
                Warning[] warningList;

                //将报表的内容按照deviceInfo指定的格式输出到CreateStream函数提供的Stream中。
                report = reportViewerExamList.LocalReport;
                report.Render("Image", deviceInfo, CreateStream, out warningList);
                Print();
            }
        }

        /// <summary>
        /// 导出PDF按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF 文件|*.pdf";
            saveFileDialog.Title = "导出 PDF 文件";

            if (saveFileDialog.ShowDialog() == DialogResult.OK &&
                saveFileDialog.FileName != "")
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                // 导出检测报告
                byte[] bytes = reportViewerMain.LocalReport.Render(
                   "PDF", null, out mimeType, out encoding, out extension,
                   out streamids, out warnings);

                try
                {
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    m_Logger.Error("导出检测报告发生错误", ex);
                }


                // 导出检测结果列表
                if (this.chkPrintList.Checked)
                {
                    bytes = reportViewerExamList.LocalReport.Render(
                       "PDF", null, out mimeType, out encoding, out extension,
                       out streamids, out warnings);

                    int index = saveFileDialog.FileName.Length - 4;
                    string fileName = saveFileDialog.FileName.Insert(index, "_列表");
                    try
                    {
                        FileStream fsList = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                        fsList.Write(bytes, 0, bytes.Length);
                        fsList.Close();
                    }
                    catch (Exception ex)
                    {
                        m_Logger.Error("导出检查结果列表发生错误", ex);
                    }

                }
            }
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvExamList.RowCount <= 0 && dgvExamList.SelectedRows.Count <= 0)
            {
                return;
            }

            if (VerifyInput())
            {
                string summary = this.txtConclusion.Text.Trim();
                string comment = this.txtMemo.Text.Trim();
                this.txtConclusion.ReadOnly = true;
                int examKey = Int32.Parse(dgvExamList.SelectedRows[0].Cells[0].Value.ToString());
                // 将医师结论和备注更新到DB中
                m_ExamInfoDAO.UpdateReportSummary(summary, comment, examKey);

                ReportParameter[] ReportParameters = new ReportParameter[2];
                ReportParameters[0] = new ReportParameter("Conclusion", summary);
                ReportParameters[1] = new ReportParameter("Memo", comment);
                // 为报告设定参数，并更新报告
                reportViewerMain.LocalReport.SetParameters(ReportParameters);
                reportViewerMain.RefreshReport();
            }
        }

        /// <summary>
        /// 退出按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ReportViewer的Load事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void reportViewerMain_Load(object sender, EventArgs e)
        {
            reportViewerMain.ShowToolBar = false;
            reportViewerMain.ShowContextMenu = false;
            reportViewerMain.LocalReport.ReportPath =
                Path.Combine(AssemblyHelper.GetExecuteAssemblyPath() + @"\Report\WearinessExamReport.rdlc");

            if (hHook == 0)
            {
                // Create an instance of HookProc.
                MouseHookProcedure = new HookProc(ReportWindow.MouseHookProc);
                //Control ctrl = this.GetNextControl(this.reportViewerMain, true);
                hHook = SetWindowsHookEx(WH_MOUSE, MouseHookProcedure, IntPtr.Zero, AppDomain.GetCurrentThreadId());
                //If the SetWindowsHookEx function fails.
                if (hHook == 0)
                {
                    m_Logger.Error("SetWindowsHookEx Failed");
                    return;
                }
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the ReportWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void ReportWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hHook != 0)
            {
                bool ret = UnhookWindowsHookEx(hHook);
                //If the UnhookWindowsHookEx function fails.
                if (ret == false)
                {
                    m_Logger.Error("UnhookWindowsHookEx Failed");
                    return;
                }
                hHook = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the ViewChart Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnViewChart_Click(object sender, EventArgs e)
        {
            ChartsWindow chartsWindow = new ChartsWindow();
            // 画曲线
            chartsWindow.UpdateCurve(m_CffLow2HighData, m_CffHigh2LowData,m_PdLeftData, m_PdRightData);
            var bindingSource = reportViewerMain.LocalReport.DataSources["ExamData"].Value as BindingSource;
            var dateSet = bindingSource.DataSource as zhmsdbDataSet;
            var examData = dateSet.M_WEARINESS_EXAM;
            double cff1 = 0d;
            if (!(examData.Rows[0]["CFF1"] is DBNull))
            {
                cff1 = Convert.ToDouble(examData.Rows[0]["CFF1"]);
            }
            double cff2 = 0d;
            if (!(examData.Rows[0]["CFF2"] is DBNull))
            {
                cff2 = Convert.ToDouble(examData.Rows[0]["CFF2"]);
            }
            double pcl1 = 0d;
            if (!(examData.Rows[0]["PCL"] is DBNull))
            {
                pcl1 = Convert.ToDouble(examData.Rows[0]["PCL"]);
            }
            double pcl2 = 0d;
            if (!(examData.Rows[0]["PCL2"] is DBNull))
            {
                pcl2 = Convert.ToDouble(examData.Rows[0]["PCL2"]);
            }
            double pmd1 = 0d;
            if (!(examData.Rows[0]["PMD"] is DBNull))
            {
                pmd1 = Convert.ToDouble(examData.Rows[0]["PMD"]);
            }
            double pmd2 = 0d;
            if (!(examData.Rows[0]["PMD2"] is DBNull))
            {
                pmd2 = Convert.ToDouble(examData.Rows[0]["PMD2"]);
            }
            // 画潜伏期等值
            chartsWindow.DrawLines(cff1, cff2, pcl1, pcl2, pmd1, pmd2);
            chartsWindow.ShowDialog(this);
        }
        #endregion 事件

        #region 检测列表

        /// <summary>
        /// 添加的检测结果列表
        /// </summary>
        public DataTable AddedList { get; set; }

        /// <summary>
        /// 绑定的检测结果列表
        /// </summary>
        private DataTable m_ExamTable;

        /// <summary>
        /// 初始化检测结果列表
        /// </summary>
        private void InitExamList()
        {
            m_ExamTable = new DataTable("Exam");
            DataColumn column;

            // ExamKey
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ExamKey";
            column.AutoIncrement = false;
            column.Caption = "ExamKey";
            column.ReadOnly = true;
            column.Unique = true;
            m_ExamTable.Columns.Add(column);

            // PatientId
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PatientId";
            column.AutoIncrement = false;
            column.Caption = "PatientId";
            column.ReadOnly = true;
            column.Unique = false;
            m_ExamTable.Columns.Add(column);

            // PatientName
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PatientName";
            column.AutoIncrement = false;
            column.Caption = "PatientName";
            column.ReadOnly = true;
            column.Unique = false;
            m_ExamTable.Columns.Add(column);

            // ExamTime
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "ExamTime";
            column.AutoIncrement = false;
            column.Caption = "ExamTime";
            column.ReadOnly = true;
            column.Unique = false;
            m_ExamTable.Columns.Add(column);

            // 设定绑定信息
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = m_ExamTable;
            dgvExamList.DataSource = bindingSource;

            DataRow row;

            // Get data from AddedList DataTable
            foreach (DataRow item in AddedList.Rows)
            {
                row = m_ExamTable.NewRow();
                row["ExamKey"] = item[ConstMessage.COLUMN_EXAM_KEY];
                row["PatientId"] = item[ConstMessage.COLUMN_ID];
                row["PatientName"] = item[ConstMessage.COLUMN_NAME];
                row["ExamTime"] = item[ConstMessage.COLUMN_EXAM_DATETIME];

                try
                {
                    m_ExamTable.Rows.Add(row);
                }
                catch (ConstraintException ex)
                {
                    m_Logger.Error(ex.Message);
                    continue;
                }
            }

            // 设置检测时间格式
            this.dgvExamList.Columns["columnExamTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            // 设置按检测先后排序，最新检测应排在前
            this.dgvExamList.Sort(this.dgvExamList.Columns["columnExamKey"], System.ComponentModel.ListSortDirection.Descending);
            // 设置检测列表交叉背景色
            this.dgvExamList.RowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvExamList.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Cyan;
        }

        /// <summary>
        /// DataGridView的SelectionChanged事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void dgvExamList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExamList.RowCount > 0 && dgvExamList.SelectedRows.Count > 0)
            {
                int examKey = Int32.Parse(dgvExamList.SelectedRows[0].Cells[0].Value.ToString());
                // 选中的检测结果改变时，更新主报表
                FillMainReport(examKey);
            }
        }

        /// <summary>
        /// 填充主报表数据
        /// </summary>
        /// <param name="examKey">检测记录主键</param>
        private void FillMainReport(int examKey)
        {
            string connectionString = IniSettingConfig.GetInstance().DatabaseDescription;
            var connection = new System.Data.SqlClient.SqlConnection(connectionString);
            zhmsdbDataSet dsZhmsdb = new zhmsdbDataSet();

            // 查询检测数据
            try
            {
                var examAdapter = new M_WEARINESS_EXAMTableAdapter();
                examAdapter.Connection = connection;
                examAdapter.FillByExamKey(dsZhmsdb.M_WEARINESS_EXAM, examKey);
            }
            catch (Exception ex)
            {
                m_Logger.Error("查询检测数据出错。");
                m_Logger.Error(ex.Message);
            }

            int patientKey = 0;
            int userKey = 0;
            if (dsZhmsdb.M_WEARINESS_EXAM.Rows.Count > 0)
            {
                patientKey = Convert.ToInt32(dsZhmsdb.M_WEARINESS_EXAM.Rows[0]["PATIENT_KEY"]);
                bool success = Int32.TryParse(dsZhmsdb.M_WEARINESS_EXAM.Rows[0]["USER_KEY"].ToString(), out userKey);

                try
                {
                    // 查询受测员信息
                    var patientAdapter = new M_PATIENTTableAdapter();
                    patientAdapter.Connection = connection;
                    patientAdapter.FillByPatientKey(dsZhmsdb.M_PATIENT, patientKey);

                    // 查询医生信息
                    if (success)
                    {
                        var userAdapter = new M_USERTableAdapter();
                        userAdapter.Connection = connection;
                        userAdapter.FillByUserKey(dsZhmsdb.M_USER, userKey);
                    }

                    // 查询基础值信息
                    var baseValueAdapter = new M_WEARINESS_BASE_VALUETableAdapter();
                    baseValueAdapter.Connection = connection;
                    baseValueAdapter.FillByPatientKey(dsZhmsdb.M_WEARINESS_BASE_VALUE, patientKey);
                }
                catch (Exception ex)
                {
                    m_Logger.Error("查询受测员信息出错。");
                    m_Logger.Error(ex.Message);
                }

                // 当没有设定基础值时，获取默认的基础值
                if (dsZhmsdb.M_WEARINESS_BASE_VALUE.Rows.Count == 0)
                {
                    var row = dsZhmsdb.M_WEARINESS_BASE_VALUE.NewM_WEARINESS_BASE_VALUERow();
                    row.USER_KEY = userKey;
                    row.PATIENT_KEY = patientKey;
                    SetDefaultBaseValue(row);
                    dsZhmsdb.M_WEARINESS_BASE_VALUE.AddM_WEARINESS_BASE_VALUERow(row);
                }

                string strDataFileName = dsZhmsdb.M_WEARINESS_EXAM.Rows[0]["DATA_FILE_NAME"].ToString();

                // 设置曲线数据
                //SetChartData(dsZhmsdb, strDataFileName);
                SetAllChartData(dsZhmsdb, strDataFileName);

                // 设置ReportDataSource
                SetReportDataSources(dsZhmsdb);

                // 获取医生结论和备注
                string summary = Convert.ToString(dsZhmsdb.M_WEARINESS_EXAM.Rows[0]["REPORT_SARMARY"]);
                string comment = Convert.ToString(dsZhmsdb.M_WEARINESS_EXAM.Rows[0]["REPORT_COMMENT"]);

                ReportParameter[] ReportParameters = CreateReportParameters(summary, comment);
                reportViewerMain.ProcessingMode = ProcessingMode.Local; 
                reportViewerMain.LocalReport.ReportEmbeddedResource =
                    Path.Combine(AssemblyHelper.GetExecuteAssemblyPath() + @"\Report\WearinessExamReport.rdlc");
                // 为报告设定参数
                reportViewerMain.LocalReport.SetParameters(ReportParameters);
                // 刷新报表中的需要呈现的数据
                reportViewerMain.RefreshReport();

                // 更新画面
                this.txtConclusion.Text = summary;
                if (!string.IsNullOrEmpty(summary))
                {
                    this.txtConclusion.ReadOnly = true;
                }
                else
                {
                    this.txtConclusion.ReadOnly = false;
                }
                this.txtMemo.Text = comment;

                // 设置Button状态
                SetButtonsEnable(true);
            }
            else
            {
                SetButtonsEnable(false);
            }

        }

        private void SetAllChartData(zhmsdbDataSet dsZhmsdb, string dataFileName)
        {
            if (!DataFileHelper.CheckFileExist(dataFileName))
            {
                m_Logger.Info(string.Format("受测员 ID={0} 原始数据不存在。", dsZhmsdb.M_PATIENT.Rows[0]["PATIENT_ID"]));
                return;
            }
            DataFileHelper dataFileHelper = new DataFileHelper(dataFileName);
            m_CffLow2HighData = dataFileHelper.GetCffLow2HighSerials();
            m_CffHigh2LowData = dataFileHelper.GetCffHigh2LowSerials();
            m_PdLeftData = dataFileHelper.GetPdLeftSerials();
            m_PdRightData = dataFileHelper.GetPdRightSerials();
            ChartsWindow chartsWindow = new ChartsWindow();
            // 画曲线
            chartsWindow.UpdateCurve(m_CffLow2HighData, m_CffHigh2LowData, m_PdLeftData, m_PdRightData);

            var examData = dsZhmsdb.M_WEARINESS_EXAM;
            double cff1 = 0d;
            if (!(examData.Rows[0]["CFF1"] is DBNull))
            {
                cff1 = Convert.ToDouble(examData.Rows[0]["CFF1"]);
            }
            double cff2 = 0d;
            if (!(examData.Rows[0]["CFF2"] is DBNull))
            {
                cff2 = Convert.ToDouble(examData.Rows[0]["CFF2"]);
            }

            double pcl1 = 0d;
            if (!(examData.Rows[0]["PCL"] is DBNull))
            {
                pcl1 = Convert.ToDouble(examData.Rows[0]["PCL"]);
            }
            double pcl2 = 0d;
            if (!(examData.Rows[0]["PCL2"] is DBNull))
            {
                pcl2 = Convert.ToDouble(examData.Rows[0]["PCL2"]);
            }
            double pmd1 = 0d;
            if (!(examData.Rows[0]["PMD"] is DBNull))
            {
                pmd1 = Convert.ToDouble(examData.Rows[0]["PMD"]);
            }
            double pmd2 = 0d;
            if (!(examData.Rows[0]["PMD2"] is DBNull))
            {
                pmd2 = Convert.ToDouble(examData.Rows[0]["PMD2"]);
            }

            // 画潜伏期等值
            chartsWindow.DrawLines(cff1, cff2, pcl1, pcl2, pmd1, pmd2);

            var row = dsZhmsdb.Images.NewImagesRow();
            row.Cff = chartsWindow.GetCffImageData();
            row.Pd = chartsWindow.GetPupilExamImageData();
            dsZhmsdb.Images.AddImagesRow(row);
            chartsWindow.Close();
        }

        /// <summary>
        /// Create Report Parameters
        /// </summary>
        /// <param name="summary">医师结论</param>
        /// <param name="comment">备注</param>
        /// <returns></returns>
        private ReportParameter[] CreateReportParameters(string summary, string comment)
        {
            ReportParameter[] reportParameters = new ReportParameter[2];

            reportParameters[0] = new ReportParameter("Conclusion", summary);
            reportParameters[1] = new ReportParameter("Memo", comment);
            //reportParameters[2] = new ReportParameter("CffDevMax", IniSettingConfig.GetInstance().CFF_Dev_Max);
            //reportParameters[3] = new ReportParameter("CffDevMin", IniSettingConfig.GetInstance().CFF_Dev_Min);

            return reportParameters;
        }

        /// <summary>
        /// 设置ReportDataSource
        /// </summary>
        /// <param name="dsZhmsdb">数据集</param>
        private void SetReportDataSources(zhmsdbDataSet dsZhmsdb)
        {
            reportViewerMain.LocalReport.DataSources.Clear();

            // 受测员信息
            BindingSource bsPatientData = new BindingSource();
            bsPatientData.DataMember = "M_PATIENT";
            bsPatientData.DataSource = dsZhmsdb;
            // 根据BindingSource设置ReportDataSource
            ReportDataSource rdsPatientData = new ReportDataSource();
            rdsPatientData.Name = "PatientData";
            rdsPatientData.Value = bsPatientData;
            reportViewerMain.LocalReport.DataSources.Add(rdsPatientData);

            // 医生信息
            BindingSource bsUserData = new BindingSource();
            bsUserData.DataMember = "M_USER";
            bsUserData.DataSource = dsZhmsdb;
            // 根据BindingSource设置ReportDataSource
            ReportDataSource rdsUserData = new ReportDataSource();
            rdsUserData.Name = "UserData";
            rdsUserData.Value = bsUserData;
            reportViewerMain.LocalReport.DataSources.Add(rdsUserData);

            // 基础值
            BindingSource bsBaseValue = new BindingSource();
            bsBaseValue.DataMember = "M_WEARINESS_BASE_VALUE";
            bsBaseValue.DataSource = dsZhmsdb;
            // 根据BindingSource设置ReportDataSource
            ReportDataSource rdsBaseValue = new ReportDataSource();
            rdsBaseValue.Name = "BaseValue";
            rdsBaseValue.Value = bsBaseValue;
            reportViewerMain.LocalReport.DataSources.Add(rdsBaseValue);

            // 检测数据
            BindingSource bsExamData = new BindingSource();
            bsExamData.DataMember = "M_WEARINESS_EXAM";
            bsExamData.DataSource = dsZhmsdb;
            // 根据BindingSource设置ReportDataSource
            ReportDataSource rdsExamData = new ReportDataSource();
            rdsExamData.Name = "ExamData";
            rdsExamData.Value = bsExamData;
            reportViewerMain.LocalReport.DataSources.Add(rdsExamData);

            // Images Data
            BindingSource bsImagesData = new BindingSource();
            bsImagesData.DataMember = "Images";
            bsImagesData.DataSource = dsZhmsdb;
            // 根据BindingSource设置ReportDataSource
            ReportDataSource rdsImagesData = new ReportDataSource();
            rdsImagesData.Name = "ImagesData";
            rdsImagesData.Value = bsImagesData;
            reportViewerMain.LocalReport.DataSources.Add(rdsImagesData);
        }

        /// <summary>
        /// Sets the default base value.
        /// </summary>
        /// <param name="row">The row.</param>
        private void SetDefaultBaseValue(zhmsdbDataSet.M_WEARINESS_BASE_VALUERow row)
        {
            if (DefaultBaseValue == null)
            {
                DefaultBaseValue = BaseValueHelper.GetDefaultBaseValue();
            }

            row.CFF = Convert.ToDecimal(DefaultBaseValue.CFF);
            row.VALUE_KEY = 0;
        }
        #endregion 检测列表

        #region 打印报表

        //声明一个Stream对象的列表用来保存报表的输出数据
        //LocalReport对象的Render方法会将报表按页输出为多个Stream对象。
        private List<Stream> m_streams = new List<Stream>();

        //用来提供Stream对象的函数，用于LocalReport对象的Render方法的第三个参数。
        private Stream CreateStream(string name, string fileNameExtension,
          Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        //用来记录当前打印到第几页了
        private int m_currentPageIndex;

        private void Print()
        {
            m_currentPageIndex = 0;

            if (m_streams == null || m_streams.Count == 0)
                return;

            //声明PrintDocument对象用于数据的打印
            PrintDocument printDoc = new PrintDocument();
            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机
            printDoc.PrinterSettings.PrinterName = IniSettingConfig.GetInstance().PrinterName;
            //判断指定的打印机是否可用
            if (!printDoc.PrinterSettings.IsValid)
            {
                string strErrorMessage = string.Format(ConstMessage.MESSAGE_M1301, printDoc.PrinterSettings.PrinterName);
                m_Logger.Error(strErrorMessage);
                MessageBox.Show(this, strErrorMessage, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            //执行打印操作，Print方法将触发PrintPage事件。
            printDoc.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            //Metafile对象用来保存EMF或WMF格式的图形，
            //在前面将报表的内容输出为EMF图形格式的数据流。
            m_streams[m_currentPageIndex].Position = 0;
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            //指定是否横向打印
            ev.PageSettings.Landscape = false;
            //这里的Graphics对象实际指向了打印机
            ev.Graphics.DrawImage(pageImage, new Rectangle(0, 0, ev.PageBounds.Width, ev.PageBounds.Height));
            m_streams[m_currentPageIndex].Close();
            m_currentPageIndex++;
            //设置是否需要继续打印
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        #endregion 打印报表

        #region 私有函数
        /// <summary>
        /// 填充检测结果报表数据
        /// </summary>
        private void FillExamListReport()
        {
            // 设置报表文件路径
            reportViewerExamList.LocalReport.ReportPath =
                Path.Combine(AssemblyHelper.GetExecuteAssemblyPath() + @"\Report\ExamDataListReport.rdlc");

            reportViewerExamList.LocalReport.DataSources.Clear();

            DsExamDataList dsExamDataList = new DsExamDataList();
            DsExamDataList.EXAMRow examRow;

            // 根据检测结果列表更新数据
            foreach (DataRow item in AddedList.Rows)
            {
                examRow = dsExamDataList.EXAM.NewEXAMRow();

                 examRow["CFF"] = item[ConstMessage.COLUMN_CFF];
                examRow["ID"] = item[ConstMessage.COLUMN_ID];
                examRow["出生日期"] = item[ConstMessage.COLUMN_BIRTHDAY];
                examRow["单位"] = item[ConstMessage.COLUMN_UNIT];
                examRow["检测情景"] = item[ConstMessage.COLUMN_EXAM_SCENARIO];
                examRow["检测时间"] = item[ConstMessage.COLUMN_EXAM_DATETIME];
                examRow["姓名"] = item[ConstMessage.COLUMN_NAME];
                examRow["性别"] = item[ConstMessage.COLUMN_SEX];
                examRow["医生"] = item[ConstMessage.COLUMN_PATIENT_KEY];

                dsExamDataList.EXAM.AddEXAMRow(examRow);
            }

            // 创建BindingSource
            BindingSource bsExamDataList = new BindingSource();
            bsExamDataList.DataMember = "EXAM";
            bsExamDataList.DataSource = dsExamDataList;

            // 根据BindingSource设置ReportDataSource
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ExamDataList";
            reportDataSource.Value = bsExamDataList;

            reportViewerExamList.LocalReport.DataSources.Add(reportDataSource);
            // 刷新报表中的需要呈现的数据
            reportViewerExamList.RefreshReport();
        }

        /// <summary>
        /// Sets the buttons enable.
        /// </summary>
        /// <param name="isEnable">if set to <c>true</c> [is enable].</param>
        private void SetButtonsEnable(bool isEnable)
        {
            this.btnExportPdf.Enabled = isEnable;
            this.btnPrint.Enabled = isEnable;
            this.btnSave.Enabled = isEnable;
        }

        /// <summary>
        /// Verifies the input.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool VerifyInput()
        {
            bool result = false;
            string strConclusion = txtConclusion.Text.Trim();

            // 判断医师结论是否填写
            if (string.IsNullOrEmpty(strConclusion))
            {
                txtConclusion.Focus();
            }
            else
            {
                result = true;
            }

            if (!result)
            {
                // 画面输入项目有任何一项为空时，弹出对话框。
                MessageBox.Show(this, ConstMessage.MESSAGE_M1406, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        // 去掉右键菜单
        private static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam.ToInt32() == WM_RBUTTONUP)
                return BREAK_PROCEDURE;
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }


        #endregion 私有函数

        #region Win32 API

        const int WM_RBUTTONUP = 0x0205;
        const int WM_RBUTTONDOWN = 0X0204;
        const int BREAK_PROCEDURE = -1;

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        //Declare the hook handle as an int.
        static int hHook = 0;

        //Declare the mouse hook constant.
        //For other hook types, you can obtain these values from Winuser.h in the Microsoft SDK.
        public const int WH_MOUSE = 7;

        //Declare MouseHookProcedure as a HookProc type.
        HookProc MouseHookProcedure;

        //Declare the wrapper managed POINT clcass.
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        //Declare the wrapper managed MouseHookStruct class.
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        //This is the Import for the SetWindowsHookEx function.
        //Use this function to install a thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //This is the Import for the UnhookWindowsHookEx function.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //This is the Import for the CallNextHookEx function.
        //Use this function to pass the hook information to the next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        #endregion Win32 API



    }
}
