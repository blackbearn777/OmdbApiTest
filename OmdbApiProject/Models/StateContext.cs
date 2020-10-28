using OmdbApiProject.Entities;
using System.Collections.ObjectModel;
using System.Linq;


namespace OmdbApiProject.Models
{
    public class StateContext
    {
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> FavouritesMovies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<ItemMovieResponse> ItemMovieResponses { get; set; } = new ObservableCollection<ItemMovieResponse>();

        public void UpdateFavourites()
        {
            FavouritesMovies.Clear();
            Movies.ToList().Where(a => a.IsFavorited == true).ToList().ForEach(a => FavouritesMovies.Add(a));
        }

    }
}
