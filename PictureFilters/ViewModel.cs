using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PictureFilters
{
    class ViewModel : INotifyPropertyChanged
    {
        private WriteableBitmap image;
        public WriteableBitmap Image
        {
            get { return image; }
            set { image = value;
                UpdateOriginalPicture();
                Histogram.UpdateHistogram(value);
                OnPropertyChanged();
            }
        }

        private FilterArea filterArea;
        public FilterArea FilterArea
        {
            get { return filterArea; }
            set { filterArea = value; OnPropertyChanged(); }
        }
        private int brushRadius;
        public int BrushRadius
        {
            get { return brushRadius; }
            set { brushRadius = value; OnPropertyChanged(); }
        }
        
        public Filter Filter { get; set; }
        
        public HistogramSource Histogram { get; set; }
        public Polygon Polygon { get; set; }

        private byte[] originalPicturePixels;
        private List<Point> pixelsToChange;
        public ViewModel()
        {
            Histogram = new HistogramSource(image);
            Filter = new Filter();
            Polygon = new Polygon();
            pixelsToChange = new List<Point>();
            BrushRadius = 5;
        }

        public void UpdateOriginalPicture()
        {
            int stride = image.PixelWidth * 4;
            int size = image.PixelHeight * stride;
            originalPicturePixels = new byte[size];
            image.CopyPixels(originalPicturePixels, stride, 0);
        }

        public void SetOriginalPicture()
        {
            if (Image != null && originalPicturePixels != null)
                Image.WritePixels(new System.Windows.Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), originalPicturePixels, image.PixelWidth * 4, 0);
        }

        public void DrawPolygon()
        {
            foreach (var e in Polygon.Edges)
                Image.DrawBresenhamLine(e.Vertices[0], e.Vertices[1], Colors.Black);
        }
        public void ApplyFilter(int x = 0, int y = 0)
        {
            byte[] changed = CopyArray(originalPicturePixels);
            int stride = image.PixelWidth * 4;
            UpdatePixelsList(x, y);
            // List<Point> pixelsToChange = GetPixelsList(x, y);
            if (FilterArea == FilterArea.whole)
            {
                foreach (var pixel in pixelsToChange)
                {
                    int[,,] pixelColorsMatrix = PreparePixelColorMatrix(pixel);
                    Filter.ApplyFilterToPixel(changed, pixel, stride, pixelColorsMatrix);
                }
                Image.WritePixels(new System.Windows.Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), changed, stride, 0);
                Histogram.UpdateHistogram(Image);
            }
            else
            {
                List<Color> colors = new List<Color>();
                foreach (var pixel in pixelsToChange)
                {
                    int[,,] pixelColorsMatrix = PreparePixelColorMatrix(pixel);
                    colors.Add(Filter.GetFilteredPixelColor(pixel, stride, pixelColorsMatrix));
                }
                Image.SetPixels(pixelsToChange, colors);
            }
           
        }

        public void ClearChangedPixels()
        {
            pixelsToChange.Clear();
        }

        public void ClearPolygon()
        {
            Polygon.Edges.Clear();
        }
        private void UpdatePixelsList(int x = 0, int y = 0)
        {
           // List<Point> pixels = new List<Point>();
            switch (FilterArea)
            {
                case FilterArea.whole:
                    for (y = 0; y < image.PixelHeight; y++)
                        for (x = 0; x < image.PixelWidth; x++)
                            pixelsToChange.Add(new Point(x, y));
                           // pixels.Add(new Point(x, y));
                break;
                case FilterArea.brush:
                    // pixelsToChange.AddRange(AreaCalculator.GetCirclePixelList(x, y, BrushRadius));
                    pixelsToChange = AreaCalculator.GetCirclePixelList(x, y, BrushRadius);
                    break;
                case FilterArea.polygon:
                    pixelsToChange = AreaCalculator.GetPolygonPixelList(Polygon);
                    break;
                default:
                    break;
            }
          //  return pixels;
        }
        private int[,,] PreparePixelColorMatrix(Point p)
        {
            int x = (int)p.X;
            int y = (int)p.Y;
            int[,,] matrix = new int[3, 3, 3];
            x = (x - 2);
            y = (y - 2);
            int stride = image.PixelWidth * 4;
            for (int i = 0; i < 3; i++)
            {
                int yInd = (y + i);
                if (yInd < 0) yInd = 0;
                if (yInd >= image.PixelHeight) yInd = image.PixelHeight-1;
                for (int j = 0; j < 3; j++)
                {
                    int xInd = (x + j);
                    if (xInd < 0) xInd = 0;
                    if (xInd >= image.PixelWidth) xInd = image.PixelWidth-1;
                    int index = yInd * stride + 4 * xInd;
                    matrix[0, i, j] = originalPicturePixels[index];
                    matrix[1, i, j] = originalPicturePixels[index + 1];
                    matrix[2, i, j] = originalPicturePixels[index + 2];
                }
            }
            return matrix;
        }

        private byte[] CopyArray(byte[] array)
        {
            byte[] newArray = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
                newArray[i] = array[i];
            return newArray;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum FilterArea
    {
        whole,
        brush,
        polygon
    };
    public enum FilterType
    {
        identical,
        blur,
        sharpen,
        edges,
        emboss,
        custom
    };
}