using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SushiMarket.BLL.Helpers;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using SushiMarket.BLL.Services.Interfaces.Cloudinary;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class UpdateCategoryCommandHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly TranslatorHelper.Translator _translator;
        private readonly Mock<ICloudinaryService> _cloudinaryServiceMock;
        private readonly Mock<ILogger<UpdateCategoryCommandHandler>> _loggerMock;
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

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _translator = new TranslatorHelper.Translator(configuration);
            _cloudinaryServiceMock = new Mock<ICloudinaryService>();
            _loggerMock = new Mock<ILogger<UpdateCategoryCommandHandler>>();

            _handler = new UpdateCategoryCommandHandler(
                _context,
                _mapper,
                _translator,
                _cloudinaryServiceMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldUpdateCategoryAndReturnUnit()
        {
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
                Image: null,
                SortOrder: 2.0
            );

            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            result.Should().Be(Unit.Value);

            var updatedCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == 1);

            updatedCategory.Should().NotBeNull();
            updatedCategory!.TitleUa.Should().Be("Нова назва");
            updatedCategory.TitleEn.Should().Be("New Name");
            updatedCategory.SortOrder.Should().Be(2.0);
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var command = new UpdateCategoryCommand(
                Id: 999,
                TitleUa: "Тест",
                TitleEn: "Test",
                Image: null,
                SortOrder: 1.0
            );

            Func<Task> act = () => _handler.Handle(
                command,
                CancellationToken.None);

            await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();
        }
    }
}