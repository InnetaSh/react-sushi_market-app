using AutoMapper;
using MediatR;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Services;
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

        public CreateProductCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            TranslatorHelper.Translator translator,
            ICloudinaryService cloudinaryService)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<int> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;
            string descUa = request.DescriptionUa;
            string descEn = request.DescriptionEn;

            if (string.IsNullOrWhiteSpace(titleEn) &&
                !string.IsNullOrWhiteSpace(titleUa))
            {
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (string.IsNullOrWhiteSpace(titleUa) &&
                     !string.IsNullOrWhiteSpace(titleEn))
            {
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

            if (string.IsNullOrWhiteSpace(descEn) &&
                !string.IsNullOrWhiteSpace(descUa))
            {
                descEn = await _translator.TranslateAsync(
                    descUa,
                    "uk",
                    "en");
            }
            else if (string.IsNullOrWhiteSpace(descUa) &&
                     !string.IsNullOrWhiteSpace(descEn))
            {
                descUa = await _translator.TranslateAsync(
                    descEn,
                    "en",
                    "uk");
            }

            string? imagePath = null;

            if (request.Image != null && request.Image.Length > 0)
            {
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

            return product.Id;
        }
    }
}