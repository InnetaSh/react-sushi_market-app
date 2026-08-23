using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly SushiMarketDbContext _context;

        public UpdateProductCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");
            }

            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;
            string descUa = request.DescriptionUa;
            string descEn = request.DescriptionEn;

            if (titleUa != product.TitleUa && (string.IsNullOrWhiteSpace(titleEn) || titleEn == product.TitleEn))
            {
                titleEn = await Translator.TranslateAsync(titleUa, "uk", "en");
            }
            else if (titleEn != product.TitleEn && (string.IsNullOrWhiteSpace(titleUa) || titleUa == product.TitleUa))
            {
                titleUa = await Translator.TranslateAsync(titleEn, "en", "uk");
            }

            if (descUa != product.DescriptionUa && (string.IsNullOrWhiteSpace(descEn) || descEn == product.DescriptionEn))
            {
                descEn = await Translator.TranslateAsync(descUa, "uk", "en");
            }
            else if (descEn != product.DescriptionEn && (string.IsNullOrWhiteSpace(descUa) || descUa == product.DescriptionUa))
            {
                descUa = await Translator.TranslateAsync(descEn, "en", "uk");
            }

            product.TitleUa = titleUa;
            product.TitleEn = titleEn;
            product.DescriptionUa = descUa;
            product.DescriptionEn = descEn;
            product.WeightOrVolume = request.WeightOrVolume;
            product.Price = request.Price;
            product.ImgSrc = request.ImgSrc;
            product.SortOrder = request.SortOrder;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}