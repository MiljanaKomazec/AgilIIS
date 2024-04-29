namespace Logger_Service.Data
{
    public interface ILoggerRepository
    {
        void InfoLogger (string message);
        void DebugLogger (string message);
        void WarnLogger (string message);
        void ErrorLogger(Exception exception, string message);
    }
}
