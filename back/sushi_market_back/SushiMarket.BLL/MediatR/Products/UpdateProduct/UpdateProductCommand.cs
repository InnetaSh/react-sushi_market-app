using MediatR;

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
    ) : IRequest<Unit>;
}