using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PictureFilters
{
    class Edge
    {
        public Point[] Vertices { get; set; }
        public Edge(Point p1, Point p2)
        {
            Vertices = new Point[2] { new Point(p1.X, p1.Y), new Point(p2.X, p2.Y) };
        }


        public bool IsPointInEdge(Point point)
        {
            List<Point> pixels = Bresenham.CalculateBresenhamLine(Vertices[0], Vertices[1]);
            foreach (var pixel in pixels)
            {
                if (point.X >= pixel.X - 5 && point.X <= pixel.X + 5 && point.Y >= pixel.Y - 5 && point.Y <= pixel.Y + 5)
                    return true;
            }
            return false;
        }

        public Point GetMiddlePoint()
        {
            double x = Math.Abs((Vertices[0].X + Vertices[1].X) / 2);
            double y = Math.Abs((Vertices[0].Y + Vertices[1].Y) / 2);
            return new Point(x, y);
        }

        public int GetLength()
        {
            double a = Math.Abs(Vertices[0].X - Vertices[1].X);
            double b = Math.Abs(Vertices[0].Y - Vertices[1].Y);
            return (int)Math.Sqrt(a * a + b * b);
        }

        public double GetMinY()
        {
            return (Vertices[0].Y > Vertices[1].Y ? Vertices[1].Y : Vertices[0].Y);
        }

        public double GetMaxY()
        {
            return (Vertices[0].Y > Vertices[1].Y ? Vertices[0].Y : Vertices[1].Y);
        }

        public double GetMinX()
        {
            return (Vertices[0].X > Vertices[1].X ? Vertices[1].X : Vertices[0].X);
        }

        public double GetMaxX()
        {
            return (Vertices[0].X > Vertices[1].X ? Vertices[0].X : Vertices[1].X);
        }

        public Point GetMinYVertex()
        {
            return (Vertices[0].Y > Vertices[1].Y ? Vertices[1] : Vertices[0]);
        }
    }
}