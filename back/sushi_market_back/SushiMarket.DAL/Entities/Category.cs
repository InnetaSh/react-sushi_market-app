namespace SushiMarket.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImgSrc { get; set; } = string.Empty;
        public double? SortOrder { get; set; } 

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}