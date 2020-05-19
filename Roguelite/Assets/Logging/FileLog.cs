using System;

namespace Logging
{
    internal sealed class FileLog : ILog
    {
        private readonly string m_Path;

        public FileLog(string filePath)
        {
#if UNITY_EDITOR
            var logPath = @"C:\Users\leeok\Desktop\logs";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            if (!File.Exists(filePath))
            {
                File.CreateText(filePath).Dispose();
            }

#endif
            m_Path = filePath;
        }

        public void Log(string message, LogLevel level)
        {
#if UNITY_EDITOR
            using (var stream = File.AppendText(m_Path))
            {
                stream.Write($"{GetPrefixForLevel(level)} {message}\n");
            }
#endif
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