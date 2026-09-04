using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class GetCategoriesWithProductsQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetCategoriesWithProductsQueryHandler _handler;

        public GetCategoriesWithProductsQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryWithProductsDto>();

                cfg.CreateMap<Product, ProductDto>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            _handler = new GetCategoriesWithProductsQueryHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenCategoriesWithProductsExist_ShouldReturnOrderedList()
        {
            // Arrange
            _context.Categories.AddRange(
                new Category
                {
                    Id = 1,
                    TitleUa = "Сети",
                    TitleEn = "Sets",
                    SortOrder = 2
                },
                new Category
                {
                    Id = 2,
                    TitleUa = "Роли",
                    TitleEn = "Rolls",
                    SortOrder = 1
                }
            );

            await _context.SaveChangesAsync();

            var query = new GetCategoriesWithProductsQuery();

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result[0].Id.Should().Be(2);
            result[1].Id.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenNoCategoriesExist_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetCategoriesWithProductsQuery();

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}