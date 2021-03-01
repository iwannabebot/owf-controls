using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWFControls.Core.Drawing
{
    public static class DrawingExtensions
    {
        public static RectangleF ToRect(this SizeF size)
        {
            return new RectangleF(new PointF(0f, 0f), size);
        }

        public static RectangleF ToRect(this Size size)
        {
            return new RectangleF(new PointF(0f, 0f), size);
        }

        public static RectangleF ToRect(this SizeF size, PointF point)
        {
            return new RectangleF(point, size);
        }

        public static RectangleF ToRect(this Size size, PointF point)
        {
            return new RectangleF(point, size);
        }

        public static RectangleF ToRect(this SizeF size, float x, float y)
        {
            return new RectangleF(new PointF(x, y), size);
        }

        public static RectangleF ToRect(this Size size, int x, int y)
        {
            return new RectangleF(new Point(x, y), size);
        }
    }

    public static class GraphicsExtensions
    {
        public static void DrawHLine(this Graphics graphics, Pen pen, float y, float start, float end)
        {
            graphics.DrawLine(pen, start, y, end, y);
        }

        public static void DrawVLine(this Graphics graphics, Pen pen, float x, float start, float end)
        {
            graphics.DrawLine(pen, x, start, x, end);
        }

        public static void DrawHLine(this Graphics graphics, Pen pen, float y, float end)
        {
            graphics.DrawLine(pen, 0, y, end, y);
        }

        public static void DrawVLine(this Graphics graphics, Pen pen, float x, float end)
        {
            graphics.DrawLine(pen, x, 0, x, end);
        }
    }
}
