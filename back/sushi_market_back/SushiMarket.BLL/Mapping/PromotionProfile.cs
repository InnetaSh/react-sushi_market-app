using AutoMapper;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL.Entities;

namespace SushiMarket.BLL.Mapping;

public class PromotionProfile : Profile
{
    public PromotionProfile()
    {
        CreateMap<Promotion, PromotionDto>();
    }
}