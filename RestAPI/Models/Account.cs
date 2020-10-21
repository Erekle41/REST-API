using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
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
        [NotMapped]
        public byte?[] Avatar { get; set; }
        [Required]
        public UserStatus Status { get; set; } = UserStatus.Registered;

        public SystemFields SysFields { get; set; } = new SystemFields();

        public virtual ICollection<Wlog> Wlogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public enum UserStatus : byte
    {
        Registered = 0,
        Verified = 1,
        Deactivated = 2
    }
}
