using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.BLL.MediatR.Products.CreateProduct
{
    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, int>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            TranslatorHelper.Translator translator,
            ICloudinaryService cloudinaryService,
            ILogger<CreateProductCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
            _logger = logger;
        }

        public async Task<int> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting creation of a new product with titles: UA='{TitleUa}', EN='{TitleEn}'", request.TitleUa, request.TitleEn);

            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;
            string descUa = request.DescriptionUa;
            string descEn = request.DescriptionEn;

            if (string.IsNullOrWhiteSpace(titleEn) &&
                !string.IsNullOrWhiteSpace(titleUa))
            {
                _logger.LogInformation("Translating product title from Ukrainian to English: '{TitleUa}'", titleUa);
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (string.IsNullOrWhiteSpace(titleUa) &&
                     !string.IsNullOrWhiteSpace(titleEn))
            {
                _logger.LogInformation("Translating product title from English to Ukrainian: '{TitleEn}'", titleEn);
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

            if (string.IsNullOrWhiteSpace(descEn) &&
                !string.IsNullOrWhiteSpace(descUa))
            {
                _logger.LogInformation("Translating product description from Ukrainian to English.");
                descEn = await _translator.TranslateAsync(
                    descUa,
                    "uk",
                    "en");
            }
            else if (string.IsNullOrWhiteSpace(descUa) &&
                     !string.IsNullOrWhiteSpace(descEn))
            {
                _logger.LogInformation("Translating product description from English to Ukrainian.");
                descUa = await _translator.TranslateAsync(
                    descEn,
                    "en",
                    "uk");
            }

            string? imagePath = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                _logger.LogInformation("Uploading image for product to Cloudinary...");
                imagePath = await _cloudinaryService.UploadImageAsync(request.Image, "products");
            }

            var product = _mapper.Map<Product>(request);

            product.TitleUa = titleUa;
            product.TitleEn = titleEn;
            product.DescriptionUa = descUa;
            product.DescriptionEn = descEn;
            product.ImgSrc = imagePath ?? string.Empty;

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Product successfully created with ID: {ProductId}", product.Id);

            return product.Id;
        }
    }
}