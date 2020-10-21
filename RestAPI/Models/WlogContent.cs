using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    [ComplexType]
    [Owned]
    public class WlogContent
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
