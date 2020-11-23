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
                mapDisplay.Circle[0].x = (random.NextDouble() - 0.5) * 1500;
                mapDisplay.Circle[0].y = (random.NextDouble() - 0.5) * 1000;
                mapDisplay.Circle[1].x = (random.NextDouble() - 0.5) * 1500;
                mapDisplay.Circle[1].y = (random.NextDouble() - 0.5) * 1000;
                mapDisplay.UpdateCanPosition();
            }
            
        }

    }
}
