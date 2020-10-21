using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTOs.CommentDTOs
{
    public class CommentCreateDto
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Wlog")]
        public int WlogID { get; set; }

        [Required]
        public SystemFields systemFields { get; set; }

        [Required]
        public virtual Wlog Wlog { get; set; }

        [Required]
        public virtual Account User { get; set; }
    }
}
