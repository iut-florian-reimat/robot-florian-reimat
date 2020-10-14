using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSerialPort;

namespace RobotConsole
{
    class SerialEvent
    {
        static Queue<byte> byteListReceived = new Queue<byte>();
        public void OnSerialConnected(SerialEventArgs e)
        {
            Program.ConsoleInformationFormat("SERIAL", "Connection Enabled: " + e.COM, true);
        }

        public void SerialPort_DataReceived(object sender, DataReceivedArgs e)
        {
            for (int i = 0; i < e.Data.Length; i++)
            {
                byteListReceived.Enqueue(e.Data[i]);
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
