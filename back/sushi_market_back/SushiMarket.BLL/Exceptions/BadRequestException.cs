namespace SushiMarket.BLL.Exceptions
{
    public class BadRequestException(string message) : BaseException(message, 400);
}