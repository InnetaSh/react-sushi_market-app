using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class CreateProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<CreateProductCommandHandler>> _loggerMock;
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

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _translator = new TranslatorHelper.Translator(configuration);
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<CreateProductCommandHandler>>();

            _handler = new CreateProductCommandHandler(
                _context,
                _mapper,
                _translator,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldCreateProductAndReturnId()
        {
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
                Image: null,
                SortOrder: 1,
                CategoryId: 1
            );

            var result = await _handler.Handle(
                command,
                CancellationToken.None);

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
            product.ImgSrc.Should().BeEmpty();
            product.SortOrder.Should().Be(1);
            product.CategoryId.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenValidCommand_ShouldAddProductToDatabase()
        {
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
                Image: null,
                SortOrder: 2,
                CategoryId: 1
            );

            var productId = await _handler.Handle(
                command,
                CancellationToken.None);

            var productsCount = await _context.Products.CountAsync();

            productsCount.Should().Be(1);

            var product = await _context.Products
                .FindAsync(productId);

            product.Should().NotBeNull();
            product!.CategoryId.Should().Be(1);
        }
    }
}