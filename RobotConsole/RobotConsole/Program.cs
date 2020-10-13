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
        
        static MsgEncoder msgEncoder;
        static MsgDecoder msgDecoder;
        
        static void Main(string[] args)
        {
            Console.WriteLine("[MAIN] Start Booting Sequence");
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();


            Console.WriteLine("[MAIN] End Booting Sequence");
            Console.ReadKey();
            
            
        }

        private void GetSerialPort()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("[SERIAL] List of Serial available:");
                    Console.WriteLine("\x1b[31mTEST\x1b[0m");
                    if (queryObj["Caption"].ToString().Contains("(COM"))
                    {
                        string queryString = queryObj["Caption"].ToString();
                        string[] queryStringArray = queryString.Split(' ');
                        string COM_Name = queryStringArray[queryStringArray.Length - 1]; // Get (COMx)
                        string COM_Description = queryString.Remove(queryString.IndexOf(COM_Name));
                        COM_Name = COM_Name.Remove(COM_Name.Length - 1).Remove(1, 1); // Remove ( ) From COM
                        
                        if (COM_Description == "USB Serial Port")
                        {
                            Console.WriteLine("\x1b[32m" + COM_Name + "\x1b[0m");
                        } else
                        {
                            Console.WriteLine("\x1b[31m" + COM_Name + "\x1b[0m");
                        }
                    }

                }
            }
            catch (ManagementException)
            {
                Console.WriteLine("[SERIAL] ERROR while getting descritption");
            }

        }
    }
}
