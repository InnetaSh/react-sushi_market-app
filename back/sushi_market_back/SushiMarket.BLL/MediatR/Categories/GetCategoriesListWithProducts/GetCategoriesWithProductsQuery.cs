using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts
{
    public record GetCategoriesWithProductsQuery() : IRequest<List<CategoryWithProductsDto>>;
}