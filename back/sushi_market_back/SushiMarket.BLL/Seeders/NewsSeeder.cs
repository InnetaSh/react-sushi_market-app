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
                Date = "01.03.2026",
                TitleKeyUa = "Весняне оновлення меню: нові смаки та фірмові роли",
                TitleKeyEn = "Spring menu update: new flavors and signature rolls",
                DescriptionKeyUa = "Зустрічайте весну разом із нашими новинками! Ми додали до меню ексклюзивні роли з унікальними поєднаннями інгредієнтів та оновили лінійку напоїв.",
                DescriptionKeyEn = "Welcome spring with our new items! We have added exclusive rolls with unique ingredient combinations to the menu and updated our drink selection.",
                Link = "/news/1"
            },
            new NewsItem
            {
                Date = "15.04.2026",
                TitleKeyUa = "Запущено нову програму лояльності",
                TitleKeyEn = "New loyalty program launched",
                DescriptionKeyUa = "Тепер замовляти улюблені суші ще вигідніше. Накопичуйте бали з кожної покупки та сплачуйте ними наступні замовлення в один клік.",
                DescriptionKeyEn = "Now ordering your favorite sushi is even more profitable. Earn points with every purchase and use them to pay for future orders in one click.",
                Link = "/news/2"
            },
            new NewsItem
            {
                Date = "10.06.2026",
                TitleKeyUa = "Безкоштовна доставка для великих компаній",
                TitleKeyEn = "Free delivery for large groups",
                DescriptionKeyUa = "Плануєте вечірку чи зустріч із друзями? Робіть замовлення на суму від 1000 грн і отримуйте швидку та безкоштовну доставку до ваших дверей.",
                DescriptionKeyEn = "Planning a party or meeting with friends? Order for 1000 UAH or more and get fast, free delivery right to your door.",
                Link = "/news/3"
            },
            new NewsItem
            {
                Date = "01.09.2026",
                TitleKeyUa = "Осінній сет тижня зі знижкою 20%",
                TitleKeyEn = "Autumn set of the week with a 20% discount",
                DescriptionKeyUa = "Спеціальна пропозиція нового сезону! Спробуйте наш оновлений мікс ролів за спеціальною ціною протягом усього тижня.",
                DescriptionKeyEn = "Special offer for the new season! Try our updated roll mix at a special price throughout the entire week.",
                Link = "/news/4"
            }
        };

        await context.News.AddRangeAsync(newsList);
        await context.SaveChangesAsync();
    }
}