using AutoMapper;
using RestAPI.DTOs.AccountDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {//source -> target
            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountCreateDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<AccountUpdateDto, Account>().ReverseMap();
        }
    }
}
