namespace SushiMarket.BLL.DTOs;

public class NewsItemDto
{
    public int Id { get; set; }
    public string Date { get; set; } = string.Empty;
    public string TitleKey { get; set; } = string.Empty;
    public string DescriptionKey { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}