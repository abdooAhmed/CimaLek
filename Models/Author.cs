using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CimaLek.Models
{
    public class Author
    {
        [Key]
        public string AuthorId { set; get; }
        
        public string author { get; set; }
        public string ImageURl { get; set; }
        public IFormFile Image { get; set; }
        public bool Again { get; set; }
        public virtual ICollection<AuthorToFilm> AuthorToFilms { get; set; }
        public virtual ICollection<AuthorToSeries> AuthorToSeries { get; set; }

    }
}
