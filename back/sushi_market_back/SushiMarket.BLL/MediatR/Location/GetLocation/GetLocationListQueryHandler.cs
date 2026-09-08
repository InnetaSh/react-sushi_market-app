using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Locations.GetLocations
{
    public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, IEnumerable<LocationDto>>
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

        public async Task<IEnumerable<LocationDto>> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching the list of locations.");

            var locations = await _context.Locations
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Successfully retrieved {Count} locations.", locations.Count);

            return locations;
        }
    }
}