namespace sushi_market_back.Models
{
    public class Category
    {
            //[JsonPropertyName("id")]
            public int Id { get; set; }

            //[JsonPropertyName("imgSrc")]
            public string ImgSrc { get; set; }

            //[JsonPropertyName("count")]
            public string Count { get; set; }

            //[JsonPropertyName("title")]
            public string Title { get; set; }

            public Category() { }

            public Category(int id, string imgSrc, string count, string title)
            {
                Id = id;
                ImgSrc = imgSrc;
                Count = count;
                Title = title;
            }
        
    }
}
