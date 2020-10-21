using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
    public class WlogHistory
    {
        public int ID { get; set; }
        [Required]
        public WlogContent Content { get; set; }
        [Required]
        public Wlog Wlog { get; set; }

        public SystemFields SysFields { get; set; } = new SystemFields();
    }
}
