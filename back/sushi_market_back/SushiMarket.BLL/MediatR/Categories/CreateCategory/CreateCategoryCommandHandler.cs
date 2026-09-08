using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Services;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly Translator _translator;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            Translator translator,
            ICloudinaryService cloudinaryService,
            ILogger<CreateCategoryCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting creation of a new category with titles: UA='{TitleUa}', EN='{TitleEn}'", request.TitleUa, request.TitleEn);

            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;

            if (string.IsNullOrWhiteSpace(titleEn) && !string.IsNullOrWhiteSpace(titleUa))
            {
                _logger.LogInformation("Translating category title from Ukrainian to English: '{TitleUa}'", titleUa);
                titleEn = await _translator.TranslateAsync(titleUa, "uk", "en");
            }
            else if (string.IsNullOrWhiteSpace(titleUa) && !string.IsNullOrWhiteSpace(titleEn))
            {
                _logger.LogInformation("Translating category title from English to Ukrainian: '{TitleEn}'", titleEn);
                titleUa = await _translator.TranslateAsync(titleEn, "en", "uk");
            }

            string? imagePath = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                _logger.LogInformation("Uploading image for category to Cloudinary...");
                imagePath = await _cloudinaryService.UploadImageAsync(request.Image, "categories");
            }

            var category = _mapper.Map<Category>(request);
            category.TitleUa = titleUa;
            category.TitleEn = titleEn;
            category.ImgSrc = imagePath ?? string.Empty;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category successfully created with ID: {CategoryId}", category.Id);

            return category.Id;
        }
    }
}