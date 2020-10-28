using OmdbApiProject.Models;
using System;
using System.Windows.Controls;

namespace OmdbApiProject.Interfaces
{
    public interface IState
    {
        UserControl CurrentStateControl { get; set; }
        StateContext StateContext { get; set; }
        Action<IState> ChangeState { get; set; }
    }
}
