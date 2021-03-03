using System;
using Application.HomeBases.Commands;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Common.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddHomeBaseCommand, HomeBase>().ReverseMap();
            CreateMap<HomeBaseResponse, HomeBase>().ReverseMap();
            CreateMap<UpdateHomeBaseCommand, HomeBase>().ReverseMap();
            CreateMap<HomeBaseCreateUpdateDto, HomeBase>().ReverseMap();
        }
    }
}