using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PictureFilters
{
    class Bresenham
    {
        public static List<Point> CalculateBresenhamLine(int x0, int y0, int x1, int y1)
        {
            List<Point> pixels = new List<Point>();
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                pixels.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
            return pixels;
        }

        public static List<Point> CalculateBresenhamLine(Point p1, Point p2)
        {
            return CalculateBresenhamLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y);
        }

        public static List<Point[]> CalculateBresenhamCircle(int xc, int yc, int r)
        {
            int x = 0;
            int y = r;
            int p = 3 - 2 * r;
            if (r == 0) return null;
            List<Point[]> pixels = new List<Point[]>();
            while (y >= x)
            {
                pixels.Add(new Point[2] { new Point(xc - x, yc - y), new Point(xc + x, yc - y) });
                pixels.Add(new Point[2] { new Point(xc - y, yc - x), new Point(xc + y, yc - x) });
                pixels.Add(new Point[2] { new Point(xc - x, yc + y), new Point(xc + x, yc + y) });
                pixels.Add(new Point[2] { new Point(xc - y, yc + x), new Point(xc + y, yc + x) });
                if (p < 0) p += 4 * x++ + 6;
                else p += 4 * (x++ - y--) + 10;
            }
            return pixels;
        }
    }
}