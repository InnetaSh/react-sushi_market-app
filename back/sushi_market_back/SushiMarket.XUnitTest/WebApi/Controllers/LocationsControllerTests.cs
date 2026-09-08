using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.BLL.MediatR.Locations.GetLocations;
using sushi_market_back.Controllers;
using Xunit;

namespace SushiMarket.Tests.Controllers
{
    public class LocationsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly LocationsController _controller;

        public LocationsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new LocationsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenLocationsExist_ReturnsOkWithLocationList()
        {
            var locations = new List<LocationDto>
            {
                new LocationDto
                {
                    Id = 1,
                    Slug = "kyiv-1",
                    TitleKeyUa = "Центр",
                    TitleKeyEn = "Center",
                    CityKeyUa = "Київ",
                    CityKeyEn = "Kyiv",
                    AddressKeyUa = "Хрещатик 1",
                    AddressKeyEn = "Khreshchatyk 1",
                    Phone = "+380000000001",
                    Lat = 50.45,
                    Lng = 30.52,
                    Hours = "10:00 - 22:00",
                    ImageSrc = "img1.jpg"
                },
                new LocationDto
                {
                    Id = 2,
                    Slug = "lviv-1",
                    TitleKeyUa = "Ремо",
                    TitleKeyEn = "Remo",
                    CityKeyUa = "Львів",
                    CityKeyEn = "Lviv",
                    AddressKeyUa = "Свободи 5",
                    AddressKeyEn = "Svobody 5",
                    Phone = "+380000000002",
                    Lat = 49.84,
                    Lng = 24.02,
                    Hours = "10:00 - 22:00",
                    ImageSrc = "img2.jpg"
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetLocationListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(locations);

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<LocationDto>>().Subject;

            returnDtos.Should().HaveCount(2);
            returnDtos.Should().Contain(l => l.Slug == "kyiv-1" && l.CityKeyEn == "Kyiv");
            returnDtos.Should().Contain(l => l.Slug == "lviv-1" && l.CityKeyEn == "Lviv");

            _mediatorMock.Verify(
                m => m.Send(It.IsAny<GetLocationListQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoLocationsExist_ReturnsOkWithEmptyList()
        {
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetLocationListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<LocationDto>());

            var result = await _controller.GetAll();

            var actionResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnDtos = actionResult.Value.Should().BeAssignableTo<IEnumerable<LocationDto>>().Subject;

            returnDtos.Should().BeEmpty();
        }
    }
}