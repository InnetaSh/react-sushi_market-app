using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IEnumerable<CategoryDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}