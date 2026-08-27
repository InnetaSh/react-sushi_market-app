using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.MediatR.Categories.GetCategoryById;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class GetCategoryByIdQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly GetCategoryByIdQueryHandler _handler;

        public GetCategoryByIdQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var loggerFactory = LoggerFactory.Create(builder => { });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>();
            }, loggerFactory);

            _mapper = config.CreateMapper();

            _handler = new GetCategoryByIdQueryHandler(
                _context,
                _mapper);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldReturnCategoryDto()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                TitleUa = "Роли",
                TitleEn = "Rolls",
                ImgSrc = "img/rolls.png",
                SortOrder = 1
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var query = new GetCategoryByIdQuery(1);

            // Act
            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<CategoryDto>();

            result.Id.Should().Be(1);
            result.TitleUa.Should().Be("Роли");
            result.TitleEn.Should().Be("Rolls");
            result.ImgSrc.Should().Be("img/rolls.png");
            result.SortOrder.Should().Be(1);
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var query = new GetCategoryByIdQuery(999);

            // Act
            Func<Task> act = () => _handler.Handle(
                query,
                CancellationToken.None);

            // Assert
            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
        }
    }
}