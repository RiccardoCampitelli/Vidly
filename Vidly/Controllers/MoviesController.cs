using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;
       

        public MoviesController()
        {
            _context = new ApplicationDbContext();
           
        }
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
        
        

        [Route("movies/")]
        public ActionResult Display()
        {
            
            var movies = _context.Movies.Include( m => m.GenreName).ToList();

            return View(movies);

        }

        [Route("movies/{id}")]
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.GenreName).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }

        [Route("Movies/New")]
        public ActionResult New()
        {

            var viewModel = new MovieFormViewModel
            {
                GenreList = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Movies/Save")]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                   
                    GenreList = _context.Genres.ToList()
                };


                return View("MovieForm", viewModel);
            }


            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;

            }

            _context.SaveChanges();

            return RedirectToAction("Display", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                
                GenreList = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);


        }





    }
}