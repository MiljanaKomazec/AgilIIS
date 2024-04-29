namespace Sprint.Models
{
    public class LoggerModel
    {
        public LogLevel LogLevel { get; set; }
        public string ServiceName { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public Exception Exc { get; set; }
    }
}
