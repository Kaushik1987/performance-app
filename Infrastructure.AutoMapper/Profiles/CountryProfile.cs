﻿using AutoMapper;
using Core.Domain.BaseData.Countries;
using Service.Dtos;
using Service.Dtos.BaseData;

namespace Infrastructure.AutoMapper.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.IsEnabled, opt => opt.MapFrom(src => src.IsEnabled))
                .ReverseMap();
        }
    }
}
