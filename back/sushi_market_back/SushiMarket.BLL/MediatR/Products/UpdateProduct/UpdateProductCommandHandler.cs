using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            TranslatorHelper.Translator translator,
            ICloudinaryService cloudinaryService,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to update product with ID: {ProductId}", request.Id);

            var product = await _context.Products
                .FirstOrDefaultAsync(
                    p => p.Id == request.Id,
                    cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} was not found for update.", request.Id);
                throw new KeyNotFoundException(
                    string.Format(
                        ErrorMessages.ProductNotFound,
                        request.Id));
            }

            string titleUa = request.TitleUa ?? "";
            string titleEn = request.TitleEn ?? "";
            string descUa = request.DescriptionUa ?? "";
            string descEn = request.DescriptionEn ?? "";

            if (titleUa != product.TitleUa &&
                (string.IsNullOrWhiteSpace(titleEn) || titleEn == product.TitleEn))
            {
                _logger.LogInformation("Translating updated product title from Ukrainian to English: '{TitleUa}'", titleUa);
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (titleEn != product.TitleEn &&
                     (string.IsNullOrWhiteSpace(titleUa) || titleUa == product.TitleUa))
            {
                _logger.LogInformation("Translating updated product title from English to Ukrainian: '{TitleEn}'", titleEn);
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

            if (descUa != product.DescriptionUa &&
                (string.IsNullOrWhiteSpace(descEn) || descEn == product.DescriptionEn))
            {
                _logger.LogInformation("Translating updated product description from Ukrainian to English.");
                descEn = await _translator.TranslateAsync(
                    descUa,
                    "uk",
                    "en");
            }
            else if (descEn != product.DescriptionEn &&
                     (string.IsNullOrWhiteSpace(descUa) || descUa == product.DescriptionUa))
            {
                _logger.LogInformation("Translating updated product description from English to Ukrainian.");
                descUa = await _translator.TranslateAsync(
                    descEn,
                    "en",
                    "uk");
            }

            string? imagePath = null;
            if (request.Image != null && request.Image.Length > 0)
            {
                _logger.LogInformation("Uploading new image for product ID {ProductId} to Cloudinary...", request.Id);

                if (!string.IsNullOrEmpty(product.ImgSrc))
                {
                    try
                    {
                        await _cloudinaryService.DeleteImageAsync(product.ImgSrc);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to delete old image from Cloudinary for product ID {ProductId}", request.Id);
                    }
                }

                imagePath = await _cloudinaryService.UploadImageAsync(request.Image, "products");
            }

            _mapper.Map(request, product);

            product.TitleUa = titleUa;
            product.TitleEn = titleEn;
            product.DescriptionUa = descUa;
            product.DescriptionEn = descEn;

            if (!string.IsNullOrEmpty(imagePath))
            {
                product.ImgSrc = imagePath;
            }

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Product with ID {ProductId} successfully updated.", request.Id);
            }
            catch (DbUpdateException ex)
            {
                var innerMessage =
                    ex.InnerException?.Message ?? ex.Message;

                _logger.LogError(ex, "Database update error while saving product ID {ProductId}: {Error}", request.Id, innerMessage);
                throw new Exception($"DB Error: {innerMessage}");
            }

            return Unit.Value;
        }
    }
}