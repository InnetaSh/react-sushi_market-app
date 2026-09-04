using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.BLL.Seeders;

public static class PromotionSeeder
{
    public static async Task SeedAsync(SushiMarketDbContext context)
    {
        if (await context.Promotions.AnyAsync())
            return;

        var promotions = new List<Promotion>
        {
        new Promotion
            {
                ImageUrl = "img/promotion/hours.png",
                DateKeyUa = "по буднях з 12:00 до 16:00",
                DateKeyEn = "Weekdays from 12:00 to 16:00",
                TitleKeyUa = "Щасливі години",
                TitleKeyEn = "Happy Hours",
                DescriptionKeyUa = "Знижка 20% на все меню при замовленні на самовинос у денний час",
                DescriptionKeyEn = "20% discount on the entire menu for pickup orders during daytime",
                Link = "/sale/happy-hours"
            },
        new Promotion
            {
                ImageUrl = "img/promotion/birthday.png",
                DateKeyUa = "діє у ваш День народження",
                DateKeyEn = "Valid on your Birthday",
                TitleKeyUa = "Іменинникам знижка",
                TitleKeyEn = "Birthday Discount",
                DescriptionKeyUa = "Святкуйте разом із нами! Знижка 15% на замовлення за 3 дні до та 3 дні після свята",
                DescriptionKeyEn = "Celebrate with us! 15% discount on orders 3 days before and 3 days after your special day",
                Link = "/sale/birthday"
            }
        };

        await context.Promotions.AddRangeAsync(promotions);
        await context.SaveChangesAsync();
    }
}