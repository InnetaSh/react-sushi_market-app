using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public UpdateCategoryCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.Id} was not found.");
            }

            if (request.TitleUa != null) category.TitleUa = request.TitleUa;
            if (request.TitleEn != null) category.TitleEn = request.TitleEn;
            if (request.SortOrder.HasValue) category.SortOrder = request.SortOrder.Value;

            if (!string.IsNullOrEmpty(request.ImgSrc))
            {
                category.ImgSrc = request.ImgSrc;
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"DB Error: {innerMessage}");
            }

            return Unit.Value;
        }
    }
}