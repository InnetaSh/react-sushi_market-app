using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(int Id, string Title, string ImgSrc, double? SortOrder) : IRequest<Unit>;
}