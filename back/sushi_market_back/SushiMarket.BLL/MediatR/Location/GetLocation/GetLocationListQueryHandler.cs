using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Locations.GetLocations
{
    public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, IEnumerable<LocationDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetLocationListQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Locations
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
