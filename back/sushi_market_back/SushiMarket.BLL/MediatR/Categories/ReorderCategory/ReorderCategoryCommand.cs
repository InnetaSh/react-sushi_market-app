using MediatR;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.ReorderCategory
{
    public record ReorderCategoryCommand(int CategoryId, double NewSortOrder) : IRequest<Unit>, IHasCategoryId;
}