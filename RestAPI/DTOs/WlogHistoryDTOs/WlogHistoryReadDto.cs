using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTOs.WlogHistoryDTOs
{
    public class WlogHistoryReadDto
    {
        public int ID { get; set; }
        [Required]
        public WlogContent Content { get; set; }
        [Required]
        public Wlog Wlog { get; set; }

        public SystemFields systemFields { get; set; } = new SystemFields();
    }
}
