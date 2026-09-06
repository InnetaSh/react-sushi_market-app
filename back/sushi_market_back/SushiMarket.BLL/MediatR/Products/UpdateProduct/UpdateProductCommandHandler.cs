using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services;
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

        public UpdateProductCommandHandler(
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

        public async Task<Unit> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(
                    p => p.Id == request.Id,
                    cancellationToken);

            if (product == null)
            {
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
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (titleEn != product.TitleEn &&
                     (string.IsNullOrWhiteSpace(titleUa) || titleUa == product.TitleUa))
            {
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

            if (descUa != product.DescriptionUa &&
                (string.IsNullOrWhiteSpace(descEn) || descEn == product.DescriptionEn))
            {
                descEn = await _translator.TranslateAsync(
                    descUa,
                    "uk",
                    "en");
            }
            else if (descEn != product.DescriptionEn &&
                     (string.IsNullOrWhiteSpace(descUa) || descUa == product.DescriptionUa))
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
            }
            catch (DbUpdateException ex)
            {
                var innerMessage =
                    ex.InnerException?.Message ?? ex.Message;

                throw new Exception($"DB Error: {innerMessage}");
            }

            return Unit.Value;
        }
    }
}