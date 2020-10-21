using AutoMapper;
using RestAPI.DTOs.WlogHistoryDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Profiles
{
    public class WlogHistoryProfile : Profile
    {
        public WlogHistoryProfile()
        {
            CreateMap<WlogHistory, WlogHistoryReadDto>();
            CreateMap<WlogHistoryCreateDto, WlogHistory>();
            //CreateMap<WlogHistoryUpdateDto, WlogHistory>();
            //CreateMap<WlogHistoryUpdateDto, WlogHistory>().ReverseMap();
        }
    }
}
