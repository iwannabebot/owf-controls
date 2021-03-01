using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWFControls.DataGrid.Controls
{
    internal class OWFGridDateTime : DateTimePicker, IOWFGridEditor
    {
        public object OldValue { get; private set; }
        public object NewValue { get => this.Value; }
        public int Row { get; private set; } = -1;
        public int Col { get; private set; } = -1;
        public OWFGridDateTime(int row, int col, DateTime oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public OWFGridDateTime(int row, int col, DateTimeOffset oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue.DateTime;
        }
        public void Activate(Rectangle bound)
        {
            this.Bounds = bound;
            this.Visible = true;
        }
        public void DeActivate()
        {
            this.Visible = false;
        }
    }

    internal class OWFGridTextBox : TextBox, IOWFGridEditor
    {
        public object OldValue { get; private set; }
        public object NewValue { get => this.Text; }
        public int Row { get; private set; } = -1;
        public int Col { get; private set; } = -1;

        public OWFGridTextBox(int row, int col, string oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Text = oldValue;
        }
        public void Activate(Rectangle bound)
        {
            this.Bounds = bound;
            this.Visible = true;
        }
        public void DeActivate()
        {
            this.Visible = false;
        }
    }

    internal class OWFGridComboBox : ComboBox, IOWFGridEditor
    {
        public object OldValue { get; private set; }
        public object NewValue { get => this.SelectedValue; }
        public int Row { get; private set; } = -1;
        public int Col { get; private set; } = -1;
        public OWFGridComboBox(int row, int col, object oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.SelectedValue = oldValue;
        }
        public void Activate(Rectangle bound)
        {
            this.Bounds = bound;
            this.Visible = true;
        }
        public void DeActivate()
        {
            this.Visible = false;
        }
    }

    internal class OWFGridNumBox : NumericUpDown, IOWFGridEditor
    {
        public object OldValue { get; private set; }
        public object NewValue { get => this.Value; }
        public int Row { get; private set; } = -1;
        public int Col { get; private set; } = -1;
        public OWFGridNumBox(int row, int col, short oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }

        public OWFGridNumBox(int row, int col, int oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }

        public OWFGridNumBox(int row, int col, ushort oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public OWFGridNumBox(int row, int col, uint oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public OWFGridNumBox(int row, int col, long oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public OWFGridNumBox(int row, int col, ulong oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public OWFGridNumBox(int row, int col, float oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = (decimal)oldValue;
        }
        public OWFGridNumBox(int row, int col, double oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = (decimal)oldValue;
        }
        public OWFGridNumBox(int row, int col, decimal oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Value = oldValue;
        }
        public void Activate(Rectangle bound)
        {
            this.Bounds = bound;
            this.Visible = true;
        }
        public void DeActivate()
        {
            this.Visible = false;
        }
    }

    internal class OWFGridCheckBox : CheckBox, IOWFGridEditor
    {
        public object OldValue { get; private set; }
        public object NewValue { get => this.Checked; }
        public int Row { get; private set; } = -1;
        public int Col { get; private set; } = -1;
        public OWFGridCheckBox(int row, int col, bool oldValue)
        {
            Row = row; Col = col; OldValue = oldValue; this.Checked = oldValue;
        }
        public void Activate(Rectangle bound)
        {
            this.Bounds = bound;
            this.Visible = true;
        }
        public void DeActivate()
        {
            this.Visible = false;
        }
    }
}
