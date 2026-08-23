using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.ReorderCategory
{
    public record ReorderCategoryCommand(int CategoryId, double NewSortOrder) : IRequest<Unit>;
}