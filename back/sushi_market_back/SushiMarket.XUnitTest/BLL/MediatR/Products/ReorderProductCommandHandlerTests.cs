using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.MediatR.Products.ReorderProduct;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Products
{
    public class ReorderProductCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly ReorderProductCommandHandler _handler;

        public ReorderProductCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            _handler = new ReorderProductCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_WhenProductExists_ShouldUpdateSortOrderAndReturnUnit()
        {
            // Arrange
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

            var command = new ReorderProductCommand(
                ProductId: 1,
                NewSortOrder: 5);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);

            var updatedProduct = await _context.Products
                .FindAsync(1);

            updatedProduct.Should().NotBeNull();
            updatedProduct!.SortOrder.Should().Be(5);
        }

        [Fact]
        public async Task Handle_WhenProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new ReorderProductCommand(
                ProductId: 999,
                NewSortOrder: 5);

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

        [Fact]
        public async Task Handle_ShouldChangeOnlySortOrder()
        {
            // Arrange
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

            var command = new ReorderProductCommand(
                ProductId: 1,
                NewSortOrder: 10);

            // Act
            await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            var updatedProduct = await _context.Products
                .FindAsync(1);

            updatedProduct.Should().NotBeNull();
            updatedProduct!.SortOrder.Should().Be(10);

            updatedProduct.TitleUa.Should().Be("Філадельфія");
            updatedProduct.TitleEn.Should().Be("Philadelphia");
            updatedProduct.DescriptionUa.Should().Be("Рол з лососем");
            updatedProduct.DescriptionEn.Should().Be("Salmon roll");
            updatedProduct.WeightOrVolume.Should().Be("250 г");
            updatedProduct.Price.Should().Be(250m);
            updatedProduct.ImgSrc.Should().Be("philadelphia.png");
            updatedProduct.CategoryId.Should().Be(1);
        }
    }
}