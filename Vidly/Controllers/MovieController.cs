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
    public class MovieController : Controller
    {

        private ApplicationDbContext _context;
       

        public MovieController()
        {
            _context = new ApplicationDbContext();
           
        }
       
        
        

        [Route("movies/")]
        public ActionResult Display()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View();


            return View("ReadOnlyDisplay");

        }

        [Route("movies/{id}")]
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.GenreName).SingleOrDefault(c => c.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
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
        [Authorize(Roles = RoleName.CanManageMovies)]
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
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;

            }

            _context.SaveChanges();

            return RedirectToAction("Display", "Movie");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
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