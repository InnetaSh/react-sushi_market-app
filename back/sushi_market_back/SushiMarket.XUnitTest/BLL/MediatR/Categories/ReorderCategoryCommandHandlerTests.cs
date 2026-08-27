using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SushiMarket.BLL.MediatR.Categories.ReorderCategory;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class ReorderCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly ReorderCategoryCommandHandler _handler;

        public ReorderCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _handler = new ReorderCategoryCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldUpdateSortOrderAndReturnUnit()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls",
                SortOrder = 5.0
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new ReorderCategoryCommand(CategoryId: 1, NewSortOrder: 2.5);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);

            var updatedCategory = await _context.Categories.FindAsync(1);
            updatedCategory.Should().NotBeNull();
            updatedCategory.SortOrder.Should().Be(2.5);
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new ReorderCategoryCommand(CategoryId: 999, NewSortOrder: 1.0);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}