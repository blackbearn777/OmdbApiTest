using GalaSoft.MvvmLight;
using OmdbApiProject.Entities;
using OmdbApiProject.Interfaces;
using OmdbApiProject.Models;
using OmdbApiProject.Services;
using OmdbApiProject.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace OmdbApiProject.ViewModels
{
    public class FavouritesViewModel : ObservableObject, IState
    {
        public UserControl CurrentStateControl { get; set; }
        public StateContext StateContext { get; set; }
        public Action<IState> ChangeState { get; set; }
        public MovieRepository MovieRepository { get; set; }
        public RelayCommands GoToSearch { get; set; }
        public RelayCommands<int> DeleteFavouriteItem { get; set; }
        public ObservableCollection<Movie> FavouriteMovie { get; set; }
        public SearchViewModel SearchViewModel { get; set; }

        public FavouritesViewModel(FavouritesView favouritesView, StateContext stateContext,  MovieRepository movieRepository)
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

        public void GoToSearchUserControl()
        {
            ChangeState.Invoke(SearchViewModel);
        }
    }
}
    

