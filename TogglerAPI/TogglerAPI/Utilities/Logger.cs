using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TogglerAPI.Interfaces;

namespace TogglerAPI.Utilities
{
    public class Logger : ILogger
    {
        private readonly string LogFolderName = "LOGS";
        private readonly string LogFileName = "LogFile.txt";
        private readonly object LogFileLockObject = new object();
        private IConfiguration Configuration => Configuration;
        public string LoggerFile;

        public Logger(string loggerFile)
        {
            LoggerFile = loggerFile;
        }

        public void LogFile(string message)
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
                        streamWriter.WriteLine($"{DateTime.UtcNow}: {message}");
                        streamWriter.Flush();
                    }
                }
            }
        }

        private bool GetLogFileStatus()
        {
            if (LoggerFile.Equals("true"))
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
