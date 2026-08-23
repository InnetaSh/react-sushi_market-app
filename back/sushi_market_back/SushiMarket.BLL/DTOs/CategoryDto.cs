namespace SushiMarket.BLL.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }
    }
}