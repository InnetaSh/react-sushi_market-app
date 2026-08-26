using MediatR;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(
        int Id,
        string? TitleUa,
        string? TitleEn,
        double? SortOrder,
        string? ImgSrc
    ) : IRequest<Unit>, IHasId;
}