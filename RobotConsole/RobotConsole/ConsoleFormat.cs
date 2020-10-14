using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class ConsoleFormat
    {
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
            Console.Write("0x" + e.msgChecksum.ToString("X2") + " ");
            Console.ResetColor();
        }
        static public void PrintWrongChecksum(object sender, MsgDecoder.DecodeMsgArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("0x" + e.msgChecksum.ToString("X2") + " ");
            Console.ResetColor();
        }
    }
}
