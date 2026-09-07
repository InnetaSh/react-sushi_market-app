using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts
{
    public class GetCategoriesWithProductsQueryHandler : IRequestHandler<GetCategoriesWithProductsQuery, List<CategoryWithProductsDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoriesWithProductsQueryHandler> _logger;

        public GetCategoriesWithProductsQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetCategoriesWithProductsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoryWithProductsDto>> Handle(GetCategoriesWithProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all categories along with their products, ordered by SortOrder.");

            var categories = await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .ProjectTo<CategoryWithProductsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} categories with products.", categories.Count);

            return categories;
        }
    }
}