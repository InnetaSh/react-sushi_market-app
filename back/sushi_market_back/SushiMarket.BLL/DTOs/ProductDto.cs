namespace SushiMarket.BLL.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string WeightOrVolume { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }
        public int? LikesCount { get; set; }
        public int CategoryId { get; set; }
    }
}