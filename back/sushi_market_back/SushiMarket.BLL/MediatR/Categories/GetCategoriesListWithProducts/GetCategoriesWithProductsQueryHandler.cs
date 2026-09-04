using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts
{
    public class GetCategoriesWithProductsQueryHandler : IRequestHandler<GetCategoriesWithProductsQuery, List<CategoryWithProductsDto>>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesWithProductsQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryWithProductsDto>> Handle(GetCategoriesWithProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .ProjectTo<CategoryWithProductsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}