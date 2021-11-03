using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entertment.Models
{
    public class filmWatch
    {
        [Key]
        public int LinkId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ServerName { get; set; }
        
        [Required]
        
        public string filmId { get; set; }
        public virtual film film { get; set; }
        public bool Again { get; set; }
    }
}
