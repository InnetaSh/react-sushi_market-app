using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.DTOs;
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
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto request)
        {
            string? imagePath = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }

                imagePath = $"/uploads/products/{uniqueFileName}";
            }

            var command = new CreateProductCommand(
                request.TitleUa,
                request.TitleEn,
                request.DescriptionUa,
                request.DescriptionEn,
                request.WeightOrVolume,
                request.Price,
                imagePath ?? string.Empty,
                request.SortOrder,
                request.CategoryId
            );

            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductRequestDto request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var existingProduct = await _mediator.Send(new GetProductByIdQuery(id));
            string? imagePath = existingProduct?.ImgSrc;

            if (request.Image != null && request.Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(fileStream);
                }

                imagePath = $"/uploads/products/{uniqueFileName}";
            }

            var command = new UpdateProductCommand(
                request.Id,
                request.TitleUa,
                request.TitleEn,
                request.DescriptionUa,
                request.DescriptionEn,
                request.WeightOrVolume,
                request.Price,
                imagePath ?? string.Empty,
                request.SortOrder,
                request.CategoryId
            );

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