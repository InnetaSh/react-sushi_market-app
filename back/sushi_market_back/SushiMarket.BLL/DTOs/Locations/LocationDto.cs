namespace SushiMarket.BLL.DTOs.Locations
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string TitleKeyUa { get; set; } = string.Empty;
        public string TitleKeyEn { get; set; } = string.Empty;
        public string CityKeyUa { get; set; } = string.Empty;
        public string CityKeyEn { get; set; } = string.Empty;
        public string AddressKeyUa { get; set; } = string.Empty;
        public string AddressKeyEn { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Hours { get; set; } = string.Empty;
        public string ImageSrc { get; set; } = string.Empty;
    }
}
