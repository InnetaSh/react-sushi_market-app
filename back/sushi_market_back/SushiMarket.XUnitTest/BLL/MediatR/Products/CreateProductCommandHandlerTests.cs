using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class CreateProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductCommand, Product>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            _handler = new CreateProductCommandHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldCreateProductAndReturnId()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls"
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new CreateProductCommand(
                TitleUa: "Філадельфія",
                TitleEn: "Philadelphia",
                DescriptionUa: "Рол з лососем та сиром",
                DescriptionEn: "Roll with salmon and cheese",
                WeightOrVolume: "250 г",
                Price: 250m,
                ImgSrc: "philadelphia.png",
                SortOrder: 1,
                CategoryId: 1
            );

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.Should().BeGreaterThan(0);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == result);

            product.Should().NotBeNull();
            product!.TitleUa.Should().Be("Філадельфія");
            product.TitleEn.Should().Be("Philadelphia");
            product.DescriptionUa.Should().Be("Рол з лососем та сиром");
            product.DescriptionEn.Should().Be("Roll with salmon and cheese");
            product.WeightOrVolume.Should().Be("250 г");
            product.Price.Should().Be(250m);
            product.ImgSrc.Should().Be("philadelphia.png");
            product.SortOrder.Should().Be(1);
            product.CategoryId.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldAddProductToDatabase()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls"
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new CreateProductCommand(
                TitleUa: "Каліфорнія",
                TitleEn: "California",
                DescriptionUa: "Рол з крабовим м'ясом",
                DescriptionEn: "Roll with crab meat",
                WeightOrVolume: "300 г",
                Price: 220m,
                ImgSrc: "california.png",
                SortOrder: 2,
                CategoryId: 1
            );

            // Act
            var productId = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            var productsCount = await _context.Products.CountAsync();

            productsCount.Should().Be(1);

            var product = await _context.Products
                .FindAsync(productId);

            product.Should().NotBeNull();
            product!.CategoryId.Should().Be(1);
        }
    }
}