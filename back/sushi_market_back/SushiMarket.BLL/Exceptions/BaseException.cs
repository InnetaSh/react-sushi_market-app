namespace SushiMarket.BLL.Exceptions
{
    public class BaseException(string message, int statusCode = 500) : Exception(message)
    {
        public int StatusCode { get; } = statusCode;
    }
}