namespace URIS_TD.Models.Others
{
    public class Logger
    {
        public LogLevel LogLevel { get; set; }
        public string ServiceName { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public Exception Exc { get; set; }
    }
}
