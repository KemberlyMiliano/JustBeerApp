using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustBeerApp.ViewModels
{
    public class RandomBeerPageViewModel : BaseViewModel
    {
        public RandomBeerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }
    }
}
