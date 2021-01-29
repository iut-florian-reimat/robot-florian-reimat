using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotInterface;
using EventArgsLibrary;

namespace RobotConsole
{
    class GUIFormat
    {
        public static void UpdatePosition(object sender, PositionMessageArgs e)
        {
            WpfRobotInterface.Position_X = e.x;
            WpfRobotInterface.Position_Y = e.y;
            WpfRobotInterface.Position_Angular = e.theta;
            WpfRobotInterface.polarAsserv.Measure_LinearSpeed = e.linearSpeed;
            WpfRobotInterface.polarAsserv.Measure_AngularSpeed = e.angularSpeed;
        }

        public static void UpdateAsserv(object sender, PolarAsservMessageArgs e)
        {
            WpfRobotInterface.polarAsserv = e;
        }



        public static void ResetPosition(object sender, EventArgs e)
        {
            Serial.msgGenerator.GenerateMessageSetResetPosition();
        }
    }
}
