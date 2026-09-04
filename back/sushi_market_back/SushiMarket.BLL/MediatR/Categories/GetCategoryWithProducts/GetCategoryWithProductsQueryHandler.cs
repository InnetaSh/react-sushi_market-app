using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public class GetCategoryWithProductsQueryHandler : IRequestHandler<GetCategoryWithProductsQuery, CategoryWithProductsDto>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryWithProductsQueryHandler(SushiMarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryWithProductsDto> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.Id == request.CategoryId)
                .ProjectTo<CategoryWithProductsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.CategoryId));
            }

            return category;
        }
    }
}