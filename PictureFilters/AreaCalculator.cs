using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PictureFilters
{
    static class AreaCalculator
    {
        public static List<Point> GetCirclePixelList(int x, int y, int r)
        {
            List<Point[]> circle = Bresenham.CalculateBresenhamCircle(x, y, r);
            List<Point> pixels = new List<Point>();
            foreach(var line in circle)
            {
                pixels.AddRange(Bresenham.CalculateBresenhamLine(line[0], line[1]));
            }
            return pixels;
        }

        public static List<Point> GetPolygonPixelList(Polygon p)
        {
            List<Edge> edges = Scanline.GetLinesToFill(p);
            List<Point> pixels = new List<Point>();
            foreach(var e in edges)
            {
                pixels.AddRange(Bresenham.CalculateBresenhamLine(e.Vertices[0], e.Vertices[1]));
            }
            return pixels;
        }
    }
}