namespace CommentService.Helper
{
    public interface ILoggerService
    {
        Task<bool> Log(LogLevel level, string method, string message, Exception exc = null);
    }
}
