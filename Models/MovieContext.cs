using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options) : base (options)
        {

        }

        //Creates a table called movies in the database
        public DbSet<Movie> Movies { get; set; }
    }
}
