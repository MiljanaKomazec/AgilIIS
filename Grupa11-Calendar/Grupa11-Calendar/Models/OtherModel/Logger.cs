namespace Grupa11_Calendar.Models.OtherModel
{
    public class Logger
    {
        //debug, info, warning, error
        public LogLevel LogLevel { get; set; }
        public string ServiceName { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public Exception Exc { get; set; }
    }
}
