using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Models
{
    public class Wlog
    {
        public int ID { get; set; }
        [Required]
        public WlogContent PublishedContent { get; set; }
        public WlogContent Draft { get; set; }

        [NotMapped]
        public byte?[] Img { get; set; }
        public DateTime PostDate { get; set; }
        [Required]
        public bool Published { get; set; }
        [ForeignKey("Account")]
        public int AccID { get; set; }


        public SystemFields SysFields { get; set; } = new SystemFields();

        public virtual Account Account { get; set; }
        public virtual ICollection<WlogHistory> History { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
