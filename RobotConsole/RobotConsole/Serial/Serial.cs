﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;
using System.IO.Ports;

namespace RobotConsole
{
    // /!\ Warning this library need an acces to ExtendedSerialPort.dll
    class Serial
    {
        #region Attributes
        public static ReliableSerialPort serialPort;

        public static MsgEncoder msgEncoder;
        public static MsgDecoder msgDecoder;
        public static MsgGenerator msgGenerator;
        public static MsgProcessor msgProcessor;
        #endregion
        #region Constructor
        public Serial()
        {
            msgDecoder = new MsgDecoder();
            msgEncoder = new MsgEncoder();
            msgProcessor = new MsgProcessor();
            msgGenerator = new MsgGenerator();

            
        }
        #endregion
        #region Method
        public bool AutoConnectSerial(uint timespan = 1000, uint trial_max = 255)
        {
            OnAutoConnectionLaunched();
            ushort i = 0;
            do
            {
                i++;
                OnNewSerialAttempts(i);
                string AvailableCOM = GetSerialPort();

                if (AvailableCOM != "")
                {
                    OnSerialAvailable(AvailableCOM);
                }
                else
                {
                    OnNoConnectionAvailable();
                }
                System.Threading.Thread.Sleep((int) timespan); // Not Good 
            } while (serialPort == null && i <= trial_max);
            return (serialPort != null);
        }

        private string GetSerialPort()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
                OnSerialAvailableList();
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
                            if (COM_Description == "USB Serial Port")
                            {
                                AvailableCOM = COM_Name;
                                OnCorrectCOMAvailable(COM_Name);
                            } else
                            {
                                OnWrongCOMAvailable(COM_Name);
                            }
                        }
                    }
                }
                return (AvailableCOM);
            }
            catch (ManagementException)
            {
                OnErrorWhileGettingDescription();
                return "";
            }
        }
        #endregion
        #region Event
        public event EventHandler<SerialEventArgs> OnSerialConnectedEvent;
        public event EventHandler<EventArgs> OnAutoConnectionLaunchedEvent;
        public event EventHandler<AttemptsEventArgs> OnNewSerialAttemptsEvent;
        public event EventHandler<EventArgs> OnSerialAvailableListEvent;
        public event EventHandler<SerialEventArgs> OnWrongCOMAvailableEvent;
        public event EventHandler<SerialEventArgs> OnCorrectCOMAvailableEvent;
        public event EventHandler<SerialEventArgs> OnSerialAvailableEvent;
        public event EventHandler<EventArgs> OnNoConnectionAvailableEvent;
        public event EventHandler<EventArgs> OnErrorWhileGettingDescriptionEvent;

        public virtual void OnSerialConnected(string COM)
        {
            serialPort = new ReliableSerialPort(COM, 115200, Parity.None, 8, StopBits.One);
            serialPort.DataReceived += SerialPort_DataReceived;
            serialPort.Open();
            OnSerialConnectedEvent?.Invoke(this, new SerialEventArgs(COM));
        }

        public virtual void OnSerialAvailable(string COM)
        {
            OnSerialConnected(COM);
            OnSerialAvailableEvent?.Invoke(this, new SerialEventArgs(COM));
        }

        public virtual void OnAutoConnectionLaunched()
        {
            OnAutoConnectionLaunchedEvent?.Invoke(this, new EventArgs());
        }

        public virtual void OnNoConnectionAvailable()
        {
            OnNoConnectionAvailableEvent?.Invoke(this, new EventArgs());
        }

        public virtual void OnErrorWhileGettingDescription()
        {
            OnErrorWhileGettingDescriptionEvent?.Invoke(this, new EventArgs());
        }

        public virtual void OnWrongCOMAvailable(string COM)
        {
            OnWrongCOMAvailableEvent?.Invoke(this, new SerialEventArgs(COM));
        }

        public virtual void OnCorrectCOMAvailable(string COM)
        {
            OnCorrectCOMAvailableEvent?.Invoke(this, new SerialEventArgs(COM));
        }

        public virtual void OnNewSerialAttempts(uint attempts)
        {
            OnNewSerialAttemptsEvent?.Invoke(this, new AttemptsEventArgs(attempts));
        }

        public virtual void OnSerialAvailableList()
        {
            OnSerialAvailableListEvent?.Invoke(this, new EventArgs());
        }

        public void SerialPort_DataReceived(object sender, DataReceivedArgs e)
        {
            for (int i = 0; i < e.Data.Length; i++)
            {
                msgDecoder.ByteReceived(e.Data[i]);
            }
        }
        #endregion
        #region Class Args
        public class AttemptsEventArgs : EventArgs
        {
            public uint attempts { get; set; }

            public AttemptsEventArgs(uint attempts_a)
            {
                attempts = attempts_a;
            }
        }

        public class SerialEventArgs : EventArgs
        {
            public string COM { get; set; }

            public SerialEventArgs(string COM_a)
            {
                COM = COM_a;
            }
        }
        #endregion
    }
}
