using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;
using System.IO.Ports;

namespace RobotConsole
{
    class Serial
    {
        public bool AutoConnectSerial()
        {
            ushort i = 0;
            do
            {
                i++;
                ConsoleFormat.ConsoleInformationFormat("SERIAL", "Attempt Connection #" + i, true);
                string AvailableCOM = GetSerialPort();

                if (AvailableCOM != "")
                {
                    ConsoleFormat.ConsoleInformationFormat("SERIAL", "Available Serial: " + AvailableCOM, true);
                    Program.serialPort = new ReliableSerialPort(AvailableCOM, 115200, Parity.None, 8, StopBits.One);
                    Program.serialPort.DataReceived += SerialPort_DataReceived;
                    OnSerialConnected(new SerialEventArgs(AvailableCOM));
                    Program.serialPort.Open();

                }
                else
                {
                    ConsoleFormat.ConsoleInformationFormat("SERIAL", "No Connection Available", false);
                }
                System.Threading.Thread.Sleep(1000);
            } while (Program.serialPort == null && i <= 255);
            return (Program.serialPort != null);
        }

        private static string GetSerialPort()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
                ConsoleFormat.ConsoleInformationFormat("SERIAL", "List of Serial available:", true);
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
                            ConsoleFormat.ConsoleListFormat(COM_Name, foreground_color);
                        }
                    }
                }
                Console.ResetColor();
                return (AvailableCOM);
            }
            catch (ManagementException)
            {
                ConsoleFormat.ConsoleInformationFormat("SERIAL", "ERROR while getting descritption", false);
                return "";
            }
        }
        public void OnSerialConnected(SerialEventArgs e)
        {
            ConsoleFormat.ConsoleInformationFormat("SERIAL", "Connection Enabled: " + e.COM, true);
        }

        public void SerialPort_DataReceived(object sender, DataReceivedArgs e)
        {
            for (int i = 0; i < e.Data.Length; i++)
            {
                Program.msgDecoder.ByteReceived(e.Data[i]);
            }
        }
    }
    

    class SerialEventArgs : EventArgs
    {
        public string COM { get; set; }

        public SerialEventArgs(string COM_a)
        {
            COM = COM_a;
        }
    }
}
