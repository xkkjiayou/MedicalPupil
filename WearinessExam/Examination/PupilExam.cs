using System.Collections.Generic;
using WearinessExam.DO;
using WearinessExam.Utility;
using MedicalSys.Framework;
using System;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class PupilExam
    /// </summary>
    public class PupilExam : IBackGroundExcutor
    {
        /// <summary>
        /// The m_ original exam data
        /// </summary>
        private PupilExamData m_OriginalExamData;
        /// <summary>
        /// The m_ exam info
        /// </summary>
        private ExamInfo m_ExamInfo;
        /// <summary>
        /// The success
        /// </summary>
        private bool success = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="PupilExam"/> class.
        /// </summary>
        public PupilExam()
        {
            BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(BackgroundWorker_DoWork);
            BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public bool Start()
        {
            success = false;
            ErrorMessage = string.Empty;
            bool result = true;
            result = DeviceFacade.StartPupilExam();
            if (result)
            {
                BackgroundWorker.RunWorkerAsync();
            }
            return result;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            CompletedHandler(success, m_OriginalExamData, m_ExamInfo);
        }

        /// <summary>
        /// Handles the DoWork event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            double[] leftValue, rightValue;
            string errorMessage;
            if (DeviceFacade.GetPupilExam(out leftValue, out rightValue, out errorMessage))
            {
                leftValue = Array.ConvertAll(leftValue, a => Math.Round(a, 4));
                rightValue = Array.ConvertAll(rightValue, a => Math.Round(a, 4));
                ExamInfo examInfoLeft;
                PupilExamHelper.CalculatePupilsValue(leftValue, out  examInfoLeft);

                ExamInfo examInfoRight;
                PupilExamHelper.CalculatePupilsValue(rightValue, out examInfoRight);
                m_OriginalExamData = new PupilExamData();
                //ExamInfo examInfo = examInfoLeft;

                //if (!string.Equals(IniSettingConfig.GetInstance().SelectedEyeData, "L"))
                //{
                //    examInfo = examInfoRight;
                //}

                if (examInfoLeft != null && examInfoRight != null)
                {
                    m_OriginalExamData.PdLeftData = leftValue;
                    m_OriginalExamData.PdRightData = rightValue;

                    if (ExamDataCenter.PupilExamData == null)
                    {
                        ExamDataCenter.PupilExamData = new PupilExamData();
                    }
                    ExamDataCenter.PupilExamData.PdLeftData = leftValue;
                    ExamDataCenter.PupilExamData.PdRightData = rightValue;
                    //m_ExamInfo = new ExamInfo();
                    m_ExamInfo = examInfoLeft;
                    m_ExamInfo.PCA2 = examInfoRight.PCA;
                    m_ExamInfo.PCD2 = examInfoRight.PCD;
                    m_ExamInfo.PCL2 = examInfoRight.PCL;
                    m_ExamInfo.PCR2 = examInfoRight.PCR;
                    m_ExamInfo.PCT2 = examInfoRight.PCT;
                    m_ExamInfo.PCV2 = examInfoRight.PCV;
                    m_ExamInfo.PID2 = examInfoRight.PID;
                    m_ExamInfo.PMD2 = examInfoRight.PMD;
                    success = true;
                }
            }
            else
            {
                ErrorMessage = errorMessage;

            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public bool Stop()
        {
            return DeviceFacade.StopPupilExam();
        }

        /// <summary>
        /// Gets or sets the background worker.
        /// </summary>
        /// <value>The background worker.</value>
        private System.ComponentModel.BackgroundWorker BackgroundWorker
        {
            get;
            set;
        }

        /// <summary>
        /// Occurs when [completed handler].
        /// </summary>
        public event Completed CompletedHandler;


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
