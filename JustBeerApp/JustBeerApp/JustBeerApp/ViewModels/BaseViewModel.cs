using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JustBeerApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService _navigationService;
        public IPageDialogService _pageDialogService;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
