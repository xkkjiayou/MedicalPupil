using MedicalSys.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WearinessExam
{
    public partial class ExportDialog : Form
    {

        /// <summary>
        /// The output folder
        /// </summary>
        private const string OUT_FOLDER = "output";

        /// <summary>
        /// Gets or sets the type of the select data.
        /// </summary>
        /// <value>The type of the select data.</value>
        public ExportDataType SelectDataType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the select directory.
        /// </summary>
        /// <value>The select directory.</value>
        public string SelectDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether multi selected.
        /// </summary>
        /// <value><c>true</c> if multi selected; otherwise, <c>false</c>.</value>
        public bool MultiSelected
        {
            get;
            set;
        }

        public ExportDialog()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Handles the Load event of the ExportDialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExportDialog_Load(object sender, EventArgs e)
        {
            //txtPath.Text = Path.Combine(AssemblyHelper.GetExecuteAssemblyPath(), OUT_FOLDER);
            //if (!Directory.Exists(txtPath.Text))
            //{
            //    Directory.CreateDirectory(txtPath.Text);
            //}
            chbOrignalData.Enabled = !MultiSelected;

        }

        /// <summary>
        /// Handles the Click event of the SelectPath button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.OverwritePrompt = false;
            saveFileDialog.InitialDirectory = Path.Combine(AssemblyHelper.GetExecuteAssemblyPath(), OUT_FOLDER);

            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.SupportMultiDottedExtensions = false;
            saveFileDialog.Filter = "Excel Worksheets|*.xls";

            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtPath.Text = saveFileDialog.FileName;
            }

        }

        /// <summary>
        /// Handles the Click event of the OK button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnOK_Click(object sender, System.EventArgs e)
        {
            //检查扩展名是否正确
            if (Path.GetExtension(txtPath.Text.Trim()).ToLower() != ".xls")
            {
                MessageBox.Show(this, ConstMessage.MESSAGE_M1106, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //检查路径是否存在
            if (!Directory.Exists(Path.GetDirectoryName(txtPath.Text)))
            {
                MessageBox.Show(this, ConstMessage.MESSAGE_M1107, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportDataType dataType = ExportDataType.PatientData;
            //检查值
            if (chbExamData.Checked)
            {
                dataType |= ExportDataType.ExamData;
            }
            //基础值
            if (chbBaseValue.Checked)
            {
                dataType |= ExportDataType.BaseValueData;
            }
            //原始数据
            if (chbOrignalData.Checked)
            {
                dataType |= ExportDataType.OriginalData;
            }
            if (dataType == ExportDataType.PatientData)
            {
                MessageBox.Show(this, ConstMessage.MESSAGE_M1108, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //检查File是否存在
            if (File.Exists(txtPath.Text))
            {
                var result = MessageBox.Show(this, ConstMessage.MESSAGE_M1109, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(txtPath.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ConstMessage.MESSAGE_M1110, "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            SelectDirectory = txtPath.Text.Trim();
            SelectDataType = dataType;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    /// <summary>
    /// Enum ExportDataType
    /// </summary>
    [Flags]
    public enum ExportDataType
    {
        //人员数据
        /// <summary>
        /// The patient data
        /// </summary>
        PatientData = 0x01,
        //检查数据
        /// <summary>
        /// The exam data
        /// </summary>
        ExamData = 0x02,
        //基础值数据
        /// <summary>
        /// The base value data
        /// </summary>
        BaseValueData = 0x04,
        //原始数据
        /// <summary>
        /// The original data
        /// </summary>
        OriginalData = 0x08,

    }
}
