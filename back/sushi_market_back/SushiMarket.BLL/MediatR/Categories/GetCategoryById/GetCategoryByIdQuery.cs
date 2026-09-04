using MediatR;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.MediatR.Interface;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>, IHasId;
}