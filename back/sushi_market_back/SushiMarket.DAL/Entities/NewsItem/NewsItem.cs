using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiMarket.DAL.Entities.NewsItem
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string TitleKeyUa { get; set; } = string.Empty;
        public string TitleKeyEn { get; set; } = string.Empty;
        public string DescriptionKeyUa { get; set; } = string.Empty;
        public string DescriptionKeyEn { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
