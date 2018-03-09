using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PictureFilters
{
    class Filter : INotifyPropertyChanged
    {
        private FilterType filterType;
        public FilterType FilterType
        {
            get { return filterType; }
            set { filterType = value; OnPropertyChanged(); }
        }

        private double factor;
        public double Factor
        {
            get { return factor; }
            set { factor = value; OnPropertyChanged(); }
        }
        public MyInt[] MatrixFilter { get; set; }

        private int sum;
        public Filter()
        {
            MatrixFilter = new MyInt[9];
            for (int i = 0; i < 9; i++)
                MatrixFilter[i] = new MyInt();
        }
        private int[,] PrepareMatrixFilter()
        {
            int[,] filter = new int[3, 3];
            Factor = 1;
            switch (FilterType)
            {
                case FilterType.identical:
                    filter[1, 1] = 1;
                    break;
                case FilterType.blur:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            filter[i, j] = 1;
                    Factor = 0.1;
                    break;
                case FilterType.sharpen:
                    filter[1, 0] = -1;
                    filter[0, 1] = -1;
                    filter[1, 2] = -1;
                    filter[2, 1] = -1;
                    filter[1, 1] = 5;
                    break;
                case FilterType.edges:
                    filter[0, 1] = -1;
                    filter[1, 1] = 1;
                    break;
                case FilterType.emboss:
                    filter[0, 0] = -1;
                    filter[0, 1] = -1;
                    filter[1, 0] = -1;
                    filter[1, 1] = 1;
                    filter[2, 2] = 1;
                    filter[1, 2] = 1;
                    filter[2, 1] = 1;
                    break;
                case FilterType.custom:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            filter[i, j] = MatrixFilter[3 * i + j].Value;
                    break;
                default:
                    break;
            }
            
            sum = GetFilterSum(filter);
            return filter;
        }
        private int GetFilterSum(int[,] filter)
        {
            int sum = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    sum += filter[i, j];
            return sum == 0 ? 1 : sum;
        }
        public void ApplyFilterToPixel(byte[] changed, Point p, int stride, int[,,] pixelColorsMatrix)
        {
            int x = (int)p.X;
            int y = (int)p.Y;
            int[,] filter = PrepareMatrixFilter();
            int redComponent = 0;
            int greenComponent = 0;
            int blueComponent = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    redComponent += filter[i, j] * pixelColorsMatrix[2, i, j];
                    greenComponent += filter[i, j] * pixelColorsMatrix[1, i, j];
                    blueComponent += filter[i, j] * pixelColorsMatrix[0, i, j];
                }
            int index = y * stride + 4 * x;

            blueComponent = Math.Min(Math.Max((int)(factor * blueComponent), 0), 255);
            redComponent = Math.Min(Math.Max((int)(factor * redComponent), 0), 255);
            greenComponent = Math.Min(Math.Max((int)(factor * greenComponent), 0), 255);
            changed[index] = (byte)(blueComponent);
            changed[index + 1] = (byte)(greenComponent);
            changed[index + 2] = (byte)(redComponent);
        }

        public Color GetFilteredPixelColor(Point p, int stride, int[,,] pixelColorsMatrix)
        {
            int x = (int)p.X;
            int y = (int)p.Y;
            int[,] filter = PrepareMatrixFilter();
            int redComponent = 0;
            int greenComponent = 0;
            int blueComponent = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    redComponent += filter[i, j] * pixelColorsMatrix[2, i, j];
                    greenComponent += filter[i, j] * pixelColorsMatrix[1, i, j];
                    blueComponent += filter[i, j] * pixelColorsMatrix[0, i, j];
                }
            int index = y * stride + 4 * x;
            blueComponent = Math.Min(Math.Max((int)(factor * blueComponent), 0), 255);
            redComponent = Math.Min(Math.Max((int)(factor * redComponent), 0), 255);
            greenComponent = Math.Min(Math.Max((int)(factor * greenComponent), 0), 255);
            Color c = new Color
            {
                R = (byte)(redComponent),
                G = (byte)(greenComponent),
                B = (byte)(blueComponent)
            };
            
            return c;
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}