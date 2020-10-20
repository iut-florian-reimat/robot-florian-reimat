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
                default:
                    return -2;


            }
        }
        public class MessageByteArgs : EventArgs
        {
            public byte SOF { get; set; }
            public byte functionMsb { get; set; }
            public byte functionLsb { get; set; }
            public byte lenghtMsb { get; set; }
            public byte lenghtLsb { get; set; }
            public byte checksum { get; set; }
            public ushort msgFunction { get; set; }
            public ushort msgPayloadLenght { get; set; }
            public byte[] msgPayload { get; set; }

            public MessageByteArgs(byte SOF_a, byte functionMsb_a, byte functionLsb_a, byte lenghtMsb_a, byte lenghtLsb_a, byte[] msgPaylaod_a, byte checksum_a)
            {
                SOF = SOF_a;
                functionMsb = functionMsb_a;
                functionLsb = functionLsb_a;
                lenghtMsb = lenghtMsb_a;
                lenghtLsb = lenghtLsb_a;
                msgPayload = msgPaylaod_a;
                checksum = checksum_a;
                ConvertByteToFunction();
            }

            public MessageByteArgs(ushort msgFunction_a, ushort msgPayloadLenght_a, byte[] msgPayload_a, byte checksum_a)
            {
                msgFunction = msgFunction_a;
                msgPayloadLenght = msgPayloadLenght_a;
                msgPayload = msgPayload_a;
                checksum = checksum_a;
                ConvertFunctionToByte();
            }
            private void ConvertByteToFunction()
            {
                msgFunction = (ushort)(functionMsb << 8 + functionLsb << 0);
                msgPayloadLenght = (ushort)(lenghtMsb << 8 + lenghtLsb << 0);
            }

            private void ConvertFunctionToByte()
            {
                functionLsb = (byte)(msgFunction >> 0);
                functionMsb = (byte)(msgFunction >> 8);

                lenghtLsb = (byte)(msgPayloadLenght >> 0);
                lenghtMsb = (byte)(msgPayloadLenght >> 8);
            }
        }
        public class LedMessageArgs : EventArgs
        {
            public ushort led_number { get; set; }
            public bool state { get; set; }

            public LedMessageArgs(ushort led_number_a, bool state_a)
            {
                led_number = led_number_a;
                state = state_a;
            }
        }
        public class MotorMessageArgs : EventArgs
        {
            public sbyte left_speed { get; set; }
            public sbyte right_speed { get; set; }

            public MotorMessageArgs(sbyte left_speed_a, sbyte right_speed_a)
            {
                left_speed = left_speed_a;
                right_speed = right_speed_a;
            }
        }
        public class StateMessageArgs : EventArgs
        {
            public ushort state { get; set; }
            public uint time { get; set; }
            public StateMessageArgs(ushort state_a)
            {
                state = state_a;
            }

            public StateMessageArgs(ushort state_a, uint time_a)
            {
                state = state_a;
                time = time_a;
            }
        }
        public class IRMessageArgs : EventArgs
        {
            public ushort left_IR { get; set; }
            public ushort center_IR { get; set; }
            public ushort right_IR { get; set; }
        
            public IRMessageArgs(ushort left, ushort center, ushort right)
            {
                left_IR = left;
                center_IR = center;
                right_IR = right;
            }
        }
    }
}
