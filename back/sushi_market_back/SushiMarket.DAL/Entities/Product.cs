namespace SushiMarket.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // Старый about_1
        public string WeightOrVolume { get; set; } = string.Empty; // Старый about_2
        public decimal Price { get; set; } 
        public string ImgSrc { get; set; } = string.Empty;

        public double? SortOrder { get; set; }
        public int? LikesCount { get; set; } 

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}