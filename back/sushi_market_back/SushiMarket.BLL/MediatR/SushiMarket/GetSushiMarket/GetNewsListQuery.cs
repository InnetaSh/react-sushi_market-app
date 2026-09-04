using MediatR;
using SushiMarket.BLL.DTOs.News;

namespace SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket
{
    public record GetNewsListQuery : IRequest<IEnumerable<NewsItemDto>>;
}
