using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Categories.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public UpdateCategoryCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.Id} was not found.");
            }

            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;

            if (titleUa != category.TitleUa && (string.IsNullOrWhiteSpace(titleEn) || titleEn == category.TitleEn))
            {
                titleEn = await Translator.TranslateAsync(titleUa, "uk", "en");
            }
            else if (titleEn != category.TitleEn && (string.IsNullOrWhiteSpace(titleUa) || titleUa == category.TitleUa))
            {
                titleUa = await Translator.TranslateAsync(titleEn, "en", "uk");
            }

            category.TitleUa = titleUa;
            category.TitleEn = titleEn;
            category.ImgSrc = request.ImgSrc;
            category.SortOrder = request.SortOrder;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}