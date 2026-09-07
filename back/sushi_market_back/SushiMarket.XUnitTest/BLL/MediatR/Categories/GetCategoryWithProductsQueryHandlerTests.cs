using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using SushiMarket.BLL.DTOs.Categories;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts;
using SushiMarket.DAL;
using SushiMarket.DAL.Entities;

namespace SushiMarket.Tests.MediatR.Categories
{
    public class GetCategoryWithProductsQueryHandlerTests
    {
        private readonly SushiMarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetCategoryWithProductsQueryHandler>> _loggerMock;
        private readonly GetCategoryWithProductsQueryHandler _handler;

        public GetCategoryWithProductsQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<SushiMarketDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new SushiMarketDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryWithProductsDto>();
                cfg.CreateMap<Product, ProductDto>();
            }, NullLoggerFactory.Instance);

            _mapper = config.CreateMapper();
            _loggerMock = new Mock<ILogger<GetCategoryWithProductsQueryHandler>>();

            _handler = new GetCategoryWithProductsQueryHandler(
                _context,
                _mapper,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCategoryExists_ShouldReturnCategoryWithProductsDto()
        {
            var category = new Category
            {
                Id = 1,
                TitleUa = "Сети",
                TitleEn = "Sets"
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var query = new GetCategoryWithProductsQuery(1);

            var result = await _handler.Handle(
                query,
                CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeOfType<CategoryWithProductsDto>();

            result.Id.Should().Be(1);
            result.TitleUa.Should().Be("Сети");
            result.TitleEn.Should().Be("Sets");

            result.Products.Should().NotBeNull();
            result.Products.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_WhenCategoryDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            var query = new GetCategoryWithProductsQuery(999);

            Func<Task> act = () => _handler.Handle(
                query,
                CancellationToken.None);

            var exception = await act
                .Should()
                .ThrowAsync<KeyNotFoundException>();

            exception.Which.Message.Should().Contain("999");
        }
    }
}