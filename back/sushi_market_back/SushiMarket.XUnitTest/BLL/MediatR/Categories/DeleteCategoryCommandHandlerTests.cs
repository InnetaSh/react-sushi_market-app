using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.MediatR.Categories.DeleteCategory;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class DeleteCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<DeleteCategoryCommandHandler>> _loggerMock;
        private readonly DeleteCategoryCommandHandler _handler;

        public DeleteCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<DeleteCategoryCommandHandler>>();

            _handler = new DeleteCategoryCommandHandler(
                _context,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldDeleteCategoryAndReturnUnit()
        {
            var category = new Category
            {
                Id = 1,
                TitleUa = "Суші",
                TitleEn = "Sushi",
                ImgSrc = "categories/test.jpg"
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var command = new DeleteCategoryCommand(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);

            _cloudinaryServiceMock.Verify(
                x => x.DeleteImageAsync("categories/test.jpg"),
                Times.Once);

            var deletedCategory = await _context.Categories.FindAsync(1);
            deletedCategory.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var command = new DeleteCategoryCommand(999);

            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>();

            _cloudinaryServiceMock.Verify(
                x => x.DeleteImageAsync(It.IsAny<string>()),
                Times.Never);
        }
    }
}