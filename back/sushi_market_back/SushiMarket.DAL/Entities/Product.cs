using System.Collections.Generic;

namespace SushiMarket.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string TitleUa { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DescriptionUa { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;
        public string WeightOrVolume { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; }

        public int? LikesCount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}