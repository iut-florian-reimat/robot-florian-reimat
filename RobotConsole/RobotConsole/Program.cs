using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;


namespace RobotConsole
{
    class Program
    {
        ReliableSerialPort serialPort;
        
        static CMDGui cmdGui;
        static MsgEncoder msgEncoder;
        static MsgDecoder msgDecoder;
        
        static void Main(string[] args)
        {
            ConsoleTitleFormat("MAIN", "Begin Booting Sequence", true);
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            cmdGui = new CMDGui();
            GetSerialPort();
            ConsoleTitleFormat("MAIN", "End  Booting Sequence", true);
            Console.ReadKey();
            //cmdGui.InitializeCMDGui(); 
        }

        static public void ConsoleTitleFormat(string title, string content, bool isCorrect)
        {
            Console.Write("[");
            if (isCorrect)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(title);
            Console.ResetColor();
            Console.WriteLine("] " + content);
        }

        static public void ConsoleListFormat(string content, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("    - " + content);
        }

        private static void GetSerialPort()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
                ConsoleTitleFormat("SERIAL", "List of Serial available:", true);
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj != null && queryObj["Caption"] != null)
                    {
                        if (queryObj["Caption"].ToString().Contains("(COM"))
                        {
                            string queryString = queryObj["Caption"].ToString();
                            string[] queryStringArray = queryString.Split(' ');
                            string COM_Name = queryStringArray[queryStringArray.Length - 1]; // Get (COMx)
                            string COM_Description = queryString.Remove(queryString.IndexOf(COM_Name));
                            COM_Name = COM_Name.Remove(COM_Name.Length - 1).Remove(1, 1); // Remove ( ) From COM
                            ConsoleColor foreground_color;
                            if (COM_Description == "USB Serial Port")
                            {
                                foreground_color = ConsoleColor.DarkGreen;
                            }
                            else
                            {
                                foreground_color = ConsoleColor.Gray;
                            }
                            ConsoleListFormat(COM_Name, foreground_color);
                        }
                    }
                }
                Console.ResetColor();
            }
            catch (ManagementException)
            {
                ConsoleTitleFormat("SERIAL", "ERROR while getting descritption", false);
            }
        }
    }
}
