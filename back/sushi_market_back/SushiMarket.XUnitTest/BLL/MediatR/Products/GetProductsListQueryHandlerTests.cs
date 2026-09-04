using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.MediatR.Products.GetProductsList;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class GetProductsListQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetProductsListQueryHandler _handler;

        public GetProductsListQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            _handler = new GetProductsListQueryHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenCategoryIdIsSpecified_ShouldReturnProductsFromCategory()
        {
            // Arrange
            var category1 = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls"
            };

            var category2 = new Category
            {
                Id = 2,
                TitleUa = "Суши",
                TitleEn = "Sushi"
            };

            _context.Categories.AddRange(category1, category2);

            var products = new[]
            {
                new Product
                {
                    Id = 1,
                    TitleUa = "Філадельфія",
                    TitleEn = "Philadelphia",
                    DescriptionUa = "Рол з лососем",
                    DescriptionEn = "Salmon roll",
                    WeightOrVolume = "250 г",
                    Price = 250m,
                    ImgSrc = "philadelphia.png",
                    SortOrder = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    TitleUa = "Каліфорнія",
                    TitleEn = "California",
                    DescriptionUa = "Рол з крабом",
                    DescriptionEn = "Crab roll",
                    WeightOrVolume = "250 г",
                    Price = 220m,
                    ImgSrc = "california.png",
                    SortOrder = 2,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    TitleUa = "Сяке",
                    TitleEn = "Sake",
                    DescriptionUa = "Суші з лососем",
                    DescriptionEn = "Salmon sushi",
                    WeightOrVolume = "100 г",
                    Price = 180m,
                    ImgSrc = "sake.png",
                    SortOrder = 1,
                    CategoryId = 2
                }
            };

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            var query = new GetProductsListQuery(CategoryId: 1);

            // Act
            var result = (await _handler.Handle(
                query,
                CancellationToken.None)).ToList();

            // Assert
            result.Should().HaveCount(2);

            result.Should().OnlyContain(p =>
                p.Id == 1 || p.Id == 2);

            result[0].Id.Should().Be(1);
            result[1].Id.Should().Be(2);
        }

        [Fact]
        public async Task Handle_WhenCategoryIdIsNull_ShouldReturnAllProducts()
        {
            // Arrange
            var category1 = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls"
            };

            var category2 = new Category
            {
                Id = 2,
                TitleUa = "Суши",
                TitleEn = "Sushi"
            };

            _context.Categories.AddRange(category1, category2);

            _context.Products.AddRange(
                new Product
                {
                    Id = 1,
                    TitleUa = "Філадельфія",
                    TitleEn = "Philadelphia",
                    DescriptionUa = "Рол з лососем",
                    DescriptionEn = "Salmon roll",
                    WeightOrVolume = "250 г",
                    Price = 250m,
                    ImgSrc = "philadelphia.png",
                    SortOrder = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    TitleUa = "Каліфорнія",
                    TitleEn = "California",
                    DescriptionUa = "Рол з крабом",
                    DescriptionEn = "Crab roll",
                    WeightOrVolume = "250 г",
                    Price = 220m,
                    ImgSrc = "california.png",
                    SortOrder = 2,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    TitleUa = "Сяке",
                    TitleEn = "Sake",
                    DescriptionUa = "Суші з лососем",
                    DescriptionEn = "Salmon sushi",
                    WeightOrVolume = "100 г",
                    Price = 180m,
                    ImgSrc = "sake.png",
                    SortOrder = 3,
                    CategoryId = 2
                });

            await _context.SaveChangesAsync();

            var query = new GetProductsListQuery(CategoryId: null);

            // Act
            var result = (await _handler.Handle(
                query,
                CancellationToken.None)).ToList();

            // Assert
            result.Should().HaveCount(3);

            result.Select(p => p.Id)
                .Should()
                .BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public async Task Handle_ShouldReturnProductsOrderedBySortOrder()
        {
            // Arrange
            _context.Products.AddRange(
                new Product
                {
                    Id = 1,
                    TitleUa = "Третій",
                    TitleEn = "Third",
                    DescriptionUa = "Description 1",
                    DescriptionEn = "Description 1",
                    WeightOrVolume = "200 г",
                    Price = 200m,
                    ImgSrc = "third.png",
                    SortOrder = 3,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    TitleUa = "Перший",
                    TitleEn = "First",
                    DescriptionUa = "Description 2",
                    DescriptionEn = "Description 2",
                    WeightOrVolume = "200 г",
                    Price = 150m,
                    ImgSrc = "first.png",
                    SortOrder = 1,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    TitleUa = "Без сортування",
                    TitleEn = "Without sort order",
                    DescriptionUa = "Description 3",
                    DescriptionEn = "Description 3",
                    WeightOrVolume = "200 г",
                    Price = 100m,
                    ImgSrc = "without-sort.png",
                    SortOrder = null,
                    CategoryId = 1
                });

            await _context.SaveChangesAsync();

            var query = new GetProductsListQuery(CategoryId: null);

            // Act
            var result = (await _handler.Handle(
                query,
                CancellationToken.None)).ToList();

            // Assert
            result.Should().HaveCount(3);

            result[0].Id.Should().Be(2);
            result[1].Id.Should().Be(1);
            result[2].Id.Should().Be(3);
        }

        [Fact]
        public async Task Handle_WhenNoProductsExist_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetProductsListQuery(CategoryId: null);

            // Act
            var result = (await _handler.Handle(
                query,
                CancellationToken.None)).ToList();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}