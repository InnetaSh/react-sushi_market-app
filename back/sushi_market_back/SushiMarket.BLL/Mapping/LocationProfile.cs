using AutoMapper;
using SushiMarket.BLL.DTOs.Locations;
using SushiMarket.DAL.Entities.Location;

namespace SushiMarket.BLL.Mapping;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();
    }
}