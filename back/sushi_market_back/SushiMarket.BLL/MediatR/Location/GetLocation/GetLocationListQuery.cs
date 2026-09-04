using MediatR;
using SushiMarket.BLL.DTOs.Locations;

namespace SushiMarket.BLL.MediatR.Locations.GetLocations
{
    public record GetLocationListQuery : IRequest<IEnumerable<LocationDto>>;
}
