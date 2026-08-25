using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly SushiMarketDbContext _context;

        public GetProductByIdQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.Id == request.Id)
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
                    LikesCount = p.LikesCount,
                    CategoryId = p.CategoryId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");
            }

            return product;
        }
    }
}