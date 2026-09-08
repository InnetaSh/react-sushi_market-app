using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.BLL.MediatR.SushiMarket.GetSushiMarket;
using sushi_market_back.Controllers;

namespace SushiMarket.Tests.Controllers
{
    public class NewsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly NewsController _controller;

        public NewsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new NewsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenNewsExist_ReturnsOkWithNewsList()
        {
            var newsList = new List<NewsItemDto>
            {
                new NewsItemDto
                {
                    Id = 1,
                    Date = "2026-09-07",
                    TitleKeyUa = "Новина 1",
                    TitleKeyEn = "News 1",
                    DescriptionKeyUa = "Опис 1",
                    DescriptionKeyEn = "Desc 1",
                    Link = "/news/1"
                },
                new NewsItemDto
                {
                    Id = 2,
                    Date = "2026-09-08",
                    TitleKeyUa = "Новина 2",
                    TitleKeyEn = "News 2",
                    DescriptionKeyUa = "Опис 2",
                    DescriptionKeyEn = "Desc 2",
                    Link = "/news/2"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetNewsListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(newsList);

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<NewsItemDto>>().Subject;

            returnDtos.Should().HaveCount(2);
            returnDtos.Should().Contain(n => n.Id == 1 && n.TitleKeyEn == "News 1");
            returnDtos.Should().Contain(n => n.Id == 2 && n.TitleKeyEn == "News 2");

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<GetNewsListQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoNewsExist_ReturnsOkWithEmptyList()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetNewsListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<NewsItemDto>());

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<NewsItemDto>>().Subject;

            returnDtos.Should().BeEmpty();
        }
    }
}