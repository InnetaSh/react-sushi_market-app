using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SushiMarket.BLL.MediatR.Categories.DeleteCategory;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class DeleteCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly DeleteCategoryCommandHandler _handler;

        public DeleteCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _handler = new DeleteCategoryCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldDeleteCategoryAndReturnUnit()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Суші",
                TitleEn = "Sushi"
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new DeleteCategoryCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);

            var deletedCategory = await _context.Categories.FindAsync(1);
            deletedCategory.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new DeleteCategoryCommand(999);

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}