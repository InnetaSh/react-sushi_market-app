using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public record GetCategoryWithProductsQuery(int CategoryId) : IRequest<CategoryWithProductsDto>;
}