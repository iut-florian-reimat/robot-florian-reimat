﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using ExtendedSerialPort;
using System.Windows.Threading;
using MouseKeyboardActivityMonitor.WinApi;
using MouseKeyboardActivityMonitor;


namespace RobotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private readonly KeyboardHookListener m_KeyboardHookManager;
        string emissionFormat = "Ascii";
        public bool autoControlActivated = true;
        DispatcherTimer timerAffichage;
        Queue<byte> byteListReceived = new Queue<byte>();
        Robot robot = new Robot();

        Paragraph receptionPara;
        private byte [] input_keyboard = new byte[5]{ 0,0,0,0,0 };
        public MainWindow()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("[MAIN] UI Start.");
            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timerAffichage.Tick += TimerAffichage_Tick;
            timerAffichage.Start();

            m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());
            m_KeyboardHookManager.Enabled = true;
            m_KeyboardHookManager.KeyDown += HookManager_KeyDown;
            m_KeyboardHookManager.KeyUp += HookManager_KeyUp;

        }


        private void TimerAffichage_Tick(object sender, EventArgs e)
        {
            if (robot.actualState != textState.Text)
            {
                textState.Text = robot.actualState;
            }
            if (robot.decodedText != textboxText.Text)
            {
                textboxText.Text = robot.decodedText;
            }
            if (robot.IR1.ToString() != textBoxIR1.Text)
            {
                textBoxIR1.Text = robot.IR1.ToString() + " cm";
            }
            if (robot.IR2.ToString() != textBoxIR2.Text)
            {
                textBoxIR2.Text = robot.IR2.ToString() + " cm";
            }
            if (robot.IR3.ToString() != textBoxIR3.Text)
            {
                textBoxIR3.Text = robot.IR3.ToString() + " cm";
            }
            if (robot.Motor1.ToString() != textBoxMotor1.Text)
            {
                textBoxMotor1.Text = robot.Motor1.ToString() + "%";
            }
            if (robot.Motor2.ToString() != textBoxMotor2.Text)
            {
                textBoxMotor2.Text = robot.Motor2.ToString() + "%";
            }
            while (byteListReceived.Count != 0)
            {
                byte byteDequeue = byteListReceived.Dequeue();
                robot.DecodeMessage(byteDequeue);
                string msg_byte = "0x" + byteDequeue.ToString("X2") + " ";
                Run run = new Run(msg_byte);


                switch (robot.rcvState)
                {
                    case Robot.StateReception.Waiting:
                        if (robot.rcvBefore == Robot.StateReception.CheckSum)
                        {
                            if (robot.msgIsWrong)
                            {
                                run.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            else
                            {
                                run.Foreground = new SolidColorBrush(Colors.Green);
                            }
                        }
                        else
                        {
                            run.Foreground = new SolidColorBrush(Colors.Gray);
                        }

                        break;
                    case Robot.StateReception.FunctionMSB:
                        run.Foreground = new SolidColorBrush(Colors.Purple);
                        break;
                    case Robot.StateReception.FunctionLSB:
                        run.Foreground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case Robot.StateReception.PayloadLengthMSB:
                        run.Foreground = new SolidColorBrush(Colors.Yellow);
                        break;
                    case Robot.StateReception.PayloadLengthLSB:
                        run.Foreground = new SolidColorBrush(Colors.LightBlue);
                        break;

                    case Robot.StateReception.Payload:
                        if (robot.rcvBefore == Robot.StateReception.PayloadLengthLSB)
                        {
                            run.Foreground = new SolidColorBrush(Colors.LightBlue);
                        }
                        else
                        {
                            run.Foreground = new SolidColorBrush(Colors.White);
                        }
                        break;
                    case Robot.StateReception.CheckSum:
                        run.Foreground = new SolidColorBrush(Colors.White);
                        break;
                }
                if (receptionPara == null)
                {
                    receptionPara = new Paragraph();
                }
                //receptionPara.Inlines.Add(run);
                //textBoxReception.Document.Blocks.Clear();
                //textBoxReception.Document.Blocks.Add(receptionPara);
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

        private void TextBoxEmission_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
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
            if (robot.serialPort != null)
            {

                SendMessage(CleanedMessage);
                run = new Run();
            }
            else
            {
                run = new Run(CleanedMessage);
                run.Background = new SolidColorBrush(Colors.Red);
            }
            Paragraph para = new Paragraph(run);

            textBoxEmission.Document.Blocks.Clear();
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
                robot.serialPort.Write(abc, 0, abc.Length);
            }
            else
            {
                robot.serialPort.Write(msg);
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
            Random random = new System.Random();
            byte[] array = Encoding.ASCII.GetBytes("Bonjour");
            robot.UartEncodeAndSendMessage(128, array.Length, array);
            array = new byte[3];
            random.NextBytes(array);
            robot.UartEncodeAndSendMessage(48, array.Length, array);
            array = new byte[2];
            random.NextBytes(array);
            robot.UartEncodeAndSendMessage(64, array.Length, array);
            array[0] = 0x01;
            array[1] = (byte)random.Next(2);
            robot.UartEncodeAndSendMessage(32, array.Length, array);
            array[0] = 0x02;
            array[1] = (byte)random.Next(2);
            robot.UartEncodeAndSendMessage(32, array.Length, array);

            array[0] = 0x03;
            array[1] = (byte)random.Next(2);
            robot.UartEncodeAndSendMessage(32, array.Length, array);
        }

        private string[] ListAllSerialOpen()
        {
            return SerialPort.GetPortNames();
        }

        private void comboSerial_Opening(object sender, RoutedEventArgs e)
        {
            string[] serialList = ListAllSerialOpen();

            for (int i = 0; i < serialList.Length; i++)
            {
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
            if (select != "Null" && comboSerial.SelectedItem != null)
            {
                if (robot.serialPort != null)
                {
                    // Can't close the serial
                    // serialPort.Close();
                }
                else
                {
                    robot.serialPort = new ReliableSerialPort(select, 115200, Parity.None, 8, StopBits.One);
                    robot.serialPort.DataReceived += SerialPort_DataReceived;
                    robot.serialPort.Open();
                }
            }
        }

        public void SendText(string msg)
        {
            textboxText.Text += msg;
        }

        

        private void UpdateManualCommand()
        {
            if (autoControlActivated == false)
            {
                int Keys =(int) (input_keyboard[0] << 4) + (int) (input_keyboard[1] << 3) + (int) (input_keyboard[2] << 2) + (int) (input_keyboard[3] << 1) + (int) (input_keyboard[4] << 0);
                Console.Write("[");
                int i;
                for (i=0; i<4; i++)
                {
                    Console.Write(input_keyboard[i]);
                }
                Console.WriteLine("]:" + Keys);
                switch (Keys)
                {
                    case 0:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_ARRET });
                        break;
                    case 1:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_TOURNE_SUR_PLACE_DROITE });
                        break;
                    case 2:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_TOURNE_SUR_PLACE_GAUCHE });
                        break;
                    case 4:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_RECULE });
                        break;
                    case 8:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_AVANCE });
                        break;
                    case 9:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_TOURNE_GAUCHE });
                        break;
                    case 10:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_TOURNE_DROITE});
                        break;
                    case 24:
                        robot.UartEncodeAndSendMessage(0x0051, 1, new byte[] {
                            (byte) Robot.StateRobot.STATE_FAST});
                        break;

                }
            }
        }

        private void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // UP | DOWN | LEFT | RIGHT

            switch (e.KeyCode)
            {
                case Keys.Space:
                    input_keyboard[0] = 1;
                    break;
                case Keys.Up:
                    input_keyboard[1] = 1;
                    break;
                case Keys.Down:
                    input_keyboard[2] = 1;
                    break;
                case Keys.Left:
                    input_keyboard[3] = 1;
                    break;
                case Keys.Right:
                    input_keyboard[4] = 1;
                    break;
            }
            UpdateManualCommand();
        }

        private void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    input_keyboard[0] = 0;
                    break;
                case Keys.Up:
                    input_keyboard[1] = 0;
                    break;
                case Keys.Down:
                    input_keyboard[2] = 0;
                    break;
                case Keys.Left:
                    input_keyboard[3] = 0;
                    break;
                case Keys.Right:
                    input_keyboard[4] = 0;
                    break;
            }
            UpdateManualCommand();
        }
        private void checkManual_Checked(object sender, RoutedEventArgs e)
        {
            autoControlActivated = !(bool)checkManual.IsChecked;
            robot.UartEncodeAndSendMessage(0x0052, 1, new byte[1] { autoControlActivated?(byte) 1:(byte) 0});
        }
    }
}
