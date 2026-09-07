using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<DeleteCategoryCommandHandler> _logger;

        public DeleteCategoryCommandHandler(
            SushiMarketDbContext context,
            ICloudinaryService cloudinaryService,
            ILogger<DeleteCategoryCommandHandler> logger)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete category with ID: {CategoryId}", request.Id);

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} was not found for deletion.", request.Id);
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.Id));
            }

            if (!string.IsNullOrEmpty(category.ImgSrc))
            {
                try
                {
                    await _cloudinaryService.DeleteImageAsync(category.ImgSrc);
                    _logger.LogInformation("Associated image for category {CategoryId} was deleted from Cloudinary.", request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete image from Cloudinary for category ID {CategoryId}", request.Id);
                }
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category with ID {CategoryId} successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}