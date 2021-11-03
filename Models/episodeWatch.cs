using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entertment.Models
{
    public class episodeWatch
    {
        [Key]
        public int LinkId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string ServerName { get; set; }

        public int episodeId { get; set; }
        public virtual episode episode { get; set; }
        
    }
}
