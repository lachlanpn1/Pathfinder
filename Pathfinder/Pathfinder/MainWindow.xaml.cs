using PathfinderLib;
using System;
using System.Collections.Generic;
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

namespace Pathfinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numberOfBarriers = 150;
        int minSize = 5;
        int maxSize = 50;
        bool markStart = false;
        bool markEnd = false;
        PathfinderLib.Point startPoint;
        PathfinderLib.Point endPoint;
        Map map;
       
        public MainWindow()
        {
            InitializeComponent();
            canvas.Background = new SolidColorBrush(Colors.White);
            GenerateMap();
        }

        private void btnGeneratenewMap_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            GenerateMap();
        }

        private void GenerateMap()
        {
            MapGenerator mg = new MapGenerator();
            map = mg.Generate(
                Convert.ToInt32(canvas.Width),
                Convert.ToInt32(canvas.Height),
                numberOfBarriers,
                minSize,
                maxSize
            );

            foreach (Barrier barrier in map.Barriers)
            {
                DrawingUtility.Instance.DrawRectangle(canvas, barrier.Location, barrier.Width, barrier.Height, Brushes.Black);
            }
        }

        private void btnMarkStart_Click(object sender, RoutedEventArgs e)
        {
            markStart = true;
            btnStart.Background = new SolidColorBrush(Colors.Green);

        }

        private void Redraw()
        {
            canvas.Children.Clear();
            foreach (Barrier barrier in map.Barriers)
            {
                DrawingUtility.Instance.DrawRectangle(canvas, barrier.Location, barrier.Width, barrier.Height, Brushes.Black);
            }
            if(startPoint != null)
            {
                DrawStart(
                    startPoint.X,
                    startPoint.Y
                    );
            }
            if(endPoint != null)
            {
                DrawEnd(
                    endPoint.X,
                    endPoint.Y
                    );
            }
        }

        private void DrawStart(int x, int y)
        {
            PathfinderLib.Point point = new PathfinderLib.Point(x, y);
            if (!map.IsBarrierPresent(x + 3, y + 3))
            {
                if (startPoint != null)
                {
                    startPoint = null;
                    Redraw();
                }
                DrawingUtility.Instance.DrawRectangle(canvas, point, 7, 7, Brushes.Green);
                startPoint = new PathfinderLib.Point(point.X + 3, point.Y + 3);
                markStart = false;
                btnStart.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void DrawEnd(int x, int y)
        {
            PathfinderLib.Point point = new PathfinderLib.Point(x, y);
            if (!map.IsBarrierPresent(x, y))
            {
                if (endPoint != null)
                {
                    endPoint = null;
                    Redraw();
                }
                DrawingUtility.Instance.DrawRectangle(canvas, point, 7, 7, Brushes.Red); // endpoint is represented by a larger square on the screen
                endPoint = point; 
                markEnd = false;
                btnEnd.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(markStart)
            {
                DrawStart(
                    Convert.ToInt32(e.GetPosition(canvas).X),
                    Convert.ToInt32(e.GetPosition(canvas).Y)
                    );
            }
            if (markEnd)
            {
                DrawEnd(
                    Convert.ToInt32(e.GetPosition(canvas).X),
                    Convert.ToInt32(e.GetPosition(canvas).Y)
                    );
            }
        }



        private void btnMarkEnd_Click(object sender, RoutedEventArgs e)
        {
            markEnd = true;
            btnEnd.Background = new SolidColorBrush(Colors.Green);
        }

        private void btnDFSSearch_Click(object sender, RoutedEventArgs e)
        {
            Redraw();
            DrawingUtility.Instance.DrawRoute(
                canvas,
                PathfinderLib.Pathfinder.Instance.FindPath("DFS", startPoint, endPoint, map),
                3, new SolidColorBrush(Colors.Aqua));
        }

        private void btnGBFSSearch_Click(object sender, RoutedEventArgs e)
        {
            Redraw();
            DrawingUtility.Instance.DrawRoute(
                canvas,
                PathfinderLib.Pathfinder.Instance.FindPath("GBFS", startPoint, endPoint, map),
                3, new SolidColorBrush(Colors.Aqua));
        }

        private void btnASTAR_Click(object sender, RoutedEventArgs e)
        {
            Redraw();
            DrawingUtility.Instance.DrawRoute(
                canvas,
                PathfinderLib.Pathfinder.Instance.FindPath("A*", startPoint, endPoint, map),
                3, new SolidColorBrush(Colors.Aqua));
        }
    }
}
