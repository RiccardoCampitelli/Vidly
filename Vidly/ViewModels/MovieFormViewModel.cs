using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
       
        public IEnumerable<Genre> GenreList { get; set; }

        [Required]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public int? GenreId { get; set; }

       
        [Required]
        [Display(Name = "Release date")]
        public DateTime? ReleaseDate { get; set; }
       
        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in stock")]
        public int? NumberInStock { get; set; }


        public string Title
        {
            get
            {
                if(Id!= 0)
                    return "Edit Movie";

                return "New Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}