using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class Protocol
    {
        public const byte SOF = 0xFE;
        public const ushort MAX_MSG_LENGHT = 255;
        public enum FunctionName : ushort
        {
            SET_LED                 = 0x0020,
            GET_IR                  = 0x0030,
            SET_MOTOR               = 0x0040,
            GET_STATE               = 0x0050,
            SET_STATE               = 0x0051,
            GET_POSITION            = 0x0061,
            SET_RESET_POSITION      = 0x0062,
            SET_POSITION            = 0x0063,
            GET_TEXT                = 0x0080,
            GET_ASSERV_POLAR_PARAM  = 0x0090,
            SET_ASSERV_PARAM        = 0x0091
            
            // Add all protocol
        }

        public enum State : ushort
        {
            CUSTOM          = 0 ,
            FORWARD         = 1 ,
            FORWARD_LEFT    = 2 ,
            FORWARD_RIGHT   = 3 ,
            TURN_LEFT       = 4 ,
            TURN_RIGHT      = 5 ,
            STOP            = 6 ,
            BACKWARD        = 7 ,
            FAST_FORWARD    = 8 ,
            SLOW_FORWARD    = 9
        }
        public static short CheckFunctionLenght(ushort msgFunction)
        {
            switch (msgFunction)
            {
                // -2               : UNKNOW
                // -1               : UNLIMITED 
                // [0:MAX_LENGHT]   : FIXED
                case (ushort)FunctionName.SET_LED:
                    return 2;
                case (ushort)FunctionName.GET_IR:
                    return 3;
                case (ushort)FunctionName.SET_MOTOR:
                    return 2;
                case (ushort)FunctionName.GET_TEXT:
                    return -1;
                case (ushort)FunctionName.GET_STATE:
                    return 5;
                case (ushort)FunctionName.SET_STATE:
                    return 1;
                case (ushort)FunctionName.GET_POSITION:
                    return 24;
                case (ushort)FunctionName.SET_RESET_POSITION:
                    return 1;
                case (ushort)FunctionName.SET_POSITION:
                    return 1; // Not implemented
                case (ushort)FunctionName.GET_ASSERV_POLAR_PARAM:
                    return 104;
                default:
                    return -2;


            }
        }

        #region Class
        
        #endregion
    }
}
