using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts
{
    public class GetCategoriesWithProductsQueryHandler : IRequestHandler<GetCategoriesWithProductsQuery, List<CategoryWithProductsDto>>
    {
        private readonly SushiMarketDbContext _context;

        public GetCategoriesWithProductsQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryWithProductsDto>> Handle(GetCategoriesWithProductsQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .Select(c => new CategoryWithProductsDto
                {
                    Id = c.Id,
                    TitleUa = c.TitleUa,
                    TitleEn = c.TitleEn,
                    ImgSrc = c.ImgSrc,
                    SortOrder = c.SortOrder,
                    Products = c.Products
                        .OrderBy(p => p.SortOrder ?? double.MaxValue)
                        .Select(p => new ProductDto
                        {
                            Id = p.Id,
                            TitleUa = p.TitleUa,
                            TitleEn = p.TitleEn,
                            DescriptionUa = p.DescriptionUa,
                            DescriptionEn = p.DescriptionEn,
                            WeightOrVolume = p.WeightOrVolume,
                            Price = p.Price,
                            ImgSrc = p.ImgSrc,
                            SortOrder = p.SortOrder,
                            CategoryId = p.CategoryId
                        })
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            return categories;
        }
    }
}