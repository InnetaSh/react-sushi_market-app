using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.ReorderCategory
{
    public class ReorderCategoryCommandHandler : IRequestHandler<ReorderCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly ILogger<ReorderCategoryCommandHandler> _logger;

        public ReorderCategoryCommandHandler(
            SushiMarketDbContext context,
            ILogger<ReorderCategoryCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(ReorderCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to reorder category with ID {CategoryId} to new sort order: {NewSortOrder}", request.CategoryId, request.NewSortOrder);

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} was not found for reordering.", request.CategoryId);
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.CategoryId));
            }

            category.SortOrder = request.NewSortOrder;
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category with ID {CategoryId} successfully reordered to {NewSortOrder}.", request.CategoryId, request.NewSortOrder);

            return Unit.Value;
        }
    }
}