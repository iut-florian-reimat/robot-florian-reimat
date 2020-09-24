using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using ExtendedSerialPort;
using System.Windows.Threading;


namespace RobotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        string receivedText, emissionFormat = "Ascii";
        ReliableSerialPort serialPort;
        DispatcherTimer timerAffichage;
        Queue<byte> byteListReceived = new Queue<byte>();
        bool msgIsWrong = false;
        Paragraph receptionPara;
        public MainWindow()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("[MAIN] UI Start.");
            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timerAffichage.Tick += TimerAffichage_Tick;
            timerAffichage.Start();
            
            
        }
        
        

        private void TimerAffichage_Tick(object sender, EventArgs e)
        {
            if (byteListReceived.Count != 0)
            {
                byte byteDequeue = byteListReceived.Dequeue();
                DecodeMessage(byteDequeue);
                string msg_byte = "0x" + byteDequeue.ToString("X2") + " ";
                Run run = new Run(msg_byte);
                
                
                switch (rcvState)
                {
                    case StateReception.Waiting:
                        if (rcvBefore == StateReception.CheckSum)
                        {
                            if (msgIsWrong)
                            {
                                run.Foreground = new SolidColorBrush(Colors.Red);
                            } else {
                                run.Foreground = new SolidColorBrush(Colors.Green);
                            }
                        } else {
                            run.Foreground = new SolidColorBrush(Colors.Gray);
                        }
                        
                        break;
                    case StateReception.FunctionMSB:
                        run.Foreground = new SolidColorBrush(Colors.Purple);
                        break;
                    case StateReception.FunctionLSB:
                        run.Foreground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case StateReception.PayloadLengthMSB:
                        run.Foreground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case StateReception.PayloadLengthLSB:
                        run.Foreground = new SolidColorBrush(Colors.LightBlue);
                        break;

                    case StateReception.Payload:
                        if (rcvBefore == StateReception.PayloadLengthLSB)
                        {
                            run.Foreground = new SolidColorBrush(Colors.LightBlue);
                        } else {
                            run.Foreground = new SolidColorBrush(Colors.White);
                        }
                        break;
                    case StateReception.CheckSum:
                        run.Foreground = new SolidColorBrush(Colors.White);
                        break;
                }
                if (receptionPara == null)
                {
                    receptionPara = new Paragraph();
                }
                receptionPara.Inlines.Add(run);
                
                textBoxReception.Document.Blocks.Clear();
                textBoxReception.Document.Blocks.Add(receptionPara);
            }
        }

        public void SerialPort_DataReceived(object sender, DataReceivedArgs e)
        {
            for (int i = 0; i < e.Data.Length; i++)
            {
                byteListReceived.Enqueue(e.Data[i]);
            }
        }


        private void ButtonEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            getMessage();
        }

        private void TextBoxEmission_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                getMessage();
            }
        }


        private void getMessage()
        {
            Block block = textBoxEmission.Document.Blocks.FirstBlock;
            string CleanedMessage = new TextRange(block.ContentStart, block.ContentEnd).Text.Replace("\n", "").Replace("\r", ""); ;
            textBoxEmission.Document.Blocks.Clear();
            Run run;
            if (serialPort != null)
            {

                SendMessage(CleanedMessage);
                run = new Run();
            } else {
                run = new Run(CleanedMessage);
                run.Background = new SolidColorBrush(Colors.Red);
            }
            Paragraph para = new Paragraph(run);
            textBoxEmission.Document.Blocks.Add(para);
        }

        private void SendMessage(string msg)
        {
            if (emissionFormat == "Hex")
            {
                string[] hex_msg = msg.Split(' ');
                byte[] abc = new byte[hex_msg.Length];

                for (int i = 0; i <= hex_msg.Length - 1; i = i + 1)
                {
                    abc[i] = Convert.ToByte(hex_msg[i], 16);
                }
                serialPort.Write(abc, 0, abc.Length);
            } else
            {
                serialPort.Write(msg);
            }
            
        }

        private void Clear()
        {
            receptionPara = new Paragraph();
            textBoxReception.Document.Blocks.Clear();
            textBoxReception.Document.Blocks.Add(new Paragraph(new Run()));
        }
        
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            byte[] array = Encoding.ASCII.GetBytes("Bonjour");
            UartEncodeAndSendMessage(128, array.Length, array);
        }

        // 0xFE | 0x00 0x?? | 0x?? 0x?? | ??? | 0x??
        byte CalculateChecksum(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);

            byte checksum = msg[0];
            for (int i = 1; i < msg.Length; i++)
            {
                checksum ^= msg[i];
            }
            System.Diagnostics.Debug.WriteLine("[CHECKSUM] " + msg + " Result : " + checksum);
            return checksum;
        }

        void UartEncodeAndSendMessage(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);
            byte[] checksum = new byte[] { CalculateChecksum(msgFunction, msgPayloadLength, msgPayload) };
            msg = Combine(msg, checksum);
            if (serialPort != null)
            serialPort.Write(msg, 0, msg.Length);
        }

        byte[] EncodeWithoutChecksum(int msgFunction, int msgPayloadLength, byte[] msgPayload)
        {
            // Convert Function to byte
            byte LbyteFunction = (byte)(msgFunction >> 0);
            byte HbyteFunction = (byte)(msgFunction >> 8);

            byte LbytePayloadsLength = (byte)(msgPayloadLength >> 0);
            byte HbytePayloadsLength = (byte)(msgPayloadLength >> 8);

            // Append all bytes
            byte[] msg = new byte[] { 0xFE, HbyteFunction, LbyteFunction, HbytePayloadsLength, LbytePayloadsLength };
            msg = Combine(msg, msgPayload);
            return msg;
        }
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }

        public enum StateReception
        {
            Waiting,
            FunctionMSB,
            FunctionLSB,
            PayloadLengthMSB,
            PayloadLengthLSB,
            Payload,
            CheckSum
        }
        StateReception rcvState = StateReception.Waiting;
        StateReception rcvBefore = StateReception.Waiting;
        int msgDecodedFunction = 0;
        int msgDecodedPayloadLength = 0;
        byte[] msgDecodedPayload;
        int msgDecodedPayloadIndex = 0;
        private void DecodeMessage(byte c){
            rcvBefore = rcvState;
            switch (rcvState){
                case StateReception.Waiting:
                    if (c == 0xFE)
                    {
                        // Message begin
                        rcvState = StateReception.FunctionMSB;
                    }
                    break;
                case StateReception.FunctionMSB:
                        msgDecodedFunction = (ushort)(c << 8);
                        rcvState = StateReception.FunctionLSB;
                    break;
                case StateReception.FunctionLSB:
                    msgDecodedFunction += (ushort)(c << 0);
                    rcvState = StateReception.PayloadLengthMSB;
                    break;
                case StateReception.PayloadLengthMSB:
                    msgDecodedPayloadLength = (ushort)(c << 8);
                    rcvState = StateReception.PayloadLengthLSB;
                    break;
                case StateReception.PayloadLengthLSB:
                    msgDecodedPayloadLength += (ushort)(c << 0);
                    if (msgDecodedPayloadLength > 0)
                    {
                        msgDecodedPayload = new byte[msgDecodedPayloadLength];
                        rcvState = StateReception.Payload;
                    } else
                    {
                        rcvState = StateReception.CheckSum;
                    }
                    break;

                case StateReception.Payload:
                    msgDecodedPayload[msgDecodedPayloadIndex] = c;
                    msgDecodedPayloadIndex++;
                    if (msgDecodedPayloadIndex >= msgDecodedPayloadLength)
                    {
                        rcvState = StateReception.CheckSum;
                    }
                    break;
                case StateReception.CheckSum:
                    byte calculatedChecksum, receivedChecksum = c;
                    calculatedChecksum = CalculateChecksum(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload);
                    if (calculatedChecksum == receivedChecksum){
                        // Success, on a un message 
                        Console.WriteLine("Message is Correct");
                        msgIsWrong = false;
                    } else {
                        Console.WriteLine("Message has an error");
                        msgIsWrong = true;
                    }
                    rcvState = StateReception.Waiting;
                    msgDecodedFunction = 0;
                    msgDecodedPayloadLength = 0;
                    msgDecodedPayloadIndex = 0;
                    break;
                default:
                    rcvState = StateReception.Waiting;
                    break;
            }
        }

        private string[] ListAllSerialOpen()
        {
            return SerialPort.GetPortNames();
        }

        private void comboSerial_Opening(object sender, RoutedEventArgs e)
        {
            string[] serialList = ListAllSerialOpen();
            
            for (int i = 0; i < serialList.Length; i++) {
                if (!comboSerial.Items.Contains(serialList[i]))
                {
                    comboSerial.Items.Add(serialList[i]);
                }
            }
        }

        private void comboEmission_Changed(object sender, EventArgs e)
        {
            emissionFormat = comboEmission.Text;
        }

        private void buttonSerial_Click(object sender, RoutedEventArgs e)
        {
            string select = comboSerial.Text;
            if (select!= "Null" && comboSerial.SelectedItem != null)
            {
                if (serialPort != null)
                {
                    // Can't close the serial
                    //serialPort.Close();
                } else {
                    serialPort = new ReliableSerialPort(select, 115200, Parity.None, 8, StopBits.One);
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();
                }
            }
        }
    }
}