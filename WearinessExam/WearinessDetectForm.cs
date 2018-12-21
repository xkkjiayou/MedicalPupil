using GxIAPINET;
using MedicalSys.Framework;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WearinessExam.DAO;
using WearinessExam.DO;
using WearinessExam.Examination;
using WearinessExam.Utility;

namespace WearinessExam
{
    public partial class WearinessDetectForm : MedicalSys.MSCommon.BaseForm
    {
        #region 变量
        /// <summary>
        /// The logger
        /// </summary>
        private IMSLogger m_Logger = LogFactory.GetLogger();
        /// <summary>
        /// The Messager
        /// </summary>
        private MyMessager msg = new MyMessager();
        /// <summary>
        /// The PROTECT TIMES
        /// </summary>
        private int PROTECT_TIMES;
        /// <summary>
        /// The Exam DAO
        /// </summary>
        ExamInfoDAO m_ExamDao = new ExamInfoDAO();
        /// <summary>
        /// The INITIAL TEXT
        /// </summary>
        private string INITIAL_TEXT = "--";
        /// <summary>
        /// The CURVE LABEL CFF2
        /// </summary>
        private const string CURVE_LABEL_CFF2 = "高到低";
        /// <summary>
        /// The CURVE LABEL CFF1
        /// </summary>
        private const string CURVE_LABEL_CFF1 = "低到高";
        /// <summary>
        /// The CURVE LABEL SV
        /// </summary>
        private const string CURVE_LABEL_SV = "SV";
        /// <summary>
        /// The CURVE LABEL PCL
        /// </summary>
        private const string CURVE_LABEL_PCL = "PCL（左眼）";
        /// <summary>
        /// The CURVE LABEL PCL
        /// </summary>
        private const string CURVE_LABEL_PCL2 = "PCL（右眼）";
        /// <summary>
        /// The TITLE CFF
        /// </summary>
        private const string TITLE_CFF = "闪光融合频率曲线";
        /// <summary>
        /// The TITLE CFF X Axis
        /// </summary>
        private const string TITLE_CFF_X = "T (s)";
        /// <summary>
        /// The TITLE CFF Y Axis
        /// </summary>
        private const string TITLE_CFF_Y = "f (Hz)";
        /// <summary>
        /// The TITLE PCL
        /// </summary>
        private const string TITLE_PCL = "瞳孔对光反应曲线";
        /// <summary>
        /// The TITLE PCL X Axis
        /// </summary>
        private const string TITLE_PCL_X = "T(ms)";
        /// <summary>
        /// The TITLE PCL Y Axis
        /// </summary>
        private const string TITLE_PCL_Y = "d (mm)";
        /// <summary>
        /// The position left eye
        /// </summary>
        private static Point PositionLeftEye;
        /// <summary>
        /// The position right eye
        /// </summary>
        private static Point PositionRightEye;
        /// <summary>
        /// The lock object
        /// </summary>
        private static object m_LockObject = new object();

        private Timer m_Timer = new Timer();

        #endregion 变量


        public WearinessDetectForm()
        {
            InitializeComponent();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = false;
            this.AutoScroll = true;
            this.BackgroundImage = null;
            Rectangle rect = System.Windows.Forms.Screen.GetBounds(this);
            this.Width = 1024;
            this.Height = rect.Height;
            this.FormClosed += WearinessDetectForm_FormClosed;

            m_CFF1TimeExcutor.Draw += DrawCFF1Curve;
            m_CFF1TimeExcutor.ExcuteCompletedHandle += CFF1Completed;
            m_CFF2TimeExcutor.Draw += DrawCFF2Curve;
            m_CFF2TimeExcutor.ExcuteCompletedHandle += CFF2Completed;
            m_PupilTrackTimeExcutor.ExcuteCompletedHandle += PTCompleted;
            m_PupilExam.CompletedHandler += PupilExam_CompletedHandler;
            ExamDataCenter.ExamWindow = this;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #region 图像捕捉显示
        /// <summary>
        /// The video open status
        /// </summary>
        private bool videoOpenStatus = false;

        /// <summary>
        /// The lock image object
        /// </summary>
        private object lockImageObject = new object();
        /// <summary>
        /// Delegate InvodeDelegate
        /// </summary>
        delegate void InvodeDelegate();
 
        /// <summary>
        /// Starts the video.
        /// </summary>
        private void StartVideo()
        {
            //设置相机帧率
            DeviceFacade.camerTrigger(1, 300);
            //启动人眼图像采集
            DeviceFacade.StartCaptureDoubleImage();
            DeviceFacade.CaptureDoubleImage(DeviceFacade.onLeftCallback, DeviceFacade.onRightCallback);

        }

        private void DeviceFacade_updateLeftImage(Bitmap bitmap)
        {
            if (this.IsHandleCreated)
            {
                //显示人眼
                InvodeDelegate d = delegate () {
                    lock (lockImageObject)
                    {
                        if (pbxVideoLeft.Image != null)
                        {
                            pbxVideoLeft.Image.Dispose();
                        }
                        pbxVideoLeft.Image = bitmap;
                    }
                };
                try
                {
                    pbxVideoLeft.Invoke(d);

                }
                catch (Exception)
                {

                    //throw;
                }
            }
            else
            {
                lock (lockImageObject)
                {
                    if (pbxVideoLeft.Image != null)
                    {
                        pbxVideoLeft.Image.Dispose();
                    }
                    pbxVideoLeft.Image = bitmap;
                }
            }
        }

        private void DeviceFacade_updateRightImage(Bitmap bitmap)
        {
            if (this.IsHandleCreated)
            {
                //显示人眼
                InvodeDelegate d = delegate () {
                    lock (lockImageObject)
                    {
                        if (pbxVideoRight.Image != null)
                        {
                            pbxVideoRight.Image.Dispose();
                        }
                        pbxVideoRight.Image = bitmap;
                    }
                };
                try
                {
                    pbxVideoRight.Invoke(d);
                }
                catch (Exception)
                {

                    //throw;
                }
                
            }
            else
            {
                lock (lockImageObject)
                {
                    if (pbxVideoRight.Image != null)
                    {
                        pbxVideoRight.Image.Dispose();
                    }
                    pbxVideoRight.Image = bitmap;
                }
            }
        }

        /// <summary>
        /// Stops the video.
        /// </summary>
        private void StopVideo()
        {
            videoOpenStatus = false;
            //m_Timer.Stop();
            //m_Timer.Enabled = false;
            DeviceFacade.updateLeftImage -= DeviceFacade_updateLeftImage;
            DeviceFacade.updateRightImage -= DeviceFacade_updateRightImage;
            // 停止图像采集
            DeviceFacade.StopCaptureImage();
        }

        #endregion


        #region 私有函数

        /// <summary>
        /// Initials the UI.
        /// </summary>
        private void InitialUI()
        {
            //初始化检测值区域
            lblCFF.Text = INITIAL_TEXT;
            lblCFF1.Text = INITIAL_TEXT;
            lblCFF2.Text = INITIAL_TEXT;

            lblPCL.Text = INITIAL_TEXT;
            lblPCR.Text = INITIAL_TEXT;
            lblPCT.Text = INITIAL_TEXT;
            lblPCV.Text = INITIAL_TEXT;
            lblPID.Text = INITIAL_TEXT;
            lblPMD.Text = INITIAL_TEXT;
            lblPCA.Text = INITIAL_TEXT;
            lblPCD.Text = INITIAL_TEXT;

            //lblPCL2.Text = INITIAL_TEXT;
            //lblPCR2.Text = INITIAL_TEXT;
            //lblPCT2.Text = INITIAL_TEXT;
            //lblPCV2.Text = INITIAL_TEXT;
            //lblPID2.Text = INITIAL_TEXT;
            //lblPMD2.Text = INITIAL_TEXT;
            //lblPCA2.Text = INITIAL_TEXT;
            //lblPCD2.Text = INITIAL_TEXT;
            //初始化状态栏
            //(MdiParent as MainWindow).InitialExamStatusToolStrip();
            //(MdiParent as MainWindow).Status1 = ConstMessage.STATUS_READY;
            //(MdiParent as MainWindow).Status2 = string.Empty;

            //设置按钮状态
            if (ExamDataCenter.CurrentPatient == null)
            {
                btnPT.Enabled = false;
                btnCFF1.Enabled = false;
                btnCFF2.Enabled = false;
                btnSettting.Enabled = false;
                btnPR.Enabled = false;
            }
            else
            {
                btnPT.Enabled = true;
                btnCFF1.Enabled = true;
                btnCFF2.Enabled = true;
                btnSettting.Enabled = true;
                btnPR.Enabled = true;

                //在检测状态栏显示当前受测员姓名
                label1.Text = "受测员：" + ExamDataCenter.CurrentPatient.Name;
            }
        }

        /// <summary>
        /// Initial the curve graphics.
        /// </summary>
        private void InitCurveGraphics()
        {
            //CFF曲线
            cgCFF.SetTitle(TITLE_CFF, TITLE_CFF_X, TITLE_CFF_Y);
            cgCFF.SetXScale(0, 30);
            cgCFF.SetYScale(0, 60);
            cgCFF.SetStep(0.1, 1);
            cgCFF.SetSampleTime(200, 30);
            cgCFF.SetUnitToMs(1000);
            cgCFF.IsShowPointValues = true;
            //CFF1曲线
            LineItemData itemDataCFF1 = new LineItemData();
            itemDataCFF1.Color = Color.Blue;
            itemDataCFF1.fun = null;
            itemDataCFF1.Name = CURVE_LABEL_CFF1;
            itemDataCFF1.SymbolType = ZedGraph.SymbolType.None;
            cgCFF.AddLineItemData(itemDataCFF1);
            //CFF2曲线
            LineItemData itemDataCFF2 = new LineItemData();
            itemDataCFF2.Color = Color.Green;
            itemDataCFF2.fun = null;
            itemDataCFF2.Name = CURVE_LABEL_CFF2;
            itemDataCFF2.SymbolType = ZedGraph.SymbolType.None;
            cgCFF.AddLineItemData(itemDataCFF2);
            cgCFF.Initial();

            //PCL曲线
            cgPCL.SetTitle(TITLE_PCL, TITLE_PCL_X, TITLE_PCL_Y);
            cgPCL.SetXScale(0, 5000);
            cgPCL.SetYScale(0, 10);
            cgPCL.SetStep(100, 500);
            cgPCL.SetSampleTime(3.333, 5000);
            cgPCL.SetUnitToMs(1);
            cgPCL.IsShowPointValues = true;
            //PCL曲线(左眼)
            LineItemData itemLeftDataPCL = new LineItemData();
            itemLeftDataPCL.Color = Color.Blue;
            itemLeftDataPCL.fun = null;
            itemLeftDataPCL.Name = CURVE_LABEL_PCL;
            itemLeftDataPCL.SymbolType = ZedGraph.SymbolType.None;
            cgPCL.AddLineItemData(itemLeftDataPCL);
            //PCL曲线(右眼)
            LineItemData itemRightDataPCL = new LineItemData();
            itemRightDataPCL.Color = Color.Green;
            itemRightDataPCL.fun = null;
            itemRightDataPCL.Name = CURVE_LABEL_PCL2;
            itemRightDataPCL.SymbolType = ZedGraph.SymbolType.None;
            cgPCL.AddLineItemData(itemRightDataPCL);
        }

        /// <summary>
        /// Gets the patient key selected.
        /// </summary>
        /// <returns>List{System.Int32}.</returns>
        private List<int> GetPatientKeySelected()
        {
            List<int> examKeyList = new List<int>();

            if (ExamDataCenter.CurrentPatient != null)
            {
                examKeyList.Add(ExamDataCenter.CurrentPatient.primaryKey);
            }

            return examKeyList;
        }

        /// <summary>
        /// Updates the curve.
        /// </summary>
        private void UpdateCurve()
        {
            //清空曲线数据
            cgCFF.InitalCurveLists();

            if (!string.IsNullOrEmpty(ExamDataCenter.ExamData.DATA_FILE_NAME))
            {
                DataFileHelper fileHelper = new DataFileHelper(ExamDataCenter.ExamData.DATA_FILE_NAME);

                //如果CFF1已经检测完成，显示CFF1曲线
                if (ExamDataCenter.ExamData.CFF1Status)
                {
                    ExamDataCenter.CFF1Value = new List<double>(fileHelper.GetCffLow2HighSerials());
                    cgCFF.AddData(CURVE_LABEL_CFF1, ExamDataCenter.CFF1Value);
                }
                //如果CFF2已经检测完成，显示CFF2曲线
                if (ExamDataCenter.ExamData.CFF2Status)
                {
                    ExamDataCenter.CFF2Value = new List<double>(fileHelper.GetCffHigh2LowSerials());
                    cgCFF.AddData(CURVE_LABEL_CFF2, ExamDataCenter.CFF2Value);

                }

            }
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="status">The status.</param>
        private void UpdateStatus(string ColumnName, string status)
        {
            //DataRow[] drList = m_ExamTable.Select(ConstMessage.COLUMN_PATIENT_KEY + " = " + ExamDataCenter.CurrentPatient.primaryKey);
            //if (drList.Length > 0)
            //{
            //    drList[0][ColumnName] = status;

            //    if (drList[0][ConstMessage.COLUMN_SV] == ConstMessage.STATUS_COMPLETED
            //        && drList[0][ConstMessage.COLUMN_PCL] == ConstMessage.STATUS_COMPLETED
            //        && drList[0][ConstMessage.COLUMN_CFF1] == ConstMessage.STATUS_COMPLETED
            //        && drList[0][ConstMessage.COLUMN_CFF2] == ConstMessage.STATUS_COMPLETED)
            //    {
            //        m_SelectedRow.DefaultCellStyle.ForeColor = Color.Green;
            //    }
            //}
        }

        /// <summary>
        /// Sets the UI lock status.
        /// </summary>
        /// <param name="isLocked">if set to <c>true</c> [is locked].</param>
        private void SetUILockStatus(bool isLocked)
        {
            if (m_BtnCFF1Status)
            {
                btnCFF2.Enabled = !isLocked;
                btnPT.Enabled = !isLocked;
                btnPR.Enabled = !isLocked;
            }
            else if (m_BtnCFF2Status)
            {
                btnCFF1.Enabled = !isLocked;
                btnPT.Enabled = !isLocked;
				btnPR.Enabled = !isLocked;
            }
			else if (m_BtnPRStatus)
            {
                btnCFF1.Enabled = !isLocked;
                btnCFF2.Enabled = !isLocked;
                btnPT.Enabled = !isLocked;
                btnPR.Enabled = false;
            }
            else if (m_BtnPTStatus)
            {
                btnCFF1.Enabled = false;
                btnCFF2.Enabled = false;
                btnPT.Enabled = true;
                //btnPR.Enabled = m_PupilTrackSuccess;
                btnPR.Enabled = true;
            }
			else
            {
                btnCFF1.Enabled = true;
                btnCFF2.Enabled = true;
				btnPT.Enabled = true;
                btnPR.Enabled = false;
            }
			
            //判断是否检测中
            if (m_BtnCFF1Status || m_BtnCFF2Status || m_BtnPRStatus || m_BtnPTStatus)
            {
                btnSelectPatient.Enabled = false;
                btnSettting.Enabled = false;
            }
            else
            {
                btnSelectPatient.Enabled = true;
                btnSettting.Enabled = true;
            }
			
			
        }

        /// <summary>
        /// Sets the exam status.
        /// </summary>
        /// <param name="examType">Type of the exam.</param>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="status">The status.</param>
        private void SetExamStatus(string examType, int patientKey, string status)
        {
            //foreach (DataGridViewRow row in dgvExamList.Rows)
            //{
            //    if (Convert.ToInt32(row.Cells[ConstMessage.COLUMN_PATIENT_KEY].Value) == patientKey)
            //    {
            //        row.Cells[examType].Value = status;
            //    }
            //}
        }
        #endregion 私有函数

        #region 窗口事件

        private void btnViewChart_Click(object sender, EventArgs e)
        {
            ViewChartsForm viewChartsForm = new ViewChartsForm(ExamDataCenter.CurrentPatient.primaryKey);
            viewChartsForm.ShowDialog(this);
        }

        /// <summary>
        /// Handles the Click event of the btnReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReport_Click(object sender, EventArgs e)
        {
            List<int> patientList = GetPatientKeySelected();
            if (patientList.Count > 0)
            {
                string value = string.Join(",", patientList);
                string strSqlCondation = "E.PATIENT_KEY in (" + value + ")";

                DataTable data = null;

                bool success = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamData(strSqlCondation); }, this, out data);
                if (success)
                {
                    if (data.Rows.Count == 0)
                    {
                        MessageBox.Show(this, ConstMessage.MESSAGE_M1101, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ReportWindow reportWindow = new ReportWindow();

                    reportWindow.AddedList = data;
                    reportWindow.Owner = this;
                    reportWindow.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(this, ConstMessage.MESSAGE_M1404, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSelectPatient_Click(object sender, EventArgs e)
        {
            PatientMgmtForm patientMgmtForm = new PatientMgmtForm();
            patientMgmtForm.Init();
            patientMgmtForm.ShowDialog(this);
            var patientList = patientMgmtForm.SelectedPatient;
            if (patientList != null && patientList.Count > 0)
            {
                ExamDataCenter.CurrentPatient = patientList[patientList.Count - 1];

                if (ExamDataCenter.CurrentPatient != null)
                {
                    ExamDataCenter.ExamData = new ExamInfo();

                    if (ExamDataCenter.CFF1Value != null)
                    {
                        ExamDataCenter.CFF1Value.Clear();
                    }

                    if (ExamDataCenter.CFF2Value != null)
                    {
                        ExamDataCenter.CFF2Value.Clear();
                    }

                    ExamDataCenter.DeleteExam(ExamDataCenter.CurrentPatient.primaryKey);
                    DeviceFacade.TestType = 0;
                }

                ExamDataCenter.ExamData.EXAM_SCENARIO = lblExamScenario.Text;
                ExamDataCenter.ExamData.EXAM_PARAMS = lblLightSetting.Text;

                BaseValueDAO baseValueDao = new BaseValueDAO();
                BaseValue baseValue = baseValueDao.GetBaseValue(ExamDataCenter.CurrentPatient.primaryKey);
                lblBaseCFF.Text = baseValue != null ? baseValue.CFF.ToString() : BaseValueHelper.GetDefaultBaseValue().CFF.ToString();

                InitialUI();
                UpdateCurve();

            }

        }

        private void btnSettting_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            var result = settingsForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                lblLightSetting.Text = settingsForm.ExamSettings;
                lblExamScenario.Text = settingsForm.ExamScenario;
                ExamDataCenter.ExamData.EXAM_PARAMS = lblLightSetting.Text;
                ExamDataCenter.ExamData.EXAM_SCENARIO = lblExamScenario.Text;
                //DeviceFacade.setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, 30);
                //DeviceFacade.setBgLight(ExamDataCenter.LightSetting.BJLiangdu, ExamDataCenter.LightSetting.StimulateColor, ExamDataCenter.LightSetting.StimulateTime, ExamDataCenter.LightSetting.StimulateStrength);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLogout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Load event of the WearinessDetectForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WearinessDetectForm_Load(object sender, EventArgs e)
        {
            //初始化UI和曲线图
            InitialUI();
            InitCurveGraphics();
            var helper = new ParamsHelper();
            var config = IniSettingConfig.GetInstance();
            lblLightSetting.Text = string.Format("亮点:{0} 亮黑比:{1} 光强:{2} 背景光强:{3} 刺激光源:{4} 时长:{5}ms 强度:{6}",
                helper.getColour(config.BrightPointColour),
                helper.getBrightBlackRatio(config.BrightBlackRatio),
                helper.getBrightPointIntensity(config.BrightPointIntensity),
                helper.getBackgroundIntensity(config.BackgroundIntensity),
                helper.getStimulateLightColour(config.StimulateLightColour),
                config.StimulateTime,
                helper.getBackgroundIntensity(config.StimulateStrength)
                );
            lblExamScenario.Text = config.ExamScenario;

            var lightSetting = new LightSetting();
            lightSetting.colorLight = config.BrightPointColour;
            lightSetting.Liangheibi = config.BrightBlackRatio;
            lightSetting.LDLiangdu = config.BrightPointIntensity;
            lightSetting.BJLiangdu = config.BackgroundIntensity;
            lightSetting.StimulateColor = config.StimulateLightColour;
            lightSetting.StimulateTime = config.StimulateTime;
            lightSetting.StimulateStrength = config.StimulateStrength;

            ExamDataCenter.LightSetting = lightSetting;
            DeviceFacade.setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, 30);
            StartVideo();

            DeviceFacade.updateLeftImage += DeviceFacade_updateLeftImage;
            DeviceFacade.updateRightImage += DeviceFacade_updateRightImage;
        }


        /// <summary>
        /// Stops all timers.
        /// </summary>
        private void StopAllTimers()
        {
            //停止所有对下位机调用
            StopVideo();
            //timerSystemProtect.Stop();
            m_CFF1TimeExcutor.Stop();
            m_CFF2TimeExcutor.Stop();
            m_PupilTrackTimeExcutor.Stop();
            m_PupilExam.Stop();
        }

        /// <summary>
        /// Handles the FormClosed event of the WearinessDetectForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void WearinessDetectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //停止所有对下位机调用
            StopAllTimers();

            //TODO: 停止设备,停止图像采集
            //if (LoginInfoManager.LogoutStatus)
            //{
            //    // 注销时不用关闭所有设备，关闭光源即可。
            //    bool ret = DeviceFacade.SystemProtect();
            //    if (!ret)
            //    {
            //        m_Logger.Error("注销,关闭光源失败!");
            //    }
            //}
            //else if (LoginInfoManager.AutoProtectStatus)
            //{
            //    // 系统自动保护已经调用一次 DeviceFacade.SystemProtect();
            //}
            //else
            //{
            //    // 退出系统时关闭所有设备。
            //    bool ret = DeviceFacade.CloseDevices();
            //    if (!ret)
            //    {
            //        m_Logger.Error("退出系统,关闭设备失败!");
            //    }
            //}
            Application.RemoveMessageFilter(msg);
        }
        #endregion


        #region CFF1
        /// <summary>
        /// The m_CFF1TimeExcutor
        /// </summary>
        private CFF1TimeExcutor m_CFF1TimeExcutor = new CFF1TimeExcutor(30, 200);
        /// <summary>
        /// The m_BtnCFF1Status
        /// </summary>
        private bool m_BtnCFF1Status = false; //false未执行状态 ,true正在执行状态
        /// <summary>
        /// The BUTTON TEXT FOR CFF1
        /// </summary>
        private const string BUTTON_TEXT_CFF1 = "CFF低到高";
        /// <summary>
        /// The BUTTON TEXT FOR CFF STOP
        /// </summary>
        private const string BUTTON_CFF_STOP = "停止({0}s)";

        /// <summary>
        /// Handles the Click event of the btnCFF1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCFF1_Click(object sender, EventArgs e)
        {
            bool doExam = true;//执行检查标记
            if (!m_BtnCFF1Status)
            {
                if (ExamDataCenter.ExamData.CFF1Status)
                {
                    if (MessageBox.Show(this, ConstMessage.MESSAGE_M1006, string.Empty,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        //不需要执行检查
                        doExam = false;
                    }
                }

                //判断是否执行检查
                if (doExam)
                {
                    cgCFF.InitialCurveList(CURVE_LABEL_CFF1);
                    ExamDataCenter.CFF1Value.Clear();
                    DeviceFacade.TestType = 0;
                    DeviceFacade.setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, 5); //5
                    if(ExamDataCenter.LightSetting.BJLiangdu!=3)
                    {
                        DeviceFacade.setBgLight(ExamDataCenter.LightSetting.BJLiangdu, ExamDataCenter.LightSetting.StimulateColor, ExamDataCenter.LightSetting.StimulateTime, 0);
                    }
                    StartCFF1();
                }
            }
            else
            {
                StopCFF1();
            }
        }

        /// <summary>
        /// Starts the CFF1.
        /// </summary>
        private void StartCFF1()
        {

            if (m_CFF1TimeExcutor.Start())
            {
                //设置CFF1开始状态
                m_BtnCFF1Status = true;
                //(MdiParent as MainWindow).Status2 = ConstMessage.MESSAGE_M1003;
                SetUILockStatus(true);
            }
            else
            {
                m_CFF1TimeExcutor.Stop();
                MessageBox.Show(this, ConstMessage.MESSAGE_M1009, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Stops the CFF1.
        /// </summary>
        private void StopCFF1()
        {
            if (m_CFF1TimeExcutor.Stop())
            {
                SetCFF1StopStatus();
                //更新数据
                SaveCFF1();
            }
        }

        /// <summary>
        /// CFF1 completed.
        /// </summary>
        private void CFF1Completed()
        {
            SetCFF1StopStatus();
            //询问用户是否保存
            if (MessageBox.Show(this, ConstMessage.MESSAGE_M1007, string.Empty,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                SaveCFF1();
            }
        }

        //public delegate void TimerFunc();

        /// <summary>
        /// Draws the CFF1 curve.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="restTime">The rest time.</param>
        private void DrawCFF1Curve(IList<double> list, int restTime)
        {
            if (list[0] != 0)
            {
                ExamDataCenter.ExamData.CFF1 = list[0];
            }

            //更新曲线
            cgCFF.AddData(CURVE_LABEL_CFF1, list);
            //更新按钮时间
            btnCFF1.ButtonText = string.Format(BUTTON_CFF_STOP, restTime);
        }

        /// <summary>
        /// Sets the CFF1 stop status.
        /// </summary>
        private void SetCFF1StopStatus()
        {
            //设置CFF1停止状态
            btnCFF1.ButtonText = BUTTON_TEXT_CFF1;
            tabControl1.SelectedIndex = 0;
            DeviceFacade.setLight(0, 0, 3, 0);
            DeviceFacade.setBgLight(3, 0, 0, 0);
            //(MdiParent as MainWindow).Status2 = string.Empty;
            m_BtnCFF1Status = false;
            SetUILockStatus(false);
        }

        /// <summary>
        /// Saves the CFF1.
        /// </summary>
        private void SaveCFF1()
        {
            //cgCFF.DrawPannelXLine(ExamDataCenter.ExamData.CFF1);
            //保存数据
            ExamDataCenter.ExamData.CFF = Math.Round(ExamDataCenter.ExamData.CFF2 == 0 ?
            ExamDataCenter.ExamData.CFF1 : (ExamDataCenter.ExamData.CFF1 + ExamDataCenter.ExamData.CFF2) / 2, 2);
            SetExamStatus(ConstMessage.COLUMN_CFF1, ExamDataCenter.CurrentPatient.primaryKey, ConstMessage.STATUS_COMPLETED);
            CreateDataFile();
            DataFileHelper fileHelper = new DataFileHelper(ExamDataCenter.ExamData.DATA_FILE_NAME);
            fileHelper.SaveCffLow2High(ExamDataCenter.CFF1Value.ToArray());
            //(MdiParent as MainWindow).CFF1Status = true;
            UpdateStatus(ConstMessage.COLUMN_CFF1, ConstMessage.STATUS_COMPLETED);
            ExamDataCenter.ExamData.CFF1Status = true;
            lblCFF1.Text = ExamDataCenter.ExamData.CFF1.ToString();
            lblCFF.Text = ExamDataCenter.ExamData.CFF.ToString();
            DataAccessProxy.Execute(() => { ExamDataCenter.UpdateExam(); }, this);

            //读取检查数据
            DataTable data = null;
            bool ret = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamDataForBaseValue("CFF", ExamDataCenter.CurrentPatient.primaryKey); }, this, out data);
            if (ret)
            {
                if (data.Rows.Count != 0)
                {
                    // 计算基础值
                    double cff = Math.Round((double)data.Compute("avg(CFF)", null), 2);

                    // 更新基础值
                    BaseValueDAO baseValueDao = new BaseValueDAO();
                    var baseValue = baseValueDao.GetBaseValue(ExamDataCenter.CurrentPatient.primaryKey);
                    if (baseValue != null)
                    {
                        baseValue.UPDATE_DATE_TIME = System.DateTime.Now;
                        baseValue.CFF = cff;
                        baseValueDao.Update(baseValue);
                    }
                    else
                    {
                        baseValue = new BaseValue
                        {
                            Patient_key = ExamDataCenter.CurrentPatient.primaryKey,
                            USER_KEY = LoginInfoManager.CurrentUser.primaryKey,
                            UPDATE_DATE_TIME = System.DateTime.Now,
                            CFF = cff
                        };

                        baseValueDao.Insert(baseValue);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the data file.
        /// </summary>
        private void CreateDataFile()
        {
            if (string.IsNullOrEmpty(ExamDataCenter.ExamData.DATA_FILE_NAME) || !DataFileHelper.CheckFileExist(ExamDataCenter.ExamData.DATA_FILE_NAME))
            {
                ExamDataCenter.ExamData.DATA_FILE_NAME = Path.GetFileName(DataFileHelper.CreateDataFile(ExamDataCenter.CurrentPatient.ID));
            }
        }
        #endregion

        #region CFF2
        /// <summary>
        /// The m_CFF2TimeExcutor
        /// </summary>
        private CFF2TimeExcutor m_CFF2TimeExcutor = new CFF2TimeExcutor(30, 200);
        /// <summary>
        /// The m_BtnCFF2Status
        /// </summary>
        private bool m_BtnCFF2Status = false; //false未执行状态 ,true正在执行状态
        /// <summary>
        /// The BUTTON TEXT FOR CFF2
        /// </summary>
        private const string BUTTON_TEXT_CFF2 = "CFF高到低";

        /// <summary>
        /// Handles the Click event of the btnCFF2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCFF2_Click(object sender, EventArgs e)
        {

            bool doExam = true; //执行检查标记
            if (!m_BtnCFF2Status)
            {
                if (ExamDataCenter.ExamData.CFF2Status)
                {
                    if (MessageBox.Show(this, ConstMessage.MESSAGE_M1006, string.Empty,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        //不需要执行检查
                        doExam = false;
                    }
                }
                //判断是否执行检查
                if (doExam)
                {
                    m_BtnCFF2Status = true;
                    cgCFF.InitialCurveList(CURVE_LABEL_CFF2);
                    ExamDataCenter.CFF2Value.Clear();
                    DeviceFacade.TestType = 0;
                    DeviceFacade.setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, 55); //55
                    if (ExamDataCenter.LightSetting.BJLiangdu != 3)
                    {
                        DeviceFacade.setBgLight(ExamDataCenter.LightSetting.BJLiangdu, ExamDataCenter.LightSetting.StimulateColor, ExamDataCenter.LightSetting.StimulateTime, 0);
                    }
                    StartCFF2();
                }
            }
            else
            {
                StopCFF2();
            }
        }
        /// <summary>
        /// Starts the CFF2.
        /// </summary>
        private void StartCFF2()
        {
            if (m_CFF2TimeExcutor.Start())
            {
                m_BtnCFF2Status = true;
                //(MdiParent as MainWindow).Status2 = ConstMessage.MESSAGE_M1004;
                SetUILockStatus(true);
            }
            else
            {
                m_CFF2TimeExcutor.Stop();
                MessageBox.Show(this, ConstMessage.MESSAGE_M1010, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Stops the CFF2.
        /// </summary>
        private void StopCFF2()
        {
            if (m_CFF2TimeExcutor.Stop())
            {
                //更新CFF2检查完成状态
                SetCFF2StopStatus();
                //保存数据
                SaveCFF2();
            }
        }
        /// <summary>
        /// CFF2 completed.
        /// </summary>
        private void CFF2Completed()
        {
            SetCFF2StopStatus();

            if (MessageBox.Show(this, ConstMessage.MESSAGE_M1007, string.Empty,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                SaveCFF2();
            }
        }


        /// <summary>
        /// Draws the CFF2 curve.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="restTime">The rest time.</param>
        private void DrawCFF2Curve(IList<double> list, int restTime)
        {
            if (list[0] != 0)
            {
                ExamDataCenter.ExamData.CFF2 = list[0];
            }
            //更新曲线
            cgCFF.AddData(CURVE_LABEL_CFF2, list);
            //更新按钮时间
            btnCFF2.ButtonText = string.Format(BUTTON_CFF_STOP, restTime);
        }

        /// <summary>
        /// Sets the CFF2 stop status.
        /// </summary>
        private void SetCFF2StopStatus()
        {
            //更新CFF2检查完成状态
            btnCFF2.ButtonText = BUTTON_TEXT_CFF2;
            tabControl1.SelectedIndex = 0;
            DeviceFacade.setLight(0, 0, 3, 0);
            DeviceFacade.setBgLight(3, 0, 0, 0);
            //(MdiParent as MainWindow).Status2 = string.Empty;
            m_BtnCFF2Status = false;
            SetUILockStatus(false);
        }

        /// <summary>
        /// Saves the CFF2.
        /// </summary>
        private void SaveCFF2()
        {
            //cgCFF.DrawPannelXLine(ExamDataCenter.ExamData.CFF2);
            //保存数据
            ExamDataCenter.ExamData.CFF = Math.Round(ExamDataCenter.ExamData.CFF1 == 0 ?
            ExamDataCenter.ExamData.CFF2 : (ExamDataCenter.ExamData.CFF1 + ExamDataCenter.ExamData.CFF2) / 2, 2);
            SetExamStatus(ConstMessage.COLUMN_CFF2, ExamDataCenter.CurrentPatient.primaryKey, ConstMessage.STATUS_COMPLETED);
            CreateDataFile();
            DataFileHelper fileHelper = new DataFileHelper(ExamDataCenter.ExamData.DATA_FILE_NAME);
            fileHelper.SaveCffHigh2Low(ExamDataCenter.CFF2Value.ToArray());

            //(MdiParent as MainWindow).CFF2Status = true;
            ExamDataCenter.ExamData.CFF2Status = true;
            UpdateStatus(ConstMessage.COLUMN_CFF2, ConstMessage.STATUS_COMPLETED);
            lblCFF2.Text = ExamDataCenter.ExamData.CFF2.ToString();
            lblCFF.Text = ExamDataCenter.ExamData.CFF.ToString(); ;
            DataAccessProxy.Execute(() => { ExamDataCenter.UpdateExam(); }, this);

            //读取检查数据
            DataTable data = null;
            bool ret = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamDataForBaseValue("CFF", ExamDataCenter.CurrentPatient.primaryKey); }, this, out data);
            if (ret)
            {
                if (data.Rows.Count != 0)
                {
                    // 计算基础值
                    double cff = Math.Round((double)data.Compute("avg(CFF)", null), 2);

                    // 更新基础值
                    BaseValueDAO baseValueDao = new BaseValueDAO();
                    var baseValue = baseValueDao.GetBaseValue(ExamDataCenter.CurrentPatient.primaryKey);
                    if (baseValue != null)
                    {
                        baseValue.UPDATE_DATE_TIME = System.DateTime.Now;
                        baseValue.CFF = cff;
                        baseValueDao.Update(baseValue);
                    }
                    else
                    {
                        baseValue = new BaseValue
                        {
                            Patient_key = ExamDataCenter.CurrentPatient.primaryKey,
                            USER_KEY = LoginInfoManager.CurrentUser.primaryKey,
                            UPDATE_DATE_TIME = System.DateTime.Now,
                            CFF = cff
                        };

                        baseValueDao.Insert(baseValue);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the cgCFF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cgCFF_DoubleClick(object sender, EventArgs e)
        {
            if (!m_BtnCFF1Status && !m_BtnCFF2Status && ExamDataCenter.CurrentPatient != null &&
                (ExamDataCenter.ExamData.CFF1Status || ExamDataCenter.ExamData.CFF2Status))
            {
                //GraphZoomInWindow graphZoomInWindow = new GraphZoomInWindow(cgCFF);
                //graphZoomInWindow.ShowDialog(this);
            }

        }
        #endregion

        #region 瞳孔追踪
        /// <summary>
        /// The m_BtnPTStatus
        /// </summary>
        private bool m_BtnPTStatus = false; //false未执行状态 ,true正在执行状态

        /// <summary>
        /// The m_PupilTrackTimeExcutor 5秒，每秒300个数据
        /// </summary>
        private PupilTrackTimeExcutor m_PupilTrackTimeExcutor = new PupilTrackTimeExcutor(10000, 40);
        /// <summary>
        /// The pupil track success flag
        /// </summary>
        private bool m_PupilTrackSuccess = false;
        /// <summary>
        /// The BUTTON TEXT FOR PupilTrack  STOP
        /// </summary>
        private const string BUTTON_TEXT_PT_STOP = "停止追踪";
        /// <summary>
        /// The BUTTON TEXT FOR PupilTrack
        /// </summary>
        private const string BUTTON_TEXT_PT = "瞳孔追踪";
        /// <summary>
        /// Handles the Click event of the btnPT control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPT_Click(object sender, EventArgs e)
        {

            if (!m_BtnPTStatus)
            {
                if (m_PupilTrackTimeExcutor.Start())
                {
                    //瞳孔追踪状态
                    //(MdiParent as MainWindow).Status1 = ConstMessage.STATUS_PUPIL_TRACK;
                    DeviceFacade.TestType = 1;
                    m_BtnPTStatus = true;
                    btnPT.ButtonText = BUTTON_TEXT_PT_STOP;
                    SetUILockStatus(true);
                }
                else
                {
                    m_PupilTrackTimeExcutor.Stop();
                    DeviceFacade.TestType = 0;
                    MessageBox.Show(this, ConstMessage.MESSAGE_M1011, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DeviceFacade.TestType = 0;
                m_PupilTrackTimeExcutor.Stop();
                StopPupilTrack();
            }

        }

        /// <summary>
        /// PTs the completed.
        /// </summary>
        private void PTCompleted()
        {
            StopPupilTrack();

            //if (!string.IsNullOrEmpty(m_PupilTrackTimeExcutor.ErrorMessage))
            //{
            //    //提示追踪失败
            //    MessageBox.Show(this, m_PupilTrackTimeExcutor.ErrorMessage, string.Empty,
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //}
            //else
            //{
            //    //提示追踪失败
            //    MessageBox.Show(this, ConstMessage.MESSAGE_M1005, string.Empty,
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        /// <summary>
        /// Stops the pupil track.
        /// </summary>
        private void StopPupilTrack()
        {
            //设置停止状态
            m_PupilTrackSuccess = false;
            DeviceFacade.TestType = 0;
            //(MdiParent as MainWindow).Status1 = ConstMessage.STATUS_RUPIL_TRACK_FAILED;
            m_BtnPTStatus = false;
            SetUILockStatus(false);
            btnPT.ButtonText = BUTTON_TEXT_PT;
        }

        /// <summary>
        /// Draws the PT curve.
        /// </summary>
        /// <param name="postionList">The postion list.</param>
        /// <param name="restTime">The rest time.</param>
        private void DrawPTCurve(List<Point> postionList, int restTime)
        {
            //判断是否获得正确数据
            if (postionList.Count == 2)
            {
                lock (m_LockObject)
                {
                    //设置两眼位置数据
                    PositionLeftEye = postionList[0];
                    PositionRightEye = postionList[1];
                }

                //更新追踪成功状态
                if (!m_PupilTrackSuccess)
                {
                    m_PupilTrackSuccess = true;
                }
            }
        }

        #endregion

        #region 瞳孔对光反应
        /// <summary>
        /// The m_ pupil exam
        /// </summary>
        private PupilExam m_PupilExam = new PupilExam();
        /// <summary>
        /// The m_ BTN PR status
        /// </summary>
        private bool m_BtnPRStatus = false;
        /// <summary>
        /// Handles the Click event of the btnPR control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPR_Click(object sender, EventArgs e)
        {
            // 瞳孔对光反应
            if (!m_BtnPRStatus)
            {
                bool doExam = true;
                DeviceFacade.danci_bogupos_left = 0;
                DeviceFacade.danci_bogupos_right = 0;

                if (ExamDataCenter.ExamData.PCLStatus)
                {
                    if (MessageBox.Show(this, ConstMessage.MESSAGE_M1006, string.Empty, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        doExam = false;
                    }
                }

                if (doExam)
                {
                    if (m_PupilExam.Start())
                    {
                        DeviceFacade.TestType = 2;
                        DeviceFacade.setBgLight(ExamDataCenter.LightSetting.StimulateStrength, ExamDataCenter.LightSetting.StimulateColor, ExamDataCenter.LightSetting.StimulateTime, 1);
                        //设置PCL测试启动状态
                        m_BtnPRStatus = true;
                        SetUILockStatus(true);
                        //(MdiParent as MainWindow).Status2 = ConstMessage.STATUS_PUPIL_EXAM;
                        //清空曲线数据和检测值
                        cgPCL.InitalCurveLists();
                        //cgRightPCL.InitalCurveLists();
                        lblPCL.Text = INITIAL_TEXT;
                        lblPCR.Text = INITIAL_TEXT;
                        lblPCT.Text = INITIAL_TEXT;
                        lblPCV.Text = INITIAL_TEXT;
                        lblPID.Text = INITIAL_TEXT;
                        lblPMD.Text = INITIAL_TEXT;
                        lblPCA.Text = INITIAL_TEXT;
                        lblPCD.Text = INITIAL_TEXT;

                        //lblPCL2.Text = INITIAL_TEXT;
                        //lblPCR2.Text = INITIAL_TEXT;
                        //lblPCT2.Text = INITIAL_TEXT;
                        //lblPCV2.Text = INITIAL_TEXT;
                        //lblPID2.Text = INITIAL_TEXT;
                        //lblPMD2.Text = INITIAL_TEXT;
                        //lblPCA2.Text = INITIAL_TEXT;
                        //lblPCD2.Text = INITIAL_TEXT;
                        btnPR.Text = "停止";
                        tabControl1.SelectedIndex = 2;
                    }
                    else
                    {
                        DeviceFacade.TestType = 0;
                        m_PupilExam.Stop();
                        DeviceFacade.setLight(0, 0, 3, 0);
                        DeviceFacade.setBgLight(3, 0, 0, 0);
                        MessageBox.Show(this, ConstMessage.MESSAGE_M1013, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                DeviceFacade.TestType = 0;
                m_PupilExam.Stop();
            }
        }

        /// <summary>
        /// Pupils the exam_ completed handler.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="originalData">The original data.</param>
        /// <param name="examData">The exam data.</param>
        private void PupilExam_CompletedHandler(bool success, object originalData, object examData)
        {
            if (success)
            {
                PupilExamData pupilExamData = originalData as PupilExamData;

                if (pupilExamData != null && pupilExamData.PdLeftData.Length > 0)
                {
                    //绘制左眼曲线
                    cgPCL.AddData(CURVE_LABEL_PCL, pupilExamData.PdLeftData);
                    //绘制潜伏期
                    cgPCL.DrawPannelYLine((examData as ExamInfo).PCL, Color.Blue);
                    //绘制最小直径
                    cgPCL.DrawPannelXLine((examData as ExamInfo).PMD, Color.Blue);

                    ////绘制右眼曲线
                    //cgPCL.AddData(CURVE_LABEL_PCL2, pupilExamData.PdRightData);
                    ////绘制潜伏期
                    //cgPCL.DrawPannelYLine((examData as ExamInfo).PCL2, Color.Green);
                    ////绘制最小直径
                    //cgPCL.DrawPannelXLine((examData as ExamInfo).PMD2, Color.Green);

                    //更新瞳孔对光反映检测结果
                    //(MdiParent as MainWindow).PIDStatus = true;
                    ExamDataCenter.ExamData.PCLStatus = true;


                    if (examData != null)
                    {
                        ExamInfo examInfo = examData as ExamInfo;
                        //处理检测值，保留两位小数
                        ExamDataCenter.ExamData.PCL = Math.Round(examInfo.PCL, 2);
                        ExamDataCenter.ExamData.PCL2 = Math.Round(examInfo.PCL2, 2);
                        ExamDataCenter.ExamData.PCR = Math.Round(examInfo.PCR, 2);
                        ExamDataCenter.ExamData.PCR2 = Math.Round(examInfo.PCR2, 2);
                        ExamDataCenter.ExamData.PCV = Math.Round(examInfo.PCV, 2);
                        ExamDataCenter.ExamData.PCV2 = Math.Round(examInfo.PCV2, 2);
                        ExamDataCenter.ExamData.PID = Math.Round(examInfo.PID, 2);
                        ExamDataCenter.ExamData.PID2 = Math.Round(examInfo.PID2, 2);
                        ExamDataCenter.ExamData.PMD = Math.Round(examInfo.PMD, 2);
                        ExamDataCenter.ExamData.PMD2 = Math.Round(examInfo.PMD2, 2);
                        ExamDataCenter.ExamData.PCT = Math.Round(examInfo.PCT, 2);
                        ExamDataCenter.ExamData.PCT2 = Math.Round(examInfo.PCT2, 2);
                        ExamDataCenter.ExamData.PCA = Math.Round(examInfo.PCA, 2);
                        ExamDataCenter.ExamData.PCA2 = Math.Round(examInfo.PCA2, 2);
                        ExamDataCenter.ExamData.PCD = Math.Round(examInfo.PCD, 2);
                        ExamDataCenter.ExamData.PCD2 = Math.Round(examInfo.PCD2, 2);
                        //保存原始数据到文件
                        CreateDataFile();
                        DataFileHelper fileHelper = new DataFileHelper(ExamDataCenter.ExamData.DATA_FILE_NAME);
                        fileHelper.SavePupilExamData(pupilExamData);

                        //更新检测值
                        lblPCL.Text = ExamDataCenter.ExamData.PCL.ToString();
                        lblPCR.Text = ExamDataCenter.ExamData.PCR.ToString();
                        lblPCV.Text = ExamDataCenter.ExamData.PCV.ToString();
                        lblPID.Text = ExamDataCenter.ExamData.PID.ToString();
                        lblPMD.Text = ExamDataCenter.ExamData.PMD.ToString();
                        lblPCT.Text = ExamDataCenter.ExamData.PCT.ToString();
                        lblPCA.Text = (ExamDataCenter.ExamData.PCA*100).ToString();
                        lblPCD.Text = (ExamDataCenter.ExamData.PCD*100).ToString();

                        //lblPCL2.Text = ExamDataCenter.ExamData.PCL2.ToString();
                        //lblPCR2.Text = ExamDataCenter.ExamData.PCR2.ToString();
                        //lblPCV2.Text = ExamDataCenter.ExamData.PCV2.ToString();
                        //lblPID2.Text = ExamDataCenter.ExamData.PID2.ToString();
                        //lblPMD2.Text = ExamDataCenter.ExamData.PMD2.ToString();
                        //lblPCT2.Text = ExamDataCenter.ExamData.PCT2.ToString();
                        //lblPCA2.Text = (ExamDataCenter.ExamData.PCA2*100).ToString();
                        //lblPCD2.Text = (ExamDataCenter.ExamData.PCD2*100).ToString();
                    }
                    //保存到数据库
                    if (DataAccessProxy.Execute(() => { ExamDataCenter.UpdateExam(); }, this))
                    {
                        UpdateStatus(ConstMessage.COLUMN_PCL, ConstMessage.STATUS_COMPLETED);
                    }

                    //读取检查数据
                    DataTable data = null;
                    bool ret = DataAccessProxy.Execute<DataTable>(() => { return m_ExamDao.GetExamDataForBaseValue("PID", ExamDataCenter.CurrentPatient.primaryKey); }, this, out data);
                    if (ret)
                    {
                        if (data.Rows.Count != 0)
                        {
                            // 计算基础值
                            double pid = Math.Round((double)data.Compute("avg(PID)", null), 2);
                            double pmd = Math.Round((double)data.Compute("avg(PMD)", null), 2);
                            double pcv = Math.Round((double)data.Compute("avg(PCV)", null), 2);
                            double pcl = Math.Round((double)data.Compute("avg(PCL)", null), 2);
                            double pcr = Math.Round((double)data.Compute("avg(PCR)", null), 2);
                            double pct = Math.Round((double)data.Compute("avg(PCT)", null), 2);
                            double pca = Math.Round((double)data.Compute("avg(PCA)", null), 2);
                            double pcd = Math.Round((double)data.Compute("avg(PCD)", null), 2);

                            // 更新基础值
                            BaseValueDAO baseValueDao = new BaseValueDAO();
                            var baseValue = baseValueDao.GetBaseValue(ExamDataCenter.CurrentPatient.primaryKey);
                            if(baseValue!=null)
                            {
                                baseValue.UPDATE_DATE_TIME = System.DateTime.Now;
                                baseValue.PID = pid;
                                baseValue.PMD = pmd;
                                baseValue.PCV = pcv;
                                baseValue.PCL = pcl;
                                baseValue.PCR = pcr;
                                baseValue.PCT = pct;
                                baseValue.PCA = pca;
                                baseValue.PCD = pcd;
                                baseValueDao.UpdatePID(baseValue);
                            }
                            else
                            {
                                baseValue = new BaseValue
                                {
                                    Patient_key = ExamDataCenter.CurrentPatient.primaryKey,
                                    USER_KEY = LoginInfoManager.CurrentUser.primaryKey,
                                    UPDATE_DATE_TIME = System.DateTime.Now,
                                    PID = pid,
                                    PMD = pmd,
                                    PCV = pcv,
                                    PCL = pcl,
                                    PCR = pcr,
                                    PCT = pct,
                                    PCA = pca,
                                    PCD = pcd
                                };

                                baseValueDao.Insert(baseValue);
                            }
                        }
                    }

                }

            }
            //else if (!string.IsNullOrEmpty(m_ScanExam.ErrorMessage))
            //{
            //    MessageBox.Show(m_ScanExam.ErrorMessage);

            //}
            //设置对光反映完成状态
            //(MdiParent as MainWindow).Status2 = string.Empty;
            m_BtnPRStatus = false;
            SetUILockStatus(false);
            tabControl1.SelectedIndex = 1;
            btnPR.Text = "瞳孔对光反应";
        }

        /// <summary>
        /// Handles the DoubleClick event of the cgPCL control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cgPCL_DoubleClick(object sender, EventArgs e)
        {
            if (!m_BtnPRStatus && ExamDataCenter.CurrentPatient != null && ExamDataCenter.ExamData.PCLStatus)
            {
                //打开曲线放大窗口
                //GraphZoomInWindow graphZoomInWindow = new GraphZoomInWindow(cgPCL);
                //graphZoomInWindow.ShowDialog(this);
            }
        }
        #endregion


        #region 系统自动保护
        /// <summary>
        /// The m_ oper count
        /// </summary>
        static int m_OperCount = 0;

        /// <summary>
        /// Class MyMessager
        /// </summary>
        internal class MyMessager : IMessageFilter
        {
            private const int WM_KEYDOWN = 0x0100;
            private const int WM_MOUSEMOVE = 0x0200;
            private const int WM_LBUTTONDOWN = 0x0201;
            private const int WM_RBUTTONDOWN = 0x0204;
            private const int WM_MBUTTONDOWN = 0x0207;

            /// <summary>
            /// Filters out a message before it is dispatched.
            /// </summary>
            /// <param name="m">The message to be dispatched. You cannot modify this message.</param>
            /// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
            public bool PreFilterMessage(ref Message m)
            {
                //如果检测到有鼠标或则键盘的消息，则使计数为0.....
                if (m.Msg == WM_KEYDOWN || m.Msg == WM_MOUSEMOVE || m.Msg == WM_LBUTTONDOWN ||
                    m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN)
                {
                    m_OperCount = 0;
                }

                return false;
            }
        }

        /// <summary>
        /// Handles the Tick event of the timerSystemProtect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timerSystemProtect_Tick(object sender, EventArgs e)
        {

        }



        #endregion 系统自动保护
    }
}
