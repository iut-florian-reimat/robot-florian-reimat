using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtendedSerialPort;
using System.Management;
using System.IO.Ports;
using System.Security.Cryptography.X509Certificates;

namespace RobotConsole
{
    class Program
    {

        static Serial serial;
        private static bool serial_viewer = true;
        private static bool hex_viewer = false;
        private static bool hex_error_viewer = true;
        private static bool hex_sender = true;
        private static bool hex_error_sender = true;
        private static bool function_received = true;

        static void Main(string[] args)
        {
            ConsoleFormat.ConsoleInformationFormat("MAIN", "Begin Booting Sequence", true);

            serial = new Serial();

            Serial.msgDecoder.OnMessageDecoderCreatedEvent += ConsoleFormat.PrintMessageDecoderCreated;
            Serial.msgEncoder.OnMessageEncoderCreatedEvent += ConsoleFormat.PrintMessageEncoderCreated;
            Serial.msgProcessor.OnMessageProcessorCreatedEvent += ConsoleFormat.PrintMessageProcessorCreated;
            Serial.msgGenerator.OnMessageGeneratorCreatedEvent += ConsoleFormat.PrintMessageGeneratorCreated;

            #region Event
            #region Serial
            if (serial_viewer)
            {
                serial.OnAutoConnectionLaunchedEvent += ConsoleFormat.PrintAutoConnectionStarted;
                serial.OnSerialAvailableListEvent += ConsoleFormat.PrintSerialListAvailable;
                serial.OnCorrectCOMAvailableEvent += ConsoleFormat.PrintCorrectAvailableCOM;
                serial.OnWrongCOMAvailableEvent += ConsoleFormat.PrintWrongAvailableCOM;
                serial.OnNoConnectionAvailableEvent += ConsoleFormat.PrintNoConnectionAvailable;
                serial.OnErrorWhileGettingDescriptionEvent += ConsoleFormat.PrintErrorWhileGettingDescription;
                serial.OnSerialAvailableEvent += ConsoleFormat.PrintCOMAvailable;
                serial.OnSerialConnectedEvent += ConsoleFormat.PrintConnectionEnabled;
            }
            #endregion
            #region Hex Viewer
            if (hex_viewer)
            {
                Serial.msgDecoder.OnUnknowByteEvent += ConsoleFormat.PrintUnknowByte;
                Serial.msgDecoder.OnSOFByteReceivedEvent += ConsoleFormat.PrintSOF;
                Serial.msgDecoder.OnFunctionMSBByteReceivedEvent += ConsoleFormat.PrintFunctionMSB;
                Serial.msgDecoder.OnFunctionLSBByteReceivedEvent += ConsoleFormat.PrintFunctionLSB;
                Serial.msgDecoder.OnPayloadLenghtMSBByteReceivedEvent += ConsoleFormat.PrintLenghtMSB;
                Serial.msgDecoder.OnPayloadLenghtLSBByteReceivedEvent += ConsoleFormat.PrintLenghtLSB;
                Serial.msgDecoder.OnPayloadByteReceivedEvent += ConsoleFormat.PrintPayloadByte;
                Serial.msgDecoder.OnCorrectChecksumEvent += ConsoleFormat.PrintCorrectChecksum;
                Serial.msgDecoder.OnWrongChecksumEvent += ConsoleFormat.PrintWrongChecksum;
            }
            #endregion
            #region Hex Viewer Error
            if (hex_error_viewer)
            {
                Serial.msgDecoder.OnOverLenghtMessageEvent += ConsoleFormat.PrintOverLenghtWarning;
                Serial.msgDecoder.OnUnknowFunctionEvent += ConsoleFormat.PrintUnknowFunctionReceived;
                Serial.msgDecoder.OnWrongLenghtFunctionEvent += ConsoleFormat.PrintWrongFonctionLenghtReceived;
                Serial.msgDecoder.OnWrongChecksumEvent += ConsoleFormat.PrintWrongMessage;
            }
            #endregion
            #region Hex Sender
            if (hex_sender)
            {
                Serial.msgEncoder.OnSendMessageEvent += ConsoleFormat.PrintSendMsg;
            }
            #endregion
            #region Hex Sender Error
            if (hex_error_sender)
            {
                Serial.msgEncoder.OnSerialDisconnectedEvent += ConsoleFormat.PrintOnSerialDisconnectedError;
                Serial.msgEncoder.OnUnknownFunctionSentEvent += ConsoleFormat.PrintUnknowFunctionSent;
                Serial.msgEncoder.OnWrongPayloadSentEvent += ConsoleFormat.PrintWrongFunctionLenghtSent;
            }
            #endregion
            #region Function Processor
            if (function_received)
            {
                Serial.msgProcessor.OnIRMessageReceivedEvent += ConsoleFormat.PrintIRMessageReceived;
                Serial.msgProcessor.OnStateMessageReceivedEvent += ConsoleFormat.PrintStateMessageReceived;
                Serial.msgProcessor.OnPositionMessageReceivedEvent += ConsoleFormat.PrintPositionMessageReceived;
                Serial.msgProcessor.OnTextMessageReceivedEvent += ConsoleFormat.PrintTextMessageReceived;
            }
            #endregion
            #endregion

            bool isSerialConnected = serial.AutoConnectSerial();
            Serial.msgDecoder.OnCorrectChecksumEvent += Serial.msgProcessor.MessageProcessor; // Obligatory


            ConsoleFormat.ConsoleInformationFormat("MAIN", "End  Booting Sequence", true);
            Serial.msgGenerator.GenerateMessageSetLed(1, true);
            Serial.msgGenerator.GenerateMessageSetLed(3, true);
            Console.ReadKey();

        }
    }
}
