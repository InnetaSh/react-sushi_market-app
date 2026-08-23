using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesList
{
    public record GetCategoriesListQuery() : IRequest<IEnumerable<CategoryDto>>;
}