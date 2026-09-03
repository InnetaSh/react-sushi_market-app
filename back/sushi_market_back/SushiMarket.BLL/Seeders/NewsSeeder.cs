using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.NewsItem;

namespace SushiMarket.BLL.Seeders;

public static class NewsSeeder
{
    public static async Task SeedAsync(SushiMarketDbContext context)
    {
        if (await context.News.AnyAsync())
            return;

        var newsList = new List<NewsItem>
        {
            new NewsItem
            {
                Date = "19.11.2020",
                TitleKey = "NEWS_SECTION.ITEMS.FIRST.TITLE",
                DescriptionKey = "NEWS_SECTION.ITEMS.FIRST.DESCRIPTION",
                Link = "/news/1"
            },
            new NewsItem
            {
                Date = "25.10.2020",
                TitleKey = "NEWS_SECTION.ITEMS.SECOND.TITLE",
                DescriptionKey = "NEWS_SECTION.ITEMS.SECOND.DESCRIPTION",
                Link = "/news/2"
            }
        };

        await context.News.AddRangeAsync(newsList);
        await context.SaveChangesAsync();
    }
}