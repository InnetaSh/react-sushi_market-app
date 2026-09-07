using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TranslatorHelper.Translator _translator;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<CreateCategoryCommandHandler>> _loggerMock;
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
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<CreateCategoryCommandHandler>>();

            _handler = new CreateCategoryCommandHandler(
                _context,
                _mapperMock.Object,
                _translator,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithBothTitlesProvided_ShouldSaveCategoryAndReturnId()
        {
            var command = new CreateCategoryCommand(
                TitleUa: "Суші",
                TitleEn: "Sushi",
                Image: null,
                SortOrder: 1.0
            );

            var categoryEntity = new Category
            {
                Id = 1,
                TitleUa = command.TitleUa,
                TitleEn = command.TitleEn,
                ImgSrc = string.Empty,
                SortOrder = command.SortOrder
            };

            _mapperMock
                .Setup(m => m.Map<Category>(command))
                .Returns(categoryEntity);

            var resultId = await _handler.Handle(
                command,
                CancellationToken.None);

            resultId.Should().Be(1);

            var categoryInDb = await _context.Categories.FindAsync(1);

            categoryInDb.Should().NotBeNull();
            categoryInDb!.TitleUa.Should().Be("Суші");
            categoryInDb.TitleEn.Should().Be("Sushi");
        }
    }
}