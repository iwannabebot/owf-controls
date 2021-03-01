using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWFControls.DataGrid.Drawing
{
    [Flags]
    public enum OWFDrawMode
    {
        None = 0,
        Resize = 1,
        Zoom = 2,
        Selection = 4,
        EditCell = 8,
        Scroll = 16,
        RowColChange = 32,
        DataChange = 64,
        All = 128
    }
}
