using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;
using System.IO.Ports;
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
            if (serial.AutoConnectSerial())
            {
                // msgDecoder.OnSOFReceived ----- TODO
            }

            ConsoleFormat.ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            Console.ReadKey();
            //cmdGui.InitializeCMDGui(); 
        }


    }
}
