namespace LogUltra.Core.Abstraction.Services
{
    public interface  ILogServiceBaseResponse
    {
        bool Success { get; set; }
        string Message { get; set; }
    }
}
