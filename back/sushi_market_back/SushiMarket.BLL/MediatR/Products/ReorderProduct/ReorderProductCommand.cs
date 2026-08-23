using MediatR;

namespace SushiMarket.BLL.MediatR.Products.ReorderProduct
{
    public record ReorderProductCommand(int ProductId, double NewSortOrder) : IRequest<Unit>;
}