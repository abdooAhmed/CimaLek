using CimaLek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entertment.Models
{
    public class film
    {
        [Key]
        public string filmId { set; get; }
        [Required]
        public string name { set; get; }
        public string country { set; get; }
        [Required]
        public string time { set; get; }
        [Required]
        public string Describtion { set; get; }
        public int rate { set; get; }
        
        public DateTime CreateDate { set; get; }
        public string image { set; get; }
        public string TrailerURl { get; set; }
        public virtual ICollection<filmtype> filmtypes { get; set; }
        public virtual ICollection<AuthorToFilm> AuthorToFilms { get; set; }
        
        
        public virtual ICollection<filmWatch> filmWatches { get; set; }
    }
}
