using MediatR;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.ReorderProduct
{
    public record ReorderProductCommand(int ProductId, double NewSortOrder) : IRequest<Unit>, IHasId
    {
        public int Id => ProductId;
    }
}