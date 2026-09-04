using MediatR;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Products.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>, IHasId;
}