namespace OWFControls.DataGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using OWFControls.Core;
    using OWFControls.Core.Drawing;
    using OWFControls.Core.Framework;
    using OWFControls.DataGrid.Controls;
    using OWFControls.DataGrid.Framework;

    public abstract class OWFGridBase : OWFControl
    {
        #region constants
        protected const int DEFAULT_CELL_HEIGHT = 26;
        protected const int DEFAULT_CELL_WIDTH = 92;
        #endregion

        #region fields
        private List<OWFCol> _columnDefinitions;
        private List<List<object>> _internalStore;
        private bool _rowHeaderVisible;
        private int _rows;
        private int _cols;
        private int _frozenrows = 1;
        private ListChangedEventHandler _listChangedHandler;
        private EventHandler _positionChangedHandler;
        private OWFSelection _selection;
        private OWFSelectionMode _selectionMode = OWFSelectionMode.None;
        private IOWFGridEditor _editor;
        private object dataSource;
        private string dataMember;
        private CurrencyManager dataManager;
        #endregion

        #region events
        public delegate void OWFSelectionChangeHandler(object sender, OWFSelectionEventArgs e);
        public event EventHandler<int> RowsChanged;
        public event EventHandler<int> ColumnsChanged;
        public event EventHandler<int> FrozenRowsChanged;
        public event OWFSelectionChangeHandler SelectionChanged;
        public event EventHandler<IOWFGridEditor> EditorChanged;
        #endregion

        public OWFGridBase()
        {
            _columnDefinitions = new List<OWFCol>();
            _rowHeaderVisible = true;
            _rows = 4;
            _cols = 5;
            _frozenrows = 1;
            _internalStore = Enumerable.Repeat<List<object>>(Enumerable.Repeat<object>("", _cols).ToList(), _rows).ToList();
            _listChangedHandler = new ListChangedEventHandler(ListChangedEventHandler);
            _positionChangedHandler = new EventHandler(PositionChangedHandler);
        }

        #region properties
        public int Rows
        {
            get => _rows;
            set 
            {
                if (_rows != value)
                {
                    _rows = value;
                    RowsChanged?.Invoke(this, _rows);
                    OnRowsChange(value);
                }

            }
        }

        public int Cols
        {
            get => _cols;
            set
            {
                if (_rows != value)
                {
                    _rows = value;
                    ColumnsChanged?.Invoke(this, _rows);
                    OnColumnsChange(value);
                }

            }
        }

        public bool RowHeaderVisible
        {
            get => _rowHeaderVisible;
            set 
            {
                if (_rowHeaderVisible != value)
                {
                    _rowHeaderVisible = value;
                    RowsChanged?.Invoke(this, -1);
                    OnRowHeaderVisibleChange(value);
                }
            }
        }

        public int FrozenRows
        {
            get => _frozenrows;
            set
            {
                if (_frozenrows != value)
                {
                    _frozenrows = value;
                    FrozenRowsChanged?.Invoke(this, value);
                    OnFrozenRowsChange(value);
                }
            }
        }

        protected List<OWFCol> ColumnDefinitions
        {
            get => _columnDefinitions;
            set
            {
                if (_columnDefinitions != value)
                {
                    _columnDefinitions = value;
                    OnColumnDefinitionsChange();
                }
            }
        }

        public int TotalRows
        {
            get => FrozenRows + Rows;
        }

        public IOWFGridEditor Editor
        {
            get => _editor;
            set
            {
                if (_editor != value)
                {
                    _editor = value;
                    EditorChanged?.Invoke(this, value);
                    OnEditorChange();
                }
            }
        }

        public OWFSelection Selection
        {
            get => _selection;
            set
            {
                if (value != _selection)
                {
                    var e = new OWFSelectionEventArgs(_selection, value);
                    _selection = value;
                    SelectionChanged?.Invoke(this, e);
                    OnSelectionChange(e);
                }
            }
        }

        [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
        [Category("Data")]
        [DefaultValue(null)]
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (this.dataSource != value)
                {
                    this.dataSource = value;
                    tryDataBinding();
                }
            }
        }

        [Category("Data")]
        [Editor("System.Windows.Forms.Design.DataMemberListEditor,System.Design", "System.Drawing.Design.UITypeEditor,System.Drawing")]
        [DefaultValue("")]
        public string DataMember
        {
            get
            {
                return this.dataMember;
            }
            set
            {
                if (this.dataMember != value)
                {
                    this.dataMember = value;
                    tryDataBinding();
                }
            }
        }
        #endregion

        #region accessor
        public object this[int row, int column]
        {
            get
            {
                return _internalStore[row][column];
            }
            set
            {
                _internalStore[row][column] = value;
            }
        }
        public object this[int row, string columnName]
        {
            get
            {
                return _internalStore[row][ColumnDefinitions.First(c => c.Title == columnName).Index];
            }
            set
            {
                _internalStore[row][ColumnDefinitions.First(c => c.Title == columnName).Index] = value;
            }
        }
        #endregion

        #region onevent
        protected virtual void OnColumnDefinitionsChange()
        {
            this.Invalidate();
        }
        protected virtual void OnRowsChange(int value)
        {
            this.Invalidate();

        }
        protected virtual void OnColumnsChange(int value)
        {
            this.Invalidate();
        }
        protected virtual void OnRowHeaderVisibleChange(bool value)
        {
            this.Invalidate();
        }
        protected virtual void OnFrozenRowsChange(int value)
        {
            this.Invalidate();
        }
        protected virtual void OnEditorChange()
        {
            this.Invalidate();
        }
        protected virtual void OnSelectionChange(OWFSelectionEventArgs e)
        {
            this.Invalidate();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            InternalFinishEditing();
            base.OnMouseClick(e);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            InternalStartEditing(e);
            base.OnMouseDoubleClick(e);
        }
        protected override void OnBindingContextChanged(EventArgs e)
        {
            this.tryDataBinding();
            base.OnBindingContextChanged(e);
        }
        #endregion

        #region selection
        
        #endregion

        #region databinding
        private void ListChangedEventHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset || e.ListChangedType == ListChangedType.ItemMoved)
            {
                // Update all data
                updateAllData();
            }
            else if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                // Add new Item
                addItem(e.NewIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                // Change Item
                updateItem(e.NewIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // Delete Item
                deleteItem(e.NewIndex);
            }
            else
            {
                // Update metadata and all data
                loadSchema();
                updateAllData();
            }
            Invalidate();
        }

        private void PositionChangedHandler(object sender, EventArgs e)
        {
            if (this.Rows > dataManager.Position)
            {
                this.Items[dataManager.Position].Selected = true;
                this.EnsureVisible(dataManager.Position);
            }
        }

        private void tryDataBinding()
        {
            if (this.DataSource == null || base.BindingContext == null)
                return;
            CurrencyManager cm;
            try
            {
                cm = (CurrencyManager)base.BindingContext[this.DataSource, this.DataMember];
                if (this.dataManager != cm)
                {
                    // Unwire the old CurrencyManager
                    if (this.dataManager != null)
                    {
                        this.dataManager.ListChanged -= ListChangedEventHandler;
                        this.dataManager.PositionChanged -= PositionChangedHandler;
                    }
                    this.dataManager = cm;
                    // Wire the new CurrencyManager
                    if (this.dataManager != null)
                    {
                        this.dataManager.ListChanged += ListChangedEventHandler;
                        this.dataManager.PositionChanged += PositionChangedHandler;
                    }

                    // Update metadata and data
                    loadSchema();
                    updateAllData();
                    Invalidate();
                }
            }
            catch (System.ArgumentException)
            {
                // If no CurrencyManager was found
                return;
            }
        }

        private void loadSchema()
        {
            this.ColumnDefinitions.Clear();
            if (dataManager == null)
                return;
            int index = 0;
            foreach (PropertyDescriptor prop in dataManager.GetItemProperties())
            {
                OWFCol cd = new OWFCol
                {
                    AllowEditing = prop.IsReadOnly,
                    CLRType = prop.PropertyType,
                    Display = prop.DisplayName,
                    Title = prop.Name,
                    AllowFiltering = true,
                    AllowSorting = true,
                    AutoSize = ColumnHeaderAutoResizeStyle.HeaderSize,
                    Index = index++,
                    Width = DEFAULT_CELL_WIDTH
                };
                if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTimeOffset))
                {
                    cd.DataType = OWFDataType.Datetime;
                    cd.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    cd.DataType = OWFDataType.Boolean;
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(float) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(short) || prop.PropertyType == typeof(uint) || prop.PropertyType == typeof(ulong) || prop.PropertyType == typeof(ushort))
                {
                    cd.DataType = OWFDataType.Number;
                }
                else if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(char))
                {
                    cd.DataType = OWFDataType.String;
                }
                else
                {
                    cd.DataType = OWFDataType.Object;
                }
                this.ColumnDefinitions.Add(cd);
            }
        }

        private void updateAllData()
        {
            this._internalStore.Clear();
            for (int i = 0; i < dataManager.Count; i++)
            {
                addItem(i);
            }
        }

        private void updateItem(int index)
        {
            if (index >= 0 && index < this._internalStore.Count)
            {
                List<object> item = getListViewItem(index);
                this._internalStore[index] = item;
            }
        }

        private void deleteItem(int index)
        {
            if (index >= 0 &&
                index < this._internalStore.Count)
                this._internalStore.RemoveAt(index);
        }

        private void addItem(int index)
        {
            List<object> item = getListViewItem(index);
            this._internalStore.Insert(index, item);
        }

        private List<object> getListViewItem(int index)
        {
            object row = dataManager.List[index];
            PropertyDescriptorCollection propColl = dataManager.GetItemProperties();
            List<object> items = new List<object>();

            // Fill value for each column
            foreach (var column in this.ColumnDefinitions)
            {
                PropertyDescriptor prop = propColl.Find(column.Title, false);
                if (prop != null)
                {
                    items.Add(prop.GetValue(row));
                }
            }
            return items;
        }
        #endregion

        #region drawing
        protected override void OnPaint(PaintEventArgs pe)
        {
            var colWidth = (DEFAULT_CELL_WIDTH * Cols) + 20;
            var rowHeight = DEFAULT_CELL_HEIGHT * TotalRows;
            var width = colWidth < Width ? Width : colWidth;
            var height = rowHeight < Height ? Height : rowHeight;
            var headerHeight = DEFAULT_CELL_HEIGHT * FrozenRows;
            this.AutoScrollMinSize = new Size(colWidth, rowHeight);

            var whiteBrush = new SolidBrush(Color.White);
            var backgroundBrush = new SolidBrush(Color.Gray);
            var foregroundBrush = new SolidBrush(Color.Black);
            var headerBrush = new SolidBrush(Color.DarkGray);
            var linePen = new Pen(Color.Gray);

            var backgroundRect = new RectangleF(0, 0, width, height);
            var gridRect = this.AutoScrollMinSize.ToRect();
            var headerRect = new RectangleF(0, 0, colWidth, headerHeight);
            var rowHeaderRect = new RectangleF(0, 0, 20, rowHeight);

            pe.Graphics.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);
            try
            {
                // draw grid
                pe.Graphics.FillRectangle(backgroundBrush, backgroundRect);
                pe.Graphics.FillRectangle(whiteBrush, gridRect);
                pe.Graphics.FillRectangle(headerBrush, headerRect);
                pe.Graphics.FillRectangle(headerBrush, rowHeaderRect);

                for (int i = 0; i < TotalRows; i++)
                {
                    pe.Graphics.DrawHLine(linePen, DEFAULT_CELL_HEIGHT * i, width);
                }

                for (int i = 0; i < Cols; i++)
                {
                    pe.Graphics.DrawVLine(linePen, 20 + (DEFAULT_CELL_WIDTH * i), height);
                }
                // paint header content
                for (int i = 0; i < Cols; i++)
                {
                    pe.Graphics.DrawString(ColumnDefinitions[i].Title, this.Font, foregroundBrush, 20 + (DEFAULT_CELL_WIDTH * i) + 5, 5,
                        new StringFormat
                        {
                            FormatFlags = StringFormatFlags.NoWrap,
                            Trimming = StringTrimming.None
                        });
                }
                // paint grid content
                for (int j = 0; j < Cols; j++)
                {
                    var cd = ColumnDefinitions[j];
                    for (int i = 0; i < Rows; i++)
                    {
                        var cellBound = new Rectangle(20 + (DEFAULT_CELL_WIDTH * j) + 5, (DEFAULT_CELL_HEIGHT * (i + FrozenRows)) + 5, DEFAULT_CELL_WIDTH, DEFAULT_CELL_HEIGHT);
                        string cellText = "";
                        switch (cd.DataType)
                        {
                            case OWFDataType.Datetime:
                                if (cd.Format != null)
                                {
                                    cellText = Convert.ToDateTime(this[i, j]).ToString(cd.Format);
                                }
                                else
                                {
                                    cellText = Convert.ToDateTime(this[i, j]).ToString();
                                }
                                break;
                            case OWFDataType.Number:
                                if (cd.Format != null)
                                {
                                    cellText = Convert.ToInt32(this[i, j]).ToString(cd.Format);
                                }
                                else
                                {
                                    cellText = Convert.ToInt32(this[i, j]).ToString();
                                }
                                break;
                            default:
                                cellText = Convert.ToString(this[i, j]).ToString();
                                break;
                        }
                        Region r = pe.Graphics.Clip;
                        pe.Graphics.SetClip(cellBound);
                        pe.Graphics.DrawString(cellText, this.Font, foregroundBrush, cellBound, new StringFormat
                        {
                            FormatFlags = StringFormatFlags.NoWrap,
                            Trimming = StringTrimming.None
                        });
                        pe.Graphics.Clip = r;
                    }
                }
            }
            finally
            {
                whiteBrush.Dispose();
                backgroundBrush.Dispose();
                foregroundBrush.Dispose();
                headerBrush.Dispose();
                linePen.Dispose();
                base.OnPaint(pe);
            }

        }
        #endregion

        #region cell editor ops
        public void StartEditing(int row, int col)
        {
            InternalStartEditing(row, col);
        }
        protected abstract void StartEditing(int row, string columnName);
        private void InternalStartEditing(MouseEventArgs e)
        {
            int row = (AutoScrollPosition.Y + e.Y) / DEFAULT_CELL_HEIGHT;
            int col = (AutoScrollPosition.X + e.X - 20) / DEFAULT_CELL_WIDTH;
            InternalStartEditing(row, col);
        }
        private void InternalStartEditing(int row, int col)
        {
            if (ColumnDefinitions[col].AllowEditing)
            {
                var cd = ColumnDefinitions[col];
                var r = row - FrozenRows;
                if (Editor != null)
                {
                    this.Controls.Remove(Editor as Control);
                    Editor.Dispose();
                    Editor = null;
                }
                var val = this[r, col];
                switch (cd.DataType)
                {
                    case OWFDataType.Datetime:
                        if(val is DateTime)
                        {
                            Editor = new OWFGridDateTime(r, col, (DateTime)this[r, col])
                            {
                                Font = this.Font,
                                Visible = false
                            };
                        }
                        else if (val is DateTimeOffset)
                        {
                            Editor = new OWFGridDateTime(r, col, (DateTimeOffset)this[r, col])
                            {
                                Font = this.Font,
                                Visible = false
                            };
                        }
                        break;
                    case OWFDataType.Number:
                        if (val is DateTime)
                        {
                            Editor = new OWFGridNumBox(r, col, this[r, col])
                            {
                                Font = this.Font,
                                Visible = false
                            };
                        }
                        
                        break;
                    case OWFDataType.Boolean:
                        Editor = new OWFGridCheckBox(r, col, (bool)this[r, col])
                        {
                            Font = this.Font,
                            Visible = false
                        };
                        break;
                    default:
                        Editor = new OWFGridTextBox(r, col, this[r, col])
                        {
                            Font = this.Font,
                            Visible = false
                        };
                        break;
                }
                if (Editor != null && Editor.Row >= 0)
                {
                    var x = 20 + (DEFAULT_CELL_WIDTH * col);
                    var y = DEFAULT_CELL_HEIGHT * row;
                    Editor.Activate(new Rectangle(x, y, DEFAULT_CELL_WIDTH, DEFAULT_CELL_HEIGHT));
                    this.Controls.Add(Editor as Control);
                }
            }
        }
        private void InternalFinishEditing()
        {
            if (Editor != null)
            {
                var cd = ColumnDefinitions[Editor.Col];
                object cellText = null;
                try
                {
                    switch (cd.DataType)
                    {
                        case OWFDataType.Datetime:
                            cellText = Convert.ToDateTime(Editor.NewValue);
                            break;
                        case OWFDataType.Number:
                            cellText = Convert.ToDecimal(Editor.NewValue);
                            break;
                        case OWFDataType.String:
                            cellText = Convert.ToString(Editor.NewValue);
                            break;
                        case OWFDataType.Boolean:
                            cellText = Convert.ToBoolean(Editor.NewValue);
                            break;
                        default:
                            cellText = Editor.NewValue;
                            break;
                    }
                }
                catch (Exception)
                {
                    cellText = Editor.OldValue;
                }
                this[Editor.Row, Editor.Col] = cellText;
                Editor.DeActivate();
            }
        }
        #endregion
    }
}
