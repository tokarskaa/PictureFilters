using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace PictureFilters
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        bool drawingPolygon;
        Point? firstEdgePoint;
        List<IntegerUpDown> matrix;
        public MainWindow()
        {
            vm = new ViewModel();
            InitializeComponent();
            DataContext = vm;
            InitializeMatrixGrid();
            firstEdgePoint = null;
            drawingPolygon = true;
            var directory = Directory.GetCurrentDirectory();
            BitmapImage bmp = new BitmapImage(new Uri(directory + "/Lenna.png"));
            vm.Image = new WriteableBitmap(bmp);
        }

        private void InitializeMatrixGrid()
        {
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    
                    int index = 3 * i + j;
                    IntegerUpDown integerUpDown = new IntegerUpDown()
                    {
                        Margin = new Thickness(5),
                        Height = 20,
                        TextAlignment = TextAlignment.Center
                    };

                    Binding binding = new Binding
                    {
                        Mode = BindingMode.TwoWay,
                        Path = new PropertyPath("Filter.MatrixFilter[" + index + "].Value")
                    };
                    integerUpDown.SetBinding(IntegerUpDown.ValueProperty, binding);
                    Grid.SetColumn(integerUpDown, i);
                    Grid.SetRow(integerUpDown, j);
                    MatrixGrid.Children.Add(integerUpDown);
                    
                }
            
        }
        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                BitmapImage bmp = new BitmapImage(new Uri(op.FileName));
                vm.Image = new WriteableBitmap(bmp);
            }
        }
        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            vm.ApplyFilter();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouse = e.GetPosition(MainImage);
            if (vm.FilterArea == FilterArea.brush)
            {
                vm.ApplyFilter((int)mouse.X, (int)mouse.Y);
            }
            else if (vm.FilterArea == FilterArea.polygon && drawingPolygon == true)
            {
                if (firstEdgePoint == null)
                    firstEdgePoint = mouse;
                else
                {
                    if (vm.Polygon.IsPolygonFinished(mouse))
                    {
                        drawingPolygon = false;
                        vm.SetOriginalPicture();
                        vm.DrawPolygon();
                        vm.ApplyFilter();
                        vm.Histogram.UpdateHistogram(vm.Image);
                    }
                    else
                    {
                        vm.Polygon.AddLine(new Edge(firstEdgePoint.Value, mouse));
                        firstEdgePoint = mouse;
                    }
                   
                }
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point mouse = e.GetPosition(MainImage);
            if (e.LeftButton == MouseButtonState.Pressed && vm.FilterArea == FilterArea.brush)
            {
                vm.ApplyFilter((int)mouse.X, (int)mouse.Y);
            }
            else if (vm.FilterArea == FilterArea.polygon && drawingPolygon == true && firstEdgePoint != null)
            {
                vm.SetOriginalPicture();
                vm.DrawPolygon();
                vm.Image.DrawBresenhamLine((int)firstEdgePoint.Value.X, (int)firstEdgePoint.Value.Y, (int)mouse.X, (int)mouse.Y, Colors.Black);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            vm.ClearChangedPixels();
            drawingPolygon = true;
            vm.ClearPolygon();
            vm.SetOriginalPicture();
            firstEdgePoint = null;
        }

        private void DeletePolygon_Click(object sender, RoutedEventArgs e)
        {
            drawingPolygon = true;
            vm.ClearPolygon();
            vm.SetOriginalPicture();
            firstEdgePoint = null;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            UIElementCollection matrix = MatrixGrid.Children;
            FilterType type = vm.Filter.FilterType;
            foreach(var child in matrix)
            {
                IntegerUpDown iup = child as IntegerUpDown;
                iup.IsEnabled = type == FilterType.custom ? true : false;
            }
           
        }

        private void MainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (vm.FilterArea == FilterArea.brush)
                vm.Histogram.UpdateHistogram(vm.Image);
        }
    }
}
