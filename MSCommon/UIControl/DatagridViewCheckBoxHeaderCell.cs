using System;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalSys.MSCommon
{
    /// <summary>
    /// Delegate CheckBoxClickedHandler
    /// </summary>
    /// <param name="state">if set to <c>true</c> [state].</param>
    public delegate void CheckBoxClickedHandler(bool state);
    /// <summary>
    /// Class DataGridViewCheckBoxHeaderCellEventArgs
    /// </summary>
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        /// <summary>
        /// The _b checked
        /// </summary>
        bool _bChecked;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridViewCheckBoxHeaderCellEventArgs"/> class.
        /// </summary>
        /// <param name="bChecked">if set to <c>true</c> [b checked].</param>
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="DataGridViewCheckBoxHeaderCellEventArgs"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get { return _bChecked; }
        }
    }
    /// <summary>
    /// Class DataGridViewCheckBoxHeaderCell
    /// </summary>
    public class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        /// <summary>
        /// The check box location
        /// </summary>
        Point checkBoxLocation;
        /// <summary>
        /// The check box size
        /// </summary>
        Size checkBoxSize;
        /// <summary>
        /// The _checked
        /// </summary>
        bool _checked = false;
        /// <summary>
        /// The _cell location
        /// </summary>
        Point _cellLocation = new Point();
        /// <summary>
        /// The _CB state
        /// </summary>
        System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
        /// <summary>
        /// Occurs when [on check box clicked].
        /// </summary>
        public event CheckBoxClickedHandler OnCheckBoxClicked;
        /// <summary>
        /// Initializes a new instance of the <see cref="DataGridViewCheckBoxHeaderCell"/> class.
        /// </summary>
        public DataGridViewCheckBoxHeaderCell()
        {
        }
        /// <summary>
        /// Paints the current <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.
        /// </summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the cell.</param>
        /// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the cell that is being painted.</param>
        /// <param name="rowIndex">The row index of the cell that is being painted.</param>
        /// <param name="dataGridViewElementState">State of the data grid view element.</param>
        /// <param name="value">The data of the cell that is being painted.</param>
        /// <param name="formattedValue">The formatted data of the cell that is being painted.</param>
        /// <param name="errorText">An error message that is associated with the cell.</param>
        /// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
        /// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
        /// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
        protected override void Paint(System.Drawing.Graphics graphics,
            System.Drawing.Rectangle clipBounds,
            System.Drawing.Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates dataGridViewElementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                dataGridViewElementState, value,
                formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(graphics,
            System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            p.X = cellBounds.Location.X +
                (cellBounds.Width / 2) - (s.Width / 2);
            p.Y = cellBounds.Location.Y +
                (cellBounds.Height / 2) - (s.Height / 2);
            _cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (_checked)
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.CheckedNormal;
            else
                _cbState = System.Windows.Forms.VisualStyles.
                    CheckBoxState.UncheckedNormal;
            CheckBoxRenderer.DrawCheckBox
            (graphics, checkBoxLocation, _cbState);
        }
        /// <summary>
        /// Called when the user clicks a mouse button while the pointer is on a cell.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <=
                checkBoxLocation.X + checkBoxSize.Width
            && p.Y >= checkBoxLocation.Y && p.Y <=
                checkBoxLocation.Y + checkBoxSize.Height)
            {
                _checked = !_checked;
                if (OnCheckBoxClicked != null)
                {
                    OnCheckBoxClicked(_checked);
                    this.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataGridViewCheckBoxHeaderCell"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                this.DataGridView.InvalidateCell(this);
            }


        }

    }

}
