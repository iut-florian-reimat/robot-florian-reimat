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
using WpfAsservissementDisplay;
using System.Windows.Threading;

namespace RobotInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfRobotInterface : Window
    {
        public static float xPos, yPos, aPos, linearSpeed, angularSpeed;
        DispatcherTimer dialogTime;
        public WpfRobotInterface()
        {
            //SciChartSurface.SetRuntimeLicenseKey("wsCOsvBlAs2dax4o8qBefxMi4Qe5BVWax7TGOMLcwzWFYRNCa/f1rA5VA1ITvLHSULvhDMKVTc+niao6URAUXmGZ9W8jv/4jtziBzFZ6Z15ek6SLU49eIqJxGoQEFWvjANJqzp0asw+zvLV0HMirjannvDRj4i/WoELfYDubEGO1O+oAToiJlgD/e2lVqg3F8JREvC0iqBbNrmfeUCQdhHt6SKS2QpdmOoGbvtCossAezGNxv92oUbog6YIhtpSyGikCEwwKSDrlKlAab6302LLyFsITqogZychLYrVXJTFvFVnDfnkQ9cDi7017vT5flesZwIzeH497lzGp3B8fKWFQyZemD2RzlQkvj5GUWBwxiKAHrYMnQjJ/PsfojF1idPEEconVsh1LoYofNk2v/Up8AzXEAvxWUEcgzANeQggaUNy+OFet8b/yACa/bgYG7QYzFQZzgdng8IK4vCPdtg4/x7g5EdovN2PI9vB76coMuKnNVPnZN60kSjtd/24N8A==");
            InitializeComponent();

            dialogTime = new DispatcherTimer();
            dialogTime.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dialogTime.Tick += UpdatePositionData;
            dialogTime.Start();

        }

        public void UpdatePositionData(object sender, EventArgs e)
        {
            xPosText.Text = xPos + "m";
            yPosText.Text = yPos + "m";
            aPosText.Text = aPos + "°";
            linearSpeedText.Text = linearSpeed + "m/s";
            angularSpeedText.Text = angularSpeed + "m/s";

            if (mapDisplay.Robot != null)
            {
                mapDisplay.Robot[0].x = xPos * 1000;
                mapDisplay.Robot[0].y = yPos * 1000;
                mapDisplay.UpdateRobotPosition();
            }
            
        }

        private void OnResetPosClick(object sender, RoutedEventArgs e)
        {
            OnResetPosition(); // Maybe make this event better
        }

        public static event EventHandler<EventArgs> OnResetPositionEvent;

        public virtual void OnResetPosition()
        {
            OnResetPositionEvent?.Invoke(this, new EventArgs());
        }
    }
}
