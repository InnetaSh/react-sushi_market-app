using MediatR;
using Microsoft.AspNetCore.Http;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(
        int Id,
        string? TitleUa,
        string? TitleEn,
        double? SortOrder,
        IFormFile? Image
    ) : IRequest<Unit>, IHasId;
}