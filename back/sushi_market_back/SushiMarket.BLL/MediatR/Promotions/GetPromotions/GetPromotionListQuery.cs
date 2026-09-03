using MediatR;
using SushiMarket.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarket.BLL.MediatR.Promotions.GetPromotions
{
    public record GetPromotionListQuery : IRequest<IEnumerable<PromotionDto>>;
}
