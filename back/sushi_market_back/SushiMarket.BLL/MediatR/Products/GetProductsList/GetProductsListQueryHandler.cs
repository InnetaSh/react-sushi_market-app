using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable<ProductDto>>
    {
        private readonly SushiMarketDbContext _context;

        public GetProductsListQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            return await query
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
                    LikesCount = p.LikesCount,
                    CategoryId = p.CategoryId
                })
                .ToListAsync(cancellationToken);
        }
    }
}