namespace SushiMarket.BLL.DTOs
{
    public class CategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }

        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}