using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sushi_market_back.Models;

namespace sushi_market_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : Controller
    {


        private List<Category> CategoriesList => categoriesByProduct.GetCategoriesList();

        private CategoriesByProduct categoriesByProduct;
        public MarketController()
        {
            categoriesByProduct = new CategoriesByProduct();
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            if (CategoriesList.Count == 0)
                return NotFound("No categories found.");
            return Ok(CategoriesList);
        }



    }
    public class CategoriesByProduct
    {

        private List<Category> CategoriesList;

        public CategoriesByProduct()
        {
            CategoriesList = LoadCategoriesList();
        }
        private List<Category> LoadCategoriesList()
        {



            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "categoriesList.json");

            var json = System.IO.File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Category>>(json) ?? new List<Category>();


        }


        public void SaveCategoriesList()
        {
            var json = JsonConvert.SerializeObject(CategoriesList, Formatting.Indented);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "categoriesList.json");
            System.IO.File.WriteAllText(path, json);
        }

        public List<Category> GetCategoriesList()
        {
            return CategoriesList;
        }
    }

}
