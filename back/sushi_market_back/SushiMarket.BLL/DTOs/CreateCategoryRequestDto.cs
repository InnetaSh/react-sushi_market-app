using Microsoft.AspNetCore.Http;

namespace SushiMarket.BLL.DTOs
{
    public class CreateCategoryRequestDto
    {
        public string TitleUa { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public double? SortOrder { get; set; }
        public IFormFile? Image { get; set; }
    }
}