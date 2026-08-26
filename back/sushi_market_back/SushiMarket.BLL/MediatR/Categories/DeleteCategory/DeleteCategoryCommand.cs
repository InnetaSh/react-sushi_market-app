using MediatR;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<Unit>, IHasId;
}