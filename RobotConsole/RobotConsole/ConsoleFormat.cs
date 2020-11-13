using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class ConsoleFormat
    {
        private static long hex_received_index = 0;
        private static long hex_sender_index = 0;

        #region General Method
        static public void ConsoleTitleFormat(string title, bool isCorrect)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
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
        #endregion
        #region Serial Init
        static public void PrintMessageDecoderCreated(object sender, EventArgs e)
        {
            ConsoleInformationFormat("DECODER", "Message Decoder is launched", true);
        }

        static public void PrintMessageEncoderCreated(object sender, EventArgs e)
        {
            ConsoleInformationFormat("ENCODER", "Message Encoder is launched", true);
        }

        static public void PrintMessageProcessorCreated(object sender, EventArgs e)
        {
            ConsoleInformationFormat("PROCESSOR", "Message Processor is launched", true);
        }

        static public void PrintMessageGeneratorCreated(object sender, EventArgs e)
        {
            ConsoleInformationFormat("GENERATOR", "Message Generator is launched", true);
        }
        #endregion
        #region Hex Decoder
        static public void PrintUnknowByte(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintSOF(object sender, MsgDecoder.DecodeByteArgs e)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.Write(hex_received_index++ + ": ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("0x" + e.b.ToString("X")+ " ");
            Console.ResetColor();
        }
        static public void PrintFunctionMSB(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintFunctionLSB(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintLenghtMSB(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintLenghtLSB(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintPayloadByte(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
            
        }
        static public void PrintCorrectChecksum(object sender, Protocol.MessageByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0x" + e.checksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintWrongChecksum(object sender, Protocol.MessageByteArgs e)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0x" + e.checksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        #endregion
        #region Hex Decoder Error
        static public void PrintOverLenghtWarning(object sender, EventArgs e)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n /!\\ WARNING A MESSAGE HAS EXCEED THE MAX LENGHT /!\\");
            Console.ResetColor();
        }

        static public void PrintWrongFonctionLenghtReceived(object sender, EventArgs e)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n /!\\ WARNING A FUNCTION HAS WRONG LENGHT /!\\");
            Console.ResetColor();
        }

        static public void PrintUnknowFunctionReceived(object sender, EventArgs e)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n /!\\ WARNING AN UNKNOW FUNCTION HAD BEEN RECEIVED /!\\");
            Console.ResetColor();
        }

        static public void PrintWrongMessage(object sender, Protocol.MessageByteArgs e)
        {
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n /!\\ WARNING AN MESSAGED HAD BEEN CORRUPTED /!\\"); 
            Console.ResetColor();
        }
        #endregion
        #region Hex Encoder
        static public void PrintSendMsg(object sender, Protocol.MessageByteArgs e)
        {
            Console.ResetColor();
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.Write(hex_sender_index++ + ": ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write("0x" + e.SOF.ToString("X2") + " ");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write("0x" + e.functionMsb.ToString("X2") + " 0x" + e.functionLsb.ToString("X2") + " ");
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("0x" + e.lenghtMsb.ToString("X2") + " 0x" + e.lenghtLsb.ToString("X2") + " ");
            Console.BackgroundColor = ConsoleColor.White;
            foreach (byte x in e.msgPayload)
            {
                Console.Write("0x" + x.ToString("X2") + " ");
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("0x" + e.checksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        #endregion
        #region Hex Encoder Error
        static public void PrintOnSerialDisconnectedError(object sender, EventArgs e)
        {
            Console.ResetColor();
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("/!\\ WARNING MESSAGE CAN'T BE SENT BECAUSE SERIAL IS CLOSED /!\\");
            Console.ResetColor();
        }

        static public void PrintUnknowFunctionSent(object sender, EventArgs e)
        {
            Console.ResetColor();
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("/!\\ WARNING AN UNKNOW FUNCTION HAD BEEN SENT /!\\");
            Console.ResetColor();
        }

        static  public void PrintWrongFunctionLenghtSent(object sender, EventArgs e)
        {
            Console.ResetColor();
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("/!\\ WARNING A FUNCTION WITH WRONG LENGHT HAS TRIED TO BE SENT /!\\");
            Console.ResetColor();
        }
        #endregion
        #region Hex Processor
        static public void PrintIRMessageReceived(object sender, Protocol.IRMessageArgs e)
        {
            Console.ResetColor();
            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
            }
            ConsoleInformationFormat("IR", "IR Received:",  true);
            ConsoleListFormat("Left   : " + e.left_IR   + "cm", ConsoleColor.White);
            ConsoleListFormat("Center : " + e.center_IR + "cm", ConsoleColor.White);
            ConsoleListFormat("Right  : " + e.right_IR  + "cm", ConsoleColor.White);
        }

        static public void PrintStateMessageReceived(object sender, Protocol.StateMessageArgs e)
        {
            ConsoleInformationFormat("STATE", "Actual State: " + e.state + " - " +  e.time, true);
        }
        #endregion

        
    }
}
