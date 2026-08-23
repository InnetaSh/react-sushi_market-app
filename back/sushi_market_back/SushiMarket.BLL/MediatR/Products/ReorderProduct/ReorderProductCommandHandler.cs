using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.ReorderProduct
{
    public class ReorderProductCommandHandler : IRequestHandler<ReorderProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public ReorderProductCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReorderProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.ProductId} was not found.");
            }

            product.SortOrder = request.NewSortOrder;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}