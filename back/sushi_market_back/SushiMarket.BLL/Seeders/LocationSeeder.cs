using Microsoft.EntityFrameworkCore;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.Location;

namespace SushiMarket.BLL.Seeders;

public static class LocationSeeder
{
    public static async Task SeedAsync(SushiMarketDbContext context)
    {
        if (await context.Locations.AnyAsync())
            return;

        var locations = new List<Location>
        {
            new Location
            {
                Id = 1,
                Slug = "kyiv-zhylianska",
                TitleKeyUa = "Осама Суши у Києві (Центр)",
                TitleKeyEn = "Osama Sushi in Kyiv (Center)",
                CityKeyUa = "Київ",
                CityKeyEn = "Kyiv",
                AddressKeyUa = "вул. Жилянська, 59",
                AddressKeyEn = "Zhylianska St, 59",
                Phone = "+38 (068) 080-00-01",
                Lat = 50.4398,
                Lng = 30.5055,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 2,
                Slug = "kyiv-mayakovskoho",
                TitleKeyUa = "Осама Суши у Києві (Троєщина)",
                TitleKeyEn = "Osama Sushi in Kyiv (Troieschyna)",
                CityKeyUa = "Київ",
                CityKeyEn = "Kyiv",
                AddressKeyUa = "просп. Володимира Маяковского, 26",
                AddressKeyEn = "Volodymyra Maiakovskoho Ave, 26",
                Phone = "+38 (068) 080-00-02",
                Lat = 50.5085,
                Lng = 30.6087,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 3,
                Slug = "lviv-shevchenka",
                TitleKeyUa = "Осама Суши у Львові (Шевченка)",
                TitleKeyEn = "Osama Sushi in Lviv (Shevchenka)",
                CityKeyUa = "Львів",
                CityKeyEn = "Lviv",
                AddressKeyUa = "вул. Тараса Шевченка, 31",
                AddressKeyEn = "Tarasa Shevchenka St, 31",
                Phone = "+38 (068) 080-00-03",
                Lat = 49.8451,
                Lng = 24.0152,
                Hours = "11:00 - 22:00",
                ImageSrc = "img/city/Lviv.avif"
            },
            new Location
            {
                Id = 4,
                Slug = "lviv-stryiska",
                TitleKeyUa = "Осама Суши у Львові (Стрийська)",
                TitleKeyEn = "Osama Sushi in Lviv (Stryiska)",
                CityKeyUa = "Львів",
                CityKeyEn = "Lviv",
                AddressKeyUa = "вул. Стрийська, 45",
                AddressKeyEn = "Stryiska St, 45",
                Phone = "+38 (068) 080-00-04",
                Lat = 49.8190,
                Lng = 24.0173,
                Hours = "11:00 - 22:00",
                ImageSrc = "img/city/Lviv.avif"
            },
            new Location
            {
                Id = 5,
                Slug = "uzhgorod-koriatho",
                TitleKeyUa = "Осама Суши в Ужгороді",
                TitleKeyEn = "Osama Sushi in Uzhhorod",
                CityKeyUa = "Ужгород",
                CityKeyEn = "Uzhhorod",
                AddressKeyUa = "площа Корятовича, 20",
                AddressKeyEn = "Koriatovych Square, 20",
                Phone = "+38 (068) 080-00-05",
                Lat = 48.6210,
                Lng = 22.2980,
                Hours = "10:00 - 21:00",
                ImageSrc = "img/city/Bukovel.avif"
            },
            new Location
            {
                Id = 6,
                Slug = "zhytomyr-kyivska",
                TitleKeyUa = "Осама Суши у Житомирі",
                TitleKeyEn = "Osama Sushi in Zhytomyr",
                CityKeyUa = "Житомир",
                CityKeyEn = "Zhytomyr",
                AddressKeyUa = "вул. Київська, 77",
                AddressKeyEn = "Kyivska St, 77",
                Phone = "+38 (068) 080-00-06",
                Lat = 50.2547,
                Lng = 28.6745,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 7,
                Slug = "vinnytsia-soborna",
                TitleKeyUa = "Осама Суши у Вінниці",
                TitleKeyEn = "Osama Sushi in Vinnytsia",
                CityKeyUa = "Вінниця",
                CityKeyEn = "Vinnytsia",
                AddressKeyUa = "вул. Соборна, 52",
                AddressKeyEn = "Soborna St, 52",
                Phone = "+38 (068) 080-00-07",
                Lat = 49.2331,
                Lng = 28.4682,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 8,
                Slug = "rivne-soborna",
                TitleKeyUa = "Осама Суши у Рівному",
                TitleKeyEn = "Osama Sushi in Rivne",
                CityKeyUa = "Рівне",
                CityKeyEn = "Rivne",
                AddressKeyUa = "вул. Соборна, 17",
                AddressKeyEn = "Soborna St, 17",
                Phone = "+38 (068) 080-00-08",
                Lat = 50.6199,
                Lng = 26.2516,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 9,
                Slug = "Khmelnytskyi-proskurivska",
                TitleKeyUa = "Осама Суши у Хмельницькому",
                TitleKeyEn = "Osama Sushi in Khmelnytskyi",
                CityKeyUa = "Хмельницький",
                CityKeyEn = "Khmelnytskyi",
                AddressKeyUa = "вул. Проскурівська, 16",
                AddressKeyEn = "Proskurivska St, 16",
                Phone = "+38 (068) 080-00-09",
                Lat = 49.4851,
                Lng = 26.9871,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Kyiv.avif"
            },
            new Location
            {
                Id = 10,
                Slug = "chernivtsi-kobylianskoi",
                TitleKeyUa = "Осама Суши у Чернівцях",
                TitleKeyEn = "Osama Sushi in Chernivtsi",
                CityKeyUa = "Чернівці",
                CityKeyEn = "Chernivtsi",
                AddressKeyUa = "вул. Ольги Кобилянської, 12",
                AddressKeyEn = "Olha Kobylianska St, 12",
                Phone = "+38 (068) 080-00-10",
                Lat = 48.2921,
                Lng = 25.9358,
                Hours = "10:00 - 22:00",
                ImageSrc = "img/city/Lviv.avif"
            }
        };

        await context.Locations.AddRangeAsync(locations);
        await context.SaveChangesAsync();
    }
}