namespace sushi_market_back.Models
{
    public class Product
    {

        //[JsonPropertyName("id")]
        public int Id { get; set; }

        //[JsonPropertyName("category")]
        public string Category { get; set; }


        //[JsonPropertyName("imgSrc")]
        public string ImgSrc { get; set; }


        //[JsonPropertyName("title")]
        public string Title { get; set; }

        //[JsonPropertyName("about_1")]
        public string About_1 { get; set; }

        //[JsonPropertyName("about_2")]
        public string About_2 { get; set; }

        //[JsonPropertyName("CalculateSize")]
        public string Price { get; set; }
        public Product() { }

        public Product(int id, string category, string imgSrc, string title, string about_1, string about_2, string price)
        {
            Id = id;
            Category = category;
            ImgSrc = imgSrc;
            Title = title;
            About_1 = about_1;
            About_2 = about_2;
            Price = price;
        }
    }
}
