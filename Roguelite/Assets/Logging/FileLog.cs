using System;
using System.IO;

namespace Logging
{
    internal sealed class FileLog : ILog
    {
        private readonly string m_Path;

        public FileLog(string filePath)
        {
            var logPath = @"C:\Users\leeok\Desktop\logs";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            if (!File.Exists(filePath))
            {
                File.CreateText(filePath).Dispose();
            }

            m_Path = filePath;
        }

        public void Log(string message, LogLevel level)
        {
            using (var stream = File.AppendText(m_Path))
            {
                stream.Write($"{GetPrefixForLevel(level)} {message}\n");
            }
        }

        private string GetPrefixForLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "[DEBUG]";
                case LogLevel.Info:
                    return "[INFO]";
                case LogLevel.Warning:
                    return "[WARN]";
                case LogLevel.Error:
                    return "[ERROR]";
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}