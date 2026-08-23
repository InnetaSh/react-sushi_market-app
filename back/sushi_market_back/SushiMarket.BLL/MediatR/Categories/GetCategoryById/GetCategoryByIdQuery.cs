using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryById
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>;
}