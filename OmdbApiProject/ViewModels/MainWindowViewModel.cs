using GalaSoft.MvvmLight;
using OmdbApiProject.Interfaces;
using System;
using System.Windows.Controls;

namespace OmdbApiProject.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
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

        public IState CurrentState { get; set; }
        public RelayCommands<UIElementCollection> GetChildrenCommand { get; set; }
        private Action<IState> ChangeState { get; set; }
        private UIElementCollection Scen { get; set; }
        private void SetNewScen(IState newState)
        {
            CurrentState = newState;
            Scen.Clear();
            Scen.Add(CurrentState.CurrentStateControl);
        }
    }
}