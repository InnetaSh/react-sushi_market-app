using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IEnumerable<CategoryDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoriesListQueryHandler> _logger;

        public GetCategoriesListQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetCategoriesListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the list of categories ordered by SortOrder.");

            var categories = await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} categories.", categories.Count);

            return categories;
        }
    }
}