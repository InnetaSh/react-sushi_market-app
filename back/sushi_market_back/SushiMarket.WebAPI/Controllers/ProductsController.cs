using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.MediatR.Products.GetProductsList;
using System.Runtime.InteropServices;

namespace SushiMarket.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? categoryId)
        {
            var query = new GetProductsListQuery(categoryId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}