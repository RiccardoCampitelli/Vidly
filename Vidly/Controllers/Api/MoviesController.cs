using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies

        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.GenreName).Where(m=>m.NumberAvailable>0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

               var moviesDto = moviesQuery .ToList()
                .Select(Mapper.Map<Movie,MovieDto>);

            return Ok(moviesDto);
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {

            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));

        }

        //POST /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.NumberAvailable = movieDto.NumberInStock;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT /api/movie/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.Single(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            var minNumberInStock = movieInDb.NumberInStock - movieInDb.NumberAvailable;

            if (movieDto.NumberInStock < minNumberInStock)
                return BadRequest("Must be at least" + minNumberInStock + "copies available");



            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            

          

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }


    }
}
