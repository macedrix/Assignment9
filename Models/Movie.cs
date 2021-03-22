using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Models
{
    public class Movie
    {
        //Primary Key 
        [Key]
        [Required]
        public int MovieId { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Rating { get; set; }

        //These 3 are not required
        public bool Edited { get; set; }
        public string LentTo { get; set; }

        //Can't go over 25 characters
        [MaxLength(25)]
        public string Notes { get; set; }
    }
}
