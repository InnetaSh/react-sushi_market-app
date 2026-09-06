using MediatR;
using Microsoft.AspNetCore.Http;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public record CreateCategoryCommand(
      string TitleUa,
      string TitleEn,
      IFormFile? Image,
      double? SortOrder
  ) : IRequest<int>;
}