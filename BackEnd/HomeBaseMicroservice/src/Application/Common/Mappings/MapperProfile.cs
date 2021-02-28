using Application.HomeBases.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddHomeBaseCommand, HomeBase>().ReverseMap();
        }
    }
}