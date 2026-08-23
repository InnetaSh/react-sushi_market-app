using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL;

namespace SushiMarket.BLL.MediatR.Categories.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, IEnumerable<CategoryDto>>
    {
        private readonly SushiMarketDbContext _context;

        public GetCategoriesListQueryHandler(SushiMarketDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .OrderBy(c => c.SortOrder ?? double.MaxValue)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    TitleUa = c.TitleUa,
                    TitleEn = c.TitleEn,
                    ImgSrc = c.ImgSrc,
                    SortOrder = c.SortOrder
                })
                .ToListAsync(cancellationToken);
        }
    }
}