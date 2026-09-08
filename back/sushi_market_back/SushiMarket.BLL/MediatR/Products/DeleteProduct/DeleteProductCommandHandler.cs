using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(
            SushiMarketDbContext context,
            ICloudinaryService cloudinaryService,
            ILogger<DeleteProductCommandHandler> logger)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete product with ID: {ProductId}", request.Id);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} was not found for deletion.", request.Id);
                throw new KeyNotFoundException(string.Format(ErrorMessages.ProductNotFound, request.Id));
            }

            if (!string.IsNullOrEmpty(product.ImgSrc))
            {
                try
                {
                    await _cloudinaryService.DeleteImageAsync(product.ImgSrc);
                    _logger.LogInformation("Associated image for product {ProductId} was deleted from Cloudinary.", request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete image from Cloudinary for product ID {ProductId}", request.Id);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Product with ID {ProductId} successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}