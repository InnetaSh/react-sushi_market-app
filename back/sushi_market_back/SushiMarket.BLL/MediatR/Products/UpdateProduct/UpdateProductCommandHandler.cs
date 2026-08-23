using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public UpdateProductCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");
            }

            product.Title = request.Title;
            product.Description = request.Description;
            product.WeightOrVolume = request.WeightOrVolume;
            product.Price = request.Price;
            product.ImgSrc = request.ImgSrc;
            product.SortOrder = request.SortOrder;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}