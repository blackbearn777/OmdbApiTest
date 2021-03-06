﻿using GalaSoft.MvvmLight;
using OmdbApiProject.Entities;
using OmdbApiProject.Interfaces;
using OmdbApiProject.Models;
using OmdbApiProject.Services;
using OmdbApiProject.Views;
using System;
using System.Linq;
using System.Windows.Controls;

namespace OmdbApiProject.ViewModels
{
    public class SearchViewModel : ObservableObject, IState
    {
        private ApiService _apiService;

        public SearchViewModel(ApiService apiService, MovieRepository movieRepository, StateContext stateContext, SearchView searchView)
        {
            CurrentStateControl = searchView;
            CurrentStateControl.DataContext = this;
            MoviesRepository = movieRepository;
            _apiService = apiService;

            GoToFavouriteCommand = new RelayCommands(GoToFavourite);
            SearchCommand = new RelayCommands(Search);
            AddToFavouriteCommand = new RelayCommands(AddToFavourite);
            StateContext = stateContext;

            var t = MoviesRepository.GetMovies();
            t.ToList().ForEach(e => StateContext.Movies.Add(e));
        }

        public RelayCommands AddToFavouriteCommand { get; set; }
        public Action<IState> ChangeState { get; set; }
        public UserControl CurrentStateControl { get; set; }
        public FavouritesViewModel FavouritesViewModel { get; set; }
        public RelayCommands GoToFavouriteCommand { get; set; }
        public string InputSearch { get; set; }
        public RelayCommands SearchCommand { get; set; }
        public ItemMovieResponse SelectedItemMovie { get; set; }
        public StateContext StateContext { get; set; }
        private MovieRepository MoviesRepository { get; set; }

        public void GoToFavourite()
        {
            StateContext.UpdateFavourites();
            ChangeState.Invoke(FavouritesViewModel);
        }

        private async void AddToFavourite()
        {
            var responseMovie = await _apiService.GetMovieById(SelectedItemMovie.imdbID);
            if (responseMovie.ImdbID != null)
            {
                var exists = MoviesRepository.IsExist(responseMovie);
                if (!exists)
                {
                    responseMovie.IsFavorited = !responseMovie.IsFavorited;
                    await MoviesRepository.AddMovie(responseMovie);
                    StateContext.Movies.Add(responseMovie);
                }
            }
        }

        private async void Search()
        {
            StateContext.ItemMovieResponses.Clear();
            if (!string.IsNullOrEmpty(InputSearch))
            {
                var responseMovie = await _apiService.GetMovieBySearch(InputSearch.ToLower());
                responseMovie.Search.ForEach(e => StateContext.ItemMovieResponses.Add(e));
                InputSearch = "";
                RaisePropertyChanged(nameof(InputSearch));
            }
        }
    }
}