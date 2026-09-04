using AutoMapper;
using SushiMarket.BLL.DTOs.News;
using SushiMarket.DAL.Entities;
using SushiMarket.DAL.Entities.NewsItem;

namespace SushiMarket.BLL.Mapping;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<NewsItem, NewsItemDto>();
    }
}