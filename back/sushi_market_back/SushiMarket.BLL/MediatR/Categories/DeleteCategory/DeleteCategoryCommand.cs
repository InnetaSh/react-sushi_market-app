using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(int Id) : IRequest<Unit>;
}