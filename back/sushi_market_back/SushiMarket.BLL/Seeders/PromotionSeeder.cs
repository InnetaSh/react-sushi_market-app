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
                DateKey = "PAGE_3_TEXT.SALE_1_DATE",
                TitleKey = "PAGE_3_TEXT.SALE_1_NAME",
                DescriptionKey = "PAGE_3_TEXT.SALE_1_DESC",
                Link = "/promotion/happy-hours"
            },
            new Promotion
            {
                ImageUrl = "img/promotion/birthday.png",
                DateKey = "PAGE_3_TEXT.SALE_2_DATE",
                TitleKey = "PAGE_3_TEXT.SALE_2_NAME",
                DescriptionKey = "PAGE_3_TEXT.SALE_2_DESC",
                Link = "/promotion/birthday"
            }
        };

        await context.Promotions.AddRangeAsync(promotions);
        await context.SaveChangesAsync();
    }
}