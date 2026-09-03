using MediatR;
using SushiMarket.BLL.DTOs;

namespace SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket
{
    public record GetNewsListQuery : IRequest<IEnumerable<NewsItemDto>>;
}
