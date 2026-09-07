using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.Resources;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(
            SushiMarketDbContext context,
            IMapper mapper,
            ILogger<GetProductByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", request.Id);

            var product = await _context.Products
                .Where(p => p.Id == request.Id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} was not found.", request.Id);
                throw new KeyNotFoundException(string.Format(ErrorMessages.ProductNotFound, request.Id));
            }

            _logger.LogInformation("Successfully retrieved product with ID: {ProductId}", request.Id);

            return product;
        }
    }
}