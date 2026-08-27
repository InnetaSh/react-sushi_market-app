namespace SushiMarket.BLL.Exceptions
{
    public class NotFoundException(string message) : BaseException(message, 404);
   
}