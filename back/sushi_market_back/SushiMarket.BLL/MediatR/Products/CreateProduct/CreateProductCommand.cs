using MediatR;
using Microsoft.AspNetCore.Http;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.CreateProduct
{
    public record CreateProductCommand(
        string TitleUa,
        string TitleEn,
        string DescriptionUa,
        string DescriptionEn,
        string WeightOrVolume,
        decimal Price,
        IFormFile? Image,
        double? SortOrder,
        int CategoryId
    ) : IRequest<int>, IHasCategoryId;
}