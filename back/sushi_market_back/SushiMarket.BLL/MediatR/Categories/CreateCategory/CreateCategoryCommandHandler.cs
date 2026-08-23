using MediatR;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Categories.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly SushiMarketDbContext _context;

        public CreateCategoryCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
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

            var category = new Category
            {
                TitleUa = titleUa,
                TitleEn = titleEn,
                ImgSrc = request.ImgSrc,
                SortOrder = request.SortOrder
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}