using AutoMapper;
using Tech.Interview.Application.Features.Users.Commands;
using Tech.Interview.Application.Features.Users.Queries;
using Tech.Interview.Application.Presentation;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetAllUsersModelResult>().ReverseMap();
            CreateMap<User, GetUserByIdModelResult>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<CreateUserCommand, CreateUserViewModel>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<UpdateUserCommand, UpdateUserViewModel>().ReverseMap();
        }
    }
}