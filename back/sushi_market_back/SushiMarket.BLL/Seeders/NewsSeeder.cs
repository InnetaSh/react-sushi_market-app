using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;
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
                TitleKeyUa = "Нам 1 рік! Акції! Знижки!",
                TitleKeyEn = "We are 1 year old! Promotions! Discounts!",
                DescriptionKeyUa = "При створенні новини, крім заголовка та вмісту, Ви можете задати ще низку параметрів. Тут Ви бачите приклад заповнення анонсу новини.",
                DescriptionKeyEn = "When creating a news item, in addition to the title and content, you can set a number of other parameters. Here you see an example of filling out a news announcement.",
                Link = "/news/1"
            },
            new NewsItem
            {
                Date = "25.10.2020",
                TitleKeyUa = "Новорічні канікули",
                TitleKeyEn = "New Year holidays",
                DescriptionKeyUa = "При створенні новини, крім заголовка та вмісту, Ви можете задати ще низку параметрів. Тут Ви бачите приклад заповнення анонсу новини.",
                DescriptionKeyEn = "When creating a news item, in addition to the title and content, you can set a number of other parameters. Here you see an example of filling out a news announcement.",
                Link = "/news/2"
            }
        };

        await context.News.AddRangeAsync(newsList);
        await context.SaveChangesAsync();
    }
}