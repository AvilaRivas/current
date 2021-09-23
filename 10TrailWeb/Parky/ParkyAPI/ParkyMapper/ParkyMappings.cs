using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Dto;
using ParkyAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDtoBase>().ReverseMap();
            CreateMap<Trail, TrailDtoWithRelations>().ReverseMap();
            CreateMap<Trail, TrailDtoUpdate>().ReverseMap();
        }
    }
}
