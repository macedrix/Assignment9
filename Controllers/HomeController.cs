using Assignment9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment9.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        //Create context variable
        private MovieContext _context;


        //Set the context sent in equal to the _context in the constructor
        public HomeController(ILogger<HomeController> logger, MovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Opens the add movie page with the form
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        //Actually adds the movie 
        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            //Only adds if it the model state is valid
            if (ModelState.IsValid)
            {
                //Add movie to context
                _context.Movies.Add(movie);
                _context.SaveChanges();
                return View("Confirmation", movie);
            }
            return View(movie);

        }

        //returns list of all movies
        public IActionResult ViewMovies()
        {
            return View(_context.Movies.Where(x => x.Title != "Independence Day"));
        }

        //Returns view of form to edit a movie
        [HttpPost]
        public IActionResult EditMovie(int MovieId)
        {
            //Finds all movies with the same ID
            IQueryable<Movie> movies = _context.Movies.Where(x => x.MovieId == MovieId);
            Movie movie = new Movie();
            //sets variable equal to the movie that was found
            foreach (Movie m in movies)
            {
                movie = m;
            }
            //send movie into the view so that it can autopopulate values
            return View(movie);
        }

        //link to podcast
        public IActionResult MyPodcasts()
        {
            return View();
        }

        //actually update the movie in the database
        [HttpPost]
        public IActionResult UpdateMovie(Movie m)
        {
            //Only update if the model that they typed in was valid
            if (ModelState.IsValid)
            {
                //Find all matching ID's
                IQueryable<Movie> movies = _context.Movies.Where(x => x.MovieId == m.MovieId);
                //Remove the model from the database
                foreach (Movie movie in movies)
                {
                    _context.Movies.Remove(movie);

                }
                //Add the new movie that was sent in
                _context.Movies.Add(m);

                _context.SaveChanges();

                return View("ViewMovies", _context.Movies.Where(x => x.Title != "Independence Day"));
            }
            return View("EditMovie", m);
        }


        //Delete movies
        public IActionResult DeleteMovie(int MovieId)
        {
            //Find movies where the ID is the same
            IQueryable<Movie> movies = _context.Movies.Where(x => x.MovieId == MovieId);
            foreach (Movie m in movies)
            {
                //Remove them from the database
                _context.Remove(m);
            }
            _context.SaveChanges();
            return View("ViewMovies", _context.Movies);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
