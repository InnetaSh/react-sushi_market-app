using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Products.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}