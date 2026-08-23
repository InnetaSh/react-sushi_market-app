using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public record CreateCategoryCommand(string TitleUa, string TitleEn, string ImgSrc, double? SortOrder) : IRequest<int>;
}