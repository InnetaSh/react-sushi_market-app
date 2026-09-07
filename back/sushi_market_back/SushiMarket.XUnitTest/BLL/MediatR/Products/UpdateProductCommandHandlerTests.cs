using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<UpdateProductCommandHandler>> _loggerMock;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProductCommand, Product>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _translator = new TranslatorHelper.Translator(configuration);
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<UpdateProductCommandHandler>>();

            _handler = new UpdateProductCommandHandler(
                _context,
                _mapper,
                _translator,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProductExists_ShouldUpdateProductAndReturnUnit()
        {
            var product = new Product
            {
                Id = 1,
                TitleUa = "Стара Філадельфія",
                TitleEn = "Old Philadelphia",
                DescriptionUa = "Старий опис",
                DescriptionEn = "Old description",
                WeightOrVolume = "200 г",
                Price = 200m,
                ImgSrc = "old.png",
                SortOrder = 1,
                CategoryId = 1
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var command = new UpdateProductCommand(
                Id: 1,
                TitleUa: "Нова Філадельфія",
                TitleEn: "New Philadelphia",
                DescriptionUa: "Новий опис",
                DescriptionEn: "New description",
                WeightOrVolume: "250 г",
                Price: 250m,
                Image: null,
                SortOrder: 2,
                CategoryId: 1
            );

            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            result.Should().Be(Unit.Value);

            var updatedProduct = await _context.Products
                .FindAsync(1);

            updatedProduct.Should().NotBeNull();

            updatedProduct!.TitleUa.Should().Be("Нова Філадельфія");
            updatedProduct.TitleEn.Should().Be("New Philadelphia");
            updatedProduct.DescriptionUa.Should().Be("Новий опис");
            updatedProduct.DescriptionEn.Should().Be("New description");
            updatedProduct.WeightOrVolume.Should().Be("250 г");
            updatedProduct.Price.Should().Be(250m);
            updatedProduct.ImgSrc.Should().Be("old.png");
            updatedProduct.SortOrder.Should().Be(2);
            updatedProduct.CategoryId.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var command = new UpdateProductCommand(
                Id: 999,
                TitleUa: "Тест",
                TitleEn: "Test",
                DescriptionUa: "Опис",
                DescriptionEn: "Description",
                WeightOrVolume: "100 г",
                Price: 100m,
                Image: null,
                SortOrder: 1,
                CategoryId: 1
            );

            Func<Task> act = () => _handler.Handle(
                command,
                CancellationToken.None);

            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
        }

        [Fact]
        public async Task Handle_WhenBothLanguagesAreProvided_ShouldNotTranslateAndShouldUpdateProduct()
        {
            var product = new Product
            {
                Id = 1,
                TitleUa = "Філадельфія",
                TitleEn = "Philadelphia",
                DescriptionUa = "Рол з лососем",
                DescriptionEn = "Salmon roll",
                WeightOrVolume = "250 г",
                Price = 250m,
                ImgSrc = "old.png",
                SortOrder = 1,
                CategoryId = 1
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var command = new UpdateProductCommand(
                Id: 1,
                TitleUa: "Каліфорнія",
                TitleEn: "California",
                DescriptionUa: "Рол з крабом",
                DescriptionEn: "Crab roll",
                WeightOrVolume: "300 г",
                Price: 300m,
                Image: null,
                SortOrder: 2,
                CategoryId: 1
            );

            await _handler.Handle(
                command,
                CancellationToken.None);

            var updatedProduct = await _context.Products
                .FindAsync(1);

            updatedProduct.Should().NotBeNull();

            updatedProduct!.TitleUa.Should().Be("Каліфорнія");
            updatedProduct.TitleEn.Should().Be("California");
            updatedProduct.DescriptionUa.Should().Be("Рол з крабом");
            updatedProduct.DescriptionEn.Should().Be("Crab roll");
            updatedProduct.WeightOrVolume.Should().Be("300 г");
            updatedProduct.Price.Should().Be(300m);
            updatedProduct.ImgSrc.Should().Be("old.png");
            updatedProduct.SortOrder.Should().Be(2);
        }
    }
}