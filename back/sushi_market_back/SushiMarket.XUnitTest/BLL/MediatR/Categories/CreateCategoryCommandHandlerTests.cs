using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TranslatorHelper.Translator _translator;
        private readonly CreateCategoryCommandHandler _handler;

        public CreateCategoryCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);
            _mapperMock = new Mock<IMapper>();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _translator = new TranslatorHelper.Translator(configuration);

            _handler = new CreateCategoryCommandHandler(
                _context,
                _mapperMock.Object,
                _translator);
        }

        [Fact]
        public async Task Handle_WithBothTitlesProvided_ShouldSaveCategoryAndReturnId()
        {
            // Arrange
            var command = new CreateCategoryCommand(
                TitleUa: "Суші",
                TitleEn: "Sushi",
                ImgSrc: "img/sushi.png",
                SortOrder: 1.0
            );

            var categoryEntity = new Category
            {
                Id = 1,
                TitleUa = command.TitleUa,
                TitleEn = command.TitleEn,
                ImgSrc = command.ImgSrc,
                SortOrder = command.SortOrder
            };

            _mapperMock
                .Setup(m => m.Map<Category>(command))
                .Returns(categoryEntity);

            // Act
            var resultId = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            resultId.Should().Be(1);

            var categoryInDb = await _context.Categories.FindAsync(1);

            categoryInDb.Should().NotBeNull();
            categoryInDb!.TitleUa.Should().Be("Суші");
            categoryInDb.TitleEn.Should().Be("Sushi");
            categoryInDb.ImgSrc.Should().Be("img/sushi.png");
        }
    }
}