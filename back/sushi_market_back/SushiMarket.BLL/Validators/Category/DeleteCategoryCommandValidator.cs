using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Categories.DeleteCategory
{
    public class DeleteCategoryCommandValidator : PositiveIdValidator<DeleteCategoryCommand>
    {
       
    }
}