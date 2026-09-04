using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs.Promotions;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Promotions.GetPromotions
{
    public class GetLocationListQueryHandler : IRequestHandler<GetPromotionListQuery, IEnumerable<PromotionDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetLocationListQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PromotionDto>> Handle(GetPromotionListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Promotions
                .ProjectTo<PromotionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
