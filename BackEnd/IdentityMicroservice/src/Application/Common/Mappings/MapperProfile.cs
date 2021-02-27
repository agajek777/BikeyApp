using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserWithTokenDto>().ReverseMap();
        }
    }
}