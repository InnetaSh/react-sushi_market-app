using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable<ProductDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsListQueryHandler> _logger;

        public GetProductsListQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetProductsListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            if (request.CategoryId.HasValue)
            {
                _logger.LogInformation("Fetching products list filtered by Category ID: {CategoryId}", request.CategoryId.Value);
            }
            else
            {
                _logger.LogInformation("Fetching the full products list.");
            }

            var query = _context.Products.AsQueryable();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            var products = await query
                .OrderBy(p => p.SortOrder ?? double.MaxValue)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} products.", products.Count);

            return products;
        }
    }
}