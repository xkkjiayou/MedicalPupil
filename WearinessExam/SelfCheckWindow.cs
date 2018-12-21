using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MedicalSys.MSCommon;
using WearinessExam.Examination;

namespace WearinessExam
{
    /// <summary>
    /// Class SelfCheckWindow
    /// </summary>
    public partial class SelfCheckWindow : Form
    {
        /// <summary>
        /// The index
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfCheckWindow"/> class.
        /// </summary>
        public SelfCheckWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Delegate ShowMessageBoxDelegate
        /// </summary>
        /// <param name="form">The form.</param>
        delegate void ShowMessageBoxDelegate(Form form);

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Call DLL
            string strStatus = string.Empty;
            if (!DeviceFacade.DeviceSelfCheck(ref strStatus))
            {
                ShowMessageBoxDelegate d = delegate(Form window)
                {
                    MessageBox.Show(window, ConstMessage.MESSAGE_M1405, ConstMessage.TITLE_M1405,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };
                this.Invoke(d, this);
                Application.Exit();
            }
            LoginInfoManager.SelfCheckStatus = true;

            Thread.Sleep(200);

            timer1.Stop();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Handles the Load event of the SelfCheckWindow.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelfCheckWindow_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void timer1_Tick(object sender, EventArgs e)
        {
            if (index > 6)
            {
                index = 0;
            }

            index++;

            string strProgress = string.Empty;

            for (int i = 0; i < index; i++)
            {
                strProgress += ".";
            }

            this.lblProgress.Text = strProgress;
        }
    }
}
