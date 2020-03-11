using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JustBeerApp.ViewModels
{
    public class RandomBeerPageViewModel : BaseViewModel
    {
        IApiBeerService _apiService = new ApiBeerService();
        public Data RandomBeer { get; set; } = new Data();
        public DelegateCommand GetRandomBeer {get; set;}

        public RandomBeerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            GetRandomBeer = new DelegateCommand(async () =>
            {
                await GetBeerData();
            });

        }
        async Task GetBeerData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    RandomBeer = await _apiService.GetRandomBeers();
                    var x = 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Config.NullMessage, Config.NoInternetAlertMessage, Config.CancelMessage);
            }
        }
    }
}
