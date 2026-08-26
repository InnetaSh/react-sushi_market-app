using AutoMapper;
using MediatR;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;

            if (string.IsNullOrWhiteSpace(titleEn) && !string.IsNullOrWhiteSpace(titleUa))
            {
                titleEn = await Translator.TranslateAsync(titleUa, "uk", "en");
            }
            else if (string.IsNullOrWhiteSpace(titleUa) && !string.IsNullOrWhiteSpace(titleEn))
            {
                titleUa = await Translator.TranslateAsync(titleEn, "en", "uk");
            }

            var category = _mapper.Map<Category>(request);

            category.TitleUa = titleUa;
            category.TitleEn = titleEn;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}