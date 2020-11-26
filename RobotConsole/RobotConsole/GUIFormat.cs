using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotInterface;

namespace RobotConsole
{
    class GUIFormat
    {
        public static void UpdatePosition(object sender, Protocol.PositionMessageArgs e)
        {
            WpfRobotInterface.xPos = e.x;
            WpfRobotInterface.yPos = e.y;
            WpfRobotInterface.aPos = e.theta;
            WpfRobotInterface.linearSpeed = e.linearSpeed;
            WpfRobotInterface.angularSpeed = e.angularSpeed;
        }

        public static void ResetPosition(object sender, EventArgs e)
        {
            Serial.msgGenerator.GenerateMessageSetResetPosition();
        }
    }
}
