using MediatR;

namespace SushiMarket.BLL.MediatR.Products.CreateProduct
{
    public record CreateProductCommand(
        string Title,
        string Description,
        string WeightOrVolume,
        decimal Price,
        string ImgSrc,
        double? SortOrder,
        int CategoryId
    ) : IRequest<int>;
}