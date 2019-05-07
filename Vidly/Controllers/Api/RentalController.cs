using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            var rentals = _context.Rentals.Include(m=>m.Movie).Include(m =>m.Customer);

            var rentalsDto = new List<RentalsDisplayDto>();
            foreach (var rental in rentals)
            {
                rentalsDto.Add(Mapper.Map<Rental, RentalsDisplayDto>(rental));
            }
            return Ok(rentalsDto);

        }

        [HttpGet]
        public IHttpActionResult GetCustomerRentals(int id)
        {
            var rentals = _context.Rentals.Include(r => r.Movie).Include(r => r.Customer).Where(r => r.Customer.Id == id);
            var rentalsDto = new List<RentalsDisplayDto>();
            foreach (var rental in rentals)
            {
                rentalsDto.Add(Mapper.Map<Rental, RentalsDisplayDto>(rental));
            }
            return Ok(rentalsDto);
        }

        [HttpPost]
        public IHttpActionResult ReturnRental(int id)
        {
            var rental = _context.Rentals.Include(m=>m.Movie).Single(r => r.Id == id);

            rental.DateReturned = DateTime.Now;

            var movie = _context.Movies.Single(m => m.Id == rental.Movie.Id);
            movie.NumberAvailable++;

            _context.SaveChanges();

            return Ok();
        }
    }
}
