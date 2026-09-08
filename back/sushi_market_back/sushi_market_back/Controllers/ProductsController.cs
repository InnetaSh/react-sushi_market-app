using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.DTOs.Products;
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
        [Authorize(Roles = "MainAdministrator")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto request)
        {
            var command = new CreateProductCommand(
                request.TitleUa,
                request.TitleEn,
                request.DescriptionUa,
                request.DescriptionEn,
                request.WeightOrVolume,
                request.Price,
                request.Image,
                request.SortOrder,
                request.CategoryId
            );

            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "MainAdministrator")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductRequestDto request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var command = new UpdateProductCommand(
                request.Id,
                request.TitleUa,
                request.TitleEn,
                request.DescriptionUa,
                request.DescriptionEn,
                request.WeightOrVolume,
                request.Price,
                request.Image,
                request.SortOrder,
                request.CategoryId
            );

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "MainAdministrator")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        [HttpPatch("reorder")]
        [Authorize(Roles = "MainAdministrator")]
        public async Task<IActionResult> ReorderProduct([FromBody] ReorderProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}