using MediatR;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public record GetProductsListQuery(int? CategoryId) : IRequest<IEnumerable<ProductDto>>;
}