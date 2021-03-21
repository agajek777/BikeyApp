using Application.Hires.Commands;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hire, HireResponse>().ReverseMap();
            CreateMap<Hire, CreateHireCommand>().ReverseMap();
            CreateMap<HomeBase, HomeBaseResponse>().ReverseMap();
            CreateMap<HomeBase, HomeBaseUpdateDto>().ReverseMap();
        }
    }
}