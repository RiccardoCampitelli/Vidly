using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class RentalsDisplayDto
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public int MovieId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}