using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JustBeerApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<Data> HomeBeers { get; set; } = new ObservableCollection<Data>();
        public HomePageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            for (int i = 0; i < 4; i++)
            {
                _ = GetBeerData();
                HomeBeers.Add(RandomBeer);
            }

        }
    }
}
