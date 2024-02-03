using AutoMapper;
using Tech.Interview.Application.Presentation;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetAllUsersViewModel>().ReverseMap();
            CreateMap<User, GetUserByIdViewModel>().ReverseMap();
            CreateMap<User, CreateUserViewModel>().ReverseMap();
            CreateMap<User, UpdateUserViewModel>().ReverseMap();
        }
    }
}