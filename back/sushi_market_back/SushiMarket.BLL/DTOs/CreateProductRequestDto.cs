using Microsoft.AspNetCore.Http;

namespace SushiMarket.BLL.DTOs
{
    public class CreateProductRequestDto
    {
        public string TitleUa { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionUa { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string WeightOrVolume { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double? SortOrder { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Image { get; set; }
    }
}