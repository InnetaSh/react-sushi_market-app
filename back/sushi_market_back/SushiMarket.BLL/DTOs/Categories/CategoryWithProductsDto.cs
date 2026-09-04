using SushiMarket.BLL.DTOs.Products;

namespace SushiMarket.BLL.DTOs.Categories
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string TitleUa { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }

        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}