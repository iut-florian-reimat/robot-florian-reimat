using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RobotConsole
{

    class MsgDecoder 
    {
        private enum State
        {
            Waiting,
            FunctionMSB,
            FunctionLSB,
            PayloadLengthMSB,
            PayloadLengthLSB,
            Payload,
            CheckSum
        }

        private enum FunctionName
        {
            Set_LED     = 0x0020,
            Get_IR      = 0x0030,
            Set_Motor   = 0x0040,
            Get_Text    = 0x0080
            // Add all protocol
        }

        static State actualState = State.Waiting;
        const byte SOF = 0xFE;
        private static byte functionMSB;
        private static byte functionLSB;
        private static byte payloadLenghtMSB;
        private static byte payloadLenghtLSB;

        private static ushort msgFunction;
        private static ushort msgPayloadLenght;
        private static byte[] msgPayload;
        private static byte msgChecksum;

        private static int msgPayloadIndex = 0; // Maybe edit type
        public static void ByteReceived(byte b)
        {
            switch (actualState)
            {
                case State.Waiting:
                    if (b == SOF)
                    {
                        OnSOFReceived(new DecodeByteArgs(b));
                    }
                    else
                    {
                        OnUnknowReceived(new DecodeByteArgs(b));
                    }
                    break;

                case State.FunctionMSB:
                    OnFunctionMSBReceived(new DecodeByteArgs(b));
                    break;

                case State.FunctionLSB:
                    OnFunctionLSBReceived(new DecodeByteArgs(b));
                    break;

                case State.PayloadLengthMSB:
                    OnPayloadLenghtMSBReceided(new DecodeByteArgs(b));
                    break;

                case State.PayloadLengthLSB:
                    OnPayloadLenghtLSBReceided(new DecodeByteArgs(b));
                    break;

                case State.Payload:
                    OnPayloadByteReceived(new DecodeByteArgs(b));
                    break;

                case State.CheckSum:
                    OnCheckSumReceived(new DecodeByteArgs(b));
                    break;
            }
        }


        private static void OnSOFReceived(DecodeByteArgs e) 
        {
            actualState = State.FunctionMSB;
        }

        private static void OnUnknowReceived(DecodeByteArgs e)
        {

        }
        private static void OnFunctionMSBReceived(DecodeByteArgs e)
        {
            functionMSB = e.b;
            msgFunction = (ushort)(e.b << 8);
            actualState = State.FunctionLSB;
        }

        private static void OnFunctionLSBReceived(DecodeByteArgs e)
        {
            functionLSB = e.b;
            msgFunction += (ushort)(e.b << 0);
            actualState = State.PayloadLengthMSB;
        }

        private static void OnPayloadLenghtMSBReceided(DecodeByteArgs e)
        {
            payloadLenghtMSB = e.b;
            msgPayloadLenght = (ushort)(e.b << 8);
            actualState = State.PayloadLengthLSB;
        }

        private static void OnPayloadLenghtLSBReceided(DecodeByteArgs e)
        {
            payloadLenghtLSB = e.b;
            msgPayloadLenght += (ushort)(e.b << 0);
            actualState = State.Payload;

            msgPayloadIndex = 0;
            msgPayload = new byte[msgPayloadLenght];
        }

        private static void OnPayloadByteReceived(DecodeByteArgs e)
        {
            msgPayload[msgPayloadIndex] = e.b;
            msgPayloadIndex++;
            if (msgPayloadIndex == msgPayloadLenght)
            {
                OnPayloadReceived(new DecodeByteArgs(msgPayload));
            }
        }

        private static void OnPayloadReceived(DecodeByteArgs e)
        {
            actualState = State.CheckSum;
        }

        private static void OnCheckSumReceived(DecodeByteArgs e)
        {
            msgChecksum = e.b;
            if (msgChecksum == CalculateChecksum())
            {
                OnCorrectChecksumReceived(new DecodeByteArgs(e.b)); // MODIFY ARGS
            } else
            {
                OnWrongChecksumReceived(new DecodeByteArgs(e.b)); // MODIFY ARGS
            }
            actualState = State.Waiting;
        }

        private static void OnCorrectChecksumReceived(DecodeByteArgs e) // MODIFY ARGS
        {

        }

        private static void OnWrongChecksumReceived(DecodeByteArgs e) // MODIFY ARGS
        {

        }

        private static byte CalculateChecksum()
        {
            byte checksum = SOF;
            checksum ^= functionMSB;
            checksum ^= functionLSB;
            checksum ^= payloadLenghtMSB;
            checksum ^= payloadLenghtLSB;
            foreach (byte x in msgPayload)
            {
                checksum ^= x;
            }
            return checksum;
        }

        public class DecodeByteArgs : EventArgs
        {
            public byte b { get; set; }
            public byte [] payload { get; set; }

            public DecodeByteArgs(byte[] payload_a)
            {
                payload = payload_a;
            }
            public DecodeByteArgs(byte b_a)
            {
                b = b_a;
            }
        }
    }
}
