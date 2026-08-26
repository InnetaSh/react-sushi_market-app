using SushiMarket.BLL.Validators;

namespace SushiMarket.BLL.MediatR.Products.GetProductById
{
    public class GetProductByIdQueryValidator : PositiveIdValidator<GetProductByIdQuery>
    {
     
    }
}