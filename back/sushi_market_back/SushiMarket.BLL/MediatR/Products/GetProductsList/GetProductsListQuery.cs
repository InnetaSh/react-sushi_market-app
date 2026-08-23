using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public record GetProductsListQuery(int? CategoryId) : IRequest<IEnumerable<ProductDto>>;
}