using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.ReorderProduct
{
    public class ReorderProductCommandHandler : IRequestHandler<ReorderProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly ILogger<ReorderProductCommandHandler> _logger;

        public ReorderProductCommandHandler(
            SushiMarketDbContext context,
            ILogger<ReorderProductCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(ReorderProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to reorder product with ID {ProductId} to new sort order: {NewSortOrder}", request.ProductId, request.NewSortOrder);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} was not found for reordering.", request.ProductId);
                throw new KeyNotFoundException(string.Format(ErrorMessages.ProductNotFound, request.ProductId));
            }

            bool supportsTransactions = _context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory";

            var transaction = supportsTransactions
                ? await _context.Database.BeginTransactionAsync(cancellationToken)
                : null;

            try
            {
                product.SortOrder = request.NewSortOrder;
                await _context.SaveChangesAsync(cancellationToken);

                if (transaction != null)
                {
                    await transaction.CommitAsync(cancellationToken);
                }

                _logger.LogInformation("Product with ID {ProductId} successfully reordered to {NewSortOrder}.", request.ProductId, request.NewSortOrder);

                return Unit.Value;
            }
            catch
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
                throw;
            }
        }
    }
}