using Application.Bikes.Commands;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Bike, BikeResponse>().ReverseMap();
            CreateMap<Bike, AddBikeCommand>().ReverseMap();
            CreateMap<Bike, UpdateBikeCommand>().ReverseMap();
        }
    }
}