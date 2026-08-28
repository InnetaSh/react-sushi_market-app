using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using SushiMarket.BLL.MediatR.Categories.DeleteCategory;
using SushiMarket.BLL.MediatR.Categories.GetCategoriesList;
using SushiMarket.BLL.MediatR.Categories.GetCategoriesWithProducts;
using SushiMarket.BLL.MediatR.Categories.GetCategoryById;
using SushiMarket.BLL.MediatR.Categories.GetCategoryWithProducts;
using SushiMarket.BLL.MediatR.Categories.ReorderCategory;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using sushi_market_back.Controllers;
using Xunit;

namespace SushiMarket.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CategoriesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetCategories_ReturnsOkResult_WithCategoriesList()
        {
            // Arrange
            var expectedList = new List<CategoryDto>();
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCategoriesListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedList);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetCategoryById_ReturnsOkResult_WithCategory()
        {
            // Arrange
            int categoryId = 1;
            var expectedCategory = new CategoryDto { Id = categoryId };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetCategoryByIdQuery>(q => q.Id == categoryId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCategory);

            // Act
            var result = await _controller.GetCategoryById(categoryId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedCategory);
        }

        [Fact]
        public async Task GetCategoryWithProducts_ReturnsOkResult_WithCategoryAndProducts()
        {
            // Arrange
            int categoryId = 1;
            var expectedResult = new CategoryWithProductsDto { Id = categoryId };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetCategoryWithProductsQuery>(q => q.CategoryId == categoryId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetCategoryWithProducts(categoryId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetCategoriesWithProducts_ReturnsOkResult_WithCategoriesAndProductsList()
        {
            // Arrange
            var expectedList = new List<CategoryWithProductsDto>();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCategoriesWithProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedList);

            // Act
            var result = await _controller.GetCategoriesWithProducts();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task CreateCategory_WhenValidRequest_ReturnsOkResultWithId()
        {
            // Arrange
            var request = new CreateCategoryRequestDto
            {
                TitleUa = "Суші",
                TitleEn = "Sushi",
                SortOrder = 1.0
            };

            int expectedId = 5;
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _controller.CreateCategory(request);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(expectedId);
        }

        [Fact]
        public async Task UpdateCategory_WhenIdMismatch_ReturnsBadRequest()
        {
            // Arrange
            int routeId = 1;
            var request = new UpdateCategoryRequestDto { Id = 2 };

            // Act
            var result = await _controller.UpdateCategory(routeId, request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateCategory_WhenValid_ReturnsNoContent()
        {
            // Arrange
            int categoryId = 1;
            var request = new UpdateCategoryRequestDto
            {
                Id = categoryId,
                TitleUa = "Оновлені суші",
                TitleEn = "Updated Sushi",
                SortOrder = 2.0
            };

            var existingCategory = new CategoryDto { Id = categoryId, ImgSrc = "/uploads/img.png" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCategory);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.UpdateCategory(categoryId, request);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteCategory_ReturnsNoContent()
        {
            // Arrange
            int categoryId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReorderCategory_ReturnsNoContent()
        {
            // Arrange
            var command = new ReorderCategoryCommand(1, 2.0);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.ReorderCategory(command);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}