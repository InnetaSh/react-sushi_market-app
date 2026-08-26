using MediatR;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest<Unit>, IHasId;
}