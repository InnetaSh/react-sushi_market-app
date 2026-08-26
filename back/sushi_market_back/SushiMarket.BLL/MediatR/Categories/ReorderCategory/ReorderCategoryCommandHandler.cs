using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.ReorderCategory
{
    public class ReorderCategoryCommandHandler : IRequestHandler<ReorderCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public ReorderCategoryCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ReorderCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.CategoryId));
            }

            category.SortOrder = request.NewSortOrder;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}