using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<UpdateCategoryCommandHandler> _logger;

        public UpdateCategoryCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            TranslatorHelper.Translator translator,
            ICloudinaryService cloudinaryService,
            ILogger<UpdateCategoryCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to update category with ID: {CategoryId}", request.Id);

            var category = await _context.Categories
                .FirstOrDefaultAsync(
                    c => c.Id == request.Id,
                    cancellationToken);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} was not found for update.", request.Id);
                throw new KeyNotFoundException(
                    string.Format(
                        ErrorMessages.CategoryNotFound,
                        request.Id));
            }

            string titleUa = request.TitleUa ?? "";
            string titleEn = request.TitleEn ?? "";

            if (titleUa != category.TitleUa &&
                (string.IsNullOrWhiteSpace(titleEn) || titleEn == category.TitleEn))
            {
                _logger.LogInformation("Translating updated category title from Ukrainian to English: '{TitleUa}'", titleUa);
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (titleEn != category.TitleEn &&
                     (string.IsNullOrWhiteSpace(titleUa) || titleUa == category.TitleUa))
            {
                _logger.LogInformation("Translating updated category title from English to Ukrainian: '{TitleEn}'", titleEn);
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

            string? newImagePath = null;
            string? oldImagePath = category.ImgSrc;

            if (request.Image != null && request.Image.Length > 0)
            {
                _logger.LogInformation("Uploading new image for category ID {CategoryId} to Cloudinary...", request.Id);
                newImagePath = await _cloudinaryService.UploadImageAsync(request.Image, "categories");
            }

            _mapper.Map(request, category);
            category.TitleUa = titleUa;
            category.TitleEn = titleEn;

            if (!string.IsNullOrEmpty(newImagePath))
            {
                category.ImgSrc = newImagePath;
            }

            bool supportsTransactions = _context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory";

            var transaction = supportsTransactions
                ? await _context.Database.BeginTransactionAsync(cancellationToken)
                : null;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                if (transaction != null)
                {
                    await transaction.CommitAsync(cancellationToken);
                }

                _logger.LogInformation("Category with ID {CategoryId} successfully updated.", request.Id);
            }
            catch (DbUpdateException ex)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }

                if (!string.IsNullOrEmpty(newImagePath))
                {
                    try { await _cloudinaryService.DeleteImageAsync(newImagePath); } catch { }
                }

                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError(ex, "Database update error while saving category ID {CategoryId}: {Error}", request.Id, innerMessage);
                throw new Exception($"DB Error: {innerMessage}");
            }

            if (!string.IsNullOrEmpty(newImagePath) && !string.IsNullOrEmpty(oldImagePath))
            {
                try
                {
                    await _cloudinaryService.DeleteImageAsync(oldImagePath);
                    _logger.LogInformation("Old image for category {CategoryId} was deleted from Cloudinary.", request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete old image from Cloudinary for category ID {CategoryId}", request.Id);
                }
            }

            return Unit.Value;
        }
    }
}