using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        public int CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime DateReturned { get; set; } 
    }
}