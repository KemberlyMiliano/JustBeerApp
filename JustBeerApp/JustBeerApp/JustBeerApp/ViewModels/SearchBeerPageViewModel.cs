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
    public class SearchBeerPageViewModel : BaseViewModel
    {
        IApiBeerService _apiBeer = new ApiBeerService();
        public string BeerId { get; set; }
        public Beer BeerInfo { get; set; }
        public Data Data { get; set; } = new Data();
        public DelegateCommand Search { get; set; }
        public SearchBeerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            Search = new DelegateCommand(async () =>
            {
                await GetBeerIngredientData();
            });
        }
        async Task GetBeerIngredientData()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                { 
                    Data = await _apiBeer.GetBeers(BeerId);
                    //BeerInfo = new Beer(GetData.Beer);


                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                    await App.Current.MainPage.DisplayAlert("error", $"{ex}", "ok");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alet", "No tienes Conexion a internet", "Ok");
            }
        }
    }
}
