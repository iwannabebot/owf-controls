using OWFControls.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWFControls.DataGrid.Framework
{
    public class OWFRowCol
    {
        public int Width { get; set; }
        public bool Visible { get; set; }
    }
    public class OWFCol : OWFRowCol
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Display { get; set; }
        public string Format { get; set; }
        public OWFDataType DataType { get; set; }
        public Type CLRType { get; set; }
        public bool AllowSorting { get; set; }
        public bool AllowEditing { get; set; }
        public bool AllowFiltering { get; set; }
        public ColumnHeaderAutoResizeStyle AutoSize { get; set; }
        public HorizontalAlignment TextAlign { get; set; }
        public ImageList ImageList { get; set; }
        public int ImageIndex { get; set; }
    }
}
