using JustBeerApp.Services;
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

        protected INavigationService _navigationService;
        protected IPageDialogService _pageDialogService;
        protected IApiBeerService ApiBeerService { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
