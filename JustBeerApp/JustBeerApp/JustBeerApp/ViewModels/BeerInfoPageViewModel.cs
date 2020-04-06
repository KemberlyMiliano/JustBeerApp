using JustBeerApp.Models;
using JustBeerApp.Services;
//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace JustBeerApp.ViewModels
{
    public class BeerInfoPageViewModel : BaseViewModel, IInitialize
    {
        protected IApiTestManager ApiTestManager = new ApiTestManager();
        public string FavoriteIcon { get; set; }
        public string ID { get; set; }
        public Datum NewBeer { get; set; } = new Datum();
        public Data BeerInfo { get; set; }
        public DelegateCommand AddToFavoritesCommand { get; set; }
        public DelegateCommand GetBeerInformation { get; set; }
        public ObservableCollection<Datum> FavoriteList { get; set; }
        public BeerInfoPageViewModel(INavigationService navigation, IApiBeerService apiService, IPageDialogService pageDialogService) : base(navigation, apiService, pageDialogService)
        {
            FavoriteList = new ObservableCollection<Datum>();
            FavoriteIcon = "favoritesIcon.png";
            AddToFavoritesCommand = new DelegateCommand(async () =>
            {
                await AddToFavorites(NewBeer);
            });

            GetBeerInformation = new DelegateCommand(async () =>
            {
                await GetBeerInfo();
            });

            GetBeerInformation.Execute();
        }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Beer"))
            {
                NewBeer = parameters["Beer"] as Datum;  
            }
        }
        public async Task GetBeerInfo()
        {
            bool internetAccess = await CheckInternetConnection();

            if (internetAccess)
            {
                try
                {
                    BeerInfo = await ApiService.GetBeers(ID);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API EXCEPTION {ex}");
                }
            }
        }

        public async Task AddToFavorites(Datum beerInformation)
        {
            FavoriteList.Add(beerInformation);
            await ApiTestManager.GetBeersAsync(beerInformation.Id);
            FavoriteIcon = FavoriteList.Contains(beerInformation) ? "orangeHeart.png" : "favoritesIcon.png";
        }
    }
}
