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

        static public void ConsoleSOFFormat(object sender, MsgDecoder.DecodeByteArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(e.b);
        }
    }
}
