namespace SushiMarket.DAL.Entities;

public class Promotion
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string DateKey { get; set; } = string.Empty;
    public string TitleKey { get; set; } = string.Empty;
    public string DescriptionKey { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}