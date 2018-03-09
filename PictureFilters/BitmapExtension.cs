using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PictureFilters
{
    static class BitmapExtension
    {
        private static void SetPixels(this WriteableBitmap wbm, List<Point> pixels, Color c)
        {

           // if (!wbm.Format.Equals(PixelFormats.Bgra32)) return;
            wbm.Lock();
            IntPtr buff = wbm.BackBuffer;
            int Stride = wbm.BackBufferStride;
            unsafe
            {
                byte* pbuff = (byte*)buff.ToPointer();
                foreach (Point pixel in pixels)
                {
                    if (pixel.Y > wbm.PixelHeight - 1 || pixel.X > wbm.PixelWidth - 1) return;
                    if (pixel.Y < 0 || pixel.X < 0) return;
                    int loc = (int)pixel.Y * Stride + (int)pixel.X * 4;
                    pbuff[loc] = c.B;
                    pbuff[loc + 1] = c.G;
                    pbuff[loc + 2] = c.R;
                    pbuff[loc + 3] = c.A;
                    wbm.AddDirtyRect(new Int32Rect((int)pixel.X, (int)pixel.Y, 1, 1));
                }
            }
            wbm.Unlock();
        }

        public static void SetPixels(this WriteableBitmap wbm, List<Point> pixels, List<Color> colors)
        {

           // if (!wbm.Format.Equals(PixelFormats.Bgra32)) return;
            wbm.Lock();
            IntPtr buff = wbm.BackBuffer;
            int Stride = wbm.BackBufferStride;
            unsafe
            {
                byte* pbuff = (byte*)buff.ToPointer();
                int index = 0;
                foreach (Point pixel in pixels)
                {
                    Color c = colors[index];
                    if (pixel.Y > wbm.PixelHeight - 1 || pixel.X > wbm.PixelWidth - 1) return;
                    if (pixel.Y < 0 || pixel.X < 0) return;
                    int loc = (int)pixel.Y * Stride + (int)pixel.X * 4;
                    pbuff[loc] = c.B;
                    pbuff[loc + 1] = c.G;
                    pbuff[loc + 2] = c.R;
                    pbuff[loc + 3] = c.A;
                    wbm.AddDirtyRect(new Int32Rect((int)pixel.X, (int)pixel.Y, 1, 1));
                    index++;
                }
            }
            wbm.Unlock();
        }
        public static void DrawBresenhamLine(this WriteableBitmap wbm, int x0, int y0, int x1, int y1, List<Color> colors)
        {
            if (x0 >= wbm.PixelWidth || x1 >= wbm.PixelWidth || y0 >= wbm.PixelHeight || y1 >= wbm.PixelHeight || x1 < 0 || x0 < 0 || y0 < 0 || y1 < 0)
                return;
            wbm.SetPixels(Bresenham.CalculateBresenhamLine(x0, y0, x1, y1), colors);
            
        }

        public static void DrawBresenhamLine(this WriteableBitmap wbm, Point p1, Point p2, Color color)
        {
            DrawBresenhamLine(wbm, (int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, color);
        }

        public static void DrawBresenhamLine(this WriteableBitmap wbm, int x0, int y0, int x1, int y1, Color color)
        {
            if (x0 >= wbm.PixelWidth || x1 >= wbm.PixelWidth || y0 >= wbm.PixelHeight || y1 >= wbm.PixelHeight || x1 < 0 || x0 < 0 || y0 < 0 || y1 < 0)
                return;
            wbm.SetPixels(Bresenham.CalculateBresenhamLine(x0, y0, x1, y1), color);
        }
        public static void DrawBresenhamCircle(this WriteableBitmap wbm, int x0, int y0, int r)
        {
            List<Point[]> pairs = Bresenham.CalculateBresenhamCircle(x0, y0, r);
            List<Point> pixels = new List<Point>();
            foreach (var pair in pairs)
            {
                pixels.Add(pair[0]);
                pixels.Add(pair[1]);
            }
            wbm.SetPixels(pixels, Colors.Black);
        }
    }
}