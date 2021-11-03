using Entertment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CimaLek.Models
{
    public class AuthorToSeries
    {
        public string serieId { set; get; }
        public string authorId { set; get; }
        public serie serie { set; get; }
        public Author Author { set; get; }
    }
}
