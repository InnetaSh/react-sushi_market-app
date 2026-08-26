using AutoMapper;
using SushiMarket.BLL.DTOs;
using SushiMarket.DAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SushiMarket.BLL.Mapping
{
    [ExcludeFromCodeCoverage]
    public class CategoryProductProfile : Profile
    {
        public CategoryProductProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequestDto, Category>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore());

            CreateMap<UpdateCategoryRequestDto, Category>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductRequestDto, Product>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore());

            CreateMap<UpdateProductRequestDto, Product>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}