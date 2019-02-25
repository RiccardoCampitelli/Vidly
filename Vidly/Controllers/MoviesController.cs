using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek" };

            var viewResult = new ViewResult();
            var customers = new List<Models.Customer> {
                new Models.Customer { Name = "Customer 1"},
                new Models.Customer { Name= "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);
        }
        
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Route("movies/")]
        public ActionResult Display()
        {
            var movies = GetMovies();

           

            return View(movies);

        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "First Movie" },
                new Movie { Id = 2, Name = "Second Movie" }
            };
        }



    }
}