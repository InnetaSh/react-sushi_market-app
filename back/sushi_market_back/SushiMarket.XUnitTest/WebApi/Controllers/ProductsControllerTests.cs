using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using SushiMarket.BLL.MediatR.Products.DeleteProduct;
using SushiMarket.BLL.MediatR.Products.GetProductById;
using SushiMarket.BLL.MediatR.Products.GetProductsList;
using SushiMarket.BLL.MediatR.Products.ReorderProduct;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;
using sushi_market_back.Controllers;
using SushiMarket.BLL.DTOs.Products;
using SushiMarket.BLL.DTOs.Categories;

namespace SushiMarket.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithProductsList()
        {
            int? categoryId = 1;
            var expectedList = new List<ProductDto>();
            _mediatorMock
                .Setup(m => m.Send(It.Is<GetProductsListQuery>(q => q.CategoryId == categoryId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedList);

            var result = await _controller.GetProducts(categoryId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedList);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkResult_WithProduct()
        {
            int productId = 1;
            var expectedProduct = new ProductDto { Id = productId };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetProductByIdQuery>(q => q.Id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);

            var result = await _controller.GetProductById(productId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedProduct);
        }

        [Fact]
        public async Task CreateProduct_WhenValidRequest_ReturnsOkResultWithId()
        {
            var request = new CreateProductRequestDto
            {
                TitleUa = "Філадельфія",
                TitleEn = "Philadelphia",
                Price = 250.0m,
                WeightOrVolume = "250г",
                CategoryId = 1
            };

            int expectedId = 10;
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedId);

            var result = await _controller.CreateProduct(request);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().Be(expectedId);
        }

        [Fact]
        public async Task UpdateProduct_WhenIdMismatch_ReturnsBadRequest()
        {
            int routeId = 1;
            var request = new UpdateProductRequestDto { Id = 2 };

            var result = await _controller.UpdateProduct(routeId, request);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task UpdateProduct_WhenValid_ReturnsNoContent()
        {
            int productId = 1;
            var request = new UpdateProductRequestDto
            {
                Id = productId,
                TitleUa = "Оновлена Філадельфія",
                TitleEn = "Updated Philadelphia",
                Price = 280.0m,
                WeightOrVolume = "260г",
                CategoryId = 1
            };

            var existingProduct = new ProductDto { Id = productId, ImgSrc = "/uploads/products/img.png" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProduct);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.UpdateProduct(productId, request);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            int productId = 1;

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.DeleteProduct(productId);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task ReorderProduct_ReturnsNoContent()
        {
            var command = new ReorderProductCommand(1, 3.0);

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var result = await _controller.ReorderProduct(command);

            result.Should().BeOfType<NoContentResult>();
        }
    }
}