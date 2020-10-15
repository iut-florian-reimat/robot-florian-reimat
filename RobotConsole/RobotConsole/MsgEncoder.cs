using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class MsgEncoder
    {
        
        public bool UartEncodeAndSendMessage(ushort msgFunction, byte[] msgPayload)
        {
            short PayloadLenghtTest = Protocol.CheckFunctionLenght(msgFunction);
            if (PayloadLenghtTest != -2)
            {
                ushort msgPayloadLenght = (ushort)msgPayload.Length;
                if (PayloadLenghtTest != -1)
                {
                    msgPayloadLenght = (ushort)PayloadLenghtTest;
                    
                }
                if (msgPayloadLenght == msgPayload.Length)
                {
                    byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLenght, msgPayload);
                    byte checksum = CalculateChecksum(msgFunction, msgPayloadLenght, msgPayload);

                    msg[msg.Length - 1] = checksum;
                    if (Program.serialPort != null)
                    {
                        Program.serialPort.Write(msg, 0, msg.Length);
                        return true;
                    } 
                }
            }
            return false;
        }

        private static byte[] EncodeWithoutChecksum(ushort msgFunction, ushort msgPayloadLength, byte[] msgPayload)
        {
            // Convert Function to byte
            byte LbyteFunction = (byte)(msgFunction >> 0);
            byte HbyteFunction = (byte)(msgFunction >> 8);

            byte LbytePayloadsLength = (byte)(msgPayloadLength >> 0);
            byte HbytePayloadsLength = (byte)(msgPayloadLength >> 8);

            // Append all bytes
            byte[] msg = new byte[6 + msgPayload.Length];
            ushort i;
            msg[0] = Protocol.SOF;
            msg[1] = HbyteFunction;
            msg[2] = LbyteFunction;
            msg[3] = HbytePayloadsLength;
            msg[4] = LbytePayloadsLength;
            for (i = 0; i < msgPayload.Length; i++)
            {
                msg[5 + i] = msgPayload[i];
            }
            return msg;
        }

        private static byte CalculateChecksum(ushort msgFunction, ushort msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);

            byte checksum = msg[0];
            for (int i = 1; i < msg.Length; i++)
            {
                checksum ^= msg[i];
            }
            return checksum;
        }
    }
}
