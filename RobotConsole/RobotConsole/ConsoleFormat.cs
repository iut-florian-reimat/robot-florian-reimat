using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class ConsoleFormat
    {
        private static long hex_index = 0;
        static public void ConsoleTitleFormat(string title, bool isCorrect)
        {
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
        static public void PrintUnknowByte(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("0x" + e.b.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintSOF(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ResetColor();
            Console.Write(hex_index++ + ": ");
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
        static public void PrintCorrectChecksum(object sender, MsgDecoder.DecodeMsgArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("0x" + e.msgChecksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintWrongChecksum(object sender, MsgDecoder.DecodeMsgArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0x" + e.msgChecksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        
        static public void PrintOverLenghtWarning(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n /!\\ WARNING A MESSAGE HAS EXCEED THE MAX LENGHT /!\\");
            Console.ResetColor();
        }

        static public void PrintWrongFonctionLenght(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n /!\\ WARNING A FUNCTION HAS WRONG LENGHT /!\\");
            Console.ResetColor();
        }

        static public void PrintUnknowFunction(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n /!\\ WARNING AN UNKNOW FUNCTION HAD BEEN SENT /!\\");
            Console.ResetColor();
        }

        static public void PrintWrongMessage(object sender, MsgDecoder.DecodeMsgArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n /!\\ WARNING AN MESSAGED HAD BEEN CORRUPTED /!\\"); 
            Console.ResetColor();
        }
    }
}
