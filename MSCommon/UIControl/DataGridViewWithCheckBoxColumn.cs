using System;
using System.Windows.Forms;

namespace MedicalSys.MSCommon.UIControl
{
    public class DataGridViewWithCheckBoxColumn : DataGridView
    {

        public const string COLUMN_SELECTALL = "";
        DataGridViewCheckBoxHeaderCell m_HeaderCell;
        public DataGridViewWithCheckBoxColumn()
        {

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnHeadersHeight = 35;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Location = new System.Drawing.Point(51, 83);
            this.MultiSelect = false;

            this.RowHeadersVisible = false;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.RowTemplate.Height = 30;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TabIndex = 5;
            this.TabStop = false;


            InitialGridView();
            this.RowHeadersVisible = false;
            this.CellValueChanged += dgvUserList_CellValueChanged;
            this.CurrentCellDirtyStateChanged += dgvUserList_CurrentCellDirtyStateChanged;
            this.CellClick += new DataGridViewCellEventHandler(DataGridViewWithCheckBoxColumn_CellClick);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            if (DesignMode)
            {
                return;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the DataGridViewWithCheckBoxColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        void DataGridViewWithCheckBoxColumn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != 0)
            {
                DataGridViewRow row = this.Rows[e.RowIndex];
                if (row.Cells[0].Value != null)
                {
                    bool state = (bool)row.Cells[0].Value;
                    row.Cells[COLUMN_SELECTALL].Value = !state;
                }
                else
                {
                    row.Cells[COLUMN_SELECTALL].Value = true;
                }
            }
        }

        private void InitialGridView()
        {
            this.Columns.Clear();
            this.AutoGenerateColumns = false;

            // Set the DataGridView control's border.
            this.BorderStyle = BorderStyle.Fixed3D;

            // Initialize and add a check box column.
            DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            m_HeaderCell = new DataGridViewCheckBoxHeaderCell();
            m_HeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(HeaderCell_OnCheckBoxClicked);
            column.HeaderCell = m_HeaderCell;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.DataPropertyName = string.Empty;
            column.Name = COLUMN_SELECTALL;
            column.DisplayIndex = 1;
            column.Width = 120;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            this.Columns.Add(column);
        }

        /// <summary>
        /// Headers the cell_ on check box clicked.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        private void HeaderCell_OnCheckBoxClicked(bool state)
        {
            //选择或取消全选 Checkbox,设置所有行状态
            foreach (DataGridViewRow row in Rows)
            {
                row.Cells[COLUMN_SELECTALL].Value = state;
            }
            RefreshEdit();
        }

        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvUserList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        /// <summary>
        /// Handles the CellValueChanged event of the dgvUserList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvUserList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //编辑全选列
            if (e.ColumnIndex == 0)
            {
                bool deleteStatus = GetDeleteButtonStatus();
                //btnDelete.Enabled = deleteStatus;
                if (OnCellSelected != null)
                {
                    OnCellSelected(deleteStatus);
                }
            }
        }

        public delegate void CellSelectedHandler(bool satus);
        //根据列表状态设置Controls状态；
        public event CellSelectedHandler OnCellSelected;
        /// <summary>
        /// Gets the delete button status.
        /// </summary>
        /// <returns><c>true</c> if one or more item is checked, <c>false</c> otherwise</returns>
        public bool GetDeleteButtonStatus()
        {
            bool deleteStatus = false;
            int selectCount = 0;
            foreach (DataGridViewRow row in Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        deleteStatus = true;
                        selectCount++;
                    }
                }
            }
            m_HeaderCell.Checked = selectCount == Rows.Count;
            return deleteStatus;
        }

        /// <summary>
        /// Unselecteds the header cell.
        /// </summary>
        public void UnselectedHeaderCell()
        {
            m_HeaderCell.Checked = false;
        }
    }
}
