using AutoMapper;
using Tech.Interview.Application.Features.Users.Queries;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.Infrastructure.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetAllUsersModelResult>().ReverseMap();
        }
    }
}