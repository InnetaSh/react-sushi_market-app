using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.BLL.Seeders
{
    public static class MenuSeeder
    {
        public static async Task SeedAsync(SushiMarketDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var categories = new List<Category>
            {
                new Category { Id = 1, TitleUa = "САШИМІ", TitleEn = "SASHIMI", ImgSrc = "img/menu_1.png", SortOrder = 1 },
                new Category { Id = 2, TitleUa = "СУШІ", TitleEn = "SUSHI", ImgSrc = "img/menu_2.png", SortOrder = 2 },
                new Category { Id = 3, TitleUa = "РОЛИ", TitleEn = "ROLLS", ImgSrc = "img/menu_3.png", SortOrder = 3 },
                new Category { Id = 4, TitleUa = "НАБОРИ", TitleEn = "SETS", ImgSrc = "img/menu_4.png", SortOrder = 4 },
                new Category { Id = 5, TitleUa = "САЛАТИ", TitleEn = "SALADS", ImgSrc = "img/menu_5.png", SortOrder = 5 },
                new Category { Id = 6, TitleUa = "ГАРЯЧІ БЛЮДА", TitleEn = "HOT DISHES", ImgSrc = "img/menu_6.png", SortOrder = 6 }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        CategoryId = 1,
                        TitleUa = "Сашимі",
                        TitleEn = "Sashimi",
                        DescriptionUa = "Сякэ блюдо от шеф-повара / З лососем",
                        DescriptionEn = "Chef's special sake dish / With salmon",
                        WeightOrVolume = "250 г",
                        Price = 260.00m,
                        ImgSrc = "img1.png",
                        SortOrder = 1
                    },
                    new Product
                    {
                        CategoryId = 2,
                        TitleUa = "Магуро",
                        TitleEn = "Maguro",
                        DescriptionUa = "Час приготування: 30 хвилин / З тунцем",
                        DescriptionEn = "Preparation time: 30 minutes / With tuna",
                        WeightOrVolume = "220 г",
                        Price = 295.00m,
                        ImgSrc = "img2.png",
                        SortOrder = 2
                    },
                    new Product
                    {
                        CategoryId = 1,
                        TitleUa = "Унагі",
                        TitleEn = "Unagi",
                        DescriptionUa = "Страва тижня / З вугром",
                        DescriptionEn = "Dish of the week / With eel",
                        WeightOrVolume = "240 г",
                        Price = 320.00m,
                        ImgSrc = "img3.png",
                        SortOrder = 3
                    },
                    new Product
                    {
                        CategoryId = 2,
                        TitleUa = "Хамачі",
                        TitleEn = "Hamachi",
                        DescriptionUa = "З жовтохвостом",
                        DescriptionEn = "With yellowtail",
                        WeightOrVolume = "200 г",
                        Price = 330.00m,
                        ImgSrc = "img4.png",
                        SortOrder = 4
                    }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}