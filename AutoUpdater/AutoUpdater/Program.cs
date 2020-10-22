using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace AutoUpdater
{

    class Program
    {
        // Only test with master
        static Process cmd, program;
        const string GITHUB_REPOSITORY = "http://github.com/iut-florian-reimat/robot-florian-reimat.git";
        const string BRANCH = "master";
        const string MS_BUILD_DIRECTORY = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe";
        const string NUGET_DIRECTORY = "C:\\Users\\Table 6\\Desktop\\nuget.exe";
        const string ABSOLUTE_HOME_DIRECTORY = "C:\\Users\\TABLE 6\\Desktop\\robot-florian-reimat";
        const string RELATIVE_PROJECT_DIRECTORY = "RobotConsole\\RobotConsole.sln";
        const string RELATIVE_BIN_DIRECTORY = "RobotConsole\\RobotConsole\\bin\\Debug\\RobotConsole.exe";
        

        static void Main(string[] args)
        {
            StartCMDProcess();
            ExecCMDCommand("git clone " + GITHUB_REPOSITORY + " \"" + ABSOLUTE_HOME_DIRECTORY + "\"");
            CompileProgram();
            CloseCMD();

            Timer timerAffichage = new Timer(RefreshGithub, null, 10000, 10000);
            LaunchProgram();
            Console.ReadKey();
            
        }

        private static void RefreshGithub(object state)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("AUTO-UPDATER");
            Console.ResetColor();
            Console.Write("] Try to Update");
            string isRepoUpToDateData = CommandOutput("git pull", ABSOLUTE_HOME_DIRECTORY);
            if (!isRepoUpToDateData.Contains("Already up to date."))
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("AUTO-UPDATER");
                Console.ResetColor();
                Console.Write("] Updating...");
                ReloadProgram();
            }
        }
        public static void StartCMDProcess()
        {
            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
        }

        public static void ExecCMDCommand(string command)
        {
            int timeout = 10000;
            if (cmd == null || cmd.StartInfo.FileName != "cmd.exe")
            {
                StartCMDProcess();
            }
            cmd.Start();
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Close();
            cmd.WaitForExit(timeout);
        }

        public static void CloseCMD()
        {
            cmd.Close();
            Thread.Sleep(5000);
        }

        private static void CompileProgram()
        {
            ExecCMDCommand("\"" + NUGET_DIRECTORY + "\" restore \"" + ABSOLUTE_HOME_DIRECTORY + "\\" + RELATIVE_PROJECT_DIRECTORY + "\"");
            ExecCMDCommand("\"" + MS_BUILD_DIRECTORY + "\" \"" + ABSOLUTE_HOME_DIRECTORY + "\\" + RELATIVE_PROJECT_DIRECTORY + "\"");
        }

        private static void LaunchProgram()
        {
            program = new Process();
            program.StartInfo.FileName = ABSOLUTE_HOME_DIRECTORY + "\\" + RELATIVE_BIN_DIRECTORY;
            program.Start();
        }

        private static void CloseProgram()
        {
            program.Kill();
        }

        private static void ReloadProgram()
        {
            CloseProgram();
            ExecCMDCommand("git pull");
            CompileProgram();
            LaunchProgram();
        }

        public static string CommandOutput(string command, string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardError = procStartInfo.RedirectStandardInput = procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                if (null != workingDirectory)
                {
                    procStartInfo.WorkingDirectory = workingDirectory;
                }

                Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                StringBuilder sb = new StringBuilder();
                proc.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };
                proc.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
                return sb.ToString();
            }
            catch (Exception objException)
            {
                return $"Error in command: {command}, {objException.Message}";
            }
        }
    }
}
