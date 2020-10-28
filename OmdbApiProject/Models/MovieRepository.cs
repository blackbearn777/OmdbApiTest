using OmdbApiProject.Data;
using OmdbApiProject.Entities;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace OmdbApiProject.Models
{
    public class MovieRepository
    {
        private MovieDbContext _movieDbContext;
        public MovieRepository(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }
        public IEnumerable<Movie> GetMovies()
        {
            return _movieDbContext.Movies.ToList();
        }
        public bool IsExist(Movie movie)
        {
            var res = _movieDbContext.Movies.FirstOrDefault(a => a.ImdbID == movie.ImdbID);
            return res == null ? false : true;
        }
        public async Task UpdateMovie(Movie movie)
        {
            _movieDbContext.Movies.AddOrUpdate(movie);
            await SaveChanges();
        }
        public async Task AddMovie(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
            await SaveChanges();
        }
        public async Task SaveChanges()
        {
            await _movieDbContext.SaveChangesAsync();
        }
    }
}
