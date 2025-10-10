using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sushi_market_back.Models;

namespace sushi_market_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private List<Product> ProductTitleList => titleByProduct.GetProductTitleList();

        private TitleByProduct titleByProduct;
        public MenuController()
        {
            titleByProduct = new TitleByProduct();
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            if (ProductTitleList.Count == 0)
                return NotFound("No categories found.");
            return Ok(ProductTitleList);
        }



        [HttpGet("search/category/{category}")]
        public ActionResult<List<Product>> GetByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category is required.");
            }

            var filteredProducts = ProductTitleList
             .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
             .ToList();

            if (filteredProducts.Count > 0)
            {
                return Ok(filteredProducts);
            }

            return NotFound(new { message = "Category not found" });
        }

    }

    public class TitleByProduct
    {

        private List<Product> ProductTitleList;

        public TitleByProduct()
        {
            ProductTitleList = LoadProductTitleList();
        }
        private List<Product> LoadProductTitleList()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "productTitleList.json");

            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();


        }


        public void SaveProductTitleList()
        {
            var json = JsonConvert.SerializeObject(ProductTitleList, Formatting.Indented);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "productTitleList.json");
            System.IO.File.WriteAllText(path, json);
        }

        public List<Product> GetProductTitleList()
        {
            return ProductTitleList;
        }
    }
}
