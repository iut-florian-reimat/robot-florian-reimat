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
        static void Main(string[] args)
        {
            ConsoleFormat.ConsoleInformationFormat("MAIN", "Begin Booting Sequence", true);
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            serial = new Serial();
            cmdGui = new CMDGui();
            if (/*serial.AutoConnectSerial()  TEMPO */ true)
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

            
            
            Console.WriteLine();
            ConsoleFormat.ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            for (int i = 0; i < 999999; i++)
            {
                Random rnd = new Random();
                Byte[] randombyte = new Byte[255];
                rnd.NextBytes(randombyte);
                foreach (byte x in randombyte)
                {
                    msgDecoder.ByteReceived(x);
                }

            }
            Console.ReadKey();
            //cmdGui.InitializeCMDGui(); 
        }


    }
}
