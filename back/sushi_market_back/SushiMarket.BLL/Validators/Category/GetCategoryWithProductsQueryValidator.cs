using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public class GetCategoryWithProductsQueryValidator : PositiveCategoryIdValidator<GetCategoryWithProductsQuery>
    {
     
    }
}