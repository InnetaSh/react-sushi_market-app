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
                new Category { Id = 1, TitleUa = "САШИМІ", TitleEn = "SASHIMI", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788606277/SASHIMI.png", SortOrder = 1 },
                new Category { Id = 2, TitleUa = "СУШІ", TitleEn = "SUSHI", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788606284/SUSHI.png", SortOrder = 2 },
                new Category { Id = 3, TitleUa = "РОЛИ", TitleEn = "ROLLS", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788606084/ROLLS.png", SortOrder = 3 },
                new Category { Id = 4, TitleUa = "НАБОРИ", TitleEn = "SETS", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788606282/SETS.png", SortOrder = 4 },
                new Category { Id = 5, TitleUa = "САЛАТИ", TitleEn = "SALADS", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788606218/SALADS.png", SortOrder = 5 },
                new Category { Id = 6, TitleUa = "ГАРЯЧІ БЛЮДА", TitleEn = "HOT DISHES", ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/f_auto,q_auto/HOT_DISHES", SortOrder = 6 }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product { CategoryId = 1, TitleUa = "Сашімі з лососем", TitleEn = "Salmon Sashimi", DescriptionUa = "Свіжий охолоджений лосось преміум-якості", DescriptionEn = "Fresh premium chilled salmon", WeightOrVolume = "200 г", Price = 340.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627255/sashimi-salmon.webp", SortOrder = 1 },
                    new Product { CategoryId = 1, TitleUa = "Сашімі з тунцем", TitleEn = "Tuna Sashimi", DescriptionUa = "Ніжне філе тунца блуфін", DescriptionEn = "Delicate bluefin tuna fillet", WeightOrVolume = "180 г", Price = 390.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627256/sashimi-tuna.webp", SortOrder = 2 },
                    new Product { CategoryId = 1, TitleUa = "Сашімі асорті", TitleEn = "Sashimi Assorted", DescriptionUa = "Лосось, тунець, вугор та гребінець", DescriptionEn = "Salmon, tuna, eel and scallop", WeightOrVolume = "280 г", Price = 520.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627253/sashimi-assorted.webp", SortOrder = 3 },

                    new Product { CategoryId = 2, TitleUa = "Суші з лососем", TitleEn = "Salmon Nigiri", DescriptionUa = "Рис, шматочок свіжого лосося", DescriptionEn = "Rice, slice of fresh salmon", WeightOrVolume = "50 г", Price = 75.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627085/nigiri-salmon.webp", SortOrder = 4 },
                    new Product { CategoryId = 2, TitleUa = "Суші з вугром", TitleEn = "Eel Nigiri", DescriptionUa = "Вугор, соус унагі, кунжут", DescriptionEn = "Eel, unagi sauce, sesame", WeightOrVolume = "50 г", Price = 95.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627084/nigiri-eel.webp", SortOrder = 5 },
                    new Product { CategoryId = 2, TitleUa = "Суші з тигровою креветкою", TitleEn = "Shrimp Nigiri", DescriptionUa = "Відварна тигрова креветка, рис", DescriptionEn = "Boiled tiger shrimp, rice", WeightOrVolume = "45 г", Price = 80.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627087/nigiri-shrimp.webp", SortOrder = 6 },

                    new Product { CategoryId = 3, TitleUa = "Філадельфія класична", TitleEn = "Philadelphia Classic", DescriptionUa = "Лосось, вершковий сир крем-чіз, огірок, рис, норі", DescriptionEn = "Salmon, cream cheese, cucumber, rice, nori", WeightOrVolume = "280 г", Price = 310.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627174/roll-philadelphia.webp", SortOrder = 7 },
                    new Product { CategoryId = 3, TitleUa = "Каліфорнія з крабом", TitleEn = "California with Crab", DescriptionUa = "Сніжний краб, авокадо, огірок, ікра масаго", DescriptionEn = "Snow crab, avocado, cucumber, masago caviar", WeightOrVolume = "260 г", Price = 280.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627172/roll-california.webp", SortOrder = 8 },
                    new Product { CategoryId = 3, TitleUa = "Дракон чорний", TitleEn = "Black Dragon", DescriptionUa = "Вугор, копчений лосось, авокадо, соус унагі", DescriptionEn = "Eel, smoked salmon, avocado, unagi sauce", WeightOrVolume = "300 г", Price = 360.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627088/roll-black-dragon.webp", SortOrder = 9 },

                    new Product { CategoryId = 4, TitleUa = "Сет Філадельфія Максі", TitleEn = "Philadelphia Maxi", DescriptionUa = "Філадельфія з лососем, з тунцем, з вугром (24 шт)", DescriptionEn = "Philadelphia with salmon, tuna, eel (24 pcs)", WeightOrVolume = "850 г", Price = 920.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627257/set-philadelphia-maxi.webp", SortOrder = 101 },
                    new Product { CategoryId = 4, TitleUa = "Сет Хіт сезону", TitleEn = "Season Hit", DescriptionUa = "Дракон, Каліфорнія, макі з огірком (24 шт)", DescriptionEn = "Dragon, California, cucumber maki (24 pcs)", WeightOrVolume = "780 г", Price = 790.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627260/set-season-hit.webp", SortOrder = 11 },
                    new Product { CategoryId = 4, TitleUa = "Самурай сет", TitleEn = "Samurai Set", DescriptionUa = "Великий набір із запечених та свіжих ролів (32 шт)", DescriptionEn = "Large set of baked and fresh rolls (32 pcs)", WeightOrVolume = "1100 г", Price = 1250.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627259/set-samurai.webp", SortOrder = 12 },

                    new Product { CategoryId = 5, TitleUa = "Салат Чука з горіховим соусом", TitleEn = "Chuka Salad with Nut Sauce", DescriptionUa = "Водорості чука, горіховий соус, лимон, кунжут", DescriptionEn = "Chuka seaweed, nut sauce, lemon, sesame", WeightOrVolume = "180 г", Price = 160.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627177/salad-chuka.webp", SortOrder = 13 },
                    new Product { CategoryId = 5, TitleUa = "Салат Цезар з креветками", TitleEn = "Caesar with Shrimp", DescriptionUa = "Тигрові креветки, мікс салатів, соус цезар, пармезан", DescriptionEn = "Tiger shrimps, salad mix, Caesar sauce, parmesan", WeightOrVolume = "220 г", Price = 290.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627176/salad-caesar-shrimp.webp", SortOrder = 14 },
                    new Product { CategoryId = 5, TitleUa = "Салат з морепродуктами", TitleEn = "Seafood Salad", DescriptionUa = "Мікс морепродуктів, свіжі овочі, заправка від шефа", DescriptionEn = "Seafood mix, fresh vegetables, chef dressing", WeightOrVolume = "200 г", Price = 310.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627179/salad-seafood.webp", SortOrder = 15 },

                    new Product { CategoryId = 6, TitleUa = "Місо суп з лососем", TitleEn = "Salmon Miso Soup", DescriptionUa = "Традиційний бульйон місо, тофу, вакаме, лосось", DescriptionEn = "Traditional miso broth, tofu, wakame, salmon", WeightOrVolume = "300 г", Price = 140.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627006/hot-miso-salmon.webp", SortOrder = 16 },
                    new Product { CategoryId = 6, TitleUa = "Рамен з курячими стріпсами", TitleEn = "Chicken Ramen", DescriptionUa = "Локшина рамен, бульйон, курка, яйце пашот, норі", DescriptionEn = "Ramen noodles, broth, chicken, poach egg, nori", WeightOrVolume = "400 г", Price = 230.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627003/hot-chicken-ramen.webp", SortOrder = 17 },
                    new Product { CategoryId = 6, TitleUa = "Удон з морепродуктами", TitleEn = "Seafood Udon", DescriptionUa = "Локшина удон, тигрові креветки, кальмар, овочі в соусі теріякі", DescriptionEn = "Udon noodles, tiger prawns, squid, vegetables in teriyaki sauce", WeightOrVolume = "350 г", Price = 295.00m, ImgSrc = "https://res.cloudinary.com/qmiijcm1/image/upload/v1788627010/hot-seafood-udon.webp", SortOrder = 18 }
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}