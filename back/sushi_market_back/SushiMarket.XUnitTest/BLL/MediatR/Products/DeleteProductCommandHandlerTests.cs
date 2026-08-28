using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.MediatR.Products.DeleteProduct;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            _handler = new DeleteProductCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_WhenProductExists_ShouldDeleteProductAndReturnUnit()
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

            var command = new DeleteProductCommand(1);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);

            var deletedProduct = await _context.Products
                .FindAsync(1);

            deletedProduct.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new DeleteProductCommand(999);

            // Act
            Func<Task> act = () => _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
        }
    }
}