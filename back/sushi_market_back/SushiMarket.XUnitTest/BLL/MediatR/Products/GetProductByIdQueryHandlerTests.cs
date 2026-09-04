using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.MediatR.Products.GetProductById;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
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

            _handler = new GetProductByIdQueryHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenProductExists_ShouldReturnProductDto()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls"
            };

            _context.Categories.Add(category);

            var product = new Product
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
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var query = new GetProductByIdQuery(1);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductDto>();

            result.Id.Should().Be(1);
            result.TitleUa.Should().Be("Філадельфія");
            result.TitleEn.Should().Be("Philadelphia");
            result.DescriptionUa.Should().Be("Рол з лососем");
            result.DescriptionEn.Should().Be("Salmon roll");
            result.WeightOrVolume.Should().Be("250 г");
            result.Price.Should().Be(250m);
            result.ImgSrc.Should().Be("philadelphia.png");
            result.SortOrder.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var query = new GetProductByIdQuery(999);

            // Act
            Func<Task> act = () => _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
        }
    }
}