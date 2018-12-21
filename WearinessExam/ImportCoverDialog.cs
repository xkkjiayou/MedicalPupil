using System;
using System.Windows.Forms;

namespace WearinessExam
{
    /// <summary>
    /// Class ImportCoverDialog
    /// </summary>
    public partial class ImportCoverDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportCoverDialog"/> class.
        /// </summary>
        public ImportCoverDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Gets or sets a value indicating whether all same handle checked.
        /// </summary>
        /// <value><c>true</c> if all same handle checked; otherwise, <c>false</c>.</value>
        public bool AllSameHandleChecked
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether cover data checked.
        /// </summary>
        /// <value><c>true</c> if cover data checked; otherwise, <c>false</c>.</value>
        public bool CoverDataChecked
        {
            get;
            set;
        }
        /// <summary>
        /// Handles the Click event of the Cancel button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            AllSameHandleChecked = chbAllSame.Checked;
            CoverDataChecked = false;
            Close();
        }
        /// <summary>
        /// Handles the Click event of the OK button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            AllSameHandleChecked = chbAllSame.Checked;
            CoverDataChecked = true;
            Close();
        }
    }
}
