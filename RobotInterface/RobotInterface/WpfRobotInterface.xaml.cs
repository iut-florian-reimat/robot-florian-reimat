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
using SciChart.Charting.Visuals;
using WpfWorldMapDisplay;
using WorldMap;
using System.IO;

namespace RobotInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfRobotInterface : Window
    {
        private int timeOscillo = 0;
        public static float xPos, yPos, aPos, linearSpeed, angularSpeed, linearSpeedCommand = 2, angularSpeedCommand= 2.5f, linearSpeedOrder = 3, angularSpeedOrder = 3.5f;
        DispatcherTimer dialogTime;
        public WpfRobotInterface()
        {
            SciChartSurface.SetRuntimeLicenseKey("wsCOsvBlAs2dax4o8qBefxMi4Qe5BVWax7TGOMLcwzWFYRNCa/f1rA5VA1ITvLHSULvhDMKVTc+niao6URAUXmGZ9W8jv/4jtziBzFZ6Z15ek6SLU49eIqJxGoQEFWvjANJqzp0asw+zvLV0HMirjannvDRj4i/WoELfYDubEGO1O+oAToiJlgD/e2lVqg3F8JREvC0iqBbNrmfeUCQdhHt6SKS2QpdmOoGbvtCossAezGNxv92oUbog6YIhtpSyGikCEwwKSDrlKlAab6302LLyFsITqogZychLYrVXJTFvFVnDfnkQ9cDi7017vT5flesZwIzeH497lzGp3B8fKWFQyZemD2RzlQkvj5GUWBwxiKAHrYMnQjJ/PsfojF1idPEEconVsh1LoYofNk2v/Up8AzXEAvxWUEcgzANeQggaUNy+OFet8b/yACa/bgYG7QYzFQZzgdng8IK4vCPdtg4/x7g5EdovN2PI9vB76coMuKnNVPnZN60kSjtd/24N8A==");
            InitializeComponent();

            oscilloLinSpeed.SetTitle("Linear Speed");
            oscilloLinSpeed.AddOrUpdateLine(1, 256, "Measure");
            oscilloLinSpeed.AddOrUpdateLine(2, 256, "Order");
            oscilloLinSpeed.AddOrUpdateLine(3, 256, "Command");
            oscilloLinSpeed.ChangeLineColor(1, Colors.Red);
            oscilloLinSpeed.ChangeLineColor(2, Colors.Blue);
            oscilloLinSpeed.ChangeLineColor(3, Colors.Green);

            oscilloAngSpeed.SetTitle("Angular Speed");
            oscilloAngSpeed.AddOrUpdateLine(1, 256, "Measure");
            oscilloAngSpeed.AddOrUpdateLine(2, 256, "Order");
            oscilloAngSpeed.AddOrUpdateLine(3, 256, "Command");
            oscilloAngSpeed.ChangeLineColor(1, Colors.Red);
            oscilloAngSpeed.ChangeLineColor(2, Colors.Blue);
            oscilloAngSpeed.ChangeLineColor(3, Colors.Green);

            var currentDir = Directory.GetCurrentDirectory();
            var racineProjets = Directory.GetParent(currentDir);
            var imagePath = racineProjets.Parent.Parent.FullName.ToString() + "\\Images\\";

            worldMap.Init("Eurobot", LocalWorldMapDisplayType.WayPointMap);
            worldMap.SetFieldImageBackGround(imagePath + "Eurobot_Background.png");
            worldMap.InitPositionText();

            worldMap.InitTeamMate(1, "Eurobot");

            


            dialogTime = new DispatcherTimer();
            dialogTime.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dialogTime.Tick += UpdatePositionData;
            dialogTime.Start();
            
            

        }

        public void UpdatePositionData(object sender, EventArgs e)
        {
            timeOscillo ++;
            xPosText.Text = xPos + "m";
            yPosText.Text = yPos + "m";
            aPosText.Text = (float)(aPos * 180 / Math.PI) + "°";
            linearSpeedText.Text = linearSpeed + "m/s";
            angularSpeedText.Text = angularSpeed + "m/s";

            worldMap.UpdateRobotLocation(1, new Utilities.Location(xPos, yPos, aPos, linearSpeed, 0, angularSpeed));

            worldMap.UpdateWorldMapDisplay();
            

            oscilloLinSpeed.AddPointToLine(1, timeOscillo, linearSpeed);
            oscilloLinSpeed.AddPointToLine(2, timeOscillo, linearSpeedOrder);
            oscilloLinSpeed.AddPointToLine(3, timeOscillo, linearSpeedCommand);

            oscilloAngSpeed.AddPointToLine(1, timeOscillo, angularSpeed);
            oscilloAngSpeed.AddPointToLine(2, timeOscillo, angularSpeedOrder);
            oscilloAngSpeed.AddPointToLine(3, timeOscillo, angularSpeedCommand);

            asservSpeedDisplay.UpdatePolarOdometrySpeed(linearSpeed, angularSpeed);
            asservSpeedDisplay.UpdatePolarSpeedConsigneValues(linearSpeedOrder, angularSpeedOrder);
            asservSpeedDisplay.UpdatePolarSpeedCommandValues(linearSpeedCommand, angularSpeedCommand);
            asservSpeedDisplay.UpdatePolarSpeedErrorValues(Math.Abs(linearSpeed - linearSpeedOrder), Math.Abs(angularSpeed - angularSpeedOrder));
            
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
