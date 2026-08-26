using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, IEnumerable<ProductDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsListQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Products.AsQueryable();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            return await query
                .OrderBy(p => p.SortOrder ?? double.MaxValue)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}