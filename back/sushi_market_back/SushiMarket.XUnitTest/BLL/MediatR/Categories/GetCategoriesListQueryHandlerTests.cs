using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.MediatR.Categories.GetCategoriesList;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class GetCategoriesListQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetCategoriesListQueryHandler>> _loggerMock;
        private readonly GetCategoriesListQueryHandler _handler;

        public GetCategoriesListQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
            }, NullLoggerFactory.Instance);

            _mapper = configuration.CreateMapper();
            _loggerMock = new Mock<ILogger<GetCategoriesListQueryHandler>>();

            _handler = new GetCategoriesListQueryHandler(_context, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCategoriesExist_ShouldReturnOrderedCategoriesList()
        {
            _context.Categories.AddRange(
                new Category { Id = 1, TitleUa = "Сети", TitleEn = "Sets", SortOrder = 2 },
                new Category { Id = 2, TitleUa = "Роли", TitleEn = "Rolls", SortOrder = 1 },
                new Category { Id = 3, TitleUa = "Напої", TitleEn = "Drinks", SortOrder = null }
            );
            await _context.SaveChangesAsync();

            var query = new GetCategoriesListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            var list = result.ToList();

            list.Should().HaveCount(3);
            list[0].Id.Should().Be(2);
            list[1].Id.Should().Be(1);
            list[2].Id.Should().Be(3);
        }

        [Fact]
        public async Task Handle_WhenNoCategoriesExist_ShouldReturnEmptyList()
        {
            var query = new GetCategoriesListQuery();

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}