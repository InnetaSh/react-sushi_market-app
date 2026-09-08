using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs.Promotions;
using SushiMarket.BLL.MediatR.Promotions.GetPromotions;
using sushi_market_back.Controllers;

namespace SushiMarket.Tests.Controllers
{
    public class PromotionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PromotionsController _controller;

        public PromotionsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PromotionsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenPromotionsExist_ReturnsOkWithPromotionList()
        {
            var promotions = new List<PromotionDto>
            {
                new PromotionDto
                {
                    Id = 1,
                    ImageUrl = "promo1.jpg",
                    DateKeyUa = "Діє до 10.09",
                    DateKeyEn = "Valid until Sep 10",
                    TitleKeyUa = "Знижка 20%",
                    TitleKeyEn = "20% Off",
                    DescriptionKeyUa = "Опис акції українською",
                    DescriptionKeyEn = "Promotion description in English",
                    Link = "/promo/1"
                },
                new PromotionDto
                {
                    Id = 2,
                    ImageUrl = "promo2.jpg",
                    DateKeyUa = "Діє до 15.09",
                    DateKeyEn = "Valid until Sep 15",
                    TitleKeyUa = "Безкоштовна доставка",
                    TitleKeyEn = "Free Delivery",
                    DescriptionKeyUa = "Ще один опис",
                    DescriptionKeyEn = "Another description",
                    Link = "/promo/2"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPromotionListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(promotions);

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<PromotionDto>>().Subject;

            returnDtos.Should().HaveCount(2);
            returnDtos.Should().Contain(p => p.Id == 1 && p.TitleKeyEn == "20% Off");
            returnDtos.Should().Contain(p => p.Id == 2 && p.TitleKeyEn == "Free Delivery");

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<GetPromotionListQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoPromotionsExist_ReturnsOkWithEmptyList()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetPromotionListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<PromotionDto>());

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<PromotionDto>>().Subject;

            returnDtos.Should().BeEmpty();
        }
    }
}