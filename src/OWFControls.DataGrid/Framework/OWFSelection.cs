using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWFControls.DataGrid.Framework
{
    public enum OWFSelectionMode
    {
        None,
        Cell,
        Row,
        Col,
        List,
        Free
    }

    public class OWFSelection
    {
        public int LeftBottom { get; set; }
        public int LeftTop { get; set; }
        public int RightBottom { get; set; }
        public int RightTop { get; set; }
    }

    public class OWFSelectionEventArgs : EventArgs
    {
        public OWFSelection OldSelection { get; private set; }
        public OWFSelection NewSelection { get; private set; }
        public OWFSelectionEventArgs(OWFSelection oldSelection, OWFSelection newSelection)
        {
            OldSelection = oldSelection;
            NewSelection = newSelection;
        }
    }
}
