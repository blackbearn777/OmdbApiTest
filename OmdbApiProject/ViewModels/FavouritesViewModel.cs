using GalaSoft.MvvmLight;
using OmdbApiProject.Entities;
using OmdbApiProject.Interfaces;
using OmdbApiProject.Models;
using OmdbApiProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace OmdbApiProject.ViewModels
{
    public class FavouritesViewModel : ObservableObject, IState
    {
        public FavouritesViewModel(FavouritesView favouritesView, StateContext stateContext, MovieRepository movieRepository)
        {
            CurrentStateControl = favouritesView;
            MovieRepository = movieRepository;
            CurrentStateControl = new FavouritesView();
            CurrentStateControl.DataContext = this;
            StateContext = stateContext;
            GoToSearch = new RelayCommands(GoToSearchUserControl);
            DeleteFavouriteItem = new RelayCommands<int>(DeleteFavourite);
            FavouriteMovie = StateContext.FavouritesMovies;
        }

        public Action<IState> ChangeState { get; set; }
        public UserControl CurrentStateControl { get; set; }
        public RelayCommands<int> DeleteFavouriteItem { get; set; }
        public ObservableCollection<Movie> FavouriteMovie { get; set; }
        public RelayCommands GoToSearch { get; set; }
        public SearchViewModel SearchViewModel { get; set; }
        public StateContext StateContext { get; set; }
        private MovieRepository MovieRepository { get; set; }
        public void GoToSearchUserControl()
        {
            ChangeState.Invoke(SearchViewModel);
        }

        private async void DeleteFavourite(int id)
        {
            var selectedItem = FavouriteMovie.FirstOrDefault(m => m.Id == id);
            if (selectedItem != null)
            {
                selectedItem.IsFavorited = !selectedItem.IsFavorited;
                await MovieRepository.UpdateMovie(selectedItem);
                FavouriteMovie.Remove(selectedItem);
            }
        }
    }
}