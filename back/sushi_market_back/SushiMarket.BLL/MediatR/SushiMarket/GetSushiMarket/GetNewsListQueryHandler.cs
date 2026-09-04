using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket
{
    public class GetNewsListQueryHandler : IRequestHandler<GetNewsListQuery, IEnumerable<NewsItemDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetNewsListQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NewsItemDto>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.News
                .ProjectTo<NewsItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
