namespace SushiMarket.BLL.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string TitleUa { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }
    }
}