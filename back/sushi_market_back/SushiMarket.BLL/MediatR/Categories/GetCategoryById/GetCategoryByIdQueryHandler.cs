using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly SushiMarketDbContext _context;

        public GetCategoryByIdQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.Id == request.Id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    TitleUa = c.TitleUa,
                    TitleEn = c.TitleEn,
                    ImgSrc = c.ImgSrc,
                    SortOrder = c.SortOrder
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.Id} was not found.");
            }

            return category;
        }
    }
}