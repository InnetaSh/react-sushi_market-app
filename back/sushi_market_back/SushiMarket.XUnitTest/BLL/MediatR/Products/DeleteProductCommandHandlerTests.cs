using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.MediatR.Products.DeleteProduct;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<DeleteProductCommandHandler>> _loggerMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<DeleteProductCommandHandler>>();

            _handler = new DeleteProductCommandHandler(
                _context,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProductExists_ShouldDeleteProductAndReturnUnit()
        {
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

            var command = new DeleteProductCommand(1);

            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            result.Should().Be(Unit.Value);

            var deletedProduct = await _context.Products
                .FindAsync(1);

            deletedProduct.Should().BeNull();
            _cloudinaryServiceMock.Verify(x => x.DeleteImageAsync("philadelphia.png"), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var command = new DeleteProductCommand(999);

            Func<Task> act = () => _handler.Handle(
                command,
                CancellationToken.None);

            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
            _cloudinaryServiceMock.Verify(x => x.DeleteImageAsync(It.IsAny<string>()), Times.Never);
        }
    }
}