namespace OWFControls.DataGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using OWFControls.Core;
    using OWFControls.Core.Drawing;
    using OWFControls.Core.Framework;
    using OWFControls.DataGrid.Controls;
    using OWFControls.DataGrid.Framework;

    public class OWFGrid : OWFGridBase
    {
        public OWFGrid() : base()
        {
        }
        //protected virtual void OnDataSourceChanged(DataTable value)
        //{
        //    if (_dataSource != value)
        //    {
        //        _dataSource = value;
        //        _dataSource.ColumnChanged += (s, e) =>
        //        {
        //            Rows = _dataSource.Rows.Count;
        //            Cols = _dataSource.Columns.Count;
        //            Invalidate();
        //        };
        //        _dataSource.RowChanged += (s, e) =>
        //        {
        //            Rows = _dataSource.Rows.Count;
        //            Cols = _dataSource.Columns.Count;
        //            Invalidate();
        //        };
        //        _dataSource.RowDeleted += (s, e) =>
        //        {
        //            Rows = _dataSource.Rows.Count;
        //            Cols = _dataSource.Columns.Count;
        //            Invalidate();
        //        };
        //        _dataSource.TableCleared += (s, e) =>
        //        {
        //            Rows = _dataSource.Rows.Count;
        //            Cols = _dataSource.Columns.Count;
        //            Invalidate();
        //        };
        //        _dataSource.TableNewRow += (s, e) =>
        //        {
        //            Rows = _dataSource.Rows.Count;
        //            Cols = _dataSource.Columns.Count;
        //            Invalidate();
        //        };
        //        Rows = _dataSource.Rows.Count;
        //        Cols = _dataSource.Columns.Count;
        //        ColumnDefinitions.Clear();

        //        for (int i = 0; i < Cols; i++)
        //        {
        //            var c = _dataSource.Columns[i];
        //            var cd = new OWFCol
        //            {
        //                Index = i,
        //                AllowEditing = !c.ReadOnly,
        //                AllowFiltering = false,
        //                AllowSorting = true,
        //                AutoSize = false,
        //                Title = c.ColumnName,
        //                Width = DEFAULT_CELL_WIDTH
        //            };
        //            if (c.DataType == typeof(DateTime) || c.DataType == typeof(DateTimeOffset))
        //            {
        //                cd.DataType = OWFDataType.Datetime;
        //                cd.Format = CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;
        //            }
        //            else if (c.DataType == typeof(bool))
        //            {
        //                cd.DataType = OWFDataType.Boolean;
        //            }
        //            else if (c.DataType == typeof(int) || c.DataType == typeof(float) || c.DataType == typeof(double) || c.DataType == typeof(long) || c.DataType == typeof(short) || c.DataType == typeof(uint) || c.DataType == typeof(ulong) || c.DataType == typeof(ushort))
        //            {
        //                cd.DataType = OWFDataType.Number;

        //            }
        //            else if (c.DataType == typeof(string) || c.DataType == typeof(char))
        //            {
        //                cd.DataType = OWFDataType.String;
        //            }
        //            else
        //            {
        //                cd.AllowEditing = false;
        //                cd.DataType = OWFDataType.Object;
        //            }
        //            ColumnDefinitions.Add(cd);
        //        }
        //        DataSourceChanged?.Invoke(this, EventArgs.Empty);
        //    }
        //}

    }
}
