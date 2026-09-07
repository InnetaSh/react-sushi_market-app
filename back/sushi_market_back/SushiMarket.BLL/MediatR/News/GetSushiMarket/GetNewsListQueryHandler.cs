using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket
{
    public class GetNewsListQueryHandler : IRequestHandler<GetNewsListQuery, IEnumerable<NewsItemDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetNewsListQueryHandler> _logger;

        public GetNewsListQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetNewsListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NewsItemDto>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the list of news items.");

            var newsItems = await _context.News
                .ProjectTo<NewsItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} news items.", newsItems.Count);

            return newsItems;
        }
    }
}