using GalaSoft.MvvmLight;
using OmdbApiProject.Data;
using OmdbApiProject.Interfaces;
using OmdbApiProject.Models;
using OmdbApiProject.Services;
using OmdbApiProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OmdbApiProject.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public IState CurrentState { get; set; }
        private UIElementCollection Scen { get; set; }
        public RelayCommands<UIElementCollection> GetChildrenCommand { get; set; }
        private Action<IState> ChangeState { get; set; }


        public MainWindowViewModel(SearchViewModel searchViewModel, FavouritesViewModel favouritesViewModel)
        {

            CurrentState = searchViewModel;

            GetChildrenCommand = new RelayCommands<UIElementCollection>(child =>
            {
                Scen = child;
                Scen?.Clear();
                Scen.Add(CurrentState.CurrentStateControl);
            });


            ChangeState = SetNewScen;
            searchViewModel.ChangeState = ChangeState;
            favouritesViewModel.ChangeState = ChangeState;
            searchViewModel.FavouritesViewModel = favouritesViewModel;
            favouritesViewModel.SearchViewModel = searchViewModel;
        }

        private void SetNewScen(IState newState)
        {
            CurrentState = newState;
            Scen.Clear();
            Scen.Add(CurrentState.CurrentStateControl);
        }
    }
}
