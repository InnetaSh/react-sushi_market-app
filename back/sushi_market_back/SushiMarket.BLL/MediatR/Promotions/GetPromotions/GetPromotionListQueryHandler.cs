using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Promotions;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Promotions.GetPromotions
{
    public class GetLocationListQueryHandler : IRequestHandler<GetPromotionListQuery, IEnumerable<PromotionDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetLocationListQueryHandler> _logger;

        public GetLocationListQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetLocationListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PromotionDto>> Handle(GetPromotionListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the list of promotions.");

            var promotions = await _context.Promotions
                .ProjectTo<PromotionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} promotions.", promotions.Count);

            return promotions;
        }
    }
}