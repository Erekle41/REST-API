using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    public class Comment
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
        public SystemFields SysFields { get; set; }

        [Required]
        public virtual Wlog Wlog { get; set; }

        [Required]
        public virtual Account User { get; set; }
    }
}
