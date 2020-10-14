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
        static ReliableSerialPort serialPort;
        
        static CMDGui cmdGui;
        static MsgEncoder msgEncoder;
        static MsgDecoder msgDecoder;
        static SerialEvent serialEvent;
        
        static void Main(string[] args)
        {
            ConsoleInformationFormat("MAIN", "Begin Booting Sequence", true);
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            serialEvent = new SerialEvent();
            cmdGui = new CMDGui();
            ushort i = 0;
            do
            {
                i++;
                ConsoleInformationFormat("SERIAL", "Attempt Connection #" + i, true);
                string AvailableCOM = GetSerialPort();

                if (AvailableCOM != "") {
                    ConsoleInformationFormat("SERIAL", "Available Serial: " + AvailableCOM , true);
                    serialPort = new ReliableSerialPort(AvailableCOM, 115200, Parity.None, 8, StopBits.One);
                    serialPort.DataReceived += serialEvent.SerialPort_DataReceived;
                    serialEvent.OnSerialConnected(new SerialEventArgs(AvailableCOM));
                    serialPort.Open();
                    
                } else
                {
                    ConsoleInformationFormat("SERIAL", "No Connection Available", false);
                }
                System.Threading.Thread.Sleep(1000);
            } while (serialPort == null && i <= 255);
            
            ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            Console.ReadKey();
            //cmdGui.InitializeCMDGui(); 
        }

        

        

        static public void ConsoleTitleFormat (string title, bool isCorrect)
        {
            Console.Write("[");
            if (isCorrect)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(title);
            Console.ResetColor();
            Console.Write("] ");
        }
        static public void ConsoleInformationFormat(string title, string content, bool isCorrect)
        {
            ConsoleTitleFormat(title, isCorrect);
            Console.WriteLine(content);
        }

        static public void ConsoleListFormat(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("    - " + content);
        }

        private static string GetSerialPort()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
                ConsoleInformationFormat("SERIAL", "List of Serial available:", true);
                string AvailableCOM = "";
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj != null && queryObj["Caption"] != null)
                    {
                        if (queryObj["Caption"].ToString().Contains("(COM"))
                        {
                            string queryString = queryObj["Caption"].ToString();
                            string[] queryStringArray = queryString.Split(' ');
                            string COM_Name = queryStringArray[queryStringArray.Length - 1]; // Get (COMx)
                            string COM_Description = queryString.Remove(queryString.IndexOf(COM_Name) - 1);
                            COM_Name = COM_Name.Remove(COM_Name.Length - 1).Remove(0, 1); // Remove ( ) From COM
                            ConsoleColor foreground_color = ConsoleColor.DarkGray;
                            if (COM_Description == "USB Serial Port")
                            {
                                AvailableCOM = COM_Name;
                                foreground_color = ConsoleColor.DarkGreen;
                            }
                            ConsoleListFormat(COM_Name, foreground_color);
                        }
                    }
                }
                Console.ResetColor();
                return (AvailableCOM);
            }
            catch (ManagementException)
            {
                ConsoleInformationFormat("SERIAL", "ERROR while getting descritption", false);
                return ""; 
            }
        }
    }
}
