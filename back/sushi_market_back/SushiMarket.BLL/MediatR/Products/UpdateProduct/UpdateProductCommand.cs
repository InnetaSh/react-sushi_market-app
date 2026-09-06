using MediatR;
using Microsoft.AspNetCore.Http;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public record UpdateProductCommand(
        int Id,
        string TitleUa,
        string TitleEn,
        string DescriptionUa,
        string DescriptionEn,
        string WeightOrVolume,
        decimal Price,
        IFormFile? Image,
        double? SortOrder,
        int CategoryId
    ) : IRequest<Unit>, IHasId, IHasCategoryId;
}