using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace MedicalSys.MSCommon
{
    /// <summary>
    /// Class BackGroundWorkerDialog
    /// </summary>
    public partial class BackGroundWorkerDialog : Form
    {

        /// <summary>
        /// Gets or sets the back ground worker object.
        /// </summary>
        /// <value>The back ground worker object.</value>
        public IBackGroundWorkerObject BackGroundWorkerObject
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackGroundWorkerDialog"/> class.
        /// </summary>
        public BackGroundWorkerDialog()
        {
            InitializeComponent();
            btnOK.Enabled = false;
        }

        /// <summary>
        /// Handles the Load event of the BackGroundWorkerDialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BackGroundWorkerDialog_Load(object sender, EventArgs e)
        {
            if (BackGroundWorkerObject != null)
            {
                BackGroundWorkerObject.BackgroundWorker = backGroundWorker;
                backGroundWorker.DoWork += backGroundWorker_DoWork;
                backGroundWorker.RunWorkerAsync();
                lblMessage.Text = BackGroundWorkerObject.ProccessingMessage;
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the backGroundWorker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void backGroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backGroundWorker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void backGroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            lblMessage.Text = BackGroundWorkerObject.CompletedMessage;
            lblError.Text = BackGroundWorkerObject.ErrorMessage;
            btnOK.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the OK button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the DoWork event of the backGroundWorker.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        public void backGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackGroundWorkerObject.DoWork();

        }
    }
}
