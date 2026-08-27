namespace SushiMarket.BLL.Exceptions
{
    public class ForbiddenException(string message) : BaseException(message, 403);
}