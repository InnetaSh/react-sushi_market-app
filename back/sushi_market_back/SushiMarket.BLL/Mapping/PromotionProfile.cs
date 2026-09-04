using AutoMapper;
using SushiMarket.BLL.DTOs.Promotions;
using SushiMarket.DAL.Entities;

namespace SushiMarket.BLL.Mapping;

public class PromotionProfile : Profile
{
    public PromotionProfile()
    {
        CreateMap<Promotion, PromotionDto>();
    }
}