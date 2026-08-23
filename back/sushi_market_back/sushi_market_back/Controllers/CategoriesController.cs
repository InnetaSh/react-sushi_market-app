using MediatR;
using Microsoft.AspNetCore.Mvc;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using SushiMarket.BLL.MediatR.Categories.DeleteCategory;
using SushiMarket.BLL.MediatR.Categories.GetCategoriesList;
using SushiMarket.BLL.MediatR.Categories.GetCategoryById;
using SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts;
using SushiMarket.BLL.MediatR.Categories.ReorderCategory;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;

namespace sushi_market_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            var result = await _mediator.Send(new GetCategoryWithProductsQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> ReorderCategory([FromBody] ReorderCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}