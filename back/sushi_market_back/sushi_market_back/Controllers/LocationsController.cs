using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.BLL.MediatR.Locations.GetLocations;

namespace sushi_market_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetLocationListQuery());
            return Ok(result);
        }
    }
}