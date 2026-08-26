using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : PositiveIdValidator<GetCategoryByIdQuery>
    {
       
    }
}