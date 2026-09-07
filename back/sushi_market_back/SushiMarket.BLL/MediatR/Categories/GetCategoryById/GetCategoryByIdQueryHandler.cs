using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

        public GetCategoryByIdQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching category with ID: {CategoryId}", request.Id);

            var category = await _context.Categories
                .Where(c => c.Id == request.Id)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} was not found.", request.Id);
                throw new KeyNotFoundException(string.Format(ErrorMessages.CategoryNotFound, request.Id));
            }

            _logger.LogInformation("Successfully retrieved category with ID: {CategoryId}", request.Id);

            return category;
        }
    }
}