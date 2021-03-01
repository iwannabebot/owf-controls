using System;
using System.Drawing;

namespace OWFControls.DataGrid.Controls
{
    public interface IOWFGridEditor : IDisposable
    {
        object OldValue { get; }
        object NewValue { get; }
        int Row { get;  }
        int Col { get; }
        void Activate(Rectangle bound);
        void DeActivate();
    }
}