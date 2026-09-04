using Microsoft.AspNetCore.Http;

public class UpdateCategoryRequestDto
{
    public int Id { get; set; }
    public string? TitleUa { get; set; }
    public string? TitleEn { get; set; }
    public double? SortOrder { get; set; }
    public IFormFile? Image { get; set; }
}