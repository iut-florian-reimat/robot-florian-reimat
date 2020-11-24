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
using System.Windows.Threading;
using WpfMapDisplay;

namespace MapWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dialogTime;

        public MainWindow()
        {
            InitializeComponent();

            dialogTime = new DispatcherTimer();
            dialogTime.Interval = new TimeSpan(0, 0, 0, 0, 20);
            dialogTime.Tick += RandomizeCan;
            dialogTime.Start();
        }

        public void RandomizeCan(object sender, EventArgs e)
        {
            Random random = new Random();
            if (mapDisplay.Circle != null)
            {
                Color Red = Color.FromRgb(0xFF, 0x00, 0x00);
                Color Green = Color.FromRgb(0x00, 0xFF, 0x00);
                mapDisplay.Circle[0] = (new Ecocup() { id = (uint) mapDisplay.Circle.Count(), x = (random.NextDouble() - 0.5) * 1500, y = (random.NextDouble() - 0.5) * 1500, color = Red });
                mapDisplay.Circle[1] = (new Ecocup() { id = (uint) mapDisplay.Circle.Count(), x = (random.NextDouble() - 0.5) * 1500, y = (random.NextDouble() - 0.5) * 1500, color = Green });
                mapDisplay.UpdateCanPosition();
            }
            
        }

    }
}
