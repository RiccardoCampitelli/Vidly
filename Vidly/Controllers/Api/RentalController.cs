using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto rentalDto)
        {
            var rentedMovies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id));
            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);


            foreach (var movie in rentedMovies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest(); 

               
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                movie.NumberAvailable--;

                _context.Rentals.Add(rental);

                
           
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
