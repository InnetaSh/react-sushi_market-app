using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class UpdateCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateCategoryCommandHandler _handler;

        public UpdateCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateCategoryCommand, Category>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            _handler = new UpdateCategoryCommandHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldUpdateCategoryAndReturnUnit()
        {
            // Arrange
            var existingCategory = new Category
            {
                Id = 1,
                TitleUa = "Стара назва",
                TitleEn = "Old Name",
                ImgSrc = "old.png",
                SortOrder = 1.0
            };

            _context.Categories.Add(existingCategory);
            await _context.SaveChangesAsync();

            var command = new UpdateCategoryCommand(
                Id: 1,
                TitleUa: "Нова назва",
                TitleEn: "New Name",
                SortOrder: 2.0,
                ImgSrc: "new.png"
            );

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);

            var updatedCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == 1);

            updatedCategory.Should().NotBeNull();
            updatedCategory!.TitleUa.Should().Be("Нова назва");
            updatedCategory.TitleEn.Should().Be("New Name");
            updatedCategory.ImgSrc.Should().Be("new.png");
            updatedCategory.SortOrder.Should().Be(2.0);
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new UpdateCategoryCommand(
                Id: 999,
                TitleUa: "Тест",
                TitleEn: "Test",
                SortOrder: 1.0,
                ImgSrc: "test.png"
            );

            // Act
            Func<Task> act = () => _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();
        }
    }
}