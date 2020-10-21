using RestAPI.DTOs.AccountDTOs;
using RestAPI.DTOs.CommentDTOs;
using RestAPI.DTOs.WlogHistoryDTOs;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTOs.WlogDTOs
{
    public class WlogCreateDto
    {
        [Required]
        public WlogContent PublishedContent { get; set; }
        public WlogContent Draft { get; set; }

        public byte?[] Img { get; set; }
        public DateTime PostDate { get; set; }
        [Required]
        public bool Published { get; set; }
        [ForeignKey("Account")]
        public int AccID { get; set; }

        [Required]
        public virtual AccountReadDto Account { get; set; }
        public virtual ICollection<WlogHistoryReadDto> History { get; set; }
        public virtual ICollection<CommentReadDto> Comments { get; set; }
    }
}
