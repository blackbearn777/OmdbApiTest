using OmdbApiProject.Entities;
using System.Data.Entity;

namespace OmdbApiProject.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies{get;set;}
        public MovieDbContext() :base("DefaultConnection")
        {

        }
    }
}
