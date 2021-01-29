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
using EventArgsLibrary;
using Utilities;

namespace RobotInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfRobotInterface : Window
    {
        private uint timestamp = 0;
        public static float Position_X, Position_Y, Position_Angular, Position_Angular_Degree;

        public static PolarAsservMessageArgs polarAsserv;

        //public static float Measure_M1, Measure_M2;
        //public static float Command_M1, Command_M2;
        //public static float Error_M1, Error_M2;
        //public static float Order_M1, Order_M2;

        //public static float Kp_M1, Ki_M1, Kd_M1;
        //public static float Correction_P_M1, Correction_I_M1, Correction_D_M1;
        //public static float Kp_Max_M1, Ki_Max_M1, Kd_Max_M1;

        //public static float Kp_M2, Ki_M2, Kd_M2;
        //public static float Correction_P_M2, Correction_I_M2, Correction_D_M2;
        //public static float Kp_Max_M2, Ki_Max_M2, Kd_Max_M2;

        DispatcherTimer dialogTime;
        public WpfRobotInterface()
        {
            SciChartSurface.SetRuntimeLicenseKey("wsCOsvBlAs2dax4o8qBefxMi4Qe5BVWax7TGOMLcwzWFYRNCa/f1rA5VA1ITvLHSULvhDMKVTc+niao6URAUXmGZ9W8jv/4jtziBzFZ6Z15ek6SLU49eIqJxGoQEFWvjANJqzp0asw+zvLV0HMirjannvDRj4i/WoELfYDubEGO1O+oAToiJlgD/e2lVqg3F8JREvC0iqBbNrmfeUCQdhHt6SKS2QpdmOoGbvtCossAezGNxv92oUbog6YIhtpSyGikCEwwKSDrlKlAab6302LLyFsITqogZychLYrVXJTFvFVnDfnkQ9cDi7017vT5flesZwIzeH497lzGp3B8fKWFQyZemD2RzlQkvj5GUWBwxiKAHrYMnQjJ/PsfojF1idPEEconVsh1LoYofNk2v/Up8AzXEAvxWUEcgzANeQggaUNy+OFet8b/yACa/bgYG7QYzFQZzgdng8IK4vCPdtg4/x7g5EdovN2PI9vB76coMuKnNVPnZN60kSjtd/24N8A==");
            InitializeComponent();

            polarAsserv = new PolarAsservMessageArgs();

            #region Oscilloscope_Init
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
            #endregion
            #region Map_Init
            var currentDir = Directory.GetCurrentDirectory();
            var racineProjets = Directory.GetParent(currentDir);
            var imagePath = racineProjets.Parent.Parent.FullName.ToString() + "\\Images\\";

            worldMap.Init("Eurobot", LocalWorldMapDisplayType.WayPointMap);
            worldMap.SetFieldImageBackGround(imagePath + "Eurobot_Background.png");
            worldMap.InitPositionText();
            worldMap.InitTeamMate(1, "Eurobot");
            #endregion

            dialogTime = new DispatcherTimer();
            dialogTime.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dialogTime.Tick += UpdatePositionData;
            dialogTime.Start();
        }

        public void UpdatePositionData(object sender, EventArgs e)
        {
            timestamp ++;
            #region Map
            worldMap.UpdateRobotLocation(1, new Location(Position_X, Position_Y, Position_Angular, polarAsserv.Measure_LinearSpeed, 0, polarAsserv.Measure_AngularSpeed));
            worldMap.UpdateWorldMapDisplay();
            #endregion
            #region Oscilloscope
            oscilloLinSpeed.AddPointToLine(1, timestamp, polarAsserv.Measure_LinearSpeed);
            oscilloLinSpeed.AddPointToLine(2, timestamp, polarAsserv.Order_LinearSpeed);
            oscilloLinSpeed.AddPointToLine(3, timestamp, polarAsserv.Command_LinearSpeed);

            oscilloAngSpeed.AddPointToLine(1, timestamp, polarAsserv.Measure_AngularSpeed);
            oscilloAngSpeed.AddPointToLine(2, timestamp, polarAsserv.Order_AngularSpeed);
            oscilloAngSpeed.AddPointToLine(3, timestamp, polarAsserv.Command_AngularSpeed);
            #endregion
            #region Asserv Array
            asservSpeedDisplay.UpdatePolarOdometrySpeed(polarAsserv.Measure_LinearSpeed, polarAsserv.Measure_AngularSpeed);
            asservSpeedDisplay.UpdatePolarSpeedConsigneValues(polarAsserv.Order_LinearSpeed, polarAsserv.Order_AngularSpeed);
            asservSpeedDisplay.UpdatePolarSpeedCommandValues(polarAsserv.Command_LinearSpeed, polarAsserv.Command_AngularSpeed);
            asservSpeedDisplay.UpdatePolarSpeedErrorValues(polarAsserv.Error_LinearSpeed, polarAsserv.Error_AngularSpeed);

            asservSpeedDisplay.UpdatePolarSpeedCorrectionGains(polarAsserv.Kp_Linear, polarAsserv.Kp_Angular,
                polarAsserv.Ki_Linear, polarAsserv.Ki_Angular, polarAsserv.Kd_Linear, polarAsserv.Kd_Angular);

            asservSpeedDisplay.UpdatePolarSpeedCorrectionValues(polarAsserv.Correction_P_Linear, polarAsserv.Correction_P_Angular,
                polarAsserv.Correction_I_Linear, polarAsserv.Correction_I_Angular, polarAsserv.Correction_D_Linear, polarAsserv.Correction_D_Angular);

            asservSpeedDisplay.UpdatePolarSpeedCorrectionLimits(polarAsserv.Kp_Max_Linear, polarAsserv.Kp_Max_Angular,
                polarAsserv.Ki_Max_Linear, polarAsserv.Ki_Max_Angular, polarAsserv.Kd_Max_Linear, polarAsserv.Kd_Max_Angular);
            #endregion
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
