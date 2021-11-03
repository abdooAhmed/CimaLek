using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entertment.Models
{
    public class episode
    {
        [Key]
        public int Episode_Id { get; set; }
        [Required]
        public int Episode_num { get; set; }

        public string SerieId { get; set; }

        public virtual serie serie { get; set; }

        public ICollection<episodeWatch> episodeWatches { get; set; }
    }
}
