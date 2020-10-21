using AutoMapper;
using RestAPI.DTOs.WlogDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Profiles
{
    public class WlogsProfile : Profile
    {
        public WlogsProfile()
        {//source -> target
            CreateMap<Wlog, WlogReadDto>();
            CreateMap<WlogCreateDto, Wlog>();
            CreateMap<WlogUpdateDto, Wlog>();
            CreateMap<WlogUpdateDto, Wlog>().ReverseMap();
        }
    }
}
