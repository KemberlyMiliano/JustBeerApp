using JustBeerApp.Models;
using JustBeerApp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JustBeerApp.ViewModels
{
    public class FavoritesBeerInfoPageViewModel : BaseViewModel , INavigationAware
    {
        protected IApiTestManager ApiTestManager = new ApiTestManager();
        public Beer FavBeerInfo { get; set; } = new Beer();
        public FavoritesBeerInfoPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            FavBeerInfo = parameters["BeerInfo"] as Beer;

        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            FavBeerInfo = parameters["Beer"] as Beer;
        }
    }
}
