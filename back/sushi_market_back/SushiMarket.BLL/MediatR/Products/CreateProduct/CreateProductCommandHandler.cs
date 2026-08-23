using MediatR;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;
using static SushiMarket.BLL.Helpers.TranslatorHelper;

namespace SushiMarket.BLL.MediatR.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly SushiMarketDbContext _context;

        public CreateProductCommandHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            string titleUa = request.TitleUa;
            string titleEn = request.TitleEn;
            string descUa = request.DescriptionUa;
            string descEn = request.DescriptionEn;

            if (string.IsNullOrWhiteSpace(titleEn) && !string.IsNullOrWhiteSpace(titleUa))
                titleEn = await Translator.TranslateAsync(titleUa, "uk", "en");
            else if (string.IsNullOrWhiteSpace(titleUa) && !string.IsNullOrWhiteSpace(titleEn))
                titleUa = await Translator.TranslateAsync(titleEn, "en", "uk");

            if (string.IsNullOrWhiteSpace(descEn) && !string.IsNullOrWhiteSpace(descUa))
                descEn = await Translator.TranslateAsync(descUa, "uk", "en");
            else if (string.IsNullOrWhiteSpace(descUa) && !string.IsNullOrWhiteSpace(descEn))
                descUa = await Translator.TranslateAsync(descEn, "en", "uk");

            var product = new Product
            {
                TitleUa = titleUa,
                TitleEn = titleEn,
                DescriptionUa = descUa,
                DescriptionEn = descEn,
                WeightOrVolume = request.WeightOrVolume,
                Price = request.Price,
                ImgSrc = request.ImgSrc,
                SortOrder = request.SortOrder,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}