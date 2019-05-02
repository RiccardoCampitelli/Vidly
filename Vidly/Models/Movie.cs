using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Display(Name = "Genre")]
        public Genre GenreName { get; set; }

        [Required]
        [Display(Name="Release date")]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        [Range(1,20)]
        [Display(Name="Number in stock")]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

    }
}