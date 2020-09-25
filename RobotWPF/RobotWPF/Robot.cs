using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSerialPort;

namespace RobotWPF
{
    class Robot
    {
        public enum StateReception
        {
            Waiting,
            FunctionMSB,
            FunctionLSB,
            PayloadLengthMSB,
            PayloadLengthLSB,
            Payload,
            CheckSum
        }

        public enum StateRobot
        {
            STATE_ATTENTE = 0,
            STATE_ATTENTE_EN_COURS = 1,
            STATE_AVANCE = 2,
            STATE_AVANCE_EN_COURS = 3,
            STATE_TOURNE_GAUCHE = 4,
            STATE_TOURNE_GAUCHE_EN_COURS = 5,
            STATE_TOURNE_DROITE = 6,
            STATE_TOURNE_DROITE_EN_COURS = 7,
            STATE_TOURNE_SUR_PLACE_GAUCHE = 8,
            STATE_TOURNE_SUR_PLACE_GAUCHE_EN_COURS = 9,
            STATE_TOURNE_SUR_PLACE_DROITE = 10 ,
            STATE_TOURNE_SUR_PLACE_DROITE_EN_COURS = 11 ,
            STATE_ARRET = 12 ,
            STATE_ARRET_EN_COURS = 13 ,
            STATE_RECULE = 14 ,
            STATE_RECULE_EN_COURS = 15
        }

        public StateReception rcvState = StateReception.Waiting;
        public StateReception rcvBefore = StateReception.Waiting;
        public string decodedText = "";
        public int IR1 = 0;
        public int IR2 = 0;
        public int IR3 = 0;
        public int Motor1 = 0;
        public int Motor2 = 0;
        int msgDecodedFunction = 0;
        int msgDecodedPayloadLength = 0;
        byte[] msgDecodedPayload;
        public ReliableSerialPort serialPort;
        public bool msgIsWrong = false;
        int msgDecodedPayloadIndex = 0;
        public void DecodeMessage(byte c)
        {
            rcvBefore = rcvState;
            switch (rcvState)
            {
                case StateReception.Waiting:
                    if (c == 0xFE)
                    {
                        // Message begin
                        msgDecodedFunction = 0;
                        msgDecodedPayloadLength = 0;
                        msgDecodedPayloadIndex = 0;
                        rcvState = StateReception.FunctionMSB;
                    }
                    break;
                case StateReception.FunctionMSB:
                    msgDecodedFunction = (ushort)(c << 8);
                    rcvState = StateReception.FunctionLSB;
                    break;
                case StateReception.FunctionLSB:
                    msgDecodedFunction += (ushort)(c << 0);
                    rcvState = StateReception.PayloadLengthMSB;
                    break;
                case StateReception.PayloadLengthMSB:
                    msgDecodedPayloadLength = (ushort)(c << 8);
                    rcvState = StateReception.PayloadLengthLSB;
                    break;
                case StateReception.PayloadLengthLSB:
                    msgDecodedPayloadLength += (ushort)(c << 0);
                    if (msgDecodedPayloadLength > 0)
                    {
                        msgDecodedPayload = new byte[msgDecodedPayloadLength];
                        rcvState = StateReception.Payload;
                    }
                    else
                    {
                        rcvState = StateReception.CheckSum;
                    }
                    break;

                case StateReception.Payload:
                    msgDecodedPayload[msgDecodedPayloadIndex] = c;
                    msgDecodedPayloadIndex++;
                    if (msgDecodedPayloadIndex >= msgDecodedPayloadLength)
                    {
                        rcvState = StateReception.CheckSum;
                    }
                    break;
                case StateReception.CheckSum:
                    byte calculatedChecksum, receivedChecksum = c;
                    calculatedChecksum = CalculateChecksum(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload);
                    if (calculatedChecksum == receivedChecksum)
                    {
                        // Success, on a un message 
                        switch (msgDecodedFunction)
                        {
                            case 0x0080:
                                // Message Text
                                decodedText += "Text: " + Encoding.UTF8.GetString(msgDecodedPayload) + "\n";
                                break;
                            case 0x0030:
                                IR1 = (int)msgDecodedPayload[0];
                                IR2 = (int)msgDecodedPayload[1];
                                IR3 = (int)msgDecodedPayload[2];
                                break;
                            case 0x0040:
                                Motor1 = (int)msgDecodedPayload[0] - 128;
                                Motor2 = (int)msgDecodedPayload[1] - 128;
                                break;
                        }
                        Console.WriteLine("Message is Correct");
                        msgIsWrong = false;
                    }
                    else
                    {
                        Console.WriteLine("Message has an error");
                        msgIsWrong = true;
                    }
                    rcvState = StateReception.Waiting;
                    break;
                default:
                    rcvState = StateReception.Waiting;
                    break;
            }
        }
        public void UartEncodeAndSendMessage(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);
            byte[] checksum = new byte[] { CalculateChecksum(msgFunction, msgPayloadLength, msgPayload) };
            msg = Combine(msg, checksum);
            if (serialPort != null)
                serialPort.Write(msg, 0, msg.Length);
        }

        private byte[] EncodeWithoutChecksum(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            // Convert Function to byte
            byte LbyteFunction = (byte)(msgFunction >> 0);
            byte HbyteFunction = (byte)(msgFunction >> 8);

            byte LbytePayloadsLength = (byte)(msgPayloadLength >> 0);
            byte HbytePayloadsLength = (byte)(msgPayloadLength >> 8);

            // Append all bytes
            byte[] msg = new byte[] { 0xFE, HbyteFunction, LbyteFunction, HbytePayloadsLength, LbytePayloadsLength };
            msg = Combine(msg, msgPayload);
            return msg;
        }
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
        private byte CalculateChecksum(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);

            byte checksum = msg[0];
            for (int i = 1; i < msg.Length; i++)
            {
                checksum ^= msg[i];
            }
            System.Diagnostics.Debug.WriteLine("[CHECKSUM] " + msg + " Result : " + checksum);
            return checksum;
        }
    }
}
