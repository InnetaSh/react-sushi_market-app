using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using SushiMarket.BLL.DTOs.Auth;
using SushiMarket.DAL.Entities.Users;

namespace SushiMarket.BLL.Mapping.Users
{
    [ExcludeFromCodeCoverage]
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<UserDto, UserLoginDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserRegisterDto, User>();
        }
    }
}
