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
using InteractiveDataDisplay.WPF;

namespace WpfMapDisplay
{
    /// <summary>
    /// Interaction logic for WpfMapDisplay.xaml
    /// </summary>
    public partial class WpfMapDisplay : UserControl
    {
        public List<Ecocup> Circle;
        public Color Red, Green;
        private CircleMarkerGraph circle_can;
        public WpfMapDisplay()
        {
            InitializeComponent();
            map_chart.Title = "Map";

            // Axis Setup
            map_chart.PlotOriginX = -1480;
            map_chart.PlotOriginY = -980;
            map_chart.PlotWidth = 2960;
            map_chart.PlotHeight = 1960;

            // Edit Only if you know what you're doing
            map_chart.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            map_chart.LegendVisibility = Visibility.Hidden;
            map_chart.IsHorizontalNavigationEnabled = false;
            map_chart.IsVerticalNavigationEnabled = false;
            map_chart.RightTitle = " "; // For avoiding Right Padding Bug

            BitmapImage img = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "../../Image/fondTable21.png"));// Need an Edit For Relative Path
            map_plot.Background = new ImageBrush(img);

            circle_can = new CircleMarkerGraph();
            map_plot.Children.Add(circle_can);

            // Can
            
            Circle = new List<Ecocup>();
            Color Red   = Color.FromRgb(0xFF, 0x00, 0x00);
            Color Green = Color.FromRgb(0x00, 0xFF, 0x00);
            Circle.Add(new Ecocup() {   id = 1, x = -1200,  y = 600,    color = Red     });
            Circle.Add(new Ecocup() {   id = 2, x = -1050,  y = 490,    color = Green   });
            UpdateCanPosition();
        }

        public void UpdateCanPosition()
        {
            if (Circle != null)
            {
                int Lenght = Circle.Count();
                double[] x = new double[Lenght];
                double[] y = new double[Lenght];
                Color[] c = new Color[Lenght];
                const int default_ecocup_size = 20;

                //circles
                int i;
                for (i = 0; i < Lenght; i++)
                {
                    x[i] = Circle[i].x;
                    y[i] = Circle[i].y;
                    c[i] = Circle[i].color;
                }
                circle_can.PlotColorSize(x, y, c, default_ecocup_size);
            }
            
        }
    }
    
}
public class Ecocup : IEquatable<Ecocup>
{
    public uint id { get; set; }
    public double x { get; set; }
    public double y { get; set; }
    public Color color { get; set; }

    #region Required For Working
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Ecocup objAsPart = obj as Ecocup;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }

    public bool Equals(Ecocup other)
    {
        if (other == null) return false;
        return (this.id.Equals(other.id));
    }
    #endregion
}
