using MediatR;

namespace SushiMarket.BLL.MediatR.Products.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest<Unit>;
}