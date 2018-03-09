using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Imaging;

namespace PictureFilters
{
    class HistogramSource : INotifyPropertyChanged
    {
        private List<KeyValuePair<int, int>> red;
        public List<KeyValuePair<int, int>> Red
        {
            get { return red; }
            set { red = value; OnPropertyChanged(); }
        }
        private List<KeyValuePair<int, int>> green;
        public List<KeyValuePair<int, int>> Green
        {
            get { return green; }
            set { green = value; OnPropertyChanged(); }
        }
        private List<KeyValuePair<int, int>> blue;
        public List<KeyValuePair<int, int>> Blue
        {
            get { return blue; }
            set { blue = value; OnPropertyChanged(); }
        }

        private byte[] pixels;
        private int stride;
        public HistogramSource(WriteableBitmap image)
        {
            Red = new List<KeyValuePair<int, int>>();
        }

        private void UpdatePixels(WriteableBitmap image)
        {
            stride = image.PixelWidth * 4;
            int size = image.PixelHeight * stride;
            pixels = new byte[size];
            image.CopyPixels(pixels, stride, 0);
        }
        public void UpdateHistogram(WriteableBitmap image)
        {
            UpdatePixels(image);
            List<KeyValuePair<int, int>> redTmp = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> greenTmp = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> blueTmp = new List<KeyValuePair<int, int>>();
            int[] reds = new int[256];
            int[] greens = new int[256];
            int[] blues = new int[256];
           
            for (int y = 0; y < image.PixelHeight; y++)
            {
                for (int x = 0; x < image.PixelWidth; x++)
                {
                    int index = y * stride + 4 * x;
                    blues[pixels[index]]++;
                    greens[pixels[index + 1]]++;
                    reds[pixels[index + 2]]++;
                   
                }
            }
            for (int i = 0; i < reds.Length; i++)
            {
                redTmp.Add(new KeyValuePair<int, int>(i, reds[i]));
                greenTmp.Add(new KeyValuePair<int, int>(i, greens[i]));
                blueTmp.Add(new KeyValuePair<int, int>(i, blues[i]));
            }
            Red = redTmp;
            Green = greenTmp;
            Blue = blueTmp;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}