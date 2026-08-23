using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public class GetCategoryWithProductsQueryHandler : IRequestHandler<GetCategoryWithProductsQuery, CategoryWithProductsDto>
    {
        private readonly SushiMarketDbContext _context;

        public GetCategoryWithProductsQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryWithProductsDto> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.Id == request.CategoryId)
                .Include(c => c.Products)
                .Select(c => new CategoryWithProductsDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImgSrc = c.ImgSrc,
                    SortOrder = c.SortOrder,
                    Products = c.Products
                        .OrderBy(p => p.SortOrder ?? double.MaxValue)
                        .Select(p => new ProductDto
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Description = p.Description,
                            WeightOrVolume = p.WeightOrVolume,
                            Price = p.Price,
                            ImgSrc = p.ImgSrc,
                            SortOrder = p.SortOrder,
                            CategoryId = p.CategoryId
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.CategoryId} was not found.");
            }

            return category;
        }
    }
}