namespace SushiMarket.BLL.Exceptions
{
    public class UnauthorizedException(string message) : BaseException(message, 401);
}