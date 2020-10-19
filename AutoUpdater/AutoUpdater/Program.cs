using System;
using System.Diagnostics;

namespace AutoUpdater
{

    class Program
    {
        
        

        const string MS_BUILD_DIRECTORY = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe";
        const string ABSOLUTE_HOME_DIRECTORY = "C:\\Users\\TABLE 6\\Desktop\\robot-florian-reimat";
        const string RELATIVE_PROJECT_DIRECTORY = "RobotConsole\\RobotConsole.sln";
        const string RELATIVE_BIN_DIRECTORY = "RobotConsole\\RobotConsole\\bin\\Debug\\RobotConsole.exe";
        const string GITHUB_REPOSITORY = "http://github.com/iut-florian-reimat/robot-florian-reimat.git";

        static void Main(string[] args)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("git clone " + GITHUB_REPOSITORY + " \"" +  ABSOLUTE_HOME_DIRECTORY + "\"");
            cmd.StandardInput.WriteLine("\"" + MS_BUILD_DIRECTORY + "\" \"" + ABSOLUTE_HOME_DIRECTORY + "\\" + RELATIVE_PROJECT_DIRECTORY + "\"");
            cmd.StandardInput.Flush();
          
            cmd.StandardInput.Close();
            //cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.Close();
            Process.Start(ABSOLUTE_HOME_DIRECTORY + "\\" + RELATIVE_BIN_DIRECTORY).Close();
            
            Console.ReadKey();
        }
    }
}
