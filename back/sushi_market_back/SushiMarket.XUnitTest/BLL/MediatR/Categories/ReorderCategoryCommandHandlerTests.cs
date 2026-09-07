using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.MediatR.Categories.ReorderCategory;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class ReorderCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly Mock<ILogger<ReorderCategoryCommandHandler>> _loggerMock;
        private readonly ReorderCategoryCommandHandler _handler;

        public ReorderCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _loggerMock = new Mock<ILogger<ReorderCategoryCommandHandler>>();

            _handler = new ReorderCategoryCommandHandler(_context, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldUpdateSortOrderAndReturnUnit()
        {
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

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);

            var updatedCategory = await _context.Categories.FindAsync(1);
            updatedCategory.Should().NotBeNull();
            updatedCategory.SortOrder.Should().Be(2.5);
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var command = new ReorderCategoryCommand(CategoryId: 999, NewSortOrder: 1.0);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}