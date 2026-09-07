using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities.NewsItem;
using Xunit;

namespace SushiMarket.Tests.MediatR.News
{
    public class GetNewsListQueryHandlerTests : IDisposable
    {
        private readonly SushiMarketDbContext _context;
        private readonly AutoMapper.IMapper _mapper;
        private readonly Mock<ILogger<GetNewsListQueryHandler>> _loggerMock;
        private readonly GetNewsListQueryHandler _handler;

        public GetNewsListQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _loggerMock = new Mock<ILogger<GetNewsListQueryHandler>>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewsItem, NewsItemDto>();
            }, NullLoggerFactory.Instance);
            _mapper = config.CreateMapper();

            _handler = new GetNewsListQueryHandler(_context, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenNewsItemsExist_ReturnsNewsItemList()
        {
            _context.News.AddRange(
                new NewsItem
                {
                    Id = 1,
                    Date = "2026-09-07",
                    TitleKeyUa = "Новина 1",
                    TitleKeyEn = "News 1",
                    DescriptionKeyUa = "Опис новини 1",
                    DescriptionKeyEn = "News description 1",
                    Link = "/news/1"
                },
                new NewsItem
                {
                    Id = 2,
                    Date = "2026-09-08",
                    TitleKeyUa = "Новина 2",
                    TitleKeyEn = "News 2",
                    DescriptionKeyUa = "Опис новини 2",
                    DescriptionKeyEn = "News description 2",
                    Link = "/news/2"
                }
            );
            await _context.SaveChangesAsync();

            var query = new GetNewsListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(dto => dto.Id == 1 && dto.TitleKeyEn == "News 1");
            result.Should().Contain(dto => dto.Id == 2 && dto.TitleKeyEn == "News 2");
        }

        [Fact]
        public async Task Handle_WhenNoNewsItemsExist_ReturnsEmptyList()
        {
            var query = new GetNewsListQuery();

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