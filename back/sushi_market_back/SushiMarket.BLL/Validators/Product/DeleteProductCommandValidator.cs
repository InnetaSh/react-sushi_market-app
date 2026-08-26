using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Products.DeleteProduct
{
    public class DeleteProductCommandValidator : PositiveIdValidator<DeleteProductCommand>
    {
    }
}