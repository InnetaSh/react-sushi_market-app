using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using SushiMarket.BLL.MediatR.Products.DeleteProduct;
using SushiMarket.BLL.MediatR.Products.GetProductById;
using SushiMarket.BLL.MediatR.Products.GetProductsList;
using SushiMarket.BLL.MediatR.Products.ReorderProduct;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;

namespace sushi_market_back.Controllers
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
            return Ok(await _mediator.Send(new GetProductsListQuery(categoryId)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> ReorderProduct([FromBody] ReorderProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}