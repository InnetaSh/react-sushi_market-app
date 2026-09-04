using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket;

namespace sushi_market_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsItemDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetNewsListQuery());
            return Ok(result);
        }
    }
}
