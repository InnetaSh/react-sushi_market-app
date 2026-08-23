using MediatR;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public record UpdateProductCommand(
        int Id,
        string Title,
        string Description,
        string WeightOrVolume,
        decimal Price,
        string ImgSrc,
        double? SortOrder,
        int CategoryId
    ) : IRequest<Unit>;
}