using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TogglerAPI.Utilities
{
    public static class LoggerFile
    {
        private static readonly string LogFolderName = "LOGS";
        private static readonly string LogFileName = "LogFile.txt";
        private static readonly object LogFileLockObject = new object();
        private static IConfiguration Configuration => Configuration;

        public static void LogFile(string message)
        {
            if (GetLogFileStatus())
            {
                lock (LogFileLockObject)
                {
                    string date = DateTime.UtcNow.ToString("yyyMMdd");

                    string logFolderPath = Path.Combine(Environment.CurrentDirectory, LogFolderName);

                    if (!Directory.Exists(logFolderPath))
                    {
                        Directory.CreateDirectory(logFolderPath);
                    }

                    string fileName = Path.Combine(logFolderPath, $"{date}_{LogFileName}");

                    using (StreamWriter streamWriter = File.AppendText(fileName))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Flush();
                    }
                }
            }
        }

        private static bool GetLogFileStatus()
        {
            string logFile = Configuration.GetSection("LoggerFile").Value;

            if (logFile.Equals("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
