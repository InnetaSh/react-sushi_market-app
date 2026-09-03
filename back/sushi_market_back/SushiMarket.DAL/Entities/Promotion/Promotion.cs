namespace SushiMarket.DAL.Entities;

public class Promotion
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string DateKeyUa { get; set; } = string.Empty;
    public string DateKeyEn { get; set; } = string.Empty;
    public string TitleKeyUa { get; set; } = string.Empty;
    public string TitleKeyEn { get; set; } = string.Empty;
    public string DescriptionKeyUa { get; set; } = string.Empty;
    public string DescriptionKeyEn { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}