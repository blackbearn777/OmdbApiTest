using OmdbApiProject.Data;
using OmdbApiProject.Interfaces;
using OmdbApiProject.Models;
using OmdbApiProject.Services;
using OmdbApiProject.ViewModels;
using OmdbApiProject.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Injection;

namespace OmdbApiProject
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<MovieRepository>();
            container.RegisterType<MainWindow>();
            container.RegisterType<FavouritesViewModel>();
            container.RegisterType<SearchViewModel>();

            container.RegisterType<StateContext>(TypeLifetime.Singleton);
            container.RegisterType<MovieDbContext>(TypeLifetime.Singleton);
            container.RegisterType<ApiService>();
            container.RegisterType<SearchView>();
            container.RegisterType<FavouritesView>();
            container.AddExtension(new Diagnostic());
            container.Resolve<MainWindow>().Show();
        }

    }
}
