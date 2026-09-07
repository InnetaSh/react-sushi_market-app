using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.BLL.MediatR.Locations.GetLocations;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.Location;

namespace SushiMarket.Tests.MediatR.Locations
{
    public class GetLocationListQueryHandlerTests : IDisposable
    {
        private readonly SushiMarketDbContext _context;
        private readonly AutoMapper.IMapper _mapper;
        private readonly Mock<ILogger<GetLocationListQueryHandler>> _loggerMock;
        private readonly GetLocationListQueryHandler _handler;

        public GetLocationListQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _loggerMock = new Mock<ILogger<GetLocationListQueryHandler>>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Location, LocationDto>();
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();

            _handler = new GetLocationListQueryHandler(_context, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenLocationsExist_ReturnsLocationList()
        {
            _context.Locations.AddRange(
                new Location
                {
                    Id = 1,
                    Slug = "kyiv-1",
                    TitleKeyUa = "Центр",
                    TitleKeyEn = "Center",
                    CityKeyUa = "Київ",
                    CityKeyEn = "Kyiv",
                    AddressKeyUa = "Хрещатик 1",
                    AddressKeyEn = "Khreshchatyk 1",
                    Phone = "+380000000001",
                    Lat = 50.45,
                    Lng = 30.52,
                    Hours = "10:00 - 22:00",
                    ImageSrc = "img1.jpg"
                },
                new Location
                {
                    Id = 2,
                    Slug = "lviv-1",
                    TitleKeyUa = "Ремо",
                    TitleKeyEn = "Remo",
                    CityKeyUa = "Львів",
                    CityKeyEn = "Lviv",
                    AddressKeyUa = "Свободи 5",
                    AddressKeyEn = "Svobody 5",
                    Phone = "+380000000002",
                    Lat = 49.84,
                    Lng = 24.02,
                    Hours = "10:00 - 22:00",
                    ImageSrc = "img2.jpg"
                }
            );
            await _context.SaveChangesAsync();

            var query = new GetLocationListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(dto => dto.Slug == "kyiv-1" && dto.CityKeyEn == "Kyiv");
            result.Should().Contain(dto => dto.Slug == "lviv-1" && dto.CityKeyEn == "Lviv");
        }

        [Fact]
        public async Task Handle_WhenNoLocationsExist_ReturnsEmptyList()
        {
            var query = new GetLocationListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}