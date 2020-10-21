using AutoMapper;
using RestAPI.DTOs.AccountDTOs;
using RestAPI.DTOs.CommentDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {//source -> target
            CreateMap<Comment, CommentReadDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>().ReverseMap();
        }

    }
}
