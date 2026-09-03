namespace SushiMarket.BLL.DTOs;

public class NewsItemDto
{
    public int Id { get; set; }
    public string Date { get; set; } = string.Empty;
    public string TitleKeyUa { get; set; } = string.Empty;
    public string TitleKeyEn { get; set; } = string.Empty;
    public string DescriptionKeyUa { get; set; } = string.Empty;
    public string DescriptionKeyEn { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}