using MediatR;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(
        int Id,
        string TitleUa,
        string TitleEn,
        string ImgSrc,
        double? SortOrder
    ) : IRequest<Unit>;
}