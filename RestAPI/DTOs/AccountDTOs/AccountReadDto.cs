using RestAPI.DTOs.CommentDTOs;
using RestAPI.DTOs.WlogDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTOs.AccountDTOs
{
    public class AccountReadDto
    {
        //public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; } 

        public string EMail { get; set; } 

        //public string Password { get; set; }

        public DateTime? DoB { get; set; }

        public byte?[] Avatar { get; set; }

        public UserStatus Status { get; set; }

        public SystemFields SysFields { get; set; }

        public virtual ICollection<WlogReadDto> Wlogs { get; set; }
        public virtual ICollection<CommentReadDto> Comments { get; set; }
    }
}
