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
            SET_LED     = 0x0020,
            GET_IR      = 0x0030,
            SET_MOTOR   = 0x0040,
            GET_STATE   = 0x0050,
            SET_STATE   = 0x0051,
            GET_TEXT    = 0x0080
            // Add all protocol
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
                default:
                    return -2;


            }
        }
    }
}
