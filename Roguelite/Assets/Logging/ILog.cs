namespace Logging
{
    internal interface ILog
    {
        void Log(string message, LogLevel level);
    }
}
