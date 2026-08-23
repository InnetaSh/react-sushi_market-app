using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public record CreateCategoryCommand(string Title, string ImgSrc, double? SortOrder) : IRequest<int>;
}