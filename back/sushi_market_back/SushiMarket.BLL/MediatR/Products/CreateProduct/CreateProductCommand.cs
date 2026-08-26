using MediatR;
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
        string ImgSrc,
        double? SortOrder,
        int CategoryId
    ) : IRequest<int>, IHasCategoryId;
}