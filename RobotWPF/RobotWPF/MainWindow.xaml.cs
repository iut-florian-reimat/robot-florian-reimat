using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string receivedText;
        ReliableSerialPort serialPort1;
        DispatcherTimer timerAffichage;
        public MainWindow()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("[MAIN] UI Start");
            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timerAffichage.Tick += TimerAffichage_Tick;
            timerAffichage.Start();

            serialPort1 = new ReliableSerialPort("COM8",115200, Parity.None, 8, StopBits.One);
            serialPort1.DataReceived += SerialPort1_DataReceived;
            serialPort1.Open();
        }

        private void TimerAffichage_Tick(object sender, EventArgs e)
        {
            if (receivedText != "" && receivedText != null)
            {
                string CleanedMessage = receivedText.Replace("\n", "").Replace("\r", "");
                if (CleanedMessage != "")
                {
                    textBoxReception.Text += "Received: " + CleanedMessage + "\n";
                }
                receivedText = "";
            }
        }

        public void SerialPort1_DataReceived(object sender, DataReceivedArgs e)
        {
            receivedText += Encoding.UTF8.GetString(e.Data, 0,e.Data.Length);
        }


        private void ButtonEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string CleanedMessage = textBoxEmission.Text.Replace("\n", "").Replace("\r", "");
            serialPort1.WriteLine(CleanedMessage);
            textBoxEmission.Text = "";
        }

        private void Clear()
        {
            textBoxReception.Text = "";
        }
        private void TextBoxEmission_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            byte[] byteList = new byte[20];
            for (int i = 0; i<byteList.Length; i++) {
                byteList[i] = (byte)(2 * i);
            }
            //serialPort1.Write(byteList);
        }
    }
}
