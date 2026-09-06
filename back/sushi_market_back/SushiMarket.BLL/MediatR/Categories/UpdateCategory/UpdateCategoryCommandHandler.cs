using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.Resources;
using SushiMarket.BLL.Services;
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

        public UpdateCategoryCommandHandler(
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
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(
                    c => c.Id == request.Id,
                    cancellationToken);

            if (category == null)
            {
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
                titleEn = await _translator.TranslateAsync(
                    titleUa,
                    "uk",
                    "en");
            }
            else if (titleEn != category.TitleEn &&
                     (string.IsNullOrWhiteSpace(titleUa) || titleUa == category.TitleUa))
            {
                titleUa = await _translator.TranslateAsync(
                    titleEn,
                    "en",
                    "uk");
            }

           
            string? imagePath = null;
            if (request.Image != null && request.Image.Length > 0)
            {
                imagePath = await _cloudinaryService.UploadImageAsync(request.Image, "categories");
            }

            _mapper.Map(request, category);

            category.TitleUa = titleUa;
            category.TitleEn = titleEn;

           
            if (!string.IsNullOrEmpty(imagePath))
            {
                category.ImgSrc = imagePath;
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