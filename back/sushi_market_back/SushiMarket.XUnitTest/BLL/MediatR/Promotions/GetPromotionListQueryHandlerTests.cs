using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.Promotions;
using SushiMarket.BLL.MediatR.Promotions.GetPromotions;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Promotions
{
    public class GetPromotionListQueryHandlerTests : IDisposable
    {
        private readonly SushiMarketDbContext _context;
        private readonly AutoMapper.IMapper _mapper;
        private readonly Mock<ILogger<GetLocationListQueryHandler>> _loggerMock;
        private readonly GetLocationListQueryHandler _handler;

        public GetPromotionListQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _loggerMock = new Mock<ILogger<GetLocationListQueryHandler>>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Promotion, PromotionDto>();
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();

            _handler = new GetLocationListQueryHandler(_context, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenPromotionsExist_ReturnsPromotionList()
        {
            _context.Promotions.AddRange(
                new Promotion
                {
                    Id = 1,
                    ImageUrl = "promo1.jpg",
                    DateKeyUa = "Діє до 10.09",
                    DateKeyEn = "Valid until Sep 10",
                    TitleKeyUa = "Знижка 20%",
                    TitleKeyEn = "20% Off",
                    DescriptionKeyUa = "Опис акції українською",
                    DescriptionKeyEn = "Promotion description in English",
                    Link = "/promo/1"
                },
                new Promotion
                {
                    Id = 2,
                    ImageUrl = "promo2.jpg",
                    DateKeyUa = "Діє до 15.09",
                    DateKeyEn = "Valid until Sep 15",
                    TitleKeyUa = "Безкоштовна доставка",
                    TitleKeyEn = "Free Delivery",
                    DescriptionKeyUa = "Ще один опис",
                    DescriptionKeyEn = "Another description",
                    Link = "/promo/2"
                }
            );
            await _context.SaveChangesAsync();

            var query = new GetPromotionListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(dto => dto.Id == 1 && dto.TitleKeyEn == "20% Off");
            result.Should().Contain(dto => dto.Id == 2 && dto.TitleKeyEn == "Free Delivery");
        }

        [Fact]
        public async Task Handle_WhenNoPromotionsExist_ReturnsEmptyList()
        {
            var query = new GetPromotionListQuery();

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