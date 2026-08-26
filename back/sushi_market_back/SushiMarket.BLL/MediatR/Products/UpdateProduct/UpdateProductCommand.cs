using MediatR;
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
        string ImgSrc,
        double? SortOrder,
        int CategoryId
    ) : IRequest<Unit>, IHasId, IHasCategoryId;
}