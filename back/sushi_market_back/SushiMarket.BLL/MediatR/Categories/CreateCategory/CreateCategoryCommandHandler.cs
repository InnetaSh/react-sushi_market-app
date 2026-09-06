using AutoMapper;
using MediatR;
using SushiMarket.BLL.Services;
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

        public CreateCategoryCommandHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            Translator translator,
            ICloudinaryService cloudinaryService)
        {
            _context = context;
            _mapper = mapper;
            _translator = translator;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;

            
            if (string.IsNullOrWhiteSpace(titleEn) && !string.IsNullOrWhiteSpace(titleUa))
            {
                titleEn = await _translator.TranslateAsync(titleUa, "uk", "en");
            }
            else if (string.IsNullOrWhiteSpace(titleUa) && !string.IsNullOrWhiteSpace(titleEn))
            {
                titleUa = await _translator.TranslateAsync(titleEn, "en", "uk");
            }

            string? imagePath = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                imagePath = await _cloudinaryService.UploadImageAsync(request.Image, "categories");
            }

            var category = _mapper.Map<Category>(request);
            category.TitleUa = titleUa;
            category.TitleEn = titleEn;
            category.ImgSrc = imagePath ?? string.Empty;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}