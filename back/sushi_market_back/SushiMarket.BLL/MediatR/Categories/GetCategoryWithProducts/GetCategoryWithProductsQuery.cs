using MediatR;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public record GetCategoryWithProductsQuery(int CategoryId) : IRequest<CategoryWithProductsDto>, IHasCategoryId;
}