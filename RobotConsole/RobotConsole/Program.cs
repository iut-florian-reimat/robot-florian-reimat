using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;

namespace RobotConsole
{
    class Program
    {
        public static ReliableSerialPort serialPort;
        
        public static MsgEncoder msgEncoder;
        public static MsgDecoder msgDecoder;
        public static MsgGenerator msgGenerator;
        public static MsgProcessor msgProcessor;
        static Serial serial;

        private static bool hex_viewer = true;
        private static bool hex_error_viewer = true;
        private static bool hex_sender = true;
        private static bool hex_error_sender = true;
        private static bool function_received = false;

        static void Main(string[] args)
        {
            ConsoleFormat.ConsoleInformationFormat("MAIN", "Begin Booting Sequence", true);
            
            serial = new Serial();
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            msgProcessor = new MsgProcessor();
            msgGenerator = new MsgGenerator();
           
            ConsoleFormat.ConsoleInformationFormat("SERIAL", "Begin Auto-Connection", true);
            bool isSerialConnected = serial.AutoConnectSerial();

            ConsoleFormat.ConsoleInformationFormat("DECODER", "Message Decoder is launched", true);
            ConsoleFormat.ConsoleInformationFormat("ENCODER", "Message Encoder is launched", true);
            ConsoleFormat.ConsoleInformationFormat("PROCESSOR", "Message Processor is launched", true);
            ConsoleFormat.ConsoleInformationFormat("GENERATOR", "Message Generator is launched", true);
            if (isSerialConnected)
            {
                
                if (hex_viewer)
                {
                    msgDecoder.OnUnknowByteEvent += ConsoleFormat.PrintUnknowByte;
                    msgDecoder.OnSOFByteReceivedEvent += ConsoleFormat.PrintSOF;
                    msgDecoder.OnFunctionMSBByteReceivedEvent += ConsoleFormat.PrintFunctionMSB;
                    msgDecoder.OnFunctionLSBByteReceivedEvent += ConsoleFormat.PrintFunctionLSB;
                    msgDecoder.OnPayloadLenghtMSBByteReceivedEvent += ConsoleFormat.PrintLenghtMSB;
                    msgDecoder.OnPayloadLenghtLSBByteReceivedEvent += ConsoleFormat.PrintLenghtLSB;
                    msgDecoder.OnPayloadByteReceivedEvent += ConsoleFormat.PrintPayloadByte;
                    msgDecoder.OnCorrectChecksumEvent += ConsoleFormat.PrintCorrectChecksum;
                    msgDecoder.OnWrongChecksumEvent += ConsoleFormat.PrintWrongChecksum;
                }

                if (hex_error_viewer)
                {
                    msgDecoder.OnOverLenghtMessageEvent += ConsoleFormat.PrintOverLenghtWarning;
                    msgDecoder.OnUnknowFunctionEvent += ConsoleFormat.PrintUnknowFunctionReceived;
                    msgDecoder.OnWrongLenghtFunctionEvent += ConsoleFormat.PrintWrongFonctionLenghtReceived;
                    msgDecoder.OnWrongChecksumEvent += ConsoleFormat.PrintWrongMessage;
                }

                if (hex_sender)
                {
                    msgEncoder.OnSendMessageEvent += ConsoleFormat.PrintSendMsg;
                }

                if (hex_error_sender)
                {
                    msgEncoder.OnSerialDisconnectedEvent += ConsoleFormat.PrintOnSerialDisconnectedError;
                    msgEncoder.OnUnknownFunctionSentEvent += ConsoleFormat.PrintUnknowFunctionSent;
                    msgEncoder.OnWrongPayloadSentEvent += ConsoleFormat.PrintWrongFunctionLenghtSent;
                }

                if (function_received)
                {
                    msgProcessor.OnIRMessageReceivedEvent += ConsoleFormat.PrintIRMessageReceived;
                }
                msgDecoder.OnCorrectChecksumEvent += msgProcessor.MessageProcessor;
            }

            ConsoleFormat.ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            msgGenerator.GenerateMessageSetLed(1, true);
            msgGenerator.GenerateMessageSetLed(3, true);
            Console.ReadKey();
            
        }
    }
}
