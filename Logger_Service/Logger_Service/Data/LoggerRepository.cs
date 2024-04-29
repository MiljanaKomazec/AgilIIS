using NLog;
using System;
using System.IO;

namespace Logger_Service.Data
{
    public class LoggerRepository : ILoggerRepository
    {
        private static NLog.ILogger log = LogManager.GetCurrentClassLogger();

        public LoggerRepository()
        {

        }

        public void DebugLogger(string message)
        {
            log.Debug(message);
        }

        public void ErrorLogger(Exception exception, string message)
        {
            log.Error(exception, message);
        }

        public void InfoLogger(string message)
        {
            log.Info(message);
        }

        public void WarnLogger(string message)
        {
            log.Warn(message);
        }

    }
}
