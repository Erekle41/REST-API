using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTOs.AccountDTOs
{
    public class AccountUpdateDto
    {
        //public int ID { get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        [Required]
        public string UserName { get; set; } //unique

        [MaxLength(200)]
        [Required]
        [EmailAddress]
        public string EMail { get; set; } //unique

        [MaxLength(200)]
        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DoB { get; set; }

        public byte?[] Avatar { get; set; }
    }
}
