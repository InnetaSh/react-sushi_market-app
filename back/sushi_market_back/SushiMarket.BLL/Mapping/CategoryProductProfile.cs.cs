using AutoMapper;
using SushiMarket.BLL.DTOs;
using SushiMarket.BLL.MediatR.Categories.CreateCategory;
using SushiMarket.BLL.MediatR.Categories.UpdateCategory;
using SushiMarket.BLL.MediatR.Products.CreateProduct;
using SushiMarket.BLL.MediatR.Products.UpdateProduct;
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

            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(dest => dest.TitleUa, opt => opt.Ignore())
                .ForMember(dest => dest.TitleEn, opt => opt.Ignore());

            CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(dest => dest.TitleUa, opt => opt.Ignore())
                .ForMember(dest => dest.TitleEn, opt => opt.Ignore())
                .ForMember(dest => dest.ImgSrc, opt => opt.Condition(src => src.ImgSrc != null))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductRequestDto, Product>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore());

            CreateMap<UpdateProductRequestDto, Product>()
                .ForMember(dest => dest.ImgSrc, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.TitleUa, opt => opt.Ignore())
                .ForMember(dest => dest.TitleEn, opt => opt.Ignore())
                .ForMember(dest => dest.DescriptionUa, opt => opt.Ignore())
                .ForMember(dest => dest.DescriptionEn, opt => opt.Ignore());

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.TitleUa, opt => opt.Ignore())
                .ForMember(dest => dest.TitleEn, opt => opt.Ignore())
                .ForMember(dest => dest.DescriptionUa, opt => opt.Ignore())
                .ForMember(dest => dest.DescriptionEn, opt => opt.Ignore())
                .ForMember(dest => dest.ImgSrc, opt => opt.Condition(src => src.ImgSrc != null))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}