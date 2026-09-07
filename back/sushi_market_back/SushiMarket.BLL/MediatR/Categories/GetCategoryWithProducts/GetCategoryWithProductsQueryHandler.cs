using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts
{
    public class GetCategoryWithProductsQueryHandler : IRequestHandler<GetCategoryWithProductsQuery, CategoryWithProductsDto>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryWithProductsQueryHandler> _logger;

        public GetCategoryWithProductsQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetCategoryWithProductsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryWithProductsDto> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching category with products for Category ID: {CategoryId}", request.CategoryId);

            var category = await _context.Categories
                .Where(c => c.Id == request.CategoryId)
                .ProjectTo<CategoryWithProductsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} and its products was not found.", request.CategoryId);
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.CategoryId));
            }

            _logger.LogInformation("Successfully retrieved category with products for Category ID: {CategoryId}", request.CategoryId);

            return category;
        }
    }
}