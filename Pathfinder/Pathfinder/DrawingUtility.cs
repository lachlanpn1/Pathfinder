using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PathfinderLib;

namespace Pathfinder
{
    public sealed class DrawingUtility
    {
        private DrawingUtility()
        {

        }

        private static DrawingUtility instance = null;
        public static DrawingUtility Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DrawingUtility();
                }
                return instance;
            }
        }

        public void DrawRectangle(Canvas canvas, Point point, int width, int height, Brush brush)
        {
            Rectangle rectangle = new Rectangle();
            Canvas.SetLeft(rectangle, Convert.ToDouble(point.X));
            Canvas.SetTop(rectangle, Convert.ToDouble(point.Y));
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.Fill = brush;
            canvas.Children.Add(rectangle);
        }

        public void DrawRoute(Canvas canvas, Route route, int width, Brush brush)
        {
            foreach(Point point in route.Path)
            {
                DrawRectangle(canvas, point, width, width, brush);
            }
        }
    }
}
