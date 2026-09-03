using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.MediatR.Promotions.GetPromotions;

namespace sushi_market_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PromotionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetPromotionListQuery());
            return Ok(result);
        }
    }
}
