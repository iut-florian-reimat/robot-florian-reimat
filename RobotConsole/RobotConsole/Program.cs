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
        
        static CMDGui cmdGui;
        static MsgEncoder msgEncoder;
        public static MsgDecoder msgDecoder;
        static Serial serial;

        private static bool hex_viewer = true;
        private static bool hex_error_viewer = true;
        static void Main(string[] args)
        {
            ConsoleFormat.ConsoleInformationFormat("MAIN", "Begin Booting Sequence", true);
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            serial = new Serial();
            cmdGui = new CMDGui();
            if (serial.AutoConnectSerial())
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
                    msgDecoder.OnUnknowFunctionEvent += ConsoleFormat.PrintUnknowFunction;
                    msgDecoder.OnWrongLenghtFunctionEvent += ConsoleFormat.PrintWrongFonctionLenght;
                    msgDecoder.OnWrongChecksumEvent += ConsoleFormat.PrintWrongMessage;
                }
            }
            ConsoleFormat.ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            byte[] array = new byte[] { 1 };
            msgEncoder.UartEncodeAndSendMessage((ushort) Protocol.FunctionName.SET_STATE, array);
            Console.ReadKey();
            //cmdGui.InitializeCMDGui(); 
        }


    }
}
